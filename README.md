# Crystal

Crystal is an XNA-inspired C# game framework for Windows with a Direct3D 12 backend written in C# on [TerraFX.Interop.Windows](https://github.com/terrafx/terrafx.interop.windows).

## Requirements
- Windows 10 21H1 or later, x64
- .NET 10 SDK
- A GPU with Direct3D 12 feature level 12_0 and a driver that supports enhanced barriers, shader model 6.6 and resource binding tier 3, or WARP. Crystal uses enhanced barriers and descriptor indexing only and skips adapters without them.

## Getting started
```csharp
using Crystal;

using var game = new MyGame();
game.Run();

sealed class MyGame() : Game(new GameOptions { Title = "My game", Width = 1280, Height = 720 })
{
    protected override void Draw(GameTime gameTime) => GraphicsDevice.Clear(Color.CornflowerBlue);
}
```

`samples/HelloWorld` is a complete example: a spinning shape; Space switches between a triangle and a square. The [API reference](docs/api/README.md) describes every public type and member.

## Game and window
- **Frame hooks**: `BeginDraw` runs before `Draw` and `EndDraw` after it. Return `false` from `BeginDraw` to skip drawing for that tick. Overrides must call `base.BeginDraw` (which begins the frame) and `base.EndDraw` (which presents it), or the game loop throws.
- **Window position**: `Window.Position` gets or sets the top-left corner of the window in desktop pixels. `Window.Display` is the display the window is on.
- **Displays**: `Display.GetAll()` lists the connected displays with their `Name`, `Bounds`, `WorkArea` (the bounds without the taskbar), `IsPrimary` and `ContentScale`. It works before a `Game` exists.
- **Text input**: set `Window.IsTextInputEnabled` to receive typed characters through the `Window.TextInput` event. Each event carries one UTF-16 `Character`, so characters outside the Basic Multilingual Plane arrive as two surrogate events. Turning text input on may show an on-screen keyboard or an input method window, so leave it off while the game only reads keys.

```csharp
protected override void Initialize()
{
    if (Display.GetAll().FirstOrDefault(display => !display.IsPrimary) is { } secondary)
        Window.Position = new Point(secondary.WorkArea.X + 100, secondary.WorkArea.Y + 100);
}
```

## Math
Crystal uses `System.Numerics` for vectors, matrices and planes, and adds the bounding volumes.

- **`BoundingBox`, `BoundingSphere` and `Ray`**: `CreateFromPoints` takes a span, `CreateMerged` combines two volumes and `Transform` applies a `Matrix4x4`. `Contains` returns a `ContainmentType`, `Intersects` with a `Plane` returns a `PlaneIntersectionType`, and `Intersects` with a `Ray` returns the distance to the hit or `null`.
- **`BoundingFrustum`**: built from a view-projection matrix with a 0 to 1 depth range, as Direct3D and `Matrix4x4.CreatePerspectiveFieldOfView` use. `Near`, `Far`, `Left`, `Right`, `Top` and `Bottom` are normalized planes whose normals point out of the frustum. `GetPlanes` and `GetCorners` fill spans. `Contains` and `Intersects` test points, boxes and spheres plane by plane, so a box near a corner of the frustum can report `Intersects` while lying just outside.
- **No allocations**: all of them are structs, and none of their methods allocate.

## Resources
One buffer type and two texture types cover every resource. A usage flag says what each one is for.

```csharp
var vertices = new GraphicsBuffer(GraphicsDevice, new BufferDescription { Size = 1024 * 32, Stride = 32, Usage = BufferUsage.Vertex });
vertices.SetData<VertexPositionUv>(data);

var albedo = new Texture2D(GraphicsDevice, new TextureDescription { Width = 512, Height = 512, MipLevels = 0, Format = SurfaceFormat.Bc7UnormSrgb });
albedo.SetData<byte>(mip0, mipLevel: 0);

var scene = new Texture2D(GraphicsDevice, new TextureDescription
{
    Width = 1280,
    Height = 720,
    Format = SurfaceFormat.Rgba16Float,
    SampleCount = 4,
    Usage = TextureUsage.RenderTarget,
});
var depth = new Texture2D(GraphicsDevice, new TextureDescription
{
    Width = 1280,
    Height = 720,
    Format = SurfaceFormat.Depth32Float,
    SampleCount = 4,
    Usage = TextureUsage.DepthStencil | TextureUsage.ShaderResource,
});
```

