using System.Text;

namespace Crystal.ImGui.Generator;

internal static class Names
{
    private static readonly HashSet<string> Keywords = new(StringComparer.Ordinal)
    {
        "abstract", "as", "base", "bool", "break", "byte", "case", "catch", "char", "checked", "class", "const", "continue",
        "decimal", "default", "delegate", "do", "double", "else", "enum", "event", "explicit", "extern", "false", "finally",
        "fixed", "float", "for", "foreach", "goto", "if", "implicit", "in", "int", "interface", "internal", "is", "lock",
        "long", "namespace", "new", "null", "object", "operator", "out", "override", "params", "private", "protected",
        "public", "readonly", "ref", "return", "sbyte", "sealed", "short", "sizeof", "stackalloc", "static", "string",
        "struct", "switch", "this", "throw", "true", "try", "typeof", "uint", "ulong", "unchecked", "unsafe", "ushort",
        "using", "virtual", "void", "volatile", "while",
    };

    public static string Identifier(string name) => Keywords.Contains(name) ? "@" + name : name;

    public static string FieldName(string cName)
    {
        var bracket = cName.IndexOf('[');
        return bracket < 0 ? cName : cName[..bracket];
    }

    public static string Parameter(string name)
    {
        var pascal = Method(name.TrimStart('_'));
        return pascal.Length == 0 ? name : char.ToLowerInvariant(pascal[0]) + pascal[1..];
    }

    public static string Method(string name)
    {
        var leading = name.Length - name.TrimStart('_').Length;
        var builder = new StringBuilder(name[..leading]);
        var upper = true;
        foreach (var character in name[leading..])
        {
            if (character == '_')
            {
                upper = true;
                continue;
            }
            builder.Append(upper ? char.ToUpperInvariant(character) : character);
            upper = false;
        }
        return builder.ToString();
    }

    public static string EnumMember(string enumName, string memberName)
    {
        string name;
        if (memberName.StartsWith(enumName + "_", StringComparison.Ordinal)) name = memberName[(enumName.Length + 1)..];
        else if (memberName.StartsWith("ImGui", StringComparison.Ordinal)) name = memberName["ImGui".Length..].Replace("_", "", StringComparison.Ordinal);
        else if (memberName.StartsWith("Im", StringComparison.Ordinal)) name = memberName["Im".Length..].Replace("_", "", StringComparison.Ordinal);
        else name = memberName;

        name = name.TrimEnd('_');
        if (name.Length > 0 && char.IsDigit(name[0])) name = "D" + name;
        return Identifier(name);
    }
}
