using Crystal.Graphics;
using static Crystal.Tests.GraphicsTestHelpers;

namespace Crystal.Tests;

[TestClass]
public sealed class ComputeTests
{
    [TestMethod]
    public void ComputeProgramsReflectThreadGroupSize() => TestGame.RunOnce(device =>
    {
        using var library = LoadLibrary(device, "Compute");
        var fill = library.GetProgram("FillBuffer");
        Assert.IsTrue(fill.IsCompute);
        Assert.AreEqual(new ThreadGroupSize(64, 1, 1), fill.ThreadGroupSize);
        Assert.AreEqual(new ThreadGroupSize(8, 8, 1), library.GetProgram("Invert").ThreadGroupSize);
        Assert.AreEqual(ShaderParameterKind.ReadWriteBuffer, fill.Parameters["Output"]!.Kind);
        Assert.AreEqual(ShaderParameterKind.ReadWriteTexture, library.GetProgram("Invert").Parameters["Destination"]!.Kind);

        using var basic = LoadLibrary(device, "Basic");
        Assert.IsFalse(basic.GetProgram("SolidColor").IsCompute);
        Assert.ThrowsExactly<ArgumentException>(() => new ComputePipeline(device, basic.GetProgram("SolidColor")));
        Assert.ThrowsExactly<ArgumentException>(() => new GraphicsPipeline(device, new GraphicsPipelineDescription { Program = fill }));
    });

    [TestMethod]
    [DataRow(false)]
    [DataRow(true)]
    public void DispatchFillsBuffer(bool gpuBasedValidation) => TestGame.RunOnce(device =>
    {
        using var library = LoadLibrary(device, "Compute");
        var program = library.GetProgram("FillBuffer");
        using var pipeline = new ComputePipeline(device, program);
        using var output = CreateUnorderedBuffer(device, 100);
        program.Parameters["Output"]!.SetValue(output);
        program.Parameters["FillValue"]!.SetValue(1000u);
        program.Parameters["FillCount"]!.SetValue(100u);

        device.Dispatch(pipeline, 2);

        var values = new uint[100];
        output.GetData<uint>(values);
        for (var index = 0; index < values.Length; index++) Assert.AreEqual(1000u + (uint)index, values[index]);
    }, gpuBasedValidation);

    [TestMethod]
    public void DispatchReadsAndWritesTextures() => TestGame.RunOnce(device =>
    {
        using var library = LoadLibrary(device, "Compute");
        var program = library.GetProgram("Invert");
        using var pipeline = new ComputePipeline(device, program);
        using var source = new Texture2D(device, new TextureDescription { Width = 16, Height = 16 });
        var sourcePixels = new Color[16 * 16];
        Array.Fill(sourcePixels, new Color(255, 0, 64));
        source.SetData<Color>(sourcePixels);
        using var destination = new Texture2D(device, new TextureDescription
        {
            Width = 16,
            Height = 16,
            Usage = TextureUsage.ShaderResource | TextureUsage.UnorderedAccess,
        });
        program.Parameters["Source"]!.SetValue(source);
        program.Parameters["Destination"]!.SetValue(destination);

        device.Dispatch(pipeline, 2, 2);

        var pixels = new uint[16 * 16];
        destination.GetData<uint>(pixels);
        AssertAllPixels(new Color(0, 255, 191), pixels);
    });

