using System.Numerics;
using System.Runtime.InteropServices;
using Crystal.Graphics.D3D12;
using TerraFX.Interop.DirectX;
using static TerraFX.Interop.DirectX.D3D12_BARRIER_LAYOUT;
using static TerraFX.Interop.DirectX.D3D12_DSV_DIMENSION;
using static TerraFX.Interop.DirectX.D3D12_DSV_FLAGS;
using static TerraFX.Interop.DirectX.D3D12_HEAP_TYPE;
using static TerraFX.Interop.DirectX.D3D12_RESOURCE_DIMENSION;
using static TerraFX.Interop.DirectX.D3D12_RESOURCE_FLAGS;
using static TerraFX.Interop.DirectX.D3D12_RTV_DIMENSION;
using static TerraFX.Interop.DirectX.D3D12_SRV_DIMENSION;
using static TerraFX.Interop.DirectX.D3D12_TEXTURE_LAYOUT;
using static TerraFX.Interop.DirectX.D3D12_UAV_DIMENSION;

namespace Crystal.Graphics;

public abstract unsafe class Texture : GraphicsResource
{
    private const uint NoDescriptor = uint.MaxValue;

    private readonly D3D12_RESOURCE_DESC1 _resourceDescription;
    private readonly D3D12CpuDescriptor _renderTargetView;
    private readonly D3D12CpuDescriptor _depthStencilView;
    private readonly bool _hasRenderTargetView;
    private readonly bool _hasDepthStencilView;
    private readonly bool _isCube;
    private readonly Lock _mipViewLock = new();
    private TextureView?[]? _mipViews;

