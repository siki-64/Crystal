using System.Numerics;
using System.Runtime.InteropServices;
using Crystal.Graphics.D3D12;

namespace Crystal.Graphics;

public enum ShaderParameterKind
{
    Value,
    Texture,
    ReadWriteTexture,
    Buffer,
    ReadWriteBuffer,
    Sampler,
}

public sealed class ShaderParameter
{
    private const int RegisterSize = 16;

    private readonly ShaderValueBinding[] _bindings = [];
    private readonly D3D12ShaderVariableInfo? _variable;
    private readonly D3D12ShaderResourceInfo? _resource;
    private readonly int _resourceIndex;

    internal ShaderParameter(ShaderProgram program, string name, ShaderValueBinding[] bindings)
    {
        Program = program;
        Name = name;
        Kind = ShaderParameterKind.Value;
        _bindings = bindings;
        _variable = bindings[0].Variable;
    }

    internal ShaderParameter(ShaderProgram program, D3D12ShaderResourceInfo resource, int resourceIndex)
    {
        Program = program;
        Name = resource.Name;
        Kind = resource.Kind switch
        {
            D3D12ShaderResourceKind.Texture => ShaderParameterKind.Texture,
            D3D12ShaderResourceKind.ReadWriteTexture => ShaderParameterKind.ReadWriteTexture,
            D3D12ShaderResourceKind.StructuredBuffer or D3D12ShaderResourceKind.ByteAddressBuffer => ShaderParameterKind.Buffer,
            D3D12ShaderResourceKind.Sampler => ShaderParameterKind.Sampler,
            _ => ShaderParameterKind.ReadWriteBuffer,
        };
        _resource = resource;
        _resourceIndex = resourceIndex;
    }

    public string Name { get; }
    public ShaderParameterKind Kind { get; }
    public ShaderProgram Program { get; }
    public ShaderConstantBuffer? ConstantBuffer => _bindings.Length == 0 ? null : _bindings[0].Buffer;
    public int Offset => _variable?.Offset ?? 0;
    public int SizeInBytes => _variable?.Size ?? 0;
    public int Rows => _variable?.Rows ?? 0;
    public int Columns => _variable?.Columns ?? 0;
    public int Elements => _variable?.Elements ?? 0;

    public object? Value => _resource == null ? null : Program.Values.Resources[_resourceIndex];

    public void SetValue(float value) => WriteRaw(MemoryMarshal.AsBytes(new ReadOnlySpan<float>(in value)));

    public void SetValue(int value) => WriteRaw(MemoryMarshal.AsBytes(new ReadOnlySpan<int>(in value)));

    public void SetValue(uint value) => WriteRaw(MemoryMarshal.AsBytes(new ReadOnlySpan<uint>(in value)));

    public void SetValue(bool value) => SetValue(value ? 1u : 0u);

    public void SetValue(Vector2 value) => WriteRaw(MemoryMarshal.AsBytes(new ReadOnlySpan<Vector2>(in value)));

    public void SetValue(Vector3 value) => WriteRaw(MemoryMarshal.AsBytes(new ReadOnlySpan<Vector3>(in value)));

    public void SetValue(Vector4 value) => WriteRaw(MemoryMarshal.AsBytes(new ReadOnlySpan<Vector4>(in value)));

    public void SetValue(Color value) => SetValue(value.ToVector4());

    public void SetValue(in Matrix4x4 value)
    {
        var variable = RequireMatrix();
        WriteMatrix(variable, 0, value);
    }

    public void SetValue(ReadOnlySpan<float> values)
    {
        var variable = RequireValue();
        if (variable.Elements == 0 || variable.Columns == 4)
        {
            WriteRaw(MemoryMarshal.AsBytes(values));
            return;
        }
        if (variable.Class is not (D3D12ShaderVariableClass.Scalar or D3D12ShaderVariableClass.Vector))
            throw new InvalidOperationException($"Parameter '{Name}' is not a scalar or vector array.");
        var columns = variable.Columns;
        if (values.Length > variable.Elements * columns)
            throw new ArgumentException($"Parameter '{Name}' holds {variable.Elements * columns} floats, but {values.Length} were given.", nameof(values));
        for (var element = 0; element * columns < values.Length; element++)
        {
            var chunk = values.Slice(element * columns, Math.Min(columns, values.Length - element * columns));
            Write(element * RegisterSize, MemoryMarshal.AsBytes(chunk));
        }
    }

    public void SetValue(ReadOnlySpan<Vector4> values) => WriteRaw(MemoryMarshal.AsBytes(values));

    public void SetValue(ReadOnlySpan<Matrix4x4> values)
    {
        var variable = RequireMatrix();
        var stride = (variable.Class == D3D12ShaderVariableClass.MatrixRows ? variable.Rows : variable.Columns) * RegisterSize;
        var capacity = Math.Max(variable.Elements, 1);
        if (values.Length > capacity) throw new ArgumentException($"Parameter '{Name}' holds {capacity} matrices, but {values.Length} were given.", nameof(values));
        for (var index = 0; index < values.Length; index++) WriteMatrix(variable, index * stride, values[index]);
    }

    public void SetValue(Texture? texture)
    {
        var resource = RequireResource(ShaderParameterKind.Texture, ShaderParameterKind.ReadWriteTexture);
        if (texture != null)
        {
            var isReadWrite = Kind == ShaderParameterKind.ReadWriteTexture;
            CheckTextureDimension(resource, texture.Name, isReadWrite ? texture.UnorderedAccessDimension : texture.ShaderResourceDimension, nameof(texture));
        }
        Program.Values.Resources[_resourceIndex] = texture;
    }

