namespace Crystal.Input;

public sealed class InputFrame
{
    public InputFrame(int gamePadIndex = 0)
    {
        ArgumentOutOfRangeException.ThrowIfNegative(gamePadIndex);
        ArgumentOutOfRangeException.ThrowIfGreaterThanOrEqual(gamePadIndex, Input.GamePad.MaximumGamePadCount);
        GamePadIndex = gamePadIndex;
    }

    public int GamePadIndex { get; }

    public KeyboardState Keyboard { get; private set; }

    public KeyboardState PreviousKeyboard { get; private set; }

    public MouseState Mouse { get; private set; }

    public MouseState PreviousMouse { get; private set; }

    public GamePadState GamePad { get; private set; }

    public GamePadState PreviousGamePad { get; private set; }

    public Point MouseMovement => new(Mouse.X - PreviousMouse.X, Mouse.Y - PreviousMouse.Y);

    public Point RelativeMovement => new(Mouse.RelativeX - PreviousMouse.RelativeX, Mouse.RelativeY - PreviousMouse.RelativeY);

    public float ScrollNotches => (Mouse.ScrollWheelValue - PreviousMouse.ScrollWheelValue) / (float)Input.Mouse.WheelDelta;

    public float HorizontalScrollNotches => (Mouse.HorizontalScrollWheelValue - PreviousMouse.HorizontalScrollWheelValue) / (float)Input.Mouse.WheelDelta;

    public void Update() => Advance(Input.Keyboard.GetState(), Input.Mouse.GetState(), Input.GamePad.GetState(GamePadIndex));

    public void Advance(KeyboardState keyboard, MouseState mouse, GamePadState gamePad)
    {
        PreviousKeyboard = Keyboard;
        PreviousMouse = Mouse;
        PreviousGamePad = GamePad;
        Keyboard = keyboard;
        Mouse = mouse;
        GamePad = gamePad;
    }

    public bool IsKeyPressed(Keys key) => Keyboard.IsKeyDown(key) && PreviousKeyboard.IsKeyUp(key);

    public bool IsKeyReleased(Keys key) => Keyboard.IsKeyUp(key) && PreviousKeyboard.IsKeyDown(key);

    public bool IsMouseButtonDown(MouseButton button) => IsDown(Mouse, button);

    public bool IsMouseButtonPressed(MouseButton button) => IsDown(Mouse, button) && !IsDown(PreviousMouse, button);

    public bool IsMouseButtonReleased(MouseButton button) => !IsDown(Mouse, button) && IsDown(PreviousMouse, button);

    public bool IsButtonPressed(Buttons buttons) => GamePad.IsButtonDown(buttons) && !PreviousGamePad.IsButtonDown(buttons);

    public bool IsButtonReleased(Buttons buttons) => !GamePad.IsButtonDown(buttons) && PreviousGamePad.IsButtonDown(buttons);

    public bool IsDown(InputBinding binding) => IsDown(binding, Keyboard, Mouse, GamePad);

    public bool WasDown(InputBinding binding) => IsDown(binding, PreviousKeyboard, PreviousMouse, PreviousGamePad);

    private static bool IsDown(InputBinding binding, KeyboardState keyboard, MouseState mouse, GamePadState gamePad) => binding.Kind switch
    {
        InputBindingKind.Key => keyboard.IsKeyDown(binding.Key),
        InputBindingKind.MouseButton => IsDown(mouse, binding.MouseButton),
        InputBindingKind.GamePadButton => gamePad.IsButtonDown(binding.GamePadButton),
        _ => false,
    };

    private static bool IsDown(MouseState mouse, MouseButton button) => button switch
    {
        MouseButton.Left => mouse.LeftButton == ButtonState.Pressed,
        MouseButton.Middle => mouse.MiddleButton == ButtonState.Pressed,
        MouseButton.Right => mouse.RightButton == ButtonState.Pressed,
        MouseButton.XButton1 => mouse.XButton1 == ButtonState.Pressed,
        MouseButton.XButton2 => mouse.XButton2 == ButtonState.Pressed,
        _ => false,
    };
}
