using System.Numerics;
using System.Runtime.InteropServices;
using Crystal.Graphics.D3D12;
using TerraFX.Interop.DirectX;
using TerraFX.Interop.Windows;
using static Crystal.Graphics.D3D12.D3D12Interop;
using static TerraFX.Interop.DirectX.D3D_PRIMITIVE_TOPOLOGY;
using static TerraFX.Interop.DirectX.D3D12_BARRIER_ACCESS;
using static TerraFX.Interop.DirectX.D3D12_BARRIER_LAYOUT;
using static TerraFX.Interop.DirectX.D3D12_BARRIER_SYNC;
using static TerraFX.Interop.DirectX.D3D12_CLEAR_FLAGS;
using static TerraFX.Interop.DirectX.D3D12_COMMAND_LIST_TYPE;
using static TerraFX.Interop.DirectX.D3D12_HEAP_TYPE;
using static TerraFX.Interop.DirectX.D3D12_RESOURCE_DIMENSION;
using static TerraFX.Interop.DirectX.D3D12_TEXTURE_LAYOUT;
using static TerraFX.Interop.Windows.Windows;

namespace Crystal.Graphics;

public sealed unsafe partial class GraphicsDevice : IDisposable
{
    public const int MaximumRenderTargets = 8;

    private const int ConstantBufferAlignment = 256;
    private const int TransientDataAlignment = 16;

    private readonly D3D12Device _device;
    private readonly D3D12SwapChain _swapChain;
    private readonly D3D12CommandContext _directContext = null!;
    private readonly D3D12CommandContext _computeContext = null!;
    private readonly D3D12CommandContext _handoffContext = null!;
    private readonly ulong[] _frameFenceValues;
    private readonly ulong[] _frameComputeFenceValues;
    private readonly int _renderThreadId = Environment.CurrentManagedThreadId;
    private readonly List<GraphicsDebugMessage> _debugMessages = [];
    private readonly List<GraphicsReadback> _readbacksAwaitingSubmission = [];
    private readonly HashSet<GraphicsResource> _liveResources = [];
    private readonly Dictionary<SamplerDescription, SamplerState> _samplerStates = [];
    private int _frameIndex;
    private bool _isInFrame;
    private Point? _pendingBackBufferSize;
    private bool _isDisposed;
    private D3D12UploadRing _uploadRing = null!;
    private const int RetainedCopyStagingPageCount = 16;
    private const int RetainedCopyCommandListCount = 32;
    private D3D12CopyStagingPool _copyStagingPool = null!;
    private readonly List<GraphicsCommandList> _commandLists = [];
    private GraphicsRecordingState _immediate = null!;
    private GraphicsMetrics _executedMetrics;
    private ulong _frameNumber;

    [ThreadStatic]
    private static GraphicsRecordingState? s_boundRecording;

    internal GraphicsDevice(GameWindow window, GraphicsDeviceOptions options)
    {
        Options = options.ApplyEnvironment();
        var frameCount = Math.Clamp(Options.MaximumFrameLatency, 1, D3D12SwapChain.BufferCount - 1);
        VSync = Options.VSync;
        _frameFenceValues = new ulong[frameCount];
        _frameComputeFenceValues = new ulong[frameCount];

        _device = new D3D12Device(Options);
        try
        {
            var size = window.ClientSize;
            _swapChain = new D3D12SwapChain(_device, window.Handle, size.X, size.Y, frameCount);
            _directContext = new D3D12CommandContext(_device, D3D12QueueKind.Direct) { Opened = OnDirectListOpened };
            _computeContext = new D3D12CommandContext(_device, D3D12QueueKind.Compute) { Peer = _directContext };
            _directContext.Peer = _computeContext;
            _handoffContext = new D3D12CommandContext(_device, D3D12QueueKind.Direct) { Peer = _computeContext };
            AsyncCompute = new AsyncComputeQueue(this);
            Profiler = new GraphicsProfiler(this, frameCount);
            _uploadRing = new D3D12UploadRing(_device);
            _immediate = new GraphicsRecordingState(this, _directContext, _uploadRing);
            _copyStagingPool = new D3D12CopyStagingPool(_device);
        }
        catch
        {
            Dispose();
            throw;
        }
        DispatchDebugMessages();
    }

    public static GraphicsAdapterInfo ProbeAdapter(GraphicsDeviceOptions options)
    {
        using var device = new D3D12Device(options.ApplyEnvironment());
        return device.Adapter;
    }

    public GraphicsDeviceOptions Options { get; }
    public GraphicsAdapterInfo Adapter => _device.Adapter;
    public bool IsDebugLayerEnabled => Options.EnableDebugLayer;
    public int BackBufferWidth => _swapChain.Width;
    public int BackBufferHeight => _swapChain.Height;
    public bool VSync { get; set; }
    public SurfaceFormat BackBufferFormat => SurfaceFormat.Rgba8Unorm;

    public void GetBackBufferData<T>(Span<T> destination) where T : unmanaged
    {
        using var readback = BeginGetBackBufferData();
        readback.GetData(destination);
    }

    public GraphicsReadback BeginGetBackBufferData()
    {
        EnsureInFrame();
        var description = new D3D12_RESOURCE_DESC1
        {
            Dimension = D3D12_RESOURCE_DIMENSION_TEXTURE2D,
            Width = (ulong)BackBufferWidth,
            Height = (uint)BackBufferHeight,
            DepthOrArraySize = 1,
            MipLevels = 1,
            Format = D3D12SwapChain.Format,
            SampleDesc = new DXGI_SAMPLE_DESC(1, 0),
            Layout = D3D12_TEXTURE_LAYOUT_UNKNOWN,
        };
        return BeginTextureReadback(_swapChain.CurrentBuffer, description, 0);
    }

    public Viewport Viewport
    {
        get => Recording.Viewport;
        set
        {
            var state = Recording;
            state.Viewport = value;
            state.ViewportNeedsBinding = true;
        }
    }

    public Rectangle ScissorRectangle
    {
        get => Recording.ScissorRectangle;
        set
        {
            var state = Recording;
            state.ScissorRectangle = value;
            state.ScissorRectangleNeedsBinding = true;
        }
    }

    public int StencilReference
    {
        get => Recording.StencilReference;
        set
        {
            var state = Recording;
            state.StencilReference = value;
            state.StencilReferenceNeedsBinding = true;
        }
    }

    public Vector4 BlendFactor
    {
        get => Recording.BlendFactor;
        set
        {
            var state = Recording;
            state.BlendFactor = value;
            state.BlendFactorNeedsBinding = true;
        }
    }

    internal D3D12Device Native => _device;
    internal bool UsesAgilitySdk => _device.UsesAgilitySdk;
    internal int FramesInFlight => _frameFenceValues.Length;
    internal int PendingReleaseCount => _device.DeferredReleases.PendingCount;
    internal int ShaderResourceDescriptorCount => _device.ShaderResourceHeap.AllocatedCount;
    internal int UploadRingPageCount => _uploadRing.PageCount;
    internal GraphicsRecordingState Recording => s_boundRecording is { } bound && bound.GraphicsDevice == this ? bound : _immediate;
    internal GraphicsRecordingState ImmediateRecording => _immediate;

    public event EventHandler<GraphicsDebugMessage>? DebugMessageReceived;

    public void SetRenderTarget(Texture2D? colorTarget, Texture2D? depthTarget = null)
    {
        if (colorTarget == null)
        {
            SetRenderTargets([], depthTarget, bindBackBuffer: true);
            return;
        }
        SetRenderTargets([colorTarget], depthTarget, bindBackBuffer: false);
    }

    public void SetRenderTargets(ReadOnlySpan<Texture2D> colorTargets, Texture2D? depthTarget = null) =>
        SetRenderTargets(colorTargets, depthTarget, bindBackBuffer: false);

