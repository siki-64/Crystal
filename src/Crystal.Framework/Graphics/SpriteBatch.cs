using System.Numerics;
using System.Text;

namespace Crystal.Graphics;

public enum SpriteSortMode
{
    Deferred,
    Immediate,
    Texture,
    BackToFront,
    FrontToBack,
}

[Flags]
public enum SpriteEffects
{
    None = 0,
    FlipHorizontally = 1,
    FlipVertically = 2,
}

public sealed class SpriteBatch : IDisposable
{
    public const int MaximumSpritesPerDraw = 8192;

    private const string ShaderResourceName = "Shaders/SpriteBatch.crystalshader";
    private const int InitialSpriteCapacity = 256;

    private readonly ShaderLibrary _shaderLibrary;
    private readonly ShaderProgram _defaultProgram;
    private readonly GraphicsBuffer _indexBuffer;
    private readonly SurfaceFormat _renderTargetFormat;
    private readonly SurfaceFormat? _depthStencilFormat;
    private readonly int _sampleCount;
    private readonly SamplerState _defaultSampler;
    private readonly Dictionary<PipelineKey, SpritePipeline> _pipelines = [];
    private readonly Dictionary<Texture2D, int> _textureSortKeys = new(ReferenceEqualityComparer.Instance);
    private readonly VertexPositionColorTexture[] _vertices = new VertexPositionColorTexture[MaximumSpritesPerDraw * 4];
    private SpriteItem[] _sprites = new SpriteItem[InitialSpriteCapacity];
    private ulong[] _sortKeys = new ulong[InitialSpriteCapacity];
    private int _spriteCount;
    private bool _hasBegun;
    private SpriteSortMode _sortMode;
    private SpritePipeline? _pipeline;
    private SamplerState? _sampler;
    private Matrix4x4 _transform;
    private bool _isDisposed;

    public SpriteBatch(GraphicsDevice graphicsDevice, SurfaceFormat? renderTargetFormat = null, SurfaceFormat? depthStencilFormat = null, int sampleCount = 1)
    {
        ArgumentNullException.ThrowIfNull(graphicsDevice);
        GraphicsDevice = graphicsDevice;
        _renderTargetFormat = renderTargetFormat ?? graphicsDevice.BackBufferFormat;
        _depthStencilFormat = depthStencilFormat;
        _sampleCount = sampleCount;

        using (var stream = typeof(SpriteBatch).Assembly.GetManifestResourceStream(ShaderResourceName)
            ?? throw new InvalidOperationException($"The embedded shader '{ShaderResourceName}' is missing."))
        {
            _shaderLibrary = new ShaderLibrary(graphicsDevice, stream, "SpriteBatch");
        }
        _defaultProgram = _shaderLibrary.GetProgram("SpriteBatch");
        _defaultSampler = graphicsDevice.GetSamplerState(SamplerDescription.LinearClamp);

        var indices = new ushort[MaximumSpritesPerDraw * 6];
        for (var sprite = 0; sprite < MaximumSpritesPerDraw; sprite++)
        {
            var vertex = (ushort)(sprite * 4);
            var index = sprite * 6;
            indices[index] = vertex;
            indices[index + 1] = (ushort)(vertex + 1);
            indices[index + 2] = (ushort)(vertex + 2);
            indices[index + 3] = (ushort)(vertex + 1);
            indices[index + 4] = (ushort)(vertex + 3);
            indices[index + 5] = (ushort)(vertex + 2);
        }
        _indexBuffer = new GraphicsBuffer(graphicsDevice, new BufferDescription { Size = indices.Length * sizeof(ushort), Stride = sizeof(ushort), Usage = BufferUsage.Index, Name = "SpriteBatchIndices" });
        _indexBuffer.SetData<ushort>(indices);

        PreparePipeline(BlendState.AlphaBlend);
        PreparePipeline(BlendState.NonPremultiplied);
        PreparePipeline(BlendState.Additive);
        PreparePipeline(BlendState.Opaque);
    }

    public GraphicsDevice GraphicsDevice { get; }

