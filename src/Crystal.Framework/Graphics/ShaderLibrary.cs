using System.Diagnostics.CodeAnalysis;
using Crystal.Graphics.D3D12;

namespace Crystal.Graphics;

public sealed class ShaderLibrary : GraphicsResource
{
    private readonly ShaderProgram[] _programs;
    private readonly Dictionary<string, ShaderProgram> _programsByName = [];

    public ShaderLibrary(GraphicsDevice graphicsDevice, Stream stream, string? name = null)
        : this(graphicsDevice, D3D12ShaderLibraryFile.Read(stream ?? throw new ArgumentNullException(nameof(stream))), name)
    {
    }

    public ShaderLibrary(GraphicsDevice graphicsDevice, ReadOnlySpan<ShaderProgramDescription> programs, string? name = null)
        : this(graphicsDevice, D3D12ShaderProgramDescriptions.ToLibraryFile(programs), name)
    {
    }

    private ShaderLibrary(GraphicsDevice graphicsDevice, D3D12ShaderLibraryFile file, string? name)
        : base(graphicsDevice, name)
    {
        _programs = new ShaderProgram[file.Programs.Length];
        try
        {
            for (var index = 0; index < _programs.Length; index++)
            {
                var info = file.Programs[index];
                var program = new ShaderProgram(this, info, file.Shaders);
                _programs[index] = program;
                _programsByName.Add(program.Name, program);
            }
        }
        catch
        {
            foreach (var program in _programs) program?.ReleaseRootSignature();
            throw;
        }
        graphicsDevice.Track(this);
    }

    public IReadOnlyList<ShaderProgram> Programs => _programs;

    public static ShaderLibrary Load(GraphicsDevice graphicsDevice, string path)
    {
        ArgumentException.ThrowIfNullOrEmpty(path);
        var fullPath = Path.IsPathRooted(path) ? path : Path.Combine(AppContext.BaseDirectory, path);
        using var stream = File.OpenRead(fullPath);
        return new ShaderLibrary(graphicsDevice, stream, Path.GetFileNameWithoutExtension(path));
    }

    public ShaderProgram GetProgram(string name)
    {
        if (TryGetProgram(name, out var program)) return program;
        throw new KeyNotFoundException($"Shader library '{Name}' has no program '{name}'. It has: {string.Join(", ", _programsByName.Keys)}.");
    }

    public bool TryGetProgram(string name, [NotNullWhen(true)] out ShaderProgram? program)
    {
        ArgumentNullException.ThrowIfNull(name);
        return _programsByName.TryGetValue(name, out program);
    }

    private protected override void ReleaseNativeResources()
    {
        foreach (var program in _programs) program.ReleaseRootSignature();
    }
}