    public void Clear(Color color) => Clear(color.ToVector4());

    public void Clear(Vector4 color)
    {
        EnsureInFrame();
        var state = Recording;
        state.Metrics = state.Metrics with { ClearCount = state.Metrics.ClearCount + 1 };
        var list = PrepareRenderTargets(state);
        if (state.IsBackBufferBound)
        {
            list->ClearRenderTargetView(_swapChain.CurrentRenderTargetView, (float*)&color, 0, null);
            return;
        }
        for (var index = 0; index < state.ColorTargetCount; index++)
        {
            list->ClearRenderTargetView(state.ColorTargets[index]!.RenderTargetView, (float*)&color, 0, null);
        }
    }

    public void ClearDepthStencil(float depth = 1f, byte stencil = 0)
    {
        EnsureInFrame();
        var state = Recording;
        if (state.DepthTarget is not { } target) throw new InvalidOperationException("No depth target is bound.");
        var list = PrepareRenderTargets(state);
        var flags = D3D12_CLEAR_FLAG_DEPTH | (target.FormatInfo.HasStencil ? D3D12_CLEAR_FLAG_STENCIL : 0);
        list->ClearDepthStencilView(target.DepthStencilView, flags, depth, stencil, 0, null);
    }

    public void CopyBuffer(GraphicsBuffer source, long sourceOffset, GraphicsBuffer destination, long destinationOffset, long size)
    {
        ArgumentNullException.ThrowIfNull(source);
        ArgumentNullException.ThrowIfNull(destination);
        if (source == destination) throw new ArgumentException("A buffer can't be copied onto itself.", nameof(destination));
        ArgumentOutOfRangeException.ThrowIfNegativeOrZero(size);
        if (sourceOffset < 0 || sourceOffset + size > source.Size) throw new ArgumentOutOfRangeException(nameof(sourceOffset));
        if (destinationOffset < 0 || destinationOffset + size > destination.Size) throw new ArgumentOutOfRangeException(nameof(destinationOffset));
        var context = RecordingFor("Copying between resources").Context;

        var list = context.Open();
        context.Use(source.Resource, D3D12_BARRIER_SYNC_COPY, D3D12_BARRIER_ACCESS_COPY_SOURCE, D3D12_BARRIER_LAYOUT_UNDEFINED);
        context.Use(destination.Resource, D3D12_BARRIER_SYNC_COPY, D3D12_BARRIER_ACCESS_COPY_DEST, D3D12_BARRIER_LAYOUT_UNDEFINED);
        list->CopyBufferRegion(destination.Resource.Pointer, (ulong)destinationOffset, source.Resource.Pointer, (ulong)sourceOffset, (ulong)size);
    }

    public void CopyTexture(Texture source, Texture destination)
    {
        ArgumentNullException.ThrowIfNull(source);
        ArgumentNullException.ThrowIfNull(destination);
        if (source == destination) throw new ArgumentException("A texture can't be copied onto itself.", nameof(destination));
        if (source.Width != destination.Width || source.Height != destination.Height || source.MipLevels != destination.MipLevels
            || source.ArrayLayers != destination.ArrayLayers || source.SampleCount != destination.SampleCount
            || source.FormatInfo.ResourceFormat != destination.FormatInfo.ResourceFormat)
            throw new ArgumentException("Both textures must have the same size, mip levels, array layers, sample count and format.", nameof(destination));
        var context = RecordingFor("Copying between resources").Context;

        var list = context.Open();
        context.Use(source.Resource, D3D12_BARRIER_SYNC_COPY, D3D12_BARRIER_ACCESS_COPY_SOURCE, D3D12_BARRIER_LAYOUT_COPY_SOURCE);
        context.Use(destination.Resource, D3D12_BARRIER_SYNC_COPY, D3D12_BARRIER_ACCESS_COPY_DEST, D3D12_BARRIER_LAYOUT_COPY_DEST);
        list->CopyResource(destination.Resource.Pointer, source.Resource.Pointer);
    }

    public void Resolve(Texture2D source, Texture2D destination)
    {
        ArgumentNullException.ThrowIfNull(source);
        ArgumentNullException.ThrowIfNull(destination);
        if (source.SampleCount == 1) throw new ArgumentException("The source must be multisampled.", nameof(source));
        if (destination.SampleCount != 1) throw new ArgumentException("The destination must not be multisampled.", nameof(destination));
        if (source.FormatInfo.IsDepth) throw new ArgumentException("Depth formats can't be resolved.", nameof(source));
        if (source.Format != destination.Format || source.Width != destination.Width || source.Height != destination.Height || source.ArrayLayers != destination.ArrayLayers)
            throw new ArgumentException("Both textures must have the same size, array layers and format.", nameof(destination));
        var context = RecordingFor("Resolving a texture").Context;

        var list = context.Open();
        context.Use(source.Resource, D3D12_BARRIER_SYNC_RESOLVE, D3D12_BARRIER_ACCESS_RESOLVE_SOURCE, D3D12_BARRIER_LAYOUT_RESOLVE_SOURCE);
        context.Use(destination.Resource, D3D12_BARRIER_SYNC_RESOLVE, D3D12_BARRIER_ACCESS_RESOLVE_DEST, D3D12_BARRIER_LAYOUT_RESOLVE_DEST);
        for (var layer = 0; layer < source.ArrayLayers; layer++)
        {
            list->ResolveSubresource(
                destination.Resource.Pointer, (uint)(layer * destination.MipLevels), source.Resource.Pointer, (uint)layer, source.FormatInfo.ViewFormat);
        }
    }

    public SamplerState GetSamplerState(in SamplerDescription description)
    {
        lock (_samplerStates)
        {
            if (_samplerStates.TryGetValue(description, out var existing)) return existing;
            var created = new SamplerState(this, description);
            _samplerStates.Add(description, created);
            return created;
        }
    }

    public GraphicsMetrics Metrics
    {
        get
        {
            var state = Recording;
            return state == _immediate ? state.Metrics.Plus(_executedMetrics) : state.Metrics;
        }
    }

    public void SetPipeline(GraphicsPipeline pipeline)
    {
        ArgumentNullException.ThrowIfNull(pipeline);
        if (pipeline.GraphicsDevice != this) throw new ArgumentException("The pipeline belongs to another GraphicsDevice.", nameof(pipeline));
        ObjectDisposedException.ThrowIf(pipeline.IsDisposed, pipeline);
        Recording.Pipeline = pipeline;
    }

    public void SetVertexBuffer(GraphicsBuffer? buffer, int slot = 0, long offsetInBytes = 0)
    {
        if (slot is < 0 or >= VertexLayout.MaximumSlots) throw new ArgumentOutOfRangeException(nameof(slot));
        if (buffer != null)
        {
            if ((buffer.Usage & BufferUsage.Vertex) == 0) throw new ArgumentException($"Buffer '{buffer.Name}' was not created with BufferUsage.Vertex.", nameof(buffer));
            if (offsetInBytes < 0 || offsetInBytes >= buffer.Size) throw new ArgumentOutOfRangeException(nameof(offsetInBytes));
        }
        var state = Recording;
        state.VertexBuffers[slot] = buffer;
        state.VertexBufferOffsets[slot] = offsetInBytes;
        state.TransientVertexViews[slot] = default;
        state.VertexBuffersNeedBinding = true;
    }

    public void SetIndexBuffer(GraphicsBuffer? buffer, long offsetInBytes = 0)
    {
        if (buffer != null)
        {
            if ((buffer.Usage & BufferUsage.Index) == 0) throw new ArgumentException($"Buffer '{buffer.Name}' was not created with BufferUsage.Index.", nameof(buffer));
            if (offsetInBytes < 0 || offsetInBytes >= buffer.Size || offsetInBytes % buffer.Stride != 0) throw new ArgumentOutOfRangeException(nameof(offsetInBytes));
        }
        var state = Recording;
        state.IndexBuffer = buffer;
        state.IndexBufferOffset = offsetInBytes;
        state.TransientIndexView = null;
        state.IndexBufferNeedsBinding = true;
    }

