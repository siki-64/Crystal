using System.Numerics;
using Crystal.Graphics;

namespace Crystal.Tests;

using Crystal.ImGui;

[TestClass]
public sealed unsafe class ImGuiTests
{
    [TestMethod]
    public void VersionIsThePinnedRelease()
    {
        Assert.AreEqual("1.93.0", ImGui.GetVersion());
    }

    [TestMethod]
    public void FrameWithWidgetsProducesDrawData()
    {
        var context = ImGui.CreateContext();
        try
        {
            var io = ImGui.GetIO();
            io.DisplaySize = new Vector2(800, 600);
            io.DeltaTime = 1f / 60f;
            io.BackendFlags |= ImGuiBackendFlags.RendererHasTextures;

            for (var frame = 0; frame < 2; frame++)
            {
                ImGui.NewFrame();
                var open = true;
                ImGui.Begin("Window", ref open);
                ImGui.Text("100% literal text");
                var value = 0.5f;
                ImGui.SliderFloat("Value", ref value, 0f, 1f);
                ImGui.PlotLines("Samples", [0f, 1f, 0.5f]);
                ImGui.End();
                ImGui.Render();
            }

            var drawData = ImGui.GetDrawData();
            Assert.IsTrue(drawData.Valid);
            Assert.IsGreaterThan(0, drawData.TotalVtxCount);
            Assert.IsGreaterThan(0, drawData.CmdLists.Size);
            Assert.IsTrue(drawData.Textures != null && drawData.Textures->Size > 0);
        }
        finally
        {
            ImGui.DestroyContext(context);
        }
    }

    [TestMethod]
    [DataRow(false)]
    [DataRow(true)]
    public void RendererDrawsWidgetsUnderTheDebugLayer(bool gpuBasedValidation)
    {
        using var game = new ImGuiGame(TestGame.WarpOptions(gpuBasedValidation), frameCount: 3);
        game.Run();

        Assert.AreEqual(3, game.DrawCount);
        Assert.IsEmpty(game.DebugErrors, string.Join(Environment.NewLine, game.DebugErrors));
        Assert.IsGreaterThan(0, game.LitPixelCount);
    }

    private sealed class ImGuiGame : Game
    {
        private const int TargetSize = 256;

        private readonly int _frameCount;
        private readonly ImGuiRenderer _renderer;
        private readonly Texture2D _target;
        private readonly float[] _samples = [0f, 1f, 0.25f, 0.75f, 0.5f];

        public ImGuiGame(GameOptions options, int frameCount)
            : base(options)
        {
            _frameCount = frameCount;
            GraphicsDevice.DebugMessageReceived += (_, message) =>
            {
                if (message.IsError) DebugErrors.Add(message);
            };
            _renderer = new ImGuiRenderer(this, SurfaceFormat.Rgba8Unorm);
            _target = new Texture2D(GraphicsDevice, new TextureDescription { Width = TargetSize, Height = TargetSize, Usage = TextureUsage.RenderTarget });
        }

        public int DrawCount { get; private set; }
        public int LitPixelCount { get; private set; }
        public List<GraphicsDebugMessage> DebugErrors { get; } = [];

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.SetRenderTarget(_target);
            GraphicsDevice.Clear(Color.Black);

            _renderer.BeginFrame(gameTime);
            ImGui.SetNextWindowPos(Vector2.Zero);
            ImGui.SetNextWindowSize(new Vector2(TargetSize, TargetSize));
            ImGui.Begin("Statistics");
            ImGui.Text($"Frame {DrawCount}");
            var enabled = true;
            ImGui.Checkbox("Enabled", ref enabled);
            ImGui.PlotLines("Samples", _samples, 0, "overlay", 0f, 1f, new Vector2(0f, 40f));
            if (ImGui.CollapsingHeader("Details")) ImGui.Text("Details");
            ImGui.End();
            _renderer.EndFrame();

            GraphicsDevice.SetRenderTarget(null);
            GraphicsDevice.Clear(Color.Black);

            DrawCount++;
            if (DrawCount < _frameCount) return;

            var pixels = new Color[TargetSize * TargetSize];
            _target.GetData<Color>(pixels);
            LitPixelCount = pixels.Count(pixel => pixel.R + pixel.G + pixel.B > 0);
            Exit();
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                _renderer.Dispose();
                _target.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
