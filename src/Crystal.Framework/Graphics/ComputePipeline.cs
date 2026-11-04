using Crystal.Graphics.D3D12;
using TerraFX.Interop.DirectX;
using TerraFX.Interop.Windows;
using static Crystal.Graphics.D3D12.D3D12Interop;
using static TerraFX.Interop.Windows.Windows;

namespace Crystal.Graphics;

public sealed unsafe class ComputePipeline : GraphicsResource
{
    private ID3D12PipelineState* _pipelineState;

    public ComputePipeline(GraphicsDevice graphicsDevice, ShaderProgram program, string? name = null)
        : base(graphicsDevice, name)
    {
        ArgumentNullException.ThrowIfNull(program);
        if (program.Library.GraphicsDevice != graphicsDevice) throw new ArgumentException("The program belongs to another GraphicsDevice.", nameof(program));
        ObjectDisposedException.ThrowIf(program.Library.IsDisposed, program.Library);
        if (!program.IsCompute) throw new ArgumentException($"Program '{program.Name}' has no compute shader. Use GraphicsPipeline for vertex and pixel programs.", nameof(program));
        Program = program;
        _pipelineState = Create(graphicsDevice.Native, program, Name ?? $"{program.Library.Name}.{program.Name}");
        graphicsDevice.Track(this);
    }

    public ShaderProgram Program { get; }

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

    private static ID3D12PipelineState* Create(D3D12Device device, ShaderProgram program, string name)
    {
        fixed (byte* computeShader = program.ComputeShader)
        {
            var description = new D3D12_COMPUTE_PIPELINE_STATE_DESC
            {
                pRootSignature = program.RootSignature,
                CS = new D3D12_SHADER_BYTECODE { pShaderBytecode = computeShader, BytecodeLength = (nuint)program.ComputeShader!.Length },
            };
            ID3D12PipelineState* pipelineState;
            var result = device.Device->CreateComputePipelineState(&description, __uuidof<ID3D12PipelineState>(), (void**)&pipelineState);
            if (FAILED(result))
                throw new GraphicsException($"Creating compute pipeline '{name}' failed (HRESULT 0x{result.Value:X8}). Enable the debug layer with CRYSTAL_DX12_DEBUG=1 for details.");
            SetName((ID3D12Object*)pipelineState, name);
            return pipelineState;
        }
    }
}