    public void SetVertexData<T>(ReadOnlySpan<T> vertices, int slot = 0) where T : unmanaged
    {
        if (slot is < 0 or >= VertexLayout.MaximumSlots) throw new ArgumentOutOfRangeException(nameof(slot));
        if (vertices.IsEmpty) throw new ArgumentException("Vertex data can't be empty.", nameof(vertices));
        EnsureInFrame();
        var bytes = MemoryMarshal.AsBytes(vertices);
        var state = Recording;
        var gpuAddress = UploadTransientData(state, bytes);
        state.VertexBuffers[slot] = null;
        state.VertexBufferOffsets[slot] = 0;
        state.TransientVertexViews[slot] = new D3D12_VERTEX_BUFFER_VIEW
        {
            BufferLocation = gpuAddress,
            SizeInBytes = (uint)bytes.Length,
            StrideInBytes = (uint)sizeof(T),
        };
        state.VertexBuffersNeedBinding = true;
    }

    public void SetIndexData(ReadOnlySpan<ushort> indices) => SetIndexData(MemoryMarshal.AsBytes(indices), DXGI_FORMAT.DXGI_FORMAT_R16_UINT);

    public void SetIndexData(ReadOnlySpan<uint> indices) => SetIndexData(MemoryMarshal.AsBytes(indices), DXGI_FORMAT.DXGI_FORMAT_R32_UINT);

    public void Draw(int vertexCount, int startVertex = 0) => DrawInstanced(vertexCount, 1, startVertex, 0);

    public void DrawInstanced(int vertexCountPerInstance, int instanceCount, int startVertex = 0, int startInstance = 0)
    {
        ArgumentOutOfRangeException.ThrowIfNegative(vertexCountPerInstance);
        ArgumentOutOfRangeException.ThrowIfNegative(instanceCount);
        ArgumentOutOfRangeException.ThrowIfNegative(startVertex);
        ArgumentOutOfRangeException.ThrowIfNegative(startInstance);
        var list = PrepareDraw(Recording, isIndexed: false);
        list->DrawInstanced((uint)vertexCountPerInstance, (uint)instanceCount, (uint)startVertex, (uint)startInstance);
    }

    public void DrawIndexed(int indexCount, int startIndex = 0, int baseVertex = 0) => DrawIndexedInstanced(indexCount, 1, startIndex, baseVertex, 0);

    public void DrawIndexedInstanced(int indexCountPerInstance, int instanceCount, int startIndex = 0, int baseVertex = 0, int startInstance = 0)
    {
        ArgumentOutOfRangeException.ThrowIfNegative(indexCountPerInstance);
        ArgumentOutOfRangeException.ThrowIfNegative(instanceCount);
        ArgumentOutOfRangeException.ThrowIfNegative(startIndex);
        ArgumentOutOfRangeException.ThrowIfNegative(startInstance);
        var list = PrepareDraw(Recording, isIndexed: true);
        list->DrawIndexedInstanced((uint)indexCountPerInstance, (uint)instanceCount, (uint)startIndex, baseVertex, (uint)startInstance);
    }

    internal void PostDebugLayerError(string text) =>
        (_device.DebugMessages ?? throw new InvalidOperationException("The debug layer is not enabled.")).PostApplicationError(text);

    internal void RequestBackBufferResize(int width, int height) => _pendingBackBufferSize = new Point(width, height);

    internal void Track(GraphicsResource resource)
    {
        lock (_liveResources) _liveResources.Add(resource);
    }

    internal void Untrack(GraphicsResource resource)
    {
        lock (_liveResources) _liveResources.Remove(resource);
    }

    internal void ReleaseResource(D3D12Resource resource)
    {
        _device.DeferredReleases.Release((IUnknown*)resource.Pointer, GetReleaseValues(resource));
    }

    internal void ReleaseDeviceObject(IUnknown* deviceObject) =>
        _device.DeferredReleases.Release(deviceObject, D3D12ReleaseValues.AwaitingGpuQueues);

    internal void FreeShaderResourceDescriptor(D3D12Resource? resource, uint index)
    {
        if (resource == null)
        {
            _device.DeferredReleases.Free(_device.ShaderResourceHeap, index, D3D12ReleaseValues.AwaitingGpuQueues);
            return;
        }
        _device.DeferredReleases.Free(_device.ShaderResourceHeap, index, GetReleaseValues(resource));
    }

    internal void FreeCpuDescriptor(D3D12CpuDescriptorHeap heap, D3D12CpuDescriptor descriptor, D3D12Resource resource)
    {
        _device.DeferredReleases.Free(heap, descriptor, GetReleaseValues(resource));
    }

    internal void UploadToBuffer(D3D12Resource destination, ulong offset, ReadOnlySpan<byte> data)
    {
        if (BoundRecording is { } bound)
        {
            var recordedMapped = bound.UploadRing.Allocate(data.Length, TransientDataAlignment, out _, out var recordedRingResource, out var recordedRingOffset);
            data.CopyTo(new Span<byte>(recordedMapped, data.Length));
            BeginRecordedUpload(bound, destination)->CopyBufferRegion(destination.Pointer, offset, recordedRingResource, recordedRingOffset, (ulong)data.Length);
            return;
        }

        if (CanUploadThroughRing(destination))
        {
            var mapped = _uploadRing.Allocate(data.Length, TransientDataAlignment, out _, out var ringResource, out var ringOffset);
            data.CopyTo(new Span<byte>(mapped, data.Length));
            var directList = BeginUpload(destination, out _);
            directList->CopyBufferRegion(destination.Pointer, offset, ringResource, ringOffset, (ulong)data.Length);
            return;
        }

        var list = BeginUpload(destination, out var copyList);
        if (copyList != null && _copyStagingPool.TryAllocate((ulong)data.Length, TransientDataAlignment, out var pooled))
        {
            data.CopyTo(new Span<byte>(pooled.CpuAddress, data.Length));
            list->CopyBufferRegion(destination.Pointer, offset, pooled.Resource, pooled.Offset, (ulong)data.Length);
            EndPooledUpload(destination, copyList, pooled);
            return;
        }
        var staging = _device.CreateStagingBuffer(D3D12_HEAP_TYPE_UPLOAD, (ulong)data.Length, "UploadBuffer");
        WriteStaging(staging, data, 0, 1, (ulong)data.Length, (ulong)data.Length);
        list->CopyBufferRegion(destination.Pointer, offset, staging, 0, (ulong)data.Length);
        EndUpload(destination, copyList, staging);
    }

    internal void UploadToTexture(D3D12Resource destination, in D3D12_RESOURCE_DESC1 description, uint subresource, ReadOnlySpan<byte> data) =>
        UploadToTexture(destination, description, subresource, subresource, data, 0, 0);

    internal void UploadToTexture(D3D12Resource destination, in D3D12_RESOURCE_DESC1 regionDescription, uint subresource, ReadOnlySpan<byte> data, int x, int y) =>
        UploadToTexture(destination, regionDescription, 0, subresource, data, x, y);

