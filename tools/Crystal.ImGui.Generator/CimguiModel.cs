using System.Globalization;
using System.Text.Json;

namespace Crystal.ImGui.Generator;

internal sealed record CimguiArgument(string Name, string Type);

internal sealed record CimguiFunction(
    string NativeName,
    string FunctionName,
    string StructName,
    string CArguments,
    string CCallArguments,
    IReadOnlyList<CimguiArgument> Arguments,
    string ReturnType,
    IReadOnlyDictionary<string, string> Defaults,
    bool IsConstructor,
    bool IsDestructor,
    bool IsVarArgs,
    bool IsTemplated,
    bool IsStatic);

internal sealed record CimguiEnumMember(string Name, long Value);

internal sealed record CimguiEnum(string Name, string UnderlyingType, IReadOnlyList<CimguiEnumMember> Members);

internal sealed record CimguiField(string Name, string Type, string? TemplateType, int ArraySize, int BitfieldWidth);

internal sealed record CimguiStruct(string Name, IReadOnlyList<CimguiField> Fields);

internal sealed class CimguiModel
{
    public required IReadOnlyList<CimguiFunction> Functions { get; init; }
    public required IReadOnlyList<CimguiEnum> Enums { get; init; }
    public required IReadOnlyList<CimguiStruct> Structs { get; init; }
    public required IReadOnlyDictionary<string, string> Typedefs { get; init; }

    public static CimguiModel Load(string apiDirectory)
    {
        using var definitions = JsonDocument.Parse(File.ReadAllText(Path.Combine(apiDirectory, "definitions.json")));
        using var structsAndEnums = JsonDocument.Parse(File.ReadAllText(Path.Combine(apiDirectory, "structs_and_enums.json")));
        using var typedefs = JsonDocument.Parse(File.ReadAllText(Path.Combine(apiDirectory, "typedefs_dict.json")));

        var typedefMap = typedefs.RootElement.EnumerateObject().ToDictionary(property => property.Name, property => property.Value.GetString()!.Trim());

        return new CimguiModel
        {
            Functions = ReadFunctions(definitions.RootElement),
            Enums = ReadEnums(structsAndEnums.RootElement, typedefMap),
            Structs = ReadStructs(structsAndEnums.RootElement),
            Typedefs = typedefMap,
        };
    }

    private static List<CimguiFunction> ReadFunctions(JsonElement root)
    {
        var functions = new List<CimguiFunction>();
        foreach (var overloads in root.EnumerateObject())
        {
            foreach (var definition in overloads.Value.EnumerateArray())
            {
                var structName = definition.GetProperty("stname").GetString()!;
                var isConstructor = definition.TryGetProperty("constructor", out var constructor) && constructor.GetBoolean();
                var returnType = definition.TryGetProperty("ret", out var ret) ? ret.GetString()! : isConstructor ? structName + "*" : "void";
                var arguments = definition.GetProperty("argsT").EnumerateArray()
                    .Select(argument => new CimguiArgument(argument.GetProperty("name").GetString()!, argument.GetProperty("type").GetString()!.Trim()))
                    .ToList();
                var defaults = definition.GetProperty("defaults").ValueKind == JsonValueKind.Object
                    ? definition.GetProperty("defaults").EnumerateObject().ToDictionary(property => property.Name, property => property.Value.GetString()!)
                    : [];

                functions.Add(new CimguiFunction(
                    NativeName: definition.GetProperty("ov_cimguiname").GetString()!,
                    FunctionName: definition.TryGetProperty("funcname", out var functionName) ? functionName.GetString()! : "",
                    StructName: structName,
                    CArguments: definition.GetProperty("args").GetString()!,
                    CCallArguments: definition.GetProperty("call_args").GetString()!,
                    Arguments: arguments,
                    ReturnType: returnType,
                    Defaults: defaults,
                    IsConstructor: isConstructor,
                    IsDestructor: definition.TryGetProperty("destructor", out var destructor) && destructor.GetBoolean(),
                    IsVarArgs: definition.TryGetProperty("isvararg", out var isVarArgs) && isVarArgs.ValueKind != JsonValueKind.Null,
                    IsTemplated: definition.TryGetProperty("templated", out var templated) && templated.GetBoolean(),
                    IsStatic: definition.TryGetProperty("is_static_function", out var isStatic) && isStatic.GetBoolean()));
            }
        }
        return functions;
    }

    private static List<CimguiEnum> ReadEnums(JsonElement root, IReadOnlyDictionary<string, string> typedefs)
    {
        var enumTypes = root.GetProperty("enumtypes").EnumerateObject().ToDictionary(property => property.Name, property => property.Value.GetString()!);
        var enums = new List<CimguiEnum>();
        foreach (var enumeration in root.GetProperty("enums").EnumerateObject())
        {
            var name = enumeration.Name.TrimEnd('_');
            var underlyingType = enumTypes.TryGetValue(name, out var enumType) ? enumType
                : typedefs.TryGetValue(name, out var typedef) ? typedef
                : "int";
            var members = enumeration.Value.EnumerateArray()
                .Select(member => new CimguiEnumMember(member.GetProperty("name").GetString()!, member.GetProperty("calc_value").GetInt64()))
                .ToList();
            enums.Add(new CimguiEnum(name, underlyingType, members));
        }
        return enums;
    }

    private static List<CimguiStruct> ReadStructs(JsonElement root)
    {
        var structs = new List<CimguiStruct>();
        foreach (var structure in root.GetProperty("structs").EnumerateObject())
        {
            var fields = structure.Value.EnumerateArray().Select(field => new CimguiField(
                Name: field.GetProperty("name").GetString()!,
                Type: field.GetProperty("type").GetString()!.Trim(),
                TemplateType: field.TryGetProperty("template_type", out var templateType) ? templateType.GetString() : null,
                ArraySize: field.TryGetProperty("size", out var size) ? size.GetInt32() : 0,
                BitfieldWidth: field.TryGetProperty("bitfield", out var bitfield) ? int.Parse(bitfield.GetString()!, CultureInfo.InvariantCulture) : 0)).ToList();
            structs.Add(new CimguiStruct(structure.Name, fields));
        }
        return structs;
    }
}
