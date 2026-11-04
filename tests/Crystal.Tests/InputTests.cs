using System.Numerics;
using Crystal.Input;
using Crystal.Platform;

namespace Crystal.Tests;

[TestClass]
public sealed unsafe class InputTests
{
    [TestMethod]
    public void KeyboardStateTracksPressedKeys()
    {
        var state = new KeyboardState(Keys.Escape, Keys.LeftShift, Keys.F12);

        Assert.IsTrue(state.IsKeyDown(Keys.Escape));
        Assert.IsTrue(state.IsKeyUp(Keys.RightShift));
        Assert.IsFalse(state.IsKeyDown((Keys)(-1)));
        Assert.IsFalse(state.IsKeyDown((Keys)KeyboardState.KeyCount));
        Assert.AreEqual(3, state.GetPressedKeyCount());
        Span<Keys> pressed = stackalloc Keys[4];
        Assert.AreEqual(3, state.GetPressedKeys(pressed));
        CollectionAssert.AreEqual(new[] { Keys.Escape, Keys.F12, Keys.LeftShift }, pressed[..3].ToArray());
        Assert.AreEqual(1, state.GetPressedKeys(pressed[..1]));
        Assert.AreEqual(new KeyboardState(Keys.F12, Keys.LeftShift, Keys.Escape), state);
        Assert.AreNotEqual(new KeyboardState(Keys.Escape), state);
        Assert.AreEqual(default, new KeyboardState());
    }

    [TestMethod]
    public void KeyboardEventsUpdateTheKeyboardState() => TestGame.RunFrames((device, frame) =>
    {
        switch (frame)
        {
            case 0:
                Assert.AreEqual(0, Keyboard.GetState().GetPressedKeyCount());
                PushKey(Keys.A, true);
                PushKey(Keys.LeftShift, true);
                break;
            case 1:
                Assert.IsTrue(Keyboard.GetState().IsKeyDown(Keys.A));
                Assert.IsTrue(Keyboard.GetState().IsKeyDown(Keys.LeftShift));
                PushKey(Keys.A, false);
                break;
            case 2:
                Assert.IsTrue(Keyboard.GetState().IsKeyUp(Keys.A));
                Assert.IsTrue(Keyboard.GetState().IsKeyDown(Keys.LeftShift));
                break;
        }
    }, frameCount: 3);

    [TestMethod]
    public void MouseEventsUpdateTheMouseState() => TestGame.RunFrames((device, frame) =>
    {
        switch (frame)
        {
            case 0:
                Assert.AreEqual(default, Mouse.GetState());
                PushMouseMotion(10.5f, 20.25f);
                PushMouseButton(SdlMouseButton.Left, true, 10.5f, 20.25f);
                PushMouseButton(SdlMouseButton.X2, true, 10.5f, 20.25f);
                PushMouseWheel(0f, 1f);
                PushMouseWheel(0.5f, -0.5f);
                break;
            case 1:
                var density = Mouse.Window!.PixelDensity;
                var state = Mouse.GetState();
                Assert.AreEqual((int)MathF.Floor(10.5f * density), state.X);
                Assert.AreEqual((int)MathF.Floor(20.25f * density), state.Y);
                Assert.AreEqual(new Point(state.X, state.Y), state.Position);
                Assert.AreEqual(ButtonState.Pressed, state.LeftButton);
                Assert.AreEqual(ButtonState.Released, state.RightButton);
                Assert.AreEqual(ButtonState.Pressed, state.XButton2);
                Assert.AreEqual(Mouse.WheelDelta / 2, state.ScrollWheelValue);
                Assert.AreEqual(Mouse.WheelDelta / 2, state.HorizontalScrollWheelValue);
                PushMouseButton(SdlMouseButton.Left, false, 10.5f, 20.25f);
                break;
            case 2:
                Assert.AreEqual(ButtonState.Released, Mouse.GetState().LeftButton);
                Assert.AreEqual(ButtonState.Pressed, Mouse.GetState().XButton2);
                break;
        }
    }, frameCount: 3);

    [TestMethod]
    public void InputStateIsResetForEachGame()
    {
        TestGame.RunOnce(_ => PushKey(Keys.Space, true));
        TestGame.RunOnce(_ => Assert.IsTrue(Keyboard.GetState().IsKeyUp(Keys.Space)));
    }

