# Crystal.Graphics: pipelines

Pipelines, the fixed-function states they bake, vertex layouts, the predefined vertex types, viewports and indirect argument records.

**Threads.** Pipelines and states can be created on any thread. Create pipelines at load time: each one compiles a D3D12 pipeline state object.

## GraphicsPipeline
`public sealed class GraphicsPipeline : GraphicsResource`

Bakes a vertex and pixel program, the states, the vertex layout, the primitive type and the target formats into one pipeline state object.

| Member | Meaning |
|---|---|
| `GraphicsPipeline(GraphicsDevice graphicsDevice, GraphicsPipelineDescription description)` | Validates the description and creates the pipeline state. |
| `GraphicsPipelineDescription Description { get; }` | The description. |
| `ShaderProgram Program { get; }` | The program. |

**Constructor throws**
- `ArgumentException` when the program belongs to another device or is a compute program; when a state is `null`; when there are more than 8 target formats, a target format is a depth or BC format, or `DepthStencilFormat` is not a depth format; when there are neither target formats nor a depth format; when the program reads vertex inputs and `VertexLayout` is `null`, or the layout lacks a semantic the vertex shader reads.
- `ArgumentOutOfRangeException` when `SampleCount` is below 1.
- `ObjectDisposedException` when the program's library is disposed.
- `GraphicsException` when the driver rejects the pipeline.

Drawing with a pipeline whose library was disposed throws `ObjectDisposedException`.

## GraphicsPipelineDescription
`public sealed record GraphicsPipelineDescription`

| Property | Default | Meaning |
|---|---|---|
| `required ShaderProgram Program { get; init; }` | — | A vertex program, with or without a pixel shader. |
| `BlendState BlendState { get; init; }` | `BlendState.Opaque` | Blending per target. |
| `RasterizerState RasterizerState { get; init; }` | `RasterizerState.CullBack` | Fill, cull and depth bias. |
| `DepthStencilState DepthStencilState { get; init; }` | `DepthStencilState.Default` | Depth and stencil tests. Ignored without a `DepthStencilFormat`. |
| `VertexLayout? VertexLayout { get; init; }` | `null` | Required when the vertex shader reads inputs. Checked against them. |
| `PrimitiveType PrimitiveType { get; init; }` | `TriangleList` | Topology. |
| `IReadOnlyList<SurfaceFormat> RenderTargetFormats { get; init; }` | `[Rgba8Unorm]` | Formats of the bound color targets, in order. `Rgba8Unorm` is the back buffer's format. Empty for depth-only pipelines. |
| `SurfaceFormat? DepthStencilFormat { get; init; }` | `null` | Format of the bound depth target, or `null` for no depth. |
| `int SampleCount { get; init; }` | 1 | Sample count of the targets. |
| `string? Name { get; init; }` | `null` | Debug name. |

Draws throw `InvalidOperationException` when the bound targets don't match `RenderTargetFormats`, `DepthStencilFormat` and `SampleCount`.

## ComputePipeline
`public sealed class ComputePipeline : GraphicsResource`

| Member | Meaning |
|---|---|
| `ComputePipeline(GraphicsDevice graphicsDevice, ShaderProgram program, string? name = null)` | Creates the pipeline for a compute program. Throws `ArgumentException` when the program has no compute shader or belongs to another device, `GraphicsException` when the driver rejects it. |
| `ShaderProgram Program { get; }` | The program. |

## PrimitiveType
`public enum PrimitiveType { TriangleList = 0, TriangleStrip = 1, LineList = 2, LineStrip = 3, PointList = 4 }`

## BlendState
`public sealed class BlendState : IEquatable<BlendState>`

| Member | Meaning |
|---|---|
| `BlendState(RenderTargetBlendDescription renderTarget, bool alphaToCoverage = false)` | One description used for every target. |
| `BlendState(ReadOnlySpan<RenderTargetBlendDescription> renderTargets, bool alphaToCoverage = false)` | One description per target, 1 to 8. Two or more turn on independent blending. |
| `bool AlphaToCoverage { get; }` | Uses pixel alpha as a coverage mask on multisampled targets. |
| `bool IndependentBlend { get; }` | `true` with more than one description. |
| `ReadOnlySpan<RenderTargetBlendDescription> RenderTargets { get; }` | The descriptions. |
| `RenderTargetBlendDescription GetRenderTarget(int index)` | The description for target `index`; the single description when blending isn't independent. |
| `Equals`, `GetHashCode` | Compare the descriptions and `AlphaToCoverage`. |
| `Opaque`, `AlphaBlend`, `NonPremultiplied`, `Additive` | Shared states built from the `RenderTargetBlendDescription` presets. |

## RenderTargetBlendDescription
`public readonly record struct RenderTargetBlendDescription`

