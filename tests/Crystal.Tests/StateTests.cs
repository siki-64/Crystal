using Crystal.Graphics;
using Crystal.Graphics.D3D12;
using TerraFX.Interop.DirectX;

namespace Crystal.Tests;

[TestClass]
public sealed class StateTests
{
    [TestMethod]
    public void BlendStatesMapToD3D12()
    {
        var alphaBlend = D3D12States.ToD3D12(BlendState.AlphaBlend);
        Assert.AreEqual(0, (int)alphaBlend.IndependentBlendEnable);
        Assert.AreEqual(1, (int)alphaBlend.RenderTarget[0].BlendEnable);
        Assert.AreEqual(D3D12_BLEND.D3D12_BLEND_ONE, alphaBlend.RenderTarget[0].SrcBlend);
        Assert.AreEqual(D3D12_BLEND.D3D12_BLEND_INV_SRC_ALPHA, alphaBlend.RenderTarget[0].DestBlend);
        Assert.AreEqual((byte)D3D12_COLOR_WRITE_ENABLE.D3D12_COLOR_WRITE_ENABLE_ALL, alphaBlend.RenderTarget[0].RenderTargetWriteMask);

        var perTarget = new BlendState([RenderTargetBlendDescription.Opaque, RenderTargetBlendDescription.Additive with { WriteMask = ColorWriteChannels.Red }]);
        var independent = D3D12States.ToD3D12(perTarget);
        Assert.AreEqual(1, (int)independent.IndependentBlendEnable);
        Assert.AreEqual(D3D12_BLEND.D3D12_BLEND_SRC_ALPHA, independent.RenderTarget[1].SrcBlend);
        Assert.AreEqual(D3D12_BLEND.D3D12_BLEND_ONE, independent.RenderTarget[1].DestBlend);
        Assert.AreEqual((byte)D3D12_COLOR_WRITE_ENABLE.D3D12_COLOR_WRITE_ENABLE_RED, independent.RenderTarget[1].RenderTargetWriteMask);

        Assert.AreEqual(perTarget, new BlendState([RenderTargetBlendDescription.Opaque, RenderTargetBlendDescription.Additive with { WriteMask = ColorWriteChannels.Red }]));
        Assert.AreEqual(perTarget.GetHashCode(), new BlendState([RenderTargetBlendDescription.Opaque, RenderTargetBlendDescription.Additive with { WriteMask = ColorWriteChannels.Red }]).GetHashCode());
        Assert.AreNotEqual(BlendState.Opaque, BlendState.Additive);
    }

    [TestMethod]
    public void RasterizerStatesMapToD3D12()
    {
        var shadowCaster = D3D12States.ToD3D12(RasterizerState.CullBack with { DepthBias = 100, SlopeScaledDepthBias = 1.5f, DepthBiasClamp = 0.01f });
        Assert.AreEqual(D3D12_CULL_MODE.D3D12_CULL_MODE_BACK, shadowCaster.CullMode);
        Assert.AreEqual(D3D12_FILL_MODE.D3D12_FILL_MODE_SOLID, shadowCaster.FillMode);
        Assert.AreEqual(100, shadowCaster.DepthBias);
        Assert.AreEqual(1.5f, shadowCaster.SlopeScaledDepthBias);
        Assert.AreEqual(0.01f, shadowCaster.DepthBiasClamp);
        Assert.AreEqual(D3D12_CULL_MODE.D3D12_CULL_MODE_NONE, D3D12States.ToD3D12(RasterizerState.CullNone).CullMode);
        Assert.AreEqual(D3D12_FILL_MODE.D3D12_FILL_MODE_WIREFRAME, D3D12States.ToD3D12(RasterizerState.Wireframe).FillMode);
    }

    [TestMethod]
    public void DepthStencilStatesMapToD3D12()
    {
        var depthRead = D3D12States.ToD3D12(DepthStencilState.DepthRead);
        Assert.AreEqual(1, (int)depthRead.DepthEnable);
        Assert.AreEqual(D3D12_DEPTH_WRITE_MASK.D3D12_DEPTH_WRITE_MASK_ZERO, depthRead.DepthWriteMask);
        Assert.AreEqual(D3D12_COMPARISON_FUNC.D3D12_COMPARISON_FUNC_LESS_EQUAL, depthRead.DepthFunc);
        Assert.AreEqual(D3D12_COMPARISON_FUNC.D3D12_COMPARISON_FUNC_GREATER_EQUAL, D3D12States.ToD3D12(DepthStencilState.ReverseZ).DepthFunc);

        var stencil = D3D12States.ToD3D12(DepthStencilState.Default with
        {
            StencilEnable = true,
            FrontFace = new StencilFaceDescription { PassOperation = StencilOperation.Replace, Function = ComparisonFunction.Equal },
        });
        Assert.AreEqual(D3D12_STENCIL_OP.D3D12_STENCIL_OP_REPLACE, stencil.FrontFace.StencilPassOp);
        Assert.AreEqual(D3D12_COMPARISON_FUNC.D3D12_COMPARISON_FUNC_EQUAL, stencil.FrontFace.StencilFunc);
        Assert.AreEqual(D3D12_STENCIL_OP.D3D12_STENCIL_OP_KEEP, stencil.BackFace.StencilPassOp);
    }

    [TestMethod]
    public void SamplerDescriptionsMapToD3D12Filters()
    {
        Assert.AreEqual(D3D12_FILTER.D3D12_FILTER_MIN_MAG_MIP_POINT, D3D12States.ToD3D12(SamplerDescription.PointClamp).Filter);
        Assert.AreEqual(D3D12_FILTER.D3D12_FILTER_MIN_MAG_MIP_LINEAR, D3D12States.ToD3D12(SamplerDescription.LinearWrap).Filter);
        Assert.AreEqual(D3D12_FILTER.D3D12_FILTER_ANISOTROPIC, D3D12States.ToD3D12(SamplerDescription.AnisotropicWrap).Filter);
        Assert.AreEqual(D3D12_FILTER.D3D12_FILTER_COMPARISON_MIN_MAG_LINEAR_MIP_POINT, D3D12States.ToD3D12(SamplerDescription.ShadowComparison).Filter);
        Assert.AreEqual(D3D12_FILTER.D3D12_FILTER_MAXIMUM_MIN_MAG_MIP_POINT, D3D12States.ToD3D12(SamplerDescription.PointClamp with { Reduction = FilterReduction.Maximum }).Filter);
        Assert.AreEqual(D3D12_TEXTURE_ADDRESS_MODE.D3D12_TEXTURE_ADDRESS_MODE_CLAMP, D3D12States.ToD3D12(SamplerDescription.LinearClamp).AddressV);
    }
}
