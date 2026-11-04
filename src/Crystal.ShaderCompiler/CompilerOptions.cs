using System.Text.RegularExpressions;

namespace Crystal.ShaderCompiler;

internal sealed record ShaderFile(string SourcePath, string OutputPath);

internal sealed partial record CompilerOptions(
    string ShaderModel, bool DebugInfo, bool WarningsAsErrors, List<string> IncludeDirectories, List<string> Defines, List<ShaderFile> Files, bool ColumnMajorMatrices = false)
{
    public static CompilerOptions Parse(IEnumerable<string> arguments)
    {
        var shaderModel = "6_6";
        var debugInfo = false;
        var warningsAsErrors = false;
        var includeDirectories = new List<string>();
        var defines = new List<string>();
        var files = new List<ShaderFile>();

        foreach (var argument in Expand(arguments))
        {
            var separator = argument.IndexOf(' ');
            var option = separator < 0 ? argument : argument[..separator];
            var value = separator < 0 ? "" : argument[(separator + 1)..].Trim();
            switch (option)
            {
                case "--shader-model":
                    if (!ShaderModelPattern().IsMatch(value)) throw new ArgumentException($"Shader model '{value}' must be 6_6 or later, like 6_6. Programs read their resources through descriptor indices, which needs shader model 6.6.");
                    shaderModel = value;
                    break;
                case "--debug-info":
                    debugInfo = true;
                    break;
                case "--warnings-as-errors":
                    warningsAsErrors = true;
                    break;
                case "--include":
                    includeDirectories.Add(value);
                    break;
                case "--define":
                    defines.Add(value);
                    break;
                case "--compile":
                    var paths = value.Split('|');
                    if (paths.Length != 2) throw new ArgumentException($"'--compile {value}' must be '--compile <source>|<output>'.");
                    files.Add(new ShaderFile(Path.GetFullPath(paths[0]), Path.GetFullPath(paths[1])));
                    break;
                default:
                    throw new ArgumentException($"Unknown option '{option}'.");
            }
        }
        return new CompilerOptions(shaderModel, debugInfo, warningsAsErrors, includeDirectories, defines, files);
    }

    private static IEnumerable<string> Expand(IEnumerable<string> arguments)
    {
        foreach (var argument in arguments)
        {
            if (!argument.StartsWith('@'))
            {
                yield return argument;
                continue;
            }
            foreach (var line in File.ReadAllLines(argument[1..]))
            {
                if (!string.IsNullOrWhiteSpace(line)) yield return line.Trim();
            }
        }
    }

    [GeneratedRegex(@"^6_[6-9]$")]
    private static partial Regex ShaderModelPattern();
}