    public void SetValue(TextureView? view)
    {
        var resource = RequireResource(ShaderParameterKind.Texture, ShaderParameterKind.ReadWriteTexture);
        if (view != null)
        {
            var isReadWrite = Kind == ShaderParameterKind.ReadWriteTexture;
            CheckTextureDimension(resource, view.ToString(), isReadWrite ? view.UnorderedAccessDimension : view.ShaderResourceDimension, nameof(view));
        }
        Program.Values.Resources[_resourceIndex] = view;
    }

    private void CheckTextureDimension(D3D12ShaderResourceInfo resource, string? textureName, int dimension, string argumentName)
    {
        var isReadWrite = Kind == ShaderParameterKind.ReadWriteTexture;
        if (dimension == 0)
            throw new ArgumentException($"Texture '{textureName}' was not created with TextureUsage.{(isReadWrite ? "UnorderedAccess" : "ShaderResource")}.", argumentName);
        if (dimension != resource.Dimension)
            throw new ArgumentException($"Parameter '{Name}' expects a {DimensionName(resource.Dimension)}, but texture '{textureName}' is viewed as a {DimensionName(dimension)}.", argumentName);
    }

    public void SetValue(GraphicsBuffer? buffer)
    {
        var resource = RequireResource(ShaderParameterKind.Buffer, ShaderParameterKind.ReadWriteBuffer);
        if (buffer != null)
        {
            var isStructured = (buffer.Usage & BufferUsage.Structured) != 0;
            var problem = resource.Kind switch
            {
                D3D12ShaderResourceKind.StructuredBuffer when !isStructured => "BufferUsage.Structured",
                D3D12ShaderResourceKind.ByteAddressBuffer when !buffer.IsRaw => "BufferUsage.UnorderedAccess without BufferUsage.Structured",
                D3D12ShaderResourceKind.ReadWriteStructuredBuffer when !isStructured || (buffer.Usage & BufferUsage.UnorderedAccess) == 0
                    => "BufferUsage.Structured and BufferUsage.UnorderedAccess",
                D3D12ShaderResourceKind.ReadWriteByteAddressBuffer when !buffer.IsRaw => "BufferUsage.UnorderedAccess without BufferUsage.Structured",
                _ => null,
            };
            if (problem != null) throw new ArgumentException($"Parameter '{Name}' is a {resource.Kind}, which needs a buffer created with {problem}.", nameof(buffer));
        }
        Program.Values.Resources[_resourceIndex] = buffer;
    }

    public void SetValue(SamplerState? sampler)
    {
        RequireResource(ShaderParameterKind.Sampler, ShaderParameterKind.Sampler);
        Program.Values.Resources[_resourceIndex] = sampler;
    }

    public override string ToString() => Name;

    private void WriteRaw(ReadOnlySpan<byte> bytes)
    {
        var variable = RequireValue();
        if (bytes.Length > variable.Size)
            throw new ArgumentException($"Parameter '{Name}' is {variable.Size} bytes, but {bytes.Length} bytes were given.");
        Write(0, bytes);
    }

    private void Write(int offsetInVariable, ReadOnlySpan<byte> bytes)
    {
        var values = Program.Values;
        foreach (var binding in _bindings) ShaderConstantBuffer.Write(values.ConstantBuffers[binding.Buffer.Slot], binding.Variable.Offset + offsetInVariable, bytes);
    }

    private void WriteMatrix(D3D12ShaderVariableInfo variable, int offsetInVariable, in Matrix4x4 value)
    {
        Span<byte> registers = stackalloc byte[4 * RegisterSize];
        var floats = MemoryMarshal.Cast<byte, float>(registers);
        var isRowMajor = variable.Class == D3D12ShaderVariableClass.MatrixRows;
        var registerCount = isRowMajor ? variable.Rows : variable.Columns;
        for (var row = 0; row < variable.Rows; row++)
        {
            for (var column = 0; column < variable.Columns; column++)
            {
                var index = isRowMajor ? row * 4 + column : column * 4 + row;
                floats[index] = value[row, column];
            }
        }
        var lastRegisterSize = (isRowMajor ? variable.Columns : variable.Rows) * sizeof(float);
        Write(offsetInVariable, registers[..((registerCount - 1) * RegisterSize + lastRegisterSize)]);
    }

    private D3D12ShaderVariableInfo RequireValue() =>
        _variable ?? throw new InvalidOperationException($"Parameter '{Name}' is a {Kind}, not a constant buffer value.");

    private D3D12ShaderVariableInfo RequireMatrix()
    {
        var variable = RequireValue();
        if (variable.Class is not (D3D12ShaderVariableClass.MatrixRows or D3D12ShaderVariableClass.MatrixColumns))
            throw new InvalidOperationException($"Parameter '{Name}' is not a matrix.");
        return variable;
    }

    private D3D12ShaderResourceInfo RequireResource(ShaderParameterKind kind, ShaderParameterKind alternativeKind)
    {
        if (_resource == null || (Kind != kind && Kind != alternativeKind))
            throw new InvalidOperationException($"Parameter '{Name}' is a {Kind}, not a {(kind == alternativeKind ? kind.ToString() : $"{kind} or {alternativeKind}")}.");
        return _resource;
    }

    private static string DimensionName(int dimension) => dimension switch
    {
        1 => "Buffer",
        2 => "Texture1D",
        3 => "Texture1DArray",
        4 => "Texture2D",
        5 => "Texture2DArray",
        6 => "Texture2DMS",
        7 => "Texture2DMSArray",
        8 => "Texture3D",
        9 => "TextureCube",
        10 => "TextureCubeArray",
        _ => $"dimension {dimension}",
    };
}

internal readonly record struct ShaderValueBinding(ShaderConstantBuffer Buffer, D3D12ShaderVariableInfo Variable);
