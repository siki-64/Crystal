# Crystal.Graphics: shaders

Shader libraries, programs, their parameters and constant buffers, and the descriptions for building programs from bytecode at run time. The README's "Shaders and drawing" section covers the `.hlsl` build.

**Threads.** Libraries can be created on any thread. `SetValue`, `SetData` and `SetBuffer` run on the render thread, or on a thread with a bound `GraphicsCommandList`, where they change that list's values only. Values live in CPU memory until a draw or dispatch uploads the changed constant buffers.

## ShaderLibrary
`public sealed class ShaderLibrary : GraphicsResource`

A set of programs, from a `.crystalshader` file the build wrote or from runtime descriptions.

| Member | Meaning |
|---|---|
| `ShaderLibrary(GraphicsDevice graphicsDevice, Stream stream, string? name = null)` | Reads a `.crystalshader` library, such as an embedded resource stream. |
| `ShaderLibrary(GraphicsDevice graphicsDevice, ReadOnlySpan<ShaderProgramDescription> programs, string? name = null)` | Builds programs from bytecode and caller-supplied reflection. See the checks below. |
| `static ShaderLibrary Load(GraphicsDevice graphicsDevice, string path)` | Opens a `.crystalshader` file. A relative path is resolved against `AppContext.BaseDirectory`. The name is the file name without extension. |
| `IReadOnlyList<ShaderProgram> Programs { get; }` | The programs in file or description order. |
| `ShaderProgram GetProgram(string name)` | The named program. Throws `KeyNotFoundException`, listing the available names, when there is none. |
| `bool TryGetProgram(string name, out ShaderProgram? program)` | The named program, or `false`. |

Programs from a `.crystalshader` file use the device's shared descriptor-indexed root signatures. Programs from descriptions bind their registers directly, and creating their library builds one root signature per program. Disposing it releases them once the GPU is done; pipelines can no longer be created from its programs, and drawing with pipelines built from them throws `ObjectDisposedException`.

**The description constructor throws** `ArgumentException` when:
- a description is `null`, has no name, or shares its name with another;
- a program has a compute shader next to a vertex or pixel shader, or has neither a vertex nor a compute shader;
- bytecode isn't a DXBC or DXIL container;
- a compute program's thread group size has a side below 1, or a non-compute program has a thread group size or a non-compute program without a vertex shader has vertex inputs;
- a constant buffer or resource has no name or shares its name with another of the same program, or a value and a resource share a name;
- a constant buffer is not 1 byte to 64 KiB, or a variable lies outside its buffer, has negative elements, or has rows or columns outside 1 to 4;
- a shared value name has declarations that differ in size, class, rows, columns or elements;
- a resource's dimension doesn't fit its kind;
- an entry is visible to no stage or to a stage the program lacks;
- a register or space is negative, or two entries share a register class, register, space and stage;
- a vertex input has no semantic or a negative index or register;
- a program needs more than 64 root signature DWORDs (2 per constant buffer, 1 per texture, buffer or sampler).

It throws `GraphicsException` when D3D12 rejects a root signature.

## ShaderProgram
`public sealed class ShaderProgram`

| Member | Meaning |
|---|---|
| `string Name { get; }` | The program name from `#pragma program` or the description. |
| `ShaderLibrary Library { get; }` | The owning library. |
| `ShaderParameterCollection Parameters { get; }` | Every constant buffer value and resource the program uses. |
| `ShaderConstantBufferCollection ConstantBuffers { get; }` | The constant buffers. |
| `bool IsCompute { get; }` | `true` for compute programs. |
| `ThreadGroupSize ThreadGroupSize { get; }` | The `[numthreads]` size of a compute program; zero otherwise. |

