using System.Runtime.InteropServices;
using Crystal.Graphics.D3D12;
using TerraFX.Interop.DirectX;
using TerraFX.Interop.Windows;
using static Crystal.Graphics.D3D12.D3D12Interop;
using static TerraFX.Interop.DirectX.D3D_PRIMITIVE_TOPOLOGY;
using static TerraFX.Interop.DirectX.D3D12_INPUT_CLASSIFICATION;
using static TerraFX.Interop.DirectX.D3D12_PRIMITIVE_TOPOLOGY_TYPE;
using static TerraFX.Interop.DirectX.DXGI_FORMAT;
using static TerraFX.Interop.Windows.Windows;

namespace Crystal.Graphics;

public enum PrimitiveType
{
    TriangleList,
    TriangleStrip,
    LineList,
    LineStrip,
    PointList,
}

public sealed record GraphicsPipelineDescription
{
    public required ShaderProgram Program { get; init; }
    public BlendState BlendState { get; init; } = BlendState.Opaque;
    public RasterizerState RasterizerState { get; init; } = RasterizerState.CullBack;
    public DepthStencilState DepthStencilState { get; init; } = DepthStencilState.Default;
    public VertexLayout? VertexLayout { get; init; }
    public PrimitiveType PrimitiveType { get; init; } = PrimitiveType.TriangleList;
    public IReadOnlyList<SurfaceFormat> RenderTargetFormats { get; init; } = [SurfaceFormat.Rgba8Unorm];
    public SurfaceFormat? DepthStencilFormat { get; init; }
    public int SampleCount { get; init; } = 1;
    public string? Name { get; init; }
}

public sealed unsafe class GraphicsPipeline : GraphicsResource
{
    private readonly SurfaceFormat[] _renderTargetFormats;
    private ID3D12PipelineState* _pipelineState;

    public GraphicsPipeline(GraphicsDevice graphicsDevice, GraphicsPipelineDescription description)
        : base(graphicsDevice, description?.Name)
    {
        ArgumentNullException.ThrowIfNull(description);
        ArgumentNullException.ThrowIfNull(description.Program);
        if (description.Program.Library.GraphicsDevice != graphicsDevice) throw new ArgumentException("The program belongs to another GraphicsDevice.", nameof(description));
        ObjectDisposedException.ThrowIf(description.Program.Library.IsDisposed, description.Program.Library);
        Description = description;
        _renderTargetFormats = [.. description.RenderTargetFormats];
        Validate(description, _renderTargetFormats);

        Program = description.Program;
        RequiredVertexSlots = description.VertexLayout?.SlotMask ?? 0;
        Topology = description.PrimitiveType switch
        {
            PrimitiveType.TriangleStrip => D3D_PRIMITIVE_TOPOLOGY_TRIANGLESTRIP,
            PrimitiveType.LineList => D3D_PRIMITIVE_TOPOLOGY_LINELIST,
            PrimitiveType.LineStrip => D3D_PRIMITIVE_TOPOLOGY_LINESTRIP,
            PrimitiveType.PointList => D3D_PRIMITIVE_TOPOLOGY_POINTLIST,
            _ => D3D_PRIMITIVE_TOPOLOGY_TRIANGLELIST,
        };
        _pipelineState = Create(graphicsDevice.Native, description, _renderTargetFormats, Name ?? $"{Program.Library.Name}.{Program.Name}");
        graphicsDevice.Track(this);
    }

    public GraphicsPipelineDescription Description { get; }
    public ShaderProgram Program { get; }

    internal ReadOnlySpan<SurfaceFormat> RenderTargetFormats => _renderTargetFormats;
    internal SurfaceFormat? DepthStencilFormat => Description.DepthStencilFormat;
    internal int SampleCount => Description.SampleCount;
    internal int RequiredVertexSlots { get; }
    internal D3D_PRIMITIVE_TOPOLOGY Topology { get; }

    internal ID3D12PipelineState* PipelineState
    {
        get
        {
            ObjectDisposedException.ThrowIf(_pipelineState == null, this);
            return _pipelineState;
        }
    }

    private protected override void ReleaseNativeResources()
    {
        GraphicsDevice.ReleaseDeviceObject((IUnknown*)_pipelineState);
        _pipelineState = null;
    }

