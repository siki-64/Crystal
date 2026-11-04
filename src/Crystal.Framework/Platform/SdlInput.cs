using System.Numerics;
using System.Runtime.InteropServices;
using System.Text;
using Crystal.Input;

namespace Crystal.Platform;

internal static unsafe class SdlInput
{
    private const float AxisScale = 1f / 32767f;
    private const int MaximumStackTextLength = 256;

    private static readonly SdlGamepad[] Gamepads = new SdlGamepad[GamePad.MaximumGamePadCount];
    private static readonly uint[] GamepadJoystickIds = new uint[GamePad.MaximumGamePadCount];

    private static readonly (SdlGamepadButton Source, Buttons Target)[] ButtonMap =
    [
        (SdlGamepadButton.South, Buttons.A),
        (SdlGamepadButton.East, Buttons.B),
        (SdlGamepadButton.West, Buttons.X),
        (SdlGamepadButton.North, Buttons.Y),
        (SdlGamepadButton.Back, Buttons.Back),
        (SdlGamepadButton.Guide, Buttons.BigButton),
        (SdlGamepadButton.Start, Buttons.Start),
        (SdlGamepadButton.LeftStick, Buttons.LeftStick),
        (SdlGamepadButton.RightStick, Buttons.RightStick),
        (SdlGamepadButton.LeftShoulder, Buttons.LeftShoulder),
        (SdlGamepadButton.RightShoulder, Buttons.RightShoulder),
        (SdlGamepadButton.DPadUp, Buttons.DPadUp),
        (SdlGamepadButton.DPadDown, Buttons.DPadDown),
        (SdlGamepadButton.DPadLeft, Buttons.DPadLeft),
        (SdlGamepadButton.DPadRight, Buttons.DPadRight),
    ];

    public static void Reset()
    {
        for (var slot = 0; slot < Gamepads.Length; slot++) CloseGamepad(slot);
        Keyboard.Reset();
        Mouse.Reset();
    }

    public static bool Process(in SdlEvent sdlEvent, GameWindow window)
    {
        switch (sdlEvent.Type)
        {
            case SdlEventType.KeyDown:
            case SdlEventType.KeyUp:
                Keyboard.SetKey((Keys)sdlEvent.Key.Scancode, sdlEvent.Key.Down != 0);
                return true;
            case SdlEventType.TextInput:
                RaiseTextInput(window, sdlEvent.Text.Text);
                return true;
            case SdlEventType.MouseMotion:
                SetMousePosition(window, sdlEvent.Motion.X, sdlEvent.Motion.Y);
                Mouse.Move(sdlEvent.Motion.RelativeX * window.PixelDensity, sdlEvent.Motion.RelativeY * window.PixelDensity);
                return true;
            case SdlEventType.MouseButtonDown:
            case SdlEventType.MouseButtonUp:
                SetMousePosition(window, sdlEvent.Button.X, sdlEvent.Button.Y);
                SetMouseButton(sdlEvent.Button.Button, sdlEvent.Button.Down != 0);
                return true;
            case SdlEventType.MouseWheel:
                Mouse.Scroll(sdlEvent.Wheel.X, sdlEvent.Wheel.Y);
                return true;
            case SdlEventType.GamepadAdded:
                OpenGamepad(sdlEvent.GamepadDevice.Which);
                return true;
            case SdlEventType.GamepadRemoved:
                var slot = Array.IndexOf(GamepadJoystickIds, sdlEvent.GamepadDevice.Which);
                if (slot >= 0) CloseGamepad(slot);
                return true;
            default:
                return false;
        }
    }

    public static GamePadState ReadGamePad(int slot)
    {
        var gamepad = Gamepads[slot];
        if (gamepad.IsNull) return default;

        var buttons = Buttons.None;
        foreach (var (source, target) in ButtonMap)
        {
            if (Sdl.GetGamepadButton(gamepad, source)) buttons |= target;
        }
        var leftTrigger = Sdl.GetGamepadAxis(gamepad, SdlGamepadAxis.LeftTrigger) * AxisScale;
        var rightTrigger = Sdl.GetGamepadAxis(gamepad, SdlGamepadAxis.RightTrigger) * AxisScale;
        if (leftTrigger > 0) buttons |= Buttons.LeftTrigger;
        if (rightTrigger > 0) buttons |= Buttons.RightTrigger;

        var thumbSticks = new GamePadThumbSticks(ReadStick(gamepad, SdlGamepadAxis.LeftX, SdlGamepadAxis.LeftY), ReadStick(gamepad, SdlGamepadAxis.RightX, SdlGamepadAxis.RightY));
        return new GamePadState(true, buttons, thumbSticks, new GamePadTriggers(leftTrigger, rightTrigger));
    }

    private static Vector2 ReadStick(SdlGamepad gamepad, SdlGamepadAxis x, SdlGamepadAxis y) =>
        new(MathF.Max(Sdl.GetGamepadAxis(gamepad, x) * AxisScale, -1f), -MathF.Max(Sdl.GetGamepadAxis(gamepad, y) * AxisScale, -1f));

    private static void RaiseTextInput(GameWindow window, byte* text)
    {
        if (text == null || !window.HasTextInputHandlers) return;
        var utf8 = MemoryMarshal.CreateReadOnlySpanFromNullTerminated(text);
        Span<char> characters = utf8.Length <= MaximumStackTextLength ? stackalloc char[utf8.Length] : new char[utf8.Length];
        var count = Encoding.UTF8.GetChars(utf8, characters);
        foreach (var character in characters[..count]) window.OnTextInput(character);
    }

    private static void SetMousePosition(GameWindow window, float x, float y)
    {
        var density = window.PixelDensity;
        Mouse.State = Mouse.State with { X = (int)MathF.Floor(x * density), Y = (int)MathF.Floor(y * density) };
    }

    private static void SetMouseButton(byte button, bool down)
    {
        var state = down ? ButtonState.Pressed : ButtonState.Released;
        Mouse.State = button switch
        {
            SdlMouseButton.Left => Mouse.State with { LeftButton = state },
            SdlMouseButton.Middle => Mouse.State with { MiddleButton = state },
            SdlMouseButton.Right => Mouse.State with { RightButton = state },
            SdlMouseButton.X1 => Mouse.State with { XButton1 = state },
            SdlMouseButton.X2 => Mouse.State with { XButton2 = state },
            _ => Mouse.State,
        };
    }

    private static void OpenGamepad(uint joystickId)
    {
        if (Array.IndexOf(GamepadJoystickIds, joystickId) >= 0) return;
        var slot = Array.FindIndex(Gamepads, gamepad => gamepad.IsNull);
        if (slot < 0) return;
        var gamepad = Sdl.OpenGamepad(joystickId);
        if (gamepad.IsNull) return;
        Sdl.SetGamepadPlayerIndex(gamepad, slot);
        Gamepads[slot] = gamepad;
        GamepadJoystickIds[slot] = joystickId;
    }

    private static void CloseGamepad(int slot)
    {
        Gamepads[slot].Dispose();
        Gamepads[slot] = default;
        GamepadJoystickIds[slot] = 0;
    }
}
