namespace Crystal.Graphics;

public sealed class Texture2D : Texture
{
    public Texture2D(GraphicsDevice graphicsDevice, in TextureDescription description)
        : base(graphicsDevice, description, description.ArraySize, isCube: false)
    {
    }

    public void SetData<T>(ReadOnlySpan<T> data, int mipLevel = 0, int arraySlice = 0) where T : unmanaged =>
        SetDataCore(data, mipLevel, arraySlice);

    public void SetData<T>(ReadOnlySpan<T> data, Rectangle region, int mipLevel = 0, int arraySlice = 0) where T : unmanaged =>
        SetDataCore(data, region, mipLevel, arraySlice);

    public void GetData<T>(Span<T> destination, int mipLevel = 0, int arraySlice = 0) where T : unmanaged =>
        GetDataCore(destination, mipLevel, arraySlice);

    public GraphicsReadback BeginGetData(int mipLevel = 0, int arraySlice = 0) => BeginGetDataCore(mipLevel, arraySlice);
}
