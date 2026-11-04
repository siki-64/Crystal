using Crystal.Graphics;
using Crystal.ShaderCompiler;
using static Crystal.Tests.ComputeTests;

namespace Crystal.Tests;

[TestClass]
public sealed class RuntimeShaderCompilerTests
{
    private const string FillSource = """
        #pragma program Fill compute=FillCS
        cbuffer FillParameters : register(b0)
        {
            uint FillValue;
        };
        RWStructuredBuffer<uint> Output : register(u0);
        Texture2D<float4> Unused : register(t0);
        [numthreads(64, 1, 1)]
        void FillCS(uint3 id : SV_DispatchThreadID)
        {
            Output[id.x] = FillValue + id.x;
        }
        """;

    [TestMethod]
    public void CompiledSourceLoadsAndDispatches() => TestGame.RunOnce(device =>
    {
        using var compiler = new RuntimeShaderCompiler();
        var compilation = compiler.Compile("Fill.hlsl", FillSource);
        Assert.IsTrue(compilation.Succeeded, string.Join("\n", compilation.Errors));

        using var library = new ShaderLibrary(device, new MemoryStream(compilation.Library!));
        var program = library.GetProgram("Fill");
        using var pipeline = new ComputePipeline(device, program);
        using var output = CreateUnorderedBuffer(device, 64);
        program.Parameters["Output"]!.SetValue(output);
        program.Parameters["FillValue"]!.SetValue(7u);

        device.Dispatch(pipeline, 1);

        var values = new uint[64];
        output.GetData<uint>(values);
        for (var index = 0; index < values.Length; index++) Assert.AreEqual(7u + (uint)index, values[index]);
    });

    [TestMethod]
    public void CompiledStageRunsFromADescription() => TestGame.RunOnce(device =>
    {
        using var compiler = new RuntimeShaderCompiler();
        var stage = compiler.CompileStage("Fill.hlsl", FillSource, "FillCS", ShaderStage.Compute);
        Assert.IsTrue(stage.Succeeded, string.Join("\n", stage.Errors));
        var output = stage.DescriptorSlots.Single(slot => slot.RegisterClass == 'u' && slot.Register == 0);
        Assert.IsFalse(stage.DescriptorSlots.Any(slot => slot.RegisterClass == 't'));

        using var library = new ShaderLibrary(device, [new ShaderProgramDescription
        {
            Name = "Fill",
            ComputeShader = stage.Bytecode,
            ThreadGroupSize = new ThreadGroupSize(64, 1, 1),
            UsesDescriptorIndices = true,
            ConstantBuffers = [new ShaderConstantBufferDescription("FillParameters", 0, 16, ShaderStages.Compute, [new ShaderVariableDescription("FillValue", 0, 4, ShaderVariableClass.Scalar, 1, 1)])],
            Resources =
            [
                new ShaderResourceDescription("Output", ShaderResourceKind.ReadWriteStructuredBuffer, ShaderResourceDimension.Buffer, 0, ShaderStages.Compute, ComputeDescriptorSlot: output.Slot),
                new ShaderResourceDescription("Unused", ShaderResourceKind.Texture, ShaderResourceDimension.Texture2D, 0, ShaderStages.Compute, ComputeDescriptorSlot: -1),
            ],
        }]);
        var program = library.GetProgram("Fill");
        using var pipeline = new ComputePipeline(device, program);
        using var buffer = CreateUnorderedBuffer(device, 64);
        using var texture = new Texture2D(device, new TextureDescription { Width = 1, Height = 1 });
        program.Parameters["Output"]!.SetValue(buffer);
        program.Parameters["Unused"]!.SetValue(texture);
        program.Parameters["FillValue"]!.SetValue(3u);

        device.Dispatch(pipeline, 1);

        var values = new uint[64];
        buffer.GetData<uint>(values);
        for (var index = 0; index < values.Length; index++) Assert.AreEqual(3u + (uint)index, values[index]);
    });

    [TestMethod]
    public void DescriptorSlotsMustBeDistinctPerStage() => TestGame.RunOnce(device =>
    {
        Assert.ThrowsExactly<ArgumentException>(() => new ShaderLibrary(device, [new ShaderProgramDescription
        {
            Name = "Clash",
            ComputeShader = new byte[] { (byte)'D', (byte)'X', (byte)'B', (byte)'C' },
            ThreadGroupSize = new ThreadGroupSize(1, 1, 1),
            UsesDescriptorIndices = true,
            Resources =
            [
                new ShaderResourceDescription("First", ShaderResourceKind.Texture, ShaderResourceDimension.Texture2D, 0, ShaderStages.Compute, ComputeDescriptorSlot: 0),
                new ShaderResourceDescription("Second", ShaderResourceKind.Texture, ShaderResourceDimension.Texture2D, 1, ShaderStages.Compute, ComputeDescriptorSlot: 0),
            ],
        }]));
    });

    [TestMethod]
    public void ErrorsComeBackWithoutALibrary()
    {
        using var compiler = new RuntimeShaderCompiler();
        var compilation = compiler.Compile("Broken.hlsl", "#pragma program Broken compute=Missing\n");
        Assert.IsFalse(compilation.Succeeded);
        Assert.IsNotEmpty(compilation.Errors);
    }
}
