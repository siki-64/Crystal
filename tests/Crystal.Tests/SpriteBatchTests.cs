using System.Numerics;
using System.Text;
using Crystal.Graphics;
using static Crystal.Tests.GraphicsTestHelpers;

namespace Crystal.Tests;

[TestClass]
public sealed class SpriteBatchTests
{
    [TestMethod]
    [DataRow(false)]
    [DataRow(true)]
    public void SpritesLandOnTheirPixels(bool gpuBasedValidation) => TestGame.RunOnce(device =>
    {
        using var spriteBatch = new SpriteBatch(device);
        using var target = CreateTarget(device);
        using var texture = CreateTexture(device, 2, 1, Color.Red, Color.Green);

        device.SetRenderTarget(target);
        device.Clear(Color.Black);
        spriteBatch.Begin(blendState: BlendState.Opaque, samplerState: device.GetSamplerState(SamplerDescription.PointClamp));
        spriteBatch.Draw(texture, new Rectangle(8, 8, 16, 8), Color.White);
        spriteBatch.Draw(texture, new Rectangle(8, 32, 16, 8), null, Color.White, 0f, Vector2.Zero, SpriteEffects.FlipHorizontally, 0f);
        spriteBatch.Draw(texture, new Vector2(40f, 8f), new Rectangle(1, 0, 1, 1), Color.White, 0f, Vector2.Zero, 4f, SpriteEffects.None, 0f);
        spriteBatch.End();

        var pixels = ReadPixels(target);
        Assert.AreEqual(Color.Red, pixels[At(10, 10)]);
        Assert.AreEqual(Color.Green, pixels[At(20, 10)]);
        Assert.AreEqual(Color.Black, pixels[At(25, 10)]);
        Assert.AreEqual(Color.Black, pixels[At(10, 17)]);
        Assert.AreEqual(Color.Green, pixels[At(10, 34)]);
        Assert.AreEqual(Color.Red, pixels[At(20, 34)]);
        Assert.AreEqual(Color.Green, pixels[At(40, 8)]);
        Assert.AreEqual(Color.Green, pixels[At(43, 11)]);
        Assert.AreEqual(Color.Black, pixels[At(44, 8)]);
    }, gpuBasedValidation);

    [TestMethod]
    public void RotationTurnsAroundTheOrigin() => TestGame.RunOnce(device =>
    {
        using var spriteBatch = new SpriteBatch(device);
        using var target = CreateTarget(device);
        using var texture = CreateTexture(device, 1, 1, Color.White);

        device.SetRenderTarget(target);
        device.Clear(Color.Black);
        spriteBatch.Begin(blendState: BlendState.Opaque);
        spriteBatch.Draw(texture, new Vector2(32f, 32f), null, Color.Blue, MathF.PI / 2f, Vector2.Zero, new Vector2(16f, 4f), SpriteEffects.None, 0f);
        spriteBatch.End();

        var pixels = ReadPixels(target);
        Assert.AreEqual(Color.Blue, pixels[At(30, 40)]);
        Assert.AreEqual(Color.Black, pixels[At(40, 33)]);
        Assert.AreEqual(Color.Black, pixels[At(33, 40)]);
    });

    [TestMethod]
    public void TransformMatrixMovesTheBatch() => TestGame.RunOnce(device =>
    {
        using var spriteBatch = new SpriteBatch(device);
        using var target = CreateTarget(device);
        using var texture = CreateTexture(device, 1, 1, Color.White);

        device.SetRenderTarget(target);
        device.Clear(Color.Black);
        spriteBatch.Begin(transformMatrix: Matrix4x4.CreateScale(2f) * Matrix4x4.CreateTranslation(10f, 20f, 0f));
        spriteBatch.Draw(texture, new Rectangle(0, 0, 4, 4), Color.White);
        spriteBatch.End();

        var pixels = ReadPixels(target);
        Assert.AreEqual(Color.White, pixels[At(10, 20)]);
        Assert.AreEqual(Color.White, pixels[At(17, 27)]);
        Assert.AreEqual(Color.Black, pixels[At(18, 20)]);
        Assert.AreEqual(Color.Black, pixels[At(9, 20)]);
    });

