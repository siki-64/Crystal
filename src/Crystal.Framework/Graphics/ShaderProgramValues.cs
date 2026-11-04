namespace Crystal.Graphics;

internal sealed class ShaderProgramValues
{
    public ShaderProgramValues(long generation, ShaderConstantBufferValues[] constantBuffers, object?[] resources)
    {
        Generation = generation;
        ConstantBuffers = constantBuffers;
        Resources = resources;
    }

    public long Generation { get; }
    public ShaderConstantBufferValues[] ConstantBuffers { get; }
    public object?[] Resources { get; }

    public ShaderProgramValues CopyFor(long generation)
    {
        var constantBuffers = new ShaderConstantBufferValues[ConstantBuffers.Length];
        for (var index = 0; index < constantBuffers.Length; index++) constantBuffers[index] = new ShaderConstantBufferValues(ConstantBuffers[index]);
        return new ShaderProgramValues(generation, constantBuffers, (object?[])Resources.Clone());
    }
}

internal sealed class ShaderConstantBufferValues
{
    public ShaderConstantBufferValues(int size) => Data = new byte[size];

    public ShaderConstantBufferValues(ShaderConstantBufferValues source)
    {
        Data = (byte[])source.Data.Clone();
        Buffer = source.Buffer;
        BufferOffset = source.BufferOffset;
    }

    public byte[] Data { get; }
    public GraphicsBuffer? Buffer { get; set; }
    public long BufferOffset { get; set; }
    public bool IsDirty { get; set; } = true;
    public ulong UploadedAddress { get; set; }
    public ulong UploadedFrame { get; set; } = ulong.MaxValue;
}