    public void PreparePipeline(BlendState? blendState = null, DepthStencilState? depthStencilState = null, RasterizerState? rasterizerState = null, ShaderProgram? program = null)
    {
        ObjectDisposedException.ThrowIf(_isDisposed, this);
        GetPipeline(new PipelineKey(
            program ?? _defaultProgram,
            blendState ?? BlendState.AlphaBlend,
            depthStencilState ?? DepthStencilState.None,
            rasterizerState ?? RasterizerState.CullNone));
    }

    public void Begin(
        SpriteSortMode sortMode = SpriteSortMode.Deferred,
        BlendState? blendState = null,
        SamplerState? samplerState = null,
        DepthStencilState? depthStencilState = null,
        RasterizerState? rasterizerState = null,
        ShaderProgram? program = null,
        Matrix4x4? transformMatrix = null)
    {
        ObjectDisposedException.ThrowIf(_isDisposed, this);
        if (_hasBegun) throw new InvalidOperationException("Begin was called twice without End.");

        _pipeline = GetPipeline(new PipelineKey(
            program ?? _defaultProgram,
            blendState ?? BlendState.AlphaBlend,
            depthStencilState ?? DepthStencilState.None,
            rasterizerState ?? RasterizerState.CullNone));
        _sortMode = sortMode;
        _sampler = samplerState ?? _defaultSampler;
        _transform = transformMatrix ?? Matrix4x4.Identity;
        _hasBegun = true;
    }

    public void End()
    {
        if (!_hasBegun) throw new InvalidOperationException("End was called without Begin.");
        try
        {
            if (_sortMode != SpriteSortMode.Immediate) Flush();
        }
        finally
        {
            _spriteCount = 0;
            _textureSortKeys.Clear();
            _hasBegun = false;
            _pipeline = null;
            _sampler = null;
        }
    }

    public void Draw(Texture2D texture, Vector2 position, Color color) =>
        Draw(texture, position, null, color, 0f, Vector2.Zero, Vector2.One, SpriteEffects.None, 0f);

    public void Draw(Texture2D texture, Vector2 position, Rectangle? sourceRectangle, Color color) =>
        Draw(texture, position, sourceRectangle, color, 0f, Vector2.Zero, Vector2.One, SpriteEffects.None, 0f);

    public void Draw(Texture2D texture, Vector2 position, Rectangle? sourceRectangle, Color color, float rotation, Vector2 origin, float scale, SpriteEffects effects, float layerDepth) =>
        Draw(texture, position, sourceRectangle, color, rotation, origin, new Vector2(scale), effects, layerDepth);

    public void Draw(Texture2D texture, Vector2 position, Rectangle? sourceRectangle, Color color, float rotation, Vector2 origin, Vector2 scale, SpriteEffects effects, float layerDepth)
    {
        CheckDraw(texture);
        var source = sourceRectangle ?? new Rectangle(0, 0, texture.Width, texture.Height);
        Enqueue(texture, position, source, source.Width * scale.X, source.Height * scale.Y, origin * scale, color, rotation, effects, layerDepth);
    }

    public void Draw(Texture2D texture, Rectangle destinationRectangle, Color color) =>
        Draw(texture, destinationRectangle, null, color, 0f, Vector2.Zero, SpriteEffects.None, 0f);

    public void Draw(Texture2D texture, Rectangle destinationRectangle, Rectangle? sourceRectangle, Color color) =>
        Draw(texture, destinationRectangle, sourceRectangle, color, 0f, Vector2.Zero, SpriteEffects.None, 0f);

    public void Draw(Texture2D texture, Rectangle destinationRectangle, Rectangle? sourceRectangle, Color color, float rotation, Vector2 origin, SpriteEffects effects, float layerDepth)
    {
        CheckDraw(texture);
        var source = sourceRectangle ?? new Rectangle(0, 0, texture.Width, texture.Height);
        var originScale = new Vector2(
            source.Width == 0 ? 0f : destinationRectangle.Width / (float)source.Width,
            source.Height == 0 ? 0f : destinationRectangle.Height / (float)source.Height);
        Enqueue(texture, new Vector2(destinationRectangle.X, destinationRectangle.Y), source, destinationRectangle.Width, destinationRectangle.Height, origin * originScale, color, rotation, effects, layerDepth);
    }

