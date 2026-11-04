# Crystal.Graphics: resources

Buffers, textures, views, samplers and the formats they use.

**Threads.** Constructors and `SetData` work on any thread. Until the GPU first uses a resource, uploads go through a background copy queue, and the frame waits for that queue only when it uses the resource. Once the GPU has used it, `SetData`, `GetData` and `BeginGetData` must run on the render thread, and uploads run in order with the frame.

**Lifetime.** `Dispose` returns at once. The native resource and its descriptors are released when the GPU has finished with them. Disposing the device disposes every resource still alive.

## GraphicsResource
`public abstract class GraphicsResource : IDisposable`

Base of buffers, textures, pipelines, shader libraries, readbacks and queries.

| Member | Meaning |
|---|---|
| `GraphicsDevice GraphicsDevice { get; }` | The owning device. |
| `string? Name { get; }` | The debug name given at creation, also set on the D3D12 object. |
| `bool IsDisposed { get; }` | `true` after `Dispose`. |
| `void Dispose()` | Releases the resource once the GPU is done with it. Calling it again does nothing. Any thread. |

## GraphicsBuffer
`public sealed class GraphicsBuffer : GraphicsResource`

| Member | Meaning |
|---|---|
| `const int MaximumConstantBufferSize = 65536` | Largest buffer with `BufferUsage.Constant`. |
| `GraphicsBuffer(GraphicsDevice graphicsDevice, in BufferDescription description)` | Creates the buffer in GPU memory. |
| `BufferDescription Description { get; }` | The description. |
| `long Size { get; }`, `int Stride { get; }`, `BufferUsage Usage { get; }` | From the description. |
| `void SetData<T>(ReadOnlySpan<T> data, long offsetInBytes = 0) where T : unmanaged` | Uploads `data` at the offset. The range must fit. |
| `void GetData<T>(Span<T> destination, long offsetInBytes = 0) where T : unmanaged` | Reads `destination.Length * sizeof(T)` bytes from the offset and waits for them. Render thread. |
| `GraphicsReadback BeginGetData(long offsetInBytes, long sizeInBytes)` | Starts a readback of the range without waiting. Render thread. |

**Constructor throws** `ArgumentException` when `Size` is not positive or `Stride` is negative, an index buffer's stride is not 2 or 4, a vertex buffer has no stride, a constant buffer is over 64 KiB, a structured buffer's stride doesn't divide its size, or a raw unordered access buffer's size is not a multiple of 4.

## BufferDescription
`public readonly record struct BufferDescription`

| Property | Default | Meaning |
|---|---|---|
| `long Size { get; init; }` | 0 | Size in bytes. Required. |
| `int Stride { get; init; }` | 0 | Bytes per element. Required for vertex, index and structured buffers. |
| `BufferUsage Usage { get; init; }` | `None` | What the buffer is for. `None` is a plain buffer for copies. |
| `string? Name { get; init; }` | `null` | Debug name. |

## BufferUsage
`[Flags] public enum BufferUsage { None = 0, Vertex = 1, Index = 2, Constant = 4, Structured = 8, Indirect = 16, UnorderedAccess = 32 }`

| Flag | Allows |
|---|---|
| `Vertex` | `SetVertexBuffer`. Needs a stride. |
| `Index` | `SetIndexBuffer`. Stride 2 or 4. |
| `Constant` | `ShaderConstantBuffer.SetBuffer`. At most 64 KiB. |
| `Structured` | `StructuredBuffer<T>` parameters. Needs a stride that divides the size. |
| `Indirect` | Argument and count buffers of the indirect draws and dispatches. |
| `UnorderedAccess` | With `Structured`: `RWStructuredBuffer<T>` parameters (and `StructuredBuffer<T>`). Without `Structured`: a raw buffer for `ByteAddressBuffer` and `RWByteAddressBuffer` parameters, with a size that is a multiple of 4. |

`ShaderParameter.SetValue(GraphicsBuffer)` throws `ArgumentException` when the buffer lacks the flags its parameter kind needs.

## Texture
`public abstract class Texture : GraphicsResource`

Base of `Texture2D` and `TextureCube`.

| Member | Meaning |
|---|---|
| `TextureDescription Description { get; }` | The description, with `MipLevels` resolved when it was 0. |
| `int Width { get; }`, `int Height { get; }` | Size of mip 0. |
| `int MipLevels { get; }` | Mip count. |
| `int ArrayLayers { get; }` | Array layers; six per cube for `TextureCube`. |
| `int SampleCount { get; }` | Samples per pixel. |
| `SurfaceFormat Format { get; }` | Pixel format. |
| `TextureUsage Usage { get; }` | Usage flags. |
| `int GetMipLevelSizeInBytes(int mipLevel)` | Bytes of one array layer of that mip with tightly packed rows (rows of 4×4 blocks for BC formats). This is the size `SetData` and `GetData` expect. |
| `TextureView GetMipView(int mipLevel)` | A view of one mip, created on first call and kept for the texture's lifetime. Any thread. Throws `InvalidOperationException` when the texture has neither `ShaderResource` nor `UnorderedAccess`. |

