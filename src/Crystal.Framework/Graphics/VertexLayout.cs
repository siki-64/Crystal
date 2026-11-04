namespace Crystal.Graphics;

public enum VertexElementFormat
{
    Single,
    Vector2,
    Vector3,
    Vector4,
    Color,
    Byte4,
    Short2,
    Short4,
    NormalizedShort2,
    NormalizedShort4,
    HalfVector2,
    HalfVector4,
    UInt,
    UInt2,
    UInt4,
}

public readonly record struct VertexElement(string Semantic, int SemanticIndex, VertexElementFormat Format, int Offset, int Slot = 0, int InstanceStepRate = 0)
{
    public static int SizeOf(VertexElementFormat format) => format switch
    {
        VertexElementFormat.Single or VertexElementFormat.Color or VertexElementFormat.Byte4 or VertexElementFormat.Short2
            or VertexElementFormat.NormalizedShort2 or VertexElementFormat.HalfVector2 or VertexElementFormat.UInt => 4,
        VertexElementFormat.Vector2 or VertexElementFormat.Short4 or VertexElementFormat.NormalizedShort4 or VertexElementFormat.HalfVector4
            or VertexElementFormat.UInt2 => 8,
        VertexElementFormat.Vector3 => 12,
        VertexElementFormat.Vector4 or VertexElementFormat.UInt4 => 16,
        _ => throw new ArgumentOutOfRangeException(nameof(format)),
    };
}

public sealed class VertexLayout
{
    public const int MaximumSlots = 16;

    private readonly VertexElement[] _elements;

    public VertexLayout(params ReadOnlySpan<VertexElement> elements)
    {
        if (elements.IsEmpty) throw new ArgumentException("A vertex layout needs at least one element.", nameof(elements));
        foreach (var element in elements)
        {
            ArgumentException.ThrowIfNullOrWhiteSpace(element.Semantic, nameof(elements));
            if (element.Slot is < 0 or >= MaximumSlots) throw new ArgumentException($"Vertex element '{element.Semantic}{element.SemanticIndex}' uses slot {element.Slot}; slots go from 0 to {MaximumSlots - 1}.", nameof(elements));
            if (element.Offset < 0 || element.SemanticIndex < 0 || element.InstanceStepRate < 0) throw new ArgumentException($"Vertex element '{element.Semantic}{element.SemanticIndex}' has a negative value.", nameof(elements));
            SlotMask |= 1 << element.Slot;
        }
        _elements = elements.ToArray();
    }

    public IReadOnlyList<VertexElement> Elements => _elements;

    internal int SlotMask { get; }
}
