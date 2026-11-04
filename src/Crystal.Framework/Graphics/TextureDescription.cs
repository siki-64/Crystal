using System.Numerics;

namespace Crystal.Graphics;

[Flags]
public enum TextureUsage
{
    None = 0,
    ShaderResource = 1 << 0,
    RenderTarget = 1 << 1,
    DepthStencil = 1 << 2,
    UnorderedAccess = 1 << 3,
}

public readonly record struct TextureDescription
{
    public TextureDescription()
    {
    }

    public int Width { get; init; }
    public int Height { get; init; }
    public int MipLevels { get; init; } = 1;
    public int ArraySize { get; init; } = 1;
    public SurfaceFormat Format { get; init; } = SurfaceFormat.Rgba8Unorm;
    public int SampleCount { get; init; } = 1;
    public TextureUsage Usage { get; init; } = TextureUsage.ShaderResource;
    public Vector4 ClearColor { get; init; }
    public float ClearDepth { get; init; } = 1f;
    public byte ClearStencil { get; init; }
    public string? Name { get; init; }
}

public enum CubeFace
{
    PositiveX,
    NegativeX,
    PositiveY,
    NegativeY,
    PositiveZ,
    NegativeZ,
}