    public void DrawString(SpriteFont font, ReadOnlySpan<char> text, Vector2 position, Color color) =>
        DrawString(font, text, position, color, 0f, Vector2.Zero, Vector2.One, SpriteEffects.None, 0f);

    public void DrawString(SpriteFont font, ReadOnlySpan<char> text, Vector2 position, Color color, float rotation, Vector2 origin, float scale, SpriteEffects effects, float layerDepth) =>
        DrawString(font, text, position, color, rotation, origin, new Vector2(scale), effects, layerDepth);

    public void DrawString(SpriteFont font, ReadOnlySpan<char> text, Vector2 position, Color color, float rotation, Vector2 origin, Vector2 scale, SpriteEffects effects, float layerDepth)
    {
        CheckDraw(font);
        var layout = new GlyphLayout(font, position, color, rotation, origin, scale, effects, layerDepth, effects == SpriteEffects.None ? Vector2.Zero : font.MeasureString(text));
        layout.Draw(this, text);
    }

    public void DrawString(SpriteFont font, StringBuilder text, Vector2 position, Color color) =>
        DrawString(font, text, position, color, 0f, Vector2.Zero, Vector2.One, SpriteEffects.None, 0f);

    public void DrawString(SpriteFont font, StringBuilder text, Vector2 position, Color color, float rotation, Vector2 origin, float scale, SpriteEffects effects, float layerDepth) =>
        DrawString(font, text, position, color, rotation, origin, new Vector2(scale), effects, layerDepth);

    public void DrawString(SpriteFont font, StringBuilder text, Vector2 position, Color color, float rotation, Vector2 origin, Vector2 scale, SpriteEffects effects, float layerDepth)
    {
        CheckDraw(font);
        ArgumentNullException.ThrowIfNull(text);
        var layout = new GlyphLayout(font, position, color, rotation, origin, scale, effects, layerDepth, effects == SpriteEffects.None ? Vector2.Zero : font.MeasureString(text));
        foreach (var chunk in text.GetChunks()) layout.Draw(this, chunk.Span);
    }

    public void Dispose()
    {
        if (_isDisposed) return;
        _isDisposed = true;
        foreach (var pipeline in _pipelines.Values) pipeline.Pipeline.Dispose();
        _pipelines.Clear();
        _indexBuffer.Dispose();
        _shaderLibrary.Dispose();
    }

    private void CheckDraw(Texture2D texture)
    {
        ArgumentNullException.ThrowIfNull(texture);
        if (!_hasBegun) throw new InvalidOperationException("Begin must be called before drawing.");
        if (texture.GraphicsDevice != GraphicsDevice) throw new ArgumentException("The texture belongs to another GraphicsDevice.", nameof(texture));
    }

    private void CheckDraw(SpriteFont font)
    {
        ArgumentNullException.ThrowIfNull(font);
        CheckDraw(font.Texture);
    }

