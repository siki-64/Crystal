using Crystal.Graphics;
using static Crystal.Tests.GraphicsTestHelpers;

namespace Crystal.Tests;

[TestClass]
public sealed class QueryTests
{
    [TestMethod]
    [DataRow(false)]
    [DataRow(true)]
    public void OcclusionQueriesCountVisiblePixels(bool gpuBasedValidation) => TestGame.RunOnce(device =>
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
        using var near = CreateVertexBuffer<VertexPositionColor>(device, LeftHalfQuad(Color.Red, 0.25f));
        using var far = CreateVertexBuffer<VertexPositionColor>(device, FullQuad(0.75f, Color.Green));
        using var visible = new OcclusionQuery(device, "Visible");
        using var hidden = new OcclusionQuery(device, "Hidden");

        device.SetRenderTarget(target, depth);
        device.Clear(Color.Black);
        device.ClearDepthStencil();
        device.SetPipeline(pipeline);
        visible.Begin();
        Assert.IsTrue(visible.IsActive);
        device.SetVertexBuffer(near);
        device.Draw(6);
        visible.End();
        hidden.Begin();
        device.SetVertexBuffer(far);
        device.Draw(6);
        hidden.End();
        Assert.IsFalse(visible.IsComplete);
        Assert.ThrowsExactly<InvalidOperationException>(() => _ = visible.PixelCount);

        ReadPixels(target);

        Assert.IsTrue(visible.IsComplete);
        Assert.AreEqual(TargetSize * TargetSize / 2, visible.PixelCount);
        Assert.AreEqual(TargetSize * TargetSize / 2, hidden.PixelCount);
    }, gpuBasedValidation);

    [TestMethod]
    public void OcclusionQueryResultsArriveInLaterFrames() => TestGame.RunFrames((device, frame) =>
    {
        using var library = LoadLibrary(device, "Basic");
        using var target = CreateTarget(device);
        var solid = library.GetProgram("SolidColor");
        solid.Parameters["Tint"]!.SetValue(Color.White);
        using var pipeline = new GraphicsPipeline(device, new GraphicsPipelineDescription { Program = solid });
        var queries = new OcclusionQuery[300];
        for (var index = 0; index < queries.Length; index++) queries[index] = new OcclusionQuery(device);

        device.SetRenderTarget(target);
        device.SetPipeline(pipeline);
        foreach (var query in queries)
        {
            query.Begin();
            device.Draw(3);
            query.End();
        }
        ReadPixels(target);
        foreach (var query in queries)
        {
            Assert.IsTrue(query.IsComplete);
            Assert.AreEqual(TargetSize * TargetSize, query.PixelCount);
            query.Dispose();
        }
    }, frameCount: 2);

    [TestMethod]
    public void OcclusionQueriesMustBeEndedBeforeSubmission() => TestGame.RunOnce(device =>
    {
        using var target = CreateTarget(device);
        using var first = new OcclusionQuery(device, "First");
        using var second = new OcclusionQuery(device, "Second");

        device.SetRenderTarget(target);
        first.Begin();
        Assert.ThrowsExactly<InvalidOperationException>(second.Begin);
        Assert.ThrowsExactly<InvalidOperationException>(second.End);
        var exception = Assert.ThrowsExactly<InvalidOperationException>(() => ReadPixels(target));
        StringAssert.Contains(exception.Message, "First");
        first.End();
        ReadPixels(target);
    });

    [TestMethod]
    public void ProfilerReportsNestedScopesFromCompletedFrames()
    {
        var sampleCounts = new List<int>();
        TestGame.RunFrames((device, frame) =>
        {
            using var library = LoadLibrary(device, "Basic");
            using var target = CreateTarget(device);
            var solid = library.GetProgram("SolidColor");
            using var pipeline = new GraphicsPipeline(device, new GraphicsPipelineDescription { Program = solid });
            device.Profiler.IsEnabled = true;
            sampleCounts.Add(device.Profiler.Samples.Count);

            using (device.Profiler.Scope("Frame"))
            {
                device.SetRenderTarget(target);
                device.Clear(Color.Black);
                using (device.Profiler.Scope("Draw"))
                {
                    device.SetPipeline(pipeline);
                    device.Draw(3);
                }
            }

            if (frame < 5) return;
            var samples = device.Profiler.Samples;
            Assert.HasCount(2, samples);
            Assert.AreEqual("Frame", samples[0].Name);
            Assert.AreEqual(0, samples[0].Depth);
            Assert.AreEqual("Draw", samples[1].Name);
            Assert.AreEqual(1, samples[1].Depth);
            Assert.IsGreaterThanOrEqualTo(0.0, samples[1].GpuMilliseconds);
            Assert.IsGreaterThanOrEqualTo(samples[1].GpuMilliseconds, samples[0].GpuMilliseconds);
            Assert.IsGreaterThanOrEqualTo(samples[1].CpuMilliseconds, samples[0].CpuMilliseconds);
            Assert.IsLessThan((ulong)(frame + 1), device.Profiler.SamplesFrameNumber);
        }, frameCount: 6);
        Assert.AreEqual(0, sampleCounts[0]);
    }

    [TestMethod]
    public void ProfilerReportsFrameGpuTimeAndTimeSincePreviousFrame()
    {
        var frameTimes = new List<double>();
        var timesSincePreviousFrame = new List<double?>();
        TestGame.RunFrames((device, frame) =>
        {
            device.Profiler.IsEnabled = true;
            frameTimes.Add(device.Profiler.FrameGpuMilliseconds);
            timesSincePreviousFrame.Add(device.Profiler.GpuMillisecondsSincePreviousFrame);
            if (frame == 4) Thread.Sleep(50);
        }, frameCount: 12);

        Assert.IsNull(timesSincePreviousFrame[0]);
        var reported = Enumerable.Range(0, timesSincePreviousFrame.Count).Where(frame => timesSincePreviousFrame[frame] is not null).ToList();
        Assert.IsNotEmpty(reported);
        foreach (var frame in reported)
        {
            Assert.IsGreaterThanOrEqualTo(0.0, frameTimes[frame]);
            Assert.IsGreaterThanOrEqualTo(frameTimes[frame], timesSincePreviousFrame[frame]!.Value);
        }
        Assert.IsGreaterThanOrEqualTo(40.0, reported.Max(frame => timesSincePreviousFrame[frame]!.Value));
    }

    [TestMethod]
    public void ProfilerScopesAreValidatedAndFreeWhenDisabled() => TestGame.RunOnce(device =>
    {
        Assert.IsFalse(device.Profiler.IsEnabled);
        using (device.Profiler.Scope("Ignored")) { }
        device.Profiler.End();
        Assert.IsEmpty(device.Profiler.Samples);
    });

    [TestMethod]
    public void ProfilerEndWithoutBeginThrows() => TestGame.RunFrames((device, frame) =>
    {
        device.Profiler.IsEnabled = true;
        if (frame == 1) Assert.ThrowsExactly<InvalidOperationException>(device.Profiler.End);
    }, frameCount: 2);

    [TestMethod]
    public void ProfilerScopesDoNotAllocate() => TestGame.RunFrames((device, frame) =>
    {
        device.Profiler.IsEnabled = true;
        if (frame == 0) return;
        using (device.Profiler.Scope("Warm")) { }
        var before = GC.GetAllocatedBytesForCurrentThread();
        for (var scope = 0; scope < 500; scope++)
        {
            using (device.Profiler.Scope("Scope")) { }
        }
        Assert.AreEqual(0, GC.GetAllocatedBytesForCurrentThread() - before);
    }, frameCount: 2);

    [TestMethod]
    public void VideoMemoryInfoReportsBudgets() => TestGame.RunOnce(device =>
    {
        var info = device.GetVideoMemoryInfo();
        Assert.IsGreaterThan(0L, info.LocalBudget + info.NonLocalBudget);
        Assert.IsGreaterThanOrEqualTo(0L, info.LocalUsage);
        Assert.IsGreaterThanOrEqualTo(0L, info.NonLocalUsage);
    });
}
