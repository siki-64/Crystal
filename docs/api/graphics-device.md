# Crystal.Graphics: device

The device, its options, and everything that reports back from the GPU: readback, debug messages, queries, profiling and metrics.

## GraphicsDevice
`public sealed class GraphicsDevice : IDisposable`

Created by `Game`; there is no public constructor. Unless a row says otherwise, members run on the render thread. Members marked *frame* throw `InvalidOperationException` ("Drawing is only allowed inside Game.Draw") outside a frame.

### Constants and information
| Member | Meaning |
|---|---|
| `const int MaximumRenderTargets = 8` | Color targets per `SetRenderTargets` and per pipeline. |
| `const int MaximumThreadGroupCount = 65535` | Thread groups per dimension of one dispatch. |
| `GraphicsDeviceOptions Options { get; }` | The options after the `CRYSTAL_DX12_*` environment variables were applied. |
| `GraphicsAdapterInfo Adapter { get; }` | The adapter in use. |
| `static GraphicsAdapterInfo ProbeAdapter(GraphicsDeviceOptions options)` | Creates a device with the options and releases it again, without a window or swap chain. Returns the adapter a `Game` would get, or throws `GraphicsException` when no adapter can create a device. Any thread. |
| `bool IsDebugLayerEnabled { get; }` | `true` when the D3D12 debug layer is on. |
| `int BackBufferWidth { get; }`, `int BackBufferHeight { get; }` | Back buffer size in pixels. It follows the window's client area, resized at the start of the frame after the window changed. |
| `SurfaceFormat BackBufferFormat { get; }` | Always `Rgba8Unorm`. |
| `bool VSync { get; set; }` | Waits for vertical blank when presenting. Starts as `Options.VSync`. |
| `AsyncComputeQueue AsyncCompute { get; }` | The asynchronous compute queue. |
| `GraphicsProfiler Profiler { get; }` | GPU and CPU timing scopes. |
| `GraphicsMetrics Metrics { get; }` | Counts for the current frame so far, including executed command lists. On a thread with a bound list, the list's counts. |
| `VideoMemoryInfo GetVideoMemoryInfo()` | Budgets and usage of the adapter's memory. Any thread. |
| `event EventHandler<GraphicsDebugMessage>? DebugMessageReceived` | Debug layer messages, raised on the render thread at the end of each frame. Without subscribers the messages go to stderr. |
| `void Dispose()` | Waits for the GPU, disposes every resource still alive, and releases the device. `Game.Dispose` calls it. |

### Targets and fixed-function state
| Member | Meaning |
|---|---|
| `void SetRenderTarget(Texture2D? colorTarget, Texture2D? depthTarget = null)` | *Frame.* Binds one color target and an optional depth target. `null` for both binds the back buffer. |
| `void SetRenderTargets(ReadOnlySpan<Texture2D> colorTargets, Texture2D? depthTarget = null)` | *Frame.* Binds up to 8 color targets, or none for depth-only passes. Throws `ArgumentException` when a texture lacks `TextureUsage.RenderTarget` or `DepthStencil`, or when the targets differ in size or sample count. |
| `Viewport Viewport { get; set; }` | The viewport for following draws. Binding targets resets it to the full target. |
| `Rectangle ScissorRectangle { get; set; }` | The scissor rectangle for following draws. Binding targets resets it to the full target. |
| `int StencilReference { get; set; }` | Stencil reference value. Starts at 0. |
| `Vector4 BlendFactor { get; set; }` | Constant for `Blend.BlendFactor`. Starts at (1, 1, 1, 1). |
| `void Clear(Color color)`, `void Clear(Vector4 color)` | *Frame.* Clears every bound color target. |
| `void ClearDepthStencil(float depth = 1f, byte stencil = 0)` | *Frame.* Clears the bound depth target. Throws `InvalidOperationException` when none is bound. |

The back buffer is bound when a frame begins.

