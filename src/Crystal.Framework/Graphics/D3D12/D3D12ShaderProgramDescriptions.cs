namespace Crystal.Graphics.D3D12;

internal static class D3D12ShaderProgramDescriptions
{
    private const int MaxConstantBufferSize = 65536;
    private const int MaxRootSignatureDwords = 64;
    private const int ConstantBufferRootDwords = 2;
    private const int ResourceRootDwords = 1;

    private static ReadOnlySpan<byte> ContainerMagic => "DXBC"u8;

    public static D3D12ShaderLibraryFile ToLibraryFile(ReadOnlySpan<ShaderProgramDescription> descriptions)
    {
        var shaders = new List<byte[]>();
        var programs = new D3D12ShaderProgramInfo[descriptions.Length];
        var names = new HashSet<string>(StringComparer.Ordinal);
        for (var index = 0; index < descriptions.Length; index++)
        {
            var description = descriptions[index] ?? throw new ArgumentException($"Program description {index} is null.", nameof(descriptions));
            if (string.IsNullOrEmpty(description.Name)) throw new ArgumentException($"Program description {index} has no name.", nameof(descriptions));
            if (!names.Add(description.Name)) throw new ArgumentException($"Two program descriptions are named '{description.Name}'.", nameof(descriptions));
            programs[index] = ToInfo(description, shaders);
        }
        return new D3D12ShaderLibraryFile([.. shaders], programs);
    }

    private static D3D12ShaderProgramInfo ToInfo(ShaderProgramDescription description, List<byte[]> shaders)
    {
        var name = description.Name;
        var hasVertex = !description.VertexShader.IsEmpty;
        var hasPixel = !description.PixelShader.IsEmpty;
        var hasCompute = !description.ComputeShader.IsEmpty;
        if (hasCompute && (hasVertex || hasPixel)) throw Invalid(name, "has a compute shader and a vertex or pixel shader.");
        if (!hasCompute && !hasVertex) throw Invalid(name, "needs a vertex shader or a compute shader.");

        var stages = (hasVertex ? ShaderStages.Vertex : 0) | (hasPixel ? ShaderStages.Pixel : 0) | (hasCompute ? ShaderStages.Compute : 0);
        var threadGroupSize = description.ThreadGroupSize;
        if (hasCompute && (threadGroupSize.X < 1 || threadGroupSize.Y < 1 || threadGroupSize.Z < 1))
            throw Invalid(name, $"has thread group size {threadGroupSize.X}x{threadGroupSize.Y}x{threadGroupSize.Z}; each side must be at least 1.");
        if (!hasCompute && threadGroupSize != default) throw Invalid(name, "has a thread group size but no compute shader.");
        if (!hasVertex && description.VertexInputs.Count > 0) throw Invalid(name, "has vertex inputs but no vertex shader.");

        var occupiedRegisters = new Dictionary<(char Class, int Space, int Register, ShaderStages Stage), string>();
        var constantBuffers = new D3D12ConstantBufferInfo[description.ConstantBuffers.Count];
        var bufferNames = new HashSet<string>(StringComparer.Ordinal);
        for (var index = 0; index < constantBuffers.Length; index++)
        {
            var buffer = description.ConstantBuffers[index];
            if (string.IsNullOrEmpty(buffer.Name)) throw Invalid(name, $"has constant buffer {index} without a name.");
            if (!bufferNames.Add(buffer.Name)) throw Invalid(name, $"has two constant buffers named '{buffer.Name}'.");
            if (buffer.Size < 1 || buffer.Size > MaxConstantBufferSize)
                throw Invalid(name, $"has constant buffer '{buffer.Name}' of {buffer.Size} bytes; sizes run from 1 to {MaxConstantBufferSize}.");
            CheckStages(name, $"constant buffer '{buffer.Name}'", buffer.Stages, stages);
            if (description.UsesDescriptorIndices) CheckDescriptorIndexedConstantBuffer(name, buffer, hasCompute);
            Occupy(occupiedRegisters, name, 'b', buffer.Space, buffer.Register, buffer.Stages, $"constant buffer '{buffer.Name}'");
            var variables = new D3D12ShaderVariableInfo[buffer.Variables.Count];
            for (var variableIndex = 0; variableIndex < variables.Length; variableIndex++)
                variables[variableIndex] = ToInfo(name, buffer, buffer.Variables[variableIndex]);
            constantBuffers[index] = new D3D12ConstantBufferInfo(buffer.Name, buffer.Register, buffer.Space, buffer.Size, (D3D12ShaderVisibility)buffer.Stages, variables);
        }

        var resources = new D3D12ShaderResourceInfo[description.Resources.Count];
        var occupiedSlots = new Dictionary<(ShaderStages Stage, int Slot), string>();
        for (var index = 0; index < resources.Length; index++)
        {
            var resource = description.Resources[index];
            if (string.IsNullOrEmpty(resource.Name)) throw Invalid(name, $"has resource {index} without a name.");
            if (!Enum.IsDefined(resource.Kind)) throw Invalid(name, $"has resource '{resource.Name}' of unknown kind {(int)resource.Kind}.");
            CheckDimension(name, resource);
            CheckStages(name, $"resource '{resource.Name}'", resource.Stages, stages);
            Occupy(occupiedRegisters, name, RegisterClass(resource.Kind), resource.Space, resource.Register, resource.Stages, $"resource '{resource.Name}'");
            if (description.UsesDescriptorIndices) CheckDescriptorSlots(name, resource, occupiedSlots);
            else if (resource.VertexDescriptorSlot >= 0 || resource.PixelDescriptorSlot >= 0 || resource.ComputeDescriptorSlot >= 0)
                throw Invalid(name, $"gives resource '{resource.Name}' a descriptor slot but doesn't use descriptor indices.");
            resources[index] = new D3D12ShaderResourceInfo(
                resource.Name, (D3D12ShaderResourceKind)resource.Kind, (int)resource.Dimension, resource.Register, resource.Space, (D3D12ShaderVisibility)resource.Stages,
                resource.VertexDescriptorSlot, resource.PixelDescriptorSlot, resource.ComputeDescriptorSlot);
        }

        var rootDwords = constantBuffers.Length * ConstantBufferRootDwords + resources.Length * ResourceRootDwords;
        if (!description.UsesDescriptorIndices && rootDwords > MaxRootSignatureDwords)
            throw Invalid(name, $"needs {rootDwords} root signature DWORDs ({ConstantBufferRootDwords} per constant buffer, {ResourceRootDwords} per resource); the limit is {MaxRootSignatureDwords}.");

        var vertexInputs = new D3D12VertexInputInfo[description.VertexInputs.Count];
        for (var index = 0; index < vertexInputs.Length; index++)
        {
            var input = description.VertexInputs[index];
            if (string.IsNullOrEmpty(input.Semantic)) throw Invalid(name, $"has vertex input {index} without a semantic.");
            if (input.SemanticIndex < 0 || input.Register < 0) throw Invalid(name, $"has vertex input {input.Semantic}{input.SemanticIndex} with a negative index or register.");
            vertexInputs[index] = new D3D12VertexInputInfo(input.Semantic, input.SemanticIndex, input.Register);
        }

        return new D3D12ShaderProgramInfo(
            name,
            AddShader(shaders, name, "vertex", description.VertexShader),
            AddShader(shaders, name, "pixel", description.PixelShader),
            AddShader(shaders, name, "compute", description.ComputeShader),
            new D3D12ThreadGroupSize(threadGroupSize.X, threadGroupSize.Y, threadGroupSize.Z),
            constantBuffers,
            resources,
            vertexInputs,
            description.UsesDescriptorIndices);
    }