- **`GraphicsBuffer`**: `BufferUsage` is any combination of `Vertex`, `Index` (stride 2 or 4), `Constant` (up to 64 KiB), `Structured` (needs a stride), `Indirect` and `UnorderedAccess`.
- **`Texture2D` and `TextureCube`**: `TextureUsage` is any combination of `ShaderResource`, `RenderTarget`, `DepthStencil` and `UnorderedAccess`.
  - `MipLevels = 0` builds the full mip chain. `ArraySize` makes texture arrays and cube arrays.
  - `SurfaceFormat` covers the common color formats, BC1–BC7 with their sRGB variants, and four depth formats.
  - Depth textures with `ShaderResource` can be sampled in shaders.
  - A texture bound to a parameter shows all its mips to a `Texture2D` and mip 0 to a `RWTexture2D`. `GetMipView(mipLevel)` returns a `TextureView` of that one mip, for both kinds, which `ShaderParameter.SetValue` takes like a texture. Views are created on first use and live as long as the texture, so get them at load time. Two views of one texture can be bound together when both are read-write, as when reducing a mip chain. A read-only view and a read-write view of the same texture in one draw or dispatch aren't supported, because barriers cover the whole texture.
- **Clear values**: `TextureDescription.ClearColor`, `ClearDepth` and `ClearStencil` set the optimized clear value. Clearing to that value is fastest.
- **Writing and reading data**:
  - `SetData` takes tightly packed rows. For BC formats a row is one row of 4×4 blocks. `GetMipLevelSizeInBytes` gives the expected size.
  - `Texture2D.SetData` with a `Rectangle` writes only that region of a mip level. BC regions must be aligned to 4×4 blocks.
  - `GetData` blocks until the GPU has copied the data. `BeginGetData` returns a `GraphicsReadback` that completes with the frame. Poll `IsComplete` or call `Wait`, then call `GetData` on it.
  - `GraphicsDevice.GetBackBufferData` and `BeginGetBackBufferData` read the back buffer the same way, as `Rgba8Unorm` rows of `BackBufferWidth` pixels. Call them inside `Draw`, after the frame is drawn; they copy what has been drawn so far.
- **Render targets and clears**: `GraphicsDevice.SetRenderTarget` and `SetRenderTargets` bind up to 8 color targets and a depth target. `null` binds the back buffer again. `Clear` and `ClearDepthStencil` clear the bound targets.
- **Copies and resolves**: `CopyBuffer`, `CopyTexture` and `Resolve` copy and resolve on the GPU. Multisampled textures must be resolved before they can be read back.
- **Samplers**: `GraphicsDevice.GetSamplerState(SamplerDescription)` returns one shared sampler per description.
- **Pipeline state descriptions**: `BlendState`, `RasterizerState` and `DepthStencilState` follow D3D12's fields: per-target blend, cull mode with front-face winding, depth bias with slope scale and clamp, and two-sided stencil. `GraphicsPipelineDescription` takes them.

### Threading and lifetime
- **Any thread**: create resources and call `SetData` from any thread. Until the GPU first uses a resource, uploads run on a background copy queue. The frame waits on that queue only when it uses the resource.
- **Render thread**: drawing, copies, readback and `SetData` on a resource the GPU has already used must run on the thread that created the `GraphicsDevice`. Those uploads run on the direct queue, in order with the frame.
- **Recording on other threads**: a thread that binds a `GraphicsCommandList` draws through the same `GraphicsDevice` members into that list, with state of its own, including its own shader parameter values and uploads. The render thread calls `GraphicsDevice.Execute` on finished lists, which run in that order after what was recorded before. Passing several lists in one `Execute` sends them as one submission. `SuspendCommandList` lets a worker set the values lists start from, for example on a program it just created.