### Drawing
| Member | Meaning |
|---|---|
| `void SetPipeline(GraphicsPipeline pipeline)` | Sets the pipeline for following draws. Throws `ArgumentException` for a pipeline of another device. |
| `void SetVertexBuffer(GraphicsBuffer? buffer, int slot = 0, long offsetInBytes = 0)` | Binds a buffer with `BufferUsage.Vertex` to a slot from 0 to 15, using the buffer's stride. `null` unbinds. |
| `void SetIndexBuffer(GraphicsBuffer? buffer, long offsetInBytes = 0)` | Binds a buffer with `BufferUsage.Index`. Its stride (2 or 4) is the index format. The offset must be a multiple of the stride. |
| `void SetVertexData<T>(ReadOnlySpan<T> vertices, int slot = 0) where T : unmanaged` | *Frame.* Copies the vertices into the per-frame upload ring and binds them with a stride of `sizeof(T)`. Stays bound until replaced or the frame ends. |
| `void SetIndexData(ReadOnlySpan<ushort> indices)`, `void SetIndexData(ReadOnlySpan<uint> indices)` | *Frame.* The same for 16-bit or 32-bit indices. |
| `void Draw(int vertexCount, int startVertex = 0)` | *Frame.* Non-indexed draw. |
| `void DrawInstanced(int vertexCountPerInstance, int instanceCount, int startVertex = 0, int startInstance = 0)` | *Frame.* Instanced non-indexed draw. |
| `void DrawIndexed(int indexCount, int startIndex = 0, int baseVertex = 0)` | *Frame.* Indexed draw. |
| `void DrawIndexedInstanced(int indexCountPerInstance, int instanceCount, int startIndex = 0, int baseVertex = 0, int startInstance = 0)` | *Frame.* Instanced indexed draw. |
| `void DrawInstancedIndirect(GraphicsBuffer arguments, long argumentsOffset = 0, int maximumDrawCount = 1, GraphicsBuffer? countBuffer = null, long countOffset = 0)` | *Frame.* Reads up to `maximumDrawCount` `IndirectDrawArguments` from `arguments`. With a `countBuffer`, the `uint` at `countOffset` limits the count. Both buffers need `BufferUsage.Indirect`, and offsets must be multiples of 4. |
| `void DrawIndexedInstancedIndirect(...)` | *Frame.* The same with `IndirectDrawIndexedArguments`. Same parameters. |

**Draws throw** `InvalidOperationException` when no pipeline is set, when the pipeline reads a vertex slot that has nothing bound, when an indexed draw has no index buffer, when a program parameter that needs a texture or buffer has no value, when a texture is bound to a parameter and as a target at once, or when the bound target formats or sample count don't match the pipeline. Draws allocate no managed memory.

### Compute
| Member | Meaning |
|---|---|
| `void Dispatch(ComputePipeline pipeline, int threadGroupCountX, int threadGroupCountY = 1, int threadGroupCountZ = 1)` | *Frame.* Runs the compute program on the graphics queue. Each count is 0 to `MaximumThreadGroupCount`. |
| `void DispatchIndirect(ComputePipeline pipeline, GraphicsBuffer arguments, long argumentsOffset = 0)` | *Frame.* Reads one `IndirectDispatchArguments` from a buffer with `BufferUsage.Indirect`, at a 4-byte aligned offset. |

### Copies and readback
| Member | Meaning |
|---|---|
| `void CopyBuffer(GraphicsBuffer source, long sourceOffset, GraphicsBuffer destination, long destinationOffset, long size)` | Copies a byte range on the GPU. Source and destination must differ and the ranges must fit. |
| `void CopyTexture(Texture source, Texture destination)` | Copies all subresources. Both textures need the same size, mip levels, array layers, sample count and format. |
| `void Resolve(Texture2D source, Texture2D destination)` | Resolves a multisampled color texture into a single-sampled one with the same size, array layers and format. Depth formats can't be resolved. |
| `void GetBackBufferData<T>(Span<T> destination) where T : unmanaged` | *Frame.* Copies what has been drawn into the back buffer so far and waits for it. The destination is exactly `BackBufferWidth * BackBufferHeight * 4` bytes of `Rgba8Unorm` rows. |
| `GraphicsReadback BeginGetBackBufferData()` | *Frame.* The same without waiting. The readback completes with the frame. |
| `SamplerState GetSamplerState(in SamplerDescription description)` | Returns the shared sampler for this description, creating it on first use. Any thread. |