    private void UploadToTexture(
        D3D12Resource destination, in D3D12_RESOURCE_DESC1 footprintDescription, uint footprintSubresource, uint subresource, ReadOnlySpan<byte> data, int x, int y)
    {
        var footprint = _device.GetFootprint(footprintDescription, footprintSubresource, out var rowCount, out var rowSize, out var totalSize);
        if ((ulong)data.Length != rowCount * rowSize)
            throw new ArgumentException($"The data must be exactly {rowCount * rowSize} bytes for this subresource, but it is {data.Length} bytes.", nameof(data));

        if (BoundRecording is { } bound)
        {
            var recordedMapped = bound.UploadRing.Allocate((int)totalSize, (int)TerraFX.Interop.DirectX.D3D12.D3D12_TEXTURE_DATA_PLACEMENT_ALIGNMENT, out _, out var recordedRingResource, out var recordedRingOffset);
            CopyRows(data, recordedMapped + footprint.Offset, rowCount, rowSize, footprint.Footprint.RowPitch);
            footprint.Offset += recordedRingOffset;
            CopyToTexture(BeginRecordedUpload(bound, destination), destination, subresource, recordedRingResource, footprint, x, y);
            return;
        }

        if (CanUploadThroughRing(destination))
        {
            var mapped = _uploadRing.Allocate((int)totalSize, (int)TerraFX.Interop.DirectX.D3D12.D3D12_TEXTURE_DATA_PLACEMENT_ALIGNMENT, out _, out var ringResource, out var ringOffset);
            CopyRows(data, mapped + footprint.Offset, rowCount, rowSize, footprint.Footprint.RowPitch);
            footprint.Offset += ringOffset;
            var directList = BeginUpload(destination, out _);
            CopyToTexture(directList, destination, subresource, ringResource, footprint, x, y);
            return;
        }

        var list = BeginUpload(destination, out var copyList);
        if (copyList != null && _copyStagingPool.TryAllocate(totalSize, TerraFX.Interop.DirectX.D3D12.D3D12_TEXTURE_DATA_PLACEMENT_ALIGNMENT, out var pooled))
        {
            CopyRows(data, pooled.CpuAddress + footprint.Offset, rowCount, rowSize, footprint.Footprint.RowPitch);
            footprint.Offset += pooled.Offset;
            CopyToTexture(list, destination, subresource, pooled.Resource, footprint, x, y);
            EndPooledUpload(destination, copyList, pooled);
            return;
        }
        var staging = _device.CreateStagingBuffer(D3D12_HEAP_TYPE_UPLOAD, totalSize, "UploadBuffer");
        WriteStaging(staging, data, footprint.Offset, rowCount, rowSize, footprint.Footprint.RowPitch);
        CopyToTexture(list, destination, subresource, staging, footprint, x, y);
        EndUpload(destination, copyList, staging);
    }

    private bool CanUploadThroughRing(D3D12Resource destination)
    {
        if (!_isInFrame || Environment.CurrentManagedThreadId != _renderThreadId) return false;
        lock (destination.Lock) return destination.Owner != D3D12QueueKind.None;
    }

    private static void CopyToTexture(
        ID3D12GraphicsCommandList7* list, D3D12Resource destination, uint subresource, ID3D12Resource* source, D3D12_PLACED_SUBRESOURCE_FOOTPRINT footprint, int x, int y)
    {
        var destinationLocation = new D3D12_TEXTURE_COPY_LOCATION { pResource = destination.Pointer, Type = D3D12_TEXTURE_COPY_TYPE.D3D12_TEXTURE_COPY_TYPE_SUBRESOURCE_INDEX };
        destinationLocation.SubresourceIndex = subresource;
        var sourceLocation = new D3D12_TEXTURE_COPY_LOCATION { pResource = source, Type = D3D12_TEXTURE_COPY_TYPE.D3D12_TEXTURE_COPY_TYPE_PLACED_FOOTPRINT };
        sourceLocation.PlacedFootprint = footprint;
        list->CopyTextureRegion(&destinationLocation, (uint)x, (uint)y, 0, &sourceLocation, null);
    }

    internal GraphicsReadback BeginBufferReadback(D3D12Resource source, ulong offset, ulong size)
    {
        EnsureRenderThread("Reading GPU data back");
        var staging = _device.CreateStagingBuffer(D3D12_HEAP_TYPE_READBACK, size, "ReadbackBuffer");
        var list = OpenDirectList();
        _directContext.Use(source, D3D12_BARRIER_SYNC_COPY, D3D12_BARRIER_ACCESS_COPY_SOURCE, D3D12_BARRIER_LAYOUT_UNDEFINED);
        list->CopyBufferRegion(staging, 0, source.Pointer, offset, size);
        return AwaitSubmission(new GraphicsReadback(this, staging, 0, 1, size, size));
    }

    internal GraphicsReadback BeginTextureReadback(D3D12Resource source, in D3D12_RESOURCE_DESC1 description, uint subresource)
    {
        EnsureRenderThread("Reading GPU data back");
        var footprint = _device.GetFootprint(description, subresource, out var rowCount, out var rowSize, out var totalSize);
        var staging = _device.CreateStagingBuffer(D3D12_HEAP_TYPE_READBACK, totalSize, "ReadbackBuffer");
        var list = OpenDirectList();
        _directContext.Use(source, D3D12_BARRIER_SYNC_COPY, D3D12_BARRIER_ACCESS_COPY_SOURCE, D3D12_BARRIER_LAYOUT_COPY_SOURCE);
        var destinationLocation = new D3D12_TEXTURE_COPY_LOCATION { pResource = staging, Type = D3D12_TEXTURE_COPY_TYPE.D3D12_TEXTURE_COPY_TYPE_PLACED_FOOTPRINT };
        destinationLocation.PlacedFootprint = footprint;
        var sourceLocation = new D3D12_TEXTURE_COPY_LOCATION { pResource = source.Pointer, Type = D3D12_TEXTURE_COPY_TYPE.D3D12_TEXTURE_COPY_TYPE_SUBRESOURCE_INDEX };
        sourceLocation.SubresourceIndex = subresource;
        list->CopyTextureRegion(&destinationLocation, 0, 0, 0, &sourceLocation, null);
        return AwaitSubmission(new GraphicsReadback(this, staging, footprint.Offset, rowCount, rowSize, footprint.Footprint.RowPitch));
    }

    internal void Flush()
    {
        EnsureRenderThread("Flushing GPU work");
        if (_directContext.IsOpen) SubmitDirectList();
    }

    internal bool IsInFrame => _isInFrame;

    internal void BeginFrame()
    {
        if (_isInFrame) throw new InvalidOperationException("BeginFrame was called twice without EndFrame.");

        if (_pendingBackBufferSize is { } size)
        {
            _swapChain.Resize(size.X, size.Y);
            _pendingBackBufferSize = null;
        }

        _swapChain.WaitForFrameLatency();
        _device.DirectQueue.WaitForValue(_frameFenceValues[_frameIndex]);
        _device.ComputeQueue.WaitForValue(_frameComputeFenceValues[_frameIndex]);
        _device.ProcessDeferredReleases();
        _copyStagingPool.TrimCompletedPages(RetainedCopyStagingPageCount);
        _device.CopyCommandLists.TrimCompleted(RetainedCopyCommandListCount);

        _immediate.ColorTargetCount = 0;
        _immediate.DepthTarget = null;
        _immediate.IsBackBufferBound = true;
        _isInFrame = true;
        _immediate.RenderTargetsNeedBinding = true;
        Array.Clear(_immediate.TransientVertexViews);
        _immediate.TransientIndexView = null;
        _frameNumber++;
        _immediate.Metrics = default;
        _executedMetrics = default;
        SetTargetSize(_immediate, BackBufferWidth, BackBufferHeight, 1);
        Profiler.BeginFrame(_frameIndex, _frameNumber);
    }

