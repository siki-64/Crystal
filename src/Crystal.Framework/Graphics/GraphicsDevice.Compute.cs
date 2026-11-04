using Crystal.Graphics.D3D12;
using TerraFX.Interop.DirectX;
using static TerraFX.Interop.DirectX.D3D12_BARRIER_ACCESS;
using static TerraFX.Interop.DirectX.D3D12_BARRIER_LAYOUT;
using static TerraFX.Interop.DirectX.D3D12_BARRIER_SYNC;

namespace Crystal.Graphics;

public sealed unsafe partial class GraphicsDevice
{
    public const int MaximumThreadGroupCount = 65535;

    private const int IndirectArgumentAlignment = 4;

    public AsyncComputeQueue AsyncCompute { get; }

    internal bool HasUnsubmittedAsyncComputeWork => _computeContext.IsOpen;

    public void Dispatch(ComputePipeline pipeline, int threadGroupCountX, int threadGroupCountY = 1, int threadGroupCountZ = 1)
    {
        CheckThreadGroupCounts(threadGroupCountX, threadGroupCountY, threadGroupCountZ);
        var state = Recording;
        var list = PrepareDispatch(state, state.Context, pipeline);
        list->Dispatch((uint)threadGroupCountX, (uint)threadGroupCountY, (uint)threadGroupCountZ);
    }

    public void DispatchIndirect(ComputePipeline pipeline, GraphicsBuffer arguments, long argumentsOffset = 0)
    {
        CheckIndirectArguments(arguments, argumentsOffset, sizeof(IndirectDispatchArguments), 1);
        var state = Recording;
        var list = PrepareDispatch(state, state.Context, pipeline);
        UseAsIndirectArguments(state.Context, arguments);
        list->ExecuteIndirect(_device.DispatchCommandSignature, 1, arguments.Resource.Pointer, (ulong)argumentsOffset, null, 0);
    }

    public void DrawInstancedIndirect(GraphicsBuffer arguments, long argumentsOffset = 0, int maximumDrawCount = 1, GraphicsBuffer? countBuffer = null, long countOffset = 0)
    {
        CheckIndirectArguments(arguments, argumentsOffset, sizeof(IndirectDrawArguments), maximumDrawCount);
        CheckCountBuffer(countBuffer, countOffset);
        var state = Recording;
        var list = PrepareDraw(state, isIndexed: false);
        ExecuteIndirectDraws(state.Context, list, _device.DrawCommandSignature, arguments, argumentsOffset, maximumDrawCount, countBuffer, countOffset);
    }

    public void DrawIndexedInstancedIndirect(
        GraphicsBuffer arguments, long argumentsOffset = 0, int maximumDrawCount = 1, GraphicsBuffer? countBuffer = null, long countOffset = 0)
    {
        CheckIndirectArguments(arguments, argumentsOffset, sizeof(IndirectDrawIndexedArguments), maximumDrawCount);
        CheckCountBuffer(countBuffer, countOffset);
        var state = Recording;
        var list = PrepareDraw(state, isIndexed: true);
        ExecuteIndirectDraws(state.Context, list, _device.DrawIndexedCommandSignature, arguments, argumentsOffset, maximumDrawCount, countBuffer, countOffset);
    }

    internal void DispatchAsync(ComputePipeline pipeline, int threadGroupCountX, int threadGroupCountY, int threadGroupCountZ)
    {
        CheckThreadGroupCounts(threadGroupCountX, threadGroupCountY, threadGroupCountZ);
        var list = PrepareDispatch(_immediate, _computeContext, pipeline);
        list->Dispatch((uint)threadGroupCountX, (uint)threadGroupCountY, (uint)threadGroupCountZ);
    }

    internal void DispatchIndirectAsync(ComputePipeline pipeline, GraphicsBuffer arguments, long argumentsOffset)
    {
        CheckIndirectArguments(arguments, argumentsOffset, sizeof(IndirectDispatchArguments), 1);
        var list = PrepareDispatch(_immediate, _computeContext, pipeline);
        UseAsIndirectArguments(_computeContext, arguments);
        list->ExecuteIndirect(_device.DispatchCommandSignature, 1, arguments.Resource.Pointer, (ulong)argumentsOffset, null, 0);
    }

    internal void SubmitAsyncCompute()
    {
        EnsureInFrame();
        if (_computeContext.IsOpen) SubmitComputeList();
    }

    private void SubmitComputeList()
    {
        if (_computeContext.RequiresPeerSubmission && _directContext.IsOpen) SubmitDirectList();
        var value = _computeContext.Submit();
        _device.DeferredReleases.AssignComputeSubmission(value);
    }