Copies, resolves and readback are recorded in order with the frame's draws. Outside a frame they are recorded too and go to the GPU with the next frame, or sooner when a readback waits.

### Command lists
| Member | Meaning |
|---|---|
| `void Execute(GraphicsCommandList commandList)` | *Frame.* Sends what the list recorded to the GPU, after everything the device and earlier lists recorded. Does nothing when the list recorded nothing. Throws `InvalidOperationException` while the list is bound, or when a profiler scope opened on it is still open. |
| `void Execute(ReadOnlySpan<GraphicsCommandList> commandLists)` | *Frame.* Sends the lists in span order, together with what the device recorded before, as one GPU submission. The barriers between them come from what each list used, so lists that hand resources to each other need no flush in between. Prefer it over one `Execute` per list: every submission drains the GPU. Lists that recorded nothing are skipped. Throws like `Execute(GraphicsCommandList)` for any list in the span. |
| `GraphicsCommandListSuspension SuspendCommandList()` | *Any thread.* Until the returned value is disposed, the calling thread works as if no list were bound: parameter values it sets are the ones lists start from. Use it to set up a new program on a worker thread. Does nothing when no list is bound. Disposing it throws `InvalidOperationException` when the thread bound a list in the meantime and still has it bound. |

## GraphicsCommandList
`public sealed class GraphicsCommandList : IDisposable`

Records draws on another thread. While a list is bound on a thread, the `GraphicsDevice` members that record (targets, fixed-function state, drawing, compute, copies, profiler scopes) record into the list on that thread, with state of their own. The device and other threads are not affected. Lists run on the graphics queue in the order `Execute` is called.

| Member | Meaning |
|---|---|
| `GraphicsCommandList(GraphicsDevice graphicsDevice, string name)` | Any thread. |
| `GraphicsDevice GraphicsDevice { get; }`, `string Name { get; }`, `bool IsDisposed { get; }` | |
| `bool IsRecording { get; }` | `true` from the first `Bind` of a frame until `Execute`. |
| `GraphicsMetrics Metrics { get; }` | Counts of what the list recorded since recording began. `GraphicsDevice.Metrics` adds them in when the list is executed. |
| `GraphicsCommandListBinding Bind()` | *Frame.* Binds the list on the calling thread until the binding is disposed. The first bind after `Execute` starts a new recording with nothing bound: set targets, viewport and pipeline again. Binding again later continues the recording. Throws `InvalidOperationException` when the thread already has a list bound or the list is bound on another thread. |
| `void Dispose()` | Waits until the GPU has finished the list's last execution. Throws `InvalidOperationException` while bound. |

- **Barriers**: each list tracks the resources it uses. `Execute` puts every resource into the layout, access and sync its first use in the list expects, waiting on what the device or an earlier list in the same call did with it, and afterwards in the layout the list left it in.
- **Uploads**: `SetData` while a list is bound records the copy into the list, from the list's own upload ring.
- **Not on a list**: occlusion queries, readback and async compute. They throw on other threads, or record on the device.
- **Parameters**: each list keeps its own values for every program, so lists on different threads can set and draw the same program. The first time a list uses a program, it starts from the values set outside any list; later changes outside lists don't reach it. Changing values outside lists while a list first uses the program is a data race.
- A list still recording when the frame ends throws `InvalidOperationException`.

## GraphicsCommandListBinding
`public readonly ref struct GraphicsCommandListBinding`

Returned by `GraphicsCommandList.Bind`. Disposing it unbinds the list from the thread. Use with `using`.

## GraphicsCommandListSuspension
`public readonly ref struct GraphicsCommandListSuspension`

Returned by `GraphicsDevice.SuspendCommandList`. Disposing it binds the suspended list again. Use with `using`.

## GraphicsDeviceOptions
`public sealed record GraphicsDeviceOptions`

