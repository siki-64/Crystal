using System.Numerics;
using Crystal.Graphics;
using static Crystal.Tests.GraphicsTestHelpers;

namespace Crystal.Tests;

[TestClass]
public sealed class ShaderTests
{
    [TestMethod]
    public void LibraryReflectsProgramsAndParameters() => TestGame.RunOnce(device =>
    {
        using var library = LoadLibrary(device, "Basic");
        CollectionAssert.AreEquivalent(
            new[] { "VertexColor", "SolidColor", "Transformed", "TransformedColumns", "Instanced", "ArrayValues", "DepthOnly" },
            library.Programs.Select(program => program.Name).ToArray());
        Assert.IsFalse(library.TryGetProgram("Missing", out _));
        Assert.ThrowsExactly<KeyNotFoundException>(() => library.GetProgram("Missing"));

        var solid = library.GetProgram("SolidColor");
        Assert.AreEqual(1, solid.ConstantBuffers.Count);
        Assert.AreEqual("Material", solid.ConstantBuffers[0].Name);
        Assert.AreEqual(16, solid.ConstantBuffers["Material"]!.Size);
        Assert.IsNull(solid.Parameters["Missing"]);
        var tint = solid.Parameters["Tint"]!;
        Assert.AreEqual(ShaderParameterKind.Value, tint.Kind);
        Assert.AreEqual(4, tint.Columns);
        Assert.ThrowsExactly<InvalidOperationException>(() => tint.SetValue(Matrix4x4.Identity));
        Assert.ThrowsExactly<ArgumentException>(() => tint.SetValue((ReadOnlySpan<Vector4>)[Vector4.One, Vector4.One]));

        var transform = library.GetProgram("Transformed").Parameters["WorldViewProjection"]!;
        Assert.AreEqual(4, transform.Rows);
        Assert.AreEqual(4, transform.Columns);
        Assert.IsEmpty(library.GetProgram("VertexColor").ConstantBuffers);

        using var resources = LoadLibrary(device, "Resources");
        var textured = resources.GetProgram("Textured");
        Assert.AreEqual(ShaderParameterKind.Texture, textured.Parameters["DiffuseTexture"]!.Kind);
        Assert.AreEqual(ShaderParameterKind.Sampler, textured.Parameters["DiffuseSampler"]!.Kind);
        Assert.AreEqual(ShaderParameterKind.Buffer, resources.GetProgram("PulledVertices").Parameters["Positions"]!.Kind);
        Assert.AreEqual(ShaderParameterKind.ReadWriteBuffer, resources.GetProgram("CountPixels").Parameters["Counter"]!.Kind);
        Assert.ThrowsExactly<InvalidOperationException>(() => textured.Parameters["DiffuseTexture"]!.SetValue(1f));
    });

    [TestMethod]
    [DataRow(false)]
    [DataRow(true)]
    public void VertexColoredTriangleDraws(bool gpuBasedValidation) => TestGame.RunOnce(device =>
    {
        using var library = LoadLibrary(device, "Basic");
        using var target = CreateTarget(device);
        using var pipeline = new GraphicsPipeline(device, new GraphicsPipelineDescription
        {
            Program = library.GetProgram("VertexColor"),
            VertexLayout = VertexPositionColor.Layout,
            RasterizerState = RasterizerState.CullNone,
        });
        using var vertices = CreateVertexBuffer<VertexPositionColor>(device,
        [
            new(new Vector3(-1f, -1f, 0f), Color.Red),
            new(new Vector3(-1f, 1f, 0f), Color.Red),
            new(new Vector3(1f, -1f, 0f), Color.Red),
        ]);

        device.SetRenderTarget(target);
        device.Clear(Color.Black);
        device.SetPipeline(pipeline);
        device.SetVertexBuffer(vertices);
        device.Draw(3);

        var pixels = ReadPixels(target);
        AssertPixel(Color.Red, pixels, 4, 60);
        AssertPixel(Color.Black, pixels, 60, 4);
    }, gpuBasedValidation);

