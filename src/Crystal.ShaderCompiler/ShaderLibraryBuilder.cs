using System.Text;
using Crystal.Graphics.D3D12;

namespace Crystal.ShaderCompiler;

internal static class ShaderLibraryBuilder
{
    public const int GraphicsConstantBufferRegisterLimit = 4;
    public const int ComputeConstantBufferRegisterLimit = 8;

    public static D3D12ShaderLibraryFile? Build(ShaderFile file, CompilerOptions options, DxcShaderCompiler compiler, List<ShaderDiagnostic> diagnostics) =>
        Build(file.SourcePath, File.ReadAllBytes(file.SourcePath), options, compiler, diagnostics);

    public static D3D12ShaderLibraryFile? Build(string path, byte[] source, CompilerOptions options, DxcShaderCompiler compiler, List<ShaderDiagnostic> diagnostics)
    {
        var errorCount = diagnostics.Count(diagnostic => diagnostic.IsError);
        var declarations = ProgramDeclarationParser.Parse(path, Encoding.UTF8.GetString(source), diagnostics);
        if (declarations.Count == 0 && !diagnostics.Exists(diagnostic => diagnostic.IsError))
            diagnostics.Add(new ShaderDiagnostic(path, 1, 1, IsError: false, "CRYSTAL1000", "The file declares no programs. Add '#pragma program <Name> vertex=<EntryPoint> pixel=<EntryPoint>' or '#pragma program <Name> compute=<EntryPoint>'."));

        var shaders = new List<byte[]>();
        var reflections = new List<StageReflection>();
        var stagesOfShaders = new List<ShaderStage>();
        string? preprocessedSource = null;
        var shaderIndices = new Dictionary<(ShaderStage, string), int>();
        int CompileOnce(ShaderStage stage, string entryPoint)
        {
            if (shaderIndices.TryGetValue((stage, entryPoint), out var existing)) return existing;
            var compiled = compiler.Compile(path, source, entryPoint, stage, options, diagnostics);
            var bytecode = compiled == null ? null : CompileWithDescriptorIndices(compiled, stage, entryPoint);
            var index = -1;
            if (compiled != null && bytecode != null)
            {
                index = shaders.Count;
                shaders.Add(bytecode);
                reflections.Add(compiled.Reflection);
                stagesOfShaders.Add(stage);
            }
            shaderIndices.Add((stage, entryPoint), index);
            return index;
        }

        byte[]? CompileWithDescriptorIndices(CompiledShader compiled, ShaderStage stage, string entryPoint)
        {
            if (compiled.Reflection.Resources.Count == 0) return compiled.Bytecode;
            preprocessedSource ??= compiler.Preprocess(path, source, options, diagnostics);
            if (preprocessedSource == null) return null;
            var rewritten = DescriptorIndexRewriter.Rewrite(preprocessedSource, compiled.Reflection.Resources, stage, compiled.Reflection.Problems);
            if (rewritten == null) return compiled.Bytecode;
            return compiler.Compile(path, Encoding.UTF8.GetBytes(rewritten), entryPoint, stage, options, diagnostics)?.Bytecode;
        }

        var programs = new List<D3D12ShaderProgramInfo>();
        foreach (var declaration in declarations)
        {
            var vertexShader = declaration.VertexEntryPoint is { } vertexEntryPoint ? CompileOnce(ShaderStage.Vertex, vertexEntryPoint) : -1;
            var pixelShader = declaration.PixelEntryPoint is { } pixelEntryPoint ? CompileOnce(ShaderStage.Pixel, pixelEntryPoint) : -1;
            var computeShader = declaration.ComputeEntryPoint is { } computeEntryPoint ? CompileOnce(ShaderStage.Compute, computeEntryPoint) : -1;
            if ((declaration.VertexEntryPoint != null && vertexShader < 0) || (declaration.PixelEntryPoint != null && pixelShader < 0)
                || (declaration.ComputeEntryPoint != null && computeShader < 0)) continue;

            var program = Merge(path, declaration, vertexShader, pixelShader, computeShader, reflections, stagesOfShaders, diagnostics);
            if (program != null) programs.Add(program);
        }

        return diagnostics.Count(diagnostic => diagnostic.IsError) > errorCount ? null : new D3D12ShaderLibraryFile([.. shaders], [.. programs]);
    }

