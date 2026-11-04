using System.Numerics;
using Crystal.Graphics.D3D12;
using TerraFX.Interop.DirectX;

namespace Crystal.Graphics;

internal sealed unsafe class GraphicsRecordingState(GraphicsDevice graphicsDevice, D3D12CommandContext context, D3D12UploadRing uploadRing)
{
    public readonly GraphicsDevice GraphicsDevice = graphicsDevice;
    public readonly D3D12CommandContext Context = context;
    public const long ImmediateValueGeneration = 0;

    public readonly D3D12UploadRing UploadRing = uploadRing;
    public int ValueSlot;
    public long ValueGeneration = ImmediateValueGeneration;
    public readonly int[] OpenProfilerScopes = new int[GraphicsProfiler.MaximumDepth];
    public int OpenProfilerScopeCount;
    public readonly Texture2D?[] ColorTargets = new Texture2D?[GraphicsDevice.MaximumRenderTargets];
    public readonly GraphicsBuffer?[] VertexBuffers = new GraphicsBuffer?[VertexLayout.MaximumSlots];
    public readonly long[] VertexBufferOffsets = new long[VertexLayout.MaximumSlots];
    public readonly D3D12_VERTEX_BUFFER_VIEW[] TransientVertexViews = new D3D12_VERTEX_BUFFER_VIEW[VertexLayout.MaximumSlots];
    public int ColorTargetCount;
    public bool IsBackBufferBound;
    public bool RenderTargetsNeedBinding;
    public Texture2D? DepthTarget;
    public D3D12_INDEX_BUFFER_VIEW? TransientIndexView;
    public GraphicsPipeline? Pipeline;
    public D3D_PRIMITIVE_TOPOLOGY AppliedTopology;
    public GraphicsPipeline? ValidatedPipeline;
    public int ValidatedTargetsVersion = -1;
    public int TargetsVersion;
    public int TargetWidth;
    public int TargetHeight;
    public int TargetSampleCount = 1;
    public bool VertexBuffersNeedBinding;
    public GraphicsBuffer? IndexBuffer;
    public long IndexBufferOffset;
    public bool IndexBufferNeedsBinding;
    public Viewport Viewport;
    public Rectangle ScissorRectangle;
    public bool ViewportNeedsBinding;
    public bool ScissorRectangleNeedsBinding;
    public int StencilReference;
    public bool StencilReferenceNeedsBinding;
    public Vector4 BlendFactor = Vector4.One;
    public bool BlendFactorNeedsBinding;
    public GraphicsMetrics Metrics;

    public void MarkAllForBinding()
    {
        RenderTargetsNeedBinding = true;
        AppliedTopology = D3D_PRIMITIVE_TOPOLOGY.D3D_PRIMITIVE_TOPOLOGY_UNDEFINED;
        VertexBuffersNeedBinding = true;
        IndexBufferNeedsBinding = true;
        ViewportNeedsBinding = true;
        ScissorRectangleNeedsBinding = true;
        StencilReferenceNeedsBinding = true;
        BlendFactorNeedsBinding = true;
    }

    public void ClearBindings()
    {
        Array.Clear(ColorTargets);
        ColorTargetCount = 0;
        DepthTarget = null;
        IsBackBufferBound = false;
        Array.Clear(VertexBuffers);
        Array.Clear(VertexBufferOffsets);
        Array.Clear(TransientVertexViews);
        TransientIndexView = null;
        IndexBuffer = null;
        IndexBufferOffset = 0;
        Pipeline = null;
        ValidatedPipeline = null;
        StencilReference = 0;
        BlendFactor = Vector4.One;
        Metrics = default;
        OpenProfilerScopeCount = 0;
        MarkAllForBinding();
    }
}