    [TestMethod]
    [DataRow(false)]
    [DataRow(true)]
    public void FullscreenDrawUsesConstantValues(bool gpuBasedValidation) => TestGame.RunOnce(device =>
    {
        using var library = LoadLibrary(device, "Basic");
        using var target = CreateTarget(device);
        var program = library.GetProgram("SolidColor");
        using var pipeline = new GraphicsPipeline(device, new GraphicsPipelineDescription { Program = program });

        device.SetRenderTarget(target);
        device.SetPipeline(pipeline);
        program.Parameters["Tint"]!.SetValue(Color.Blue);
        device.Draw(3);
        AssertAllPixels(Color.Blue, ReadPixels(target));

        program.ConstantBuffers["Material"]!.SetValue(new Vector4(0f, 1f, 0f, 1f));
        device.Draw(3);
        AssertAllPixels(new Color(0, 255, 0), ReadPixels(target));
    }, gpuBasedValidation);

    [TestMethod]
    public void ConstantBufferCanComeFromAGpuBuffer() => TestGame.RunOnce(device =>
    {
        using var library = LoadLibrary(device, "Basic");
        using var target = CreateTarget(device);
        var program = library.GetProgram("SolidColor");
        using var pipeline = new GraphicsPipeline(device, new GraphicsPipelineDescription { Program = program });
        using var constants = new GraphicsBuffer(device, new BufferDescription { Size = 512, Usage = BufferUsage.Constant });
        constants.SetData<Vector4>([new Vector4(1f, 0f, 1f, 1f)], 256);

        var material = program.ConstantBuffers["Material"]!;
        Assert.ThrowsExactly<ArgumentOutOfRangeException>(() => material.SetBuffer(constants, 16));
        material.SetBuffer(constants, 256);
        device.SetRenderTarget(target);
        device.SetPipeline(pipeline);
        device.Draw(3);
        AssertAllPixels(new Color(255, 0, 255), ReadPixels(target));

        material.SetBuffer(null);
        material.SetValue(new Vector4(1f, 1f, 0f, 1f));
        device.Draw(3);
        AssertAllPixels(new Color(255, 255, 0), ReadPixels(target));
    });

    [TestMethod]
    public void ScalarAndVectorArraysUseRegisterStrides() => TestGame.RunOnce(device =>
    {
        using var library = LoadLibrary(device, "Basic");
        using var target = CreateTarget(device, 32);
        var program = library.GetProgram("ArrayValues");
        using var pipeline = new GraphicsPipeline(device, new GraphicsPipelineDescription { Program = program });

        program.Parameters["Weights"]!.SetValue((ReadOnlySpan<float>)[1f, 0f, 1f, 1f]);
        program.Parameters["Palette"]!.SetValue((ReadOnlySpan<Vector4>)[Vector4.Zero, new Vector4(0f, 1f, 1f, 1f)]);
        program.Parameters["PaletteIndex"]!.SetValue(1u);
        device.SetRenderTarget(target);
        device.SetPipeline(pipeline);
        device.Draw(3);

        var pixels = ReadPixels(target, 32);
        AssertPixel(new Color(255, 0, 255), pixels, 4, 4, 32);
        AssertPixel(new Color(0, 255, 255), pixels, 28, 4, 32);
    });

    [TestMethod]
    [DataRow("Transformed", "WorldViewProjection")]
    [DataRow("TransformedColumns", "ColumnWorldViewProjection")]
    public void MatrixParametersUseRowVectors(string programName, string parameterName) => TestGame.RunOnce(device =>
    {
        using var library = LoadLibrary(device, "Basic");
        using var target = CreateTarget(device);
        var program = library.GetProgram(programName);
        using var pipeline = new GraphicsPipeline(device, new GraphicsPipelineDescription
        {
            Program = program,
            VertexLayout = VertexPositionColor.Layout,
            RasterizerState = RasterizerState.CullNone,
        });
        using var vertices = CreateVertexBuffer<VertexPositionColor>(device, LeftHalfQuad(Color.White));

        library.GetProgram(programName).ConstantBuffers["Material"]!.SetValue(Vector4.One);
        program.Parameters[parameterName]!.SetValue(Matrix4x4.CreateScale(1f, 0.5f, 1f) * Matrix4x4.CreateTranslation(1f, 0f, 0f));
        device.SetRenderTarget(target);
        device.Clear(Color.Black);
        device.SetPipeline(pipeline);
        device.SetVertexBuffer(vertices);
        device.Draw(6);

        var pixels = ReadPixels(target);
        AssertPixel(Color.Black, pixels, 16, 32);
        AssertPixel(Color.White, pixels, 48, 32);
        AssertPixel(Color.Black, pixels, 48, 4);
        AssertPixel(Color.Black, pixels, 48, 60);
    });

