using System.Numerics;
using Crystal.Graphics;
using static Crystal.Tests.GraphicsTestHelpers;

namespace Crystal.Tests;

[TestClass]
public sealed class CommandListTests
{
    [TestMethod]
    [DataRow(false, false)]
    [DataRow(true, false)]
    [DataRow(true, true)]
    public void ListsRecordedOnWorkerThreadsRunInExecutionOrder(bool gpuBasedValidation, bool executesTogether) => TestGame.RunOnce(device =>
    {
        using var library = LoadLibrary(device, "Basic");
        using var target = CreateTarget(device);
        using var pipeline = VertexColorPipeline(device, library);
        using var first = new GraphicsCommandList(device, "First");
        using var second = new GraphicsCommandList(device, "Second");

        Parallel.Invoke(
            () =>
            {
                using var binding = first.Bind();
                device.SetRenderTarget(target);
                device.Clear(Color.Black);
                device.SetPipeline(pipeline);
                device.SetVertexData<VertexPositionColor>(FullQuad(0f, Color.Red));
                device.Draw(6);
            },
            () =>
            {
                using var binding = second.Bind();
                device.SetRenderTarget(target);
                device.SetPipeline(pipeline);
                device.SetVertexData<VertexPositionColor>(LeftHalfQuad(Color.Blue));
                device.Draw(6);
            });

        Assert.IsTrue(first.IsRecording);
        Assert.AreEqual(1, second.Metrics.DrawCount);
        ExecuteInOrder(device, executesTogether, first, second);
        Assert.IsFalse(first.IsRecording);
        Assert.AreEqual(2, device.Metrics.DrawCount);

        var pixels = ReadPixels(target);
        AssertPixel(Color.Blue, pixels, TargetSize / 4, TargetSize / 2);
        AssertPixel(Color.Red, pixels, TargetSize * 3 / 4, TargetSize / 2);
    }, gpuBasedValidation);

    [TestMethod]
    [DataRow(false)]
    [DataRow(true)]
    public void ListSamplesWhatTheDeviceAndAnEarlierListRendered(bool executesTogether) => TestGame.RunOnce(device =>
    {
        using var library = LoadLibrary(device, "Basic");
        using var resources = LoadLibrary(device, "Resources");
        using var left = CreateSampledTarget(device);
        using var right = CreateSampledTarget(device);
        using var destination = CreateTarget(device);
        using var producer = new GraphicsCommandList(device, "Producer");
        using var consumer = new GraphicsCommandList(device, "Consumer");
        using var colorPipeline = VertexColorPipeline(device, library);
        var textured = resources.GetProgram("Textured");
        using var texturedPipeline = new GraphicsPipeline(device, new GraphicsPipelineDescription
        {
            Program = textured,
            VertexLayout = VertexPositionTexture.Layout,
            RasterizerState = RasterizerState.CullNone,
        });

        device.SetRenderTarget(left);
        device.Clear(Color.Green);

        using (producer.Bind())
        {
            device.SetRenderTarget(right);
            device.Clear(Color.Black);
            device.SetPipeline(colorPipeline);
            device.SetVertexData<VertexPositionColor>(FullQuad(0f, Color.White));
            device.Draw(6);
        }

        Task.Run(() =>
        {
            using var binding = consumer.Bind();
            device.SetRenderTarget(destination);
            device.SetPipeline(texturedPipeline);
            textured.Parameters["DiffuseSampler"]!.SetValue(device.GetSamplerState(SamplerDescription.PointClamp));
            textured.Parameters["DiffuseTexture"]!.SetValue(left);
            device.SetVertexData<VertexPositionTexture>(TexturedHalf(-1f, 0f));
            device.Draw(6);
            textured.Parameters["DiffuseTexture"]!.SetValue(right);
            device.SetVertexData<VertexPositionTexture>(TexturedHalf(0f, 1f));
            device.Draw(6);
        }).GetAwaiter().GetResult();

        ExecuteInOrder(device, executesTogether, producer, consumer);

        var pixels = ReadPixels(destination);
        AssertPixel(Color.Green, pixels, TargetSize / 4, TargetSize / 2);
        AssertPixel(Color.White, pixels, TargetSize * 3 / 4, TargetSize / 2);
    }, gpuBasedValidation: executesTogether);

    [TestMethod]
    public void BindingIsExclusive() => TestGame.RunOnce(device =>
    {
        using var first = new GraphicsCommandList(device, "First");
        using var second = new GraphicsCommandList(device, "Second");

        using (first.Bind())
        {
            Assert.ThrowsExactly<InvalidOperationException>(() => second.Bind());
            Assert.ThrowsExactly<InvalidOperationException>(() => device.Execute(first));
            var otherThreadBinds = Task.Run(() =>
            {
                using var binding = first.Bind();
            });
            Assert.ThrowsExactly<InvalidOperationException>(() => otherThreadBinds.GetAwaiter().GetResult());
        }

        device.Execute(first);
        device.Execute(second);
        Assert.IsFalse(first.IsRecording);
        Assert.IsFalse(second.IsRecording);
    });