```csharp
using var shadows = new GraphicsCommandList(GraphicsDevice, "Shadows");
var recorded = Task.Run(() =>
{
    using var binding = shadows.Bind();
    DrawShadows();
});
DrawNormals();
recorded.Wait();
GraphicsDevice.Execute(shadows);
```
- **Barriers**: enhanced barriers are tracked per resource. You never write a barrier.
- **Lifetime**: `Dispose` returns immediately. The device releases the D3D12 resource and its descriptors once the GPU has finished with them, and disposes any resources still alive when it is disposed itself.
- **Descriptors**: every shader resource, unordered access and constant buffer view lives in one shader-visible heap that is allocated up front. Its size is set by `GraphicsDeviceOptions.ShaderResourceDescriptorCapacity` (default 1,000,000).

## Shaders and drawing
Shaders are plain `.hlsl` files. A `#pragma program` line names a program and its entry points. `pixel=` is optional, which allows depth-only programs.

```hlsl
#pragma program Lit vertex=LitVS pixel=LitPS
#pragma program ShadowDepth vertex=LitVS

cbuffer Frame
{
    float4x4 WorldViewProjection;
    float4 Tint;
};

Texture2D Albedo;
SamplerState AlbedoSampler;
```

### Build
Import the shader targets into the project that owns the shaders:

```xml
<Import Project="path/to/src/Crystal.Framework/build/Crystal.Shaders.targets" />
```

- **Items**: every `**/*.hlsl` is a `Shader` and every `**/*.hlsli` is a `ShaderInclude`. Set `EnableDefaultShaderItems` to `false` to list them yourself. `#include` resolves relative to the including file and to each `CrystalShaderIncludeDirectory` item. `CrystalShaderDefine` items (`NAME=VALUE`) become defines.
- **Properties**: `CrystalShaderModel` (default `6_6`, the minimum), `CrystalShaderDebugInfo` (default on in Debug, embeds PDB data) and `CrystalShaderWarningsAsErrors` (defaults to `TreatWarningsAsErrors`).
- **Output**: each `.hlsl` becomes one `.crystalshader` library with the same relative path in the output directory. It holds DXIL for every entry point and the reflection data. DXC errors appear as normal build errors with file and line. The step is incremental.
- **Embedding**: set `CrystalEmbedShaders` to `true` to embed the libraries in the assembly instead of copying them. The resource name is the relative path with `/` separators, such as `Shaders/Lit.crystalshader`. Pass `GetManifestResourceStream` to the `ShaderLibrary(GraphicsDevice, Stream)` constructor. Libraries that ship shaders use this.
- **Runtime compiling**: set `CrystalRuntimeShaderCompiler` to `true` to reference `Crystal.ShaderCompiler` and ship DXC with the app. `new RuntimeShaderCompiler(includeDirectories, defines).Compile(name, source)` returns a `ShaderCompilation` whose `Library` holds the same `.crystalshader` bytes the build writes, or `null` with `Errors`. Load it with `new ShaderLibrary(device, new MemoryStream(library))`. `RuntimeShaderCompiler.Version` changes with every compiler build; put it in disk cache keys. One instance is not thread-safe; use one per thread.
- **Matrices are row-major**: shaders compile with `-Zpr`, so a `float4x4` matches `System.Numerics.Matrix4x4` byte for byte. Use row vectors, `mul(position, WorldViewProjection)`. `column_major` matrices still work, and Crystal transposes them when it writes the value.
- **Descriptor indices**: compiled programs share one root signature for graphics and one for compute. The compiler turns each texture, buffer and sampler declaration into a read from `ResourceDescriptorHeap` or `SamplerDescriptorHeap` at an index Crystal passes as root constants, so switching programs never changes the root signature. Write declarations the usual way, one per line: `Texture2D<float4> Albedo : register(t0);`. A vertex shader reads at most 16 resources and samplers, a pixel or compute shader 32. Constant buffers stay root descriptors and must be in `space0`, below `b4` for vertex and pixel shaders and below `b8` for compute shaders. `space1` is reserved for the indices.
- **Not supported yet**: resource arrays and typed buffers (`Buffer<T>`).