    private protected Texture(GraphicsDevice graphicsDevice, in TextureDescription description, int arrayLayers, bool isCube)
        : base(graphicsDevice, description.Name)
    {
        FormatInfo = D3D12Formats.Get(description.Format);
        var mipLevels = description.MipLevels == 0 ? FullMipChainLength(description.Width, description.Height) : description.MipLevels;
        Validate(description, FormatInfo, mipLevels, isCube);
        var device = graphicsDevice.Native;
        if (!device.SupportsSampleCount(FormatInfo.DepthStencilFormat != 0 ? FormatInfo.DepthStencilFormat : FormatInfo.ResourceFormat, description.SampleCount))
            throw new NotSupportedException($"{description.Format} doesn't support {description.SampleCount} samples on this adapter.");

        Description = description with { MipLevels = mipLevels };
        ArrayLayers = arrayLayers;
        _isCube = isCube;

        var usage = description.Usage;
        var flags = D3D12_RESOURCE_FLAG_NONE;
        if ((usage & TextureUsage.RenderTarget) != 0) flags |= D3D12_RESOURCE_FLAG_ALLOW_RENDER_TARGET;
        if ((usage & TextureUsage.DepthStencil) != 0) flags |= D3D12_RESOURCE_FLAG_ALLOW_DEPTH_STENCIL;
        if ((usage & TextureUsage.UnorderedAccess) != 0) flags |= D3D12_RESOURCE_FLAG_ALLOW_UNORDERED_ACCESS;
        if ((usage & TextureUsage.DepthStencil) != 0 && (usage & TextureUsage.ShaderResource) == 0) flags |= D3D12_RESOURCE_FLAG_DENY_SHADER_RESOURCE;

        _resourceDescription = new D3D12_RESOURCE_DESC1
        {
            Dimension = D3D12_RESOURCE_DIMENSION_TEXTURE2D,
            Width = (ulong)description.Width,
            Height = (uint)description.Height,
            DepthOrArraySize = (ushort)arrayLayers,
            MipLevels = (ushort)mipLevels,
            Format = FormatInfo.ResourceFormat,
            SampleDesc = new DXGI_SAMPLE_DESC((uint)description.SampleCount, 0),
            Layout = D3D12_TEXTURE_LAYOUT_UNKNOWN,
            Flags = flags,
        };

        D3D12_CLEAR_VALUE clearValue = default;
        var hasClearValue = (usage & (TextureUsage.RenderTarget | TextureUsage.DepthStencil)) != 0;
        if ((usage & TextureUsage.DepthStencil) != 0)
        {
            clearValue.Format = FormatInfo.DepthStencilFormat;
            clearValue.DepthStencil = new D3D12_DEPTH_STENCIL_VALUE { Depth = description.ClearDepth, Stencil = description.ClearStencil };
        }
        else if (hasClearValue)
        {
            clearValue.Format = FormatInfo.ViewFormat;
            MemoryMarshal.Write(MemoryMarshal.AsBytes(clearValue.Color), description.ClearColor);
        }

        var pointer = device.CreateCommittedResource(
            D3D12_HEAP_TYPE_DEFAULT, _resourceDescription, D3D12_BARRIER_LAYOUT_COMMON, hasClearValue ? &clearValue : null, Name ?? "Texture");
        Resource = new D3D12Resource(pointer, isTexture: true, D3D12_BARRIER_LAYOUT_COMMON, Name ?? "Texture");

        var isMultisampled = description.SampleCount > 1;
        var isArray = arrayLayers > 1;
        if ((usage & TextureUsage.ShaderResource) != 0)
        {
            ShaderResourceViewIndex = device.ShaderResourceHeap.Allocate();
            var view = new D3D12_SHADER_RESOURCE_VIEW_DESC { Format = FormatInfo.ViewFormat, Shader4ComponentMapping = D3D12Formats.DefaultComponentMapping };
            if (isCube && arrayLayers > 6)
            {
                view.ViewDimension = D3D12_SRV_DIMENSION_TEXTURECUBEARRAY;
                view.TextureCubeArray = new D3D12_TEXCUBE_ARRAY_SRV { MipLevels = (uint)mipLevels, NumCubes = (uint)(arrayLayers / 6) };
            }
            else if (isCube)
            {
                view.ViewDimension = D3D12_SRV_DIMENSION_TEXTURECUBE;
                view.TextureCube = new D3D12_TEXCUBE_SRV { MipLevels = (uint)mipLevels };
            }
            else if (isMultisampled && isArray)
            {
                view.ViewDimension = D3D12_SRV_DIMENSION_TEXTURE2DMSARRAY;
                view.Texture2DMSArray = new D3D12_TEX2DMS_ARRAY_SRV { ArraySize = (uint)arrayLayers };
            }
            else if (isMultisampled)
            {
                view.ViewDimension = D3D12_SRV_DIMENSION_TEXTURE2DMS;
            }
            else if (isArray)
            {
                view.ViewDimension = D3D12_SRV_DIMENSION_TEXTURE2DARRAY;
                view.Texture2DArray = new D3D12_TEX2D_ARRAY_SRV { MipLevels = (uint)mipLevels, ArraySize = (uint)arrayLayers };
            }
            else
            {
                view.ViewDimension = D3D12_SRV_DIMENSION_TEXTURE2D;
                view.Texture2D = new D3D12_TEX2D_SRV { MipLevels = (uint)mipLevels };
            }
            device.Device->CreateShaderResourceView(pointer, &view, device.ShaderResourceHeap.CpuHandle(ShaderResourceViewIndex));
            ShaderResourceDimension = (int)view.ViewDimension;
        }

        if ((usage & TextureUsage.UnorderedAccess) != 0)
        {
            UnorderedAccessViewIndex = device.ShaderResourceHeap.Allocate();
            var view = new D3D12_UNORDERED_ACCESS_VIEW_DESC { Format = FormatInfo.ViewFormat };
            if (isArray)
            {
                view.ViewDimension = D3D12_UAV_DIMENSION_TEXTURE2DARRAY;
                view.Texture2DArray = new D3D12_TEX2D_ARRAY_UAV { ArraySize = (uint)arrayLayers };
            }
            else
            {
                view.ViewDimension = D3D12_UAV_DIMENSION_TEXTURE2D;
            }
            device.Device->CreateUnorderedAccessView(pointer, null, &view, device.ShaderResourceHeap.CpuHandle(UnorderedAccessViewIndex));
            UnorderedAccessDimension = (int)view.ViewDimension;
        }

        if ((usage & TextureUsage.RenderTarget) != 0)
        {
            _renderTargetView = device.RenderTargetViewHeap.Allocate();
            _hasRenderTargetView = true;
            var view = new D3D12_RENDER_TARGET_VIEW_DESC { Format = FormatInfo.ViewFormat };
            if (isMultisampled && isArray)
            {
                view.ViewDimension = D3D12_RTV_DIMENSION_TEXTURE2DMSARRAY;
                view.Texture2DMSArray = new D3D12_TEX2DMS_ARRAY_RTV { ArraySize = (uint)arrayLayers };
            }
            else if (isMultisampled)
            {
                view.ViewDimension = D3D12_RTV_DIMENSION_TEXTURE2DMS;
            }
            else if (isArray)
            {
                view.ViewDimension = D3D12_RTV_DIMENSION_TEXTURE2DARRAY;
                view.Texture2DArray = new D3D12_TEX2D_ARRAY_RTV { ArraySize = (uint)arrayLayers };
            }
            else
            {
                view.ViewDimension = D3D12_RTV_DIMENSION_TEXTURE2D;
            }
            device.Device->CreateRenderTargetView(pointer, &view, _renderTargetView.Handle);
        }

        if ((usage & TextureUsage.DepthStencil) != 0)
        {
            _depthStencilView = device.DepthStencilViewHeap.Allocate();
            _hasDepthStencilView = true;
            var view = new D3D12_DEPTH_STENCIL_VIEW_DESC { Format = FormatInfo.DepthStencilFormat, Flags = D3D12_DSV_FLAG_NONE };
            if (isMultisampled && isArray)
            {
                view.ViewDimension = D3D12_DSV_DIMENSION_TEXTURE2DMSARRAY;
                view.Texture2DMSArray = new D3D12_TEX2DMS_ARRAY_DSV { ArraySize = (uint)arrayLayers };
            }
            else if (isMultisampled)
            {
                view.ViewDimension = D3D12_DSV_DIMENSION_TEXTURE2DMS;
            }
            else if (isArray)
            {
                view.ViewDimension = D3D12_DSV_DIMENSION_TEXTURE2DARRAY;
                view.Texture2DArray = new D3D12_TEX2D_ARRAY_DSV { ArraySize = (uint)arrayLayers };
            }
            else
            {
                view.ViewDimension = D3D12_DSV_DIMENSION_TEXTURE2D;
            }
            device.Device->CreateDepthStencilView(pointer, &view, _depthStencilView.Handle);
        }

        graphicsDevice.Track(this);
    }

