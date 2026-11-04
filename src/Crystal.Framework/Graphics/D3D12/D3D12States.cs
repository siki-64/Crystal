using TerraFX.Interop.DirectX;
using static TerraFX.Interop.DirectX.D3D12_CONSERVATIVE_RASTERIZATION_MODE;
using static TerraFX.Interop.DirectX.D3D12_DEPTH_WRITE_MASK;
using static TerraFX.Interop.DirectX.D3D12_LOGIC_OP;
using static TerraFX.Interop.Windows.Windows;

namespace Crystal.Graphics.D3D12;

internal static unsafe class D3D12States
{
    private const int AnisotropicFilterBit = 0x40;

    public static D3D12_SAMPLER_DESC ToD3D12(in SamplerDescription description)
    {
        var reduction = (int)description.Reduction << 7;
        var filter = description.MaxAnisotropy > 1
            ? AnisotropicFilterBit | (1 << 4) | (1 << 2) | 1 | reduction
            : ((int)description.MinFilter << 4) | ((int)description.MagFilter << 2) | (int)description.MipFilter | reduction;
        var sampler = new D3D12_SAMPLER_DESC
        {
            Filter = (D3D12_FILTER)filter,
            AddressU = (D3D12_TEXTURE_ADDRESS_MODE)description.AddressU,
            AddressV = (D3D12_TEXTURE_ADDRESS_MODE)description.AddressV,
            AddressW = (D3D12_TEXTURE_ADDRESS_MODE)description.AddressW,
            MipLODBias = description.MipLodBias,
            MaxAnisotropy = (uint)description.MaxAnisotropy,
            ComparisonFunc = (D3D12_COMPARISON_FUNC)description.ComparisonFunction,
            MinLOD = description.MinLod,
            MaxLOD = description.MaxLod,
        };
        sampler.BorderColor[0] = description.BorderColor.X;
        sampler.BorderColor[1] = description.BorderColor.Y;
        sampler.BorderColor[2] = description.BorderColor.Z;
        sampler.BorderColor[3] = description.BorderColor.W;
        return sampler;
    }

    public static D3D12_BLEND_DESC ToD3D12(BlendState state)
    {
        var blend = new D3D12_BLEND_DESC
        {
            AlphaToCoverageEnable = state.AlphaToCoverage ? TRUE : FALSE,
            IndependentBlendEnable = state.IndependentBlend ? TRUE : FALSE,
        };
        for (var index = 0; index < GraphicsDevice.MaximumRenderTargets; index++)
        {
            var renderTarget = index < state.RenderTargets.Length ? state.RenderTargets[index] : RenderTargetBlendDescription.Opaque;
            blend.RenderTarget[index] = new D3D12_RENDER_TARGET_BLEND_DESC
            {
                BlendEnable = renderTarget.BlendEnable ? TRUE : FALSE,
                LogicOpEnable = FALSE,
                SrcBlend = (D3D12_BLEND)renderTarget.SourceBlend,
                DestBlend = (D3D12_BLEND)renderTarget.DestinationBlend,
                BlendOp = (D3D12_BLEND_OP)renderTarget.BlendOperation,
                SrcBlendAlpha = (D3D12_BLEND)renderTarget.SourceBlendAlpha,
                DestBlendAlpha = (D3D12_BLEND)renderTarget.DestinationBlendAlpha,
                BlendOpAlpha = (D3D12_BLEND_OP)renderTarget.BlendOperationAlpha,
                LogicOp = D3D12_LOGIC_OP_NOOP,
                RenderTargetWriteMask = (byte)renderTarget.WriteMask,
            };
        }
        return blend;
    }

    public static D3D12_RASTERIZER_DESC ToD3D12(RasterizerState state) => new()
    {
        FillMode = (D3D12_FILL_MODE)state.FillMode,
        CullMode = (D3D12_CULL_MODE)state.CullMode,
        FrontCounterClockwise = state.FrontCounterClockwise ? TRUE : FALSE,
        DepthBias = state.DepthBias,
        DepthBiasClamp = state.DepthBiasClamp,
        SlopeScaledDepthBias = state.SlopeScaledDepthBias,
        DepthClipEnable = state.DepthClipEnable ? TRUE : FALSE,
        MultisampleEnable = FALSE,
        AntialiasedLineEnable = state.AntialiasedLineEnable ? TRUE : FALSE,
        ForcedSampleCount = 0,
        ConservativeRaster = state.ConservativeRaster ? D3D12_CONSERVATIVE_RASTERIZATION_MODE_ON : D3D12_CONSERVATIVE_RASTERIZATION_MODE_OFF,
    };

    public static D3D12_DEPTH_STENCIL_DESC ToD3D12(DepthStencilState state) => new()
    {
        DepthEnable = state.DepthEnable ? TRUE : FALSE,
        DepthWriteMask = state.DepthWriteEnable ? D3D12_DEPTH_WRITE_MASK_ALL : D3D12_DEPTH_WRITE_MASK_ZERO,
        DepthFunc = (D3D12_COMPARISON_FUNC)state.DepthFunction,
        StencilEnable = state.StencilEnable ? TRUE : FALSE,
        StencilReadMask = state.StencilReadMask,
        StencilWriteMask = state.StencilWriteMask,
        FrontFace = ToD3D12(state.FrontFace),
        BackFace = ToD3D12(state.BackFace),
    };

    private static D3D12_DEPTH_STENCILOP_DESC ToD3D12(in StencilFaceDescription face) => new()
    {
        StencilFailOp = (D3D12_STENCIL_OP)face.FailOperation,
        StencilDepthFailOp = (D3D12_STENCIL_OP)face.DepthFailOperation,
        StencilPassOp = (D3D12_STENCIL_OP)face.PassOperation,
        StencilFunc = (D3D12_COMPARISON_FUNC)face.Function,
    };
}