Sampler parameters start as `LinearWrap`. Texture and buffer parameters start unset, and drawing with one unset throws `InvalidOperationException`. Parameter values belong to the program, so every pipeline built from it sees them. A bound `GraphicsCommandList` has values of its own (see [command lists](graphics-device.md#graphicscommandlist)).

## ShaderParameterCollection
`public sealed class ShaderParameterCollection : IReadOnlyList<ShaderParameter>`

| Member | Meaning |
|---|---|
| `int Count { get; }` | Number of parameters. |
| `ShaderParameter this[int index] { get; }` | By index. |
| `ShaderParameter? this[string name] { get; }` | By name, or `null` when the program doesn't use that name. Look parameters up once and keep them. |
| `IEnumerator<ShaderParameter> GetEnumerator()` | All parameters. |

## ShaderParameter
`public sealed class ShaderParameter`

A value in a constant buffer, or a texture, buffer or sampler binding.

| Member | Meaning |
|---|---|
| `string Name { get; }` | The HLSL name. |
| `ShaderParameterKind Kind { get; }` | What it binds. |
| `ShaderProgram Program { get; }` | The owning program. |
| `ShaderConstantBuffer? ConstantBuffer { get; }` | For values: the constant buffer of the first declaration. `null` for resources. |
| `int Offset { get; }` | For values: byte offset of the first declaration in its buffer. |
| `int SizeInBytes { get; }` | For values: size in bytes. |
| `int Rows { get; }`, `int Columns { get; }` | For values: rows and columns (1 for scalars). |
| `int Elements { get; }` | For arrays: element count. 0 when not an array. |
| `object? Value { get; }` | For resources: the bound `Texture`, `TextureView`, `GraphicsBuffer` or `SamplerState`. `null` for values. |

### SetValue
A value declared in several constant buffers (typically one per stage) is one parameter, and `SetValue` writes every declaration.

| Overload | Accepts |
|---|---|
| `SetValue(float)`, `SetValue(int)`, `SetValue(uint)`, `SetValue(bool)` | Scalars. `bool` writes 1 or 0 as a `uint`. |
| `SetValue(Vector2)`, `SetValue(Vector3)`, `SetValue(Vector4)` | Vectors. |
| `SetValue(Color)` | A `float4` from `Color.ToVector4()`. |
| `SetValue(in Matrix4x4)` | Matrices. A smaller matrix such as `float3x4` takes the top-left block. `column_major` matrices are transposed while written. |
| `SetValue(ReadOnlySpan<float>)` | `float`, `float2` or `float3` arrays, packed into 16-byte registers element by element; other values byte for byte. |
| `SetValue(ReadOnlySpan<Vector4>)` | `float4` arrays and other values, byte for byte. |
| `SetValue(ReadOnlySpan<Matrix4x4>)` | Matrix arrays. |
| `SetValue(Texture?)` | `Texture2D`, `Texture2DArray`, `TextureCube` and their `RW` forms. A texture shows all its mips, or mip 0 to a read-write parameter. |
| `SetValue(TextureView?)` | The same, with one mip. |
| `SetValue(GraphicsBuffer?)` | `StructuredBuffer`, `ByteAddressBuffer` and their `RW` forms. |
| `SetValue(SamplerState?)` | `SamplerState` and `SamplerComparisonState`. |

**Throws**
- `InvalidOperationException` when the overload doesn't fit the kind: a value overload on a resource, a resource overload on a value or on another resource kind, a matrix overload on a non-matrix, a `float` span on an array that is neither scalar nor vector.
- `ArgumentException` when the data is larger than the value or the array; when a texture lacks `ShaderResource` (or `UnorderedAccess` for read-write parameters) or its dimension differs from the declaration (a cube to a `Texture2D`, an array to a non-array); when a buffer lacks the usage flags of the parameter kind (see [BufferUsage](graphics-resources.md#bufferusage)).

Setting values allocates no managed memory.

## ShaderParameterKind
`public enum ShaderParameterKind { Value = 0, Texture = 1, ReadWriteTexture = 2, Buffer = 3, ReadWriteBuffer = 4, Sampler = 5 }`

## ShaderConstantBufferCollection
`public sealed class ShaderConstantBufferCollection : IReadOnlyList<ShaderConstantBuffer>`

| Member | Meaning |
|---|---|
| `int Count { get; }` | Number of constant buffers. |
| `ShaderConstantBuffer this[int index] { get; }` | By index. |
| `ShaderConstantBuffer? this[string name] { get; }` | By name, or `null`. |
| `IEnumerator<ShaderConstantBuffer> GetEnumerator()` | All constant buffers. |

## ShaderConstantBuffer
`public sealed class ShaderConstantBuffer`

| Member | Meaning |
|---|---|
| `string Name { get; }` | The `cbuffer` name. |
| `int Size { get; }` | Size in bytes. |
| `ShaderProgram Program { get; }` | The owning program. |
| `GraphicsBuffer? Buffer { get; }` | The buffer set with `SetBuffer`, or `null`. |
| `long BufferOffset { get; }` | The offset set with `SetBuffer`. |
| `void SetValue<T>(in T value) where T : unmanaged` | Writes a struct at offset 0. Its layout must match the HLSL packing. |
| `void SetData<T>(ReadOnlySpan<T> data, int offsetInBytes = 0) where T : unmanaged` | Writes bytes at an offset. Throws `ArgumentOutOfRangeException` when they don't fit. |
| `void SetBuffer(GraphicsBuffer? buffer, long offsetInBytes = 0)` | Reads the constant buffer from a `GraphicsBuffer` instead of the CPU copy. The buffer needs `BufferUsage.Constant`, the offset must be a multiple of 256, and `Size` bytes must fit from it. `null` returns to the CPU copy. |

Without a buffer, a draw uploads the CPU copy of the recording it is in to the per-frame upload ring when it changed, and reuses the previous upload otherwise.

## Runtime program descriptions
Bytecode that doesn't come from the shader build becomes a library through `ShaderProgramDescription`. The caller supplies the reflection the build would otherwise write. Bindings go by register, space and stage, so names are free; each constant buffer name and each resource name is unique within a program.

### ShaderProgramDescription
`public sealed record ShaderProgramDescription`

| Property | Default | Meaning |
|---|---|---|
| `required string Name { get; init; }` | — | Program name, unique in the library. |
| `ReadOnlyMemory<byte> VertexShader { get; init; }` | empty | DXIL or DXBC (shader model 5.0 and later) container. |
| `ReadOnlyMemory<byte> PixelShader { get; init; }` | empty | Needs a vertex shader. May be empty for depth-only programs. |
| `ReadOnlyMemory<byte> ComputeShader { get; init; }` | empty | Excludes the other two. |
| `ThreadGroupSize ThreadGroupSize { get; init; }` | (0, 0, 0) | The `[numthreads]` size of the compute shader. |
| `IReadOnlyList<ShaderConstantBufferDescription> ConstantBuffers { get; init; }` | `[]` | Constant buffers. |
| `IReadOnlyList<ShaderResourceDescription> Resources { get; init; }` | `[]` | Textures, buffers and samplers. |
| `IReadOnlyList<ShaderVertexInputDescription> VertexInputs { get; init; }` | `[]` | Inputs of the vertex shader. |
| `bool UsesDescriptorIndices { get; init; }` | `false` | The bytecode reads its resources through descriptor indices, as `RuntimeShaderCompiler.CompileStage` produces. The program then uses the device's shared root signature: constant buffers must be in space 0 below `b4` (`b8` for compute), and each resource gives its slot per stage. |

### ShaderConstantBufferDescription
`public sealed record ShaderConstantBufferDescription(string Name, int Register, int Size, ShaderStages Stages, IReadOnlyList<ShaderVariableDescription> Variables, int Space = 0)`

A `cbuffer` at register `b{Register}` in `space{Space}`, `Size` bytes (at most 64 KiB), visible to `Stages`. Values with the same name in several buffers become one shared parameter; every declaration must have the same size, class, rows, columns and elements.

### ShaderVariableDescription
`public sealed record ShaderVariableDescription(string Name, int Offset, int Size, ShaderVariableClass Class, int Rows, int Columns, int Elements = 0)`

A value at a byte offset in its buffer. `Rows` and `Columns` are 1 for scalars, 1 and N for vectors. `Elements` is the array length, 0 when not an array. The variable must lie inside the buffer.

### ShaderVariableClass
`public enum ShaderVariableClass { Scalar = 0, Vector = 1, MatrixRows = 2, MatrixColumns = 3, Struct = 4 }`

`MatrixRows` is row-major storage, `MatrixColumns` column-major.

### ShaderResourceDescription
`public sealed record ShaderResourceDescription(string Name, ShaderResourceKind Kind, ShaderResourceDimension Dimension, int Register, ShaderStages Stages, int Space = 0, int VertexDescriptorSlot = -1, int PixelDescriptorSlot = -1, int ComputeDescriptorSlot = -1)`

A resource at register `t`, `u` or `s{Register}` (by kind) in `space{Space}`, visible to `Stages`. Samplers use `ShaderResourceDimension.None`, buffers use `Buffer`.

The descriptor slots apply only with `UsesDescriptorIndices`: the slot the stage reads the resource's index from, 0 to 15 for vertex shaders and 0 to 31 for pixel and compute shaders, or -1 when that stage doesn't read it. Slots are unique per stage. A slot for a stage the resource isn't visible to throws.

### ShaderResourceKind
`public enum ShaderResourceKind { Texture = 0, StructuredBuffer = 1, ByteAddressBuffer = 2, ReadWriteTexture = 3, ReadWriteStructuredBuffer = 4, ReadWriteByteAddressBuffer = 5, Sampler = 6 }`

### ShaderResourceDimension
`public enum ShaderResourceDimension { None = 0, Buffer = 1, Texture1D = 2, Texture1DArray = 3, Texture2D = 4, Texture2DArray = 5, Texture2DMS = 6, Texture2DMSArray = 7, Texture3D = 8, TextureCube = 9, TextureCubeArray = 10 }`

Crystal creates `Texture2D`, `Texture2DArray`, `Texture2DMS`, `Texture2DMSArray`, `TextureCube` and `TextureCubeArray` views. The other dimensions can be declared but no Crystal texture fits them.

### ShaderVertexInputDescription
`public sealed record ShaderVertexInputDescription(string Semantic, int SemanticIndex, int Register)`

A vertex shader input: semantic name without index, semantic index, and input register. Pipelines check that their vertex layout provides every input.

### ShaderStages
`[Flags] public enum ShaderStages { None = 0, Vertex = 1, Pixel = 2, Compute = 4 }`

## ThreadGroupSize
`public readonly record struct ThreadGroupSize(int X, int Y, int Z)`

The `[numthreads]` size of a compute shader.

## RuntimeShaderCompiler
`public sealed class RuntimeShaderCompiler : IDisposable` in `Crystal.ShaderCompiler`. Set `CrystalRuntimeShaderCompiler` to `true` in a project that imports `Crystal.Shaders.targets` to reference it.

| Member | Description |
| --- | --- |
| `RuntimeShaderCompiler(IEnumerable<string>? includeDirectories = null, IEnumerable<string>? defines = null, bool columnMajorMatrices = false)` | Creates a DXC instance. Matrices are row-major like the build step unless `columnMajorMatrices` is set, which suits HLSL written for the D3D default. Defines are `NAME=VALUE`. Not thread-safe; use one per thread. |
| `static string Version { get; }` | Identifies the compiler build. Changes whenever the compiler is rebuilt; use it in cache keys. |
| `ShaderCompilation Compile(string sourceName, string source)` | Compiles one source with its `#pragma program` lines, exactly as the build step does. `sourceName` appears in diagnostics and anchors relative includes. |
| `ShaderStageCompilation CompileStage(string sourceName, string source, string entryPoint, ShaderStage stage)` | Compiles one entry point with descriptor indices, for a `ShaderProgramDescription` with `UsesDescriptorIndices`. Resources must be global declarations, one per line. |

`public sealed record ShaderCompilation(byte[]? Library, IReadOnlyList<string> Errors, IReadOnlyList<string> Warnings)`: `Library` is a `.crystalshader` image for `ShaderLibrary(GraphicsDevice, Stream)`, or `null` when `Errors` is not empty. `Succeeded` is `Library != null`.

`public sealed record ShaderStageCompilation(byte[]? Bytecode, IReadOnlyList<ShaderDescriptorSlot> DescriptorSlots, IReadOnlyList<string> Errors, IReadOnlyList<string> Warnings)`: `Bytecode` is DXIL, or `null` when `Errors` is not empty. `DescriptorSlots` lists every resource the stage reads, `ShaderDescriptorSlot(string Name, char RegisterClass, int Register, int Space, int Slot)` with `RegisterClass` `t`, `u` or `s`. Resources the stage never reads are absent. `ShaderStage` is `Vertex`, `Pixel` or `Compute`.