    internal void EndFrame()
    {
        EnsureInFrame();
        ThrowIfOcclusionQueryActive();
        ThrowIfCommandListsRecording();
        if (_computeContext.IsOpen) SubmitComputeList();
        Profiler.EndFrame(OpenDirectList());
        _directContext.Use(_swapChain.CurrentBuffer, D3D12_BARRIER_SYNC_NONE, D3D12_BARRIER_ACCESS_NO_ACCESS, D3D12_BARRIER_LAYOUT_PRESENT);
        Array.Clear(_immediate.ColorTargets);
        _immediate.ColorTargetCount = 0;
        _immediate.DepthTarget = null;
        _isInFrame = false;
        SubmitDirectList();

        var presentResult = _swapChain.Present(VSync);
        _frameFenceValues[_frameIndex] = _device.DirectQueue.Signal();
        _frameComputeFenceValues[_frameIndex] = _device.ComputeQueue.LastSubmittedValue;
        _device.DeferredReleases.AssignComputeSubmission(_frameComputeFenceValues[_frameIndex]);
        _uploadRing.Retire(_frameFenceValues[_frameIndex], _frameComputeFenceValues[_frameIndex]);
        RetireCommandListUploads(_frameFenceValues[_frameIndex], _frameComputeFenceValues[_frameIndex]);
        _frameIndex = (_frameIndex + 1) % _frameFenceValues.Length;

        _device.ThrowIfPresentFailed(presentResult);
        DispatchDebugMessages();
    }

    public void Dispose()
    {
        if (_isDisposed) return;
        _isDisposed = true;

        GraphicsResource[] remaining;
        lock (_liveResources) remaining = [.. _liveResources];
        foreach (var resource in remaining) resource.Dispose();

        _device.WaitForIdle();
        DisposeCommandLists();
        _device.DeferredReleases.ProcessAll();
        Profiler?.Dispose();
        _uploadRing?.Dispose();
        _copyStagingPool?.Dispose();
        _computeContext?.Dispose();
        _directContext?.Dispose();
        _handoffContext?.Dispose();
        _swapChain?.Dispose();
        DispatchDebugMessages(throwOnErrors: false);
        _device.Dispose();
    }

    private void SetRenderTargets(ReadOnlySpan<Texture2D> colorTargets, Texture2D? depthTarget, bool bindBackBuffer)
    {
        EnsureInFrame();
        if (colorTargets.Length > MaximumRenderTargets) throw new ArgumentException($"At most {MaximumRenderTargets} color targets can be bound.", nameof(colorTargets));

        var width = bindBackBuffer ? BackBufferWidth : -1;
        var height = bindBackBuffer ? BackBufferHeight : -1;
        var sampleCount = bindBackBuffer ? 1 : -1;
        foreach (var target in colorTargets)
        {
            ArgumentNullException.ThrowIfNull(target, nameof(colorTargets));
            if ((target.Usage & TextureUsage.RenderTarget) == 0) throw new ArgumentException($"Texture '{target.Name}' was not created with TextureUsage.RenderTarget.", nameof(colorTargets));
            CheckTargetSize(target, ref width, ref height, ref sampleCount);
        }
        if (depthTarget != null)
        {
            if ((depthTarget.Usage & TextureUsage.DepthStencil) == 0) throw new ArgumentException($"Texture '{depthTarget.Name}' was not created with TextureUsage.DepthStencil.", nameof(depthTarget));
            CheckTargetSize(depthTarget, ref width, ref height, ref sampleCount);
        }

        var state = Recording;
        Array.Clear(state.ColorTargets);
        for (var index = 0; index < colorTargets.Length; index++) state.ColorTargets[index] = colorTargets[index];
        state.ColorTargetCount = colorTargets.Length;
        state.DepthTarget = depthTarget;
        state.IsBackBufferBound = bindBackBuffer;
        state.RenderTargetsNeedBinding = true;
        SetTargetSize(state, width, height, sampleCount);
    }

    private static void SetTargetSize(GraphicsRecordingState state, int width, int height, int sampleCount)
    {
        state.TargetWidth = Math.Max(width, 0);
        state.TargetHeight = Math.Max(height, 0);
        state.TargetSampleCount = Math.Max(sampleCount, 1);
        state.TargetsVersion++;
        state.Viewport = new Viewport(0, 0, state.TargetWidth, state.TargetHeight);
        state.ScissorRectangle = new Rectangle(0, 0, state.TargetWidth, state.TargetHeight);
        state.ViewportNeedsBinding = true;
        state.ScissorRectangleNeedsBinding = true;
    }

    private ID3D12GraphicsCommandList7* PrepareDraw(GraphicsRecordingState state, bool isIndexed)
    {
        EnsureInFrame();
        state.Metrics = state.Metrics with { DrawCount = state.Metrics.DrawCount + 1 };
        var pipeline = state.Pipeline ?? throw new InvalidOperationException("No pipeline is set. Call SetPipeline before drawing.");
        ObjectDisposedException.ThrowIf(pipeline.IsDisposed, pipeline);
        var program = pipeline.Program;
        var rootSignature = program.RootSignature;
        if (state.ValidatedPipeline != pipeline || state.ValidatedTargetsVersion != state.TargetsVersion)
        {
            ValidatePipelineTargets(state, pipeline);
            state.ValidatedPipeline = pipeline;
            state.ValidatedTargetsVersion = state.TargetsVersion;
        }

        var list = PrepareRenderTargets(state);
        var pipelineState = pipeline.PipelineState;
        if (state.Context.AppliedPipelineState != pipelineState)
        {
            list->SetPipelineState(pipelineState);
            state.Metrics = state.Metrics with { PipelineStateChangeCount = state.Metrics.PipelineStateChangeCount + 1 };
            state.Context.AppliedPipelineState = pipelineState;
        }
        if (state.Context.AppliedGraphicsRootSignature != rootSignature)
        {
            list->SetGraphicsRootSignature(rootSignature);
            state.Context.AppliedGraphicsRootSignature = rootSignature;
            Array.Clear(state.Context.AppliedGraphicsRootArguments);
            state.Metrics = state.Metrics with { RootSignatureChangeCount = state.Metrics.RootSignatureChangeCount + 1 };
        }
        if (state.AppliedTopology != pipeline.Topology)
        {
            list->IASetPrimitiveTopology(pipeline.Topology);
            state.AppliedTopology = pipeline.Topology;
        }

        BindVertexBuffers(state, list, pipeline.RequiredVertexSlots);
        if (isIndexed) BindIndexBuffer(state, list);
        BindConstantBuffers(state, state.Context, program, isCompute: false);
        BindResources(state, state.Context, program, isCompute: false);
        BindFixedFunctionState(state, list);
        return list;
    }

    private void ValidatePipelineTargets(GraphicsRecordingState state, GraphicsPipeline pipeline)
    {
        var formats = pipeline.RenderTargetFormats;
        var colorTargetCount = state.IsBackBufferBound ? 1 : state.ColorTargetCount;
        var matches = formats.Length == colorTargetCount && pipeline.SampleCount == state.TargetSampleCount && pipeline.DepthStencilFormat == state.DepthTarget?.Format;
        for (var index = 0; matches && index < colorTargetCount; index++)
            matches = formats[index] == (state.IsBackBufferBound ? BackBufferFormat : state.ColorTargets[index]!.Format);
        if (matches) return;

        var bound = new List<string>();
        if (state.IsBackBufferBound) bound.Add($"{BackBufferFormat} (back buffer)");
        for (var index = 0; index < state.ColorTargetCount; index++) bound.Add(state.ColorTargets[index]!.Format.ToString());
        throw new InvalidOperationException(
            $"Pipeline '{pipeline.Name ?? pipeline.Program.Name}' writes [{string.Join(", ", pipeline.RenderTargetFormats.ToArray())}] with depth {pipeline.DepthStencilFormat?.ToString() ?? "none"} "
            + $"and {pipeline.SampleCount} samples, but the bound targets are [{string.Join(", ", bound)}] with depth {state.DepthTarget?.Format.ToString() ?? "none"} and {state.TargetSampleCount} samples.");
    }

