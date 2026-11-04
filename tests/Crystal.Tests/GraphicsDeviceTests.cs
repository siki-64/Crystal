using Crystal.Graphics;

namespace Crystal.Tests;

[TestClass]
public sealed class GraphicsDeviceTests
{
    [TestMethod]
    [DataRow(false)]
    [DataRow(true)]
    public void ClearAndPresentRunCleanUnderTheDebugLayer(bool gpuBasedValidation)
    {
        using var game = new FrameCountingGame(TestGame.WarpOptions(gpuBasedValidation), frameCount: 30);
        game.Run();

        Assert.AreEqual(30, game.DrawCount);
        Assert.IsTrue(game.GraphicsDevice.IsDebugLayerEnabled);
        Assert.IsTrue(game.GraphicsDevice.Adapter.IsSoftware);
        Assert.IsTrue(game.GraphicsDevice.UsesAgilitySdk);
        Assert.IsEmpty(game.DebugErrors);
    }

    [TestMethod]
    public void ProbeAdapterFindsTheAdapterWithoutAWindow()
    {
        var adapter = GraphicsDevice.ProbeAdapter(TestGame.WarpOptions(gpuBasedValidation: false).Graphics);

        Assert.IsTrue(adapter.IsSoftware);
        Assert.IsFalse(string.IsNullOrEmpty(adapter.Description));
    }

    [TestMethod]
    public void BackBufferResizesBetweenFrames()
    {
        using var game = new FrameCountingGame(TestGame.WarpOptions(gpuBasedValidation: false), frameCount: 10, resizeAtFrame: 5, resizeTo: new Point(400, 240));
        game.Run();

        Assert.AreEqual(400, game.GraphicsDevice.BackBufferWidth);
        Assert.AreEqual(240, game.GraphicsDevice.BackBufferHeight);
        Assert.IsEmpty(game.DebugErrors);
    }

    [TestMethod]
    public void DebugLayerErrorsAreReportedAndThrownAtEndOfFrame()
    {
        using var game = new FrameCountingGame(TestGame.WarpOptions(gpuBasedValidation: false), frameCount: 3, postErrorAtFrame: 2);

        var exception = Assert.ThrowsExactly<GraphicsDebugException>(game.Run);

        Assert.AreEqual(2, game.DrawCount);
        Assert.HasCount(1, exception.Errors);
        Assert.Contains(FrameCountingGame.PostedErrorText, exception.Errors[0].Text);
        Assert.HasCount(1, game.DebugErrors);
    }

    [TestMethod]
    [DataRow(false)]
    [DataRow(true)]
    public void BackBufferReadsBackWhatWasDrawn(bool gpuBasedValidation)
    {
        var width = 0;
        var height = 0;
        uint[] firstClear = [];
        uint[] secondClear = [];
        TestGame.RunOnce(device =>
        {
            width = device.BackBufferWidth;
            height = device.BackBufferHeight;
            firstClear = new uint[width * height];
            secondClear = new uint[width * height];
            device.Clear(Color.CornflowerBlue);
            device.GetBackBufferData<uint>(firstClear);
            device.Clear(new Color(40, 44, 52));
            using var readback = device.BeginGetBackBufferData();
            readback.GetData<uint>(secondClear);
        }, gpuBasedValidation);

        Assert.AreEqual(320, width);
        Assert.AreEqual(180, height);
        GraphicsTestHelpers.AssertAllPixels(Color.CornflowerBlue, firstClear);
        GraphicsTestHelpers.AssertAllPixels(new Color(40, 44, 52), secondClear);
    }

    [TestMethod]
    public void BackBufferReadbackOutsideDrawThrows()
    {
        using var game = new FrameCountingGame(TestGame.WarpOptions(gpuBasedValidation: false), frameCount: 1);

        Assert.ThrowsExactly<InvalidOperationException>(() => game.GraphicsDevice.BeginGetBackBufferData());
    }

    [TestMethod]
    public void ClearOutsideDrawThrows()
    {
        using var game = new FrameCountingGame(TestGame.WarpOptions(gpuBasedValidation: false), frameCount: 1);

        Assert.ThrowsExactly<InvalidOperationException>(() => game.GraphicsDevice.Clear(Color.Black));
    }

    private sealed class FrameCountingGame : Game
    {
        public const string PostedErrorText = "Crystal.Tests posted error";

        private readonly int _frameCount;
        private readonly int _resizeAtFrame;
        private readonly Point _resizeTo;
        private readonly int _postErrorAtFrame;

        public FrameCountingGame(GameOptions options, int frameCount, int resizeAtFrame = -1, Point resizeTo = default, int postErrorAtFrame = -1)
            : base(options)
        {
            _frameCount = frameCount;
            _resizeAtFrame = resizeAtFrame;
            _resizeTo = resizeTo;
            _postErrorAtFrame = postErrorAtFrame;
            GraphicsDevice.DebugMessageReceived += (_, message) =>
            {
                if (message.IsError) DebugErrors.Add(message);
            };
        }

        public int DrawCount { get; private set; }
        public List<GraphicsDebugMessage> DebugErrors { get; } = [];

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(DrawCount % 2 == 0 ? Color.CornflowerBlue : Color.Black);
            DrawCount++;
            if (DrawCount == _resizeAtFrame) GraphicsDevice.RequestBackBufferResize(_resizeTo.X, _resizeTo.Y);
            if (DrawCount == _postErrorAtFrame) GraphicsDevice.PostDebugLayerError(PostedErrorText);
            if (DrawCount >= _frameCount) Exit();
        }
    }
}