    private void Enqueue(Texture2D texture, Vector2 position, Rectangle source, float width, float height, Vector2 origin, Color color, float rotation, SpriteEffects effects, float layerDepth)
    {
        if (_spriteCount == _sprites.Length)
        {
            Array.Resize(ref _sprites, _sprites.Length * 2);
            Array.Resize(ref _sortKeys, _sortKeys.Length * 2);
        }

        var inverseWidth = 1f / texture.Width;
        var inverseHeight = 1f / texture.Height;
        var left = source.X * inverseWidth;
        var right = source.Right * inverseWidth;
        var top = source.Y * inverseHeight;
        var bottom = source.Bottom * inverseHeight;
        if ((effects & SpriteEffects.FlipHorizontally) != 0) (left, right) = (right, left);
        if ((effects & SpriteEffects.FlipVertically) != 0) (top, bottom) = (bottom, top);

        ref var sprite = ref _sprites[_spriteCount];
        sprite.Texture = texture;
        if (rotation == 0f)
        {
            var x = position.X - origin.X;
            var y = position.Y - origin.Y;
            sprite.TopLeft = new VertexPositionColorTexture(new Vector3(x, y, layerDepth), color, new Vector2(left, top));
            sprite.TopRight = new VertexPositionColorTexture(new Vector3(x + width, y, layerDepth), color, new Vector2(right, top));
            sprite.BottomLeft = new VertexPositionColorTexture(new Vector3(x, y + height, layerDepth), color, new Vector2(left, bottom));
            sprite.BottomRight = new VertexPositionColorTexture(new Vector3(x + width, y + height, layerDepth), color, new Vector2(right, bottom));
        }
        else
        {
            var (sin, cos) = MathF.SinCos(rotation);
            var x = -origin.X;
            var y = -origin.Y;
            sprite.TopLeft = new VertexPositionColorTexture(Rotate(position, x, y, sin, cos, layerDepth), color, new Vector2(left, top));
            sprite.TopRight = new VertexPositionColorTexture(Rotate(position, x + width, y, sin, cos, layerDepth), color, new Vector2(right, top));
            sprite.BottomLeft = new VertexPositionColorTexture(Rotate(position, x, y + height, sin, cos, layerDepth), color, new Vector2(left, bottom));
            sprite.BottomRight = new VertexPositionColorTexture(Rotate(position, x + width, y + height, sin, cos, layerDepth), color, new Vector2(right, bottom));
        }

        _sortKeys[_spriteCount] = _sortMode switch
        {
            SpriteSortMode.Texture => SortKey((uint)GetTextureSortKey(texture), _spriteCount),
            SpriteSortMode.BackToFront => SortKey(~SortableDepth(layerDepth), _spriteCount),
            SpriteSortMode.FrontToBack => SortKey(SortableDepth(layerDepth), _spriteCount),
            _ => (ulong)_spriteCount,
        };
        _spriteCount++;

        if (_sortMode == SpriteSortMode.Immediate)
        {
            Flush();
            _spriteCount = 0;
        }
    }

    private static Vector3 Rotate(Vector2 position, float x, float y, float sin, float cos, float depth) =>
        new(position.X + x * cos - y * sin, position.Y + x * sin + y * cos, depth);

    private static ulong SortKey(uint primary, int index) => ((ulong)primary << 32) | (uint)index;

    private static uint SortableDepth(float depth)
    {
        var bits = BitConverter.SingleToUInt32Bits(depth);
        return (bits & 0x80000000u) != 0 ? ~bits : bits | 0x80000000u;
    }

    private int GetTextureSortKey(Texture2D texture)
    {
        if (!_textureSortKeys.TryGetValue(texture, out var key))
        {
            key = _textureSortKeys.Count;
            _textureSortKeys.Add(texture, key);
        }
        return key;
    }

    private void Flush()
    {
        if (_spriteCount == 0) return;
        if (_sortMode is SpriteSortMode.Texture or SpriteSortMode.BackToFront or SpriteSortMode.FrontToBack)
            _sortKeys.AsSpan(0, _spriteCount).Sort();

        var pipeline = _pipeline!;
        var viewport = GraphicsDevice.Viewport;
        var projection = Matrix4x4.CreateOrthographicOffCenter(0f, viewport.Width, viewport.Height, 0f, 0f, -1f);
        GraphicsDevice.SetPipeline(pipeline.Pipeline);
        GraphicsDevice.SetIndexBuffer(_indexBuffer);
        pipeline.MatrixTransform?.SetValue(_transform * projection);
        pipeline.TextureSampler?.SetValue(_sampler);

        var start = 0;
        while (start < _spriteCount)
        {
            var texture = _sprites[(int)_sortKeys[start]].Texture!;
            var count = 0;
            while (start + count < _spriteCount && count < MaximumSpritesPerDraw)
            {
                ref var sprite = ref _sprites[(int)_sortKeys[start + count]];
                if (sprite.Texture != texture) break;
                var vertex = count * 4;
                _vertices[vertex] = sprite.TopLeft;
                _vertices[vertex + 1] = sprite.TopRight;
                _vertices[vertex + 2] = sprite.BottomLeft;
                _vertices[vertex + 3] = sprite.BottomRight;
                count++;
            }

            pipeline.Texture?.SetValue(texture);
            GraphicsDevice.SetVertexData<VertexPositionColorTexture>(_vertices.AsSpan(0, count * 4));
            GraphicsDevice.DrawIndexed(count * 6);
            start += count;
        }

        for (var index = 0; index < _spriteCount; index++) _sprites[index].Texture = null;
    }

