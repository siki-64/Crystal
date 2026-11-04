#include "Common.hlsli"

#pragma program VertexColor vertex=VertexColorVS pixel=VertexColorPS
#pragma program SolidColor vertex=FullscreenVS pixel=SolidColorPS
#pragma program Transformed vertex=TransformedVS pixel=SolidColorPS
#pragma program TransformedColumns vertex=TransformedColumnsVS pixel=SolidColorPS
#pragma program Instanced vertex=InstancedVS pixel=VertexColorPS
#pragma program ArrayValues vertex=FullscreenVS pixel=ArrayValuesPS
#pragma program DepthOnly vertex=VertexColorVS

cbuffer Material : register(b0)
{
    float4 Tint;
};

cbuffer Transform : register(b1)
{
    float4x4 WorldViewProjection;
    column_major float4x4 ColumnWorldViewProjection;
};

cbuffer Arrays : register(b2)
{
    float Weights[4];
    float4 Palette[2];
    uint PaletteIndex;
};

ColorVertex VertexColorVS(float3 position : POSITION, float4 color : COLOR)
{
    ColorVertex output;
    output.Position = float4(position, 1.0);
    output.Color = color;
    return output;
}

float4 VertexColorPS(ColorVertex input) : SV_Target
{
    return input.Color;
}

float4 SolidColorPS() : SV_Target
{
    return Tint;
}

float4 TransformedVS(float3 position : POSITION) : SV_Position
{
    return mul(float4(position, 1.0), WorldViewProjection);
}

float4 TransformedColumnsVS(float3 position : POSITION) : SV_Position
{
    return mul(float4(position, 1.0), ColumnWorldViewProjection);
}

ColorVertex InstancedVS(float3 position : POSITION, float2 offset : TEXCOORD1, float4 color : COLOR1)
{
    ColorVertex output;
    output.Position = float4(position.xy + offset, position.z, 1.0);
    output.Color = color;
    return output;
}

float4 ArrayValuesPS(float4 position : SV_Position) : SV_Target
{
    if (position.x < 16.0)
    {
        return float4(Weights[0], Weights[1], Weights[2], Weights[3]);
    }
    return Palette[PaletteIndex];
}
