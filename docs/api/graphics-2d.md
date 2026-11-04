# Crystal.Graphics: 2D

Batched sprite and text drawing.

## SpriteBatch
`public sealed class SpriteBatch : IDisposable`

Collects sprites between `Begin` and `End` and draws them in as few draws as possible. Render thread; `End` (and every sprite in `Immediate` mode) draws, so it must run inside a frame.

| Member | Meaning |
|---|---|
| `const int MaximumSpritesPerDraw = 8192` | Sprites per indexed draw. Longer runs are split. |
| `SpriteBatch(GraphicsDevice graphicsDevice, SurfaceFormat? renderTargetFormat = null, SurfaceFormat? depthStencilFormat = null, int sampleCount = 1)` | Creates a batch for one set of target formats; `null` uses the back buffer format. Bakes the pipelines for `BlendState.AlphaBlend`, `NonPremultiplied`, `Additive` and `Opaque` with the default depth and rasterizer states. |
| `GraphicsDevice GraphicsDevice { get; }` | The device. |
| `void PreparePipeline(BlendState? blendState = null, DepthStencilState? depthStencilState = null, RasterizerState? rasterizerState = null, ShaderProgram? program = null)` | Bakes the pipeline for this combination now. Call it at load time for every combination other than the four prepared ones; otherwise `Begin` creates it on first use and stalls that frame. |
| `void Begin(SpriteSortMode sortMode = SpriteSortMode.Deferred, BlendState? blendState = null, SamplerState? samplerState = null, DepthStencilState? depthStencilState = null, RasterizerState? rasterizerState = null, ShaderProgram? program = null, Matrix4x4? transformMatrix = null)` | Starts a batch. Throws `InvalidOperationException` when a batch is already open. |
| `void End()` | Sorts and draws the batch. Throws `InvalidOperationException` without `Begin`. |
| `void Dispose()` | Releases the shaders, pipelines and index buffer. |

`Begin` defaults: `BlendState.AlphaBlend` (premultiplied colors), `SamplerDescription.LinearClamp`, `DepthStencilState.None`, `RasterizerState.CullNone`, the built-in program and the identity transform. The transform is applied before a projection that maps the current `GraphicsDevice.Viewport` in pixels, top left at (0, 0), to clip space. The batch reads the viewport when it draws.

### Draw
| Overload | Meaning |
|---|---|
| `Draw(Texture2D texture, Vector2 position, Color color)` | The whole texture at its size. |
| `Draw(Texture2D texture, Vector2 position, Rectangle? sourceRectangle, Color color)` | A region (`null` for the whole texture) at its size. |
| `Draw(Texture2D texture, Vector2 position, Rectangle? sourceRectangle, Color color, float rotation, Vector2 origin, float scale, SpriteEffects effects, float layerDepth)` | With rotation in radians around `origin` (in source pixels), a uniform scale, flips and a layer depth. |
| `Draw(Texture2D texture, Vector2 position, Rectangle? sourceRectangle, Color color, float rotation, Vector2 origin, Vector2 scale, SpriteEffects effects, float layerDepth)` | The same with a scale per axis. |
| `Draw(Texture2D texture, Rectangle destinationRectangle, Color color)` | The whole texture stretched to the rectangle. |
| `Draw(Texture2D texture, Rectangle destinationRectangle, Rectangle? sourceRectangle, Color color)` | A region stretched to the rectangle. |
| `Draw(Texture2D texture, Rectangle destinationRectangle, Rectangle? sourceRectangle, Color color, float rotation, Vector2 origin, SpriteEffects effects, float layerDepth)` | With rotation, origin, flips and layer depth. |

`color` multiplies the texture. With the default `AlphaBlend` it is a premultiplied color.

### DrawString
| Overload | Meaning |
|---|---|
| `DrawString(SpriteFont font, ReadOnlySpan<char> text, Vector2 position, Color color)` | Draws text; a `string` converts to the span. |
| `DrawString(SpriteFont font, ReadOnlySpan<char> text, Vector2 position, Color color, float rotation, Vector2 origin, float scale, SpriteEffects effects, float layerDepth)` | With rotation, origin, uniform scale, flips and layer depth. |
| `DrawString(SpriteFont font, ReadOnlySpan<char> text, Vector2 position, Color color, float rotation, Vector2 origin, Vector2 scale, SpriteEffects effects, float layerDepth)` | With a scale per axis. |
| `DrawString(SpriteFont font, StringBuilder text, ...)` | The same three overloads for a `StringBuilder`, read without copying. |