    private static void BindVertexBuffers(GraphicsRecordingState state, ID3D12GraphicsCommandList7* list, int requiredSlots)
    {
        for (var slot = 0; slot < VertexLayout.MaximumSlots; slot++)
        {
            if ((requiredSlots & (1 << slot)) == 0) continue;
            if (state.VertexBuffers[slot] is not { } buffer)
            {
                if (state.TransientVertexViews[slot].BufferLocation == 0) throw new InvalidOperationException($"The pipeline reads vertex slot {slot}, but no vertex buffer or vertex data is set there.");
                continue;
            }
            ObjectDisposedException.ThrowIf(buffer.IsDisposed, buffer);
            state.Context.Use(buffer.Resource, D3D12_BARRIER_SYNC_VERTEX_SHADING, D3D12_BARRIER_ACCESS_VERTEX_BUFFER, D3D12_BARRIER_LAYOUT_UNDEFINED);
        }
        if (!state.VertexBuffersNeedBinding) return;

        var views = stackalloc D3D12_VERTEX_BUFFER_VIEW[VertexLayout.MaximumSlots];
        var count = 0;
        for (var slot = 0; slot < VertexLayout.MaximumSlots; slot++)
        {
            if (state.VertexBuffers[slot] is not { } buffer)
            {
                views[slot] = state.TransientVertexViews[slot];
                if (views[slot].BufferLocation != 0) count = slot + 1;
                continue;
            }
            var offset = state.VertexBufferOffsets[slot];
            views[slot] = new D3D12_VERTEX_BUFFER_VIEW
            {
                BufferLocation = buffer.GpuAddress + (ulong)offset,
                SizeInBytes = (uint)(buffer.Size - offset),
                StrideInBytes = (uint)buffer.Stride,
            };
            count = slot + 1;
        }
        if (count > 0)
        {
            list->IASetVertexBuffers(0, (uint)count, views);
            state.Metrics = state.Metrics with { VertexBufferBindCount = state.Metrics.VertexBufferBindCount + 1 };
        }
        state.VertexBuffersNeedBinding = false;
    }

    private void SetIndexData(ReadOnlySpan<byte> indices, DXGI_FORMAT format)
    {
        if (indices.IsEmpty) throw new ArgumentException("Index data can't be empty.", nameof(indices));
        EnsureInFrame();
        var state = Recording;
        var gpuAddress = UploadTransientData(state, indices);
        state.IndexBuffer = null;
        state.IndexBufferOffset = 0;
        state.TransientIndexView = new D3D12_INDEX_BUFFER_VIEW { BufferLocation = gpuAddress, SizeInBytes = (uint)indices.Length, Format = format };
        state.IndexBufferNeedsBinding = true;
    }

    private static ulong UploadTransientData(GraphicsRecordingState state, ReadOnlySpan<byte> data)
    {
        var destination = state.UploadRing.Allocate(data.Length, TransientDataAlignment, out var gpuAddress);
        data.CopyTo(new Span<byte>(destination, data.Length));
        return gpuAddress;
    }

    private static void BindIndexBuffer(GraphicsRecordingState state, ID3D12GraphicsCommandList7* list)
    {
        if (state.TransientIndexView is { } transientView)
        {
            if (state.IndexBufferNeedsBinding)
            {
                list->IASetIndexBuffer(&transientView);
                state.Metrics = state.Metrics with { IndexBufferBindCount = state.Metrics.IndexBufferBindCount + 1 };
            }
            state.IndexBufferNeedsBinding = false;
            return;
        }
        var buffer = state.IndexBuffer ?? throw new InvalidOperationException("Indexed draws need an index buffer. Call SetIndexBuffer or SetIndexData first.");
        ObjectDisposedException.ThrowIf(buffer.IsDisposed, buffer);
        state.Context.Use(buffer.Resource, D3D12_BARRIER_SYNC_INDEX_INPUT, D3D12_BARRIER_ACCESS_INDEX_BUFFER, D3D12_BARRIER_LAYOUT_UNDEFINED);
        if (!state.IndexBufferNeedsBinding) return;
        var view = new D3D12_INDEX_BUFFER_VIEW
        {
            BufferLocation = buffer.GpuAddress + (ulong)state.IndexBufferOffset,
            SizeInBytes = (uint)(buffer.Size - state.IndexBufferOffset),
            Format = buffer.Stride == 2 ? DXGI_FORMAT.DXGI_FORMAT_R16_UINT : DXGI_FORMAT.DXGI_FORMAT_R32_UINT,
        };
        list->IASetIndexBuffer(&view);
        state.Metrics = state.Metrics with { IndexBufferBindCount = state.Metrics.IndexBufferBindCount + 1 };
        state.IndexBufferNeedsBinding = false;
    }

    private void BindConstantBuffers(GraphicsRecordingState state, D3D12CommandContext context, ShaderProgram program, bool isCompute)
    {
        var constantBuffers = program.ValuesFor(state).ConstantBuffers;
        for (var index = 0; index < constantBuffers.Length; index++)
        {
            var constantBuffer = constantBuffers[index];
            var sync = program.ConstantBufferSlots[index].Sync;
            ulong address;
            if (constantBuffer.Buffer is { } buffer)
            {
                ObjectDisposedException.ThrowIf(buffer.IsDisposed, buffer);
                context.Use(buffer.Resource, sync, D3D12_BARRIER_ACCESS_CONSTANT_BUFFER, D3D12_BARRIER_LAYOUT_UNDEFINED);
                address = buffer.GpuAddress + (ulong)constantBuffer.BufferOffset;
            }
            else
            {
                if (constantBuffer.IsDirty || constantBuffer.UploadedFrame != _frameNumber)
                {
                    var data = constantBuffer.Data;
                    var destination = state.UploadRing.Allocate(data.Length, ConstantBufferAlignment, out var uploadedAddress);
                    data.AsSpan().CopyTo(new Span<byte>(destination, data.Length));
                    constantBuffer.UploadedAddress = uploadedAddress;
                    constantBuffer.UploadedFrame = _frameNumber;
                    constantBuffer.IsDirty = false;
                }
                address = constantBuffer.UploadedAddress;
            }
            BindConstantBufferAddress(state, context, isCompute, (uint)program.ConstantBufferRootParameters[index], address);
            if (program.ConstantBufferPixelRootParameters[index] >= 0)
                BindConstantBufferAddress(state, context, isCompute, (uint)program.ConstantBufferPixelRootParameters[index], address);
        }
    }

    private static void BindConstantBufferAddress(GraphicsRecordingState state, D3D12CommandContext context, bool isCompute, uint rootParameter, ulong address)
    {
        CountRootArgument(state, context, isCompute, rootParameter, address, isDescriptorTable: false);
        if (isCompute) context.List->SetComputeRootConstantBufferView(rootParameter, address);
        else context.List->SetGraphicsRootConstantBufferView(rootParameter, address);
    }

