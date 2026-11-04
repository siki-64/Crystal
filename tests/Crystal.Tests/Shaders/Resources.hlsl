#include "Common.hlsli"

#pragma program Textured vertex=TexturedVS pixel=TexturedPS
#pragma program PulledVertices vertex=PulledVS pixel=WhitePS
#pragma program DepthVisualize vertex=FullscreenVS pixel=DepthVisualizePS
#pragma program RawColor vertex=FullscreenVS pixel=RawColorPS
#pragma program CountPixels vertex=FullscreenVS pixel=CountPixelsPS

Texture2D DiffuseTexture;
SamplerState DiffuseSampler;
StructuredBuffer<float2> Positions;
Texture2D<float> DepthTexture;
ByteAddressBuffer ColorData;
RWByteAddressBuffer Counter;

struct TexturedVertex
{
    float4 Position : SV_Position;
    float2 TextureCoordinate : TEXCOORD0;
};

TexturedVertex TexturedVS(float3 position : POSITION, float2 textureCoordinate : TEXCOORD0)
{
    TexturedVertex output;
    output.Position = float4(position, 1.0);
    output.TextureCoordinate = textureCoordinate;
    return output;
}

float4 TexturedPS(TexturedVertex input) : SV_Target
{
    return DiffuseTexture.Sample(DiffuseSampler, input.TextureCoordinate);
}

float4 PulledVS(uint vertexId : SV_VertexID) : SV_Position
{
    return float4(Positions[vertexId], 0.0, 1.0);
}

float4 DepthVisualizePS(float4 position : SV_Position) : SV_Target
{
    return float4(DepthTexture.Load(int3(position.xy, 0)), 0.0, 0.0, 1.0);
}

float4 RawColorPS() : SV_Target
{
    return asfloat(ColorData.Load4(0));
}

float4 CountPixelsPS() : SV_Target
{
    Counter.InterlockedAdd(0, 1);
    return float4(1.0, 1.0, 1.0, 1.0);
}
