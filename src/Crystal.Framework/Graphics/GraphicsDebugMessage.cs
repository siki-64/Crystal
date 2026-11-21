using static Crystal.Interop.D3D12_MESSAGE_SEVERITY;

namespace Crystal.Graphics;

public enum GraphicsDebugSeverity
{
    Corruption = (int)D3D12_MESSAGE_SEVERITY_CORRUPTION,
    Error = (int)D3D12_MESSAGE_SEVERITY_ERROR,
    Warning = (int)D3D12_MESSAGE_SEVERITY_WARNING,
    Info = (int)D3D12_MESSAGE_SEVERITY_INFO,
    Message = (int)D3D12_MESSAGE_SEVERITY_MESSAGE,
}

public sealed record GraphicsDebugMessage(GraphicsDebugSeverity Severity, int Id, string Text)
{
    public bool IsError => Severity is GraphicsDebugSeverity.Corruption or GraphicsDebugSeverity.Error;

    public override string ToString() => $"D3D12 {Severity} #{Id}: {Text}";
}

public sealed class GraphicsDebugException(IReadOnlyList<GraphicsDebugMessage> errors)
    : Exception(string.Join(Environment.NewLine, errors))
{
    public IReadOnlyList<GraphicsDebugMessage> Errors { get; } = errors;
}

public sealed class GraphicsException(string message) : Exception(message);
