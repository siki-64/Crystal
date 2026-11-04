namespace Crystal.Input;

public static class Mouse
{
    public const int WheelDelta = 120;

    private static MouseState _state;
    private static double _scrollWheel;
    private static double _horizontalScrollWheel;
    private static double _relativeX;
    private static double _relativeY;
    private static bool _isRelativeMode;

    internal static GameWindow? Window { get; set; }

    public static MouseState GetState() => _state;

    public static void SetPosition(int x, int y)
    {
        var window = Window ?? throw new InvalidOperationException("Mouse.SetPosition needs a running Game.");
        window.WarpMouse(x, y);
        _state = _state with { X = x, Y = y };
    }

    public static bool IsRelativeMode
    {
        get => _isRelativeMode;
        set
        {
            var window = Window ?? throw new InvalidOperationException("Mouse.IsRelativeMode needs a running Game.");
            if (window.IsHosted) throw new InvalidOperationException("Mouse.IsRelativeMode needs a window the game owns.");
            window.SetRelativeMouseMode(value);
            _isRelativeMode = value;
        }
    }

    internal static void Move(float horizontalPixels, float verticalPixels)
    {
        _relativeX += horizontalPixels;
        _relativeY += verticalPixels;
        _state = _state with { RelativeX = (int)Math.Round(_relativeX), RelativeY = (int)Math.Round(_relativeY) };
    }

    internal static MouseState State
    {
        get => _state;
        set => _state = value;
    }

    internal static void Scroll(float horizontalNotches, float verticalNotches)
    {
        _scrollWheel += verticalNotches * WheelDelta;
        _horizontalScrollWheel += horizontalNotches * WheelDelta;
        _state = _state with
        {
            ScrollWheelValue = (int)Math.Round(_scrollWheel),
            HorizontalScrollWheelValue = (int)Math.Round(_horizontalScrollWheel),
        };
    }

    internal static void Reset()
    {
        _state = default;
        _scrollWheel = 0;
        _horizontalScrollWheel = 0;
        _relativeX = 0;
        _relativeY = 0;
        _isRelativeMode = false;
    }
}