    [TestMethod]
    public void ListsKeepTheirOwnParameterValues() => TestGame.RunOnce(device =>
    {
        using var library = LoadLibrary(device, "Basic");
        var solid = library.GetProgram("SolidColor");
        var tint = solid.Parameters["Tint"]!;
        using var pipeline = new GraphicsPipeline(device, new GraphicsPipelineDescription { Program = solid });
        using var green = CreateTarget(device);
        using var inherited = CreateTarget(device);
        using var immediate = CreateTarget(device);
        using var first = new GraphicsCommandList(device, "First");
        using var second = new GraphicsCommandList(device, "Second");
        tint.SetValue(Color.Red);

        Parallel.Invoke(
            () =>
            {
                using var binding = first.Bind();
                device.SetRenderTarget(green);
                device.SetPipeline(pipeline);
                tint.SetValue(Color.Green);
                device.Draw(3);
            },
            () =>
            {
                using var binding = second.Bind();
                device.SetRenderTarget(inherited);
                device.SetPipeline(pipeline);
                device.Draw(3);
            });

        tint.SetValue(Color.Blue);
        device.SetRenderTarget(immediate);
        device.SetPipeline(pipeline);
        device.Draw(3);
        device.Execute(first);
        device.Execute(second);

        AssertAllPixels(Color.Green, ReadPixels(green));
        AssertAllPixels(Color.Red, ReadPixels(inherited));
        AssertAllPixels(Color.Blue, ReadPixels(immediate));
    });

    [TestMethod]
    public void ValuesSetWhileSuspendedAreTheOnesListsStartFrom() => TestGame.RunOnce(device =>
    {
        using var library = LoadLibrary(device, "Basic");
        var solid = library.GetProgram("SolidColor");
        var tint = solid.Parameters["Tint"]!;
        using var pipeline = new GraphicsPipeline(device, new GraphicsPipelineDescription { Program = solid });
        using var suspendedTarget = CreateTarget(device);
        using var laterTarget = CreateTarget(device);
        using var first = new GraphicsCommandList(device, "First");
        using var second = new GraphicsCommandList(device, "Second");
        tint.SetValue(Color.Red);

        Task.Run(() =>
        {
            using (first.Bind())
            {
                using (device.SuspendCommandList()) tint.SetValue(Color.Green);
                device.SetRenderTarget(suspendedTarget);
                device.SetPipeline(pipeline);
                device.Draw(3);
            }
            using (second.Bind())
            {
                device.SetRenderTarget(laterTarget);
                device.SetPipeline(pipeline);
                device.Draw(3);
            }
        }).GetAwaiter().GetResult();
        device.Execute(first);
        device.Execute(second);

        AssertAllPixels(Color.Green, ReadPixels(suspendedTarget));
        AssertAllPixels(Color.Green, ReadPixels(laterTarget));
    });

    [TestMethod]
    [DataRow(false)]
    [DataRow(true)]
    public void SetDataOnAListRecordsTheCopyInTheList(bool gpuBasedValidation) => TestGame.RunOnce(device =>
    {
        using var library = LoadLibrary(device, "Basic");
        using var pipeline = VertexColorPipeline(device, library);
        using var vertices = CreateVertexBuffer<VertexPositionColor>(device, FullQuad(0f, Color.Red));
        using var before = CreateTarget(device);
        using var after = CreateTarget(device);
        using var commandList = new GraphicsCommandList(device, "Upload");

        device.SetRenderTarget(before);
        device.SetPipeline(pipeline);
        device.SetVertexBuffer(vertices);
        device.Draw(6);

        Task.Run(() =>
        {
            using var binding = commandList.Bind();
            vertices.SetData<VertexPositionColor>(FullQuad(0f, Color.Green));
            device.SetRenderTarget(after);
            device.SetPipeline(pipeline);
            device.SetVertexBuffer(vertices);
            device.Draw(6);
        }).GetAwaiter().GetResult();
        device.Execute(commandList);

        AssertAllPixels(Color.Red, ReadPixels(before));
        AssertAllPixels(Color.Green, ReadPixels(after));
    }, gpuBasedValidation);

    private static void ExecuteInOrder(GraphicsDevice device, bool executesTogether, GraphicsCommandList first, GraphicsCommandList second)
    {
        if (executesTogether)
        {
            device.Execute([first, second]);
            return;
        }
        device.Execute(first);
        device.Execute(second);
    }

    private static Texture2D CreateSampledTarget(GraphicsDevice device) =>
        new(device, new TextureDescription { Width = TargetSize, Height = TargetSize, Usage = TextureUsage.RenderTarget | TextureUsage.ShaderResource });

    private static GraphicsPipeline VertexColorPipeline(GraphicsDevice device, ShaderLibrary library) => new(device, new GraphicsPipelineDescription
    {
        Program = library.GetProgram("VertexColor"),
        VertexLayout = VertexPositionColor.Layout,
        RasterizerState = RasterizerState.CullNone,
    });

    private static VertexPositionTexture[] TexturedHalf(float left, float right) =>
    [
        new(new Vector3(left, 1f, 0f), new Vector2(0f, 0f)),
        new(new Vector3(right, 1f, 0f), new Vector2(1f, 0f)),
        new(new Vector3(left, -1f, 0f), new Vector2(0f, 1f)),
        new(new Vector3(left, -1f, 0f), new Vector2(0f, 1f)),
        new(new Vector3(right, 1f, 0f), new Vector2(1f, 0f)),
        new(new Vector3(right, -1f, 0f), new Vector2(1f, 1f)),
    ];
}