| Property | Default |
|---|---|
| `bool BlendEnable { get; init; }` | `false` |
| `Blend SourceBlend { get; init; }` | `One` |
| `Blend DestinationBlend { get; init; }` | `Zero` |
| `BlendOperation BlendOperation { get; init; }` | `Add` |
| `Blend SourceBlendAlpha { get; init; }` | `One` |
| `Blend DestinationBlendAlpha { get; init; }` | `Zero` |
| `BlendOperation BlendOperationAlpha { get; init; }` | `Add` |
| `ColorWriteChannels WriteMask { get; init; }` | `All` |

| Preset | Color and alpha |
|---|---|
| `Opaque` | Blending off. |
| `AlphaBlend` | `One`, `InverseSourceAlpha`: premultiplied alpha. |
| `NonPremultiplied` | `SourceAlpha`, `InverseSourceAlpha`. |
| `Additive` | `SourceAlpha`, `One`. |

## Blend
`public enum Blend { Zero = 1, One = 2, SourceColor = 3, InverseSourceColor = 4, SourceAlpha = 5, InverseSourceAlpha = 6, DestinationAlpha = 7, InverseDestinationAlpha = 8, DestinationColor = 9, InverseDestinationColor = 10, SourceAlphaSaturate = 11, BlendFactor = 14, InverseBlendFactor = 15, Source1Color = 16, InverseSource1Color = 17, Source1Alpha = 18, InverseSource1Alpha = 19, AlphaFactor = 20, InverseAlphaFactor = 21 }`

`BlendFactor` and `AlphaFactor` read `GraphicsDevice.BlendFactor`. `Source1*` read the pixel shader's second output for dual-source blending.

## BlendOperation
`public enum BlendOperation { Add = 1, Subtract = 2, ReverseSubtract = 3, Min = 4, Max = 5 }`

## ColorWriteChannels
`[Flags] public enum ColorWriteChannels { None = 0, Red = 1, Green = 2, Blue = 4, Alpha = 8, All = 15 }`

## RasterizerState
`public sealed record RasterizerState`

| Property | Default | Meaning |
|---|---|---|
| `FillMode FillMode { get; init; }` | `Solid` | Solid or wireframe. |
| `CullMode CullMode { get; init; }` | `Back` | Which faces to drop. |
| `bool FrontCounterClockwise { get; init; }` | `false` | `false`: clockwise triangles face front. |
| `int DepthBias { get; init; }` | 0 | Constant depth bias. |
| `float DepthBiasClamp { get; init; }` | 0 | Largest bias; 0 means no clamp. |
| `float SlopeScaledDepthBias { get; init; }` | 0 | Bias scaled by the depth slope. |
| `bool DepthClipEnable { get; init; }` | `true` | Clips against the near and far planes. |
| `bool AntialiasedLineEnable { get; init; }` | `false` | Smooth lines on single-sampled targets. |
| `bool ConservativeRaster { get; init; }` | `false` | Conservative rasterization. |

| Preset | Description |
|---|---|
| `CullNone` | Defaults with `CullMode.None`. |
| `CullBack` | The defaults. |
| `CullFront` | Defaults with `CullMode.Front`. |
| `Wireframe` | `FillMode.Wireframe` with `CullMode.None`. |

## FillMode
`public enum FillMode { Wireframe = 2, Solid = 3 }`

## CullMode
`public enum CullMode { None = 1, Front = 2, Back = 3 }`

## DepthStencilState
`public sealed record DepthStencilState`

| Property | Default | Meaning |
|---|---|---|
| `bool DepthEnable { get; init; }` | `true` | Depth test. |
| `bool DepthWriteEnable { get; init; }` | `true` | Depth writes. |
| `ComparisonFunction DepthFunction { get; init; }` | `LessEqual` | Depth comparison. |
| `bool StencilEnable { get; init; }` | `false` | Stencil test. |
| `byte StencilReadMask { get; init; }` | `0xFF` | Mask applied before the comparison. |
| `byte StencilWriteMask { get; init; }` | `0xFF` | Mask applied to writes. |
| `StencilFaceDescription FrontFace { get; init; }` | `new()` | Stencil for front faces. |
| `StencilFaceDescription BackFace { get; init; }` | `new()` | Stencil for back faces. |

The stencil reference is `GraphicsDevice.StencilReference`.

| Preset | Description |
|---|---|
| `Default` | The defaults. |
| `DepthRead` | Test without writing. |
| `None` | No test, no writing. |
| `ReverseZ` | `GreaterEqual`, for a depth buffer cleared to 0 with far at 0. |
| `ReverseZRead` | `GreaterEqual` without writing. |

## StencilFaceDescription
`public readonly record struct StencilFaceDescription`

| Property | Default |
|---|---|
| `StencilOperation FailOperation { get; init; }` | `Keep` |
| `StencilOperation DepthFailOperation { get; init; }` | `Keep` |
| `StencilOperation PassOperation { get; init; }` | `Keep` |
| `ComparisonFunction Function { get; init; }` | `Always` |

