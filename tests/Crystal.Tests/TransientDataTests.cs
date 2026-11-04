using Crystal.Graphics;
using static Crystal.Tests.GraphicsTestHelpers;

namespace Crystal.Tests;

[TestClass]
public sealed class TransientDataTests
{
    [TestMethod]
    [DataRow(false)]
    [DataRow(true)]
    public void TransientVertexAndIndexDataDraw(bool gpuBasedValidation) => TestGame.RunOnce(device =>
    {
        using var library = LoadLibrary(device, "Basic");
        using var target = CreateTarget(device);
        using var pipeline = new GraphicsPipeline(device, new GraphicsPipelineDescription
        {
            Program = library.GetProgram("VertexColor"),
            VertexLayout = VertexPositionColor.Layout,
            RasterizerState = RasterizerState.CullNone,
        });
        var quad = LeftHalfQuad(Color.Red);

        device.SetRenderTarget(target);
        device.Clear(Color.Black);
        device.SetPipeline(pipeline);
        device.SetVertexData<VertexPositionColor>(quad);
        device.SetIndexData((ReadOnlySpan<ushort>)[0, 1, 2, 3, 4, 5]);
        device.DrawIndexed(6, 0, 0);

        var pixels = new Color[TargetSize * TargetSize];
        target.GetData<Color>(pixels);
        Assert.AreEqual(Color.Red, pixels[TargetSize / 2 * TargetSize + TargetSize / 4]);
        Assert.AreEqual(Color.Black, pixels[TargetSize / 2 * TargetSize + TargetSize * 3 / 4]);
    }, gpuBasedValidation);

    [TestMethod]
    public void TransientDataLargerThanAnUploadPageDraws() => TestGame.RunOnce(device =>
    {
        using var library = LoadLibrary(device, "Basic");
        using var target = CreateTarget(device);
        using var pipeline = new GraphicsPipeline(device, new GraphicsPipelineDescription
        {
            Program = library.GetProgram("VertexColor"),
            VertexLayout = VertexPositionColor.Layout,
            RasterizerState = RasterizerState.CullNone,
        });
        const int copies = 80_000;
        var quad = FullQuad(0f, Color.Green);
        var vertices = new VertexPositionColor[copies * quad.Length];
        for (var copy = 0; copy < copies; copy++) quad.CopyTo(vertices, copy * quad.Length);
        var indices = new uint[vertices.Length];
        for (var index = 0; index < indices.Length; index++) indices[index] = (uint)(indices.Length - 1 - index);

        device.SetRenderTarget(target);
        device.Clear(Color.Black);
        device.SetPipeline(pipeline);
        device.SetVertexData<VertexPositionColor>(vertices);
        device.SetIndexData(indices);
        device.DrawIndexed(indices.Length, 0, 0);

        var pixels = new Color[TargetSize * TargetSize];
        target.GetData<Color>(pixels);
        Assert.AreEqual(Color.Green, pixels[TargetSize / 2 * TargetSize + TargetSize / 2]);
    });

    [TestMethod]
    public void SettingAVertexBufferReplacesTransientVertexData() => TestGame.RunOnce(device =>
    {
        using var library = LoadLibrary(device, "Basic");
        using var target = CreateTarget(device);
        using var pipeline = new GraphicsPipeline(device, new GraphicsPipelineDescription
        {
            Program = library.GetProgram("VertexColor"),
            VertexLayout = VertexPositionColor.Layout,
            RasterizerState = RasterizerState.CullNone,
        });
        using var buffer = CreateVertexBuffer<VertexPositionColor>(device, FullQuad(0f, Color.Blue));

        device.SetRenderTarget(target);
        device.Clear(Color.Black);
        device.SetPipeline(pipeline);
        device.SetVertexData<VertexPositionColor>(LeftHalfQuad(Color.Red));
        device.SetVertexBuffer(buffer);
        device.Draw(6);

        var pixels = new Color[TargetSize * TargetSize];
        target.GetData<Color>(pixels);
        Assert.AreEqual(Color.Blue, pixels[TargetSize / 2 * TargetSize + TargetSize / 4]);
    });

    [TestMethod]
    public void TextureRegionSetDataWritesOnlyTheRegion() => TestGame.RunOnce(device =>
    {
        using var texture = new Texture2D(device, new TextureDescription { Width = 8, Height = 8 });
        var background = Enumerable.Repeat(Color.Black, 64).ToArray();
        texture.SetData<Color>(background);
        var patch = Enumerable.Repeat(Color.White, 6).ToArray();
        texture.SetData<Color>(patch, new Rectangle(2, 3, 3, 2));

        var pixels = new Color[64];
        texture.GetData<Color>(pixels);
        for (var y = 0; y < 8; y++)
        {
            for (var x = 0; x < 8; x++)
            {
                var inside = x >= 2 && x < 5 && y >= 3 && y < 5;
                Assert.AreEqual(inside ? Color.White : Color.Black, pixels[y * 8 + x], $"({x}, {y})");
            }
        }

        Assert.ThrowsExactly<ArgumentOutOfRangeException>(() => texture.SetData<Color>(patch, new Rectangle(6, 0, 3, 2)));
        Assert.ThrowsExactly<ArgumentException>(() => texture.SetData<Color>(patch, new Rectangle(0, 0, 2, 2)));
    });
}