**Constructors throw** `ArgumentException` when:
- a size or `ArraySize` is not positive, or `MipLevels` exceeds the full chain;
- `SampleCount` is not 1, 2, 4, 8 or 16, or `Usage` is `None`;
- a cube texture isn't square;
- a BC texture's size isn't a multiple of 4, or it has a usage other than `ShaderResource`;
- a depth format has `RenderTarget` or `UnorderedAccess`, or a non-depth format has `DepthStencil`;
- an sRGB format has `UnorderedAccess`;
- a multisampled texture has more than one mip, is a cube, isn't a render or depth target, or has `UnorderedAccess`.

They throw `NotSupportedException` when the adapter doesn't support the sample count for the format.

## Texture2D
`public sealed class Texture2D : Texture`

| Member | Meaning |
|---|---|
| `Texture2D(GraphicsDevice graphicsDevice, in TextureDescription description)` | A 2D texture, or a 2D array when `ArraySize` > 1. |
| `void SetData<T>(ReadOnlySpan<T> data, int mipLevel = 0, int arraySlice = 0) where T : unmanaged` | Uploads a whole mip of one slice. `data` must be exactly `GetMipLevelSizeInBytes(mipLevel)` bytes. |
| `void SetData<T>(ReadOnlySpan<T> data, Rectangle region, int mipLevel = 0, int arraySlice = 0) where T : unmanaged` | Uploads a region of a mip. The region must lie inside the mip, and for BC formats be aligned to 4×4 blocks. |
| `void GetData<T>(Span<T> destination, int mipLevel = 0, int arraySlice = 0) where T : unmanaged` | Reads a whole mip of one slice and waits for it. Render thread. |
| `GraphicsReadback BeginGetData(int mipLevel = 0, int arraySlice = 0)` | Starts the same readback without waiting. Render thread. |

`SetData` and `GetData` throw `InvalidOperationException` for multisampled textures and for formats with stencil. Resolve multisampled textures before reading them.

## TextureCube
`public sealed class TextureCube : Texture`

| Member | Meaning |
|---|---|
| `TextureCube(GraphicsDevice graphicsDevice, in TextureDescription description)` | A cube, or a cube array when `ArraySize` > 1. `Width` must equal `Height`. |
| `int Size { get; }` | Edge length of mip 0. |
| `int CubeCount { get; }` | Number of cubes (`ArraySize`). |
| `void SetData<T>(CubeFace face, ReadOnlySpan<T> data, int mipLevel = 0, int cubeIndex = 0) where T : unmanaged` | Uploads a whole mip of one face. |
| `void GetData<T>(CubeFace face, Span<T> destination, int mipLevel = 0, int cubeIndex = 0) where T : unmanaged` | Reads a whole mip of one face and waits for it. Render thread. |
| `GraphicsReadback BeginGetData(CubeFace face, int mipLevel = 0, int cubeIndex = 0)` | Starts the same readback without waiting. Render thread. |

## CubeFace
`public enum CubeFace { PositiveX = 0, NegativeX = 1, PositiveY = 2, NegativeY = 3, PositiveZ = 4, NegativeZ = 5 }`

The array layer of a face is `cubeIndex * 6 + (int)face`.

## TextureDescription
`public readonly record struct TextureDescription`

| Property | Default | Meaning |
|---|---|---|
| `int Width { get; init; }`, `int Height { get; init; }` | 0 | Size of mip 0. Required. |
| `int MipLevels { get; init; }` | 1 | Mip count. 0 builds the full chain down to 1×1. |
| `int ArraySize { get; init; }` | 1 | Array slices, or cubes for `TextureCube`. |
| `SurfaceFormat Format { get; init; }` | `Rgba8Unorm` | Pixel format. |
| `int SampleCount { get; init; }` | 1 | Samples per pixel. |
| `TextureUsage Usage { get; init; }` | `ShaderResource` | What the texture is for. |
| `Vector4 ClearColor { get; init; }` | (0, 0, 0, 0) | Optimized clear color of render targets. Clearing to it is fastest. |
| `float ClearDepth { get; init; }` | 1 | Optimized clear depth of depth targets. |
| `byte ClearStencil { get; init; }` | 0 | Optimized clear stencil of depth targets. |
| `string? Name { get; init; }` | `null` | Debug name. |

## TextureUsage
`[Flags] public enum TextureUsage { None = 0, ShaderResource = 1, RenderTarget = 2, DepthStencil = 4, UnorderedAccess = 8 }`

`ShaderResource` allows `Texture2D`/`TextureCube` parameters, including sampling a depth texture. `UnorderedAccess` allows `RWTexture2D` parameters.

## TextureView
`public sealed class TextureView`

