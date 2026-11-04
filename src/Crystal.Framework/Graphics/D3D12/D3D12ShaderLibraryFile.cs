using System.Text;

namespace Crystal.Graphics.D3D12;

[Flags]
internal enum D3D12ShaderVisibility : byte
{
    None = 0,
    Vertex = 1 << 0,
    Pixel = 1 << 1,
    Compute = 1 << 2,
}

internal enum D3D12ShaderResourceKind : byte
{
    Texture,
    StructuredBuffer,
    ByteAddressBuffer,
    ReadWriteTexture,
    ReadWriteStructuredBuffer,
    ReadWriteByteAddressBuffer,
    Sampler,
}

internal enum D3D12ShaderVariableClass : byte
{
    Scalar,
    Vector,
    MatrixRows,
    MatrixColumns,
    Struct,
}

internal sealed record D3D12ShaderVariableInfo(string Name, int Offset, int Size, D3D12ShaderVariableClass Class, int Rows, int Columns, int Elements);

internal sealed record D3D12ConstantBufferInfo(string Name, int Register, int Space, int Size, D3D12ShaderVisibility Visibility, D3D12ShaderVariableInfo[] Variables);

internal sealed record D3D12ShaderResourceInfo(
    string Name,
    D3D12ShaderResourceKind Kind,
    int Dimension,
    int Register,
    int Space,
    D3D12ShaderVisibility Visibility,
    int VertexDescriptorSlot = -1,
    int PixelDescriptorSlot = -1,
    int ComputeDescriptorSlot = -1);

internal sealed record D3D12VertexInputInfo(string Semantic, int SemanticIndex, int Register);

internal readonly record struct D3D12ThreadGroupSize(int X, int Y, int Z);

internal sealed record D3D12ShaderProgramInfo(
    string Name,
    int VertexShader,
    int PixelShader,
    int ComputeShader,
    D3D12ThreadGroupSize ThreadGroupSize,
    D3D12ConstantBufferInfo[] ConstantBuffers,
    D3D12ShaderResourceInfo[] Resources,
    D3D12VertexInputInfo[] VertexInputs,
    bool UsesDescriptorIndices = false);

internal sealed record D3D12ShaderLibraryFile(byte[][] Shaders, D3D12ShaderProgramInfo[] Programs)
{
    public const string Extension = ".crystalshader";

    private static ReadOnlySpan<byte> Magic => "CRYSSHDR"u8;
    private const int Version = 3;