    private static D3D12ShaderVariableInfo ToInfo(string program, ShaderConstantBufferDescription buffer, ShaderVariableDescription variable)
    {
        var where = $"variable '{variable.Name}' in constant buffer '{buffer.Name}'";
        if (string.IsNullOrEmpty(variable.Name)) throw Invalid(program, $"has a variable without a name in constant buffer '{buffer.Name}'.");
        if (!Enum.IsDefined(variable.Class)) throw Invalid(program, $"has {where} of unknown class {(int)variable.Class}.");
        if (variable.Offset < 0 || variable.Size < 1 || (long)variable.Offset + variable.Size > buffer.Size)
            throw Invalid(program, $"has {where} at offset {variable.Offset} with {variable.Size} bytes, which isn't inside the buffer's {buffer.Size} bytes.");
        if (variable.Elements < 0) throw Invalid(program, $"has {where} with {variable.Elements} elements.");
        if (variable.Class != ShaderVariableClass.Struct && (variable.Rows is < 1 or > 4 || variable.Columns is < 1 or > 4))
            throw Invalid(program, $"has {where} with {variable.Rows} rows and {variable.Columns} columns; each runs from 1 to 4.");
        return new D3D12ShaderVariableInfo(
            variable.Name, variable.Offset, variable.Size, (D3D12ShaderVariableClass)variable.Class, variable.Rows, variable.Columns, variable.Elements);
    }

    private static void CheckDescriptorIndexedConstantBuffer(string program, ShaderConstantBufferDescription buffer, bool isCompute)
    {
        var registerLimit = isCompute ? D3D12RootSignatures.ComputeConstantBufferRegisters : D3D12RootSignatures.GraphicsConstantBufferRegisters;
        if (buffer.Space != 0 || buffer.Register < 0 || buffer.Register >= registerLimit)
            throw Invalid(program, $"has constant buffer '{buffer.Name}' at b{buffer.Register}, space {buffer.Space}; with descriptor indices constant buffers must be in space 0 below b{registerLimit}.");
    }