    private SpritePipeline GetPipeline(PipelineKey key)
    {
        if (_pipelines.TryGetValue(key, out var existing)) return existing;
        if (key.Program.Library.GraphicsDevice != GraphicsDevice) throw new ArgumentException("The program belongs to another GraphicsDevice.", "program");

        var pipeline = new GraphicsPipeline(GraphicsDevice, new GraphicsPipelineDescription
        {
            Program = key.Program,
            BlendState = key.BlendState,
            DepthStencilState = key.DepthStencilState,
            RasterizerState = key.RasterizerState,
            VertexLayout = VertexPositionColorTexture.Layout,
            RenderTargetFormats = [_renderTargetFormat],
            DepthStencilFormat = _depthStencilFormat,
            SampleCount = _sampleCount,
            Name = "SpriteBatch",
        });
        var created = new SpritePipeline(pipeline, key.Program.Parameters["MatrixTransform"], key.Program.Parameters["Texture"], key.Program.Parameters["TextureSampler"]);
        _pipelines.Add(key, created);
        return created;
    }

    private struct SpriteItem
    {
        public Texture2D? Texture;
        public VertexPositionColorTexture TopLeft;
        public VertexPositionColorTexture TopRight;
        public VertexPositionColorTexture BottomLeft;
        public VertexPositionColorTexture BottomRight;
    }

    private readonly record struct PipelineKey(ShaderProgram Program, BlendState BlendState, DepthStencilState DepthStencilState, RasterizerState RasterizerState);

    private sealed record SpritePipeline(GraphicsPipeline Pipeline, ShaderParameter? MatrixTransform, ShaderParameter? Texture, ShaderParameter? TextureSampler);

    private struct GlyphLayout(SpriteFont font, Vector2 position, Color color, float rotation, Vector2 origin, Vector2 scale, SpriteEffects effects, float layerDepth, Vector2 size)
    {
        private Vector2 _offset;
        private bool _firstGlyphOfLine = true;

        public void Draw(SpriteBatch spriteBatch, ReadOnlySpan<char> text)
        {
            foreach (var character in text)
            {
                if (character == '\r') continue;
                if (character == '\n')
                {
                    _offset.X = 0f;
                    _offset.Y += font.LineSpacing;
                    _firstGlyphOfLine = true;
                    continue;
                }

                ref readonly var glyph = ref font.GetGlyph(character);
                if (_firstGlyphOfLine)
                {
                    _offset.X = MathF.Max(glyph.LeftSideBearing, 0f);
                    _firstGlyphOfLine = false;
                }
                else
                {
                    _offset.X += font.Spacing + glyph.LeftSideBearing;
                }

                var bounds = glyph.BoundsInTexture;
                var glyphPosition = new Vector2(_offset.X + glyph.Cropping.X, _offset.Y + glyph.Cropping.Y);
                if ((effects & SpriteEffects.FlipHorizontally) != 0) glyphPosition.X = size.X - glyphPosition.X - bounds.Width;
                if ((effects & SpriteEffects.FlipVertically) != 0) glyphPosition.Y = size.Y - glyphPosition.Y - bounds.Height;
                if (bounds.Width > 0 && bounds.Height > 0)
                {
                    var glyphOrigin = origin - glyphPosition;
                    spriteBatch.Enqueue(font.Texture, position, bounds, bounds.Width * scale.X, bounds.Height * scale.Y, glyphOrigin * scale, color, rotation, effects, layerDepth);
                }

                _offset.X += glyph.Width + glyph.RightSideBearing;
            }
        }
    }
}