    [TestMethod]
    [DataRow(false)]
    [DataRow(true)]
    public void IndexedAndInstancedDrawsWork(bool gpuBasedValidation) => TestGame.RunOnce(device =>
    {
        using var library = LoadLibrary(device, "Basic");
        using var target = CreateTarget(device);
        using var colorPipeline = new GraphicsPipeline(device, new GraphicsPipelineDescription
        {
            Program = library.GetProgram("VertexColor"),
            VertexLayout = VertexPositionColor.Layout,
            RasterizerState = RasterizerState.CullNone,
        });
        using var quad = CreateVertexBuffer<VertexPositionColor>(device,
        [
            new(new Vector3(-1f, 1f, 0f), Color.Green),
            new(new Vector3(0f, 1f, 0f), Color.Green),
            new(new Vector3(-1f, 0f, 0f), Color.Green),
            new(new Vector3(0f, 0f, 0f), Color.Green),
        ]);
        using var indices = new GraphicsBuffer(device, new BufferDescription { Size = 6 * sizeof(ushort), Stride = sizeof(ushort), Usage = BufferUsage.Index });
        indices.SetData<ushort>([0, 1, 2, 2, 1, 3]);

        device.SetRenderTarget(target);
        device.Clear(Color.Black);
        device.SetPipeline(colorPipeline);
        device.SetVertexBuffer(quad);
        Assert.ThrowsExactly<InvalidOperationException>(() => device.DrawIndexed(6));
        device.SetIndexBuffer(indices);
        device.DrawIndexed(6);

        var instanceLayout = new VertexLayout(
            new VertexElement("POSITION", 0, VertexElementFormat.Vector3, 0),
            new VertexElement("TEXCOORD", 1, VertexElementFormat.Vector2, 0, Slot: 1, InstanceStepRate: 1),
            new VertexElement("COLOR", 1, VertexElementFormat.Color, 8, Slot: 1, InstanceStepRate: 1));
        using var instancedPipeline = new GraphicsPipeline(device, new GraphicsPipelineDescription
        {
            Program = library.GetProgram("Instanced"),
            VertexLayout = instanceLayout,
            RasterizerState = RasterizerState.CullNone,
        });
        using var positions = CreateVertexBuffer<Vector3>(device, [new(0f, 1f, 0f), new(1f, 1f, 0f), new(0f, 0f, 0f), new(1f, 0f, 0f)]);
        using var instances = CreateVertexBuffer<InstanceData>(device, [new(Vector2.Zero, Color.Blue), new(new Vector2(0f, -1f), Color.Red)]);
        device.SetPipeline(instancedPipeline);
        device.SetVertexBuffer(positions);
        Assert.ThrowsExactly<InvalidOperationException>(() => device.DrawIndexedInstanced(6, 2));
        device.SetVertexBuffer(instances, 1);
        device.DrawIndexedInstanced(6, 2);

        var pixels = ReadPixels(target);
        AssertPixel(Color.Green, pixels, 16, 16);
        AssertPixel(Color.Blue, pixels, 48, 16);
        AssertPixel(Color.Red, pixels, 48, 48);
        AssertPixel(Color.Black, pixels, 16, 48);
    }, gpuBasedValidation);

