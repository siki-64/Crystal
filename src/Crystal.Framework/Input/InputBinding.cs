namespace Crystal.Input;

public enum MouseButton
{
    Left,
    Middle,
    Right,
    XButton1,
    XButton2,
}

public enum InputBindingKind
{
    None,
    Key,
    MouseButton,
    GamePadButton,
}

public readonly record struct InputBinding
{
    private InputBinding(InputBindingKind kind, Keys key, MouseButton mouseButton, Buttons gamePadButton)
    {
        Kind = kind;
        Key = key;
        MouseButton = mouseButton;
        GamePadButton = gamePadButton;
    }

    public InputBindingKind Kind { get; }

    public Keys Key { get; }

    public MouseButton MouseButton { get; }

    public Buttons GamePadButton { get; }

    public static InputBinding FromKey(Keys key) => new(InputBindingKind.Key, key, default, Buttons.None);

    public static InputBinding FromMouseButton(MouseButton button) => new(InputBindingKind.MouseButton, Keys.None, button, Buttons.None);

    public static InputBinding FromGamePadButton(Buttons button) => new(InputBindingKind.GamePadButton, Keys.None, default, button);

    public static implicit operator InputBinding(Keys key) => FromKey(key);

    public static implicit operator InputBinding(MouseButton button) => FromMouseButton(button);

    public static implicit operator InputBinding(Buttons button) => FromGamePadButton(button);
}
