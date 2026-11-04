using System.Runtime.InteropServices;
using TerraFX.Interop.DirectX;
using static TerraFX.Interop.DirectX.D3D12_BARRIER_ACCESS;
using static TerraFX.Interop.DirectX.D3D12_BARRIER_LAYOUT;
using static TerraFX.Interop.DirectX.D3D12_BARRIER_SYNC;
using static TerraFX.Interop.DirectX.D3D12_BARRIER_TYPE;

namespace Crystal.Graphics.D3D12;

internal sealed unsafe class D3D12CommandContext : IDisposable
{
    private const D3D12_BARRIER_ACCESS WriteAccesses =
        D3D12_BARRIER_ACCESS_RENDER_TARGET | D3D12_BARRIER_ACCESS_DEPTH_STENCIL_WRITE | D3D12_BARRIER_ACCESS_COPY_DEST
        | D3D12_BARRIER_ACCESS_RESOLVE_DEST | D3D12_BARRIER_ACCESS_UNORDERED_ACCESS;

    private const int MaximumRootParameters = 64;

    private static ulong s_lastCommandListNumber;

    private readonly D3D12Device _device;
    private readonly D3D12CommandListPool _pool;
    private readonly Dictionary<D3D12Resource, D3D12DeferredUse>? _deferredUses;
    private D3D12CommandList? _commandList;

    public D3D12CommandContext(D3D12Device device, D3D12QueueKind kind, bool isDeferred = false)
    {
        _device = device;
        Kind = kind;
        Queue = kind == D3D12QueueKind.Direct ? device.DirectQueue : device.ComputeQueue;
        _pool = new D3D12CommandListPool(device.Device, Queue, isDeferred ? "DeferredCommandList" : kind == D3D12QueueKind.Direct ? "DirectCommandList" : "ComputeCommandList");
        if (isDeferred) _deferredUses = new Dictionary<D3D12Resource, D3D12DeferredUse>(ReferenceEqualityComparer.Instance);
    }

    public D3D12QueueKind Kind { get; }
    public D3D12Queue Queue { get; }
    public D3D12CommandContext? Peer { get; set; }
    public Action? Opened { get; set; }
    public bool IsOpen => _commandList != null;
    public bool IsDeferred => _deferredUses != null;
    public Dictionary<D3D12Resource, D3D12DeferredUse> DeferredUses => _deferredUses ?? throw new InvalidOperationException("Only deferred contexts track uses locally.");
    public ID3D12GraphicsCommandList7* List { get; private set; }
    public ulong CommandListNumber { get; private set; }
    public ulong CopyWaitValue { get; private set; }
    public ulong PeerWaitValue { get; private set; }
    public bool RequiresPeerSubmission { get; private set; }
    public ID3D12PipelineState* AppliedPipelineState { get; set; }
    public ID3D12RootSignature* AppliedGraphicsRootSignature { get; set; }
    public ID3D12RootSignature* AppliedComputeRootSignature { get; set; }
    public ulong[] AppliedGraphicsRootArguments { get; } = new ulong[MaximumRootParameters];
    public ulong[] AppliedComputeRootArguments { get; } = new ulong[MaximumRootParameters];

    public ID3D12GraphicsCommandList7* Open()
    {
        if (_commandList != null) return List;
        _commandList = _pool.Rent();
        List = _commandList.List;
        CommandListNumber = Interlocked.Increment(ref s_lastCommandListNumber);
        CopyWaitValue = 0;
        PeerWaitValue = 0;
        RequiresPeerSubmission = false;
        AppliedPipelineState = null;
        AppliedGraphicsRootSignature = null;
        AppliedComputeRootSignature = null;
        Array.Clear(AppliedGraphicsRootArguments);
        Array.Clear(AppliedComputeRootArguments);
        _deferredUses?.Clear();
        var heaps = stackalloc ID3D12DescriptorHeap*[] { _device.ShaderResourceHeap.Heap, _device.SamplerHeap.Heap };
        List->SetDescriptorHeaps(2, heaps);
        Opened?.Invoke();
        return List;
    }

    public ulong Submit() => Submit([]);