### Programs and parameters
```csharp
var shaders = ShaderLibrary.Load(GraphicsDevice, "Shaders/Lit.crystalshader");
var lit = shaders.GetProgram("Lit");
var worldViewProjection = lit.Parameters["WorldViewProjection"]!;
worldViewProjection.SetValue(world * view * projection);
lit.Parameters["Albedo"]!.SetValue(albedo);
lit.Parameters["AlbedoSampler"]!.SetValue(GraphicsDevice.GetSamplerState(SamplerDescription.AnisotropicWrap));
```

- **Lookup**: `Parameters["Name"]` returns `null` when the program doesn't use that name. Look parameters up once and keep the `ShaderParameter`.
- **Values**: `SetValue` takes `float`, `int`, `uint`, `bool`, `Vector2`–`Vector4`, `Color`, `Matrix4x4`, spans of `float`, `Vector4` and `Matrix4x4` for arrays, and `Texture`, `GraphicsBuffer` or `SamplerState` for resources. Sizes, kinds and texture dimensions are checked when you set them.
- **Typed constant buffers**: `lit.ConstantBuffers["Frame"]!.SetValue(frameConstants)` writes a whole struct. `SetBuffer(buffer, offset)` reads the constant buffer from a `GraphicsBuffer` with `BufferUsage.Constant` instead, at a 256-byte aligned offset.
- **Uploads**: values live in CPU memory. A draw uploads each changed constant buffer to a per-frame ring of persistently mapped upload pages, or reuses the last upload when nothing changed.
- **Samplers** default to `LinearWrap`. Textures and buffers have no default, and drawing with one unset throws.
- **Root signatures** come from reflection: a root constant buffer view per constant buffer and a one-descriptor table per texture, buffer or sampler.

### Runtime programs
Bytecode that doesn't come from the shader build, such as shaders loaded from content files, becomes a library through a description. The caller supplies the reflection that the build would otherwise write:

```csharp
var library = new ShaderLibrary(GraphicsDevice, [new ShaderProgramDescription
{
    Name = "Material",
    VertexShader = vertexBytecode,
    PixelShader = pixelBytecode,
    ConstantBuffers =
    [
        new ShaderConstantBufferDescription("VertexConstants", 0, 64, ShaderStages.Vertex,
            [new ShaderVariableDescription("WorldViewProjection", 0, 64, ShaderVariableClass.MatrixRows, 4, 4)]),
        new ShaderConstantBufferDescription("PixelConstants", 0, 16, ShaderStages.Pixel,
            [new ShaderVariableDescription("Tint", 0, 16, ShaderVariableClass.Vector, 1, 4)]),
    ],
    Resources =
    [
        new ShaderResourceDescription("Albedo", ShaderResourceKind.Texture, ShaderResourceDimension.Texture2D, 0, ShaderStages.Pixel),
        new ShaderResourceDescription("AlbedoSampler", ShaderResourceKind.Sampler, ShaderResourceDimension.None, 0, ShaderStages.Pixel),
    ],
    VertexInputs = [new ShaderVertexInputDescription("POSITION", 0, 0), new ShaderVertexInputDescription("TEXCOORD", 0, 1)],
}], "Materials");
```