    [TestMethod]
    public void AlphaBlendUsesPremultipliedColors() => TestGame.RunOnce(device =>
    {
        using var spriteBatch = new SpriteBatch(device);
        using var target = CreateTarget(device);
        using var texture = CreateTexture(device, 1, 1, Color.White);

        device.SetRenderTarget(target);
        device.Clear(Color.Black);
        spriteBatch.Begin();
        spriteBatch.Draw(texture, new Rectangle(0, 0, 8, 8), new Color(128, 0, 0, 128));
        spriteBatch.End();

        var pixel = ReadPixels(target)[At(4, 4)];
        Assert.AreEqual(128f, pixel.R, 1f);
        Assert.AreEqual(0, pixel.G);
    });

    [TestMethod]
    [DataRow(SpriteSortMode.Deferred, 3, 0)]
    [DataRow(SpriteSortMode.Immediate, 3, 0)]
    [DataRow(SpriteSortMode.Texture, 2, 0)]
    [DataRow(SpriteSortMode.BackToFront, 2, 1)]
    [DataRow(SpriteSortMode.FrontToBack, 2, 2)]
    public void SortModesOrderAndBatchSprites(SpriteSortMode sortMode, int expectedDrawCount, int expectedTopSprite) => TestGame.RunOnce(device =>
    {
        using var spriteBatch = new SpriteBatch(device);
        using var target = CreateTarget(device);
        using var red = CreateTexture(device, 1, 1, Color.Red);
        using var green = CreateTexture(device, 1, 1, Color.Green);
        var colors = new[] { Color.Red, Color.Green, Color.Red };
        var textures = new[] { red, green, red };
        var depths = new[] { 0.5f, 0f, 1f };

        device.SetRenderTarget(target);
        device.Clear(Color.Black);
        var drawsBefore = device.Metrics.DrawCount;
        spriteBatch.Begin(sortMode, BlendState.Opaque);
        for (var index = 0; index < 3; index++)
            spriteBatch.Draw(textures[index], new Vector2(8f, 8f), null, Color.White, 0f, Vector2.Zero, 8f + index, SpriteEffects.None, depths[index]);
        spriteBatch.End();

        Assert.AreEqual(expectedDrawCount, device.Metrics.DrawCount - drawsBefore);
        if (sortMode is SpriteSortMode.BackToFront or SpriteSortMode.FrontToBack)
            Assert.AreEqual(colors[expectedTopSprite], ReadPixels(target)[At(9, 9)]);
    });

    [TestMethod]
    public void LayerDepthWorksWithADepthBuffer() => TestGame.RunOnce(device =>
    {
        using var spriteBatch = new SpriteBatch(device, depthStencilFormat: SurfaceFormat.Depth32Float);
        using var target = CreateTarget(device);
        using var depth = new Texture2D(device, new TextureDescription { Width = TargetSize, Height = TargetSize, Format = SurfaceFormat.Depth32Float, Usage = TextureUsage.DepthStencil });
        using var texture = CreateTexture(device, 1, 1, Color.White);
        spriteBatch.PreparePipeline(BlendState.Opaque, DepthStencilState.Default);

        device.SetRenderTarget(target, depth);
        device.Clear(Color.Black);
        device.ClearDepthStencil();
        spriteBatch.Begin(blendState: BlendState.Opaque, depthStencilState: DepthStencilState.Default);
        spriteBatch.Draw(texture, new Vector2(0f, 0f), null, Color.Red, 0f, Vector2.Zero, 16f, SpriteEffects.None, 0.2f);
        spriteBatch.Draw(texture, new Vector2(8f, 0f), null, Color.Green, 0f, Vector2.Zero, 16f, SpriteEffects.None, 0.8f);
        spriteBatch.End();

        var pixels = ReadPixels(target);
        Assert.AreEqual(Color.Red, pixels[At(12, 4)]);
        Assert.AreEqual(Color.Green, pixels[At(20, 4)]);
    });

