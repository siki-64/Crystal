using TerraFX.Interop.DirectX;
using TerraFX.Interop.Windows;
using static Crystal.Graphics.D3D12.D3D12Interop;
using static TerraFX.Interop.DirectX.D3D12_BARRIER_LAYOUT;
using static TerraFX.Interop.DirectX.D3D12_DESCRIPTOR_HEAP_FLAGS;
using static TerraFX.Interop.DirectX.D3D12_DESCRIPTOR_HEAP_TYPE;
using static TerraFX.Interop.DirectX.DXGI;
using static TerraFX.Interop.DirectX.DXGI_ALPHA_MODE;
using static TerraFX.Interop.DirectX.DXGI_FEATURE;
using static TerraFX.Interop.DirectX.DXGI_FORMAT;
using static TerraFX.Interop.DirectX.DXGI_SCALING;
using static TerraFX.Interop.DirectX.DXGI_SWAP_CHAIN_FLAG;
using static TerraFX.Interop.DirectX.DXGI_SWAP_EFFECT;
using static TerraFX.Interop.Windows.Windows;

namespace Crystal.Graphics.D3D12;

internal sealed unsafe class D3D12SwapChain : IDisposable
{
    public const int BufferCount = 3;
    public const DXGI_FORMAT Format = DXGI_FORMAT_R8G8B8A8_UNORM;

    private readonly D3D12Device _device;
    private readonly uint _flags;
    private readonly D3D12Resource?[] _buffers = new D3D12Resource?[BufferCount];
    private IDXGISwapChain3* _swapChain;
    private ID3D12DescriptorHeap* _renderTargetViewHeap;
    private readonly uint _renderTargetViewSize;
    private HANDLE _frameLatencyWaitable;

    public D3D12SwapChain(D3D12Device device, nint windowHandle, int width, int height, int maximumFrameLatency)
    {
        _device = device;
        SupportsTearing = QueryTearingSupport(device.Factory);
        _flags = (uint)DXGI_SWAP_CHAIN_FLAG_FRAME_LATENCY_WAITABLE_OBJECT | (SupportsTearing ? (uint)DXGI_SWAP_CHAIN_FLAG_ALLOW_TEARING : 0);
        Width = Math.Max(width, 1);
        Height = Math.Max(height, 1);

        var description = new DXGI_SWAP_CHAIN_DESC1
        {
            Width = (uint)Width,
            Height = (uint)Height,
            Format = Format,
            SampleDesc = new DXGI_SAMPLE_DESC(1, 0),
            BufferUsage = DXGI_USAGE_RENDER_TARGET_OUTPUT,
            BufferCount = BufferCount,
            Scaling = DXGI_SCALING_STRETCH,
            SwapEffect = DXGI_SWAP_EFFECT_FLIP_DISCARD,
            AlphaMode = DXGI_ALPHA_MODE_IGNORE,
            Flags = _flags,
        };

        IDXGISwapChain1* swapChain1 = null;
        ThrowOnFailure(
            device.Factory->CreateSwapChainForHwnd((IUnknown*)device.DirectQueue.Queue, (HWND)windowHandle, &description, null, null, &swapChain1),
            "IDXGIFactory2::CreateSwapChainForHwnd");
        try
        {
            IDXGISwapChain3* swapChain;
            ThrowOnFailure(swapChain1->QueryInterface(__uuidof<IDXGISwapChain3>(), (void**)&swapChain), "QueryInterface(IDXGISwapChain3)");
            _swapChain = swapChain;
        }
        finally
        {
            Release(ref swapChain1);
        }

        ThrowOnFailure(device.Factory->MakeWindowAssociation((HWND)windowHandle, DXGI_MWA_NO_ALT_ENTER), "IDXGIFactory::MakeWindowAssociation");
        ThrowOnFailure(_swapChain->SetMaximumFrameLatency((uint)maximumFrameLatency), "IDXGISwapChain2::SetMaximumFrameLatency");
        _frameLatencyWaitable = _swapChain->GetFrameLatencyWaitableObject();

        var heapDescription = new D3D12_DESCRIPTOR_HEAP_DESC
        {
            Type = D3D12_DESCRIPTOR_HEAP_TYPE_RTV,
            NumDescriptors = BufferCount,
            Flags = D3D12_DESCRIPTOR_HEAP_FLAG_NONE,
        };
        ID3D12DescriptorHeap* heap;
        ThrowOnFailure(device.Device->CreateDescriptorHeap(&heapDescription, __uuidof<ID3D12DescriptorHeap>(), (void**)&heap), "ID3D12Device::CreateDescriptorHeap(RTV)");
        _renderTargetViewHeap = heap;
        _renderTargetViewSize = device.Device->GetDescriptorHandleIncrementSize(D3D12_DESCRIPTOR_HEAP_TYPE_RTV);

        AcquireBuffers();
    }

