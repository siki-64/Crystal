namespace Crystal.ShaderCompiler;

internal sealed record ShaderDiagnostic(string File, int Line, int Column, bool IsError, string Code, string Message)
{
    public override string ToString() => $"{File}({Line},{Column}): {(IsError ? "error" : "warning")} {Code}: {Message}";
}
