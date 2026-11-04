using System.Numerics;
using Crystal.Graphics;

namespace Crystal.Tests;

[TestClass]
public sealed class ResourceTests
{
    [TestMethod]
    [DataRow(false)]
    [DataRow(true)]
    public void BufferRoundTripsBeforeAndAfterGpuUse(bool gpuBasedValidation) => TestGame.RunOnce(device =>
    {
        var data = Enumerable.Range(0, 1000).Select(value => value * 7).ToArray();
        using var buffer = new GraphicsBuffer(device, new BufferDescription
        {
            Size = data.Length * sizeof(int),
            Stride = sizeof(int),
            Usage = BufferUsage.Structured | BufferUsage.UnorderedAccess,
            Name = "RoundTripBuffer",
        });

        buffer.SetData<int>(data);
        var readBack = new int[data.Length];
        buffer.GetData<int>(readBack);
        CollectionAssert.AreEqual(data, readBack);

        buffer.SetData<int>([-1, -2, -3], offsetInBytes: 40);
        data[10] = -1;
        data[11] = -2;
        data[12] = -3;
        buffer.GetData<int>(readBack);
        CollectionAssert.AreEqual(data, readBack);
    }, gpuBasedValidation);

    [TestMethod]
    [DataRow(false)]
    [DataRow(true)]
    public void TextureMipLevelsRoundTrip(bool gpuBasedValidation) => TestGame.RunOnce(device =>
    {
        using var texture = new Texture2D(device, new TextureDescription { Width = 37, Height = 19, MipLevels = 0, Format = SurfaceFormat.Rgba8Unorm });
        Assert.AreEqual(6, texture.MipLevels);

        var random = new Random(1);
        var mips = new byte[texture.MipLevels][];
        for (var mip = 0; mip < texture.MipLevels; mip++)
        {
            mips[mip] = new byte[texture.GetMipLevelSizeInBytes(mip)];
            random.NextBytes(mips[mip]);
            texture.SetData<byte>(mips[mip], mip);
        }

        for (var mip = 0; mip < texture.MipLevels; mip++)
        {
            var readBack = new byte[mips[mip].Length];
            texture.GetData<byte>(readBack, mip);
            CollectionAssert.AreEqual(mips[mip], readBack, $"mip {mip}");
        }
    }, gpuBasedValidation);

    [TestMethod]
    [DataRow(false)]
    [DataRow(true)]
    public void TextureUpdatesAfterGpuUseRoundTripAcrossFrames(bool gpuBasedValidation)
    {
        Texture2D? texture = null;
        var expected = new uint[37 * 19];
        TestGame.RunFrames((device, frame) =>
        {
            texture ??= new Texture2D(device, new TextureDescription { Width = 37, Height = 19, Format = SurfaceFormat.Rgba8Unorm });
            for (var update = 0; update < 3; update++)
            {
                Array.Fill(expected, (uint)(frame * 10 + update));
                texture.SetData<uint>(expected);
                var region = new uint[5 * 3];
                Array.Fill(region, 0xFFFF0000u + (uint)update);
                texture.SetData<uint>(region, new Rectangle(7, 4, 5, 3));
                for (var row = 4; row < 7; row++) Array.Fill(expected, 0xFFFF0000u + (uint)update, row * 37 + 7, 5);

                var readBack = new uint[expected.Length];
                texture.GetData<uint>(readBack);
                CollectionAssert.AreEqual(expected, readBack, $"frame {frame} update {update}");
            }
        }, 4, gpuBasedValidation);
        texture?.Dispose();
    }