One mip of a texture, from `Texture.GetMipView`. Pass it to `ShaderParameter.SetValue` like a texture. A texture bound directly shows all its mips to a `Texture2D` parameter and mip 0 to a `RWTexture2D` parameter. A view shows only its mip to either.

| Member | Meaning |
|---|---|
| `Texture Texture { get; }` | The viewed texture. |
| `int MipLevel { get; }` | The viewed mip. |
| `int Width { get; }`, `int Height { get; }` | Size of that mip, at least 1. |

Two read-write views of one texture can be bound in one draw or dispatch. A read-only and a read-write view of the same texture in one draw or dispatch aren't supported, because barriers cover the whole texture.

## SurfaceFormat
`public enum SurfaceFormat`

| Group | Members |
|---|---|
| 8-bit unorm | `R8Unorm = 0`, `Rg8Unorm = 1`, `Rgba8Unorm = 2`, `Rgba8UnormSrgb = 3`, `Bgra8Unorm = 4`, `Bgra8UnormSrgb = 5` |
| 16-bit | `R16Unorm = 6`, `Rg16Unorm = 7`, `Rgba16Unorm = 8`, `R16Float = 9`, `Rg16Float = 10`, `Rgba16Float = 11` |
| 32-bit | `R32Float = 12`, `Rg32Float = 13`, `Rgba32Float = 14`, `R32Uint = 15` |
| Packed | `Rg11B10Float = 16`, `Rgb10A2Unorm = 17` |
| Block-compressed | `Bc1Unorm = 18`, `Bc1UnormSrgb = 19`, `Bc2Unorm = 20`, `Bc2UnormSrgb = 21`, `Bc3Unorm = 22`, `Bc3UnormSrgb = 23`, `Bc4Unorm = 24`, `Bc4Snorm = 25`, `Bc5Unorm = 26`, `Bc5Snorm = 27`, `Bc6hUfloat = 28`, `Bc6hSfloat = 29`, `Bc7Unorm = 30`, `Bc7UnormSrgb = 31` |
| Depth | `Depth16Unorm = 32`, `Depth24UnormStencil8 = 33`, `Depth32Float = 34`, `Depth32FloatStencil8 = 35` |

sRGB formats convert to linear when sampled and from linear when written as targets.

## SamplerState
`public sealed class SamplerState`

A sampler from `GraphicsDevice.GetSamplerState`, shared by every caller with the same description. There is no public constructor and nothing to dispose.

| Member | Meaning |
|---|---|
| `SamplerDescription Description { get; }` | The description. |

`GetSamplerState` throws `ArgumentOutOfRangeException` when `MaxAnisotropy` is outside 1 to 16.

## SamplerDescription
`public readonly record struct SamplerDescription`

| Property | Default | Meaning |
|---|---|---|
| `FilterMode MinFilter`, `MagFilter`, `MipFilter { get; init; }` | `Linear` | Filters. `MaxAnisotropy` > 1 turns on anisotropic filtering. |
| `FilterReduction Reduction { get; init; }` | `Standard` | How filtered samples combine. |
| `int MaxAnisotropy { get; init; }` | 1 | 1 to 16. |
| `TextureAddressMode AddressU`, `AddressV`, `AddressW { get; init; }` | `Wrap` | Addressing per axis. |
| `float MipLodBias { get; init; }` | 0 | Added to the computed mip level. |
| `ComparisonFunction ComparisonFunction { get; init; }` | `Never` | Used when `Reduction` is `Comparison`. |
| `Vector4 BorderColor { get; init; }` | (0, 0, 0, 0) | Used by `Border` addressing. |
| `float MinLod { get; init; }` | 0 | Lowest mip level used. |
| `float MaxLod { get; init; }` | `float.MaxValue` | Highest mip level used. |

| Preset | Description |
|---|---|
| `PointWrap`, `PointClamp` | Point filtering for all three filters. |
| `LinearWrap`, `LinearClamp` | The defaults, with wrap or clamp. |
| `AnisotropicWrap`, `AnisotropicClamp` | Linear with `MaxAnisotropy` 16. |
| `ShadowComparison` | `LinearClamp` with point mips, `Comparison` reduction and `LessEqual`. |

## FilterMode
`public enum FilterMode { Point = 0, Linear = 1 }`

## FilterReduction
`public enum FilterReduction { Standard = 0, Comparison = 1, Minimum = 2, Maximum = 3 }`

`Comparison` compares each sample with the reference value before filtering (`SampleCmp`). `Minimum` and `Maximum` return the smallest or largest sample in the footprint.

## TextureAddressMode
`public enum TextureAddressMode { Wrap = 1, Mirror = 2, Clamp = 3, Border = 4, MirrorOnce = 5 }`

## ComparisonFunction
`public enum ComparisonFunction { Never = 1, Less = 2, Equal = 3, LessEqual = 4, Greater = 5, NotEqual = 6, GreaterEqual = 7, Always = 8 }`

Used by samplers, depth tests and stencil tests.
