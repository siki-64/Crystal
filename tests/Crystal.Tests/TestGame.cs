using Crystal.Graphics;

namespace Crystal.Tests;

internal sealed class TestGame : Game
{
    private readonly int _frameCount;
    private readonly Action<GraphicsDevice, int> _draw;

    public TestGame(Action<GraphicsDevice, int> draw, int frameCount = 1, bool gpuBasedValidation = false)
        : base(WarpOptions(gpuBasedValidation))
    {
        _draw = draw;
        _frameCount = frameCount;
        GraphicsDevice.DebugMessageReceived += (_, message) =>
        {
            if (message.IsError) DebugErrors.Add(message);
        };
    }

    public int DrawCount { get; private set; }
    public List<GraphicsDebugMessage> DebugErrors { get; } = [];

    public static GameOptions WarpOptions(bool gpuBasedValidation) => new()
    {
        Title = "Crystal.Tests",
        Width = 320,
        Height = 180,
        StartHidden = true,
        IsFixedTimeStep = false,
        Graphics = new GraphicsDeviceOptions
        {
            VSync = false,
            AdapterPreference = GraphicsAdapterPreference.Software,
            EnableDebugLayer = true,
            EnableGpuBasedValidation = gpuBasedValidation,
        },
    };

    public static void RunFrames(Action<GraphicsDevice, int> draw, int frameCount = 1, bool gpuBasedValidation = false)
    {
        var game = new TestGame(draw, frameCount, gpuBasedValidation);
        try
        {
            game.Run();
        }
        finally
        {
            game.Dispose();
        }
        Assert.AreEqual(frameCount, game.DrawCount);
        Assert.IsEmpty(game.DebugErrors, string.Join(Environment.NewLine, game.DebugErrors));
    }

    public static void RunOnce(Action<GraphicsDevice> draw, bool gpuBasedValidation = false) =>
        RunFrames((device, _) => draw(device), 1, gpuBasedValidation);

    protected override void Draw(GameTime gameTime)
    {
        _draw(GraphicsDevice, DrawCount);
        DrawCount++;
        if (DrawCount >= _frameCount) Exit();
    }
}