    private static void Validate(GraphicsPipelineDescription description, SurfaceFormat[] renderTargetFormats)
    {
        ArgumentNullException.ThrowIfNull(description.BlendState, nameof(description));
        ArgumentNullException.ThrowIfNull(description.RasterizerState, nameof(description));
        ArgumentNullException.ThrowIfNull(description.DepthStencilState, nameof(description));
        if (renderTargetFormats.Length > GraphicsDevice.MaximumRenderTargets)
            throw new ArgumentException($"A pipeline can write at most {GraphicsDevice.MaximumRenderTargets} render targets.", nameof(description));
        foreach (var format in renderTargetFormats)
        {
            var info = D3D12Formats.Get(format);
            if (info.IsDepth || info.IsBlockCompressed) throw new ArgumentException($"{format} can't be a render target format.", nameof(description));
        }
        if (description.DepthStencilFormat is { } depthFormat && !D3D12Formats.Get(depthFormat).IsDepth)
            throw new ArgumentException($"{depthFormat} is not a depth format.", nameof(description));
        if (renderTargetFormats.Length == 0 && description.DepthStencilFormat == null)
            throw new ArgumentException("A pipeline needs at least one render target or depth format.", nameof(description));
        if (description.SampleCount < 1) throw new ArgumentOutOfRangeException(nameof(description), "SampleCount must be at least 1.");

        var program = description.Program;
        if (program.IsCompute) throw new ArgumentException($"Program '{program.Name}' is a compute program. Use ComputePipeline for it.", nameof(description));
        if (program.Info.VertexInputs.Length > 0 && description.VertexLayout == null)
            throw new ArgumentException($"Program '{program.Name}' reads vertex inputs, so the pipeline needs a VertexLayout.", nameof(description));
        foreach (var input in program.Info.VertexInputs)
        {
            var found = false;
            foreach (var element in description.VertexLayout!.Elements)
            {
                if (element.SemanticIndex == input.SemanticIndex && string.Equals(element.Semantic, input.Semantic, StringComparison.OrdinalIgnoreCase))
                {
                    found = true;
                    break;
                }
            }
            if (!found)
                throw new ArgumentException($"Program '{program.Name}' reads {input.Semantic}{input.SemanticIndex}, which the vertex layout doesn't provide.", nameof(description));
        }
    }