    [TestMethod]
    public void LargeBatchesSplitIntoSeveralDraws() => TestGame.RunOnce(device =>
    {
        using var spriteBatch = new SpriteBatch(device);
        using var target = CreateTarget(device);
        using var texture = CreateTexture(device, 1, 1, Color.White);
        const int spriteCount = SpriteBatch.MaximumSpritesPerDraw + 100;

        device.SetRenderTarget(target);
        device.Clear(Color.Black);
        var drawsBefore = device.Metrics.DrawCount;
        spriteBatch.Begin();
        for (var index = 0; index < spriteCount; index++)
            spriteBatch.Draw(texture, new Rectangle(index % TargetSize, index / TargetSize % TargetSize, 1, 1), Color.White);
        spriteBatch.End();

        Assert.AreEqual(2, device.Metrics.DrawCount - drawsBefore);
        var pixels = ReadPixels(target);
        Assert.IsTrue(pixels.All(pixel => pixel == Color.White));
    });

    [TestMethod]
    public void DrawStringPlacesGlyphsLikeMeasureString() => TestGame.RunOnce(device =>
    {
        using var spriteBatch = new SpriteBatch(device);
        using var target = CreateTarget(device);
        var font = CreateBlockFont(device, out var fontTexture);
        using var texture = fontTexture;

        Assert.AreEqual(new Vector2(11f, 10f), font.MeasureString("AB"));
        Assert.AreEqual(new Vector2(11f, 20f), font.MeasureString(new StringBuilder("AB\r\nA")));
        Assert.AreEqual(Vector2.Zero, font.MeasureString(""));
        Assert.ThrowsExactly<ArgumentException>(() => font.MeasureString("C"));

        device.SetRenderTarget(target);
        device.Clear(Color.Black);
        spriteBatch.Begin(blendState: BlendState.Opaque, samplerState: device.GetSamplerState(SamplerDescription.PointClamp));
        spriteBatch.DrawString(font, "AB\nA", new Vector2(10f, 10f), Color.White);
        spriteBatch.DrawString(font, new StringBuilder("AB"), new Vector2(10f, 40f), Color.White, 0f, Vector2.Zero, 1f, SpriteEffects.FlipHorizontally, 0f);
        spriteBatch.End();

        var pixels = ReadPixels(target);
        Assert.AreEqual(Color.Red, pixels[At(11, 11)]);
        Assert.AreEqual(Color.Black, pixels[At(15, 11)]);
        Assert.AreEqual(Color.Blue, pixels[At(17, 11)]);
        Assert.AreEqual(Color.Red, pixels[At(11, 21)]);
        Assert.AreEqual(Color.Black, pixels[At(17, 21)]);
        Assert.AreEqual(Color.Blue, pixels[At(11, 41)]);
        Assert.AreEqual(Color.Red, pixels[At(17, 41)]);
    });

    [TestMethod]
    public void DefaultCharacterReplacesMissingGlyphs() => TestGame.RunOnce(device =>
    {
        var font = CreateBlockFont(device, out var fontTexture);
        using var texture = fontTexture;

        Assert.IsFalse(font.TryGetGlyph('C', out _));
        Assert.IsTrue(font.TryGetGlyph('A', out var glyph));
        Assert.AreEqual('A', glyph.Character);
        CollectionAssert.AreEqual(new[] { 'A', 'B' }, font.Characters.ToArray());
        Assert.ThrowsExactly<ArgumentException>(() => font.DefaultCharacter = 'C');
        font.DefaultCharacter = 'B';
        Assert.AreEqual(font.MeasureString("AB"), font.MeasureString("AC"));
    });