    public int Width { get; private set; }
    public int Height { get; private set; }
    public bool SupportsTearing { get; }
    public D3D12Resource CurrentBuffer => _buffers[_swapChain->GetCurrentBackBufferIndex()]!;

    public D3D12_CPU_DESCRIPTOR_HANDLE CurrentRenderTargetView
    {
        get
        {
            var handle = _renderTargetViewHeap->GetCPUDescriptorHandleForHeapStart();
            handle.ptr += _swapChain->GetCurrentBackBufferIndex() * _renderTargetViewSize;
            return handle;
        }
    }

    public void WaitForFrameLatency() => WaitForSingleObjectEx(_frameLatencyWaitable, 1000, TRUE);

    public void Resize(int width, int height)
    {
        width = Math.Max(width, 1);
        height = Math.Max(height, 1);
        if (width == Width && height == Height) return;

        _device.WaitForIdle();
        ReleaseBuffers();
        ThrowOnFailure(_swapChain->ResizeBuffers(BufferCount, (uint)width, (uint)height, Format, _flags), "IDXGISwapChain::ResizeBuffers");
        Width = width;
        Height = height;
        AcquireBuffers();
    }

    public HRESULT Present(bool vSync)
    {
        var syncInterval = vSync ? 1u : 0u;
        var flags = !vSync && SupportsTearing ? DXGI_PRESENT_ALLOW_TEARING : 0u;
        return _swapChain->Present(syncInterval, flags);
    }

    public void Dispose()
    {
        if (_swapChain == null) return;
        ReleaseBuffers();
        Release(ref _renderTargetViewHeap);
        CloseHandle(_frameLatencyWaitable);
        Release(ref _swapChain);
    }

    private void AcquireBuffers()
    {
        var handle = _renderTargetViewHeap->GetCPUDescriptorHandleForHeapStart();
        for (uint index = 0; index < BufferCount; index++)
        {
            ID3D12Resource* buffer;
            ThrowOnFailure(_swapChain->GetBuffer(index, __uuidof<ID3D12Resource>(), (void**)&buffer), "IDXGISwapChain::GetBuffer");
            _buffers[index] = new D3D12Resource(buffer, isTexture: true, D3D12_BARRIER_LAYOUT_PRESENT, $"BackBuffer{index}");
            _device.Device->CreateRenderTargetView(buffer, null, handle);
            handle.ptr += _renderTargetViewSize;
        }
    }

    private void ReleaseBuffers()
    {
        for (var index = 0; index < BufferCount; index++)
        {
            if (_buffers[index] is not { } buffer) continue;
            buffer.Pointer->Release();
            _buffers[index] = null;
        }
    }

    private static bool QueryTearingSupport(IDXGIFactory6* factory)
    {
        BOOL allowTearing = FALSE;
        return SUCCEEDED(factory->CheckFeatureSupport(DXGI_FEATURE_PRESENT_ALLOW_TEARING, &allowTearing, (uint)sizeof(BOOL))) && allowTearing;
    }
}
