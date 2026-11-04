struct ColorVertex
{
    float4 Position : SV_Position;
    float4 Color : COLOR0;
};

float4 FullscreenVS(uint vertexId : SV_VertexID) : SV_Position
{
    return float4(vertexId == 2 ? 3.0 : -1.0, vertexId == 1 ? 3.0 : -1.0, 0.0, 1.0);
}

float4 WhitePS() : SV_Target
{
    return float4(1.0, 1.0, 1.0, 1.0);
}
