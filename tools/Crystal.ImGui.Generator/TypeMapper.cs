using System.Globalization;
using System.Text.RegularExpressions;

namespace Crystal.ImGui.Generator;

internal sealed partial class TypeMapper
{
    private static readonly Dictionary<string, string> Primitives = new()
    {
        ["void"] = "void",
        ["bool"] = "bool",
        ["char"] = "byte",
        ["signed char"] = "sbyte",
        ["unsigned char"] = "byte",
        ["short"] = "short",
        ["signed short"] = "short",
        ["unsigned short"] = "ushort",
        ["int"] = "int",
        ["signed int"] = "int",
        ["unsigned int"] = "uint",
        ["long long"] = "long",
        ["signed long long"] = "long",
        ["unsigned long long"] = "ulong",
        ["float"] = "float",
        ["double"] = "double",
        ["size_t"] = "nuint",
        ["ImVec2"] = "Vector2",
        ["ImVec2_c"] = "Vector2",
        ["ImVec4"] = "Vector4",
        ["ImVec4_c"] = "Vector4",
        ["ImColor_c"] = "ImColor",
        ["ImTextureRef_c"] = "ImTextureRef",
    };

    private readonly HashSet<string> _enumNames = [];
    private readonly HashSet<string> _structNames = [];
    private readonly IReadOnlyDictionary<string, string> _typedefs;
    private readonly Dictionary<string, (string Name, string ElementType, int Capacity)> _smallStacks = [];

    public TypeMapper(CimguiModel model)
    {
        _typedefs = model.Typedefs;
        foreach (var enumeration in model.Enums) _enumNames.Add(enumeration.Name);
        foreach (var structure in model.Structs) _structNames.Add(structure.Name);
        foreach (var typedef in model.Typedefs)
        {
            if (typedef.Value.StartsWith("struct ", StringComparison.Ordinal) && !_structNames.Contains(typedef.Key)) OpaqueStructs.Add(typedef.Key);
        }
    }

    public SortedSet<string> OpaqueStructs { get; } = new(StringComparer.Ordinal);

    public IEnumerable<(string Name, string ElementType, int Capacity)> SmallStacks => _smallStacks.Values;

    public bool IsStruct(string name) => _structNames.Contains(name) && !Primitives.ContainsKey(name);

    public bool IsEnum(string name) => _enumNames.Contains(name);

    public string Map(string cType)
    {
        var type = StripConst(cType);

        if (type.Contains("(*)", StringComparison.Ordinal)) return "void*";
        if (type is "const char* const[]" or "char* const[]") return "byte**";
        var arrayMatch = ArrayPattern().Match(type);
        if (arrayMatch.Success) return Map(arrayMatch.Groups[1].Value) + "*";

        var pointerDepth = 0;
        while (type.EndsWith('*'))
        {
            pointerDepth++;
            type = StripConst(type[..^1]);
        }
        return MapBase(type) + new string('*', pointerDepth);
    }

    public string MapBase(string type)
    {
        type = StripConst(type);
        if (Primitives.TryGetValue(type, out var primitive)) return primitive;
        if (_enumNames.Contains(type)) return type;
        if (_structNames.Contains(type) || OpaqueStructs.Contains(type)) return type;
        if (type.StartsWith("ImVector_", StringComparison.Ordinal)) return MapVector(type["ImVector_".Length..]);
        var smallStack = SmallStackPattern().Match(type);
        if (smallStack.Success) return SmallStackName(smallStack.Groups[1].Value, int.Parse(smallStack.Groups[2].Value, CultureInfo.InvariantCulture));
        if (_typedefs.TryGetValue(type, out var target)) return target.Contains("(*)", StringComparison.Ordinal) ? "void*" : MapBase(target.TrimEnd(';'));
        throw new InvalidOperationException($"No C# type for C type '{type}'.");
    }

    public string MapVector(string element)
    {
        element = StripConst(element);
        if (element.EndsWith('*')) return $"ImPtrVector<{MapBase(element[..^1])}>";
        if (element.EndsWith("Ptr", StringComparison.Ordinal) && (_structNames.Contains(element[..^3]) || OpaqueStructs.Contains(element[..^3])))
            return $"ImPtrVector<{MapBase(element[..^3])}>";
        return $"ImVector<{MapBase(element)}>";
    }

    public static string StripConst(string type)
    {
        type = WhitespacePattern().Replace(type.Trim(), " ");
        if (type.StartsWith("const ", StringComparison.Ordinal)) type = type["const ".Length..].Trim();
        if (type.EndsWith(" const", StringComparison.Ordinal)) type = type[..^" const".Length].Trim();
        return type;
    }

    public static bool IsConst(string type) => type.Trim().StartsWith("const ", StringComparison.Ordinal);

    private string SmallStackName(string element, int capacity)
    {
        var name = $"ImSmallStack{element}{capacity}";
        _smallStacks[name] = (name, MapBase(element), capacity);
        return name;
    }

    [GeneratedRegex(@"^(.+?)\s*\[(\d+)\]$")]
    private static partial Regex ArrayPattern();

    [GeneratedRegex(@"^ImSmallStack__(\w+?)__(\d+)$")]
    private static partial Regex SmallStackPattern();

    [GeneratedRegex(@"\s+")]
    private static partial Regex WhitespacePattern();
}
