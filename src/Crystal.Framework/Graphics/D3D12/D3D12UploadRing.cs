using TerraFX.Interop.DirectX;
using TerraFX.Interop.Windows;
using static Crystal.Graphics.D3D12.D3D12Interop;
using static TerraFX.Interop.DirectX.D3D12_BARRIER_LAYOUT;
using static TerraFX.Interop.DirectX.D3D12_HEAP_TYPE;
using static TerraFX.Interop.DirectX.D3D12_RESOURCE_FLAGS;

namespace Crystal.Graphics.D3D12;

internal sealed unsafe class D3D12UploadRing(D3D12Device device) : IDisposable
{
    public const int PageSize = 1 << 20;
    public const int RetirementsPerTrim = 256;
    public const int RetainedSparePageCount = 2;

    private readonly Queue<Page> _retiredPages = new();
    private readonly List<Page> _pagesInUse = [];
    private readonly List<Page> _allPages = [];
    private readonly List<nint> _dedicatedInUse = [];
    private Page? _currentPage;
    private int _offset;
    private int _retirementsSinceTrim;
    private int _fewestSparePagesSinceTrim = int.MaxValue;

    public int PageCount => _allPages.Count;

    public byte* Allocate(int size, int alignment, out ulong gpuAddress) => Allocate(size, alignment, out gpuAddress, out _, out _);

    public byte* Allocate(int size, int alignment, out ulong gpuAddress, out ID3D12Resource* resource, out ulong resourceOffset)
    {
        if (size > PageSize)
        {
            resourceOffset = 0;
            return AllocateDedicated(size, out gpuAddress, out resource);
        }
        var offset = (_offset + alignment - 1) / alignment * alignment;
        if (_currentPage == null || offset + size > PageSize)
        {
            _currentPage = AcquirePage();
            _pagesInUse.Add(_currentPage);
            offset = 0;
        }
        _offset = offset + size;
        gpuAddress = _currentPage.GpuAddress + (ulong)offset;
        resource = _currentPage.Resource;
        resourceOffset = (ulong)offset;
        return _currentPage.CpuAddress + offset;
    }

    public void Retire(ulong directValue, ulong computeValue)
    {
        foreach (var dedicated in _dedicatedInUse) device.DeferredReleases.Release((IUnknown*)dedicated, new D3D12ReleaseValues(directValue, computeValue, 0));
        _dedicatedInUse.Clear();
        TrimSparePages();
        foreach (var page in _pagesInUse)
        {
            page.DirectValue = directValue;
            page.ComputeValue = computeValue;
            _retiredPages.Enqueue(page);
        }
        _pagesInUse.Clear();
        _currentPage = null;
        _offset = 0;
    }

    public void Dispose()
    {
        foreach (var page in _allPages) page.Resource->Release();
        foreach (var dedicated in _dedicatedInUse) ((ID3D12Resource*)dedicated)->Release();
        _dedicatedInUse.Clear();
        _allPages.Clear();
        _retiredPages.Clear();
        _pagesInUse.Clear();
        _currentPage = null;
    }

    private void TrimSparePages()
    {
        var sparePages = 0;
        foreach (var page in _retiredPages)
        {
            if (!IsComplete(page)) break;
            sparePages++;
        }
        _fewestSparePagesSinceTrim = Math.Min(_fewestSparePagesSinceTrim, sparePages);
        if (++_retirementsSinceTrim < RetirementsPerTrim) return;

        for (var surplus = _fewestSparePagesSinceTrim - RetainedSparePageCount; surplus > 0; surplus--)
        {
            var page = _retiredPages.Dequeue();
            _allPages.Remove(page);
            page.Resource->Release();
        }
        _retirementsSinceTrim = 0;
        _fewestSparePagesSinceTrim = int.MaxValue;
    }

    private bool IsComplete(Page page) => device.DirectQueue.IsComplete(page.DirectValue) && device.ComputeQueue.IsComplete(page.ComputeValue);

    private Page AcquirePage()
    {
        if (_retiredPages.TryPeek(out var oldest) && IsComplete(oldest)) return _retiredPages.Dequeue();

        var resource = CreateMappedBuffer(PageSize, "UploadRingPage", out var cpuAddress);
        var page = new Page(resource, cpuAddress, resource->GetGPUVirtualAddress());
        _allPages.Add(page);
        return page;
    }

    private byte* AllocateDedicated(int size, out ulong gpuAddress, out ID3D12Resource* dedicatedResource)
    {
        var resource = CreateMappedBuffer(size, "UploadRingDedicated", out var cpuAddress);
        _dedicatedInUse.Add((nint)resource);
        gpuAddress = resource->GetGPUVirtualAddress();
        dedicatedResource = resource;
        return cpuAddress;
    }

    private ID3D12Resource* CreateMappedBuffer(int size, string name, out byte* cpuAddress)
    {
        var resource = device.CreateCommittedResource(
            D3D12_HEAP_TYPE_UPLOAD, D3D12Device.BufferDescription((ulong)size, D3D12_RESOURCE_FLAG_NONE), D3D12_BARRIER_LAYOUT_UNDEFINED, null, name);
        var emptyRange = new D3D12_RANGE();
        byte* mapped;
        ThrowOnFailure(resource->Map(0, &emptyRange, (void**)&mapped), "ID3D12Resource::Map");
        cpuAddress = mapped;
        return resource;
    }

    private sealed class Page(ID3D12Resource* resource, byte* cpuAddress, ulong gpuAddress)
    {
        public ID3D12Resource* Resource { get; } = resource;
        public byte* CpuAddress { get; } = cpuAddress;
        public ulong GpuAddress { get; } = gpuAddress;
        public ulong DirectValue { get; set; }
        public ulong ComputeValue { get; set; }
    }
}
