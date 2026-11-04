using System.Text;

namespace Crystal.ImGui.Generator;

internal sealed class CodeWriter
{
    private readonly StringBuilder _builder = new();
    private int _indent;

    public void Line(string text = "")
    {
        if (text.Length == 0)
        {
            _builder.Append('\n');
            return;
        }
        _builder.Append(' ', _indent * 4).Append(text).Append('\n');
    }

    public void Open(string text)
    {
        Line(text);
        OpenBlock();
    }

    public void OpenBlock()
    {
        Line("{");
        _indent++;
    }

    public void Close(string suffix = "")
    {
        _indent--;
        Line("}" + suffix);
    }

    public void Usings(params string[] namespaces)
    {
        foreach (var name in namespaces) Line($"using {name};");
        if (namespaces.Length > 0) Line();
    }

    public void WriteTo(string path)
    {
        Directory.CreateDirectory(Path.GetDirectoryName(path)!);
        File.WriteAllText(path, _builder.ToString());
    }
}
