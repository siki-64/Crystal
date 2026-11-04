using System.Runtime.InteropServices;
using Crystal.Graphics.D3D12;
using TerraFX.Interop.DirectX;

namespace Crystal.Graphics;

public sealed class ShaderConstantBuffer
{
    private const int ConstantBufferAlignment = 256;

    internal ShaderConstantBuffer(ShaderProgram program, D3D12ConstantBufferInfo info, int slot)
    {
        Program = program;
        Name = info.Name;
        Size = info.Size;
        Slot = slot;
        Sync = ShaderProgram.ToSync(info.Visibility);
    }

    public string Name { get; }
    public int Size { get; }
    public ShaderProgram Program { get; }
    public GraphicsBuffer? Buffer => Values.Buffer;
    public long BufferOffset => Values.BufferOffset;

    internal int Slot { get; }
    internal D3D12_BARRIER_SYNC Sync { get; }

    private ShaderConstantBufferValues Values => Program.Values.ConstantBuffers[Slot];

    public void SetValue<T>(in T value) where T : unmanaged => SetData(new ReadOnlySpan<T>(in value));

    public void SetData<T>(ReadOnlySpan<T> data, int offsetInBytes = 0) where T : unmanaged
    {
        var bytes = MemoryMarshal.AsBytes(data);
        if (offsetInBytes < 0 || offsetInBytes + bytes.Length > Size)
            throw new ArgumentOutOfRangeException(nameof(data), $"Constant buffer '{Name}' is {Size} bytes; {bytes.Length} bytes at offset {offsetInBytes} don't fit.");
        Write(Values, offsetInBytes, bytes);
    }

    public void SetBuffer(GraphicsBuffer? buffer, long offsetInBytes = 0)
    {
        if (buffer != null)
        {
            if ((buffer.Usage & BufferUsage.Constant) == 0) throw new ArgumentException($"Buffer '{buffer.Name}' was not created with BufferUsage.Constant.", nameof(buffer));
            if (offsetInBytes < 0 || offsetInBytes % ConstantBufferAlignment != 0)
                throw new ArgumentOutOfRangeException(nameof(offsetInBytes), $"Constant buffer offsets must be multiples of {ConstantBufferAlignment} bytes.");
            if ((ulong)offsetInBytes + (ulong)Size > buffer.AllocatedSize)
                throw new ArgumentException($"Constant buffer '{Name}' needs {Size} bytes from offset {offsetInBytes}, but buffer '{buffer.Name}' is {buffer.Size} bytes.", nameof(buffer));
        }
        var values = Values;
        values.Buffer = buffer;
        values.BufferOffset = offsetInBytes;
    }

    internal static void Write(ShaderConstantBufferValues values, int offset, ReadOnlySpan<byte> bytes)
    {
        bytes.CopyTo(values.Data.AsSpan(offset));
        values.IsDirty = true;
    }
}
