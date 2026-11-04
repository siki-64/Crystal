namespace Crystal.Graphics;

[Flags]
public enum BufferUsage
{
    None = 0,
    Vertex = 1 << 0,
    Index = 1 << 1,
    Constant = 1 << 2,
    Structured = 1 << 3,
    Indirect = 1 << 4,
    UnorderedAccess = 1 << 5,
}

public readonly record struct BufferDescription
{
    public long Size { get; init; }
    public int Stride { get; init; }
    public BufferUsage Usage { get; init; }
    public string? Name { get; init; }
}