`\n` starts a new line and `\r` is ignored.

**Draw and DrawString throw** `InvalidOperationException` outside `Begin`/`End`, `ArgumentException` for a texture of another device, and `ArgumentException` for a character the font lacks when it has no `DefaultCharacter`.

### Custom programs
A program passed to `Begin` or `PreparePipeline` reads `POSITION` (`float3`), `COLOR` (`float4`) and `TEXCOORD0` (`float2`), the layout of `VertexPositionColorTexture`. The batch sets its `MatrixTransform` (`float4x4`), `Texture` and `TextureSampler` parameters when the program declares them; set any other parameter yourself before `End`. A program from another device throws `ArgumentException`.

Once warmed up, `Begin`, `Draw`, `DrawString` and `End` allocate no managed memory.

## SpriteSortMode
`public enum SpriteSortMode { Deferred = 0, Immediate = 1, Texture = 2, BackToFront = 3, FrontToBack = 4 }`

| Member | Meaning |
|---|---|
| `Deferred` | Draws in call order at `End`. |
| `Immediate` | Draws each sprite as it is added. |
| `Texture` | Groups sprites by texture at `End`. |
| `BackToFront` | Sorts by descending layer depth at `End`. |
| `FrontToBack` | Sorts by ascending layer depth at `End`. |

Sorting is stable, so sprites with equal keys keep their call order.

## SpriteEffects
`[Flags] public enum SpriteEffects { None = 0, FlipHorizontally = 1, FlipVertically = 2 }`

## SpriteFont
`public sealed class SpriteFont`

A bitmap font: a texture and the glyphs in it.

| Member | Meaning |
|---|---|
| `SpriteFont(Texture2D texture, ReadOnlySpan<SpriteFontGlyph> glyphs, int lineSpacing, float spacing = 0f, char? defaultCharacter = null)` | Throws `ArgumentException` when there are no glyphs, a character has two glyphs, a glyph lies outside the texture, or the default character has no glyph. |
| `Texture2D Texture { get; }` | The glyph texture. |
| `int LineSpacing { get; set; }` | Pixels from one line to the next. |
| `float Spacing { get; set; }` | Extra pixels between characters. |
| `IReadOnlyList<char> Characters { get; }` | The characters, in ascending order. |
| `IReadOnlyList<SpriteFontGlyph> Glyphs { get; }` | The glyphs, in the same order. |
| `char? DefaultCharacter { get; set; }` | Drawn for characters the font lacks. Throws `ArgumentException` when set to a character without a glyph. |
| `bool TryGetGlyph(char character, out SpriteFontGlyph glyph)` | The glyph of a character, or `false`. |
| `Vector2 MeasureString(ReadOnlySpan<char> text)` | Width of the widest line and height of all lines, as `DrawString` lays them out. (0, 0) for empty text. |
| `Vector2 MeasureString(StringBuilder text)` | The same for a `StringBuilder`. |

Glyphs are looked up per UTF-16 code unit.

## SpriteFontGlyph
`public readonly record struct SpriteFontGlyph(char Character, Rectangle BoundsInTexture, Rectangle Cropping, float LeftSideBearing, float Width, float RightSideBearing)`

| Field | Meaning |
|---|---|
| `Character` | The character. |
| `BoundsInTexture` | The glyph's pixels in the texture. |
| `Cropping` | `X` and `Y` offset the glyph from the pen position; `Width` and `Height` are the cell the glyph was cropped from. |
| `LeftSideBearing` | Pixels before the glyph. |
| `Width` | Advance of the glyph itself. |
| `RightSideBearing` | Pixels after the glyph. |

The pen advances by `Spacing + LeftSideBearing + Width + RightSideBearing` per character. The first character of a line starts at `max(LeftSideBearing, 0)` without `Spacing`. A line is `LineSpacing` high, or taller when a glyph's `Cropping.Height` is.