    [TestMethod]
    [DataRow(SurfaceFormat.Bc1Unorm)]
    [DataRow(SurfaceFormat.Bc3UnormSrgb)]
    [DataRow(SurfaceFormat.Bc4Unorm)]
    [DataRow(SurfaceFormat.Bc5Snorm)]
    [DataRow(SurfaceFormat.Bc6hUfloat)]
    [DataRow(SurfaceFormat.Bc7Unorm)]
    public void BlockCompressedTexturesRoundTrip(SurfaceFormat format) => TestGame.RunOnce(device =>
    {
        using var texture = new Texture2D(device, new TextureDescription { Width = 64, Height = 32, MipLevels = 3, Format = format });
        var random = new Random((int)format);
        for (var mip = 0; mip < texture.MipLevels; mip++)
        {
            var data = new byte[texture.GetMipLevelSizeInBytes(mip)];
            random.NextBytes(data);
            texture.SetData<byte>(data, mip);
            var readBack = new byte[data.Length];
            texture.GetData<byte>(readBack, mip);
            CollectionAssert.AreEqual(data, readBack, $"mip {mip}");
        }
    });

    [TestMethod]
    public void CubeFacesRoundTrip() => TestGame.RunOnce(device =>
    {
        using var cube = new TextureCube(device, new TextureDescription { Width = 16, Height = 16, Format = SurfaceFormat.R32Float });
        foreach (var face in Enum.GetValues<CubeFace>()) cube.SetData<float>(face, Enumerable.Repeat((float)face + 0.5f, 16 * 16).ToArray());

        var readBack = new float[16 * 16];
        foreach (var face in Enum.GetValues<CubeFace>())
        {
            cube.GetData<float>(face, readBack);
            Assert.IsTrue(readBack.All(value => value == (float)face + 0.5f), face.ToString());
        }
    });

    [TestMethod]
    [DataRow(false)]
    [DataRow(true)]
    public void RenderTargetClearReadsBack(bool gpuBasedValidation) => TestGame.RunOnce(device =>
    {
        using var color = new Texture2D(device, new TextureDescription
        {
            Width = 64,
            Height = 48,
            Usage = TextureUsage.RenderTarget | TextureUsage.ShaderResource,
            ClearColor = Color.CornflowerBlue.ToVector4(),
        });
        using var hdr = new Texture2D(device, new TextureDescription
        {
            Width = 64,
            Height = 48,
            Format = SurfaceFormat.Rgba16Float,
            Usage = TextureUsage.RenderTarget,
            ClearColor = new Vector4(0.5f, 1f, 2f, 0.25f),
        });

        device.SetRenderTarget(color);
        device.Clear(Color.CornflowerBlue);
        var pixels = new uint[64 * 48];
        color.GetData<uint>(pixels);
        Assert.IsTrue(pixels.All(pixel => pixel == Color.CornflowerBlue.PackedValue));

        device.SetRenderTarget(hdr);
        device.Clear(new Vector4(0.5f, 1f, 2f, 0.25f));
        var halves = new Half[64 * 48 * 4];
        hdr.GetData<Half>(halves);
        for (var index = 0; index < halves.Length; index += 4)
        {
            Assert.AreEqual((Half)0.5f, halves[index]);
            Assert.AreEqual((Half)2f, halves[index + 2]);
            Assert.AreEqual((Half)0.25f, halves[index + 3]);
        }

        device.SetRenderTarget(null);
        device.Clear(Color.Black);
    }, gpuBasedValidation);

    [TestMethod]
    [DataRow(false)]
    [DataRow(true)]
    public void MultisampledClearResolvesAndReadsBack(bool gpuBasedValidation) => TestGame.RunOnce(device =>
    {
        using var multisampled = new Texture2D(device, new TextureDescription
        {
            Width = 32,
            Height = 32,
            SampleCount = 4,
            Usage = TextureUsage.RenderTarget | TextureUsage.ShaderResource,
            ClearColor = Color.Red.ToVector4(),
        });
        using var resolved = new Texture2D(device, new TextureDescription { Width = 32, Height = 32 });

        device.SetRenderTarget(multisampled);
        device.Clear(Color.Red);
        device.Resolve(multisampled, resolved);

        var pixels = new uint[32 * 32];
        resolved.GetData<uint>(pixels);
        Assert.IsTrue(pixels.All(pixel => pixel == Color.Red.PackedValue));
    }, gpuBasedValidation);