## StencilOperation
`public enum StencilOperation { Keep = 1, Zero = 2, Replace = 3, IncrementSaturate = 4, DecrementSaturate = 5, Invert = 6, Increment = 7, Decrement = 8 }`

## VertexLayout
`public sealed class VertexLayout`

| Member | Meaning |
|---|---|
| `const int MaximumSlots = 16` | Vertex buffer slots. |
| `VertexLayout(params ReadOnlySpan<VertexElement> elements)` | Throws `ArgumentException` when there are no elements, a semantic is empty, a slot is outside 0 to 15, or an offset, semantic index or step rate is negative. |
| `IReadOnlyList<VertexElement> Elements { get; }` | The elements. |

The stride of each slot is the stride of the buffer bound there (or `sizeof(T)` for `SetVertexData<T>`). A draw throws `InvalidOperationException` when a slot the layout uses has nothing bound.

## VertexElement
`public readonly record struct VertexElement(string Semantic, int SemanticIndex, VertexElementFormat Format, int Offset, int Slot = 0, int InstanceStepRate = 0)`

| Member | Meaning |
|---|---|
| `string Semantic` | HLSL semantic name without the index, such as `"POSITION"` or `"TEXCOORD"`. |
| `int SemanticIndex` | The index, so `TEXCOORD1` is `("TEXCOORD", 1)`. |
| `VertexElementFormat Format` | The data format. |
| `int Offset` | Byte offset in the vertex. |
| `int Slot` | Vertex buffer slot, 0 to 15. |
| `int InstanceStepRate` | 0 for per-vertex data. N > 0 advances once every N instances. |
| `static int SizeOf(VertexElementFormat format)` | Bytes of one element. |

## VertexElementFormat
`public enum VertexElementFormat`

| Member | HLSL type | Bytes |
|---|---|---|
| `Single = 0` | `float` | 4 |
| `Vector2 = 1` | `float2` | 8 |
| `Vector3 = 2` | `float3` | 12 |
| `Vector4 = 3` | `float4` | 16 |
| `Color = 4` | `float4`, 4 unsigned bytes normalized to 0–1 | 4 |
| `Byte4 = 5` | `uint4`, 4 unsigned bytes | 4 |
| `Short2 = 6` | `int2`, 2 signed shorts | 4 |
| `Short4 = 7` | `int4`, 4 signed shorts | 8 |
| `NormalizedShort2 = 8` | `float2`, signed shorts normalized to -1–1 | 4 |
| `NormalizedShort4 = 9` | `float4`, signed shorts normalized to -1–1 | 8 |
| `HalfVector2 = 10` | `float2`, two halves | 4 |
| `HalfVector4 = 11` | `float4`, four halves | 8 |
| `UInt = 12` | `uint` | 4 |
| `UInt2 = 13` | `uint2` | 8 |
| `UInt4 = 14` | `uint4` | 16 |

## Vertex types
Four record structs with `Pack = 1` and a static `VertexLayout Layout` on slot 0.

| Type | Fields and byte offsets |
|---|---|
| `VertexPositionColor(Vector3 Position, Color Color)` | `POSITION` 0, `COLOR` 12 |
| `VertexPositionTexture(Vector3 Position, Vector2 TextureCoordinate)` | `POSITION` 0, `TEXCOORD` 12 |
| `VertexPositionColorTexture(Vector3 Position, Color Color, Vector2 TextureCoordinate)` | `POSITION` 0, `COLOR` 12, `TEXCOORD` 16 |
| `VertexPositionNormalTexture(Vector3 Position, Vector3 Normal, Vector2 TextureCoordinate)` | `POSITION` 0, `NORMAL` 12, `TEXCOORD` 24 |

## Viewport
`public readonly record struct Viewport(float X, float Y, float Width, float Height, float MinDepth = 0f, float MaxDepth = 1f)`

| Member | Meaning |
|---|---|
| `Viewport(Rectangle bounds)` | The rectangle with depth 0 to 1. |
| `float AspectRatio { get; }` | `Width / Height`, or 0 when `Height` is 0. |
| `Rectangle Bounds { get; }` | The position and size truncated to integers. |

## Indirect arguments
Record structs laid out as the GPU reads them from a buffer with `BufferUsage.Indirect`.

| Type | Fields |
|---|---|
| `IndirectDrawArguments(uint VertexCountPerInstance, uint InstanceCount, uint StartVertex, uint StartInstance)` | 16 bytes, for `DrawInstancedIndirect`. |
| `IndirectDrawIndexedArguments(uint IndexCountPerInstance, uint InstanceCount, uint StartIndex, int BaseVertex, uint StartInstance)` | 20 bytes, for `DrawIndexedInstancedIndirect`. |
| `IndirectDispatchArguments(uint ThreadGroupCountX, uint ThreadGroupCountY, uint ThreadGroupCountZ)` | 12 bytes, for `DispatchIndirect`. |