    [TestMethod]
    public void BeginAndEndMustPair() => TestGame.RunOnce(device =>
    {
        using var spriteBatch = new SpriteBatch(device);
        using var texture = CreateTexture(device, 1, 1, Color.White);

        Assert.ThrowsExactly<InvalidOperationException>(() => spriteBatch.Draw(texture, Vector2.Zero, Color.White));
        Assert.ThrowsExactly<InvalidOperationException>(spriteBatch.End);
        spriteBatch.Begin();
        Assert.ThrowsExactly<InvalidOperationException>(() => spriteBatch.Begin());
        spriteBatch.End();
    });

    [TestMethod]
    public void DrawingAWarmBatchAllocatesNothing() => TestGame.RunFrames((device, frame) =>
    {
        var state = DrawingState.Get(device);
        device.SetRenderTarget(state.Target);
        device.Clear(Color.Black);

        var allocatedBefore = GC.GetAllocatedBytesForCurrentThread();
        state.SpriteBatch.Begin(SpriteSortMode.BackToFront);
        for (var index = 0; index < 500; index++)
            state.SpriteBatch.Draw(state.Texture, new Vector2(index % 64, index / 64), null, Color.White, index * 0.1f, Vector2.One, 2f, SpriteEffects.FlipVertically, index / 500f);
        state.SpriteBatch.DrawString(state.Font, "ABBA\nBA", new Vector2(4f, 4f), Color.White);
        state.SpriteBatch.DrawString(state.Font, state.Text, new Vector2(4f, 24f), Color.White, 0.5f, Vector2.Zero, 2f, SpriteEffects.FlipHorizontally, 0f);
        state.SpriteBatch.End();
        var allocated = GC.GetAllocatedBytesForCurrentThread() - allocatedBefore;

        if (frame >= 4) Assert.AreEqual(0L, allocated, $"frame {frame}");
        if (frame == 5) state.Dispose();
    }, frameCount: 6);

    private static Texture2D CreateTexture(GraphicsDevice device, int width, int height, params Color[] pixels)
    {
        var texture = new Texture2D(device, new TextureDescription { Width = width, Height = height });
        texture.SetData<Color>(pixels);
        return texture;
    }

    private static SpriteFont CreateBlockFont(GraphicsDevice device, out Texture2D texture)
    {
        var pixels = new Color[16 * 8];
        for (var y = 0; y < 8; y++)
        {
            for (var x = 0; x < 16; x++) pixels[y * 16 + x] = x < 8 ? Color.Red : Color.Blue;
        }
        texture = CreateTexture(device, 16, 8, pixels);
        return new SpriteFont(texture,
        [
            new SpriteFontGlyph('B', new Rectangle(8, 0, 4, 8), new Rectangle(0, 0, 4, 8), 1f, 4f, 0f),
            new SpriteFontGlyph('A', new Rectangle(0, 0, 4, 8), new Rectangle(0, 0, 4, 8), 0f, 4f, 2f),
        ], lineSpacing: 10);
    }

    private static Color[] ReadPixels(Texture2D target)
    {
        var pixels = new Color[target.Width * target.Height];
        target.GetData<Color>(pixels);
        return pixels;
    }

    private static int At(int x, int y) => y * TargetSize + x;

    private sealed class DrawingState : IDisposable
    {
        private static DrawingState? _current;

        private DrawingState(GraphicsDevice device)
        {
            SpriteBatch = new SpriteBatch(device);
            Target = CreateTarget(device);
            Texture = CreateTexture(device, 1, 1, Color.White);
            Font = CreateBlockFont(device, out var fontTexture);
            FontTexture = fontTexture;
        }

        public SpriteBatch SpriteBatch { get; }
        public Texture2D Target { get; }
        public Texture2D Texture { get; }
        public Texture2D FontTexture { get; }
        public SpriteFont Font { get; }
        public StringBuilder Text { get; } = new("BAAB");

        public static DrawingState Get(GraphicsDevice device) => _current ??= new DrawingState(device);

        public void Dispose()
        {
            SpriteBatch.Dispose();
            Target.Dispose();
            Texture.Dispose();
            FontTexture.Dispose();
            _current = null;
        }
    }
}