    [TestMethod]
    public void ViewportAndScissorLimitDrawing() => TestGame.RunOnce(device =>
    {
        using var library = LoadLibrary(device, "Basic");
        using var target = CreateTarget(device);
        var program = library.GetProgram("SolidColor");
        using var pipeline = new GraphicsPipeline(device, new GraphicsPipelineDescription { Program = program });

        device.SetRenderTarget(target);
        Assert.AreEqual(new Viewport(0, 0, TargetSize, TargetSize), device.Viewport);
        Assert.AreEqual(new Rectangle(0, 0, TargetSize, TargetSize), device.ScissorRectangle);
        device.Clear(Color.Black);
        device.SetPipeline(pipeline);
        program.Parameters["Tint"]!.SetValue(Color.Red);
        device.Viewport = new Viewport(0, 0, 32, 32);
        device.Draw(3);
        device.Viewport = new Viewport(0, 0, TargetSize, TargetSize);
        device.ScissorRectangle = new Rectangle(32, 32, 32, 32);
        program.Parameters["Tint"]!.SetValue(Color.Blue);
        device.Draw(3);

        var pixels = ReadPixels(target);
        AssertPixel(Color.Red, pixels, 16, 16);
        AssertPixel(Color.Black, pixels, 48, 16);
        AssertPixel(Color.Black, pixels, 16, 48);
        AssertPixel(Color.Blue, pixels, 48, 48);

        device.SetRenderTarget(target);
        Assert.AreEqual(new Rectangle(0, 0, TargetSize, TargetSize), device.ScissorRectangle);
    });

    [TestMethod]
    public void AdditiveBlendAddsToTheTarget() => TestGame.RunOnce(device =>
    {
        using var library = LoadLibrary(device, "Basic");
        using var target = CreateTarget(device);
        var program = library.GetProgram("SolidColor");
        using var pipeline = new GraphicsPipeline(device, new GraphicsPipelineDescription { Program = program, BlendState = BlendState.Additive });

        device.SetRenderTarget(target);
        device.Clear(new Color(100, 0, 0));
        device.SetPipeline(pipeline);
        program.Parameters["Tint"]!.SetValue(new Color(50, 80, 0));
        device.Draw(3);
        device.Draw(3);
        AssertAllPixels(new Color(200, 160, 0), ReadPixels(target));
    });

    [TestMethod]
    [DataRow(false)]
    [DataRow(true)]
    public void DepthTestKeepsTheNearestSurface(bool gpuBasedValidation) => TestGame.RunOnce(device =>
    {
        using var library = LoadLibrary(device, "Basic");
        using var target = CreateTarget(device);
        using var depth = new Texture2D(device, new TextureDescription
        {
            Width = TargetSize,
            Height = TargetSize,
            Format = SurfaceFormat.Depth32Float,
            Usage = TextureUsage.DepthStencil,
        });
        using var pipeline = new GraphicsPipeline(device, new GraphicsPipelineDescription
        {
            Program = library.GetProgram("VertexColor"),
            VertexLayout = VertexPositionColor.Layout,
            RasterizerState = RasterizerState.CullNone,
            DepthStencilFormat = SurfaceFormat.Depth32Float,
        });
        using var depthOnly = new GraphicsPipeline(device, new GraphicsPipelineDescription
        {
            Program = library.GetProgram("DepthOnly"),
            VertexLayout = VertexPositionColor.Layout,
            RasterizerState = RasterizerState.CullNone,
            RenderTargetFormats = [],
            DepthStencilFormat = SurfaceFormat.Depth32Float,
        });
        using var near = CreateVertexBuffer<VertexPositionColor>(device, FullQuad(0.25f, Color.Red));
        using var far = CreateVertexBuffer<VertexPositionColor>(device, FullQuad(0.75f, Color.Green));
        using var nearest = CreateVertexBuffer<VertexPositionColor>(device, LeftHalfQuad(Color.White, 0.1f));
        using var middle = CreateVertexBuffer<VertexPositionColor>(device, FullQuad(0.2f, Color.Blue));

        device.SetRenderTarget(target, depth);
        device.Clear(Color.Black);
        device.ClearDepthStencil();
        device.SetPipeline(pipeline);
        device.SetVertexBuffer(near);
        device.Draw(6);
        device.SetVertexBuffer(far);
        device.Draw(6);

        device.SetRenderTargets([], depth);
        device.SetPipeline(depthOnly);
        device.SetVertexBuffer(nearest);
        device.Draw(6);

        device.SetRenderTarget(target, depth);
        device.SetPipeline(pipeline);
        device.SetVertexBuffer(middle);
        device.Draw(6);

        var pixels = ReadPixels(target);
        AssertPixel(Color.Red, pixels, 16, 32);
        AssertPixel(Color.Blue, pixels, 48, 32);
    }, gpuBasedValidation);