    private static void CheckDescriptorSlots(string program, ShaderResourceDescription resource, Dictionary<(ShaderStages Stage, int Slot), string> occupiedSlots)
    {
        CheckDescriptorSlot(program, resource, ShaderStages.Vertex, resource.VertexDescriptorSlot, D3D12RootSignatures.VertexDescriptorSlots, occupiedSlots);
        CheckDescriptorSlot(program, resource, ShaderStages.Pixel, resource.PixelDescriptorSlot, D3D12RootSignatures.PixelDescriptorSlots, occupiedSlots);
        CheckDescriptorSlot(program, resource, ShaderStages.Compute, resource.ComputeDescriptorSlot, D3D12RootSignatures.ComputeDescriptorSlots, occupiedSlots);
    }

    private static void CheckDescriptorSlot(
        string program, ShaderResourceDescription resource, ShaderStages stage, int slot, int slotLimit, Dictionary<(ShaderStages Stage, int Slot), string> occupiedSlots)
    {
        var isVisible = (resource.Stages & stage) != 0;
        if (!isVisible && slot >= 0) throw Invalid(program, $"gives resource '{resource.Name}' a {stage} descriptor slot, but it isn't visible to that stage.");
        if (!isVisible || slot == -1) return;
        if (slot < 0 || slot >= slotLimit) throw Invalid(program, $"gives resource '{resource.Name}' {stage} descriptor slot {slot}; slots run from 0 to {slotLimit - 1}, or -1 when the stage doesn't read it.");
        if (!occupiedSlots.TryAdd((stage, slot), resource.Name))
            throw Invalid(program, $"gives resources '{resource.Name}' and '{occupiedSlots[(stage, slot)]}' the same {stage} descriptor slot {slot}.");
    }

    private static void CheckDimension(string program, ShaderResourceDescription resource)
    {
        var dimension = resource.Dimension;
        var isValid = resource.Kind switch
        {
            ShaderResourceKind.Sampler => dimension == ShaderResourceDimension.None,
            ShaderResourceKind.Texture => dimension is >= ShaderResourceDimension.Texture1D and <= ShaderResourceDimension.TextureCubeArray,
            ShaderResourceKind.ReadWriteTexture => dimension is >= ShaderResourceDimension.Texture1D and <= ShaderResourceDimension.Texture3D
                and not ShaderResourceDimension.Texture2DMS and not ShaderResourceDimension.Texture2DMSArray,
            _ => dimension == ShaderResourceDimension.Buffer,
        };
        if (!isValid) throw Invalid(program, $"has {resource.Kind} '{resource.Name}' with dimension {dimension}.");
    }

    private static void CheckStages(string program, string what, ShaderStages entryStages, ShaderStages programStages)
    {
        if (entryStages == ShaderStages.None) throw Invalid(program, $"has {what} visible to no stage.");
        if ((entryStages & ~programStages) != 0) throw Invalid(program, $"has {what} visible to {entryStages}, but the program's stages are {programStages}.");
    }

    private static void Occupy(
        Dictionary<(char Class, int Space, int Register, ShaderStages Stage), string> occupied,
        string program,
        char registerClass,
        int space,
        int register,
        ShaderStages stages,
        string what)
    {
        if (register < 0 || space < 0) throw Invalid(program, $"has {what} with a negative register or space.");
        foreach (var stage in (ReadOnlySpan<ShaderStages>)[ShaderStages.Vertex, ShaderStages.Pixel, ShaderStages.Compute])
        {
            if ((stages & stage) == 0) continue;
            if (!occupied.TryAdd((registerClass, space, register, stage), what))
                throw Invalid(program, $"binds {what} and {occupied[(registerClass, space, register, stage)]} to {registerClass}{register}, space {space}, in the {stage} stage.");
        }
    }

    private static char RegisterClass(ShaderResourceKind kind) => kind switch
    {
        ShaderResourceKind.Sampler => 's',
        ShaderResourceKind.ReadWriteTexture or ShaderResourceKind.ReadWriteStructuredBuffer or ShaderResourceKind.ReadWriteByteAddressBuffer => 'u',
        _ => 't',
    };

    private static int AddShader(List<byte[]> shaders, string program, string stage, ReadOnlyMemory<byte> bytecode)
    {
        if (bytecode.IsEmpty) return -1;
        if (!bytecode.Span.StartsWith(ContainerMagic)) throw Invalid(program, $"has a {stage} shader that isn't a DXBC or DXIL container.");
        shaders.Add(bytecode.ToArray());
        return shaders.Count - 1;
    }

    private static ArgumentException Invalid(string program, string problem) => new($"Program '{program}' {problem}", "programs");
}
