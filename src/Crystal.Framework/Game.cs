using System.Diagnostics;
using Crystal.Graphics;
using Crystal.Input;
using Crystal.Platform;

namespace Crystal;

public abstract unsafe class Game : IDisposable
{
    private static readonly TimeSpan MinimizedSleep = TimeSpan.FromMilliseconds(10);

    private readonly GameClock _clock;
    private readonly PreciseSleeper _sleeper = new();
    private long _lastTimestamp;
    private bool _exitRequested;
    private bool _isDisposed;
    private bool _isMouseVisible = true;

    protected Game(GameOptions? options = null)
    {
        Options = options ?? new GameOptions();
        _clock = new GameClock(Options.TargetElapsedTime, Options.IsFixedTimeStep);

        if (Options.HostWindowHandle != 0)
        {
            Window = new GameWindow(Options);
            GraphicsDevice = new GraphicsDevice(Window, Options.Graphics);
            return;
        }

        SdlRuntime.Acquire();
        try
        {
            SdlInput.Reset();
            Window = new GameWindow(Options);
            GraphicsDevice = new GraphicsDevice(Window, Options.Graphics);
            IsMouseVisible = false;
            Mouse.Window = Window;
        }
        catch
        {
            Window?.Dispose();
            SdlRuntime.Release();
            throw;
        }
    }

    public GameOptions Options { get; }
    public GameWindow Window { get; }
    public GraphicsDevice GraphicsDevice { get; }
    public bool IsActive { get; private set; } = true;

    public bool IsMouseVisible
    {
        get => _isMouseVisible;
        set
        {
            if (Window.IsHosted)
            {
                _isMouseVisible = value;
                return;
            }
            if (!(value ? Sdl.ShowCursor() : Sdl.HideCursor())) throw Sdl.Failure(value ? "SDL_ShowCursor" : "SDL_HideCursor");
            _isMouseVisible = value;
        }
    }

    public TimeSpan TargetElapsedTime
    {
        get => _clock.TargetElapsedTime;
        set
        {
            ArgumentOutOfRangeException.ThrowIfLessThanOrEqual(value, TimeSpan.Zero);
            _clock.TargetElapsedTime = value;
        }
    }

    public bool IsFixedTimeStep
    {
        get => _clock.IsFixedTimeStep;
        set => _clock.IsFixedTimeStep = value;
    }

    public void Run()
    {
        ObjectDisposedException.ThrowIf(_isDisposed, this);
        Initialize();
        LoadContent();
        if (!Options.StartHidden) Window.Show();

        _lastTimestamp = Stopwatch.GetTimestamp();
        try
        {
            while (!_exitRequested)
            {
                PumpEvents();
                if (_exitRequested) break;
                Tick();
            }
        }
        finally
        {
            UnloadContent();
        }
    }

    public void Exit() => _exitRequested = true;

    protected virtual void Initialize()
    {
    }

    protected virtual void LoadContent()
    {
    }

    protected virtual void UnloadContent()
    {
    }

    protected virtual void Update(GameTime gameTime)
    {
    }

    protected virtual bool BeginDraw()
    {
        GraphicsDevice.BeginFrame();
        return true;
    }

    protected virtual void Draw(GameTime gameTime)
    {
    }

    protected virtual void EndDraw() => GraphicsDevice.EndFrame();

    private void Tick()
    {
        AccumulateElapsedTime();
        while (_clock.TimeUntilNextUpdate is var remaining && remaining > TimeSpan.Zero)
        {
            _sleeper.Sleep(remaining);
            AccumulateElapsedTime();
        }

        var steps = _clock.TakeUpdateSteps();
        for (var step = 0; step < steps && !_exitRequested; step++)
        {
            _clock.BeginUpdateStep();
            Update(_clock.UpdateTime);
        }

        if (_exitRequested) return;
        if (Window.IsMinimized)
        {
            _sleeper.Sleep(MinimizedSleep);
            return;
        }

        if (!BeginDraw())
        {
            if (GraphicsDevice.IsInFrame) throw new InvalidOperationException("BeginDraw returned false after base.BeginDraw began a frame.");
            return;
        }
        if (!GraphicsDevice.IsInFrame) throw new InvalidOperationException("An override of BeginDraw must call base.BeginDraw.");
        Draw(_clock.DrawTime);
        EndDraw();
        if (GraphicsDevice.IsInFrame) throw new InvalidOperationException("An override of EndDraw must call base.EndDraw.");
    }

    private void AccumulateElapsedTime()
    {
        var now = Stopwatch.GetTimestamp();
        _clock.Accumulate(Stopwatch.GetElapsedTime(_lastTimestamp, now));
        _lastTimestamp = now;
    }

    private void PumpEvents()
    {
        if (Window.IsHosted)
        {
            FollowHostWindow();
            HostWindowInput.Poll(Window);
            return;
        }

        SdlEvent sdlEvent;
        while (Sdl.PollEvent(&sdlEvent))
        {
            if (SdlInput.Process(sdlEvent, Window)) continue;
            switch (sdlEvent.Type)
            {
                case SdlEventType.Quit:
                case SdlEventType.WindowCloseRequested:
                    Exit();
                    break;
                case SdlEventType.WindowPixelSizeChanged:
                    GraphicsDevice.RequestBackBufferResize(sdlEvent.Window.Data1, sdlEvent.Window.Data2);
                    Window.OnClientSizeChanged();
                    break;
                case SdlEventType.WindowFocusGained:
                    IsActive = true;
                    break;
                case SdlEventType.WindowFocusLost:
                    IsActive = false;
                    break;
            }
        }
    }

    private void FollowHostWindow()
    {
        IsActive = Window.IsHostForeground;
        var size = Window.ClientSize;
        if (size.X <= 0 || size.Y <= 0 || (size.X == GraphicsDevice.BackBufferWidth && size.Y == GraphicsDevice.BackBufferHeight)) return;
        GraphicsDevice.RequestBackBufferResize(size.X, size.Y);
        Window.OnClientSizeChanged();
    }

    public void Dispose()
    {
        Dispose(disposing: true);
        GC.SuppressFinalize(this);
    }

    protected virtual void Dispose(bool disposing)
    {
        if (_isDisposed || !disposing) return;
        _isDisposed = true;
        GraphicsDevice.Dispose();
        Window.Dispose();
        _sleeper.Dispose();
        if (Window.IsHosted) return;
        if (Mouse.Window == Window) Mouse.Window = null;
        SdlInput.Reset();
        SdlRuntime.Release();
    }
}