- **Bytecode**: DXIL or DXBC (shader model 5.0 and later) containers.
- **Names are the caller's**: bindings go by register, space and stage, so constant buffers and resources can carry any name. Constant buffer names are unique per program, and so are resource names.
- **Shared values**: a value name declared in several constant buffers, typically one per stage, is one `ShaderParameter` that writes all of them. Every declaration must have the same size, class, rows, columns and elements. `Offset` and `ConstantBuffer` report the first declaration.
- **Checks**: the constructor throws `ArgumentException` for duplicate program names, a compute shader next to a vertex or pixel shader, a pixel shader without a vertex shader, variables outside their buffer, buffers over 64 KiB, entries visible to a stage the program lacks, two entries on one register, space and stage, dimensions that don't fit the resource kind, and more than 64 root signature DWORDs.

### Pipelines and draws
```csharp
var pipeline = new GraphicsPipeline(GraphicsDevice, new GraphicsPipelineDescription
{
    Program = lit,
    VertexLayout = VertexPositionNormalTexture.Layout,
    BlendState = BlendState.Opaque,
    RasterizerState = RasterizerState.CullBack,
    DepthStencilState = DepthStencilState.Default,
    RenderTargetFormats = [SurfaceFormat.Rgba16Float],
    DepthStencilFormat = SurfaceFormat.Depth32Float,
    SampleCount = 4,
});

GraphicsDevice.SetRenderTarget(scene, depth);
GraphicsDevice.SetPipeline(pipeline);
GraphicsDevice.SetVertexBuffer(vertices);
GraphicsDevice.SetIndexBuffer(indices);
GraphicsDevice.DrawIndexed(indexCount);
```

- **`GraphicsPipeline`** bakes the program, states, vertex layout, primitive type and target formats into one D3D12 pipeline state object. Create pipelines at load time. The vertex layout is checked against the vertex shader inputs.
- **Defaults**: `BlendState.Opaque`, `RasterizerState.CullBack`, `DepthStencilState.Default`, `PrimitiveType.TriangleList` and one `Rgba8Unorm` target, which is `GraphicsDevice.BackBufferFormat`. Without a `DepthStencilFormat`, depth testing is off.
- **Draws must match the targets**: the bound color formats, depth format and sample count must match the pipeline, or the draw throws.
- **Vertex layouts**: `VertexElement` uses HLSL semantic names (`"POSITION"`, `"TEXCOORD"`, index 1), a slot from 0 to 15 and an optional instance step rate. `VertexPositionColor`, `VertexPositionTexture`, `VertexPositionColorTexture` and `VertexPositionNormalTexture` come with a `Layout`.
- **Draw calls**: `Draw`, `DrawIndexed`, `DrawInstanced` and `DrawIndexedInstanced` take vertex and index counts. Index buffers use their stride (2 or 4) as the index format.
- **Transient geometry**: `SetVertexData<T>(span, slot)` and `SetIndexData` (`ushort` or `uint` spans) copy geometry into the per-frame upload ring and bind it, with no `GraphicsBuffer`. Use them for geometry that changes every frame. The data stays bound until `SetVertexBuffer`, `SetIndexBuffer` or the next frame replaces it. Data larger than an upload page (1 MiB) gets its own upload buffer for that frame.
- **Fixed-function state**: `Viewport`, `ScissorRectangle`, `StencilReference` and `BlendFactor` are device properties. Binding render targets resets the viewport and scissor rectangle to the full target.
- **Barriers**: draws track every bound texture and buffer. A texture that is bound to a parameter and as a target at the same time throws.
- **No allocations**: setting values and drawing allocate no managed memory.

### Threading
- **Any thread**: `ShaderLibrary` and `GraphicsPipeline` creation. Load them on a worker thread while the game keeps running.
- **Render thread**: `SetValue`, `SetBuffer`, `SetPipeline` and draws, or a thread with a bound `GraphicsCommandList`.
- **Lifetime**: disposing a library or pipeline releases its root signatures or pipeline state once the GPU is done with them. Drawing with a pipeline whose library was disposed throws.

## Compute
A `compute=` entry point makes a compute program. Its thread group size comes from `[numthreads]`.

```hlsl
#pragma program Cull compute=CullCS

RWStructuredBuffer<uint> Visible;

[numthreads(64, 1, 1)]
void CullCS(uint3 id : SV_DispatchThreadID) { }
```

