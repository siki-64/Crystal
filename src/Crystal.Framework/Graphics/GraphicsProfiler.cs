using System.Diagnostics;
using Crystal.Graphics.D3D12;
using TerraFX.Interop.DirectX;
using static Crystal.Graphics.D3D12.D3D12Interop;
using static TerraFX.Interop.DirectX.D3D12_HEAP_TYPE;
using static TerraFX.Interop.DirectX.D3D12_QUERY_HEAP_TYPE;
using static TerraFX.Interop.DirectX.D3D12_QUERY_TYPE;
using static TerraFX.Interop.Windows.Windows;

namespace Crystal.Graphics;

public readonly record struct GraphicsProfilerSample(string Name, int Depth, double GpuMilliseconds, double CpuMilliseconds);

public readonly ref struct GraphicsProfilerScope
{
    private readonly GraphicsProfiler? _profiler;

    internal GraphicsProfilerScope(GraphicsProfiler? profiler) => _profiler = profiler;

    public void Dispose() => _profiler?.End();
}

public sealed unsafe class GraphicsProfiler
{
    public const int MaximumScopesPerFrame = 1024;
    public const int MaximumDepth = 64;
    private const uint FrameBeginQuery = MaximumScopesPerFrame * 2;
    private const uint FrameEndQuery = FrameBeginQuery + 1;
    private const int QueriesPerFrame = MaximumScopesPerFrame * 2 + 2;

    private readonly GraphicsDevice _graphicsDevice;
    private readonly FrameRecording?[] _frames;
    private readonly List<GraphicsProfilerSample> _samples = new(MaximumScopesPerFrame);
    private readonly double _gpuTicksPerMillisecond;
    private FrameRecording? _recording;
    private ulong _previousFrameEndTimestamp;
    private ulong _previousFrameNumber;

    internal GraphicsProfiler(GraphicsDevice graphicsDevice, int framesInFlight)
    {
        _graphicsDevice = graphicsDevice;
        ulong frequency;
        ThrowOnFailure(graphicsDevice.Native.DirectQueue.Queue->GetTimestampFrequency(&frequency), "ID3D12CommandQueue::GetTimestampFrequency");
        _gpuTicksPerMillisecond = frequency / 1000.0;
        _frames = new FrameRecording?[framesInFlight];
    }

    public bool IsEnabled { get; set; }
    public IReadOnlyList<GraphicsProfilerSample> Samples => _samples;
    public ulong SamplesFrameNumber { get; private set; }
    public double FrameGpuMilliseconds { get; private set; }
    public double? GpuMillisecondsSincePreviousFrame { get; private set; }

    public GraphicsProfilerScope Scope(string name)
    {
        Begin(name);
        return new GraphicsProfilerScope(_recording == null ? null : this);
    }

    public void Begin(string name)
    {
        ArgumentNullException.ThrowIfNull(name);
        var state = _graphicsDevice.RecordingForProfiler();
        var recording = _recording;
        if (recording == null) return;
        if (state.OpenProfilerScopeCount == MaximumDepth) throw new InvalidOperationException($"Profiler scopes can be nested at most {MaximumDepth} deep.");

        var index = Interlocked.Increment(ref recording.Count) - 1;
        if (index >= MaximumScopesPerFrame)
        {
            Interlocked.Decrement(ref recording.Count);
            throw new InvalidOperationException($"At most {MaximumScopesPerFrame} profiler scopes can be recorded per frame.");
        }
        recording.Scopes[index] = new ScopeRecord(name, state.OpenProfilerScopeCount, Stopwatch.GetTimestamp(), 0);
        state.OpenProfilerScopes[state.OpenProfilerScopeCount++] = index;
        GraphicsDevice.WriteTimestamp(state, recording.Heap, (uint)index * 2);
    }

    public void End()
    {
        var state = _graphicsDevice.RecordingForProfiler();
        var recording = _recording;
        if (recording == null) return;
        if (state.OpenProfilerScopeCount == 0) throw new InvalidOperationException("GraphicsProfiler.End was called without a matching Begin.");

        var index = state.OpenProfilerScopes[--state.OpenProfilerScopeCount];
        GraphicsDevice.WriteTimestamp(state, recording.Heap, (uint)index * 2 + 1);
        recording.Scopes[index] = recording.Scopes[index] with { CpuEnd = Stopwatch.GetTimestamp() };
    }

    internal void ThrowIfScopesOpen(GraphicsRecordingState state, string ending)
    {
        if (state.OpenProfilerScopeCount == 0) return;
        var name = _recording?.Scopes[state.OpenProfilerScopes[state.OpenProfilerScopeCount - 1]].Name;
        state.OpenProfilerScopeCount = 0;
        throw new InvalidOperationException($"Profiler scope '{name}' was not ended before {ending}.");
    }