| Property | Default | Meaning |
|---|---|---|
| `bool VSync { get; init; }` | `true` | Initial value of `GraphicsDevice.VSync`. |
| `int MaximumFrameLatency { get; init; }` | `2` | Frames the CPU may run ahead of the GPU. |
| `GraphicsAdapterPreference AdapterPreference { get; init; }` | `HighPerformance` | Which adapter to pick. `CRYSTAL_DX12_WARP=1` forces `Software`. |
| `bool EnableDebugLayer { get; init; }` | `false` | Turns on the D3D12 debug layer and DRED. `CRYSTAL_DX12_DEBUG=1` turns it on too. |
| `bool EnableGpuBasedValidation { get; init; }` | `false` | Turns on GPU-based validation and the debug layer. `CRYSTAL_DX12_GBV=1` turns it on too. |
| `bool ThrowOnDebugLayerErrors { get; init; }` | `true` | Throws `GraphicsDebugException` at the end of a frame that produced debug layer errors. |
| `int ShaderResourceDescriptorCapacity { get; init; }` | `1_000_000` | Size of the shader-visible descriptor heap. Running out throws `GraphicsException`. |

Environment variables accept `1` or `true`.

## GraphicsAdapterPreference
`public enum GraphicsAdapterPreference { HighPerformance = 0, MinimumPower = 1, Software = 2 }`

`Software` is WARP. Adapters without feature level 12_0, enhanced barriers, shader model 6.6 and resource binding tier 3 are skipped.

## GraphicsAdapterInfo
`public sealed record GraphicsAdapterInfo(string Description, uint VendorId, uint DeviceId, ulong DedicatedVideoMemory, bool IsSoftware)`

The adapter's name, PCI vendor and device IDs, dedicated video memory in bytes, and whether it is a software adapter.

## VideoMemoryInfo
`public readonly record struct VideoMemoryInfo(long LocalBudget, long LocalUsage, long NonLocalBudget, long NonLocalUsage)`

Bytes. Local is the adapter's own memory, non-local is system memory the adapter uses.

## GraphicsMetrics
`public readonly record struct GraphicsMetrics(int DrawCount, int DispatchCount, int ClearCount, int PipelineStateChangeCount, int RootSignatureChangeCount = 0, int VertexBufferBindCount = 0, int IndexBufferBindCount = 0, int ConstantBufferBindCount = 0, int RepeatedConstantBufferBindCount = 0, int DescriptorTableBindCount = 0, int RepeatedDescriptorTableBindCount = 0, int DescriptorIndexBindCount = 0)`

Counts recorded in the current frame so far. They reset when the next frame begins, so `EndDraw` sees the whole frame.

## GraphicsReadback
`public sealed class GraphicsReadback : GraphicsResource`

Returned by `BeginGetData` and `BeginGetBackBufferData`. Dispose it when done.

| Member | Meaning |
|---|---|
| `long SizeInBytes { get; }` | Bytes `GetData` returns. |
| `bool IsComplete { get; }` | `true` once the GPU has finished the copy. Polls without blocking. |
| `void Wait()` | Blocks until the copy is done. Submits the open frame's work first when the copy hasn't been submitted yet. Render thread. |
| `void GetData<T>(Span<T> destination) where T : unmanaged` | Waits, then copies tightly packed rows. The destination must be exactly `SizeInBytes` bytes. |

## AsyncComputeQueue
`public sealed class AsyncComputeQueue`

Records compute work on a dedicated compute queue. Render thread, inside a frame.

| Member | Meaning |
|---|---|
| `GraphicsDevice GraphicsDevice { get; }` | The owning device. |
| `bool HasUnsubmittedWork { get; }` | `true` when work was recorded since the last `Submit`. |
| `void Dispatch(ComputePipeline pipeline, int threadGroupCountX, int threadGroupCountY = 1, int threadGroupCountZ = 1)` | As `GraphicsDevice.Dispatch`, on the compute queue. |
| `void DispatchIndirect(ComputePipeline pipeline, GraphicsBuffer arguments, long argumentsOffset = 0)` | As `GraphicsDevice.DispatchIndirect`, on the compute queue. |
| `void Submit()` | Sends the recorded work. The end of the frame submits anything still open. |

Each queue waits for the other only on resources they share. Using a resource on the graphics queue while unsubmitted async compute work uses it throws `InvalidOperationException`.

