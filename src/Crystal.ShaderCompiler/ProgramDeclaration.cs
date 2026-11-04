using System.Text.RegularExpressions;

namespace Crystal.ShaderCompiler;

internal sealed record ProgramDeclaration(string Name, string? VertexEntryPoint, string? PixelEntryPoint, string? ComputeEntryPoint, int Line);

internal static partial class ProgramDeclarationParser
{
    public static List<ProgramDeclaration> Parse(string path, string source, List<ShaderDiagnostic> diagnostics)
    {
        var declarations = new List<ProgramDeclaration>();
        var lines = source.Split('\n');
        for (var index = 0; index < lines.Length; index++)
        {
            var match = PragmaPattern().Match(lines[index].TrimEnd('\r'));
            if (!match.Success) continue;

            var lineNumber = index + 1;
            var tokens = match.Groups["arguments"].Value.Split((char[]?)null, StringSplitOptions.RemoveEmptyEntries);
            if (tokens.Length == 0 || !IdentifierPattern().IsMatch(tokens[0]))
            {
                diagnostics.Add(Error(path, lineNumber, "Expected '#pragma program <Name> vertex=<EntryPoint> [pixel=<EntryPoint>]' or '#pragma program <Name> compute=<EntryPoint>'."));
                continue;
            }

            string? vertex = null;
            string? pixel = null;
            string? compute = null;
            var isValid = true;
            foreach (var token in tokens.AsSpan(1))
            {
                var separator = token.IndexOf('=');
                var key = separator < 0 ? token : token[..separator];
                var value = separator < 0 ? "" : token[(separator + 1)..];
                if (!IdentifierPattern().IsMatch(value))
                {
                    diagnostics.Add(Error(path, lineNumber, $"'{token}' must have the form key=EntryPoint."));
                    isValid = false;
                    continue;
                }
                switch (key)
                {
                    case "vertex":
                        vertex = value;
                        break;
                    case "pixel":
                        pixel = value;
                        break;
                    case "compute":
                        compute = value;
                        break;
                    default:
                        diagnostics.Add(Error(path, lineNumber, $"Unknown program stage '{key}'. Use vertex=, pixel= or compute=."));
                        isValid = false;
                        break;
                }
            }

            if (compute != null && (vertex != null || pixel != null))
            {
                diagnostics.Add(Error(path, lineNumber, $"Program '{tokens[0]}' mixes compute= with vertex= or pixel=. Declare compute programs separately."));
                continue;
            }
            if (vertex == null && compute == null)
            {
                diagnostics.Add(Error(path, lineNumber, $"Program '{tokens[0]}' needs a vertex= or compute= entry point."));
                continue;
            }
            if (declarations.Exists(existing => existing.Name == tokens[0]))
            {
                diagnostics.Add(Error(path, lineNumber, $"Program '{tokens[0]}' is declared twice."));
                continue;
            }
            if (isValid) declarations.Add(new ProgramDeclaration(tokens[0], vertex, pixel, compute, lineNumber));
        }
        return declarations;
    }

    private static ShaderDiagnostic Error(string path, int line, string message) => new(path, line, 1, IsError: true, "CRYSTAL1001", message);

    [GeneratedRegex(@"^\s*#\s*pragma\s+program(?<arguments>(\s.*)?)$")]
    private static partial Regex PragmaPattern();

    [GeneratedRegex(@"^[A-Za-z_][A-Za-z0-9_]*$")]
    private static partial Regex IdentifierPattern();
}
