using System.Numerics;
using Crystal.Graphics.D3D12;
using Crystal.Interop;
using static Crystal.Interop.D3D12_COMPARISON_FUNC;
using static Crystal.Interop.D3D12_FILTER_REDUCTION_TYPE;
using static Crystal.Interop.D3D12_FILTER_TYPE;
using static Crystal.Interop.D3D12_TEXTURE_ADDRESS_MODE;
using static Crystal.Interop.D3D12;

namespace Crystal.Graphics;

public enum FilterMode
{
    Point = (int)D3D12_FILTER_TYPE_POINT,
    Linear = (int)D3D12_FILTER_TYPE_LINEAR,
}

public enum FilterReduction
{
    Standard = (int)D3D12_FILTER_REDUCTION_TYPE_STANDARD,
    Comparison = (int)D3D12_FILTER_REDUCTION_TYPE_COMPARISON,
    Minimum = (int)D3D12_FILTER_REDUCTION_TYPE_MINIMUM,
    Maximum = (int)D3D12_FILTER_REDUCTION_TYPE_MAXIMUM,
}

public enum TextureAddressMode
{
    Wrap = (int)D3D12_TEXTURE_ADDRESS_MODE_WRAP,
    Mirror = (int)D3D12_TEXTURE_ADDRESS_MODE_MIRROR,
    Clamp = (int)D3D12_TEXTURE_ADDRESS_MODE_CLAMP,
    Border = (int)D3D12_TEXTURE_ADDRESS_MODE_BORDER,
    MirrorOnce = (int)D3D12_TEXTURE_ADDRESS_MODE_MIRROR_ONCE,
}

public enum ComparisonFunction
{
    Never = (int)D3D12_COMPARISON_FUNC_NEVER,
    Less = (int)D3D12_COMPARISON_FUNC_LESS,
    Equal = (int)D3D12_COMPARISON_FUNC_EQUAL,
    LessEqual = (int)D3D12_COMPARISON_FUNC_LESS_EQUAL,
    Greater = (int)D3D12_COMPARISON_FUNC_GREATER,
    NotEqual = (int)D3D12_COMPARISON_FUNC_NOT_EQUAL,
    GreaterEqual = (int)D3D12_COMPARISON_FUNC_GREATER_EQUAL,
    Always = (int)D3D12_COMPARISON_FUNC_ALWAYS,
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
    public float MaxLod { get; init; } = D3D12_FLOAT32_MAX;

    public static SamplerDescription PointWrap => new() { MinFilter = FilterMode.Point, MagFilter = FilterMode.Point, MipFilter = FilterMode.Point };
    public static SamplerDescription PointClamp => PointWrap with { AddressU = TextureAddressMode.Clamp, AddressV = TextureAddressMode.Clamp, AddressW = TextureAddressMode.Clamp };
    public static SamplerDescription LinearWrap => new();
    public static SamplerDescription LinearClamp => LinearWrap with { AddressU = TextureAddressMode.Clamp, AddressV = TextureAddressMode.Clamp, AddressW = TextureAddressMode.Clamp };
    public static SamplerDescription AnisotropicWrap => new() { MaxAnisotropy = (int)D3D12_MAX_MAXANISOTROPY };
    public static SamplerDescription AnisotropicClamp => AnisotropicWrap with { AddressU = TextureAddressMode.Clamp, AddressV = TextureAddressMode.Clamp, AddressW = TextureAddressMode.Clamp };
    public static SamplerDescription ShadowComparison => LinearClamp with { MipFilter = FilterMode.Point, Reduction = FilterReduction.Comparison, ComparisonFunction = ComparisonFunction.LessEqual };
}

public sealed unsafe class SamplerState
{
    internal SamplerState(GraphicsDevice graphicsDevice, in SamplerDescription description)
    {
        if (description.MaxAnisotropy is < 1 or > (int)D3D12_MAX_MAXANISOTROPY) throw new ArgumentOutOfRangeException(nameof(description), "MaxAnisotropy must be between 1 and 16.");
        Description = description;
        var device = graphicsDevice.Native;
        DescriptorIndex = device.SamplerHeap.Allocate();
        var sampler = D3D12States.ToD3D12(description);
        device.Device->CreateSampler(&sampler, device.SamplerHeap.CpuHandle(DescriptorIndex));
    }

    public SamplerDescription Description { get; }

    internal uint DescriptorIndex { get; }
}
