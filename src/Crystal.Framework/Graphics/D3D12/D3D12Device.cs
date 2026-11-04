using System.Text;
using TerraFX.Interop.DirectX;
using TerraFX.Interop.Windows;
using static Crystal.Graphics.D3D12.D3D12Interop;
using static TerraFX.Interop.DirectX.D3D_FEATURE_LEVEL;
using static TerraFX.Interop.DirectX.D3D12_BARRIER_LAYOUT;
using static TerraFX.Interop.DirectX.D3D12_COMMAND_LIST_TYPE;
using static TerraFX.Interop.DirectX.D3D12_DESCRIPTOR_HEAP_TYPE;
using static TerraFX.Interop.DirectX.D3D12_DRED_ENABLEMENT;
using static TerraFX.Interop.DirectX.D3D12_FEATURE;
using static TerraFX.Interop.DirectX.D3D12_HEAP_FLAGS;
using static TerraFX.Interop.DirectX.D3D12_INDIRECT_ARGUMENT_TYPE;
using static TerraFX.Interop.DirectX.D3D12_RESOURCE_DIMENSION;
using static TerraFX.Interop.DirectX.D3D12_RESOURCE_FLAGS;
using static TerraFX.Interop.DirectX.D3D12_TEXTURE_LAYOUT;
using static TerraFX.Interop.DirectX.DirectX;
using static TerraFX.Interop.DirectX.DXGI;
using static TerraFX.Interop.DirectX.DXGI_ADAPTER_FLAG;
using static TerraFX.Interop.DirectX.DXGI_FORMAT;
using static TerraFX.Interop.DirectX.DXGI_GPU_PREFERENCE;
using static TerraFX.Interop.DirectX.DXGI_MEMORY_SEGMENT_GROUP;
using static TerraFX.Interop.Windows.Windows;

namespace Crystal.Graphics.D3D12;

internal sealed unsafe class D3D12Device : IDisposable
{
    private const D3D_FEATURE_LEVEL MinimumFeatureLevel = D3D_FEATURE_LEVEL_12_0;
    private const uint SamplerHeapCapacity = 2048;

    private IDXGIFactory6* _factory;
    private IDXGIAdapter3* _adapter;
    private ID3D12Device10* _device;
    private ID3D12CommandSignature* _drawCommandSignature;
    private readonly Lock _descriptorIndexedRootSignatureLock = new();
    private ID3D12RootSignature* _descriptorIndexedGraphicsRootSignature;
    private ID3D12RootSignature* _descriptorIndexedComputeRootSignature;
    private ID3D12CommandSignature* _drawIndexedCommandSignature;
    private ID3D12CommandSignature* _dispatchCommandSignature;

    public D3D12Device(GraphicsDeviceOptions options)
    {
        var deviceFactory = TryCreateAgilityDeviceFactory();
        try
        {
            UsesAgilitySdk = deviceFactory != null;
            if (options.EnableDebugLayer) EnableDebugLayer(deviceFactory, options.EnableGpuBasedValidation);

            _factory = CreateDxgiFactory(options.EnableDebugLayer);
            _device = CreateDevice(deviceFactory, _factory, options.AdapterPreference, out var adapter);
            Adapter = adapter;
            var luid = _device->GetAdapterLuid();
            IDXGIAdapter3* adapter3;
            ThrowOnFailure(_factory->EnumAdapterByLuid(luid, __uuidof<IDXGIAdapter3>(), (void**)&adapter3), "IDXGIFactory4::EnumAdapterByLuid");
            _adapter = adapter3;
        }
        finally
        {
            Release(ref deviceFactory);
        }

        if (options.EnableDebugLayer) DebugMessages = D3D12DebugMessages.TryRegister(Device);

        DirectQueue = new D3D12Queue(Device, D3D12_COMMAND_LIST_TYPE_DIRECT, "DirectQueue");
        ComputeQueue = new D3D12Queue(Device, D3D12_COMMAND_LIST_TYPE_COMPUTE, "ComputeQueue");
        CopyQueue = new D3D12Queue(Device, D3D12_COMMAND_LIST_TYPE_COPY, "CopyQueue");
        CopyCommandLists = new D3D12CommandListPool(Device, CopyQueue, "CopyCommandList");
        ShaderResourceHeap = new D3D12ShaderVisibleHeap(Device, D3D12_DESCRIPTOR_HEAP_TYPE_CBV_SRV_UAV, (uint)options.ShaderResourceDescriptorCapacity, "ShaderResourceHeap");
        SamplerHeap = new D3D12ShaderVisibleHeap(Device, D3D12_DESCRIPTOR_HEAP_TYPE_SAMPLER, SamplerHeapCapacity, "SamplerHeap");
        RenderTargetViewHeap = new D3D12CpuDescriptorHeap(Device, D3D12_DESCRIPTOR_HEAP_TYPE_RTV, "RenderTargetViewHeap");
        DepthStencilViewHeap = new D3D12CpuDescriptorHeap(Device, D3D12_DESCRIPTOR_HEAP_TYPE_DSV, "DepthStencilViewHeap");
        OcclusionQueries = new D3D12OcclusionQueryPool(_device);
        _drawCommandSignature = CreateCommandSignature(D3D12_INDIRECT_ARGUMENT_TYPE_DRAW, (uint)sizeof(IndirectDrawArguments), "DrawCommandSignature");
        _drawIndexedCommandSignature = CreateCommandSignature(D3D12_INDIRECT_ARGUMENT_TYPE_DRAW_INDEXED, (uint)sizeof(IndirectDrawIndexedArguments), "DrawIndexedCommandSignature");
        _dispatchCommandSignature = CreateCommandSignature(D3D12_INDIRECT_ARGUMENT_TYPE_DISPATCH, (uint)sizeof(IndirectDispatchArguments), "DispatchCommandSignature");
    }