    [TestMethod]
    [DataRow(false)]
    [DataRow(true)]
    public void MipViewsReduceAndSampleSingleMips(bool gpuBasedValidation) => TestGame.RunOnce(device =>
    {
        using var library = LoadLibrary(device, "Compute");
        var reduce = library.GetProgram("ReduceMip");
        using var reducePipeline = new ComputePipeline(device, reduce);
        using var pyramid = new Texture2D(device, new TextureDescription
        {
            Width = 16,
            Height = 16,
            MipLevels = 3,
            Usage = TextureUsage.ShaderResource | TextureUsage.UnorderedAccess,
        });
        var basePixels = new Color[16 * 16];
        Array.Fill(basePixels, Color.Black);
        basePixels[9 * 16 + 5] = Color.Red;
        pyramid.SetData<Color>(basePixels);

        Assert.AreSame(pyramid.GetMipView(1), pyramid.GetMipView(1));
        Assert.AreEqual(4, pyramid.GetMipView(2).Width);
        Assert.ThrowsExactly<ArgumentOutOfRangeException>(() => pyramid.GetMipView(3));

        for (var mipLevel = 1; mipLevel < pyramid.MipLevels; mipLevel++)
        {
            reduce.Parameters["MipSource"]!.SetValue(pyramid.GetMipView(mipLevel - 1));
            reduce.Parameters["MipTarget"]!.SetValue(pyramid.GetMipView(mipLevel));
            device.Dispatch(reducePipeline, 1, 1);
        }

        var middle = new uint[8 * 8];
        pyramid.GetData<uint>(middle, mipLevel: 1);
        AssertPixel(Color.Red, middle, 2, 4, 8);
        AssertPixel(Color.Black, middle, 3, 4, 8);
        var smallest = new uint[4 * 4];
        pyramid.GetData<uint>(smallest, mipLevel: 2);
        AssertPixel(Color.Red, smallest, 1, 2, 4);
        AssertPixel(Color.Black, smallest, 2, 2, 4);

        var invert = library.GetProgram("Invert");
        using var invertPipeline = new ComputePipeline(device, invert);
        using var inverted = new Texture2D(device, new TextureDescription
        {
            Width = 4,
            Height = 4,
            Usage = TextureUsage.ShaderResource | TextureUsage.UnorderedAccess,
        });
        invert.Parameters["Source"]!.SetValue(pyramid.GetMipView(2));
        invert.Parameters["Destination"]!.SetValue(inverted);
        device.Dispatch(invertPipeline, 1, 1);

        var invertedPixels = new uint[4 * 4];
        inverted.GetData<uint>(invertedPixels);
        AssertPixel(new Color(0, 255, 255), invertedPixels, 1, 2, 4);
        AssertPixel(Color.White, invertedPixels, 2, 2, 4);
    }, gpuBasedValidation);

    [TestMethod]
    public void DrawReadsBufferWrittenByDispatch() => TestGame.RunOnce(device =>
    {
        using var compute = LoadLibrary(device, "Compute");
        using var resources = LoadLibrary(device, "Resources");
        using var positions = CreatePositionBuffer(device);
        var writePositions = compute.GetProgram("WritePositions");
        writePositions.Parameters["PositionsOutput"]!.SetValue(positions);
        using var writePipeline = new ComputePipeline(device, writePositions);
        var pulled = resources.GetProgram("PulledVertices");
        pulled.Parameters["Positions"]!.SetValue(positions);
        using var drawPipeline = new GraphicsPipeline(device, new GraphicsPipelineDescription { Program = pulled, RasterizerState = RasterizerState.CullNone });
        using var target = CreateTarget(device);

        device.SetRenderTarget(target);
        device.Clear(Color.Black);
        device.Dispatch(writePipeline, 1);
        device.SetPipeline(drawPipeline);
        device.Draw(3);

        AssertAllPixels(Color.White, ReadPixels(target));
    });

    [TestMethod]
    [DataRow(false)]
    [DataRow(true)]
    public void AsyncComputeOutputFeedsGraphicsQueue(bool gpuBasedValidation) => TestGame.RunFrames((device, frame) =>
    {
        using var compute = LoadLibrary(device, "Compute");
        using var resources = LoadLibrary(device, "Resources");
        using var positions = CreatePositionBuffer(device);
        var writePositions = compute.GetProgram("WritePositions");
        writePositions.Parameters["PositionsOutput"]!.SetValue(positions);
        using var writePipeline = new ComputePipeline(device, writePositions);
        var pulled = resources.GetProgram("PulledVertices");
        pulled.Parameters["Positions"]!.SetValue(positions);
        using var drawPipeline = new GraphicsPipeline(device, new GraphicsPipelineDescription { Program = pulled, RasterizerState = RasterizerState.CullNone });
        using var target = CreateTarget(device);

        device.SetRenderTarget(target);
        device.Clear(Color.Black);
        device.AsyncCompute.Dispatch(writePipeline, 1);
        Assert.IsTrue(device.AsyncCompute.HasUnsubmittedWork);
        device.AsyncCompute.Submit();
        Assert.IsFalse(device.AsyncCompute.HasUnsubmittedWork);
        device.SetPipeline(drawPipeline);
        device.Draw(3);

        AssertAllPixels(Color.White, ReadPixels(target));
    }, frameCount: 3, gpuBasedValidation);

