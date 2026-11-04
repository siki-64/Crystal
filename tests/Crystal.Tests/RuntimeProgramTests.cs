using System.Numerics;
using System.Text;
using Crystal.Graphics;
using Crystal.Graphics.D3D12;
using TerraFX.Interop.DirectX;
using static Crystal.Tests.GraphicsTestHelpers;

namespace Crystal.Tests;

[TestClass]
public sealed class RuntimeProgramTests
{
    private const string SharedTintVertexSource = """
        cbuffer VertexValues : register(b0)
        {
            float4 Shift;
            float4 Tint;
        };

        struct Output
        {
            float4 Position : SV_Position;
            float4 Color : COLOR0;
        };

        Output main(float3 position : POSITION)
        {
            Output output;
            output.Position = float4(position.xy + Shift.xy, position.z, 1.0);
            output.Color = Tint;
            return output;
        }
        """;

    private const string SharedTintPixelSource = """
        cbuffer PixelValues : register(b0)
        {
            float4 Scale;
            float4 Tint;
        };

        float4 main(float4 position : SV_Position, float4 color : COLOR0) : SV_Target
        {
            return color * Tint * Scale;
        }
        """;

    private static readonly ShaderVariableDescription Float4 = new("Value", 0, 16, ShaderVariableClass.Vector, 1, 4);

    [TestMethod]
    [DataRow(false)]
    [DataRow(true)]
    public void DxilProgramFromDescriptionDraws(bool gpuBasedValidation) => TestGame.RunOnce(device =>
    {
        D3D12ShaderLibraryFile compiled;
        using (var stream = File.OpenRead(Path.Combine(AppContext.BaseDirectory, "Shaders", "Basic.crystalshader"))) compiled = D3D12ShaderLibraryFile.Read(stream);
        var solidColor = compiled.Programs.Single(program => program.Name == "SolidColor");

        using var library = new ShaderLibrary(device, [new ShaderProgramDescription
        {
            Name = "RuntimeSolidColor",
            VertexShader = compiled.Shaders[solidColor.VertexShader],
            PixelShader = compiled.Shaders[solidColor.PixelShader],
            ConstantBuffers = [new ShaderConstantBufferDescription("Material", 0, 16, ShaderStages.Pixel, [Float4 with { Name = "Tint" }])],
        }], "Runtime");
        var program = library.GetProgram("RuntimeSolidColor");
        using var pipeline = new GraphicsPipeline(device, new GraphicsPipelineDescription { Program = program });
        using var target = CreateTarget(device);

        device.SetRenderTarget(target);
        device.SetPipeline(pipeline);
        program.Parameters["Tint"]!.SetValue(Color.Blue);
        device.Draw(3);
        AssertAllPixels(Color.Blue, ReadPixels(target));
    }, gpuBasedValidation);

    [TestMethod]
    [DataRow(false)]
    [DataRow(true)]
    public void DxbcProgramSharesValueAndRegisterAcrossStages(bool gpuBasedValidation) => TestGame.RunOnce(device =>
    {
        using var library = new ShaderLibrary(device, [SharedTintDescription()], "Runtime");
        var program = library.GetProgram("SharedTint");
        Assert.AreEqual(3, program.Parameters.Count);
        using var pipeline = new GraphicsPipeline(device, new GraphicsPipelineDescription
        {
            Program = program,
            VertexLayout = VertexPositionColor.Layout,
            RasterizerState = RasterizerState.CullNone,
        });
        using var vertices = CreateVertexBuffer<VertexPositionColor>(device, FullQuad(0f, Color.White));
        using var target = CreateTarget(device);

        device.SetRenderTarget(target);
        device.Clear(Color.Black);
        device.SetPipeline(pipeline);
        device.SetVertexBuffer(vertices);
        program.Parameters["Shift"]!.SetValue(new Vector4(1f, 0f, 0f, 0f));
        program.Parameters["Scale"]!.SetValue(Vector4.One);
        program.Parameters["Tint"]!.SetValue(new Vector4(1f, 1f, 0f, 1f));
        device.Draw(6);

        var pixels = ReadPixels(target);
        AssertPixel(Color.Black, pixels, 8, 32);
        AssertPixel(new Color(255, 255, 0), pixels, 56, 32);
    }, gpuBasedValidation);

