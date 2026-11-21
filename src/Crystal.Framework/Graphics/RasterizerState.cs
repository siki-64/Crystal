using static Crystal.Interop.D3D12_CULL_MODE;
using static Crystal.Interop.D3D12_FILL_MODE;

namespace Crystal.Graphics;

public enum FillMode
{
    Wireframe = (int)D3D12_FILL_MODE_WIREFRAME,
    Solid = (int)D3D12_FILL_MODE_SOLID,
}

public enum CullMode
{
    None = (int)D3D12_CULL_MODE_NONE,
    Front = (int)D3D12_CULL_MODE_FRONT,
    Back = (int)D3D12_CULL_MODE_BACK,
}

public sealed record RasterizerState
{
    public FillMode FillMode { get; init; } = FillMode.Solid;
    public CullMode CullMode { get; init; } = CullMode.Back;
    public bool FrontCounterClockwise { get; init; }
    public int DepthBias { get; init; }
    public float DepthBiasClamp { get; init; }
    public float SlopeScaledDepthBias { get; init; }
    public bool DepthClipEnable { get; init; } = true;
    public bool AntialiasedLineEnable { get; init; }
    public bool ConservativeRaster { get; init; }

    public static RasterizerState CullNone { get; } = new() { CullMode = CullMode.None };
    public static RasterizerState CullBack { get; } = new();
    public static RasterizerState CullFront { get; } = new() { CullMode = CullMode.Front };
    public static RasterizerState Wireframe { get; } = new() { FillMode = FillMode.Wireframe, CullMode = CullMode.None };
}