    [TestMethod]
    [DataRow(false)]
    [DataRow(true)]
    public void AsyncComputeReadsDepthWrittenOnGraphicsQueue(bool gpuBasedValidation) => TestGame.RunFrames((device, frame) =>
    {
        using var library = LoadLibrary(device, "Compute");
        var program = library.GetProgram("ReadDepth");
        using var pipeline = new ComputePipeline(device, program);
        using var depth = new Texture2D(device, new TextureDescription
        {
            Width = 8,
            Height = 8,
            Format = SurfaceFormat.Depth32Float,
            Usage = TextureUsage.DepthStencil | TextureUsage.ShaderResource,
        });
        using var output = new GraphicsBuffer(device, new BufferDescription { Size = 4, Stride = 4, Usage = BufferUsage.Structured | BufferUsage.UnorderedAccess });
        program.Parameters["DepthInput"]!.SetValue(depth);
        program.Parameters["DepthOutput"]!.SetValue(output);
        var expected = 0.25f + frame * 0.25f;

        device.SetRenderTargets([], depth);
        device.ClearDepthStencil(expected);
        device.AsyncCompute.Dispatch(pipeline, 1);
        device.AsyncCompute.Submit();
        device.ClearDepthStencil(1f);

        var values = new float[1];
        output.GetData<float>(values);
        Assert.AreEqual(expected, values[0]);
    }, frameCount: 3, gpuBasedValidation);

    [TestMethod]
    public void GraphicsQueueCannotUseUnsubmittedAsyncComputeResources() => TestGame.RunOnce(device =>
    {
        using var compute = LoadLibrary(device, "Compute");
        using var positions = CreatePositionBuffer(device);
        var writePositions = compute.GetProgram("WritePositions");
        writePositions.Parameters["PositionsOutput"]!.SetValue(positions);
        using var writePipeline = new ComputePipeline(device, writePositions);
        using var copy = new GraphicsBuffer(device, new BufferDescription { Size = positions.Size, Usage = BufferUsage.Vertex, Stride = 8 });

        device.AsyncCompute.Dispatch(writePipeline, 1);
        var exception = Assert.ThrowsExactly<InvalidOperationException>(() => device.CopyBuffer(positions, 0, copy, 0, positions.Size));
        StringAssert.Contains(exception.Message, "AsyncCompute.Submit");
        device.AsyncCompute.Submit();
        device.CopyBuffer(positions, 0, copy, 0, positions.Size);
    });

    [TestMethod]
    public void UnsubmittedAsyncComputeWorkIsSubmittedAtEndOfFrame() => TestGame.RunFrames((device, frame) =>
    {
        using var library = LoadLibrary(device, "Compute");
        var program = library.GetProgram("FillBuffer");
        using var pipeline = new ComputePipeline(device, program);
        var output = CreateUnorderedBuffer(device, 64);
        program.Parameters["Output"]!.SetValue(output);
        program.Parameters["FillValue"]!.SetValue(7u);
        program.Parameters["FillCount"]!.SetValue(64u);
        device.AsyncCompute.Dispatch(pipeline, 1);
        output.Dispose();
    }, frameCount: 3);