```csharp
var cull = new ComputePipeline(GraphicsDevice, shaders.GetProgram("Cull"));
cull.Program.Parameters["Visible"]!.SetValue(visibleBuffer);
GraphicsDevice.Dispatch(cull, (instanceCount + 63) / 64);
```

- **Parameters** work as they do for graphics programs. `RWTexture2D` and `RWStructuredBuffer` parameters need `TextureUsage.UnorderedAccess` or `BufferUsage.UnorderedAccess`. `ShaderProgram.ThreadGroupSize` gives the `[numthreads]` size.
- **Dispatch**: `Dispatch` takes up to `GraphicsDevice.MaximumThreadGroupCount` groups per dimension. Barriers between dispatches and draws are tracked like everything else.
- **Async compute**: `GraphicsDevice.AsyncCompute.Dispatch` records on a dedicated compute queue, and `AsyncCompute.Submit` sends the work. `EndFrame` submits anything still open. Each queue waits for the other only on the resources they share. Using a resource on the graphics queue while it's in unsubmitted async compute work throws.
- **Indirect**: `DispatchIndirect`, `DrawInstancedIndirect` and `DrawIndexedInstancedIndirect` read `IndirectDispatchArguments`, `IndirectDrawArguments` or `IndirectDrawIndexedArguments` from a buffer with `BufferUsage.Indirect`, at 4-byte aligned offsets. The draw calls take a `maximumDrawCount` and an optional count buffer, so a compute shader can decide how many draws run.
- **No allocations**: dispatches allocate no managed memory.

## Queries and profiling
- **Occlusion queries**: `OcclusionQuery.Begin` and `End` surround draws. Only one query can be active at a time, and it must end before the frame ends or data is read back. Results arrive once the GPU has finished. Poll `IsComplete`, then read `PixelCount`. Queries can be created on any thread.
- **Profiler**: set `GraphicsDevice.Profiler.IsEnabled` and wrap work in `using (GraphicsDevice.Profiler.Scope("Shadows")) { … }`, or call `Begin` and `End`. `Profiler.Samples` lists the scopes of the latest finished frame in order, with their nesting depth and GPU and CPU time in milliseconds. `SamplesFrameNumber` says which frame that was. `FrameGpuMilliseconds` is that frame's whole GPU time, and `GpuMillisecondsSincePreviousFrame` the time since the frame before ended, which shows GPU stalls between frames. A frame holds up to 1024 scopes nested up to 64 deep. Scopes allocate nothing, and cost nothing while the profiler is off.
- **Video memory**: `GraphicsDevice.GetVideoMemoryInfo()` returns the adapter's local and non-local budgets and current usage in bytes.
- **Metrics**: `GraphicsDevice.Metrics` counts the draws, dispatches, clears, pipeline state and root signature changes, vertex and index buffer binds, and constant buffer and descriptor table binds (with how many repeat the argument already bound), and descriptor index uploads recorded so far in the current frame. It resets when the next frame begins, so `EndDraw` sees the whole frame.

## 2D
```csharp
_spriteBatch = new SpriteBatch(GraphicsDevice);

_spriteBatch.Begin(SpriteSortMode.BackToFront);
_spriteBatch.Draw(player, position, null, Color.White, rotation, origin, 1f, SpriteEffects.FlipHorizontally, 0.5f);
_spriteBatch.DrawString(font, "Score: 100", new Vector2(8f, 8f), Color.White);
_spriteBatch.End();
```