    private static D3D12ShaderProgramInfo? Merge(
        string path, ProgramDeclaration declaration, int vertexShader, int pixelShader, int computeShader, List<StageReflection> reflections, List<ShaderStage> stagesOfShaders, List<ShaderDiagnostic> diagnostics)
    {
        var errors = new List<string>();
        var stages = new[] { vertexShader, pixelShader, computeShader }.Where(shader => shader >= 0).Select(shader => (Kind: stagesOfShaders[shader], Reflection: reflections[shader])).ToArray();
        foreach (var stage in stages) errors.AddRange(stage.Reflection.Problems);
        var constantBufferRegisterLimit = computeShader >= 0 ? ComputeConstantBufferRegisterLimit : GraphicsConstantBufferRegisterLimit;

        var constantBuffers = new Dictionary<(int Register, int Space), D3D12ConstantBufferInfo>();
        var resources = new Dictionary<(char RegisterType, int Register, int Space), D3D12ShaderResourceInfo>();
        foreach (var stage in stages)
        {
            foreach (var buffer in stage.Reflection.ConstantBuffers)
            {
                if (buffer.Space != 0 || buffer.Register >= constantBufferRegisterLimit)
                    errors.Add($"Constant buffer {buffer.Name} is at b{buffer.Register}, space{buffer.Space}; constant buffers must be in space0 below b{constantBufferRegisterLimit}.");
                var key = (buffer.Register, buffer.Space);
                if (!constantBuffers.TryGetValue(key, out var existing))
                {
                    constantBuffers.Add(key, buffer);
                    continue;
                }
                if (existing.Name != buffer.Name)
                {
                    errors.Add($"Constant buffers '{existing.Name}' and '{buffer.Name}' share register b{buffer.Register}, space{buffer.Space}.");
                    continue;
                }
                constantBuffers[key] = existing with
                {
                    Size = Math.Max(existing.Size, buffer.Size),
                    Visibility = existing.Visibility | buffer.Visibility,
                    Variables = MergeVariables(existing, buffer, errors),
                };
            }

            for (var slot = 0; slot < stage.Reflection.Resources.Count; slot++)
            {
                var resource = WithDescriptorSlot(stage.Reflection.Resources[slot], stage.Kind, slot);
                var key = (RegisterType(resource.Kind), resource.Register, resource.Space);
                if (!resources.TryGetValue(key, out var existing))
                {
                    resources.Add(key, resource);
                    continue;
                }
                if (existing.Name != resource.Name || existing.Kind != resource.Kind || existing.Dimension != resource.Dimension)
                {
                    errors.Add($"'{existing.Name}' and '{resource.Name}' share register {key.Item1}{resource.Register}, space{resource.Space} with different declarations.");
                    continue;
                }
                resources[key] = existing with
                {
                    Visibility = existing.Visibility | resource.Visibility,
                    VertexDescriptorSlot = Math.Max(existing.VertexDescriptorSlot, resource.VertexDescriptorSlot),
                    PixelDescriptorSlot = Math.Max(existing.PixelDescriptorSlot, resource.PixelDescriptorSlot),
                    ComputeDescriptorSlot = Math.Max(existing.ComputeDescriptorSlot, resource.ComputeDescriptorSlot),
                };
            }
        }

        var orderedBuffers = constantBuffers.Values.OrderBy(buffer => buffer.Space).ThenBy(buffer => buffer.Register).ToArray();
        var orderedResources = resources
            .OrderBy(entry => entry.Value.Kind == D3D12ShaderResourceKind.Sampler)
            .ThenBy(entry => entry.Key.RegisterType)
            .ThenBy(entry => entry.Key.Space)
            .ThenBy(entry => entry.Key.Register)
            .Select(entry => entry.Value)
            .ToArray();

        var names = new HashSet<string>();
        foreach (var name in orderedBuffers.SelectMany(buffer => buffer.Variables).Select(variable => variable.Name).Concat(orderedResources.Select(resource => resource.Name)))
        {
            if (!names.Add(name)) errors.Add($"Parameter name '{name}' is declared more than once.");
        }
        if (orderedBuffers.Select(buffer => buffer.Name).Distinct().Count() != orderedBuffers.Length) errors.Add("Two constant buffers have the same name.");

        foreach (var error in errors) diagnostics.Add(new ShaderDiagnostic(path, declaration.Line, 1, IsError: true, "CRYSTAL1002", $"Program '{declaration.Name}': {error}"));
        if (errors.Count > 0) return null;

        var threadGroupSize = computeShader >= 0 ? reflections[computeShader].ThreadGroupSize : default;
        D3D12VertexInputInfo[] vertexInputs = vertexShader >= 0 ? [.. reflections[vertexShader].VertexInputs] : [];
        return new D3D12ShaderProgramInfo(declaration.Name, vertexShader, pixelShader, computeShader, threadGroupSize, orderedBuffers, orderedResources, vertexInputs, UsesDescriptorIndices: true);
    }

    private static D3D12ShaderResourceInfo WithDescriptorSlot(D3D12ShaderResourceInfo resource, ShaderStage stage, int slot) => stage switch
    {
        ShaderStage.Vertex => resource with { VertexDescriptorSlot = slot },
        ShaderStage.Pixel => resource with { PixelDescriptorSlot = slot },
        _ => resource with { ComputeDescriptorSlot = slot },
    };

    private static D3D12ShaderVariableInfo[] MergeVariables(D3D12ConstantBufferInfo existing, D3D12ConstantBufferInfo added, List<string> errors)
    {
        var variables = existing.Variables.ToDictionary(variable => variable.Name);
        foreach (var variable in added.Variables)
        {
            if (!variables.TryGetValue(variable.Name, out var known))
            {
                variables.Add(variable.Name, variable);
                continue;
            }
            if (known.Offset != variable.Offset || known.Size != variable.Size)
                errors.Add($"'{variable.Name}' in constant buffer '{added.Name}' has a different layout in the vertex and pixel shaders.");
        }
        return [.. variables.Values.OrderBy(variable => variable.Offset)];
    }

    internal static char RegisterType(D3D12ShaderResourceKind kind) => kind switch
    {
        D3D12ShaderResourceKind.Sampler => 's',
        D3D12ShaderResourceKind.ReadWriteTexture or D3D12ShaderResourceKind.ReadWriteStructuredBuffer or D3D12ShaderResourceKind.ReadWriteByteAddressBuffer => 'u',
        _ => 't',
    };
}