    private void BindResources(GraphicsRecordingState state, D3D12CommandContext context, ShaderProgram program, bool isCompute)
    {
        var list = context.List;
        var resources = program.Info.Resources;
        var resourceValues = program.ValuesFor(state).Resources;
        var usesDescriptorIndices = program.UsesDescriptorIndices;
        var firstStageSlots = stackalloc uint[D3D12RootSignatures.ComputeDescriptorSlots];
        var pixelSlots = stackalloc uint[D3D12RootSignatures.PixelDescriptorSlots];
        var rootParameter = (uint)program.ConstantBufferSlots.Length;
        for (var index = 0; index < resources.Length; index++, rootParameter++)
        {
            var info = resources[index];
            var sync = program.ResourceSyncs[index];
            var isReadWrite = info.Kind is D3D12ShaderResourceKind.ReadWriteTexture or D3D12ShaderResourceKind.ReadWriteStructuredBuffer
                or D3D12ShaderResourceKind.ReadWriteByteAddressBuffer;
            var access = isReadWrite ? D3D12_BARRIER_ACCESS_UNORDERED_ACCESS : D3D12_BARRIER_ACCESS_SHADER_RESOURCE;
            uint descriptorIndex;
            var isSampler = false;
            switch (resourceValues[index])
            {
                case Texture texture:
                    ObjectDisposedException.ThrowIf(texture.IsDisposed, texture);
                    if (!isCompute && IsBoundAsTarget(state, texture))
                        throw new InvalidOperationException($"Texture '{texture.Name}' is bound to parameter '{info.Name}' and as a render or depth target at the same time.");
                    context.Use(texture.Resource, sync, access, isReadWrite ? D3D12_BARRIER_LAYOUT_UNORDERED_ACCESS : D3D12_BARRIER_LAYOUT_SHADER_RESOURCE);
                    descriptorIndex = isReadWrite ? texture.UnorderedAccessViewIndex : texture.ShaderResourceViewIndex;
                    break;
                case TextureView view:
                    ObjectDisposedException.ThrowIf(view.Texture.IsDisposed, view.Texture);
                    if (!isCompute && IsBoundAsTarget(state, view.Texture))
                        throw new InvalidOperationException($"Texture '{view.Texture.Name}' is bound to parameter '{info.Name}' and as a render or depth target at the same time.");
                    context.Use(view.Texture.Resource, sync, access, isReadWrite ? D3D12_BARRIER_LAYOUT_UNORDERED_ACCESS : D3D12_BARRIER_LAYOUT_SHADER_RESOURCE);
                    descriptorIndex = isReadWrite ? view.UnorderedAccessViewIndex : view.ShaderResourceViewIndex;
                    break;
                case GraphicsBuffer buffer:
                    ObjectDisposedException.ThrowIf(buffer.IsDisposed, buffer);
                    context.Use(buffer.Resource, sync, access, D3D12_BARRIER_LAYOUT_UNDEFINED);
                    descriptorIndex = isReadWrite ? buffer.UnorderedAccessViewIndex : buffer.ShaderResourceViewIndex;
                    break;
                case SamplerState sampler:
                    descriptorIndex = sampler.DescriptorIndex;
                    isSampler = true;
                    break;
                default:
                    throw new InvalidOperationException($"Parameter '{info.Name}' of program '{program.Name}' has no value.");
            }

            if (usesDescriptorIndices)
            {
                if (isCompute) firstStageSlots[info.ComputeDescriptorSlot] = descriptorIndex;
                else
                {
                    if (info.VertexDescriptorSlot >= 0) firstStageSlots[info.VertexDescriptorSlot] = descriptorIndex;
                    if (info.PixelDescriptorSlot >= 0) pixelSlots[info.PixelDescriptorSlot] = descriptorIndex;
                }
                continue;
            }

            var handle = isSampler ? _device.SamplerHeap.GpuHandle(descriptorIndex) : _device.ShaderResourceHeap.GpuHandle(descriptorIndex);
            CountRootArgument(state, context, isCompute, rootParameter, handle.ptr, isDescriptorTable: true);
            if (isCompute) list->SetComputeRootDescriptorTable(rootParameter, handle);
            else list->SetGraphicsRootDescriptorTable(rootParameter, handle);
        }
        if (!usesDescriptorIndices) return;

        if (isCompute)
        {
            BindDescriptorIndices(state, list, isCompute, D3D12RootSignatures.ComputeDescriptorIndexParameter, firstStageSlots, program.ComputeDescriptorSlotCount);
            return;
        }
        BindDescriptorIndices(state, list, isCompute, D3D12RootSignatures.GraphicsVertexDescriptorIndexParameter, firstStageSlots, program.VertexDescriptorSlotCount);
        BindDescriptorIndices(state, list, isCompute, D3D12RootSignatures.GraphicsPixelDescriptorIndexParameter, pixelSlots, program.PixelDescriptorSlotCount);
    }

    private static void BindDescriptorIndices(GraphicsRecordingState state, ID3D12GraphicsCommandList7* list, bool isCompute, uint rootParameter, uint* slots, int slotCount)
    {
        if (slotCount == 0) return;
        state.Metrics = state.Metrics with { DescriptorIndexBindCount = state.Metrics.DescriptorIndexBindCount + 1 };
        if (isCompute) list->SetComputeRoot32BitConstants(rootParameter, (uint)slotCount, slots, 0);
        else list->SetGraphicsRoot32BitConstants(rootParameter, (uint)slotCount, slots, 0);
    }

    private static void CountRootArgument(GraphicsRecordingState state, D3D12CommandContext context, bool isCompute, uint rootParameter, ulong argument, bool isDescriptorTable)
    {
        var appliedArguments = isCompute ? context.AppliedComputeRootArguments : context.AppliedGraphicsRootArguments;
        var isRepeated = appliedArguments[rootParameter] == argument;
        appliedArguments[rootParameter] = argument;
        state.Metrics = isDescriptorTable
            ? state.Metrics with
            {
                DescriptorTableBindCount = state.Metrics.DescriptorTableBindCount + 1,
                RepeatedDescriptorTableBindCount = state.Metrics.RepeatedDescriptorTableBindCount + (isRepeated ? 1 : 0),
            }
            : state.Metrics with
            {
                ConstantBufferBindCount = state.Metrics.ConstantBufferBindCount + 1,
                RepeatedConstantBufferBindCount = state.Metrics.RepeatedConstantBufferBindCount + (isRepeated ? 1 : 0),
            };
    }

    private static void BindFixedFunctionState(GraphicsRecordingState state, ID3D12GraphicsCommandList7* list)
    {
        if (state.ViewportNeedsBinding)
        {
            var viewport = new D3D12_VIEWPORT
            {
                TopLeftX = state.Viewport.X,
                TopLeftY = state.Viewport.Y,
                Width = state.Viewport.Width,
                Height = state.Viewport.Height,
                MinDepth = state.Viewport.MinDepth,
                MaxDepth = state.Viewport.MaxDepth,
            };
            list->RSSetViewports(1, &viewport);
            state.ViewportNeedsBinding = false;
        }
        if (state.ScissorRectangleNeedsBinding)
        {
            var rectangle = new RECT { left = state.ScissorRectangle.Left, top = state.ScissorRectangle.Top, right = state.ScissorRectangle.Right, bottom = state.ScissorRectangle.Bottom };
            list->RSSetScissorRects(1, &rectangle);
            state.ScissorRectangleNeedsBinding = false;
        }
        if (state.StencilReferenceNeedsBinding)
        {
            list->OMSetStencilRef((uint)state.StencilReference);
            state.StencilReferenceNeedsBinding = false;
        }
        if (state.BlendFactorNeedsBinding)
        {
            var blendFactor = state.BlendFactor;
            list->OMSetBlendFactor((float*)&blendFactor);
            state.BlendFactorNeedsBinding = false;
        }
    }

    private static bool IsBoundAsTarget(GraphicsRecordingState state, Texture texture)
    {
        if (texture == state.DepthTarget) return true;
        for (var index = 0; index < state.ColorTargetCount; index++)
            if (texture == state.ColorTargets[index]) return true;
        return false;
    }

    private static void CheckTargetSize(Texture2D target, ref int width, ref int height, ref int sampleCount)
    {
        if (width < 0)
        {
            width = target.Width;
            height = target.Height;
            sampleCount = target.SampleCount;
            return;
        }
        if (target.Width != width || target.Height != height || target.SampleCount != sampleCount)
            throw new ArgumentException("All bound targets must have the same size and sample count.");
    }