    public TextureDescription Description { get; }
    public int Width => Description.Width;
    public int Height => Description.Height;
    public int MipLevels => Description.MipLevels;
    public int ArrayLayers { get; }
    public int SampleCount => Description.SampleCount;
    public SurfaceFormat Format => Description.Format;
    public TextureUsage Usage => Description.Usage;

    internal D3D12Resource Resource { get; }
    internal D3D12FormatInfo FormatInfo { get; }
    internal uint ShaderResourceViewIndex { get; } = NoDescriptor;
    internal uint UnorderedAccessViewIndex { get; } = NoDescriptor;
    internal int ShaderResourceDimension { get; }
    internal int UnorderedAccessDimension { get; }
    internal D3D12_CPU_DESCRIPTOR_HANDLE RenderTargetView => _renderTargetView.Handle;
    internal D3D12_CPU_DESCRIPTOR_HANDLE DepthStencilView => _depthStencilView.Handle;

    public int GetMipLevelSizeInBytes(int mipLevel)
    {
        ArgumentOutOfRangeException.ThrowIfNegative(mipLevel);
        ArgumentOutOfRangeException.ThrowIfGreaterThanOrEqual(mipLevel, MipLevels);
        var blockSize = FormatInfo.BlockSize;
        var blocksWide = (Math.Max(Width >> mipLevel, 1) + blockSize - 1) / blockSize;
        var blocksHigh = (Math.Max(Height >> mipLevel, 1) + blockSize - 1) / blockSize;
        return blocksWide * blocksHigh * FormatInfo.BytesPerBlock;
    }

