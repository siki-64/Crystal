using Crystal.Graphics.D3D12;
using TerraFX.Interop.DirectX;
using static TerraFX.Interop.DirectX.D3D12_BARRIER_ACCESS;
using static TerraFX.Interop.DirectX.D3D12_BARRIER_SYNC;

namespace Crystal.Graphics;

public sealed unsafe partial class GraphicsDevice
{
    private static long s_lastValueGeneration;
    private readonly Stack<int> _freeValueSlots = new();
    private int _nextValueSlot = 1;
    private readonly List<GraphicsCommandList> _executingLists = [];
    private readonly HashSet<D3D12Resource> _executedResources = new(ReferenceEqualityComparer.Instance);
    private readonly List<D3D12CommandList> _closedHandoffs = [];
    private readonly List<D3D12CommandList> _closedRecordings = [];

    public void Execute(GraphicsCommandList commandList)
    {
        ArgumentNullException.ThrowIfNull(commandList);
        Execute(new ReadOnlySpan<GraphicsCommandList>(ref commandList));
    }

    public void Execute(ReadOnlySpan<GraphicsCommandList> commandLists)
    {
        EnsureInFrame();
        EnsureRenderThread("Executing a command list");
        foreach (var commandList in commandLists)
        {
            ArgumentNullException.ThrowIfNull(commandList, nameof(commandLists));
            if (commandList.GraphicsDevice != this) throw new ArgumentException("The command list belongs to another GraphicsDevice.", nameof(commandLists));
            ObjectDisposedException.ThrowIf(commandList.IsDisposed, commandList);
            if (commandList.IsBound) throw new InvalidOperationException($"Command list '{commandList.Name}' is still bound. Dispose its binding before executing it.");
            if (commandList.Recording.Context.IsOpen) Profiler.ThrowIfScopesOpen(commandList.Recording, $"command list '{commandList.Name}' was executed");
        }

        _executingLists.Clear();
        foreach (var commandList in commandLists)
            if (commandList.Recording.Context.IsOpen && !_executingLists.Contains(commandList)) _executingLists.Add(commandList);
        if (_executingLists.Count == 0) return;

        OpenDirectList();
        var directListNumber = _directContext.CommandListNumber;
        foreach (var commandList in _executingLists)
            foreach (var resource in commandList.Recording.Context.DeferredUses.Keys)
                if (resource.Owner != D3D12QueueKind.Direct) _directContext.Use(resource, D3D12_BARRIER_SYNC_NONE, D3D12_BARRIER_ACCESS_NO_ACCESS, resource.Layout);

        Span<nint> following = stackalloc nint[_executingLists.Count * 2];
        var followingCount = 0;
        _executedResources.Clear();
        _closedHandoffs.Clear();
        _closedRecordings.Clear();
        foreach (var commandList in _executingLists)
        {
            var context = commandList.Recording.Context;
            var uses = context.DeferredUses;
            if (followingCount == 0)
            {
                foreach (var (resource, use) in uses) _directContext.Use(resource, use.FirstSync, use.FirstAccess, use.FirstLayout);
            }
            else
            {
                _handoffContext.Open();
                foreach (var (resource, use) in uses)
                {
                    if (_executedResources.Contains(resource) || resource.AccessCommandList == directListNumber) resource.AccessCommandList = _handoffContext.CommandListNumber;
                    _handoffContext.Use(resource, use.FirstSync, use.FirstAccess, use.FirstLayout);
                }
                var handoff = _handoffContext.CloseDeferred();
                _closedHandoffs.Add(handoff);
                following[followingCount++] = (nint)handoff.List;
            }

            var recorded = context.CloseDeferred();
            _closedRecordings.Add(recorded);
            following[followingCount++] = (nint)recorded.List;
            foreach (var (resource, use) in uses)
            {
                resource.Layout = use.Layout;
                resource.Access = use.Access;
                resource.Sync = use.Sync;
                resource.AccessCommandList = context.CommandListNumber;
                _executedResources.Add(resource);
            }
        }

        var value = SubmitDirectList(following[..followingCount]);
        foreach (var handoff in _closedHandoffs) _handoffContext.ReturnDeferred(handoff, value);
        for (var index = 0; index < _executingLists.Count; index++)
        {
            var commandList = _executingLists[index];
            var state = commandList.Recording;
            state.Context.ReturnDeferred(_closedRecordings[index], value);
            state.Context.DeferredUses.Clear();
            _executedMetrics = _executedMetrics.Plus(state.Metrics);
            commandList.LastSubmittedValue = value;
        }
        foreach (var resource in _executedResources)
        {
            resource.Access = D3D12_BARRIER_ACCESS_NO_ACCESS;
            resource.Sync = D3D12_BARRIER_SYNC_NONE;
        }
        _executingLists.Clear();
        _executedResources.Clear();
        _closedHandoffs.Clear();
        _closedRecordings.Clear();
    }

