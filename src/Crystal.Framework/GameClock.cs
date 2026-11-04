namespace Crystal;

internal sealed class GameClock(TimeSpan targetElapsedTime, bool isFixedTimeStep)
{
    public static readonly TimeSpan MaxElapsedTime = TimeSpan.FromMilliseconds(500);

    private TimeSpan _accumulated;

    public TimeSpan TargetElapsedTime { get; set; } = targetElapsedTime;
    public bool IsFixedTimeStep { get; set; } = isFixedTimeStep;
    public GameTime UpdateTime { get; } = new();
    public GameTime DrawTime { get; } = new();

    public TimeSpan TimeUntilNextUpdate =>
        IsFixedTimeStep && _accumulated < TargetElapsedTime ? TargetElapsedTime - _accumulated : TimeSpan.Zero;

    public void Accumulate(TimeSpan realElapsed)
    {
        _accumulated += realElapsed;
        if (_accumulated > MaxElapsedTime) _accumulated = MaxElapsedTime;
    }

    public int TakeUpdateSteps()
    {
        TimeSpan stepElapsed;
        int steps;
        if (IsFixedTimeStep)
        {
            steps = (int)(_accumulated.Ticks / TargetElapsedTime.Ticks);
            stepElapsed = TargetElapsedTime;
            _accumulated -= TargetElapsedTime * steps;
        }
        else
        {
            steps = 1;
            stepElapsed = _accumulated;
            _accumulated = TimeSpan.Zero;
        }

        if (steps == 0) return 0;

        var isRunningSlowly = steps > 1;
        UpdateTime.ElapsedGameTime = stepElapsed;
        UpdateTime.IsRunningSlowly = isRunningSlowly;
        DrawTime.ElapsedGameTime = stepElapsed * steps;
        DrawTime.TotalGameTime += DrawTime.ElapsedGameTime;
        DrawTime.IsRunningSlowly = isRunningSlowly;
        return steps;
    }

    public void BeginUpdateStep() => UpdateTime.TotalGameTime += UpdateTime.ElapsedGameTime;
}
