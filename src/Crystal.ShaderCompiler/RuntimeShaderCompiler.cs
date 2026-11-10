using System.Text;

namespace Crystal.ShaderCompiler;

public sealed record ShaderCompilation(byte[]? Library, IReadOnlyList<string> Errors, IReadOnlyList<string> Warnings)
{
    public bool Succeeded => Library != null;
}

public sealed record ShaderDescriptorSlot(string Name, char RegisterClass, int Register, int Space, int Slot);

public sealed record ShaderStageCompilation(byte[]? Bytecode, IReadOnlyList<ShaderDescriptorSlot> DescriptorSlots, IReadOnlyList<string> Errors, IReadOnlyList<string> Warnings)
{
    public bool Succeeded => Bytecode != null;
}

public sealed class RuntimeShaderCompiler : IDisposable
{
    private readonly DxcShaderCompiler _compiler = new();
    private readonly CompilerOptions _options;

    public RuntimeShaderCompiler(IEnumerable<string>? includeDirectories = null, IEnumerable<string>? defines = null, bool columnMajorMatrices = false)
    {
        _options = new CompilerOptions("6_6", DebugInfo: false, WarningsAsErrors: false, [.. includeDirectories ?? []], [.. defines ?? []], [], columnMajorMatrices);
    }

    public static string Version { get; } = typeof(RuntimeShaderCompiler).Assembly.ManifestModule.ModuleVersionId.ToString("N");

    public ShaderCompilation Compile(string sourceName, string source)
    {
        ArgumentException.ThrowIfNullOrEmpty(sourceName);
        ArgumentNullException.ThrowIfNull(source);
        var diagnostics = new List<ShaderDiagnostic>();
        var library = ShaderLibraryBuilder.Build(sourceName, Encoding.UTF8.GetBytes(source), _options, _compiler, diagnostics);
        byte[]? bytes = null;
        if (library != null)
        {
            using var stream = new MemoryStream();
            library.Write(stream);
            bytes = stream.ToArray();
        }
        return new ShaderCompilation(
            bytes,
            [.. diagnostics.Where(diagnostic => diagnostic.IsError).Select(diagnostic => diagnostic.ToString())],
            [.. diagnostics.Where(diagnostic => !diagnostic.IsError).Select(diagnostic => diagnostic.ToString())]);
    }

    public ShaderStageCompilation CompileStage(string sourceName, string source, string entryPoint, ShaderStage stage)
    {
        ArgumentException.ThrowIfNullOrEmpty(sourceName);
        ArgumentNullException.ThrowIfNull(source);
        ArgumentException.ThrowIfNullOrEmpty(entryPoint);
        var diagnostics = new List<ShaderDiagnostic>();
        var sourceBytes = Encoding.UTF8.GetBytes(source);
        var compiled = _compiler.Compile(sourceName, sourceBytes, entryPoint, stage, _options, diagnostics);
        byte[]? bytecode = null;
        var slots = new List<ShaderDescriptorSlot>();
        if (compiled != null)
        {
            List<string> problems = [.. compiled.Reflection.Problems];
            var constantBufferRegisterLimit = stage == ShaderStage.Compute
                ? ShaderLibraryBuilder.ComputeConstantBufferRegisterLimit
                : ShaderLibraryBuilder.GraphicsConstantBufferRegisterLimit;
            foreach (var buffer in compiled.Reflection.ConstantBuffers)
                if (buffer.Space != 0 || buffer.Register >= constantBufferRegisterLimit)
                    problems.Add($"Constant buffer {buffer.Name} is at b{buffer.Register}, space{buffer.Space}; constant buffers must be in space0 below b{constantBufferRegisterLimit}.");

            var resources = compiled.Reflection.Resources;
            bytecode = compiled.Bytecode;
            if (resources.Count > 0 && problems.Count == 0)
            {
                var rewritten = _compiler.Preprocess(sourceName, sourceBytes, _options, diagnostics) is { } preprocessed
                    ? DescriptorIndexRewriter.Rewrite(preprocessed, resources, stage, problems)
                    : null;
                bytecode = rewritten == null ? null : _compiler.Compile(sourceName, Encoding.UTF8.GetBytes(rewritten), entryPoint, stage, _options, diagnostics)?.Bytecode;
            }
            for (var slot = 0; slot < resources.Count; slot++)
                slots.Add(new ShaderDescriptorSlot(resources[slot].Name, ShaderLibraryBuilder.RegisterType(resources[slot].Kind), resources[slot].Register, resources[slot].Space, slot));
            foreach (var problem in problems) diagnostics.Add(new ShaderDiagnostic(sourceName, 1, 1, IsError: true, "CRYSTAL1002", problem));
            if (problems.Count > 0) bytecode = null;
        }
        return new ShaderStageCompilation(
            bytecode,
            bytecode == null ? [] : slots,
            [.. diagnostics.Where(diagnostic => diagnostic.IsError).Select(diagnostic => diagnostic.ToString())],
            [.. diagnostics.Where(diagnostic => !diagnostic.IsError).Select(diagnostic => diagnostic.ToString())]);
    }

    public void Dispose() => _compiler.Dispose();
}
