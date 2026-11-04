using TerraFX.Interop.Windows;

namespace Crystal.Graphics.D3D12;

internal readonly record struct D3D12ReleaseValues(ulong Direct, ulong Compute, ulong Copy)
{
    public const ulong AwaitingSubmission = ulong.MaxValue;

    public static D3D12ReleaseValues AwaitingGpuQueues => new(AwaitingSubmission, AwaitingSubmission, 0);
}

internal sealed unsafe class D3D12DeferredReleases
{
    private readonly Lock _lock = new();
    private readonly List<PendingRelease> _pending = [];

    public int PendingCount
    {
        get
        {
            lock (_lock) return _pending.Count;
        }
    }

    public void Release(IUnknown* unknown, D3D12ReleaseValues values)
    {
        lock (_lock) _pending.Add(new PendingRelease(values, (nint)unknown, null, 0, null, default, null));
    }

    public void Free(D3D12ShaderVisibleHeap heap, uint index, D3D12ReleaseValues values)
    {
        lock (_lock) _pending.Add(new PendingRelease(values, 0, heap, index, null, default, null));
    }

    public void Free(D3D12CpuDescriptorHeap heap, D3D12CpuDescriptor descriptor, D3D12ReleaseValues values)
    {
        lock (_lock) _pending.Add(new PendingRelease(values, 0, null, 0, heap, descriptor, null));
    }

    public void Free(D3D12OcclusionQueryPool pool, uint index, D3D12ReleaseValues values)
    {
        lock (_lock) _pending.Add(new PendingRelease(values, 0, null, index, null, default, pool));
    }

    public void AssignDirectSubmission(ulong directValue)
    {
        lock (_lock)
        {
            for (var index = 0; index < _pending.Count; index++)
            {
                var pending = _pending[index];
                if (pending.Values.Direct == D3D12ReleaseValues.AwaitingSubmission) _pending[index] = pending with { Values = pending.Values with { Direct = directValue } };
            }
        }
    }

    public void AssignComputeSubmission(ulong computeValue)
    {
        lock (_lock)
        {
            for (var index = 0; index < _pending.Count; index++)
            {
                var pending = _pending[index];
                if (pending.Values.Compute == D3D12ReleaseValues.AwaitingSubmission) _pending[index] = pending with { Values = pending.Values with { Compute = computeValue } };
            }
        }
    }

    public void Process(ulong directCompleted, ulong computeCompleted, ulong copyCompleted)
    {
        lock (_lock)
        {
            var kept = 0;
            for (var index = 0; index < _pending.Count; index++)
            {
                var pending = _pending[index];
                var values = pending.Values;
                if (values.Direct <= directCompleted && values.Compute <= computeCompleted && values.Copy <= copyCompleted) pending.Execute();
                else _pending[kept++] = pending;
            }
            _pending.RemoveRange(kept, _pending.Count - kept);
        }
    }

    public void ProcessAll() => Process(ulong.MaxValue, ulong.MaxValue, ulong.MaxValue);

    private readonly record struct PendingRelease(
        D3D12ReleaseValues Values,
        nint Unknown,
        D3D12ShaderVisibleHeap? ShaderVisibleHeap,
        uint Index,
        D3D12CpuDescriptorHeap? CpuHeap,
        D3D12CpuDescriptor CpuDescriptor,
        D3D12OcclusionQueryPool? OcclusionQueryPool)
    {
        public void Execute()
        {
            if (Unknown != 0) ((IUnknown*)Unknown)->Release();
            ShaderVisibleHeap?.Free(Index);
            OcclusionQueryPool?.Free(Index);
            CpuHeap?.Free(CpuDescriptor);
        }
    }
}
