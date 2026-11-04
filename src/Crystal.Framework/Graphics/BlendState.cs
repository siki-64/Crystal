namespace Crystal.Graphics;

public enum Blend
{
    Zero = 1,
    One,
    SourceColor,
    InverseSourceColor,
    SourceAlpha,
    InverseSourceAlpha,
    DestinationAlpha,
    InverseDestinationAlpha,
    DestinationColor,
    InverseDestinationColor,
    SourceAlphaSaturate,
    BlendFactor = 14,
    InverseBlendFactor,
    Source1Color,
    InverseSource1Color,
    Source1Alpha,
    InverseSource1Alpha,
    AlphaFactor,
    InverseAlphaFactor,
}

public enum BlendOperation
{
    Add = 1,
    Subtract,
    ReverseSubtract,
    Min,
    Max,
}

[Flags]
public enum ColorWriteChannels
{
    None = 0,
    Red = 1,
    Green = 2,
    Blue = 4,
    Alpha = 8,
    All = Red | Green | Blue | Alpha,
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