    public TextureView GetMipView(int mipLevel)
    {
        ThrowIfDisposed();
        ArgumentOutOfRangeException.ThrowIfNegative(mipLevel);
        ArgumentOutOfRangeException.ThrowIfGreaterThanOrEqual(mipLevel, MipLevels);
        if ((Usage & (TextureUsage.ShaderResource | TextureUsage.UnorderedAccess)) == 0)
            throw new InvalidOperationException($"Texture '{Name}' was created without TextureUsage.ShaderResource or TextureUsage.UnorderedAccess, so it has no shader views.");
        lock (_mipViewLock)
        {
            _mipViews ??= new TextureView?[MipLevels];
            return _mipViews[mipLevel] ??= CreateMipView(mipLevel);
        }
    }

    private TextureView CreateMipView(int mipLevel)
    {
        var device = GraphicsDevice.Native;
        var pointer = Resource.Pointer;
        var isArray = ArrayLayers > 1;
        var shaderResourceViewIndex = NoDescriptor;
        var shaderResourceDimension = 0;
        if ((Usage & TextureUsage.ShaderResource) != 0)
        {
            shaderResourceViewIndex = device.ShaderResourceHeap.Allocate();
            var view = new D3D12_SHADER_RESOURCE_VIEW_DESC { Format = FormatInfo.ViewFormat, Shader4ComponentMapping = D3D12Formats.DefaultComponentMapping };
            if (_isCube && ArrayLayers > 6)
            {
                view.ViewDimension = D3D12_SRV_DIMENSION_TEXTURECUBEARRAY;
                view.TextureCubeArray = new D3D12_TEXCUBE_ARRAY_SRV { MostDetailedMip = (uint)mipLevel, MipLevels = 1, NumCubes = (uint)(ArrayLayers / 6) };
            }
            else if (_isCube)
            {
                view.ViewDimension = D3D12_SRV_DIMENSION_TEXTURECUBE;
                view.TextureCube = new D3D12_TEXCUBE_SRV { MostDetailedMip = (uint)mipLevel, MipLevels = 1 };
            }
            else if (SampleCount > 1 && isArray)
            {
                view.ViewDimension = D3D12_SRV_DIMENSION_TEXTURE2DMSARRAY;
                view.Texture2DMSArray = new D3D12_TEX2DMS_ARRAY_SRV { ArraySize = (uint)ArrayLayers };
            }
            else if (SampleCount > 1)
            {
                view.ViewDimension = D3D12_SRV_DIMENSION_TEXTURE2DMS;
            }
            else if (isArray)
            {
                view.ViewDimension = D3D12_SRV_DIMENSION_TEXTURE2DARRAY;
                view.Texture2DArray = new D3D12_TEX2D_ARRAY_SRV { MostDetailedMip = (uint)mipLevel, MipLevels = 1, ArraySize = (uint)ArrayLayers };
            }
            else
            {
                view.ViewDimension = D3D12_SRV_DIMENSION_TEXTURE2D;
                view.Texture2D = new D3D12_TEX2D_SRV { MostDetailedMip = (uint)mipLevel, MipLevels = 1 };
            }
            device.Device->CreateShaderResourceView(pointer, &view, device.ShaderResourceHeap.CpuHandle(shaderResourceViewIndex));
            shaderResourceDimension = (int)view.ViewDimension;
        }

        var unorderedAccessViewIndex = NoDescriptor;
        var unorderedAccessDimension = 0;
        if ((Usage & TextureUsage.UnorderedAccess) != 0)
        {
            unorderedAccessViewIndex = device.ShaderResourceHeap.Allocate();
            var view = new D3D12_UNORDERED_ACCESS_VIEW_DESC { Format = FormatInfo.ViewFormat };
            if (isArray)
            {
                view.ViewDimension = D3D12_UAV_DIMENSION_TEXTURE2DARRAY;
                view.Texture2DArray = new D3D12_TEX2D_ARRAY_UAV { MipSlice = (uint)mipLevel, ArraySize = (uint)ArrayLayers };
            }
            else
            {
                view.ViewDimension = D3D12_UAV_DIMENSION_TEXTURE2D;
                view.Texture2D = new D3D12_TEX2D_UAV { MipSlice = (uint)mipLevel };
            }
            device.Device->CreateUnorderedAccessView(pointer, null, &view, device.ShaderResourceHeap.CpuHandle(unorderedAccessViewIndex));
            unorderedAccessDimension = (int)view.ViewDimension;
        }

        return new TextureView(this, mipLevel, shaderResourceViewIndex, shaderResourceDimension, unorderedAccessViewIndex, unorderedAccessDimension);
    }

