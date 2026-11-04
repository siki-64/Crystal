namespace Crystal.Graphics;

public sealed class GraphicsCommandList : IDisposable
{
    private int _boundThreadId;

    public GraphicsCommandList(GraphicsDevice graphicsDevice, string name)
    {
        ArgumentNullException.ThrowIfNull(graphicsDevice);
        ArgumentNullException.ThrowIfNull(name);
        GraphicsDevice = graphicsDevice;
        Name = name;
        Recording = graphicsDevice.CreateRecording(this);
    }

    public GraphicsDevice GraphicsDevice { get; }
    public string Name { get; }
    public bool IsDisposed { get; private set; }
    public bool IsRecording => Recording.Context.IsOpen;
    public GraphicsMetrics Metrics => Recording.Metrics;

    internal GraphicsRecordingState Recording { get; }
    internal ulong LastSubmittedValue { get; set; }
    internal bool IsBound => Volatile.Read(ref _boundThreadId) != 0;

    public GraphicsCommandListBinding Bind()
    {
        ObjectDisposedException.ThrowIf(IsDisposed, this);
        GraphicsDevice.Bind(this);
        return new GraphicsCommandListBinding(this);
    }

    public void Dispose()
    {
        if (IsDisposed) return;
        if (IsBound) throw new InvalidOperationException($"Command list '{Name}' is still bound. Dispose its binding first.");
        IsDisposed = true;
        GraphicsDevice.DestroyRecording(this);
    }

    internal bool TryClaim(int threadId) => Interlocked.CompareExchange(ref _boundThreadId, threadId, 0) == 0;

    internal void ReleaseClaim() => Volatile.Write(ref _boundThreadId, 0);
}

public readonly ref struct GraphicsCommandListBinding
{
    private readonly GraphicsCommandList? _commandList;

    internal GraphicsCommandListBinding(GraphicsCommandList commandList) => _commandList = commandList;

    public void Dispose() => _commandList?.GraphicsDevice.Unbind(_commandList);
}

public readonly ref struct GraphicsCommandListSuspension
{
    private readonly GraphicsDevice? _graphicsDevice;
    private readonly GraphicsRecordingState? _suspended;

    internal GraphicsCommandListSuspension(GraphicsDevice graphicsDevice, GraphicsRecordingState? suspended)
    {
        _graphicsDevice = graphicsDevice;
        _suspended = suspended;
    }

    public void Dispose()
    {
        if (_suspended != null) _graphicsDevice!.ResumeCommandList(_suspended);
    }
}
