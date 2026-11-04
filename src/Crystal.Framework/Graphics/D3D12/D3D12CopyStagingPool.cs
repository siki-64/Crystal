using TerraFX.Interop.DirectX;
using static Crystal.Graphics.D3D12.D3D12Interop;
using static TerraFX.Interop.DirectX.D3D12_BARRIER_LAYOUT;
using static TerraFX.Interop.DirectX.D3D12_HEAP_TYPE;
using static TerraFX.Interop.DirectX.D3D12_RESOURCE_FLAGS;

namespace Crystal.Graphics.D3D12;

internal sealed unsafe class D3D12CopyStagingPool(D3D12Device device) : IDisposable
{
    public const int PageSize = 4 << 20;

    private readonly object _lock = new();
    private readonly Queue<Page> _retiredPages = new();
    private readonly List<Page> _allPages = [];
    private Page? _currentPage;
    private int _offset;

    public int PageCount
    {
        get
        {
            lock (_lock) return _allPages.Count;
        }
    }

    public bool TryAllocate(ulong size, ulong alignment, out D3D12CopyStagingAllocation allocation)
    {
        allocation = default;
        if (size > PageSize) return false;
        lock (_lock)
        {
            var offset = (int)(((ulong)_offset + alignment - 1) / alignment * alignment);
            if (_currentPage == null || offset + (int)size > PageSize)
            {
                RetireCurrentPage();
                _currentPage = AcquirePage();
                offset = 0;
            }
            _offset = offset + (int)size;
            _currentPage.UploadsAwaitingSubmission++;
            allocation = new D3D12CopyStagingAllocation(_currentPage, _currentPage.Resource, (ulong)offset, _currentPage.CpuAddress + offset);
            return true;
        }
    }

    public void Submitted(in D3D12CopyStagingAllocation allocation, ulong copyValue)
    {
        lock (_lock)
        {
            var page = allocation.Page;
            page.CopyValue = Math.Max(page.CopyValue, copyValue);
            page.UploadsAwaitingSubmission--;
            if (page.IsFull && page.UploadsAwaitingSubmission == 0) _retiredPages.Enqueue(page);
        }
    }

    public void TrimCompletedPages(int retainedPageCount)
    {
        lock (_lock)
        {
            while (_allPages.Count > retainedPageCount && _retiredPages.TryPeek(out var oldest) && device.CopyQueue.IsComplete(oldest.CopyValue))
            {
                _retiredPages.Dequeue();
                _allPages.Remove(oldest);
                oldest.Resource->Release();
            }
        }
    }

    public void Dispose()
    {
        lock (_lock)
        {
            foreach (var page in _allPages) page.Resource->Release();
            _allPages.Clear();
            _retiredPages.Clear();
            _currentPage = null;
        }
    }

    private void RetireCurrentPage()
    {
        if (_currentPage is not { } page) return;
        page.IsFull = true;
        if (page.UploadsAwaitingSubmission == 0) _retiredPages.Enqueue(page);
        _currentPage = null;
    }

    private Page AcquirePage()
    {
        if (_retiredPages.TryPeek(out var oldest) && device.CopyQueue.IsComplete(oldest.CopyValue))
        {
            var reused = _retiredPages.Dequeue();
            reused.IsFull = false;
            return reused;
        }

        var resource = device.CreateCommittedResource(
            D3D12_HEAP_TYPE_UPLOAD, D3D12Device.BufferDescription(PageSize, D3D12_RESOURCE_FLAG_NONE), D3D12_BARRIER_LAYOUT_UNDEFINED, null, "CopyStagingPage");
        var emptyRange = new D3D12_RANGE();
        byte* mapped;
        ThrowOnFailure(resource->Map(0, &emptyRange, (void**)&mapped), "ID3D12Resource::Map");
        var page = new Page(resource, mapped);
        _allPages.Add(page);
        return page;
    }

    internal sealed class Page(ID3D12Resource* resource, byte* cpuAddress)
    {
        public ID3D12Resource* Resource { get; } = resource;
        public byte* CpuAddress { get; } = cpuAddress;
        public ulong CopyValue { get; set; }
        public int UploadsAwaitingSubmission { get; set; }
        public bool IsFull { get; set; }
    }
}

internal readonly unsafe struct D3D12CopyStagingAllocation(D3D12CopyStagingPool.Page page, ID3D12Resource* resource, ulong offset, byte* cpuAddress)
{
    public D3D12CopyStagingPool.Page Page { get; } = page;
    public ID3D12Resource* Resource { get; } = resource;
    public ulong Offset { get; } = offset;
    public byte* CpuAddress { get; } = cpuAddress;
}
