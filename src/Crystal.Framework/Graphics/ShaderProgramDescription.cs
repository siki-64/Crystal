using static Crystal.Interop.D3D_SHADER_VARIABLE_CLASS;
using static Crystal.Interop.D3D_SRV_DIMENSION;

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
    None = (int)D3D_SRV_DIMENSION_UNKNOWN,
    Buffer = (int)D3D_SRV_DIMENSION_BUFFER,
    Texture1D = (int)D3D_SRV_DIMENSION_TEXTURE1D,
    Texture1DArray = (int)D3D_SRV_DIMENSION_TEXTURE1DARRAY,
    Texture2D = (int)D3D_SRV_DIMENSION_TEXTURE2D,
    Texture2DArray = (int)D3D_SRV_DIMENSION_TEXTURE2DARRAY,
    Texture2DMS = (int)D3D_SRV_DIMENSION_TEXTURE2DMS,
    Texture2DMSArray = (int)D3D_SRV_DIMENSION_TEXTURE2DMSARRAY,
    Texture3D = (int)D3D_SRV_DIMENSION_TEXTURE3D,
    TextureCube = (int)D3D_SRV_DIMENSION_TEXTURECUBE,
    TextureCubeArray = (int)D3D_SRV_DIMENSION_TEXTURECUBEARRAY,
}

public enum ShaderVariableClass
{
    Scalar = (int)D3D_SVC_SCALAR,
    Vector = (int)D3D_SVC_VECTOR,
    MatrixRows = (int)D3D_SVC_MATRIX_ROWS,
    MatrixColumns = (int)D3D_SVC_MATRIX_COLUMNS,
    Struct = (int)D3D_SVC_STRUCT,
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