    public ulong Submit(ReadOnlySpan<nint> followingCommandLists)
    {
        var commandList = _commandList ?? throw new InvalidOperationException("No command list is open.");
        _commandList = null;
        List = null;
        commandList.Close();
        Queue.Wait(_device.CopyQueue, CopyWaitValue);
        if (RequiresPeerSubmission) PeerWaitValue = Math.Max(PeerWaitValue, Peer!.Queue.LastSubmittedValue);
        Queue.Wait(Peer!.Queue, PeerWaitValue);
        var value = Queue.Submit((ID3D12CommandList*)commandList.List, followingCommandLists);
        _pool.Return(commandList, value);
        return value;
    }

    public D3D12CommandList CloseDeferred()
    {
        var commandList = _commandList ?? throw new InvalidOperationException("No command list is open.");
        _commandList = null;
        List = null;
        commandList.Close();
        return commandList;
    }

    public void ReturnDeferred(D3D12CommandList commandList, ulong fenceValue) => _pool.Return(commandList, fenceValue);

    public void Use(D3D12Resource resource, D3D12_BARRIER_SYNC sync, D3D12_BARRIER_ACCESS access, D3D12_BARRIER_LAYOUT layout)
    {
        if (_deferredUses != null)
        {
            UseDeferred(resource, sync, access, layout);
            return;
        }
        if (resource.Owner != Kind) TakeOwnership(resource, layout);

        if (resource.AccessCommandList != CommandListNumber)
        {
            resource.AccessCommandList = CommandListNumber;
            resource.Access = D3D12_BARRIER_ACCESS_NO_ACCESS;
            resource.Sync = D3D12_BARRIER_SYNC_NONE;
        }

        var hasPriorAccess = resource.Access != D3D12_BARRIER_ACCESS_NO_ACCESS;
        var layoutChanges = resource.IsTexture && resource.Layout != layout;
        var needsBarrier = layoutChanges || (hasPriorAccess && NeedsHazardBarrier(resource.Access, access));

        if (!needsBarrier)
        {
            resource.Access = hasPriorAccess ? resource.Access | access : access;
            resource.Sync = hasPriorAccess ? resource.Sync | sync : sync;
            return;
        }

        EmitBarrier(resource, hasPriorAccess ? resource.Sync : D3D12_BARRIER_SYNC_NONE, resource.Access, resource.Layout, sync, access, layout);
        resource.Layout = layout;
        resource.Access = access;
        resource.Sync = sync;
    }

    public void Dispose() => _pool.Dispose();

    private void UseDeferred(D3D12Resource resource, D3D12_BARRIER_SYNC sync, D3D12_BARRIER_ACCESS access, D3D12_BARRIER_LAYOUT layout)
    {
        ref var use = ref CollectionsMarshal.GetValueRefOrAddDefault(_deferredUses!, resource, out var exists);
        if (!exists)
        {
            use = new D3D12DeferredUse(sync, access, layout, sync, access, layout, HasBarrier: false);
            return;
        }

        var layoutChanges = resource.IsTexture && use.Layout != layout;
        if (!layoutChanges && !NeedsHazardBarrier(use.Access, access))
        {
            use = use.HasBarrier
                ? use with { Sync = use.Sync | sync, Access = use.Access | access }
                : use with { Sync = use.Sync | sync, Access = use.Access | access, FirstSync = use.FirstSync | sync, FirstAccess = use.FirstAccess | access };
            return;
        }

        EmitBarrier(resource, use.Sync, use.Access, use.Layout, sync, access, layout);
        use = use with { Sync = sync, Access = access, Layout = layout, HasBarrier = true };
    }

    private static bool NeedsHazardBarrier(D3D12_BARRIER_ACCESS priorAccess, D3D12_BARRIER_ACCESS access)
    {
        var isWrite = (access & WriteAccesses) != 0;
        var wasWrite = (priorAccess & WriteAccesses) != 0;
        var isOrderedAttachmentWrite = access == priorAccess && access is D3D12_BARRIER_ACCESS_RENDER_TARGET or D3D12_BARRIER_ACCESS_DEPTH_STENCIL_WRITE;
        return (isWrite || wasWrite) && !isOrderedAttachmentWrite;
    }