- **`SpriteBatch`**: `Draw` takes a position or destination rectangle, an optional source rectangle, color, rotation, origin, scale, `SpriteEffects` and layer depth, like XNA. `Begin` takes the sort mode, blend, sampler, depth stencil and rasterizer states, an optional `ShaderProgram` and a transform matrix. The defaults are `BlendState.AlphaBlend` with premultiplied colors, `LinearClamp`, `DepthStencilState.None` and `RasterizerState.CullNone`.
- **Sort modes**: `Deferred` draws in call order, `Immediate` draws each sprite as it comes, `Texture` groups sprites by texture, and `BackToFront` and `FrontToBack` sort by layer depth. Sorting is stable, so sprites with equal keys keep their call order.
- **Batching**: `End` draws each run of sprites with the same texture as one indexed draw of up to `MaximumSpritesPerDraw` (8192) sprites. Vertices go through the per-frame upload ring, and the indices come from one buffer that the batch fills once.
- **Pipelines**: a `SpriteBatch` draws into one set of target formats, given to the constructor (the back buffer by default, plus an optional depth format and sample count). It bakes pipelines for the four standard blend states when it is created. Call `PreparePipeline` at load time for any other combination of states or program. Otherwise the pipeline is created the first time `Begin` sees it, which stalls that frame.
- **Custom programs**: a program passed to `Begin` takes `POSITION`, `COLOR` and `TEXCOORD` like `VertexPositionColorTexture`. The batch sets its `MatrixTransform`, `Texture` and `TextureSampler` parameters when the program declares them.
- **`SpriteFont`**: built from a `Texture2D` and a list of `SpriteFontGlyph` (character, bounds in the texture, cropping, and left bearing, width and right bearing), plus line spacing, spacing and an optional `DefaultCharacter` for missing characters. `MeasureString` and `DrawString` take a `ReadOnlySpan<char>` (a `string` works too) or a `StringBuilder`, and handle `\n` line breaks. Glyphs are looked up per UTF-16 character.
- **No allocations**: once warmed up, `Begin`, `Draw`, `DrawString` and `End` allocate no managed memory.

## Input
```csharp
using Crystal.Input;

var keyboard = Keyboard.GetState();
if (keyboard.IsKeyDown(Keys.Escape)) Exit();

var mouse = Mouse.GetState();
if (mouse.LeftButton == ButtonState.Pressed) Select(mouse.Position);

var pad = GamePad.GetState(0);
if (pad.IsConnected) Move(pad.ThumbSticks.Left);
```

- **Updates**: the game loop processes window events before each tick. `GetState` returns a snapshot, and the snapshot only changes between ticks.
- **Keyboard**: `Keys` are physical key positions, so `Keys.W` is the key left of `E` on any layout. `KeyboardState.GetPressedKeys` fills a span you pass in.
- **Mouse**: `X` and `Y` are in back buffer pixels, relative to the window's client area. `ScrollWheelValue` adds `Mouse.WheelDelta` (120) per notch, and `HorizontalScrollWheelValue` does the same for horizontal scrolling. `Mouse.SetPosition` moves the cursor. `Game.IsMouseVisible` shows or hides the cursor over the window, and is `false` by default. `Mouse.IsRelativeMode` holds the cursor for mouse-look; `RelativeX` and `RelativeY` keep accumulating motion while it is on.
- **Gamepads**: `GamePad.GetState(playerIndex)` reads up to `GamePad.MaximumGamePadCount` (8) gamepads through SDL's gamepad database. A connected gamepad takes the lowest free index and keeps it until it disconnects. Face buttons use Xbox names, so `A` is the bottom button. Thumbsticks are -1 to 1 with up positive, and triggers are 0 to 1. `GamePadDeadZone.IndependentAxes` is the default, with XNA's dead zone sizes. `Circular` and `None` are the other options.
- **Edges and actions**: an `InputFrame` keeps this tick's and the previous tick's state. Call `Update` once per tick, then ask for `IsKeyPressed`, `IsMouseButtonReleased`, `MouseMovement` or `ScrollNotches`. An `InputMap<TAction>` binds your own action enum to keys, mouse buttons and gamepad buttons, and answers `IsDown`, `WasPressed` and `WasReleased` for the action as a whole.
- **No allocations**: reading input state allocates no managed memory.

## Dear ImGui
`Crystal.ImGui` is a separate project with Dear ImGui 1.93.0 (docking branch) and a renderer for it. Reference it next to `Crystal.Framework`.

