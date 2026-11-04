using System.Numerics;
using System.Runtime.InteropServices;

namespace Crystal.Graphics;

[StructLayout(LayoutKind.Sequential, Pack = 1)]
public readonly record struct VertexPositionColor(Vector3 Position, Color Color)
{
    public static VertexLayout Layout { get; } = new(
        new VertexElement("POSITION", 0, VertexElementFormat.Vector3, 0),
        new VertexElement("COLOR", 0, VertexElementFormat.Color, 12));
}

[StructLayout(LayoutKind.Sequential, Pack = 1)]
public readonly record struct VertexPositionTexture(Vector3 Position, Vector2 TextureCoordinate)
{
    public static VertexLayout Layout { get; } = new(
        new VertexElement("POSITION", 0, VertexElementFormat.Vector3, 0),
        new VertexElement("TEXCOORD", 0, VertexElementFormat.Vector2, 12));
}

[StructLayout(LayoutKind.Sequential, Pack = 1)]
public readonly record struct VertexPositionColorTexture(Vector3 Position, Color Color, Vector2 TextureCoordinate)
{
    public static VertexLayout Layout { get; } = new(
        new VertexElement("POSITION", 0, VertexElementFormat.Vector3, 0),
        new VertexElement("COLOR", 0, VertexElementFormat.Color, 12),
        new VertexElement("TEXCOORD", 0, VertexElementFormat.Vector2, 16));
}

[StructLayout(LayoutKind.Sequential, Pack = 1)]
public readonly record struct VertexPositionNormalTexture(Vector3 Position, Vector3 Normal, Vector2 TextureCoordinate)
{
    public static VertexLayout Layout { get; } = new(
        new VertexElement("POSITION", 0, VertexElementFormat.Vector3, 0),
        new VertexElement("NORMAL", 0, VertexElementFormat.Vector3, 12),
        new VertexElement("TEXCOORD", 0, VertexElementFormat.Vector2, 24));
}
