#pragma program Shape vertex=ShapeVS pixel=ShapePS

cbuffer Frame
{
    float4x4 Rotation;
};

struct ShapeVertex
{
    float4 Position : SV_Position;
    float4 Color : COLOR0;
};

ShapeVertex ShapeVS(float3 position : POSITION, float4 color : COLOR)
{
    ShapeVertex output;
    output.Position = mul(float4(position, 1.0), Rotation);
    output.Color = color;
    return output;
}

float4 ShapePS(ShapeVertex input) : SV_Target
{
    return input.Color;
}
