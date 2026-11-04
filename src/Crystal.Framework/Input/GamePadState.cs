using System.Numerics;

namespace Crystal.Input;

[Flags]
public enum Buttons
{
    None = 0,
    DPadUp = 1 << 0,
    DPadDown = 1 << 1,
    DPadLeft = 1 << 2,
    DPadRight = 1 << 3,
    Start = 1 << 4,
    Back = 1 << 5,
    LeftStick = 1 << 6,
    RightStick = 1 << 7,
    LeftShoulder = 1 << 8,
    RightShoulder = 1 << 9,
    BigButton = 1 << 11,
    A = 1 << 12,
    B = 1 << 13,
    X = 1 << 14,
    Y = 1 << 15,
    RightTrigger = 1 << 22,
    LeftTrigger = 1 << 23,
}

public enum GamePadDeadZone
{
    None,
    IndependentAxes,
    Circular,
}

public readonly record struct GamePadButtons(Buttons Pressed)
{
    public ButtonState A => StateOf(Buttons.A);
    public ButtonState B => StateOf(Buttons.B);
    public ButtonState X => StateOf(Buttons.X);
    public ButtonState Y => StateOf(Buttons.Y);
    public ButtonState Back => StateOf(Buttons.Back);
    public ButtonState Start => StateOf(Buttons.Start);
    public ButtonState BigButton => StateOf(Buttons.BigButton);
    public ButtonState LeftShoulder => StateOf(Buttons.LeftShoulder);
    public ButtonState RightShoulder => StateOf(Buttons.RightShoulder);
    public ButtonState LeftStick => StateOf(Buttons.LeftStick);
    public ButtonState RightStick => StateOf(Buttons.RightStick);

    private ButtonState StateOf(Buttons button) => (Pressed & button) != 0 ? ButtonState.Pressed : ButtonState.Released;
}

public readonly record struct GamePadDPad(ButtonState Up, ButtonState Down, ButtonState Left, ButtonState Right);

public readonly record struct GamePadThumbSticks(Vector2 Left, Vector2 Right);

public readonly record struct GamePadTriggers(float Left, float Right);

public readonly record struct GamePadState(bool IsConnected, Buttons PressedButtons, GamePadThumbSticks ThumbSticks, GamePadTriggers Triggers)
{
    public GamePadButtons Buttons => new(PressedButtons);

    public GamePadDPad DPad => new(StateOf(Input.Buttons.DPadUp), StateOf(Input.Buttons.DPadDown), StateOf(Input.Buttons.DPadLeft), StateOf(Input.Buttons.DPadRight));

    public bool IsButtonDown(Buttons buttons) => buttons != Input.Buttons.None && (PressedButtons & buttons) == buttons;

    public bool IsButtonUp(Buttons buttons) => (PressedButtons & buttons) == Input.Buttons.None;

    private ButtonState StateOf(Buttons button) => (PressedButtons & button) != 0 ? ButtonState.Pressed : ButtonState.Released;
}