    [TestMethod]
    [DataRow(false)]
    [DataRow(true)]
    public void TexturesAreSampledWithTheBoundSampler(bool gpuBasedValidation) => TestGame.RunOnce(device =>
    {
        using var library = LoadLibrary(device, "Resources");
        using var target = CreateTarget(device);
        using var texture = new Texture2D(device, new TextureDescription { Width = 2, Height = 2 });
        texture.SetData<Color>([Color.Red, new Color(0, 255, 0), Color.Blue, Color.White]);
        var program = library.GetProgram("Textured");
        using var pipeline = new GraphicsPipeline(device, new GraphicsPipelineDescription
        {
            Program = program,
            VertexLayout = VertexPositionTexture.Layout,
            RasterizerState = RasterizerState.CullNone,
        });
        using var vertices = CreateVertexBuffer<VertexPositionTexture>(device,
        [
            new(new Vector3(-1f, 1f, 0f), new Vector2(0f, 0f)),
            new(new Vector3(1f, 1f, 0f), new Vector2(1f, 0f)),
            new(new Vector3(-1f, -1f, 0f), new Vector2(0f, 1f)),
            new(new Vector3(-1f, -1f, 0f), new Vector2(0f, 1f)),
            new(new Vector3(1f, 1f, 0f), new Vector2(1f, 0f)),
            new(new Vector3(1f, -1f, 0f), new Vector2(1f, 1f)),
        ]);

        device.SetRenderTarget(target);
        device.SetPipeline(pipeline);
        device.SetVertexBuffer(vertices);
        Assert.ThrowsExactly<InvalidOperationException>(() => device.Draw(6));
        program.Parameters["DiffuseTexture"]!.SetValue(texture);
        program.Parameters["DiffuseSampler"]!.SetValue(device.GetSamplerState(SamplerDescription.PointClamp));
        device.Draw(6);

        var pixels = ReadPixels(target);
        AssertPixel(Color.Red, pixels, 16, 16);
        AssertPixel(new Color(0, 255, 0), pixels, 48, 16);
        AssertPixel(Color.Blue, pixels, 16, 48);
        AssertPixel(Color.White, pixels, 48, 48);

        using var cube = new TextureCube(device, new TextureDescription { Width = 4, Height = 4 });
        Assert.ThrowsExactly<ArgumentException>(() => program.Parameters["DiffuseTexture"]!.SetValue(cube));
        using var sampledTarget = new Texture2D(device, new TextureDescription { Width = TargetSize, Height = TargetSize, Usage = TextureUsage.RenderTarget | TextureUsage.ShaderResource });
        device.SetRenderTarget(sampledTarget);
        program.Parameters["DiffuseTexture"]!.SetValue(sampledTarget);
        Assert.ThrowsExactly<InvalidOperationException>(() => device.Draw(6));
    }, gpuBasedValidation);