    [TestMethod]
    [DataRow(false)]
    [DataRow(true)]
    public void IndirectDrawUsesArgumentsAndCountWrittenByCompute(bool gpuBasedValidation) => TestGame.RunOnce(device =>
    {
        using var compute = LoadLibrary(device, "Compute");
        using var basic = LoadLibrary(device, "Basic");
        var writeArguments = compute.GetProgram("WriteDrawArguments");
        using var writePipeline = new ComputePipeline(device, writeArguments);
        using var arguments = new GraphicsBuffer(device, new BufferDescription { Size = 32, Usage = BufferUsage.Indirect | BufferUsage.UnorderedAccess });
        using var count = new GraphicsBuffer(device, new BufferDescription { Size = 4, Usage = BufferUsage.Indirect | BufferUsage.UnorderedAccess });
        writeArguments.Parameters["DrawArguments"]!.SetValue(arguments);
        writeArguments.Parameters["DrawCountOutput"]!.SetValue(count);
        var solid = basic.GetProgram("SolidColor");
        solid.Parameters["Tint"]!.SetValue(Color.Red);
        using var drawPipeline = new GraphicsPipeline(device, new GraphicsPipelineDescription { Program = solid });
        using var target = CreateTarget(device);
        device.SetRenderTarget(target);

        writeArguments.Parameters["DrawCount"]!.SetValue(0u);
        device.Clear(Color.Black);
        device.Dispatch(writePipeline, 1);
        device.SetPipeline(drawPipeline);
        device.DrawInstancedIndirect(arguments, 0, maximumDrawCount: 1, count);
        AssertAllPixels(Color.Black, ReadPixels(target));

        writeArguments.Parameters["DrawCount"]!.SetValue(1u);
        device.Dispatch(writePipeline, 1);
        device.SetPipeline(drawPipeline);
        device.DrawInstancedIndirect(arguments, 0, maximumDrawCount: 1, count);
        AssertAllPixels(Color.Red, ReadPixels(target));
    }, gpuBasedValidation);

    [TestMethod]
    public void IndexedIndirectDrawAndIndirectDispatchReadUploadedArguments() => TestGame.RunOnce(device =>
    {
        using var basic = LoadLibrary(device, "Basic");
        var solid = basic.GetProgram("SolidColor");
        solid.Parameters["Tint"]!.SetValue(Color.Blue);
        using var drawPipeline = new GraphicsPipeline(device, new GraphicsPipelineDescription { Program = solid });
        using var indices = new GraphicsBuffer(device, new BufferDescription { Size = 6, Stride = 2, Usage = BufferUsage.Index });
        indices.SetData<ushort>([0, 1, 2]);
        using var drawArguments = new GraphicsBuffer(device, new BufferDescription { Size = 20, Usage = BufferUsage.Indirect });
        drawArguments.SetData<IndirectDrawIndexedArguments>([new(3, 1, 0, 0, 0)]);
        using var target = CreateTarget(device);

        device.SetRenderTarget(target);
        device.Clear(Color.Black);
        device.SetPipeline(drawPipeline);
        device.SetIndexBuffer(indices);
        device.DrawIndexedInstancedIndirect(drawArguments);
        AssertAllPixels(Color.Blue, ReadPixels(target));

        using var compute = LoadLibrary(device, "Compute");
        var fill = compute.GetProgram("FillBuffer");
        using var fillPipeline = new ComputePipeline(device, fill);
        using var output = CreateUnorderedBuffer(device, 128);
        fill.Parameters["Output"]!.SetValue(output);
        fill.Parameters["FillValue"]!.SetValue(5u);
        fill.Parameters["FillCount"]!.SetValue(128u);
        using var dispatchArguments = new GraphicsBuffer(device, new BufferDescription { Size = 12, Usage = BufferUsage.Indirect });
        dispatchArguments.SetData<IndirectDispatchArguments>([new(2, 1, 1)]);
        device.DispatchIndirect(fillPipeline, dispatchArguments);
        device.AsyncCompute.DispatchIndirect(fillPipeline, dispatchArguments);
        device.AsyncCompute.Submit();

        var values = new uint[128];
        output.GetData<uint>(values);
        Assert.AreEqual(5u, values[0]);
        Assert.AreEqual(132u, values[127]);
    });

