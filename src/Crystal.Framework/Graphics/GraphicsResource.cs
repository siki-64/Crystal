namespace Crystal.Graphics;

public abstract class GraphicsResource : IDisposable
{
    private int _isDisposed;

    private protected GraphicsResource(GraphicsDevice graphicsDevice, string? name)
    {
        ArgumentNullException.ThrowIfNull(graphicsDevice);
        GraphicsDevice = graphicsDevice;
        Name = name;
    }

    public GraphicsDevice GraphicsDevice { get; }
    public string? Name { get; }
    public bool IsDisposed => Volatile.Read(ref _isDisposed) != 0;

    public void Dispose()
    {
        if (Interlocked.Exchange(ref _isDisposed, 1) != 0) return;
        GraphicsDevice.Untrack(this);
        ReleaseNativeResources();
    }

    private protected abstract void ReleaseNativeResources();

    private protected void ThrowIfDisposed() => ObjectDisposedException.ThrowIf(IsDisposed, this);
}