    public bool UsesAgilitySdk { get; }
    public GraphicsAdapterInfo Adapter { get; }
    public D3D12DebugMessages? DebugMessages { get; }
    public IDXGIFactory6* Factory => _factory;
    public ID3D12Device* Device => (ID3D12Device*)_device;
    public ID3D12Device10* Device10 => _device;
    public D3D12Queue DirectQueue { get; } = null!;
    public D3D12Queue ComputeQueue { get; } = null!;
    public D3D12Queue CopyQueue { get; } = null!;
    public D3D12CommandListPool CopyCommandLists { get; } = null!;
    public D3D12ShaderVisibleHeap ShaderResourceHeap { get; } = null!;
    public D3D12ShaderVisibleHeap SamplerHeap { get; } = null!;
    public D3D12CpuDescriptorHeap RenderTargetViewHeap { get; } = null!;
    public D3D12CpuDescriptorHeap DepthStencilViewHeap { get; } = null!;
    public D3D12DeferredReleases DeferredReleases { get; } = new();
    public D3D12OcclusionQueryPool OcclusionQueries { get; } = null!;
    public ID3D12CommandSignature* DrawCommandSignature => _drawCommandSignature;
    public ID3D12CommandSignature* DrawIndexedCommandSignature => _drawIndexedCommandSignature;
    public ID3D12CommandSignature* DispatchCommandSignature => _dispatchCommandSignature;

    public ID3D12RootSignature* DescriptorIndexedGraphicsRootSignature
    {
        get
        {
            lock (_descriptorIndexedRootSignatureLock)
            {
                if (_descriptorIndexedGraphicsRootSignature == null) _descriptorIndexedGraphicsRootSignature = D3D12RootSignatures.CreateDescriptorIndexedGraphics(this);
                return _descriptorIndexedGraphicsRootSignature;
            }
        }
    }

    public ID3D12RootSignature* DescriptorIndexedComputeRootSignature
    {
        get
        {
            lock (_descriptorIndexedRootSignatureLock)
            {
                if (_descriptorIndexedComputeRootSignature == null) _descriptorIndexedComputeRootSignature = D3D12RootSignatures.CreateDescriptorIndexedCompute(this);
                return _descriptorIndexedComputeRootSignature;
            }
        }
    }

    public VideoMemoryInfo QueryVideoMemoryInfo()
    {
        DXGI_QUERY_VIDEO_MEMORY_INFO local;
        DXGI_QUERY_VIDEO_MEMORY_INFO nonLocal;
        ThrowOnFailure(_adapter->QueryVideoMemoryInfo(0, DXGI_MEMORY_SEGMENT_GROUP_LOCAL, &local), "IDXGIAdapter3::QueryVideoMemoryInfo");
        ThrowOnFailure(_adapter->QueryVideoMemoryInfo(0, DXGI_MEMORY_SEGMENT_GROUP_NON_LOCAL, &nonLocal), "IDXGIAdapter3::QueryVideoMemoryInfo");
        return new VideoMemoryInfo((long)local.Budget, (long)local.CurrentUsage, (long)nonLocal.Budget, (long)nonLocal.CurrentUsage);
    }

    public void WaitForIdle()
    {
        CopyQueue?.WaitForIdle();
        ComputeQueue?.WaitForIdle();
        DirectQueue?.WaitForIdle();
    }