    private ID3D12GraphicsCommandList7* PrepareDispatch(GraphicsRecordingState state, D3D12CommandContext context, ComputePipeline pipeline)
    {
        EnsureInFrame();
        state.Metrics = state.Metrics with { DispatchCount = state.Metrics.DispatchCount + 1 };
        ArgumentNullException.ThrowIfNull(pipeline);
        if (pipeline.GraphicsDevice != this) throw new ArgumentException("The pipeline belongs to another GraphicsDevice.", nameof(pipeline));
        ObjectDisposedException.ThrowIf(pipeline.IsDisposed, pipeline);
        var program = pipeline.Program;
        var rootSignature = program.RootSignature;
        var pipelineState = pipeline.PipelineState;

        var list = context.Open();
        if (context.AppliedPipelineState != pipelineState)
        {
            list->SetPipelineState(pipelineState);
            state.Metrics = state.Metrics with { PipelineStateChangeCount = state.Metrics.PipelineStateChangeCount + 1 };
            context.AppliedPipelineState = pipelineState;
        }
        if (context.AppliedComputeRootSignature != rootSignature)
        {
            list->SetComputeRootSignature(rootSignature);
            context.AppliedComputeRootSignature = rootSignature;
            Array.Clear(context.AppliedComputeRootArguments);
            state.Metrics = state.Metrics with { RootSignatureChangeCount = state.Metrics.RootSignatureChangeCount + 1 };
        }
        BindConstantBuffers(state, context, program, isCompute: true);
        BindResources(state, context, program, isCompute: true);
        return list;
    }

    private static void ExecuteIndirectDraws(
        D3D12CommandContext context, ID3D12GraphicsCommandList7* list, ID3D12CommandSignature* signature, GraphicsBuffer arguments, long argumentsOffset, int maximumDrawCount,
        GraphicsBuffer? countBuffer, long countOffset)
    {
        UseAsIndirectArguments(context, arguments);
        if (countBuffer != null) UseAsIndirectArguments(context, countBuffer);
        list->ExecuteIndirect(
            signature, (uint)maximumDrawCount, arguments.Resource.Pointer, (ulong)argumentsOffset, countBuffer == null ? null : countBuffer.Resource.Pointer, (ulong)countOffset);
    }

    private static void UseAsIndirectArguments(D3D12CommandContext context, GraphicsBuffer buffer) =>
        context.Use(buffer.Resource, D3D12_BARRIER_SYNC_EXECUTE_INDIRECT, D3D12_BARRIER_ACCESS_INDIRECT_ARGUMENT, D3D12_BARRIER_LAYOUT_UNDEFINED);

    private static void CheckThreadGroupCounts(int threadGroupCountX, int threadGroupCountY, int threadGroupCountZ)
    {
        if (threadGroupCountX is < 0 or > MaximumThreadGroupCount) throw new ArgumentOutOfRangeException(nameof(threadGroupCountX));
        if (threadGroupCountY is < 0 or > MaximumThreadGroupCount) throw new ArgumentOutOfRangeException(nameof(threadGroupCountY));
        if (threadGroupCountZ is < 0 or > MaximumThreadGroupCount) throw new ArgumentOutOfRangeException(nameof(threadGroupCountZ));
    }

    private static void CheckIndirectArguments(GraphicsBuffer arguments, long argumentsOffset, int stride, int maximumCount)
    {
        ArgumentNullException.ThrowIfNull(arguments);
        ObjectDisposedException.ThrowIf(arguments.IsDisposed, arguments);
        ArgumentOutOfRangeException.ThrowIfNegativeOrZero(maximumCount);
        if ((arguments.Usage & BufferUsage.Indirect) == 0) throw new ArgumentException($"Buffer '{arguments.Name}' was not created with BufferUsage.Indirect.", nameof(arguments));
        if (argumentsOffset < 0 || argumentsOffset % IndirectArgumentAlignment != 0 || argumentsOffset + (long)stride * maximumCount > arguments.Size)
            throw new ArgumentOutOfRangeException(nameof(argumentsOffset), $"{maximumCount} argument records of {stride} bytes at offset {argumentsOffset} don't fit buffer '{arguments.Name}' of {arguments.Size} bytes, or the offset isn't a multiple of 4.");
    }

    private static void CheckCountBuffer(GraphicsBuffer? countBuffer, long countOffset)
    {
        if (countBuffer == null) return;
        ObjectDisposedException.ThrowIf(countBuffer.IsDisposed, countBuffer);
        if ((countBuffer.Usage & BufferUsage.Indirect) == 0) throw new ArgumentException($"Buffer '{countBuffer.Name}' was not created with BufferUsage.Indirect.", nameof(countBuffer));
        if (countOffset < 0 || countOffset % IndirectArgumentAlignment != 0 || countOffset + sizeof(uint) > countBuffer.Size)
            throw new ArgumentOutOfRangeException(nameof(countOffset));
    }
}
