using Crystal.Graphics.D3D12;

namespace Crystal.Graphics;

public sealed unsafe class OcclusionQuery : GraphicsResource
{
    private readonly D3D12OcclusionQuerySlot _slot;

    public OcclusionQuery(GraphicsDevice graphicsDevice, string? name = null)
        : base(graphicsDevice, name)
    {
        _slot = graphicsDevice.Native.OcclusionQueries.Allocate();
        graphicsDevice.Track(this);
    }

    public bool IsActive { get; internal set; }

    public bool IsComplete
    {
        get
        {
            ThrowIfDisposed();
            return FenceValue != 0 && !IsActive && GraphicsDevice.Native.DirectQueue.IsComplete(FenceValue);
        }
    }

    public long PixelCount
    {
        get
        {
            if (!IsComplete) throw new InvalidOperationException($"Occlusion query '{Name}' has no result yet. Check IsComplete first.");
            return (long)Volatile.Read(ref *_slot.Result);
        }
    }

    internal D3D12OcclusionQuerySlot Slot => _slot;
    internal ulong FenceValue { get; set; }

    public void Begin() => GraphicsDevice.BeginOcclusionQuery(this);

    public void End() => GraphicsDevice.EndOcclusionQuery(this);

    private protected override void ReleaseNativeResources() => GraphicsDevice.FreeOcclusionQuery(this);
}
