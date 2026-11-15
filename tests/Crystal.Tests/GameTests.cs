using System.Diagnostics;
using Crystal.Graphics;

namespace Crystal.Tests;

[TestClass]
public sealed class GameTests
{
    [TestMethod]
    public void BeginDrawCanSkipFramesAndEndDrawSeesTheFrameMetrics()
    {
        using var game = new HookedGame();
        game.Run();

        Assert.AreEqual(3, game.SkippedFrames);
        Assert.AreEqual(2, game.DrawnFrames);
        Assert.HasCount(2, game.MetricsAtEndDraw);
        var metrics = game.MetricsAtEndDraw[0];
        Assert.AreEqual((2, 0, 1, 1), (metrics.DrawCount, metrics.DispatchCount, metrics.ClearCount, metrics.PipelineStateChangeCount));
        Assert.AreEqual(game.MetricsAtEndDraw[0], game.MetricsAtEndDraw[1]);
    }

    [TestMethod]
    public void OverridesThatSkipTheBaseImplementationThrow()
    {
        using (var game = new BrokenGame(skipBaseBeginDraw: true))
        {
            var exception = Assert.ThrowsExactly<InvalidOperationException>(game.Run);
            StringAssert.Contains(exception.Message, "base.BeginDraw");
        }
        using (var game = new BrokenGame(skipBaseBeginDraw: false))
        {
            var exception = Assert.ThrowsExactly<InvalidOperationException>(game.Run);
            StringAssert.Contains(exception.Message, "base.EndDraw");
        }
    }

    [TestMethod]
    public void MaximumFrameRateSpacesFramesAtLeastOnePeriodApart()
    {
        const int frameRate = 50;
        const int frameCount = 6;
        var presented = new List<long>();
        using var game = new PacedGame(frameCount, presented) { MaximumFrameRate = frameRate };
        game.Run();

        Assert.HasCount(frameCount, presented);
        var period = TimeSpan.FromSeconds(1.0 / frameRate) - TimeSpan.FromMilliseconds(0.5);
        for (var frame = 1; frame < frameCount; frame++)
            Assert.IsGreaterThanOrEqualTo(period, Stopwatch.GetElapsedTime(presented[frame - 1], presented[frame]));
    }

    [TestMethod]
    public void MaximumFrameRateComesFromTheOptionsAndRejectsNegativeValues()
    {
        using var game = new PacedGame(1, [], TestGame.WarpOptions(gpuBasedValidation: false) with { MaximumFrameRate = 30 });
        Assert.AreEqual(30, game.MaximumFrameRate);
        game.MaximumFrameRate = 0;
        Assert.AreEqual(0, game.MaximumFrameRate);
        Assert.ThrowsExactly<ArgumentOutOfRangeException>(() => game.MaximumFrameRate = -1);
    }

    [TestMethod]
    public void ClipboardTextRoundTripsUnicode()
    {
        using var game = new TestGame((_, _) => { });
        var previous = game.Window.ClipboardText;
        try
        {
            const string text = "Crystal 東方 💎\nsecond line";
            game.Window.ClipboardText = text;
            Assert.AreEqual(text.Replace("\n", "\r\n"), game.Window.ClipboardText);
            game.Window.ClipboardText = string.Empty;
            Assert.AreEqual(string.Empty, game.Window.ClipboardText);
        }
        finally
        {
            game.Window.ClipboardText = previous;
        }
    }

    [TestMethod]
    public void WindowPositionAndDisplays()
    {
        var displays = Display.GetAll();
        Assert.IsNotEmpty(displays);
        Assert.AreEqual(1, displays.Count(display => display.IsPrimary));
        foreach (var display in displays)
        {
            Assert.IsFalse(display.Bounds.IsEmpty);
            Assert.IsFalse(display.WorkArea.IsEmpty);
            Assert.AreEqual(display.WorkArea, Rectangle.Intersect(display.Bounds, display.WorkArea));
            Assert.IsGreaterThan(0f, display.ContentScale);
        }

        using var game = new TestGame((_, _) => { });
        var primary = displays.First(display => display.IsPrimary);
        var position = new Point(primary.WorkArea.X + 40, primary.WorkArea.Y + 60);
        game.Window.Position = position;
        Assert.AreEqual(position, game.Window.Position);
        Assert.AreEqual(primary, game.Window.Display);
    }

    private sealed class HookedGame() : Game(TestGame.WarpOptions(gpuBasedValidation: false))
    {
        private GraphicsPipeline? _pipeline;
        private ShaderLibrary? _library;

        public int SkippedFrames { get; private set; }
        public int DrawnFrames { get; private set; }
        public List<GraphicsMetrics> MetricsAtEndDraw { get; } = [];

        protected override void LoadContent()
        {
            _library = GraphicsTestHelpers.LoadLibrary(GraphicsDevice, "Basic");
            _pipeline = new GraphicsPipeline(GraphicsDevice, new GraphicsPipelineDescription { Program = _library.GetProgram("SolidColor") });
        }

        protected override bool BeginDraw()
        {
            if (SkippedFrames < 3)
            {
                SkippedFrames++;
                return false;
            }
            return base.BeginDraw();
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.Black);
            GraphicsDevice.SetPipeline(_pipeline!);
            GraphicsDevice.Draw(3);
            GraphicsDevice.Draw(3);
            DrawnFrames++;
            if (DrawnFrames == 2) Exit();
        }

        protected override void EndDraw()
        {
            MetricsAtEndDraw.Add(GraphicsDevice.Metrics);
            base.EndDraw();
        }

        protected override void UnloadContent()
        {
            _pipeline?.Dispose();
            _library?.Dispose();
        }
    }

    private sealed class PacedGame(int frameCount, List<long> presented, GameOptions? options = null)
        : Game(options ?? TestGame.WarpOptions(gpuBasedValidation: false))
    {
        protected override void Draw(GameTime gameTime) => GraphicsDevice.Clear(Color.Black);

        protected override void EndDraw()
        {
            base.EndDraw();
            presented.Add(Stopwatch.GetTimestamp());
            if (presented.Count == frameCount) Exit();
        }
    }

    private sealed class BrokenGame(bool skipBaseBeginDraw) : Game(TestGame.WarpOptions(gpuBasedValidation: false))
    {
        protected override bool BeginDraw() => skipBaseBeginDraw || base.BeginDraw();

        protected override void EndDraw()
        {
        }
    }
}
