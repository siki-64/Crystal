#pragma program SpriteBatch vertex=SpriteVS pixel=SpritePS

cbuffer SpriteTransform
{
    float4x4 MatrixTransform;
};

Texture2D Texture;
SamplerState TextureSampler;

struct SpriteVertex
{
    float4 Position : SV_Position;
    float4 Color : COLOR0;
    float2 TextureCoordinate : TEXCOORD0;
};

SpriteVertex SpriteVS(float3 position : POSITION, float4 color : COLOR0, float2 textureCoordinate : TEXCOORD0)
{
    SpriteVertex output;
    output.Position = mul(float4(position, 1.0), MatrixTransform);
    output.Color = color;
    output.TextureCoordinate = textureCoordinate;
    return output;
}

float4 SpritePS(SpriteVertex input) : SV_Target
{
    return Texture.Sample(TextureSampler, input.TextureCoordinate) * input.Color;
}