    public static D3D12ShaderLibraryFile Read(Stream stream)
    {
        using var reader = new BinaryReader(stream, Encoding.UTF8, leaveOpen: true);
        if (!reader.ReadBytes(Magic.Length).AsSpan().SequenceEqual(Magic)) throw new InvalidDataException("The stream is not a Crystal shader library.");
        var version = reader.ReadInt32();
        if (version != Version) throw new InvalidDataException($"The shader library has version {version}, but this Crystal reads version {Version}. Rebuild the shaders.");

        var shaders = new byte[reader.ReadInt32()][];
        for (var index = 0; index < shaders.Length; index++) shaders[index] = reader.ReadBytes(reader.ReadInt32());

        var programs = new D3D12ShaderProgramInfo[reader.ReadInt32()];
        for (var index = 0; index < programs.Length; index++)
        {
            var name = reader.ReadString();
            var vertexShader = reader.ReadInt32();
            var pixelShader = reader.ReadInt32();
            var computeShader = reader.ReadInt32();
            var usesDescriptorIndices = reader.ReadBoolean();
            var threadGroupSize = new D3D12ThreadGroupSize(reader.ReadInt32(), reader.ReadInt32(), reader.ReadInt32());

            var constantBuffers = new D3D12ConstantBufferInfo[reader.ReadInt32()];
            for (var bufferIndex = 0; bufferIndex < constantBuffers.Length; bufferIndex++)
            {
                var bufferName = reader.ReadString();
                var register = reader.ReadInt32();
                var space = reader.ReadInt32();
                var size = reader.ReadInt32();
                var visibility = (D3D12ShaderVisibility)reader.ReadByte();
                var variables = new D3D12ShaderVariableInfo[reader.ReadInt32()];
                for (var variableIndex = 0; variableIndex < variables.Length; variableIndex++)
                {
                    variables[variableIndex] = new D3D12ShaderVariableInfo(
                        reader.ReadString(), reader.ReadInt32(), reader.ReadInt32(), (D3D12ShaderVariableClass)reader.ReadByte(),
                        reader.ReadInt32(), reader.ReadInt32(), reader.ReadInt32());
                }
                constantBuffers[bufferIndex] = new D3D12ConstantBufferInfo(bufferName, register, space, size, visibility, variables);
            }

            var resources = new D3D12ShaderResourceInfo[reader.ReadInt32()];
            for (var resourceIndex = 0; resourceIndex < resources.Length; resourceIndex++)
            {
                resources[resourceIndex] = new D3D12ShaderResourceInfo(
                    reader.ReadString(), (D3D12ShaderResourceKind)reader.ReadByte(), reader.ReadInt32(), reader.ReadInt32(), reader.ReadInt32(),
                    (D3D12ShaderVisibility)reader.ReadByte(), reader.ReadInt32(), reader.ReadInt32(), reader.ReadInt32());
            }

            var vertexInputs = new D3D12VertexInputInfo[reader.ReadInt32()];
            for (var inputIndex = 0; inputIndex < vertexInputs.Length; inputIndex++)
                vertexInputs[inputIndex] = new D3D12VertexInputInfo(reader.ReadString(), reader.ReadInt32(), reader.ReadInt32());

            programs[index] = new D3D12ShaderProgramInfo(name, vertexShader, pixelShader, computeShader, threadGroupSize, constantBuffers, resources, vertexInputs, usesDescriptorIndices);
        }
        return new D3D12ShaderLibraryFile(shaders, programs);
    }

    public void Write(Stream stream)
    {
        using var writer = new BinaryWriter(stream, Encoding.UTF8, leaveOpen: true);
        writer.Write(Magic);
        writer.Write(Version);

        writer.Write(Shaders.Length);
        foreach (var shader in Shaders)
        {
            writer.Write(shader.Length);
            writer.Write(shader);
        }

        writer.Write(Programs.Length);
        foreach (var program in Programs)
        {
            writer.Write(program.Name);
            writer.Write(program.VertexShader);
            writer.Write(program.PixelShader);
            writer.Write(program.ComputeShader);
            writer.Write(program.UsesDescriptorIndices);
            writer.Write(program.ThreadGroupSize.X);
            writer.Write(program.ThreadGroupSize.Y);
            writer.Write(program.ThreadGroupSize.Z);

            writer.Write(program.ConstantBuffers.Length);
            foreach (var buffer in program.ConstantBuffers)
            {
                writer.Write(buffer.Name);
                writer.Write(buffer.Register);
                writer.Write(buffer.Space);
                writer.Write(buffer.Size);
                writer.Write((byte)buffer.Visibility);
                writer.Write(buffer.Variables.Length);
                foreach (var variable in buffer.Variables)
                {
                    writer.Write(variable.Name);
                    writer.Write(variable.Offset);
                    writer.Write(variable.Size);
                    writer.Write((byte)variable.Class);
                    writer.Write(variable.Rows);
                    writer.Write(variable.Columns);
                    writer.Write(variable.Elements);
                }
            }

            writer.Write(program.Resources.Length);
            foreach (var resource in program.Resources)
            {
                writer.Write(resource.Name);
                writer.Write((byte)resource.Kind);
                writer.Write(resource.Dimension);
                writer.Write(resource.Register);
                writer.Write(resource.Space);
                writer.Write((byte)resource.Visibility);
                writer.Write(resource.VertexDescriptorSlot);
                writer.Write(resource.PixelDescriptorSlot);
                writer.Write(resource.ComputeDescriptorSlot);
            }

            writer.Write(program.VertexInputs.Length);
            foreach (var input in program.VertexInputs)
            {
                writer.Write(input.Semantic);
                writer.Write(input.SemanticIndex);
                writer.Write(input.Register);
            }
        }
    }
}
