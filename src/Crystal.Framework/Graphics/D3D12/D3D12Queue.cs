using TerraFX.Interop.DirectX;
using TerraFX.Interop.Windows;
using static Crystal.Graphics.D3D12.D3D12Interop;
using static TerraFX.Interop.DirectX.D3D12_COMMAND_QUEUE_FLAGS;
using static TerraFX.Interop.DirectX.D3D12_FENCE_FLAGS;
using static TerraFX.Interop.Windows.Windows;

namespace Crystal.Graphics.D3D12;

internal sealed unsafe class D3D12Queue : IDisposable
{
    private readonly Lock _lock = new();
    private ID3D12CommandQueue* _queue;
    private ID3D12Fence* _fence;
    private ulong _nextSignalValue = 1;

    public D3D12Queue(ID3D12Device* device, D3D12_COMMAND_LIST_TYPE type, string name)
    {
        Type = type;
        var description = new D3D12_COMMAND_QUEUE_DESC { Type = type, Flags = D3D12_COMMAND_QUEUE_FLAG_NONE };
        ID3D12CommandQueue* queue;
        ThrowOnFailure(device->CreateCommandQueue(&description, __uuidof<ID3D12CommandQueue>(), (void**)&queue), "ID3D12Device::CreateCommandQueue");
        _queue = queue;
        SetName((ID3D12Object*)_queue, name);

        ID3D12Fence* fence;
        ThrowOnFailure(device->CreateFence(0, D3D12_FENCE_FLAG_NONE, __uuidof<ID3D12Fence>(), (void**)&fence), "ID3D12Device::CreateFence");
        _fence = fence;
        SetName((ID3D12Object*)_fence, name + "_Fence");
    }

    public D3D12_COMMAND_LIST_TYPE Type { get; }
    public ID3D12CommandQueue* Queue => _queue;
    public ID3D12Fence* Fence => _fence;
    public ulong NextSignalValue => Volatile.Read(ref _nextSignalValue);
    public ulong LastSubmittedValue => NextSignalValue - 1;
    public ulong CompletedValue => _fence->GetCompletedValue();

    public void Wait(D3D12Queue other, ulong value)
    {
        if (value == 0 || other.IsComplete(value)) return;
        lock (_lock) ThrowOnFailure(_queue->Wait(other.Fence, value), "ID3D12CommandQueue::Wait");
    }

    public ulong Submit(ID3D12CommandList* commandList) => Submit(commandList, []);

    public ulong Submit(ID3D12CommandList* commandList, ReadOnlySpan<nint> followingCommandLists)
    {
        lock (_lock)
        {
            if (commandList != null)
            {
                var count = 1 + followingCommandLists.Length;
                var commandLists = stackalloc ID3D12CommandList*[count];
                commandLists[0] = commandList;
                for (var index = 0; index < followingCommandLists.Length; index++) commandLists[index + 1] = (ID3D12CommandList*)followingCommandLists[index];
                _queue->ExecuteCommandLists((uint)count, commandLists);
            }
            var value = _nextSignalValue;
            ThrowOnFailure(_queue->Signal(_fence, value), "ID3D12CommandQueue::Signal");
            Volatile.Write(ref _nextSignalValue, value + 1);
            return value;
        }
    }

    public ulong Signal() => Submit(null);

    public bool IsComplete(ulong value) => value <= _fence->GetCompletedValue();

    public void WaitForValue(ulong value)
    {
        if (IsComplete(value)) return;
        ThrowOnFailure(_fence->SetEventOnCompletion(value, HANDLE.NULL), "ID3D12Fence::SetEventOnCompletion");
    }

    public void WaitForIdle() => WaitForValue(Signal());

    public void Dispose()
    {
        if (_queue == null) return;
        WaitForIdle();
        Release(ref _fence);
        Release(ref _queue);
    }
}