    private void EmitBarrier(
        D3D12Resource resource, D3D12_BARRIER_SYNC syncBefore, D3D12_BARRIER_ACCESS accessBefore, D3D12_BARRIER_LAYOUT layoutBefore,
        D3D12_BARRIER_SYNC sync, D3D12_BARRIER_ACCESS access, D3D12_BARRIER_LAYOUT layout)
    {
        if (resource.IsTexture)
        {
            var barrier = new D3D12_TEXTURE_BARRIER
            {
                SyncBefore = syncBefore,
                SyncAfter = sync,
                AccessBefore = accessBefore,
                AccessAfter = access,
                LayoutBefore = layoutBefore,
                LayoutAfter = layout,
                pResource = resource.Pointer,
                Subresources = new D3D12_BARRIER_SUBRESOURCE_RANGE { IndexOrFirstMipLevel = uint.MaxValue },
            };
            var group = new D3D12_BARRIER_GROUP { Type = D3D12_BARRIER_TYPE_TEXTURE, NumBarriers = 1 };
            group.pTextureBarriers = &barrier;
            List->Barrier(1, &group);
            return;
        }

        var bufferBarrier = new D3D12_BUFFER_BARRIER
        {
            SyncBefore = syncBefore,
            SyncAfter = sync,
            AccessBefore = accessBefore,
            AccessAfter = access,
            pResource = resource.Pointer,
            Offset = 0,
            Size = ulong.MaxValue,
        };
        var bufferGroup = new D3D12_BARRIER_GROUP { Type = D3D12_BARRIER_TYPE_BUFFER, NumBarriers = 1 };
        bufferGroup.pBufferBarriers = &bufferBarrier;
        List->Barrier(1, &bufferGroup);
    }

    private void TakeOwnership(D3D12Resource resource, D3D12_BARRIER_LAYOUT layout)
    {
        var previousOwner = resource.Owner;
        if (previousOwner != D3D12QueueKind.None)
        {
            var peer = Peer!;
            var isInOpenPeerList = peer.IsOpen && resource.AccessCommandList == peer.CommandListNumber;
            if (Kind == D3D12QueueKind.Direct && isInOpenPeerList)
                throw new InvalidOperationException(
                    $"'{resource.Name}' is used by async compute work that hasn't been submitted. Call GraphicsDevice.AsyncCompute.Submit before using it on the graphics queue.");
            if (Kind == D3D12QueueKind.Compute && resource.IsTexture && !IsComputeQueueLayout(resource.Layout))
            {
                peer.Open();
                peer.Use(resource, D3D12_BARRIER_SYNC_NONE, D3D12_BARRIER_ACCESS_NO_ACCESS, layout);
                isInOpenPeerList = true;
            }
            if (isInOpenPeerList) RequiresPeerSubmission = true;
            else PeerWaitValue = Math.Max(PeerWaitValue, peer.Queue.LastSubmittedValue);
        }

        lock (resource.Lock)
        {
            if (previousOwner == D3D12QueueKind.None && resource.PendingCopyValue > CopyWaitValue) CopyWaitValue = resource.PendingCopyValue;
            resource.UsedQueues |= Kind;
            resource.Owner = Kind;
        }
    }

    private static bool IsComputeQueueLayout(D3D12_BARRIER_LAYOUT layout) => layout is D3D12_BARRIER_LAYOUT_COMMON or D3D12_BARRIER_LAYOUT_GENERIC_READ
        or D3D12_BARRIER_LAYOUT_UNORDERED_ACCESS or D3D12_BARRIER_LAYOUT_SHADER_RESOURCE or D3D12_BARRIER_LAYOUT_COPY_SOURCE or D3D12_BARRIER_LAYOUT_COPY_DEST
        or D3D12_BARRIER_LAYOUT_COMPUTE_QUEUE_COMMON or D3D12_BARRIER_LAYOUT_COMPUTE_QUEUE_GENERIC_READ or D3D12_BARRIER_LAYOUT_COMPUTE_QUEUE_UNORDERED_ACCESS
        or D3D12_BARRIER_LAYOUT_COMPUTE_QUEUE_SHADER_RESOURCE or D3D12_BARRIER_LAYOUT_COMPUTE_QUEUE_COPY_SOURCE or D3D12_BARRIER_LAYOUT_COMPUTE_QUEUE_COPY_DEST;
}

internal readonly record struct D3D12DeferredUse(
    D3D12_BARRIER_SYNC FirstSync, D3D12_BARRIER_ACCESS FirstAccess, D3D12_BARRIER_LAYOUT FirstLayout,
    D3D12_BARRIER_SYNC Sync, D3D12_BARRIER_ACCESS Access, D3D12_BARRIER_LAYOUT Layout, bool HasBarrier);
