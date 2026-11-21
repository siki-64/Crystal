using static Crystal.Interop.D3D12_BLEND;
using static Crystal.Interop.D3D12_BLEND_OP;
using static Crystal.Interop.D3D12_COLOR_WRITE_ENABLE;

namespace Crystal.Graphics;

public enum Blend
{
    Zero = (int)D3D12_BLEND_ZERO,
    One = (int)D3D12_BLEND_ONE,
    SourceColor = (int)D3D12_BLEND_SRC_COLOR,
    InverseSourceColor = (int)D3D12_BLEND_INV_SRC_COLOR,
    SourceAlpha = (int)D3D12_BLEND_SRC_ALPHA,
    InverseSourceAlpha = (int)D3D12_BLEND_INV_SRC_ALPHA,
    DestinationAlpha = (int)D3D12_BLEND_DEST_ALPHA,
    InverseDestinationAlpha = (int)D3D12_BLEND_INV_DEST_ALPHA,
    DestinationColor = (int)D3D12_BLEND_DEST_COLOR,
    InverseDestinationColor = (int)D3D12_BLEND_INV_DEST_COLOR,
    SourceAlphaSaturate = (int)D3D12_BLEND_SRC_ALPHA_SAT,
    BlendFactor = (int)D3D12_BLEND_BLEND_FACTOR,
    InverseBlendFactor = (int)D3D12_BLEND_INV_BLEND_FACTOR,
    Source1Color = (int)D3D12_BLEND_SRC1_COLOR,
    InverseSource1Color = (int)D3D12_BLEND_INV_SRC1_COLOR,
    Source1Alpha = (int)D3D12_BLEND_SRC1_ALPHA,
    InverseSource1Alpha = (int)D3D12_BLEND_INV_SRC1_ALPHA,
    AlphaFactor = (int)D3D12_BLEND_ALPHA_FACTOR,
    InverseAlphaFactor = (int)D3D12_BLEND_INV_ALPHA_FACTOR,
}

public enum BlendOperation
{
    Add = (int)D3D12_BLEND_OP_ADD,
    Subtract = (int)D3D12_BLEND_OP_SUBTRACT,
    ReverseSubtract = (int)D3D12_BLEND_OP_REV_SUBTRACT,
    Min = (int)D3D12_BLEND_OP_MIN,
    Max = (int)D3D12_BLEND_OP_MAX,
}

[Flags]
public enum ColorWriteChannels
{
    None = 0,
    Red = (int)D3D12_COLOR_WRITE_ENABLE_RED,
    Green = (int)D3D12_COLOR_WRITE_ENABLE_GREEN,
    Blue = (int)D3D12_COLOR_WRITE_ENABLE_BLUE,
    Alpha = (int)D3D12_COLOR_WRITE_ENABLE_ALPHA,
    All = (int)D3D12_COLOR_WRITE_ENABLE_ALL,
}

public readonly record struct RenderTargetBlendDescription
{
    public RenderTargetBlendDescription()
    {
    }

    public bool BlendEnable { get; init; }
    public Blend SourceBlend { get; init; } = Blend.One;
    public Blend DestinationBlend { get; init; } = Blend.Zero;
    public BlendOperation BlendOperation { get; init; } = BlendOperation.Add;
    public Blend SourceBlendAlpha { get; init; } = Blend.One;
    public Blend DestinationBlendAlpha { get; init; } = Blend.Zero;
    public BlendOperation BlendOperationAlpha { get; init; } = BlendOperation.Add;
    public ColorWriteChannels WriteMask { get; init; } = ColorWriteChannels.All;

    public static RenderTargetBlendDescription Opaque => new();

    public static RenderTargetBlendDescription AlphaBlend => new()
    {
        BlendEnable = true,
        SourceBlend = Blend.One,
        DestinationBlend = Blend.InverseSourceAlpha,
        SourceBlendAlpha = Blend.One,
        DestinationBlendAlpha = Blend.InverseSourceAlpha,
    };

    public static RenderTargetBlendDescription NonPremultiplied => new()
    {
        BlendEnable = true,
        SourceBlend = Blend.SourceAlpha,
        DestinationBlend = Blend.InverseSourceAlpha,
        SourceBlendAlpha = Blend.SourceAlpha,
        DestinationBlendAlpha = Blend.InverseSourceAlpha,
    };

    public static RenderTargetBlendDescription Additive => new()
    {
        BlendEnable = true,
        SourceBlend = Blend.SourceAlpha,
        DestinationBlend = Blend.One,
        SourceBlendAlpha = Blend.SourceAlpha,
        DestinationBlendAlpha = Blend.One,
    };
}

public sealed class BlendState : IEquatable<BlendState>
{
    private readonly RenderTargetBlendDescription[] _renderTargets;

    public BlendState(RenderTargetBlendDescription renderTarget, bool alphaToCoverage = false)
    {
        _renderTargets = [renderTarget];
        AlphaToCoverage = alphaToCoverage;
    }

    public BlendState(ReadOnlySpan<RenderTargetBlendDescription> renderTargets, bool alphaToCoverage = false)
    {
        if (renderTargets.IsEmpty || renderTargets.Length > GraphicsDevice.MaximumRenderTargets)
            throw new ArgumentException($"Between 1 and {GraphicsDevice.MaximumRenderTargets} render target blend descriptions are needed.", nameof(renderTargets));
        _renderTargets = renderTargets.ToArray();
        AlphaToCoverage = alphaToCoverage;
    }

    public bool AlphaToCoverage { get; }
    public bool IndependentBlend => _renderTargets.Length > 1;
    public ReadOnlySpan<RenderTargetBlendDescription> RenderTargets => _renderTargets;

    public static BlendState Opaque { get; } = new(RenderTargetBlendDescription.Opaque);
    public static BlendState AlphaBlend { get; } = new(RenderTargetBlendDescription.AlphaBlend);
    public static BlendState NonPremultiplied { get; } = new(RenderTargetBlendDescription.NonPremultiplied);
    public static BlendState Additive { get; } = new(RenderTargetBlendDescription.Additive);

    public RenderTargetBlendDescription GetRenderTarget(int index) => _renderTargets[IndependentBlend ? index : 0];

    public bool Equals(BlendState? other) =>
        other != null && (ReferenceEquals(this, other) || (AlphaToCoverage == other.AlphaToCoverage && _renderTargets.AsSpan().SequenceEqual(other._renderTargets)));

    public override bool Equals(object? obj) => Equals(obj as BlendState);

    public override int GetHashCode()
    {
        var hash = new HashCode();
        hash.Add(AlphaToCoverage);
        foreach (var renderTarget in _renderTargets) hash.Add(renderTarget);
        return hash.ToHashCode();
    }
}
