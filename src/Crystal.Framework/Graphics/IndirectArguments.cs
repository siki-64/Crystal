namespace Crystal.Graphics;

public readonly record struct IndirectDrawArguments(uint VertexCountPerInstance, uint InstanceCount, uint StartVertex, uint StartInstance);

public readonly record struct IndirectDrawIndexedArguments(uint IndexCountPerInstance, uint InstanceCount, uint StartIndex, int BaseVertex, uint StartInstance);

public readonly record struct IndirectDispatchArguments(uint ThreadGroupCountX, uint ThreadGroupCountY, uint ThreadGroupCountZ);