    private protected void SetDataCore<T>(ReadOnlySpan<T> data, int mipLevel, int arrayLayer) where T : unmanaged
    {
        ThrowIfDisposed();
        if (SampleCount > 1) throw new InvalidOperationException("Multisampled textures can't be written from the CPU.");
        if (FormatInfo.HasStencil) throw new InvalidOperationException("Depth-stencil textures can't be written from the CPU.");
        GraphicsDevice.UploadToTexture(Resource, _resourceDescription, Subresource(mipLevel, arrayLayer), MemoryMarshal.AsBytes(data));
    }

    private protected void SetDataCore<T>(ReadOnlySpan<T> data, Rectangle region, int mipLevel, int arrayLayer) where T : unmanaged
    {
        ThrowIfDisposed();
        if (SampleCount > 1) throw new InvalidOperationException("Multisampled textures can't be written from the CPU.");
        if (FormatInfo.HasStencil) throw new InvalidOperationException("Depth-stencil textures can't be written from the CPU.");
        var subresource = Subresource(mipLevel, arrayLayer);
        var mipWidth = Math.Max(Width >> mipLevel, 1);
        var mipHeight = Math.Max(Height >> mipLevel, 1);
        if (region.X < 0 || region.Y < 0 || region.Width <= 0 || region.Height <= 0 || region.Right > mipWidth || region.Bottom > mipHeight)
            throw new ArgumentOutOfRangeException(nameof(region), $"The region {region} is not inside mip level {mipLevel} ({mipWidth}x{mipHeight}).");
        var blockSize = FormatInfo.BlockSize;
        if (region.X % blockSize != 0 || region.Y % blockSize != 0 || region.Width % blockSize != 0 || region.Height % blockSize != 0)
            throw new ArgumentException($"Regions of {Format} textures must be aligned to {blockSize}x{blockSize} blocks.", nameof(region));

        var regionDescription = _resourceDescription;
        regionDescription.Width = (ulong)region.Width;
        regionDescription.Height = (uint)region.Height;
        regionDescription.MipLevels = 1;
        regionDescription.DepthOrArraySize = 1;
        GraphicsDevice.UploadToTexture(Resource, regionDescription, subresource, MemoryMarshal.AsBytes(data), region.X, region.Y);
    }

    private protected void GetDataCore<T>(Span<T> destination, int mipLevel, int arrayLayer) where T : unmanaged
    {
        using var readback = BeginGetDataCore(mipLevel, arrayLayer);
        readback.GetData(destination);
    }

    private protected GraphicsReadback BeginGetDataCore(int mipLevel, int arrayLayer)
    {
        ThrowIfDisposed();
        if (SampleCount > 1) throw new InvalidOperationException("Multisampled textures can't be read back. Resolve them first.");
        if (FormatInfo.HasStencil) throw new InvalidOperationException("Depth-stencil textures can't be read back.");
        return GraphicsDevice.BeginTextureReadback(Resource, _resourceDescription, Subresource(mipLevel, arrayLayer));
    }

    private protected override void ReleaseNativeResources()
    {
        if (_mipViews != null)
        {
            foreach (var view in _mipViews)
            {
                if (view == null) continue;
                if (view.ShaderResourceViewIndex != NoDescriptor) GraphicsDevice.FreeShaderResourceDescriptor(Resource, view.ShaderResourceViewIndex);
                if (view.UnorderedAccessViewIndex != NoDescriptor) GraphicsDevice.FreeShaderResourceDescriptor(Resource, view.UnorderedAccessViewIndex);
            }
        }
        if (ShaderResourceViewIndex != NoDescriptor) GraphicsDevice.FreeShaderResourceDescriptor(Resource, ShaderResourceViewIndex);
        if (UnorderedAccessViewIndex != NoDescriptor) GraphicsDevice.FreeShaderResourceDescriptor(Resource, UnorderedAccessViewIndex);
        if (_hasRenderTargetView) GraphicsDevice.FreeCpuDescriptor(GraphicsDevice.Native.RenderTargetViewHeap, _renderTargetView, Resource);
        if (_hasDepthStencilView) GraphicsDevice.FreeCpuDescriptor(GraphicsDevice.Native.DepthStencilViewHeap, _depthStencilView, Resource);
        GraphicsDevice.ReleaseResource(Resource);
    }