    public void ProcessDeferredReleases() => DeferredReleases.Process(DirectQueue.CompletedValue, ComputeQueue.CompletedValue, CopyQueue.CompletedValue);

    public ID3D12Resource* CreateCommittedResource(
        D3D12_HEAP_TYPE heapType, in D3D12_RESOURCE_DESC1 description, D3D12_BARRIER_LAYOUT initialLayout, D3D12_CLEAR_VALUE* optimizedClearValue, string name)
    {
        var heapProperties = new D3D12_HEAP_PROPERTIES { Type = heapType };
        var resourceDescription = description;
        ID3D12Resource* resource;
        ThrowOnFailure(
            _device->CreateCommittedResource3(
                &heapProperties, D3D12_HEAP_FLAG_NONE, &resourceDescription, initialLayout, optimizedClearValue, null, 0, null,
                __uuidof<ID3D12Resource>(), (void**)&resource),
            $"ID3D12Device10::CreateCommittedResource3({name})");
        SetName((ID3D12Object*)resource, name);
        return resource;
    }

    public ID3D12Resource* CreateStagingBuffer(D3D12_HEAP_TYPE heapType, ulong size, string name) =>
        CreateCommittedResource(heapType, BufferDescription(size, D3D12_RESOURCE_FLAG_NONE), D3D12_BARRIER_LAYOUT_UNDEFINED, null, name);

    public static D3D12_RESOURCE_DESC1 BufferDescription(ulong size, D3D12_RESOURCE_FLAGS flags) => new()
    {
        Dimension = D3D12_RESOURCE_DIMENSION_BUFFER,
        Width = size,
        Height = 1,
        DepthOrArraySize = 1,
        MipLevels = 1,
        Format = DXGI_FORMAT_UNKNOWN,
        SampleDesc = new DXGI_SAMPLE_DESC(1, 0),
        Layout = D3D12_TEXTURE_LAYOUT_ROW_MAJOR,
        Flags = flags,
    };

    public D3D12_PLACED_SUBRESOURCE_FOOTPRINT GetFootprint(in D3D12_RESOURCE_DESC1 description, uint subresource, out uint rowCount, out ulong rowSize, out ulong totalSize)
    {
        var resourceDescription = description;
        D3D12_PLACED_SUBRESOURCE_FOOTPRINT footprint;
        uint rows;
        ulong rowBytes;
        ulong total;
        _device->GetCopyableFootprints1(&resourceDescription, subresource, 1, 0, &footprint, &rows, &rowBytes, &total);
        rowCount = rows;
        rowSize = rowBytes;
        totalSize = total;
        return footprint;
    }

    public bool SupportsSampleCount(DXGI_FORMAT format, int sampleCount)
    {
        if (sampleCount == 1) return true;
        var levels = new D3D12_FEATURE_DATA_MULTISAMPLE_QUALITY_LEVELS { Format = format, SampleCount = (uint)sampleCount };
        return SUCCEEDED(_device->CheckFeatureSupport(D3D12_FEATURE_MULTISAMPLE_QUALITY_LEVELS, &levels, (uint)sizeof(D3D12_FEATURE_DATA_MULTISAMPLE_QUALITY_LEVELS)))
            && levels.NumQualityLevels > 0;
    }

    public void ThrowIfPresentFailed(HRESULT result)
    {
        if (result == DXGI_ERROR_DEVICE_REMOVED || result == DXGI_ERROR_DEVICE_RESET)
            throw new GraphicsException($"The D3D12 device was removed (reason 0x{_device->GetDeviceRemovedReason().Value:X8}).");
        ThrowOnFailure(result, "IDXGISwapChain::Present");
    }

    public void Dispose()
    {
        if (_device == null) return;
        WaitForIdle();
        DeferredReleases.ProcessAll();
        CopyCommandLists?.Dispose();
        Release(ref _drawCommandSignature);
        Release(ref _drawIndexedCommandSignature);
        Release(ref _dispatchCommandSignature);
        Release(ref _descriptorIndexedGraphicsRootSignature);
        Release(ref _descriptorIndexedComputeRootSignature);
        OcclusionQueries?.Dispose();
        ShaderResourceHeap?.Dispose();
        SamplerHeap?.Dispose();
        RenderTargetViewHeap?.Dispose();
        DepthStencilViewHeap?.Dispose();
        CopyQueue?.Dispose();
        ComputeQueue?.Dispose();
        DirectQueue?.Dispose();
        DebugMessages?.Dispose();
        Release(ref _device);
        Release(ref _adapter);
        Release(ref _factory);
    }

