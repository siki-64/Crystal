using static TerraFX.Interop.DirectX.D3D12_STENCIL_OP;

namespace Crystal.Graphics;

public enum StencilOperation
{
    Keep = (int)D3D12_STENCIL_OP_KEEP,
    Zero = (int)D3D12_STENCIL_OP_ZERO,
    Replace = (int)D3D12_STENCIL_OP_REPLACE,
    IncrementSaturate = (int)D3D12_STENCIL_OP_INCR_SAT,
    DecrementSaturate = (int)D3D12_STENCIL_OP_DECR_SAT,
    Invert = (int)D3D12_STENCIL_OP_INVERT,
    Increment = (int)D3D12_STENCIL_OP_INCR,
    Decrement = (int)D3D12_STENCIL_OP_DECR,
}

public readonly record struct StencilFaceDescription
{
    public StencilFaceDescription()
    {
    }

    public StencilOperation FailOperation { get; init; } = StencilOperation.Keep;
    public StencilOperation DepthFailOperation { get; init; } = StencilOperation.Keep;
    public StencilOperation PassOperation { get; init; } = StencilOperation.Keep;
    public ComparisonFunction Function { get; init; } = ComparisonFunction.Always;
}

public sealed record DepthStencilState
{
    public bool DepthEnable { get; init; } = true;
    public bool DepthWriteEnable { get; init; } = true;
    public ComparisonFunction DepthFunction { get; init; } = ComparisonFunction.LessEqual;
    public bool StencilEnable { get; init; }
    public byte StencilReadMask { get; init; } = byte.MaxValue;
    public byte StencilWriteMask { get; init; } = byte.MaxValue;
    public StencilFaceDescription FrontFace { get; init; } = new();
    public StencilFaceDescription BackFace { get; init; } = new();

    public static DepthStencilState Default { get; } = new();
    public static DepthStencilState DepthRead { get; } = new() { DepthWriteEnable = false };
    public static DepthStencilState None { get; } = new() { DepthEnable = false, DepthWriteEnable = false };
    public static DepthStencilState ReverseZ { get; } = new() { DepthFunction = ComparisonFunction.GreaterEqual };
    public static DepthStencilState ReverseZRead { get; } = new() { DepthFunction = ComparisonFunction.GreaterEqual, DepthWriteEnable = false };
}