    [TestMethod]
    public void VirtualGamepadUpdatesTheGamePadState()
    {
        Assert.IsTrue(Sdl.SetHint(Sdl.JoystickAllowBackgroundEventsHint, "1"));
        uint joystickId = 0;
        SdlJoystick joystick = default;
        try
        {
            TestGame.RunFrames((device, frame) =>
            {
                switch (frame)
                {
                    case 0:
                        Assert.IsFalse(GamePad.GetState(0).IsConnected);
                        joystickId = AttachVirtualGamepad();
                        joystick = Sdl.OpenJoystick(joystickId);
                        Assert.IsFalse(joystick.IsNull, Sdl.GetError());
                        break;
                    case 1:
                        Assert.IsTrue(GamePad.GetState(0).IsConnected);
                        Assert.AreEqual(default, GamePad.GetState(0).PressedButtons);
                        Assert.IsTrue(Sdl.SetJoystickVirtualButton(joystick, (int)SdlGamepadButton.South, true));
                        Assert.IsTrue(Sdl.SetJoystickVirtualButton(joystick, (int)SdlGamepadButton.DPadLeft, true));
                        Assert.IsTrue(Sdl.SetJoystickVirtualAxis(joystick, (int)SdlGamepadAxis.LeftX, short.MaxValue));
                        Assert.IsTrue(Sdl.SetJoystickVirtualAxis(joystick, (int)SdlGamepadAxis.LeftY, short.MinValue));
                        Assert.IsTrue(Sdl.SetJoystickVirtualAxis(joystick, (int)SdlGamepadAxis.RightX, 4000));
                        Assert.IsTrue(Sdl.SetJoystickVirtualAxis(joystick, (int)SdlGamepadAxis.RightTrigger, short.MaxValue));
                        break;
                    case 2:
                        var state = GamePad.GetState(0);
                        Assert.AreEqual(ButtonState.Pressed, state.Buttons.A);
                        Assert.AreEqual(ButtonState.Released, state.Buttons.B);
                        Assert.AreEqual(ButtonState.Pressed, state.DPad.Left);
                        Assert.IsTrue(state.IsButtonDown(Buttons.A | Buttons.DPadLeft | Buttons.RightTrigger));
                        Assert.IsTrue(state.IsButtonUp(Buttons.LeftTrigger));
                        Assert.AreEqual(new Vector2(1f, 1f), state.ThumbSticks.Left);
                        Assert.AreEqual(Vector2.Zero, state.ThumbSticks.Right);
                        Assert.AreEqual(1f, state.Triggers.Right);
                        Assert.AreEqual(4000f / 32767f, GamePad.GetState(0, GamePadDeadZone.None).ThumbSticks.Right.X, 1e-6f);
                        var circular = GamePad.GetState(0, GamePadDeadZone.Circular).ThumbSticks.Left;
                        Assert.AreEqual(1f, circular.Length(), 1e-6f);
                        Assert.AreEqual(circular.X, circular.Y);
                        Assert.IsFalse(GamePad.GetState(1).IsConnected);

                        var before = GC.GetAllocatedBytesForCurrentThread();
                        for (var poll = 0; poll < 100; poll++)
                        {
                            _ = GamePad.GetState(0);
                            _ = Keyboard.GetState();
                            _ = Mouse.GetState();
                        }
                        Assert.AreEqual(0, GC.GetAllocatedBytesForCurrentThread() - before);

                        joystick.Dispose();
                        joystick = default;
                        Assert.IsTrue(Sdl.DetachVirtualJoystick(joystickId));
                        joystickId = 0;
                        break;
                    case 3:
                        Assert.IsFalse(GamePad.GetState(0).IsConnected);
                        break;
                }
            }, frameCount: 4);
        }
        finally
        {
            Assert.IsTrue(Sdl.SetHint(Sdl.JoystickAllowBackgroundEventsHint, "0"));
        }
        Assert.ThrowsExactly<ArgumentOutOfRangeException>(() => GamePad.GetState(GamePad.MaximumGamePadCount));
    }

