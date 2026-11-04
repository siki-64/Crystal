using Crystal.Graphics.D3D12;
using TerraFX.Interop.DirectX;
using static TerraFX.Interop.DirectX.D3D12_QUERY_TYPE;

namespace Crystal.Graphics;

public sealed unsafe partial class GraphicsDevice
{
    private readonly List<OcclusionQuery> _occlusionQueriesAwaitingSubmission = [];
    private OcclusionQuery? _activeOcclusionQuery;

    public GraphicsProfiler Profiler { get; }

    public VideoMemoryInfo GetVideoMemoryInfo() => _device.QueryVideoMemoryInfo();

    public int AllUploadRingPageCount
    {
        get
        {
            var pages = _uploadRing.PageCount;
            lock (_commandLists)
                foreach (var commandList in _commandLists) pages += commandList.Recording.UploadRing.PageCount;
            return pages;
        }
    }

    public int CopyStagingPageCount => _copyStagingPool.PageCount;

    public int CopyCommandListCount => _device.CopyCommandLists.Count;

    public int CommandListCount
    {
        get
        {
            lock (_commandLists) return _commandLists.Count;
        }
    }

    internal void BeginOcclusionQuery(OcclusionQuery query)
    {
        EnsureInFrame();
        if (Recording != _immediate) throw new InvalidOperationException("Occlusion queries are recorded on the device only, not on a GraphicsCommandList.");
        EnsureRenderThread("Occlusion queries");
        ObjectDisposedException.ThrowIf(query.IsDisposed, query);
        if (_activeOcclusionQuery != null)
            throw new InvalidOperationException($"Occlusion query '{_activeOcclusionQuery.Name}' is still active. End it before beginning another.");
        var list = OpenDirectList();
        var slot = query.Slot;
        list->BeginQuery(slot.Heap, D3D12_QUERY_TYPE_OCCLUSION, slot.HeapIndex);
        query.IsActive = true;
        _activeOcclusionQuery = query;
    }

    internal void EndOcclusionQuery(OcclusionQuery query)
    {
        EnsureInFrame();
        ObjectDisposedException.ThrowIf(query.IsDisposed, query);
        if (_activeOcclusionQuery != query) throw new InvalidOperationException($"Occlusion query '{query.Name}' is not active.");
        var list = _directContext.List;
        var slot = query.Slot;
        list->EndQuery(slot.Heap, D3D12_QUERY_TYPE_OCCLUSION, slot.HeapIndex);
        list->ResolveQueryData(slot.Heap, D3D12_QUERY_TYPE_OCCLUSION, slot.HeapIndex, 1, slot.Readback, slot.HeapIndex * sizeof(ulong));
        query.IsActive = false;
        query.FenceValue = D3D12ReleaseValues.AwaitingSubmission;
        _activeOcclusionQuery = null;
        _occlusionQueriesAwaitingSubmission.Add(query);
    }

    internal void FreeOcclusionQuery(OcclusionQuery query)
    {
        if (_activeOcclusionQuery == query) _activeOcclusionQuery = null;
        _device.DeferredReleases.Free(_device.OcclusionQueries, query.Slot.Index, new D3D12ReleaseValues(D3D12ReleaseValues.AwaitingSubmission, 0, 0));
    }

    internal GraphicsRecordingState RecordingForProfiler()
    {
        EnsureInFrame();
        return Recording;
    }

    internal static void WriteTimestamp(GraphicsRecordingState state, ID3D12QueryHeap* heap, uint index) => state.Context.Open()->EndQuery(heap, D3D12_QUERY_TYPE_TIMESTAMP, index);

    private void ThrowIfOcclusionQueryActive()
    {
        if (_activeOcclusionQuery != null)
            throw new InvalidOperationException(
                $"Occlusion query '{_activeOcclusionQuery.Name}' is active. End it before the graphics queue submits, for example before reading back data or ending the frame.");
    }

    private void AssignOcclusionQuerySubmission(ulong value)
    {
        foreach (var query in _occlusionQueriesAwaitingSubmission)
        {
            if (query.FenceValue == D3D12ReleaseValues.AwaitingSubmission) query.FenceValue = value;
        }
        _occlusionQueriesAwaitingSubmission.Clear();
    }
}
