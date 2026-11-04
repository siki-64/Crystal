using TerraFX.Interop.DirectX;
using static Crystal.Graphics.D3D12.D3D12Interop;
using static TerraFX.Interop.DirectX.D3D12_HEAP_TYPE;
using static TerraFX.Interop.DirectX.D3D12_QUERY_HEAP_TYPE;
using static TerraFX.Interop.Windows.Windows;

namespace Crystal.Graphics.D3D12;

internal readonly unsafe struct D3D12OcclusionQuerySlot(uint index, ID3D12QueryHeap* heap, uint heapIndex, ID3D12Resource* readback, ulong* result)
{
    public uint Index { get; } = index;
    public ID3D12QueryHeap* Heap { get; } = heap;
    public uint HeapIndex { get; } = heapIndex;
    public ID3D12Resource* Readback { get; } = readback;
    public ulong* Result { get; } = result;
}

internal sealed unsafe class D3D12OcclusionQueryPool(ID3D12Device10* device) : IDisposable
{
    private const uint PageSize = 256;

    private readonly Lock _lock = new();
    private readonly List<Page> _pages = [];
    private readonly Stack<uint> _freeIndices = new();

    public D3D12OcclusionQuerySlot Allocate()
    {
        lock (_lock)
        {
            if (_freeIndices.Count == 0) AddPage();
            var index = _freeIndices.Pop();
            var page = _pages[(int)(index / PageSize)];
            var heapIndex = index % PageSize;
            return new D3D12OcclusionQuerySlot(index, page.Heap, heapIndex, page.Readback, page.Results + heapIndex);
        }
    }

    public void Free(uint index)
    {
        lock (_lock) _freeIndices.Push(index);
    }

    public void Dispose()
    {
        lock (_lock)
        {
            foreach (var page in _pages)
            {
                page.Heap->Release();
                page.Readback->Release();
            }
            _pages.Clear();
            _freeIndices.Clear();
        }
    }

    private void AddPage()
    {
        var name = $"OcclusionQueryHeap{_pages.Count}";
        var description = new D3D12_QUERY_HEAP_DESC { Type = D3D12_QUERY_HEAP_TYPE_OCCLUSION, Count = PageSize };
        ID3D12QueryHeap* heap;
        ThrowOnFailure(device->CreateQueryHeap(&description, __uuidof<ID3D12QueryHeap>(), (void**)&heap), $"ID3D12Device::CreateQueryHeap({name})");
        SetName((ID3D12Object*)heap, name);

        var heapProperties = new D3D12_HEAP_PROPERTIES { Type = D3D12_HEAP_TYPE_READBACK };
        var bufferDescription = D3D12Device.BufferDescription(PageSize * sizeof(ulong), D3D12_RESOURCE_FLAGS.D3D12_RESOURCE_FLAG_NONE);
        ID3D12Resource* readback;
        var result = device->CreateCommittedResource3(
            &heapProperties, D3D12_HEAP_FLAGS.D3D12_HEAP_FLAG_NONE, &bufferDescription, D3D12_BARRIER_LAYOUT.D3D12_BARRIER_LAYOUT_UNDEFINED, null, null, 0, null,
            __uuidof<ID3D12Resource>(), (void**)&readback);
        if (FAILED(result))
        {
            heap->Release();
            ThrowOnFailure(result, $"ID3D12Device10::CreateCommittedResource3({name}Readback)");
        }
        SetName((ID3D12Object*)readback, $"{name}Readback");

        ulong* results;
        var mapResult = readback->Map(0, null, (void**)&results);
        if (FAILED(mapResult))
        {
            heap->Release();
            readback->Release();
            ThrowOnFailure(mapResult, "ID3D12Resource::Map");
        }

        var firstIndex = (uint)_pages.Count * PageSize;
        _pages.Add(new Page(heap, readback, results));
        for (var index = PageSize; index > 0; index--) _freeIndices.Push(firstIndex + index - 1);
    }

    private readonly struct Page(ID3D12QueryHeap* heap, ID3D12Resource* readback, ulong* results)
    {
        public ID3D12QueryHeap* Heap { get; } = heap;
        public ID3D12Resource* Readback { get; } = readback;
        public ulong* Results { get; } = results;
    }
}