    [TestMethod]
    [DataRow(false)]
    [DataRow(true)]
    public void ShadersReadBuffersAndDepthTextures(bool gpuBasedValidation) => TestGame.RunOnce(device =>
    {
        using var library = LoadLibrary(device, "Resources");
        using var target = CreateTarget(device);

        var pulled = library.GetProgram("PulledVertices");
        using var pulledPipeline = new GraphicsPipeline(device, new GraphicsPipelineDescription { Program = pulled, RasterizerState = RasterizerState.CullNone });
        using var positions = new GraphicsBuffer(device, new BufferDescription { Size = 3 * 8, Stride = 8, Usage = BufferUsage.Structured });
        positions.SetData<Vector2>([new(-1f, -1f), new(-1f, 1f), new(1f, -1f)]);
        pulled.Parameters["Positions"]!.SetValue(positions);
        device.SetRenderTarget(target);
        device.Clear(Color.Black);
        device.SetPipeline(pulledPipeline);
        device.Draw(3);
        var pixels = ReadPixels(target);
        AssertPixel(Color.White, pixels, 4, 60);
        AssertPixel(Color.Black, pixels, 60, 4);

        using var depth = new Texture2D(device, new TextureDescription
        {
            Width = TargetSize,
            Height = TargetSize,
            Format = SurfaceFormat.Depth32Float,
            Usage = TextureUsage.DepthStencil | TextureUsage.ShaderResource,
            ClearDepth = 0.25f,
        });
        device.SetRenderTarget(target, depth);
        device.ClearDepthStencil(0.25f);
        device.SetRenderTarget(target);
        var visualize = library.GetProgram("DepthVisualize");
        visualize.Parameters["DepthTexture"]!.SetValue(depth);
        using var visualizePipeline = new GraphicsPipeline(device, new GraphicsPipelineDescription { Program = visualize });
        device.SetPipeline(visualizePipeline);
        device.Draw(3);
        AssertAllPixels(new Color(64, 0, 0), ReadPixels(target));

        var raw = library.GetProgram("RawColor");
        using var colorData = new GraphicsBuffer(device, new BufferDescription { Size = 16, Usage = BufferUsage.UnorderedAccess });
        colorData.SetData<Vector4>([new Vector4(0f, 0f, 1f, 1f)]);
        Assert.ThrowsExactly<ArgumentException>(() => raw.Parameters["ColorData"]!.SetValue(positions));
        raw.Parameters["ColorData"]!.SetValue(colorData);
        using var rawPipeline = new GraphicsPipeline(device, new GraphicsPipelineDescription { Program = raw });
        device.SetPipeline(rawPipeline);
        device.Draw(3);
        AssertAllPixels(Color.Blue, ReadPixels(target));

        var count = library.GetProgram("CountPixels");
        using var counter = new GraphicsBuffer(device, new BufferDescription { Size = 4, Usage = BufferUsage.UnorderedAccess });
        counter.SetData<uint>([0u]);
        count.Parameters["Counter"]!.SetValue(counter);
        using var countPipeline = new GraphicsPipeline(device, new GraphicsPipelineDescription { Program = count });
        device.SetPipeline(countPipeline);
        device.Draw(3);
        device.Draw(3);
        var counted = new uint[1];
        counter.GetData<uint>(counted);
        Assert.AreEqual(2u * TargetSize * TargetSize, counted[0]);
    }, gpuBasedValidation);

    [TestMethod]
    public void PipelineMustMatchTheBoundTargets() => TestGame.RunOnce(device =>
    {
        using var library = LoadLibrary(device, "Basic");
        using var target = CreateTarget(device);
        using var hdrPipeline = new GraphicsPipeline(device, new GraphicsPipelineDescription
        {
            Program = library.GetProgram("SolidColor"),
            RenderTargetFormats = [SurfaceFormat.Rgba16Float],
        });
        using var backBufferPipeline = new GraphicsPipeline(device, new GraphicsPipelineDescription
        {
            Program = library.GetProgram("SolidColor"),
            RenderTargetFormats = [device.BackBufferFormat],
        });

        Assert.ThrowsExactly<InvalidOperationException>(() => device.Draw(3));
        device.SetRenderTarget(target);
        device.SetPipeline(hdrPipeline);
        Assert.ThrowsExactly<InvalidOperationException>(() => device.Draw(3));
        device.SetPipeline(backBufferPipeline);
        device.Draw(3);
        device.SetRenderTarget(null);
        device.Draw(3);

        Assert.ThrowsExactly<ArgumentException>(() => new GraphicsPipeline(device, new GraphicsPipelineDescription { Program = library.GetProgram("VertexColor") }));
        Assert.ThrowsExactly<ArgumentException>(() => new GraphicsPipeline(device, new GraphicsPipelineDescription
        {
            Program = library.GetProgram("VertexColor"),
            VertexLayout = VertexPositionTexture.Layout,
        }));
        Assert.ThrowsExactly<ArgumentException>(() => new GraphicsPipeline(device, new GraphicsPipelineDescription
        {
            Program = library.GetProgram("SolidColor"),
            RenderTargetFormats = [SurfaceFormat.Depth32Float],
        }));
    });

