using TerraFX.Interop.DirectX;
using static TerraFX.Interop.DirectX.DXGI_FORMAT;

namespace Crystal.Graphics.D3D12;

internal readonly record struct D3D12FormatInfo(
    DXGI_FORMAT ResourceFormat,
    DXGI_FORMAT ViewFormat,
    DXGI_FORMAT DepthStencilFormat,
    int BlockSize,
    int BytesPerBlock)
{
    public bool IsDepth => DepthStencilFormat != DXGI_FORMAT_UNKNOWN;
    public bool IsBlockCompressed => BlockSize == 4;
    public bool HasStencil => DepthStencilFormat is DXGI_FORMAT_D24_UNORM_S8_UINT or DXGI_FORMAT_D32_FLOAT_S8X24_UINT;
    public bool IsSrgb => ViewFormat is DXGI_FORMAT_R8G8B8A8_UNORM_SRGB or DXGI_FORMAT_B8G8R8A8_UNORM_SRGB or DXGI_FORMAT_BC1_UNORM_SRGB
        or DXGI_FORMAT_BC2_UNORM_SRGB or DXGI_FORMAT_BC3_UNORM_SRGB or DXGI_FORMAT_BC7_UNORM_SRGB;
}

internal static class D3D12Formats
{
    public const uint DefaultComponentMapping = 0x1688;

    public static D3D12FormatInfo Get(SurfaceFormat format) => format switch
    {
        SurfaceFormat.R8Unorm => Color(DXGI_FORMAT_R8_UNORM, 1),
        SurfaceFormat.Rg8Unorm => Color(DXGI_FORMAT_R8G8_UNORM, 2),
        SurfaceFormat.Rgba8Unorm => Color(DXGI_FORMAT_R8G8B8A8_UNORM, 4),
        SurfaceFormat.Rgba8UnormSrgb => Color(DXGI_FORMAT_R8G8B8A8_UNORM_SRGB, 4),
        SurfaceFormat.Bgra8Unorm => Color(DXGI_FORMAT_B8G8R8A8_UNORM, 4),
        SurfaceFormat.Bgra8UnormSrgb => Color(DXGI_FORMAT_B8G8R8A8_UNORM_SRGB, 4),
        SurfaceFormat.R16Unorm => Color(DXGI_FORMAT_R16_UNORM, 2),
        SurfaceFormat.Rg16Unorm => Color(DXGI_FORMAT_R16G16_UNORM, 4),
        SurfaceFormat.Rgba16Unorm => Color(DXGI_FORMAT_R16G16B16A16_UNORM, 8),
        SurfaceFormat.R16Float => Color(DXGI_FORMAT_R16_FLOAT, 2),
        SurfaceFormat.Rg16Float => Color(DXGI_FORMAT_R16G16_FLOAT, 4),
        SurfaceFormat.Rgba16Float => Color(DXGI_FORMAT_R16G16B16A16_FLOAT, 8),
        SurfaceFormat.R32Float => Color(DXGI_FORMAT_R32_FLOAT, 4),
        SurfaceFormat.Rg32Float => Color(DXGI_FORMAT_R32G32_FLOAT, 8),
        SurfaceFormat.Rgba32Float => Color(DXGI_FORMAT_R32G32B32A32_FLOAT, 16),
        SurfaceFormat.R32Uint => Color(DXGI_FORMAT_R32_UINT, 4),
        SurfaceFormat.Rg11B10Float => Color(DXGI_FORMAT_R11G11B10_FLOAT, 4),
        SurfaceFormat.Rgb10A2Unorm => Color(DXGI_FORMAT_R10G10B10A2_UNORM, 4),
        SurfaceFormat.Bc1Unorm => Block(DXGI_FORMAT_BC1_UNORM, 8),
        SurfaceFormat.Bc1UnormSrgb => Block(DXGI_FORMAT_BC1_UNORM_SRGB, 8),
        SurfaceFormat.Bc2Unorm => Block(DXGI_FORMAT_BC2_UNORM, 16),
        SurfaceFormat.Bc2UnormSrgb => Block(DXGI_FORMAT_BC2_UNORM_SRGB, 16),
        SurfaceFormat.Bc3Unorm => Block(DXGI_FORMAT_BC3_UNORM, 16),
        SurfaceFormat.Bc3UnormSrgb => Block(DXGI_FORMAT_BC3_UNORM_SRGB, 16),
        SurfaceFormat.Bc4Unorm => Block(DXGI_FORMAT_BC4_UNORM, 8),
        SurfaceFormat.Bc4Snorm => Block(DXGI_FORMAT_BC4_SNORM, 8),
        SurfaceFormat.Bc5Unorm => Block(DXGI_FORMAT_BC5_UNORM, 16),
        SurfaceFormat.Bc5Snorm => Block(DXGI_FORMAT_BC5_SNORM, 16),
        SurfaceFormat.Bc6hUfloat => Block(DXGI_FORMAT_BC6H_UF16, 16),
        SurfaceFormat.Bc6hSfloat => Block(DXGI_FORMAT_BC6H_SF16, 16),
        SurfaceFormat.Bc7Unorm => Block(DXGI_FORMAT_BC7_UNORM, 16),
        SurfaceFormat.Bc7UnormSrgb => Block(DXGI_FORMAT_BC7_UNORM_SRGB, 16),
        SurfaceFormat.Depth16Unorm => Depth(DXGI_FORMAT_R16_TYPELESS, DXGI_FORMAT_R16_UNORM, DXGI_FORMAT_D16_UNORM, 2),
        SurfaceFormat.Depth24UnormStencil8 => Depth(DXGI_FORMAT_R24G8_TYPELESS, DXGI_FORMAT_R24_UNORM_X8_TYPELESS, DXGI_FORMAT_D24_UNORM_S8_UINT, 4),
        SurfaceFormat.Depth32Float => Depth(DXGI_FORMAT_R32_TYPELESS, DXGI_FORMAT_R32_FLOAT, DXGI_FORMAT_D32_FLOAT, 4),
        SurfaceFormat.Depth32FloatStencil8 => Depth(DXGI_FORMAT_R32G8X24_TYPELESS, DXGI_FORMAT_R32_FLOAT_X8X24_TYPELESS, DXGI_FORMAT_D32_FLOAT_S8X24_UINT, 8),
        _ => throw new ArgumentOutOfRangeException(nameof(format), format, null),
    };

    private static D3D12FormatInfo Color(DXGI_FORMAT format, int bytesPerPixel) => new(format, format, DXGI_FORMAT_UNKNOWN, 1, bytesPerPixel);

    private static D3D12FormatInfo Block(DXGI_FORMAT format, int bytesPerBlock) => new(format, format, DXGI_FORMAT_UNKNOWN, 4, bytesPerBlock);

    private static D3D12FormatInfo Depth(DXGI_FORMAT typeless, DXGI_FORMAT shaderView, DXGI_FORMAT depthStencil, int bytesPerPixel) =>
        new(typeless, shaderView, depthStencil, 1, bytesPerPixel);
}
