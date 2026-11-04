namespace Crystal.Graphics;

public enum FillMode
{
    Wireframe = 2,
    Solid,
}

public enum CullMode
{
    None = 1,
    Front,
    Back,
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
