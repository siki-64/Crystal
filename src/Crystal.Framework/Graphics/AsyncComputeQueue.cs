namespace Crystal.Graphics;

public sealed class AsyncComputeQueue
{
    internal AsyncComputeQueue(GraphicsDevice graphicsDevice) => GraphicsDevice = graphicsDevice;

    public GraphicsDevice GraphicsDevice { get; }
    public bool HasUnsubmittedWork => GraphicsDevice.HasUnsubmittedAsyncComputeWork;

    public void Dispatch(ComputePipeline pipeline, int threadGroupCountX, int threadGroupCountY = 1, int threadGroupCountZ = 1) =>
        GraphicsDevice.DispatchAsync(pipeline, threadGroupCountX, threadGroupCountY, threadGroupCountZ);

    public void DispatchIndirect(ComputePipeline pipeline, GraphicsBuffer arguments, long argumentsOffset = 0) =>
        GraphicsDevice.DispatchIndirectAsync(pipeline, arguments, argumentsOffset);

    public void Submit() => GraphicsDevice.SubmitAsyncCompute();
}