    internal void BeginFrame(int frameIndex, ulong frameNumber)
    {
        var frame = _frames[frameIndex];
        if (frame is { IsRecorded: true }) Collect(frame);
        _graphicsDevice.ImmediateRecording.OpenProfilerScopeCount = 0;
        if (!IsEnabled)
        {
            _recording = null;
            return;
        }
        frame ??= _frames[frameIndex] = new FrameRecording(_graphicsDevice.Native, frameIndex);
        frame.Count = 0;
        frame.FrameNumber = frameNumber;
        frame.IsRecorded = true;
        _recording = frame;
        GraphicsDevice.WriteTimestamp(_graphicsDevice.ImmediateRecording, frame.Heap, FrameBeginQuery);
    }

    internal void EndFrame(ID3D12GraphicsCommandList7* list)
    {
        var recording = _recording;
        _recording = null;
        if (recording == null) return;
        var state = _graphicsDevice.ImmediateRecording;
        if (state.OpenProfilerScopeCount != 0)
        {
            var name = recording.Scopes[state.OpenProfilerScopes[state.OpenProfilerScopeCount - 1]].Name;
            recording.Count = 0;
            recording.IsRecorded = false;
            state.OpenProfilerScopeCount = 0;
            throw new InvalidOperationException($"Profiler scope '{name}' was not ended before the frame ended.");
        }
        list->EndQuery(recording.Heap, D3D12_QUERY_TYPE_TIMESTAMP, FrameEndQuery);
        if (recording.Count > 0) list->ResolveQueryData(recording.Heap, D3D12_QUERY_TYPE_TIMESTAMP, 0, (uint)recording.Count * 2, recording.Readback, 0);
        list->ResolveQueryData(recording.Heap, D3D12_QUERY_TYPE_TIMESTAMP, FrameBeginQuery, 2, recording.Readback, FrameBeginQuery * sizeof(ulong));
    }

    internal void Dispose()
    {
        foreach (var frame in _frames) frame?.Dispose();
    }

    private void Collect(FrameRecording frame)
    {
        _samples.Clear();
        var cpuTicksPerMillisecond = Stopwatch.Frequency / 1000.0;
        for (var index = 0; index < frame.Count; index++)
        {
            var scope = frame.Scopes[index];
            var gpuTicks = frame.Timestamps[index * 2 + 1] - frame.Timestamps[index * 2];
            _samples.Add(new GraphicsProfilerSample(scope.Name, scope.Depth, gpuTicks / _gpuTicksPerMillisecond, (scope.CpuEnd - scope.CpuBegin) / cpuTicksPerMillisecond));
        }
        var frameBegin = frame.Timestamps[FrameBeginQuery];
        var frameEnd = frame.Timestamps[FrameEndQuery];
        FrameGpuMilliseconds = (frameEnd - frameBegin) / _gpuTicksPerMillisecond;
        GpuMillisecondsSincePreviousFrame = _previousFrameNumber != 0 && _previousFrameNumber + 1 == frame.FrameNumber ? (frameEnd - _previousFrameEndTimestamp) / _gpuTicksPerMillisecond : null;
        _previousFrameEndTimestamp = frameEnd;
        _previousFrameNumber = frame.FrameNumber;
        SamplesFrameNumber = frame.FrameNumber;
        frame.Count = 0;
        frame.IsRecorded = false;
    }

    private readonly record struct ScopeRecord(string Name, int Depth, long CpuBegin, long CpuEnd);

    private sealed class FrameRecording
    {
        private ID3D12QueryHeap* _heap;
        private ID3D12Resource* _readback;

        public FrameRecording(D3D12Device device, int frameIndex)
        {
            var description = new D3D12_QUERY_HEAP_DESC { Type = D3D12_QUERY_HEAP_TYPE_TIMESTAMP, Count = QueriesPerFrame };
            ID3D12QueryHeap* heap;
            ThrowOnFailure(device.Device->CreateQueryHeap(&description, __uuidof<ID3D12QueryHeap>(), (void**)&heap), "ID3D12Device::CreateQueryHeap");
            SetName((ID3D12Object*)heap, $"ProfilerTimestampHeap{frameIndex}");
            _heap = heap;
            _readback = device.CreateStagingBuffer(D3D12_HEAP_TYPE_READBACK, QueriesPerFrame * sizeof(ulong), $"ProfilerTimestampReadback{frameIndex}");
            ulong* timestamps;
            ThrowOnFailure(_readback->Map(0, null, (void**)&timestamps), "ID3D12Resource::Map");
            Timestamps = timestamps;
        }

        public ID3D12QueryHeap* Heap => _heap;
        public ID3D12Resource* Readback => _readback;
        public ulong* Timestamps { get; }
        public ScopeRecord[] Scopes { get; } = new ScopeRecord[MaximumScopesPerFrame];
        public int Count;
        public ulong FrameNumber { get; set; }
        public bool IsRecorded { get; set; }

        public void Dispose()
        {
            Release(ref _heap);
            Release(ref _readback);
        }
    }
}