    internal GraphicsRecordingState CreateRecording(GraphicsCommandList commandList)
    {
        var context = new D3D12CommandContext(_device, D3D12QueueKind.Direct, isDeferred: true);
        var state = new GraphicsRecordingState(this, context, new D3D12UploadRing(_device));
        context.Opened = state.ClearBindings;
        state.ValueGeneration = Interlocked.Increment(ref s_lastValueGeneration);
        lock (_commandLists)
        {
            state.ValueSlot = _freeValueSlots.TryPop(out var slot) ? slot : _nextValueSlot++;
            _commandLists.Add(commandList);
        }
        return state;
    }

    internal void DestroyRecording(GraphicsCommandList commandList)
    {
        lock (_commandLists)
        {
            _commandLists.Remove(commandList);
            _freeValueSlots.Push(commandList.Recording.ValueSlot);
        }
        _device.DirectQueue.WaitForValue(commandList.LastSubmittedValue);
        commandList.Recording.UploadRing.Dispose();
        commandList.Recording.Context.Dispose();
    }

    internal void Bind(GraphicsCommandList commandList)
    {
        if (commandList.GraphicsDevice != this) throw new ArgumentException("The command list belongs to another GraphicsDevice.", nameof(commandList));
        EnsureInFrame();
        if (s_boundRecording != null) throw new InvalidOperationException("This thread already has a command list bound. Dispose its binding first.");
        if (!commandList.TryClaim(Environment.CurrentManagedThreadId))
            throw new InvalidOperationException($"Command list '{commandList.Name}' is bound on another thread.");
        var state = commandList.Recording;
        state.Context.Open();
        s_boundRecording = state;
    }

    internal void Unbind(GraphicsCommandList commandList)
    {
        if (s_boundRecording != commandList.Recording) throw new InvalidOperationException($"Command list '{commandList.Name}' is not bound on this thread.");
        s_boundRecording = null;
        commandList.ReleaseClaim();
    }

    public GraphicsCommandListSuspension SuspendCommandList()
    {
        var suspended = BoundRecording;
        if (suspended != null) s_boundRecording = null;
        return new GraphicsCommandListSuspension(this, suspended);
    }

    internal void ResumeCommandList(GraphicsRecordingState suspended)
    {
        if (s_boundRecording != null) throw new InvalidOperationException("A command list was bound while another was suspended. Dispose its binding before the suspension.");
        s_boundRecording = suspended;
    }

    private void ThrowIfCommandListsRecording()
    {
        lock (_commandLists)
        {
            foreach (var commandList in _commandLists)
            {
                if (commandList.IsRecording)
                    throw new InvalidOperationException($"Command list '{commandList.Name}' was recorded but not executed before the frame ended.");
            }
        }
    }

    private void RetireCommandListUploads(ulong directValue, ulong computeValue)
    {
        lock (_commandLists)
        {
            foreach (var commandList in _commandLists) commandList.Recording.UploadRing.Retire(directValue, computeValue);
        }
    }

    private void DisposeCommandLists()
    {
        GraphicsCommandList[] remaining;
        lock (_commandLists) remaining = [.. _commandLists];
        foreach (var commandList in remaining) commandList.Dispose();
    }

}
