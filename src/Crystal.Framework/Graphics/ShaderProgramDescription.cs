namespace Crystal.Graphics;

[Flags]
public enum ShaderStages
{
    None = 0,
    Vertex = 1 << 0,
    Pixel = 1 << 1,
    Compute = 1 << 2,
}

public enum ShaderResourceKind
{
    Texture,
    StructuredBuffer,
    ByteAddressBuffer,
    ReadWriteTexture,
    ReadWriteStructuredBuffer,
    ReadWriteByteAddressBuffer,
    Sampler,
}

public enum ShaderResourceDimension
{
    None = 0,
    Buffer = 1,
    Texture1D = 2,
    Texture1DArray = 3,
    Texture2D = 4,
    Texture2DArray = 5,
    Texture2DMS = 6,
    Texture2DMSArray = 7,
    Texture3D = 8,
    TextureCube = 9,
    TextureCubeArray = 10,
}

public enum ShaderVariableClass
{
    Scalar,
    Vector,
    MatrixRows,
    MatrixColumns,
    Struct,
}

public sealed record ShaderProgramDescription
{
    public required string Name { get; init; }
    public ReadOnlyMemory<byte> VertexShader { get; init; }
    public ReadOnlyMemory<byte> PixelShader { get; init; }
    public ReadOnlyMemory<byte> ComputeShader { get; init; }
    public ThreadGroupSize ThreadGroupSize { get; init; }
    public IReadOnlyList<ShaderConstantBufferDescription> ConstantBuffers { get; init; } = [];
    public IReadOnlyList<ShaderResourceDescription> Resources { get; init; } = [];
    public IReadOnlyList<ShaderVertexInputDescription> VertexInputs { get; init; } = [];
    public bool UsesDescriptorIndices { get; init; }
}

public sealed record ShaderConstantBufferDescription(
    string Name,
    int Register,
    int Size,
    ShaderStages Stages,
    IReadOnlyList<ShaderVariableDescription> Variables,
    int Space = 0);

public sealed record ShaderVariableDescription(string Name, int Offset, int Size, ShaderVariableClass Class, int Rows, int Columns, int Elements = 0);

public sealed record ShaderResourceDescription(
    string Name,
    ShaderResourceKind Kind,
    ShaderResourceDimension Dimension,
    int Register,
    ShaderStages Stages,
    int Space = 0,
    int VertexDescriptorSlot = -1,
    int PixelDescriptorSlot = -1,
    int ComputeDescriptorSlot = -1);

public sealed record ShaderVertexInputDescription(string Semantic, int SemanticIndex, int Register);