```csharp
using Crystal.ImGui;

_imGui = new ImGuiRenderer(this);

protected override void Draw(GameTime gameTime)
{
    GraphicsDevice.Clear(Color.CornflowerBlue);
    _imGui.BeginFrame(gameTime);
    ImGui.Begin("Statistics");
    ImGui.Text($"Draws: {GraphicsDevice.Metrics.DrawCount}");
    ImGui.End();
    _imGui.EndFrame();
}
```

- **Binding**: `ImGui` holds the functions, with `string` and span overloads that encode text into a stack buffer instead of allocating. Structs such as `ImGuiIO` and `ImDrawList` are used through `ImGuiIOPtr`-style wrappers. The binding is generated from cimgui by `tools/Crystal.ImGui.Generator`. `Regenerate.ps1` rebuilds cimgui's JSON and runs the generator, and the generated layout tests check every struct against the native sizes and offsets.
- **Renderer**: `ImGuiRenderer` creates its own ImGui context. `BeginFrame` feeds it the display size, time, mouse, keyboard and focus, then starts the ImGui frame. `EndFrame` renders the frame into the bound render target with its geometry in the upload ring. Pass the target format, depth format and sample count to the constructor when it doesn't draw into the back buffer.
- **Textures**: ImGui's font atlas and other textures are created and updated through the 1.92 texture protocol, so atlases grow on demand. `BindTexture` turns a `Texture2D` into an `ImTextureRef` for `ImGui.Image`. `UnbindTexture` forgets it.
- **Input**: `WantCaptureMouse` and `WantCaptureKeyboard` tell the game when ImGui uses the input. While a text field has focus, the renderer turns on `Window.IsTextInputEnabled` and feeds typed characters to ImGui.
- **Settings**: `IniFilePath` is `null` by default, so no `imgui.ini` is written. `DockingEnabled` turns on docking.
- **Callbacks**: `ImDrawList.AddCallback` with an unmanaged function pointer runs during `EndFrame`. A callback of `(void*)ImGuiRenderer.ResetRenderStateCallback` makes the renderer set its pipeline and state again.

## Native dependencies
The build places these next to the application:

| File | Source |
|---|---|
| `SDL3.dll` | The official SDL 3.4.18 Windows x64 release, downloaded once into `artifacts/natives` and checked against its SHA-256 |
| `D3D12/D3D12Core.dll`, `D3D12/d3d12SDKLayers.dll` | The `Microsoft.Direct3D.D3D12` Agility SDK package |
| `Crystal.ImGui.Native.dll` | Dear ImGui 1.93.0 (docking) and cimgui, compiled at build time with MSVC. The Dear ImGui source is downloaded once into `artifacts/natives` and checked against its SHA-256. Building `Crystal.ImGui` needs Visual Studio or the Build Tools with the C++ x64 tools |

Crystal loads the Agility SDK through `ID3D12SDKConfiguration1::CreateDeviceFactory`, because a managed executable can't export `D3D12SDKVersion`. If the Agility SDK can't load, it falls back to the D3D12 runtime that ships with Windows.

## Debugging
| Variable | Effect |
|---|---|
| `CRYSTAL_DX12_DEBUG=1` | Turns on the D3D12 debug layer and DRED |
| `CRYSTAL_DX12_GBV=1` | Also turns on GPU-based validation |
| `CRYSTAL_DX12_WARP=1` | Uses the WARP software adapter |

These variables do the same as `GraphicsDeviceOptions.EnableDebugLayer`, `EnableGpuBasedValidation` and `AdapterPreference`. Debug layer messages go to `GraphicsDevice.DebugMessageReceived`, or to stderr if the event has no subscribers. Errors throw `GraphicsDebugException` at the end of the frame unless `ThrowOnDebugLayerErrors` is off.

## Building and testing
```
dotnet build Crystal.slnx
dotnet test tests/Crystal.Tests
```

The GPU tests run on WARP with the debug layer, and once more with GPU-based validation. Any debug layer error fails the test.
