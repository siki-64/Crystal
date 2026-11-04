namespace Crystal.Input;

public static class Keyboard
{
    private static KeyboardState _state;

    public static KeyboardState GetState() => _state;

    internal static void SetKey(Keys key, bool down) => _state.SetKey(key, down);

    internal static void Reset() => _state = default;
}