    private uint Subresource(int mipLevel, int arrayLayer)
    {
        ArgumentOutOfRangeException.ThrowIfNegative(mipLevel);
        ArgumentOutOfRangeException.ThrowIfGreaterThanOrEqual(mipLevel, MipLevels);
        ArgumentOutOfRangeException.ThrowIfNegative(arrayLayer);
        ArgumentOutOfRangeException.ThrowIfGreaterThanOrEqual(arrayLayer, ArrayLayers);
        return (uint)(mipLevel + arrayLayer * MipLevels);
    }

    private static int FullMipChainLength(int width, int height) => BitOperations.Log2((uint)Math.Max(Math.Max(width, height), 1)) + 1;

    private static void Validate(in TextureDescription description, in D3D12FormatInfo formatInfo, int mipLevels, bool isCube)
    {
        ArgumentOutOfRangeException.ThrowIfNegativeOrZero(description.Width, nameof(description));
        ArgumentOutOfRangeException.ThrowIfNegativeOrZero(description.Height, nameof(description));
        ArgumentOutOfRangeException.ThrowIfNegativeOrZero(description.ArraySize, nameof(description));
        if (mipLevels < 1 || mipLevels > FullMipChainLength(description.Width, description.Height))
            throw new ArgumentException($"{mipLevels} mip levels don't fit a {description.Width}x{description.Height} texture.", nameof(description));
        if (description.SampleCount is not (1 or 2 or 4 or 8 or 16))
            throw new ArgumentException("The sample count must be 1, 2, 4, 8 or 16.", nameof(description));

        var usage = description.Usage;
        if (usage == TextureUsage.None) throw new ArgumentException("A texture needs at least one usage.", nameof(description));
        if (isCube && description.Width != description.Height) throw new ArgumentException("Cube textures must be square.", nameof(description));
        if (formatInfo.IsBlockCompressed && (description.Width % 4 != 0 || description.Height % 4 != 0))
            throw new ArgumentException("Block-compressed textures need a width and height that are multiples of 4.", nameof(description));
        if (formatInfo.IsBlockCompressed && (usage & ~TextureUsage.ShaderResource) != 0)
            throw new ArgumentException("Block-compressed textures can only be shader resources.", nameof(description));
        if (formatInfo.IsDepth && (usage & (TextureUsage.RenderTarget | TextureUsage.UnorderedAccess)) != 0)
            throw new ArgumentException("Depth formats can't be render targets or unordered access textures.", nameof(description));
        if (!formatInfo.IsDepth && (usage & TextureUsage.DepthStencil) != 0)
            throw new ArgumentException($"{description.Format} is not a depth format.", nameof(description));
        if ((usage & TextureUsage.UnorderedAccess) != 0 && formatInfo.IsSrgb)
            throw new ArgumentException("sRGB formats can't be unordered access textures.", nameof(description));
        if (description.SampleCount > 1)
        {
            if (mipLevels != 1) throw new ArgumentException("Multisampled textures have exactly one mip level.", nameof(description));
            if (isCube) throw new ArgumentException("Cube textures can't be multisampled.", nameof(description));
            if ((usage & (TextureUsage.RenderTarget | TextureUsage.DepthStencil)) == 0)
                throw new ArgumentException("Multisampled textures must be render targets or depth targets.", nameof(description));
            if ((usage & TextureUsage.UnorderedAccess) != 0)
                throw new ArgumentException("Multisampled textures can't be unordered access textures.", nameof(description));
        }
    }
}
