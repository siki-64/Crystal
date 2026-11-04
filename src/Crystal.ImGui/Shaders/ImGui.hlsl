#pragma program ImGui vertex=ImGuiVS pixel=ImGuiPS

cbuffer Projection
{
    float4x4 ProjectionMatrix;
};

Texture2D FontTexture;
SamplerState FontSampler;

struct ImGuiVertex
{
    float4 Position : SV_Position;
    float2 TextureCoordinate : TEXCOORD0;
    float4 Color : COLOR0;
};

ImGuiVertex ImGuiVS(float2 position : POSITION, float2 textureCoordinate : TEXCOORD0, float4 color : COLOR0)
{
    ImGuiVertex output;
    output.Position = mul(float4(position, 0.0, 1.0), ProjectionMatrix);
    output.TextureCoordinate = textureCoordinate;
    output.Color = color;
    return output;
}

float4 ImGuiPS(ImGuiVertex input) : SV_Target
{
    return input.Color * FontTexture.Sample(FontSampler, input.TextureCoordinate);
}
