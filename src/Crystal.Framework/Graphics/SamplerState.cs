using System.Numerics;
using Crystal.Graphics.D3D12;
using TerraFX.Interop.DirectX;

namespace Crystal.Graphics;

public enum FilterMode
{
    Point,
    Linear,
}

public enum FilterReduction
{
    Standard,
    Comparison,
    Minimum,
    Maximum,
}

public enum TextureAddressMode
{
    Wrap = 1,
    Mirror,
    Clamp,
    Border,
    MirrorOnce,
}

public enum ComparisonFunction
{
    Never = 1,
    Less,
    Equal,
    LessEqual,
    Greater,
    NotEqual,
    GreaterEqual,
    Always,
}

public readonly record struct SamplerDescription
{
    public SamplerDescription()
    {
    }

    public FilterMode MinFilter { get; init; } = FilterMode.Linear;
    public FilterMode MagFilter { get; init; } = FilterMode.Linear;
    public FilterMode MipFilter { get; init; } = FilterMode.Linear;
    public FilterReduction Reduction { get; init; } = FilterReduction.Standard;
    public int MaxAnisotropy { get; init; } = 1;
    public TextureAddressMode AddressU { get; init; } = TextureAddressMode.Wrap;
    public TextureAddressMode AddressV { get; init; } = TextureAddressMode.Wrap;
    public TextureAddressMode AddressW { get; init; } = TextureAddressMode.Wrap;
    public float MipLodBias { get; init; }
    public ComparisonFunction ComparisonFunction { get; init; } = ComparisonFunction.Never;
    public Vector4 BorderColor { get; init; }
    public float MinLod { get; init; }
    public float MaxLod { get; init; } = float.MaxValue;

    public static SamplerDescription PointWrap => new() { MinFilter = FilterMode.Point, MagFilter = FilterMode.Point, MipFilter = FilterMode.Point };
    public static SamplerDescription PointClamp => PointWrap with { AddressU = TextureAddressMode.Clamp, AddressV = TextureAddressMode.Clamp, AddressW = TextureAddressMode.Clamp };
    public static SamplerDescription LinearWrap => new();
    public static SamplerDescription LinearClamp => LinearWrap with { AddressU = TextureAddressMode.Clamp, AddressV = TextureAddressMode.Clamp, AddressW = TextureAddressMode.Clamp };
    public static SamplerDescription AnisotropicWrap => new() { MaxAnisotropy = 16 };
    public static SamplerDescription AnisotropicClamp => AnisotropicWrap with { AddressU = TextureAddressMode.Clamp, AddressV = TextureAddressMode.Clamp, AddressW = TextureAddressMode.Clamp };
    public static SamplerDescription ShadowComparison => LinearClamp with { MipFilter = FilterMode.Point, Reduction = FilterReduction.Comparison, ComparisonFunction = ComparisonFunction.LessEqual };
}

public sealed unsafe class SamplerState
{
    internal SamplerState(GraphicsDevice graphicsDevice, in SamplerDescription description)
    {
        if (description.MaxAnisotropy is < 1 or > 16) throw new ArgumentOutOfRangeException(nameof(description), "MaxAnisotropy must be between 1 and 16.");
        Description = description;
        var device = graphicsDevice.Native;
        DescriptorIndex = device.SamplerHeap.Allocate();
        var sampler = D3D12States.ToD3D12(description);
        device.Device->CreateSampler(&sampler, device.SamplerHeap.CpuHandle(DescriptorIndex));
    }

    public SamplerDescription Description { get; }

    internal uint DescriptorIndex { get; }
}