    [TestMethod]
    public void InvalidDescriptionsAreRejected() => TestGame.RunOnce(device =>
    {
        var valid = SharedTintDescription();
        var vertexBuffer = valid.ConstantBuffers[0];
        var pixelBuffer = valid.ConstantBuffers[1];

        AssertRejected(device, valid, valid);
        AssertRejected(device, valid with { VertexShader = new byte[] { 1, 2, 3, 4 } });
        AssertRejected(device, valid with { ComputeShader = valid.PixelShader, ThreadGroupSize = new ThreadGroupSize(1, 1, 1) });
        AssertRejected(device, valid with { VertexShader = default });
        AssertRejected(device, valid with { ConstantBuffers = [vertexBuffer, vertexBuffer with { Name = "Again" }] });
        AssertRejected(device, valid with { ConstantBuffers = [vertexBuffer, pixelBuffer with { Name = vertexBuffer.Name }] });
        AssertRejected(device, valid with { ConstantBuffers = [vertexBuffer with { Size = 16 }, pixelBuffer] });
        AssertRejected(device, valid with { ConstantBuffers = [vertexBuffer with { Stages = ShaderStages.Compute }, pixelBuffer] });
        AssertRejected(device, valid with
        {
            ConstantBuffers = [vertexBuffer, pixelBuffer with { Variables = [Float4 with { Name = "Scale" }, new ShaderVariableDescription("Tint", 16, 4, ShaderVariableClass.Scalar, 1, 1)] }],
        });
        AssertRejected(device, valid with { Resources = [new ShaderResourceDescription("Tint", ShaderResourceKind.Texture, ShaderResourceDimension.Texture2D, 0, ShaderStages.Pixel)] });
        AssertRejected(device, valid with { Resources = [new ShaderResourceDescription("Sampler", ShaderResourceKind.Sampler, ShaderResourceDimension.Texture2D, 0, ShaderStages.Pixel)] });
        AssertRejected(device, valid with
        {
            Resources =
            [
                new ShaderResourceDescription("First", ShaderResourceKind.Texture, ShaderResourceDimension.Texture2D, 0, ShaderStages.Pixel),
                new ShaderResourceDescription("Second", ShaderResourceKind.Texture, ShaderResourceDimension.Texture2D, 0, ShaderStages.Vertex | ShaderStages.Pixel),
            ],
        });
        AssertRejected(device, valid with
        {
            Resources = [.. Enumerable.Range(0, 61).Select(index => new ShaderResourceDescription($"Texture{index}", ShaderResourceKind.Texture, ShaderResourceDimension.Texture2D, index, ShaderStages.Pixel))],
        });
    });

    private static void AssertRejected(GraphicsDevice device, params ShaderProgramDescription[] descriptions) =>
        Assert.ThrowsExactly<ArgumentException>(() => new ShaderLibrary(device, descriptions).Dispose());

    private static ShaderProgramDescription SharedTintDescription() => new()
    {
        Name = "SharedTint",
        VertexShader = CompileDxbc(SharedTintVertexSource, "vs_5_0"),
        PixelShader = CompileDxbc(SharedTintPixelSource, "ps_5_0"),
        ConstantBuffers =
        [
            new ShaderConstantBufferDescription("VertexValues", 0, 32, ShaderStages.Vertex, [Float4 with { Name = "Shift" }, Float4 with { Name = "Tint", Offset = 16 }]),
            new ShaderConstantBufferDescription("PixelValues", 0, 32, ShaderStages.Pixel, [Float4 with { Name = "Scale" }, Float4 with { Name = "Tint", Offset = 16 }]),
        ],
        VertexInputs = [new ShaderVertexInputDescription("POSITION", 0, 0)],
    };

    private static unsafe byte[] CompileDxbc(string source, string profile)
    {
        var sourceBytes = Encoding.UTF8.GetBytes(source);
        var entryPoint = "main\0"u8;
        var target = Encoding.ASCII.GetBytes(profile + "\0");
        ID3DBlob* code = null;
        ID3DBlob* errors = null;
        try
        {
            fixed (byte* sourcePointer = sourceBytes)
            fixed (byte* entryPointer = entryPoint)
            fixed (byte* targetPointer = target)
            {
                var result = DirectX.D3DCompile(sourcePointer, (nuint)sourceBytes.Length, null, null, null, (sbyte*)entryPointer, (sbyte*)targetPointer, 0, 0, &code, &errors);
                if (result.FAILED)
                {
                    var message = errors == null ? "" : Encoding.UTF8.GetString((byte*)errors->GetBufferPointer(), (int)errors->GetBufferSize());
                    Assert.Fail($"D3DCompile {profile} failed: {message}");
                }
            }
            return new ReadOnlySpan<byte>(code->GetBufferPointer(), (int)code->GetBufferSize()).ToArray();
        }
        finally
        {
            if (code != null) code->Release();
            if (errors != null) errors->Release();
        }
    }
}
