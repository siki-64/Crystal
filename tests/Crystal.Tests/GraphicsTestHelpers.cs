using System.Numerics;
using Crystal.Graphics;

namespace Crystal.Tests;

internal static class GraphicsTestHelpers
{
    public const int TargetSize = 64;

    public static ShaderLibrary LoadLibrary(GraphicsDevice device, string name) => ShaderLibrary.Load(device, $"Shaders/{name}.crystalshader");

    public static Texture2D CreateTarget(GraphicsDevice device, int size = TargetSize) =>
        new(device, new TextureDescription { Width = size, Height = size, Usage = TextureUsage.RenderTarget });

    public static GraphicsBuffer CreateVertexBuffer<T>(GraphicsDevice device, ReadOnlySpan<T> vertices) where T : unmanaged
    {
        var stride = System.Runtime.CompilerServices.Unsafe.SizeOf<T>();
        var buffer = new GraphicsBuffer(device, new BufferDescription { Size = vertices.Length * stride, Stride = stride, Usage = BufferUsage.Vertex });
        buffer.SetData(vertices);
        return buffer;
    }

    public static VertexPositionColor[] FullQuad(float depth, Color color) =>
    [
        new(new Vector3(-1f, 1f, depth), color),
        new(new Vector3(1f, 1f, depth), color),
        new(new Vector3(-1f, -1f, depth), color),
        new(new Vector3(-1f, -1f, depth), color),
        new(new Vector3(1f, 1f, depth), color),
        new(new Vector3(1f, -1f, depth), color),
    ];

    public static VertexPositionColor[] LeftHalfQuad(Color color, float depth = 0f) =>
    [
        new(new Vector3(-1f, 1f, depth), color),
        new(new Vector3(0f, 1f, depth), color),
        new(new Vector3(-1f, -1f, depth), color),
        new(new Vector3(-1f, -1f, depth), color),
        new(new Vector3(0f, 1f, depth), color),
        new(new Vector3(0f, -1f, depth), color),
    ];

    public static uint[] ReadPixels(Texture2D target, int size = TargetSize)
    {
        var pixels = new uint[size * size];
        target.GetData<uint>(pixels);
        return pixels;
    }

    public static void AssertPixel(Color expected, uint[] pixels, int x, int y, int size = TargetSize)
    {
        var actual = Color.FromPacked(pixels[y * size + x]);
        Assert.IsTrue(IsClose(expected, actual), $"Pixel ({x}, {y}) is {actual}, expected {expected}.");
    }

    public static void AssertAllPixels(Color expected, uint[] pixels)
    {
        foreach (var pixel in pixels)
        {
            var actual = Color.FromPacked(pixel);
            if (!IsClose(expected, actual)) Assert.Fail($"A pixel is {actual}, expected {expected}.");
        }
    }

    public static bool IsClose(Color expected, Color actual) =>
        Math.Abs(expected.R - actual.R) <= 1 && Math.Abs(expected.G - actual.G) <= 1 && Math.Abs(expected.B - actual.B) <= 1 && Math.Abs(expected.A - actual.A) <= 1;
}
