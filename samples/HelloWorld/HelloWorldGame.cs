using System.Numerics;
using Crystal;
using Crystal.Graphics;
using Crystal.Input;

namespace HelloWorld;

public sealed unsafe class HelloWorldGame() : Game(new GameOptions { Title = "Hello, World" })
{
    private float _hue;
    private float _angle;
    private bool _showingSquare;
    private int _vertexCount;
    private KeyboardState _previousKeyboard;
    private ShaderLibrary _shaders = null!;
    private ShaderParameter _rotation = null!;
    private GraphicsPipeline _pipeline = null!;
    private GraphicsBuffer _vertices = null!;

    protected override void Initialize()
    {
        Window.Title = $"Hello, World ({GraphicsDevice.Adapter.Description})";
    }

    protected override void LoadContent()
    {
        _shaders = ShaderLibrary.Load(GraphicsDevice, "Shape.crystalshader");
        var program = _shaders.GetProgram("Shape");
        _rotation = program.Parameters["Rotation"]!;
        _pipeline = new GraphicsPipeline(GraphicsDevice, new GraphicsPipelineDescription
        {
            Program = program,
            VertexLayout = VertexPositionColor.Layout,
            RasterizerState = RasterizerState.CullNone,
            RenderTargetFormats = [GraphicsDevice.BackBufferFormat],
        });

        _vertices = new GraphicsBuffer(GraphicsDevice, new BufferDescription
        {
            Size = Shape.MaxVertexCount * sizeof(VertexPositionColor),
            Stride = sizeof(VertexPositionColor),
            Usage = BufferUsage.Vertex,
            Name = "ShapeVertices",
        });
        ShowShape(new Triangle());
    }

    protected override void UnloadContent()
    {
        _vertices.Dispose();
        _pipeline.Dispose();
        _shaders.Dispose();
    }

    protected override void Update(GameTime gameTime)
    {
        var keyboard = Keyboard.GetState();
        if (keyboard.IsKeyDown(Keys.Space) && _previousKeyboard.IsKeyUp(Keys.Space))
        {
            _showingSquare = !_showingSquare;
            ShowShape(_showingSquare ? new Square() : new Triangle());
        }
        _previousKeyboard = keyboard;

        var seconds = (float)gameTime.ElapsedGameTime.TotalSeconds;
        _hue = (_hue + seconds * 0.1f) % 1f;
        _angle = (_angle + seconds) % MathF.Tau;
    }

    protected override void Draw(GameTime gameTime)
    {
        GraphicsDevice.Clear(FromHue(_hue));
        var aspectRatio = GraphicsDevice.Viewport.AspectRatio;
        _rotation.SetValue(Matrix4x4.CreateRotationZ(_angle) * Matrix4x4.CreateScale(1f / aspectRatio, 1f, 1f));
        GraphicsDevice.SetPipeline(_pipeline);
        GraphicsDevice.SetVertexBuffer(_vertices);
        GraphicsDevice.Draw(_vertexCount);
    }

    private void ShowShape(Shape shape)
    {
        Span<VertexPositionColor> vertices = stackalloc VertexPositionColor[Shape.MaxVertexCount];
        _vertexCount = shape.WriteVertices(vertices);
        _vertices.SetData<VertexPositionColor>(vertices[.._vertexCount]);
    }

    private static Color FromHue(float hue)
    {
        var channels = Vector3.Clamp(
            new Vector3(
                MathF.Abs(hue * 6f - 3f) - 1f,
                2f - MathF.Abs(hue * 6f - 2f),
                2f - MathF.Abs(hue * 6f - 4f)),
            Vector3.Zero,
            Vector3.One) * 0.6f;
        return new Color((byte)(channels.X * 255f), (byte)(channels.Y * 255f), (byte)(channels.Z * 255f), (byte)255);
    }
}