    private ID3D12CommandSignature* CreateCommandSignature(D3D12_INDIRECT_ARGUMENT_TYPE type, uint stride, string name)
    {
        var argument = new D3D12_INDIRECT_ARGUMENT_DESC { Type = type };
        var description = new D3D12_COMMAND_SIGNATURE_DESC { ByteStride = stride, NumArgumentDescs = 1, pArgumentDescs = &argument };
        ID3D12CommandSignature* signature;
        ThrowOnFailure(Device->CreateCommandSignature(&description, null, __uuidof<ID3D12CommandSignature>(), (void**)&signature), $"ID3D12Device::CreateCommandSignature({name})");
        SetName((ID3D12Object*)signature, name);
        return signature;
    }

    private static ID3D12DeviceFactory* TryCreateAgilityDeviceFactory()
    {
        var sdkPath = Path.Combine(AppContext.BaseDirectory, "D3D12") + Path.DirectorySeparatorChar;
        if (!File.Exists(Path.Combine(sdkPath, "D3D12Core.dll"))) return null;

        ID3D12SDKConfiguration1* configuration = null;
        if (FAILED(D3D12GetInterface(AddressOf(in CLSID.CLSID_D3D12SDKConfiguration), __uuidof<ID3D12SDKConfiguration1>(), (void**)&configuration)))
            return null;

        try
        {
            var sdkPathBytes = Encoding.UTF8.GetBytes(sdkPath + '\0');
            ID3D12DeviceFactory* deviceFactory = null;
            fixed (byte* sdkPathPointer = sdkPathBytes)
            {
                var result = configuration->CreateDeviceFactory(AgilitySdkVersionAttribute.Current, (sbyte*)sdkPathPointer, __uuidof<ID3D12DeviceFactory>(), (void**)&deviceFactory);
                if (FAILED(result)) return null;
            }
            return deviceFactory;
        }
        finally
        {
            Release(ref configuration);
        }
    }

    private static void EnableDebugLayer(ID3D12DeviceFactory* deviceFactory, bool gpuBasedValidation)
    {
        ID3D12Debug1* debug = null;
        var result = deviceFactory != null
            ? deviceFactory->GetConfigurationInterface(AddressOf(in CLSID.CLSID_D3D12Debug), __uuidof<ID3D12Debug1>(), (void**)&debug)
            : D3D12GetDebugInterface(__uuidof<ID3D12Debug1>(), (void**)&debug);
        if (FAILED(result))
            throw new GraphicsException($"The D3D12 debug layer is unavailable (HRESULT 0x{result.Value:X8}). Install the Graphics Tools optional feature.");

        debug->EnableDebugLayer();
        if (gpuBasedValidation) debug->SetEnableGPUBasedValidation(TRUE);
        Release(ref debug);

        ID3D12DeviceRemovedExtendedDataSettings* dred = null;
        result = deviceFactory != null
            ? deviceFactory->GetConfigurationInterface(AddressOf(in CLSID.CLSID_D3D12DeviceRemovedExtendedData), __uuidof<ID3D12DeviceRemovedExtendedDataSettings>(), (void**)&dred)
            : D3D12GetDebugInterface(__uuidof<ID3D12DeviceRemovedExtendedDataSettings>(), (void**)&dred);
        if (SUCCEEDED(result))
        {
            dred->SetAutoBreadcrumbsEnablement(D3D12_DRED_ENABLEMENT_FORCED_ON);
            dred->SetPageFaultEnablement(D3D12_DRED_ENABLEMENT_FORCED_ON);
            Release(ref dred);
        }
    }

    private static IDXGIFactory6* CreateDxgiFactory(bool debug)
    {
        IDXGIFactory6* factory = null;
        if (debug && SUCCEEDED(CreateDXGIFactory2(DXGI_CREATE_FACTORY_DEBUG, __uuidof<IDXGIFactory6>(), (void**)&factory))) return factory;
        ThrowOnFailure(CreateDXGIFactory2(0, __uuidof<IDXGIFactory6>(), (void**)&factory), "CreateDXGIFactory2");
        return factory;
    }

