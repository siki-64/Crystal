namespace Crystal;

public sealed class GameTime
{
    public TimeSpan TotalGameTime { get; internal set; }
    public TimeSpan ElapsedGameTime { get; internal set; }
    public bool IsRunningSlowly { get; internal set; }
}
