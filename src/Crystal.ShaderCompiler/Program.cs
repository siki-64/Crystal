using Crystal.Graphics.D3D12;

namespace Crystal.ShaderCompiler;

internal static class Program
{
    private static int Main(string[] arguments)
    {
        CompilerOptions options;
        try
        {
            options = CompilerOptions.Parse(arguments);
        }
        catch (Exception exception) when (exception is ArgumentException or IOException)
        {
            Console.Error.WriteLine($"Crystal.ShaderCompiler: error CRYSTAL1003: {exception.Message}");
            return 1;
        }

        var results = new (ShaderFile File, D3D12ShaderLibraryFile? Library, List<ShaderDiagnostic> Diagnostics)[options.Files.Count];
        using var compilers = new ThreadLocal<DxcShaderCompiler>(() => new DxcShaderCompiler(), trackAllValues: true);
        try
        {
            Parallel.For(0, options.Files.Count, index =>
            {
                var file = options.Files[index];
                var diagnostics = new List<ShaderDiagnostic>();
                D3D12ShaderLibraryFile? library = null;
                try
                {
                    library = ShaderLibraryBuilder.Build(file, options, compilers.Value!, diagnostics);
                }
                catch (IOException exception)
                {
                    diagnostics.Add(new ShaderDiagnostic(file.SourcePath, 1, 1, IsError: true, "CRYSTAL1003", exception.Message));
                }
                results[index] = (file, library, diagnostics);
            });
        }
        finally
        {
            foreach (var compiler in compilers.Values) compiler.Dispose();
        }

        var failed = false;
        foreach (var (file, library, diagnostics) in results)
        {
            foreach (var diagnostic in diagnostics) Console.WriteLine(diagnostic);
            if (library == null)
            {
                failed = true;
                continue;
            }

            Directory.CreateDirectory(Path.GetDirectoryName(file.OutputPath)!);
            var temporaryPath = file.OutputPath + ".tmp";
            using (var stream = File.Create(temporaryPath)) library.Write(stream);
            File.Move(temporaryPath, file.OutputPath, overwrite: true);
        }
        return failed ? 1 : 0;
    }
}