    private static ID3D12Device10* CreateDevice(
        ID3D12DeviceFactory* deviceFactory, IDXGIFactory6* factory, GraphicsAdapterPreference preference, out GraphicsAdapterInfo adapterInfo)
    {
        if (preference == GraphicsAdapterPreference.Software)
        {
            IDXGIAdapter1* warp = null;
            ThrowOnFailure(factory->EnumWarpAdapter(__uuidof<IDXGIAdapter1>(), (void**)&warp), "IDXGIFactory4::EnumWarpAdapter");
            try
            {
                var device = TryCreateDevice(deviceFactory, warp, out var failure);
                if (device == null) throw new GraphicsException($"WARP could not create a feature level 12_0 device with enhanced barriers, shader model 6.6 and resource binding tier 3: {failure}.");
                adapterInfo = Describe(warp);
                return device;
            }
            finally
            {
                Release(ref warp);
            }
        }

        var gpuPreference = preference == GraphicsAdapterPreference.MinimumPower ? DXGI_GPU_PREFERENCE_MINIMUM_POWER : DXGI_GPU_PREFERENCE_HIGH_PERFORMANCE;
        var failures = new List<string>();
        IDXGIAdapter1* adapter = null;
        for (uint index = 0; factory->EnumAdapterByGpuPreference(index, gpuPreference, __uuidof<IDXGIAdapter1>(), (void**)&adapter) != DXGI_ERROR_NOT_FOUND; index++)
        {
            try
            {
                var info = Describe(adapter);
                if (info.IsSoftware) continue;
                var device = TryCreateDevice(deviceFactory, adapter, out var failure);
                if (device == null)
                {
                    failures.Add($"{info.Description}: {failure}");
                    continue;
                }
                adapterInfo = info;
                return device;
            }
            finally
            {
                Release(ref adapter);
            }
        }

        throw new GraphicsException(failures.Count == 0
            ? "No hardware adapter supports Direct3D 12."
            : $"No hardware adapter could create a feature level 12_0 device with enhanced barriers: {string.Join("; ", failures)}.");
    }

    private static ID3D12Device10* TryCreateDevice(ID3D12DeviceFactory* deviceFactory, IDXGIAdapter1* adapter, out string? failure)
    {
        ID3D12Device10* device = null;
        var result = deviceFactory != null
            ? deviceFactory->CreateDevice((IUnknown*)adapter, MinimumFeatureLevel, __uuidof<ID3D12Device10>(), (void**)&device)
            : D3D12CreateDevice((IUnknown*)adapter, MinimumFeatureLevel, __uuidof<ID3D12Device10>(), (void**)&device);
        if (FAILED(result))
        {
            failure = $"HRESULT 0x{result.Value:X8}";
            return null;
        }

        if (!SupportsEnhancedBarriers(device))
        {
            Release(ref device);
            failure = "enhanced barriers are not supported";
            return null;
        }

        if (!SupportsDescriptorIndexing(device))
        {
            Release(ref device);
            failure = "shader model 6.6 with resource binding tier 3 is not supported";
            return null;
        }

        failure = null;
        return device;
    }

    private static bool SupportsEnhancedBarriers(ID3D12Device10* device)
    {
        D3D12_FEATURE_DATA_D3D12_OPTIONS12 options = default;
        return SUCCEEDED(device->CheckFeatureSupport(D3D12_FEATURE_D3D12_OPTIONS12, &options, (uint)sizeof(D3D12_FEATURE_DATA_D3D12_OPTIONS12)))
            && options.EnhancedBarriersSupported;
    }

    private static bool SupportsDescriptorIndexing(ID3D12Device10* device)
    {
        var shaderModel = new D3D12_FEATURE_DATA_SHADER_MODEL { HighestShaderModel = D3D_SHADER_MODEL.D3D_SHADER_MODEL_6_6 };
        D3D12_FEATURE_DATA_D3D12_OPTIONS options = default;
        return SUCCEEDED(device->CheckFeatureSupport(D3D12_FEATURE_SHADER_MODEL, &shaderModel, (uint)sizeof(D3D12_FEATURE_DATA_SHADER_MODEL)))
            && shaderModel.HighestShaderModel >= D3D_SHADER_MODEL.D3D_SHADER_MODEL_6_6
            && SUCCEEDED(device->CheckFeatureSupport(D3D12_FEATURE_D3D12_OPTIONS, &options, (uint)sizeof(D3D12_FEATURE_DATA_D3D12_OPTIONS)))
            && options.ResourceBindingTier >= D3D12_RESOURCE_BINDING_TIER.D3D12_RESOURCE_BINDING_TIER_3;
    }

    private static GraphicsAdapterInfo Describe(IDXGIAdapter1* adapter)
    {
        DXGI_ADAPTER_DESC1 description;
        ThrowOnFailure(adapter->GetDesc1(&description), "IDXGIAdapter1::GetDesc1");
        ReadOnlySpan<char> name = description.Description;
        var terminator = name.IndexOf('\0');
        return new GraphicsAdapterInfo(
            (terminator < 0 ? name : name[..terminator]).ToString(),
            description.VendorId,
            description.DeviceId,
            description.DedicatedVideoMemory,
            (description.Flags & (uint)DXGI_ADAPTER_FLAG_SOFTWARE) != 0);
    }
}
