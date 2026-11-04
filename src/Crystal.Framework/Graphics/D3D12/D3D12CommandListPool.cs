using TerraFX.Interop.DirectX;
using static Crystal.Graphics.D3D12.D3D12Interop;
using static TerraFX.Interop.Windows.Windows;

namespace Crystal.Graphics.D3D12;

internal sealed unsafe class D3D12CommandList : IDisposable
{
    private ID3D12CommandAllocator* _allocator;
    private ID3D12GraphicsCommandList7* _list;

    public D3D12CommandList(ID3D12Device* device, D3D12_COMMAND_LIST_TYPE type, string name)
    {
        ID3D12CommandAllocator* allocator;
        ThrowOnFailure(device->CreateCommandAllocator(type, __uuidof<ID3D12CommandAllocator>(), (void**)&allocator), "ID3D12Device::CreateCommandAllocator");
        _allocator = allocator;

        ID3D12GraphicsCommandList7* list;
        ThrowOnFailure(
            device->CreateCommandList(0, type, _allocator, null, __uuidof<ID3D12GraphicsCommandList7>(), (void**)&list),
            "ID3D12Device::CreateCommandList");
        _list = list;
        SetName((ID3D12Object*)_list, name);
        ThrowOnFailure(_list->Close(), "ID3D12GraphicsCommandList::Close");
    }

    public ID3D12GraphicsCommandList7* List => _list;
    public ulong FenceValue { get; set; }

    public void Reset()
    {
        ThrowOnFailure(_allocator->Reset(), "ID3D12CommandAllocator::Reset");
        ThrowOnFailure(_list->Reset(_allocator, null), "ID3D12GraphicsCommandList::Reset");
    }

    public void Reopen() => ThrowOnFailure(_list->Reset(_allocator, null), "ID3D12GraphicsCommandList::Reset");

    public void Close() => ThrowOnFailure(_list->Close(), "ID3D12GraphicsCommandList::Close");

    public void Dispose()
    {
        Release(ref _list);
        Release(ref _allocator);
    }
}

internal sealed unsafe class D3D12CommandListPool(ID3D12Device* device, D3D12Queue queue, string name) : IDisposable
{
    private readonly Lock _lock = new();
    private readonly Queue<D3D12CommandList> _retired = new();
    private readonly List<D3D12CommandList> _all = [];

    public D3D12CommandList Rent()
    {
        D3D12CommandList? commandList = null;
        lock (_lock)
        {
            if (_retired.TryPeek(out var oldest) && queue.IsComplete(oldest.FenceValue)) commandList = _retired.Dequeue();
            else
            {
                commandList = new D3D12CommandList(device, queue.Type, $"{name}{_all.Count}");
                _all.Add(commandList);
            }
        }
        commandList.Reset();
        return commandList;
    }

    public void Return(D3D12CommandList commandList, ulong fenceValue)
    {
        commandList.FenceValue = fenceValue;
        lock (_lock) _retired.Enqueue(commandList);
    }

    public int Count
    {
        get
        {
            lock (_lock) return _all.Count;
        }
    }

    public void TrimCompleted(int retainedCount)
    {
        lock (_lock)
        {
            while (_all.Count > retainedCount && _retired.TryPeek(out var oldest) && queue.IsComplete(oldest.FenceValue))
            {
                _retired.Dequeue();
                _all.Remove(oldest);
                oldest.Dispose();
            }
        }
    }

    public void Dispose()
    {
        foreach (var commandList in _all) commandList.Dispose();
        _all.Clear();
        _retired.Clear();
    }
}