## OcclusionQuery
`public sealed class OcclusionQuery : GraphicsResource`

Counts the samples that pass the depth and stencil tests between `Begin` and `End`.

| Member | Meaning |
|---|---|
| `OcclusionQuery(GraphicsDevice graphicsDevice, string? name = null)` | Any thread. |
| `void Begin()` | *Frame.* Throws `InvalidOperationException` when another query is active. |
| `void End()` | *Frame.* Throws `InvalidOperationException` when this query is not active. |
| `bool IsActive { get; }` | `true` between `Begin` and `End`. |
| `bool IsComplete { get; }` | `true` once the GPU has finished the last `Begin`/`End` pair. |
| `long PixelCount { get; }` | The sample count. Throws `InvalidOperationException` until `IsComplete`. |

A query must end before the frame ends and before any readback.

## GraphicsProfiler
`public sealed class GraphicsProfiler`

Times nested scopes on the GPU and CPU. Inside a frame, on the render thread or on a thread with a bound `GraphicsCommandList`. Scopes nest per thread.

| Member | Meaning |
|---|---|
| `const int MaximumScopesPerFrame = 1024` | Scopes per frame. One more throws `InvalidOperationException`. |
| `const int MaximumDepth = 64` | Nesting depth. Deeper throws `InvalidOperationException`. |
| `bool IsEnabled { get; set; }` | Takes effect when the next frame begins. While off, scopes record nothing and cost nothing. |
| `GraphicsProfilerScope Scope(string name)` | Calls `Begin`, and `End` when the returned scope is disposed. Use with `using`. |
| `void Begin(string name)` | Opens a scope. |
| `void End()` | Closes the innermost scope. Throws `InvalidOperationException` without a matching `Begin`. |
| `IReadOnlyList<GraphicsProfilerSample> Samples { get; }` | The scopes of the latest finished frame, in begin order. A frame's samples arrive once the GPU has finished it, a few frames later. |
| `ulong SamplesFrameNumber { get; }` | The frame that `Samples` belongs to. |
| `double FrameGpuMilliseconds { get; }` | GPU time of that frame on the graphics queue, from its first command to its last before present. Async compute work is not included. |
| `double? GpuMillisecondsSincePreviousFrame { get; }` | GPU time from the end of the frame before to the end of that frame, including idle time and work outside the frame. `null` when the frame before was not profiled. |

A scope still open when the frame ends throws `InvalidOperationException`. Scopes allocate nothing.

## GraphicsProfilerScope
`public readonly ref struct GraphicsProfilerScope`

| Member | Meaning |
|---|---|
| `void Dispose()` | Ends the scope. |

## GraphicsProfilerSample
`public readonly record struct GraphicsProfilerSample(string Name, int Depth, double GpuMilliseconds, double CpuMilliseconds)`

One scope: its name, nesting depth (0 at the top), and the GPU and CPU time between `Begin` and `End`.

## Exceptions and debug messages

### GraphicsException
`public sealed class GraphicsException(string message) : Exception`

A D3D12 or DXGI call failed: no usable adapter, a removed device, a full descriptor heap, a pipeline that the driver rejected. Turn on the debug layer for details.

### GraphicsDebugException
`public sealed class GraphicsDebugException : Exception`

| Member | Meaning |
|---|---|
| `GraphicsDebugException(IReadOnlyList<GraphicsDebugMessage> errors)` | |
| `IReadOnlyList<GraphicsDebugMessage> Errors { get; }` | The error and corruption messages of the frame. |

Thrown at the end of a frame when the debug layer reported errors and `ThrowOnDebugLayerErrors` is on.

### GraphicsDebugMessage
`public sealed record GraphicsDebugMessage(GraphicsDebugSeverity Severity, int Id, string Text)`

| Member | Meaning |
|---|---|
| `GraphicsDebugSeverity Severity` | The message severity. |
| `int Id` | The D3D12 message ID. |
| `string Text` | The message text. |
| `bool IsError { get; }` | `true` for `Corruption` and `Error`. |

### GraphicsDebugSeverity
`public enum GraphicsDebugSeverity { Corruption = 0, Error = 1, Warning = 2, Info = 3, Message = 4 }`
