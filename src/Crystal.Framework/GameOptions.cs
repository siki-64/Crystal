using Crystal.Graphics;

namespace Crystal;

public sealed record GameOptions
{
    public string Title { get; init; } = "Crystal";
    public int Width { get; init; } = 1280;
    public int Height { get; init; } = 720;
    public bool AllowUserResizing { get; init; } = true;
    public bool StartHidden { get; init; }
    public nint HostWindowHandle { get; init; }
    public bool IsFixedTimeStep { get; init; } = true;
    public TimeSpan TargetElapsedTime { get; init; } = TimeSpan.FromTicks(TimeSpan.TicksPerSecond / 60);
    public GraphicsDeviceOptions Graphics { get; init; } = new();
}
