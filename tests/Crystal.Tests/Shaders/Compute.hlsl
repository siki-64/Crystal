#pragma program FillBuffer compute=FillBufferCS
#pragma program Invert compute=InvertCS
#pragma program WritePositions compute=WritePositionsCS
#pragma program WriteDrawArguments compute=WriteDrawArgumentsCS
#pragma program ReadDepth compute=ReadDepthCS
#pragma program ReduceMip compute=ReduceMipCS

cbuffer FillParameters
{
    uint FillValue;
    uint FillCount;
};

cbuffer DrawParameters
{
    uint DrawCount;
};

RWStructuredBuffer<uint> Output;
Texture2D<float4> Source;
RWTexture2D<float4> Destination;
RWStructuredBuffer<float2> PositionsOutput;
RWByteAddressBuffer DrawArguments;
RWByteAddressBuffer DrawCountOutput;
Texture2D<float> DepthInput;
RWStructuredBuffer<float> DepthOutput;
RWTexture2D<float4> MipSource;
RWTexture2D<float4> MipTarget;

[numthreads(64, 1, 1)]
void FillBufferCS(uint3 id : SV_DispatchThreadID)
{
    if (id.x < FillCount) Output[id.x] = FillValue + id.x;
}

[numthreads(8, 8, 1)]
void InvertCS(uint3 id : SV_DispatchThreadID)
{
    Destination[id.xy] = float4(1.0 - Source[id.xy].rgb, 1.0);
}

[numthreads(3, 1, 1)]
void WritePositionsCS(uint3 id : SV_DispatchThreadID)
{
    PositionsOutput[id.x] = float2(id.x == 2 ? 3.0 : -1.0, id.x == 1 ? 3.0 : -1.0);
}

[numthreads(1, 1, 1)]
void WriteDrawArgumentsCS()
{
    DrawArguments.Store4(0, uint4(3, 1, 0, 0));
    DrawCountOutput.Store(0, DrawCount);
}

[numthreads(1, 1, 1)]
void ReadDepthCS()
{
    DepthOutput[0] = DepthInput.Load(int3(1, 1, 0));
}

[numthreads(8, 8, 1)]
void ReduceMipCS(uint3 id : SV_DispatchThreadID)
{
    uint2 source = id.xy * 2;
    MipTarget[id.xy] = max(max(MipSource[source], MipSource[source + uint2(1, 0)]), max(MipSource[source + uint2(0, 1)], MipSource[source + uint2(1, 1)]));
}
