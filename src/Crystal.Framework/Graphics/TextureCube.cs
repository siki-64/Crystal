namespace Crystal.Graphics;

public sealed class TextureCube : Texture
{
    public TextureCube(GraphicsDevice graphicsDevice, in TextureDescription description)
        : base(graphicsDevice, description, description.ArraySize * 6, isCube: true)
    {
    }

    public int Size => Width;
    public int CubeCount => ArrayLayers / 6;

    public void SetData<T>(CubeFace face, ReadOnlySpan<T> data, int mipLevel = 0, int cubeIndex = 0) where T : unmanaged =>
        SetDataCore(data, mipLevel, cubeIndex * 6 + (int)face);

    public void GetData<T>(CubeFace face, Span<T> destination, int mipLevel = 0, int cubeIndex = 0) where T : unmanaged =>
        GetDataCore(destination, mipLevel, cubeIndex * 6 + (int)face);

    public GraphicsReadback BeginGetData(CubeFace face, int mipLevel = 0, int cubeIndex = 0) => BeginGetDataCore(mipLevel, cubeIndex * 6 + (int)face);
}
