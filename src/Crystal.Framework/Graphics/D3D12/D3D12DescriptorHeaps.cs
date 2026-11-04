using TerraFX.Interop.DirectX;
using static Crystal.Graphics.D3D12.D3D12Interop;
using static TerraFX.Interop.DirectX.D3D12_DESCRIPTOR_HEAP_FLAGS;
using static TerraFX.Interop.Windows.Windows;

namespace Crystal.Graphics.D3D12;

internal sealed unsafe class D3D12ShaderVisibleHeap : IDisposable
{
    private readonly Lock _lock = new();
    private readonly Stack<uint> _freeIndices = new();
    private readonly uint _increment;
    private readonly D3D12_CPU_DESCRIPTOR_HANDLE _cpuStart;
    private readonly D3D12_GPU_DESCRIPTOR_HANDLE _gpuStart;
    private ID3D12DescriptorHeap* _heap;
    private uint _nextIndex;

    public D3D12ShaderVisibleHeap(ID3D12Device* device, D3D12_DESCRIPTOR_HEAP_TYPE type, uint capacity, string name)
    {
        Capacity = capacity;
        var description = new D3D12_DESCRIPTOR_HEAP_DESC { Type = type, NumDescriptors = capacity, Flags = D3D12_DESCRIPTOR_HEAP_FLAG_SHADER_VISIBLE };
        ID3D12DescriptorHeap* heap;
        ThrowOnFailure(device->CreateDescriptorHeap(&description, __uuidof<ID3D12DescriptorHeap>(), (void**)&heap), "ID3D12Device::CreateDescriptorHeap");
        _heap = heap;
        SetName((ID3D12Object*)_heap, name);
        _increment = device->GetDescriptorHandleIncrementSize(type);
        _cpuStart = _heap->GetCPUDescriptorHandleForHeapStart();
        _gpuStart = _heap->GetGPUDescriptorHandleForHeapStart();
    }

    public uint Capacity { get; }
    public ID3D12DescriptorHeap* Heap => _heap;

    public int AllocatedCount
    {
        get
        {
            lock (_lock) return (int)_nextIndex - _freeIndices.Count;
        }
    }

    public uint Allocate()
    {
        lock (_lock)
        {
            if (_freeIndices.TryPop(out var index)) return index;
            if (_nextIndex == Capacity) throw new GraphicsException($"The shader-visible descriptor heap is full ({Capacity} descriptors).");
            return _nextIndex++;
        }
    }

    public void Free(uint index)
    {
        lock (_lock) _freeIndices.Push(index);
    }

    public D3D12_CPU_DESCRIPTOR_HANDLE CpuHandle(uint index) => new() { ptr = _cpuStart.ptr + index * _increment };

    public D3D12_GPU_DESCRIPTOR_HANDLE GpuHandle(uint index) => new() { ptr = _gpuStart.ptr + index * _increment };

    public void Dispose() => Release(ref _heap);
}

internal readonly record struct D3D12CpuDescriptor(D3D12_CPU_DESCRIPTOR_HANDLE Handle, int Page, int Index);

internal sealed unsafe class D3D12CpuDescriptorHeap(ID3D12Device* device, D3D12_DESCRIPTOR_HEAP_TYPE type, string name) : IDisposable
{
    private const int DescriptorsPerPage = 256;

    private readonly Lock _lock = new();
    private readonly List<nint> _pages = [];
    private readonly Stack<(int Page, int Index)> _freeSlots = new();
    private readonly uint _increment = device->GetDescriptorHandleIncrementSize(type);
    private int _nextIndexInLastPage = DescriptorsPerPage;

    public D3D12CpuDescriptor Allocate()
    {
        lock (_lock)
        {
            if (!_freeSlots.TryPop(out var slot))
            {
                if (_nextIndexInLastPage == DescriptorsPerPage) AddPage();
                slot = (_pages.Count - 1, _nextIndexInLastPage++);
            }
            var start = ((ID3D12DescriptorHeap*)_pages[slot.Page])->GetCPUDescriptorHandleForHeapStart();
            return new D3D12CpuDescriptor(new D3D12_CPU_DESCRIPTOR_HANDLE { ptr = start.ptr + (nuint)slot.Index * _increment }, slot.Page, slot.Index);
        }
    }

    public void Free(D3D12CpuDescriptor descriptor)
    {
        lock (_lock) _freeSlots.Push((descriptor.Page, descriptor.Index));
    }

    public void Dispose()
    {
        foreach (var page in _pages)
        {
            var heap = (ID3D12DescriptorHeap*)page;
            Release(ref heap);
        }
        _pages.Clear();
    }

    private void AddPage()
    {
        var description = new D3D12_DESCRIPTOR_HEAP_DESC { Type = type, NumDescriptors = DescriptorsPerPage, Flags = D3D12_DESCRIPTOR_HEAP_FLAG_NONE };
        ID3D12DescriptorHeap* heap;
        ThrowOnFailure(device->CreateDescriptorHeap(&description, __uuidof<ID3D12DescriptorHeap>(), (void**)&heap), "ID3D12Device::CreateDescriptorHeap");
        SetName((ID3D12Object*)heap, $"{name}{_pages.Count}");
        _pages.Add((nint)heap);
        _nextIndexInLastPage = 0;
    }
}