    private static ID3D12PipelineState* Create(D3D12Device device, GraphicsPipelineDescription description, SurfaceFormat[] renderTargetFormats, string name)
    {
        var program = description.Program;
        var elements = description.VertexLayout?.Elements ?? [];
        var semanticNames = new nint[elements.Count];
        var inputElements = new D3D12_INPUT_ELEMENT_DESC[elements.Count];
        try
        {
            for (var index = 0; index < elements.Count; index++)
            {
                var element = elements[index];
                semanticNames[index] = Marshal.StringToCoTaskMemUTF8(element.Semantic);
                inputElements[index] = new D3D12_INPUT_ELEMENT_DESC
                {
                    SemanticName = (sbyte*)semanticNames[index],
                    SemanticIndex = (uint)element.SemanticIndex,
                    Format = ToDxgi(element.Format),
                    InputSlot = (uint)element.Slot,
                    AlignedByteOffset = (uint)element.Offset,
                    InputSlotClass = element.InstanceStepRate > 0 ? D3D12_INPUT_CLASSIFICATION_PER_INSTANCE_DATA : D3D12_INPUT_CLASSIFICATION_PER_VERTEX_DATA,
                    InstanceDataStepRate = (uint)element.InstanceStepRate,
                };
            }

            var depthStencil = D3D12States.ToD3D12(description.DepthStencilState);
            if (description.DepthStencilFormat == null)
            {
                depthStencil.DepthEnable = FALSE;
                depthStencil.StencilEnable = FALSE;
            }

            fixed (byte* vertexShader = program.VertexShader)
            fixed (byte* pixelShader = program.PixelShader)
            fixed (D3D12_INPUT_ELEMENT_DESC* inputElementPointer = inputElements)
            {
                var pipeline = new D3D12_GRAPHICS_PIPELINE_STATE_DESC
                {
                    pRootSignature = program.RootSignature,
                    VS = new D3D12_SHADER_BYTECODE { pShaderBytecode = vertexShader, BytecodeLength = (nuint)program.VertexShader!.Length },
                    PS = pixelShader == null ? default : new D3D12_SHADER_BYTECODE { pShaderBytecode = pixelShader, BytecodeLength = (nuint)program.PixelShader!.Length },
                    BlendState = D3D12States.ToD3D12(description.BlendState),
                    SampleMask = uint.MaxValue,
                    RasterizerState = D3D12States.ToD3D12(description.RasterizerState),
                    DepthStencilState = depthStencil,
                    InputLayout = new D3D12_INPUT_LAYOUT_DESC { pInputElementDescs = inputElementPointer, NumElements = (uint)inputElements.Length },
                    PrimitiveTopologyType = description.PrimitiveType switch
                    {
                        PrimitiveType.LineList or PrimitiveType.LineStrip => D3D12_PRIMITIVE_TOPOLOGY_TYPE_LINE,
                        PrimitiveType.PointList => D3D12_PRIMITIVE_TOPOLOGY_TYPE_POINT,
                        _ => D3D12_PRIMITIVE_TOPOLOGY_TYPE_TRIANGLE,
                    },
                    NumRenderTargets = (uint)renderTargetFormats.Length,
                    DSVFormat = description.DepthStencilFormat is { } depthFormat ? D3D12Formats.Get(depthFormat).DepthStencilFormat : DXGI_FORMAT_UNKNOWN,
                    SampleDesc = new DXGI_SAMPLE_DESC { Count = (uint)description.SampleCount, Quality = 0 },
                };
                for (var index = 0; index < renderTargetFormats.Length; index++) pipeline.RTVFormats[index] = D3D12Formats.Get(renderTargetFormats[index]).ViewFormat;

                ID3D12PipelineState* pipelineState;
                var result = device.Device->CreateGraphicsPipelineState(&pipeline, __uuidof<ID3D12PipelineState>(), (void**)&pipelineState);
                if (FAILED(result))
                    throw new GraphicsException($"Creating pipeline '{name}' failed (HRESULT 0x{result.Value:X8}). Enable the debug layer with CRYSTAL_DX12_DEBUG=1 for details.");
                SetName((ID3D12Object*)pipelineState, name);
                return pipelineState;
            }
        }
        finally
        {
            foreach (var semanticName in semanticNames) Marshal.FreeCoTaskMem(semanticName);
        }
    }

    private static DXGI_FORMAT ToDxgi(VertexElementFormat format) => format switch
    {
        VertexElementFormat.Single => DXGI_FORMAT_R32_FLOAT,
        VertexElementFormat.Vector2 => DXGI_FORMAT_R32G32_FLOAT,
        VertexElementFormat.Vector3 => DXGI_FORMAT_R32G32B32_FLOAT,
        VertexElementFormat.Vector4 => DXGI_FORMAT_R32G32B32A32_FLOAT,
        VertexElementFormat.Color => DXGI_FORMAT_R8G8B8A8_UNORM,
        VertexElementFormat.Byte4 => DXGI_FORMAT_R8G8B8A8_UINT,
        VertexElementFormat.Short2 => DXGI_FORMAT_R16G16_SINT,
        VertexElementFormat.Short4 => DXGI_FORMAT_R16G16B16A16_SINT,
        VertexElementFormat.NormalizedShort2 => DXGI_FORMAT_R16G16_SNORM,
        VertexElementFormat.NormalizedShort4 => DXGI_FORMAT_R16G16B16A16_SNORM,
        VertexElementFormat.HalfVector2 => DXGI_FORMAT_R16G16_FLOAT,
        VertexElementFormat.HalfVector4 => DXGI_FORMAT_R16G16B16A16_FLOAT,
        VertexElementFormat.UInt => DXGI_FORMAT_R32_UINT,
        VertexElementFormat.UInt2 => DXGI_FORMAT_R32G32_UINT,
        VertexElementFormat.UInt4 => DXGI_FORMAT_R32G32B32A32_UINT,
        _ => throw new ArgumentOutOfRangeException(nameof(format)),
    };
}