    private ID3D12GraphicsCommandList7* PrepareRenderTargets(GraphicsRecordingState state)
    {
        var list = state.Context.Open();
        var handles = stackalloc D3D12_CPU_DESCRIPTOR_HANDLE[MaximumRenderTargets];
        var count = 0;
        if (state.IsBackBufferBound)
        {
            UseAsRenderTarget(state.Context, _swapChain.CurrentBuffer);
            handles[count++] = _swapChain.CurrentRenderTargetView;
        }
        for (var index = 0; index < state.ColorTargetCount; index++)
        {
            var target = state.ColorTargets[index]!;
            UseAsRenderTarget(state.Context, target.Resource);
            handles[count++] = target.RenderTargetView;
        }

        var depthStencilView = default(D3D12_CPU_DESCRIPTOR_HANDLE);
        if (state.DepthTarget is { } depthTarget)
        {
            UseAsDepthTarget(state.Context, depthTarget.Resource);
            depthStencilView = depthTarget.DepthStencilView;
        }

        if (state.RenderTargetsNeedBinding)
        {
            list->OMSetRenderTargets((uint)count, handles, FALSE, state.DepthTarget != null ? &depthStencilView : null);
            state.RenderTargetsNeedBinding = false;
        }
        return list;
    }

    private static void UseAsRenderTarget(D3D12CommandContext context, D3D12Resource resource) =>
        context.Use(resource, D3D12_BARRIER_SYNC_RENDER_TARGET, D3D12_BARRIER_ACCESS_RENDER_TARGET, D3D12_BARRIER_LAYOUT_RENDER_TARGET);

    private static void UseAsDepthTarget(D3D12CommandContext context, D3D12Resource resource) =>
        context.Use(resource, D3D12_BARRIER_SYNC_DEPTH_STENCIL, D3D12_BARRIER_ACCESS_DEPTH_STENCIL_WRITE, D3D12_BARRIER_LAYOUT_DEPTH_STENCIL_WRITE);

    private ID3D12GraphicsCommandList7* OpenDirectList() => _directContext.Open();

    private void OnDirectListOpened() => _immediate.MarkAllForBinding();

    private ulong SubmitDirectList(bool assignsReleases = true) => SubmitDirectList([], assignsReleases);

    private ulong SubmitDirectList(ReadOnlySpan<nint> followingCommandLists, bool assignsReleases = true)
    {
        ThrowIfOcclusionQueryActive();
        var value = _directContext.Submit(followingCommandLists);
        AssignOcclusionQuerySubmission(value);
        if (assignsReleases) _device.DeferredReleases.AssignDirectSubmission(value);
        foreach (var readback in _readbacksAwaitingSubmission) readback.FenceValue = value;
        _readbacksAwaitingSubmission.Clear();
        return value;
    }

    private GraphicsReadback AwaitSubmission(GraphicsReadback readback)
    {
        _readbacksAwaitingSubmission.Add(readback);
        return readback;
    }

    private GraphicsRecordingState? BoundRecording => s_boundRecording is { } bound && bound.GraphicsDevice == this ? bound : null;

    private static ID3D12GraphicsCommandList7* BeginRecordedUpload(GraphicsRecordingState bound, D3D12Resource destination)
    {
        var list = bound.Context.Open();
        bound.Context.Use(destination, D3D12_BARRIER_SYNC_COPY, D3D12_BARRIER_ACCESS_COPY_DEST, destination.IsTexture ? D3D12_BARRIER_LAYOUT_COPY_DEST : D3D12_BARRIER_LAYOUT_UNDEFINED);
        return list;
    }

    private ID3D12GraphicsCommandList7* BeginUpload(D3D12Resource destination, out D3D12CommandList? copyList)
    {
        destination.Lock.Enter();
        if (destination.Owner == D3D12QueueKind.None)
        {
            copyList = _device.CopyCommandLists.Rent();
            return copyList.List;
        }
        destination.Lock.Exit();

        copyList = null;
        EnsureRenderThread("Updating a resource after the GPU used it");
        var list = OpenDirectList();
        var layout = destination.IsTexture ? D3D12_BARRIER_LAYOUT_COPY_DEST : D3D12_BARRIER_LAYOUT_UNDEFINED;
        _directContext.Use(destination, D3D12_BARRIER_SYNC_COPY, D3D12_BARRIER_ACCESS_COPY_DEST, layout);
        return list;
    }

    private void EndPooledUpload(D3D12Resource destination, D3D12CommandList copyList, in D3D12CopyStagingAllocation pooled)
    {
        var value = 0UL;
        try
        {
            copyList.Close();
            value = _device.CopyQueue.Submit((ID3D12CommandList*)copyList.List);
            _device.CopyCommandLists.Return(copyList, value);
            destination.PendingCopyValue = Math.Max(destination.PendingCopyValue, value);
        }
        finally
        {
            _copyStagingPool.Submitted(pooled, value);
            destination.Lock.Exit();
        }
        _device.ProcessDeferredReleases();
    }

    private void EndUpload(D3D12Resource destination, D3D12CommandList? copyList, ID3D12Resource* staging)
    {
        if (copyList == null)
        {
            _device.DeferredReleases.Release((IUnknown*)staging, new D3D12ReleaseValues(D3D12ReleaseValues.AwaitingSubmission, 0, 0));
            return;
        }

        try
        {
            copyList.Close();
            var value = _device.CopyQueue.Submit((ID3D12CommandList*)copyList.List);
            _device.CopyCommandLists.Return(copyList, value);
            destination.PendingCopyValue = Math.Max(destination.PendingCopyValue, value);
            _device.DeferredReleases.Release((IUnknown*)staging, new D3D12ReleaseValues(0, 0, value));
        }
        finally
        {
            destination.Lock.Exit();
        }
        _device.ProcessDeferredReleases();
    }

    private static void WriteStaging(ID3D12Resource* staging, ReadOnlySpan<byte> data, ulong offset, uint rowCount, ulong rowSize, ulong rowPitch)
    {
        var emptyRange = new D3D12_RANGE();
        byte* mapped;
        ThrowOnFailure(staging->Map(0, &emptyRange, (void**)&mapped), "ID3D12Resource::Map");
        CopyRows(data, mapped + offset, rowCount, rowSize, rowPitch);
        staging->Unmap(0, null);
    }

    private static void CopyRows(ReadOnlySpan<byte> data, byte* destination, uint rowCount, ulong rowSize, ulong rowPitch)
    {
        for (var row = 0u; row < rowCount; row++)
            data.Slice((int)(row * rowSize), (int)rowSize).CopyTo(new Span<byte>(destination + row * rowPitch, (int)rowSize));
    }

    private static D3D12ReleaseValues GetReleaseValues(D3D12Resource resource)
    {
        lock (resource.Lock)
        {
            return new D3D12ReleaseValues(
                (resource.UsedQueues & D3D12QueueKind.Direct) != 0 ? D3D12ReleaseValues.AwaitingSubmission : 0,
                (resource.UsedQueues & D3D12QueueKind.Compute) != 0 ? D3D12ReleaseValues.AwaitingSubmission : 0,
                resource.PendingCopyValue);
        }
    }

    private void EnsureInFrame()
    {
        if (!_isInFrame) throw new InvalidOperationException("Drawing is only allowed inside Game.Draw.");
    }

    private GraphicsRecordingState RecordingFor(string operation)
    {
        if (s_boundRecording is { } bound && bound.GraphicsDevice == this) return bound;
        EnsureRenderThread(operation);
        return _immediate;
    }

    private void EnsureRenderThread(string operation)
    {
        if (Environment.CurrentManagedThreadId != _renderThreadId)
            throw new InvalidOperationException($"{operation} is only allowed on the thread that created the GraphicsDevice.");
    }

    private void DispatchDebugMessages(bool throwOnErrors = true)
    {
        if (_device.DebugMessages is not { } source) return;
        _debugMessages.Clear();
        source.Drain(_debugMessages);
        if (_debugMessages.Count == 0) return;

        List<GraphicsDebugMessage>? errors = null;
        foreach (var message in _debugMessages)
        {
            if (DebugMessageReceived is { } handler) handler(this, message);
            else Console.Error.WriteLine(message);
            if (message.IsError) (errors ??= []).Add(message);
        }

        if (throwOnErrors && errors != null && Options.ThrowOnDebugLayerErrors) throw new GraphicsDebugException(errors);
    }
}
