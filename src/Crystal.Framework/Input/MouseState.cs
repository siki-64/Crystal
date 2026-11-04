namespace Crystal.Input;

public enum ButtonState
{
    Released,
    Pressed,
}

public readonly record struct MouseState(
    int X,
    int Y,
    int ScrollWheelValue,
    ButtonState LeftButton,
    ButtonState MiddleButton,
    ButtonState RightButton,
    ButtonState XButton1,
    ButtonState XButton2,
    int HorizontalScrollWheelValue = 0,
    int RelativeX = 0,
    int RelativeY = 0)
{
    public Point Position => new(X, Y);
}
