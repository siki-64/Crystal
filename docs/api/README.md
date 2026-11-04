# API reference

Every public type of `Crystal.Framework` and `Crystal.ImGui`, grouped by namespace. The [README](../../README.md) explains the concepts. These pages list each type and member with its signature, defaults, what it throws and which thread may call it.

| Page | Contents |
|---|---|
| [Crystal](crystal.md) | `Game`, `GameOptions`, `GameTime`, `GameWindow`, `Display`, `TextInputEventArgs`, `Color`, `Point`, `Rectangle`, bounding volumes |
| [Crystal.Graphics: device](graphics-device.md) | `GraphicsDevice`, options, adapter, debug messages, readback, async compute, queries, profiler, metrics |
| [Crystal.Graphics: resources](graphics-resources.md) | `GraphicsResource`, `GraphicsBuffer`, `Texture`, `Texture2D`, `TextureCube`, `TextureView`, samplers, formats |
| [Crystal.Graphics: pipelines](graphics-pipelines.md) | `GraphicsPipeline`, `ComputePipeline`, blend, rasterizer and depth-stencil states, vertex layouts and vertex types, `Viewport`, indirect arguments |
| [Crystal.Graphics: shaders](graphics-shaders.md) | `ShaderLibrary`, `ShaderProgram`, parameters, constant buffers, runtime program descriptions |
| [Crystal.Graphics: 2D](graphics-2d.md) | `SpriteBatch`, `SpriteFont`, `SpriteFontGlyph` |
| [Crystal.Input](input.md) | `Keyboard`, `Mouse`, `GamePad` and their states |
| [Crystal.ImGui](imgui.md) | `ImGuiRenderer`, the UTF-8 helpers, `ImVector<T>`, and the rules that map Dear ImGui's C API to the generated binding |

## Conventions
- **Signatures** use C# keywords. `in` parameters are written as `in`. Optional parameters show their default.
- **Records**: types marked *record* are C# records or record structs. They also have value equality (`Equals`, `==`, `!=`, `GetHashCode`), `ToString`, `with` expressions and, for positional records, `Deconstruct`. Those members are not listed again.
- **`init` properties** can only be set in an object initializer or a `with` expression.
- **Threads**: "render thread" is the thread that created the `GraphicsDevice`, which is the thread that runs `Game.Run`. "Any thread" means the member is safe to call from worker threads.
- **Frames**: "inside a frame" means between `Game.BeginDraw` and `Game.EndDraw`, which is during `Draw`.
- **Throws**: argument checks throw `ArgumentNullException`, `ArgumentOutOfRangeException` or `ArgumentException` with a message that names the problem. Members of a disposed object throw `ObjectDisposedException`. The tables list the conditions that are not obvious from the signature.

## Keeping it current
Change these pages in the same commit as any change to a public type or member.