    [TestMethod]
    [DataRow(false)]
    [DataRow(true)]
    public void DepthClearReadsBack(bool gpuBasedValidation) => TestGame.RunOnce(device =>
    {
        using var color = new Texture2D(device, new TextureDescription { Width = 40, Height = 30, Usage = TextureUsage.RenderTarget, ClearColor = Vector4.One });
        using var depth = new Texture2D(device, new TextureDescription
        {
            Width = 40,
            Height = 30,
            Format = SurfaceFormat.Depth32Float,
            Usage = TextureUsage.DepthStencil | TextureUsage.ShaderResource,
            ClearDepth = 0.25f,
        });
        using var depthStencil = new Texture2D(device, new TextureDescription
        {
            Width = 40,
            Height = 30,
            Format = SurfaceFormat.Depth24UnormStencil8,
            Usage = TextureUsage.DepthStencil,
            ClearStencil = 7,
        });

        device.SetRenderTarget(color, depth);
        device.Clear(Color.White);
        device.ClearDepthStencil(0.25f);
        var depths = new float[40 * 30];
        depth.GetData<float>(depths);
        Assert.IsTrue(depths.All(value => value == 0.25f));

        device.SetRenderTargets([], depthStencil);
        device.ClearDepthStencil(1f, 7);
    }, gpuBasedValidation);

    [TestMethod]
    public void CopiesBetweenBuffersAndTextures() => TestGame.RunOnce(device =>
    {
        using var source = new GraphicsBuffer(device, new BufferDescription { Size = 256, Usage = BufferUsage.Vertex, Stride = 16 });
        using var destination = new GraphicsBuffer(device, new BufferDescription { Size = 256, Usage = BufferUsage.Index, Stride = 4 });
        var bytes = Enumerable.Range(0, 256).Select(value => (byte)value).ToArray();
        source.SetData<byte>(bytes);
        destination.SetData<byte>(new byte[256]);
        device.CopyBuffer(source, 16, destination, 64, 32);

        var readBack = new byte[256];
        destination.GetData<byte>(readBack);
        CollectionAssert.AreEqual(bytes[16..48], readBack[64..96]);
        Assert.IsTrue(readBack[..64].All(value => value == 0));

        using var first = new Texture2D(device, new TextureDescription { Width = 8, Height = 8, Format = SurfaceFormat.R8Unorm });
        using var second = new Texture2D(device, new TextureDescription { Width = 8, Height = 8, Format = SurfaceFormat.R8Unorm });
        first.SetData<byte>(bytes[..64]);
        device.CopyTexture(first, second);
        var pixels = new byte[64];
        second.GetData<byte>(pixels);
        CollectionAssert.AreEqual(bytes[..64], pixels);
    });

    [TestMethod]
    public void ResourcesCreatedOnWorkerThreadsUploadOnTheCopyQueue() => TestGame.RunOnce(device =>
    {
        const int Count = 16;
        var textures = new Texture2D[Count];
        var buffers = new GraphicsBuffer[Count];
        Parallel.For(0, Count, index =>
        {
            textures[index] = new Texture2D(device, new TextureDescription { Width = 16, Height = 16, Format = SurfaceFormat.R32Uint });
            textures[index].SetData<uint>(Enumerable.Repeat((uint)index, 16 * 16).ToArray());
            buffers[index] = new GraphicsBuffer(device, new BufferDescription { Size = 64, Usage = BufferUsage.Constant });
            buffers[index].SetData<uint>(Enumerable.Repeat((uint)index * 3, 16).ToArray());
        });

        var pixels = new uint[16 * 16];
        var constants = new uint[16];
        for (var index = 0; index < Count; index++)
        {
            textures[index].GetData<uint>(pixels);
            Assert.IsTrue(pixels.All(value => value == index));
            buffers[index].GetData<uint>(constants);
            Assert.IsTrue(constants.All(value => value == index * 3));
            textures[index].Dispose();
            buffers[index].Dispose();
        }
    });