    [TestMethod]
    public void TextInputEventsRaiseCharacters()
    {
        using var game = new TestGame((_, _) => { });
        var received = new List<char>();
        game.Window.TextInput += (_, input) => received.Add(input.Character);

        fixed (byte* text = "aé\U0001F600\0"u8)
        {
            var sdlEvent = new SdlEvent();
            sdlEvent.Text = new SdlTextInputEvent { Type = SdlEventType.TextInput, Text = text };
            Assert.IsTrue(SdlInput.Process(sdlEvent, game.Window));
        }

        CollectionAssert.AreEqual("aé\U0001F600".ToCharArray(), received.ToArray());
    }

    [TestMethod]
    public void TextInputCanBeToggled()
    {
        using var game = new TestGame((_, _) => { });
        Assert.IsFalse(game.Window.IsTextInputEnabled);
        game.Window.IsTextInputEnabled = true;
        Assert.IsTrue(game.Window.IsTextInputEnabled);
        game.Window.IsTextInputEnabled = false;
        Assert.IsFalse(game.Window.IsTextInputEnabled);
    }

    private static uint AttachVirtualGamepad()
    {
        var name = "CRYSTAL_VirtualGamepad\0"u8;
        fixed (byte* namePointer = name)
        {
            var description = new SdlVirtualJoystickDescription
            {
                Version = (uint)sizeof(SdlVirtualJoystickDescription),
                Type = SdlVirtualJoystickDescription.GamepadType,
                AxisCount = (ushort)SdlGamepadAxis.Count,
                ButtonCount = (ushort)(SdlGamepadButton.DPadRight + 1),
                ButtonMask = (1u << (int)(SdlGamepadButton.DPadRight + 1)) - 1,
                AxisMask = (1u << (int)SdlGamepadAxis.Count) - 1,
                Name = namePointer,
            };
            var joystickId = Sdl.AttachVirtualJoystick(&description);
            Assert.AreNotEqual(0u, joystickId, Sdl.GetError());
            return joystickId;
        }
    }

    private static void PushKey(Keys key, bool down)
    {
        var sdlEvent = new SdlEvent();
        sdlEvent.Key = new SdlKeyboardEvent { Type = down ? SdlEventType.KeyDown : SdlEventType.KeyUp, Scancode = (uint)key, Down = down ? (byte)1 : (byte)0 };
        Push(&sdlEvent);
    }

    [TestMethod]
    public void RelativeMotionAccumulatesAndRelativeModeToggles() => TestGame.RunFrames((device, frame) =>
    {
        switch (frame)
        {
            case 0:
                Mouse.IsRelativeMode = true;
                Assert.IsTrue(Mouse.IsRelativeMode);
                PushMouseMotion(0f, 0f, 3f, -2f);
                PushMouseMotion(0f, 0f, 4f, 1f);
                break;
            case 1:
                var density = Mouse.Window!.PixelDensity;
                Assert.AreEqual((int)Math.Round(7f * density), Mouse.GetState().RelativeX);
                Assert.AreEqual((int)Math.Round(-1f * density), Mouse.GetState().RelativeY);
                Mouse.IsRelativeMode = false;
                Assert.IsFalse(Mouse.IsRelativeMode);
                break;
        }
    }, frameCount: 2);

    private static void PushMouseMotion(float x, float y, float relativeX = 0f, float relativeY = 0f)
    {
        var sdlEvent = new SdlEvent();
        sdlEvent.Motion = new SdlMouseMotionEvent { Type = SdlEventType.MouseMotion, X = x, Y = y, RelativeX = relativeX, RelativeY = relativeY };
        Push(&sdlEvent);
    }

    private static void PushMouseButton(byte button, bool down, float x, float y)
    {
        var sdlEvent = new SdlEvent();
        sdlEvent.Button = new SdlMouseButtonEvent
        {
            Type = down ? SdlEventType.MouseButtonDown : SdlEventType.MouseButtonUp,
            Button = button,
            Down = down ? (byte)1 : (byte)0,
            X = x,
            Y = y,
        };
        Push(&sdlEvent);
    }

    private static void PushMouseWheel(float x, float y)
    {
        var sdlEvent = new SdlEvent();
        sdlEvent.Wheel = new SdlMouseWheelEvent { Type = SdlEventType.MouseWheel, X = x, Y = y };
        Push(&sdlEvent);
    }

    private static void Push(SdlEvent* sdlEvent) => Assert.IsTrue(Sdl.PushEvent(sdlEvent), Sdl.GetError());
}
