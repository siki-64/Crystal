namespace Crystal.Graphics;

public readonly record struct GraphicsMetrics(
    int DrawCount,
    int DispatchCount,
    int ClearCount,
    int PipelineStateChangeCount,
    int RootSignatureChangeCount = 0,
    int VertexBufferBindCount = 0,
    int IndexBufferBindCount = 0,
    int ConstantBufferBindCount = 0,
    int RepeatedConstantBufferBindCount = 0,
    int DescriptorTableBindCount = 0,
    int RepeatedDescriptorTableBindCount = 0,
    int DescriptorIndexBindCount = 0)
{
    internal GraphicsMetrics Plus(in GraphicsMetrics other) => new(
        DrawCount + other.DrawCount,
        DispatchCount + other.DispatchCount,
        ClearCount + other.ClearCount,
        PipelineStateChangeCount + other.PipelineStateChangeCount,
        RootSignatureChangeCount + other.RootSignatureChangeCount,
        VertexBufferBindCount + other.VertexBufferBindCount,
        IndexBufferBindCount + other.IndexBufferBindCount,
        ConstantBufferBindCount + other.ConstantBufferBindCount,
        RepeatedConstantBufferBindCount + other.RepeatedConstantBufferBindCount,
        DescriptorTableBindCount + other.DescriptorTableBindCount,
        RepeatedDescriptorTableBindCount + other.RepeatedDescriptorTableBindCount,
        DescriptorIndexBindCount + other.DescriptorIndexBindCount);
}