    [TestMethod]
    public void ManyDrawsSpanSeveralUploadPagesAcrossFrames() => TestGame.RunFrames((device, frame) =>
    {
        using var library = LoadLibrary(device, "Basic");
        using var target = CreateTarget(device, 8);
        var program = library.GetProgram("SolidColor");
        using var pipeline = new GraphicsPipeline(device, new GraphicsPipelineDescription { Program = program });
        var tint = program.Parameters["Tint"]!;

        device.SetRenderTarget(target);
        device.SetPipeline(pipeline);
        for (var draw = 0; draw < 5000; draw++)
        {
            tint.SetValue(new Vector4(draw % 2, frame, 0f, 1f));
            device.Draw(3);
        }
        Assert.IsGreaterThanOrEqualTo(2, device.UploadRingPageCount);
        AssertAllPixels(new Color(255, (byte)(frame * 255), 0), ReadPixels(target, 8));
    }, frameCount: 2);

    [TestMethod]
    public void DrawsDoNotAllocate() => TestGame.RunOnce(device =>
    {
        using var library = LoadLibrary(device, "Resources");
        using var target = CreateTarget(device, 8);
        using var texture = new Texture2D(device, new TextureDescription { Width = 1, Height = 1 });
        texture.SetData<Color>([Color.White]);
        var program = library.GetProgram("Textured");
        program.Parameters["DiffuseTexture"]!.SetValue(texture);
        using var pipeline = new GraphicsPipeline(device, new GraphicsPipelineDescription { Program = program, VertexLayout = VertexPositionTexture.Layout });
        using var vertices = CreateVertexBuffer<VertexPositionTexture>(device, [default, default, default]);

        using var basic = LoadLibrary(device, "Basic");
        var solid = basic.GetProgram("SolidColor");
        using var solidPipeline = new GraphicsPipeline(device, new GraphicsPipelineDescription { Program = solid });
        var tint = solid.Parameters["Tint"]!;

        device.SetRenderTarget(target);
        device.SetVertexBuffer(vertices);
        void DrawBatch()
        {
            for (var draw = 0; draw < 1000; draw++)
            {
                device.SetPipeline(pipeline);
                device.Draw(3);
                device.SetPipeline(solidPipeline);
                tint.SetValue(new Vector4(draw, 0f, 0f, 1f));
                device.Draw(3);
            }
        }

        DrawBatch();
        var before = GC.GetAllocatedBytesForCurrentThread();
        DrawBatch();
        Assert.AreEqual(0, GC.GetAllocatedBytesForCurrentThread() - before);
    });

    [TestMethod]
    public void LibrariesAndPipelinesCanBeCreatedOnWorkerThreads() => TestGame.RunOnce(device =>
    {
        var (library, pipeline) = Task.Run(() =>
        {
            var created = LoadLibrary(device, "Basic");
            return (created, new GraphicsPipeline(device, new GraphicsPipelineDescription { Program = created.GetProgram("SolidColor") }));
        }).GetAwaiter().GetResult();
        using var target = CreateTarget(device);

        using (library)
        using (pipeline)
        {
            library.GetProgram("SolidColor").Parameters["Tint"]!.SetValue(Color.Red);
            device.SetRenderTarget(target);
            device.SetPipeline(pipeline);
            device.Draw(3);
            AssertAllPixels(Color.Red, ReadPixels(target));
        }

        Assert.ThrowsExactly<ObjectDisposedException>(() => device.SetPipeline(pipeline));
    });

    private readonly record struct InstanceData(Vector2 Offset, Color Color);
}
