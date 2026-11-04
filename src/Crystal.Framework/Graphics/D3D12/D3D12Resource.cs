using TerraFX.Interop.DirectX;
using static TerraFX.Interop.DirectX.D3D12_BARRIER_ACCESS;
using static TerraFX.Interop.DirectX.D3D12_BARRIER_SYNC;

namespace Crystal.Graphics.D3D12;

[Flags]
internal enum D3D12QueueKind : byte
{
    None = 0,
    Direct = 1 << 0,
    Compute = 1 << 1,
}

internal sealed unsafe class D3D12Resource(ID3D12Resource* pointer, bool isTexture, D3D12_BARRIER_LAYOUT initialLayout, string name)
{
    private volatile D3D12QueueKind _owner;

    public ID3D12Resource* Pointer { get; } = pointer;
    public bool IsTexture { get; } = isTexture;
    public string Name { get; } = name;
    public Lock Lock { get; } = new();

    public D3D12_BARRIER_LAYOUT Layout { get; set; } = initialLayout;
    public D3D12_BARRIER_ACCESS Access { get; set; } = D3D12_BARRIER_ACCESS_NO_ACCESS;
    public D3D12_BARRIER_SYNC Sync { get; set; } = D3D12_BARRIER_SYNC_NONE;
    public ulong PendingCopyValue { get; set; }
    public ulong AccessCommandList { get; set; }
    public D3D12QueueKind UsedQueues { get; set; }

    public D3D12QueueKind Owner
    {
        get => _owner;
        set => _owner = value;
    }
}