    [TestMethod]
    public void UpdatingAResourceTheGpuUsedFromAWorkerThreadThrows() => TestGame.RunOnce(device =>
    {
        using var buffer = new GraphicsBuffer(device, new BufferDescription { Size = 16, Usage = BufferUsage.Structured, Stride = 4 });
        buffer.SetData<int>([1, 2, 3, 4]);
        Task.Run(() => buffer.SetData<int>([5])).Wait();

        var readBack = new int[4];
        buffer.GetData<int>(readBack);
        Assert.AreEqual(5, readBack[0]);

        var exception = Assert.Throws<AggregateException>(() => Task.Run(() => buffer.SetData<int>([6])).Wait());
        Assert.IsInstanceOfType<InvalidOperationException>(exception.InnerException);
    });

    [TestMethod]
    public void DisposedResourcesAreReleasedAfterTheGpuFinishes()
    {
        var descriptorsBefore = 0;
        TestGame.RunFrames((device, frame) =>
        {
            if (frame == 0)
            {
                descriptorsBefore = device.ShaderResourceDescriptorCount;
                var used = new GraphicsBuffer(device, new BufferDescription { Size = 64, Usage = BufferUsage.Structured, Stride = 16 });
                var target = new Texture2D(device, new TextureDescription
                {
                    Width = 8,
                    Height = 8,
                    Usage = TextureUsage.RenderTarget | TextureUsage.ShaderResource,
                    ClearColor = Color.Green.ToVector4(),
                });
                device.SetRenderTarget(target);
                device.Clear(Color.Green);
                used.SetData<byte>(new byte[64]);
                using var readback = used.BeginGetData(0, 64);
                used.Dispose();
                target.Dispose();
                device.SetRenderTarget(null);
                Assert.IsGreaterThanOrEqualTo(4, device.PendingReleaseCount);
                Assert.IsFalse(readback.IsComplete);
                readback.Wait();
                Assert.IsTrue(readback.IsComplete);
            }
            if (frame == 5)
            {
                Assert.AreEqual(0, device.PendingReleaseCount);
                Assert.AreEqual(descriptorsBefore, device.ShaderResourceDescriptorCount);
            }
        }, frameCount: 6);
    }

    [TestMethod]
    public void SamplerStatesAreSharedByDescription() => TestGame.RunOnce(device =>
    {
        var linearWrap = device.GetSamplerState(SamplerDescription.LinearWrap);
        Assert.AreSame(linearWrap, device.GetSamplerState(new SamplerDescription()));
        var pointClamp = device.GetSamplerState(SamplerDescription.PointClamp);
        Assert.AreNotEqual(linearWrap.DescriptorIndex, pointClamp.DescriptorIndex);
        device.GetSamplerState(SamplerDescription.AnisotropicClamp);
        device.GetSamplerState(SamplerDescription.ShadowComparison);
    });

    [TestMethod]
    public void InvalidDescriptionsThrow() => TestGame.RunOnce(device =>
    {
        Assert.Throws<ArgumentException>(() => new GraphicsBuffer(device, new BufferDescription { Size = 12, Usage = BufferUsage.Index, Stride = 3 }));
        Assert.Throws<ArgumentException>(() => new GraphicsBuffer(device, new BufferDescription { Size = 100_000, Usage = BufferUsage.Constant }));
        Assert.Throws<ArgumentException>(() => new Texture2D(device, new TextureDescription { Width = 6, Height = 8, Format = SurfaceFormat.Bc1Unorm }));
        Assert.Throws<ArgumentException>(() => new Texture2D(device, new TextureDescription { Width = 8, Height = 8, Format = SurfaceFormat.Bc1Unorm, Usage = TextureUsage.RenderTarget }));
        Assert.Throws<ArgumentException>(() => new Texture2D(device, new TextureDescription { Width = 8, Height = 8, Format = SurfaceFormat.Depth32Float, Usage = TextureUsage.RenderTarget }));
        Assert.Throws<ArgumentException>(() => new Texture2D(device, new TextureDescription { Width = 8, Height = 8, SampleCount = 4 }));
        Assert.Throws<ArgumentException>(() => new TextureCube(device, new TextureDescription { Width = 8, Height = 4 }));

        using var texture = new Texture2D(device, new TextureDescription { Width = 4, Height = 4 });
        Assert.Throws<ArgumentException>(() => texture.SetData<byte>(new byte[10]));
        Assert.Throws<ArgumentException>(() => device.SetRenderTarget(texture));
    });
}
