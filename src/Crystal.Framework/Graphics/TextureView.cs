namespace Crystal.Graphics;

public sealed class TextureView
{
    internal TextureView(Texture texture, int mipLevel, uint shaderResourceViewIndex, int shaderResourceDimension, uint unorderedAccessViewIndex, int unorderedAccessDimension)
    {
        Texture = texture;
        MipLevel = mipLevel;
        ShaderResourceViewIndex = shaderResourceViewIndex;
        ShaderResourceDimension = shaderResourceDimension;
        UnorderedAccessViewIndex = unorderedAccessViewIndex;
        UnorderedAccessDimension = unorderedAccessDimension;
    }

    public Texture Texture { get; }
    public int MipLevel { get; }
    public int Width => Math.Max(Texture.Width >> MipLevel, 1);
    public int Height => Math.Max(Texture.Height >> MipLevel, 1);

    internal uint ShaderResourceViewIndex { get; }
    internal int ShaderResourceDimension { get; }
    internal uint UnorderedAccessViewIndex { get; }
    internal int UnorderedAccessDimension { get; }

    public override string ToString() => $"{Texture.Name} mip {MipLevel}";
}