    [TestMethod]
    public void DispatchAndIndirectArgumentsAreValidated() => TestGame.RunOnce(device =>
    {
        using var library = LoadLibrary(device, "Compute");
        var program = library.GetProgram("FillBuffer");
        using var pipeline = new ComputePipeline(device, program);
        using var output = CreateUnorderedBuffer(device, 4);
        program.Parameters["Output"]!.SetValue(output);
        using var plain = new GraphicsBuffer(device, new BufferDescription { Size = 64, Usage = BufferUsage.UnorderedAccess });
        using var arguments = new GraphicsBuffer(device, new BufferDescription { Size = 16, Usage = BufferUsage.Indirect });

        Assert.ThrowsExactly<ArgumentOutOfRangeException>(() => device.Dispatch(pipeline, GraphicsDevice.MaximumThreadGroupCount + 1));
        Assert.ThrowsExactly<ArgumentOutOfRangeException>(() => device.Dispatch(pipeline, -1));
        Assert.ThrowsExactly<ArgumentException>(() => device.DispatchIndirect(pipeline, plain));
        Assert.ThrowsExactly<ArgumentOutOfRangeException>(() => device.DispatchIndirect(pipeline, arguments, 2));
        Assert.ThrowsExactly<ArgumentOutOfRangeException>(() => device.DispatchIndirect(pipeline, arguments, 8));
        Assert.ThrowsExactly<ArgumentOutOfRangeException>(() => device.DrawInstancedIndirect(arguments, 0, maximumDrawCount: 2));
        Assert.ThrowsExactly<ArgumentException>(() => device.DrawInstancedIndirect(arguments, 0, 1, plain));
    });

    [TestMethod]
    public void DispatchesDoNotAllocate() => TestGame.RunOnce(device =>
    {
        using var library = LoadLibrary(device, "Compute");
        var program = library.GetProgram("FillBuffer");
        using var pipeline = new ComputePipeline(device, program);
        using var output = CreateUnorderedBuffer(device, 64);
        using var arguments = new GraphicsBuffer(device, new BufferDescription { Size = 12, Usage = BufferUsage.Indirect });
        arguments.SetData<IndirectDispatchArguments>([new(1, 1, 1)]);
        program.Parameters["Output"]!.SetValue(output);
        var fillValue = program.Parameters["FillValue"]!;

        void DispatchDirect()
        {
            for (var dispatch = 0; dispatch < 500; dispatch++)
            {
                fillValue.SetValue((uint)dispatch);
                device.Dispatch(pipeline, 1);
                device.DispatchIndirect(pipeline, arguments);
            }
        }

        void DispatchAsync()
        {
            for (var dispatch = 0; dispatch < 500; dispatch++) device.AsyncCompute.Dispatch(pipeline, 1);
        }

        DispatchDirect();
        DispatchAsync();
        device.AsyncCompute.Submit();

        device.Dispatch(pipeline, 1);
        var before = GC.GetAllocatedBytesForCurrentThread();
        DispatchDirect();
        Assert.AreEqual(0, GC.GetAllocatedBytesForCurrentThread() - before);

        device.AsyncCompute.Dispatch(pipeline, 1);
        before = GC.GetAllocatedBytesForCurrentThread();
        DispatchAsync();
        Assert.AreEqual(0, GC.GetAllocatedBytesForCurrentThread() - before);
        device.AsyncCompute.Submit();
    });

    internal static GraphicsBuffer CreateUnorderedBuffer(GraphicsDevice device, int count) =>
        new(device, new BufferDescription { Size = count * 4, Stride = 4, Usage = BufferUsage.Structured | BufferUsage.UnorderedAccess });

    private static GraphicsBuffer CreatePositionBuffer(GraphicsDevice device) =>
        new(device, new BufferDescription { Size = 3 * 8, Stride = 8, Usage = BufferUsage.Structured | BufferUsage.UnorderedAccess, Name = "Positions" });
}
