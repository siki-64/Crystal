using System.Globalization;
using System.Text.RegularExpressions;

namespace Crystal.ImGui.Generator;

internal sealed class FriendlyArgument
{
    public required string NativeName { get; init; }
    public string? ParameterType { get; init; }
    public string? ParameterName { get; init; }
    public string? Prologue { get; init; }
    public string? Fixed { get; init; }
    public required string NativeExpression { get; init; }
    public string? OmittedExpression { get; init; }
}

internal sealed partial class FunctionsWriter
{
    public const string ShimPrefix = "CRYSTAL_";

    private static readonly HashSet<string> TemplatedStructs = ["ImVector", "ImSmallStack_"];

    private readonly CimguiModel _model;
    private readonly TypeMapper _mapper;
    private readonly List<CimguiFunction> _functions = [];
    private readonly List<CimguiFunction> _varArgsFunctions = [];

    public FunctionsWriter(CimguiModel model, TypeMapper mapper)
    {
        _model = model;
        _mapper = mapper;

        var varArgsNames = model.Functions.Where(function => function.IsVarArgs).Select(function => function.NativeName).ToHashSet();
        foreach (var function in model.Functions)
        {
            if (function.NativeName.EndsWith('0') && varArgsNames.Contains(function.NativeName[..^1]))
            {
                Skipped.Add($"{function.NativeName}: passes text as a format string, the {ShimPrefix} shim replaces it");
                continue;
            }
            if (function.NativeName.StartsWith("ImGuiFreeType_", StringComparison.Ordinal))
            {
                Skipped.Add($"{function.NativeName}: the native library is built without FreeType");
                continue;
            }
            if (!IsBindable(function))
            {
                Skipped.Add($"{function.NativeName}: templated or va_list");
                continue;
            }
            if (function.IsVarArgs)
            {
                _varArgsFunctions.Add(function);
                _functions.Add(TextShim(function));
                continue;
            }
            _functions.Add(function);
        }
    }

    public List<string> Skipped { get; } = [];

    public void WriteNative(string path)
    {
        var writer = new CodeWriter();
        writer.Usings("System.Numerics", "System.Runtime.InteropServices");
        writer.Line(Program.Namespace);
        writer.Line();
        writer.Open("public static unsafe partial class ImGuiNative");
        var first = true;
        foreach (var function in _functions)
        {
            if (!first) writer.Line();
            first = false;
            var parameters = function.Arguments.Select(argument => $"{BoolMarshalling(_mapper.Map(argument.Type))}{_mapper.Map(argument.Type)} {Names.Identifier(argument.Name)}");
            writer.Line("[LibraryImport(LibraryName)]");
            if (_mapper.Map(function.ReturnType) == "bool") writer.Line("[return: MarshalAs(UnmanagedType.U1)]");
            writer.Line($"public static partial {_mapper.Map(function.ReturnType)} {function.NativeName}({string.Join(", ", parameters)});");
        }
        writer.Close();
        writer.WriteTo(path);
    }

    public void WriteTextShims(CodeWriter writer)
    {
        foreach (var function in _varArgsFunctions)
        {
            var parameters = FmtPattern().Replace(function.CArguments.Replace(",...)", ")", StringComparison.Ordinal), "text");
            var call = FmtPattern().Replace(function.CCallArguments.Replace(",...)", ")", StringComparison.Ordinal), "\"%s\",text");
            var returnKeyword = function.ReturnType == "void" ? "" : "return ";
            writer.Line($"CIMGUI_API {function.ReturnType} {ShimPrefix}{function.NativeName}{parameters}");
            writer.Line("{");
            writer.Line($"    {returnKeyword}{function.NativeName}{call};");
            writer.Line("}");
            writer.Line();
        }
    }

    public void WriteFriendly(string imguiPath, string pointersPath)
    {
        var imgui = new CodeWriter();
        imgui.Usings("System.Numerics");
        imgui.Line(Program.Namespace);
        imgui.Line();
        imgui.Open("public static unsafe partial class ImGui");
        var imguiSignatures = new HashSet<string>();
        var first = true;
        foreach (var function in _functions.Where(function => function.StructName.Length == 0))
            WriteOverloads(imgui, function, isInstance: false, imguiSignatures, ref first);
        imgui.Close();
        imgui.WriteTo(imguiPath);

        var pointers = new CodeWriter();
        pointers.Usings("System.Numerics");
        pointers.Line(Program.Namespace);
        foreach (var structure in _model.Structs.OrderBy(structure => structure.Name, StringComparer.Ordinal))
        {
            if (!TypesWriter.MappedStructs.Contains(structure.Name)) WritePointer(pointers, structure);
        }
        pointers.WriteTo(pointersPath);
    }

    private static string BoolMarshalling(string csharpType) => csharpType == "bool" ? "[MarshalAs(UnmanagedType.U1)] " : "";

    private static bool IsBindable(CimguiFunction function)
    {
        if (function.IsTemplated || TemplatedStructs.Contains(function.StructName)) return false;
        if (function.ReturnType.Contains("T*", StringComparison.Ordinal) || function.ReturnType == "T") return false;
        foreach (var argument in function.Arguments)
        {
            var type = TypeMapper.StripConst(argument.Type);
            if (type is "va_list" or "T" or "ImVector*" || type.Contains("ImVector_T", StringComparison.Ordinal) || type.StartsWith("ImSmallStack_", StringComparison.Ordinal)) return false;
        }
        return true;
    }

    private static CimguiFunction TextShim(CimguiFunction function) => function with
    {
        NativeName = ShimPrefix + function.NativeName,
        Arguments = function.Arguments.Where(argument => argument.Type != "...").Select(argument => argument.Name == "fmt" ? argument with { Name = "text" } : argument).ToList(),
        IsVarArgs = false,
    };

    private void WritePointer(CodeWriter writer, CimguiStruct structure)
    {
        var pointerName = structure.Name + "Ptr";
        writer.Line();
        writer.Open($"public readonly unsafe partial struct {pointerName}");
        writer.Line($"public readonly {structure.Name}* Handle;");
        writer.Line();
        writer.Line($"public {pointerName}({structure.Name}* handle) => Handle = handle;");
        writer.Line();
        writer.Line("public bool IsNull => Handle == null;");
        writer.Line();
        writer.Line($"public static implicit operator {pointerName}({structure.Name}* handle) => new(handle);");
        writer.Line();
        writer.Line($"public static implicit operator {structure.Name}*({pointerName} pointer) => pointer.Handle;");

        var memberNames = new HashSet<string> { "Handle", "IsNull" };
        foreach (var field in structure.Fields)
        {
            if (field.Type.StartsWith("union", StringComparison.Ordinal))
            {
                foreach (Match member in UnionNamePattern().Matches(field.Type))
                {
                    var unionMember = Names.Identifier(member.Groups[1].Value);
                    memberNames.Add(unionMember);
                    var memberType = member.Groups[0].Value[..member.Groups[0].Value.LastIndexOf(member.Groups[1].Value, StringComparison.Ordinal)].Trim();
                    writer.Line();
                    writer.Line($"public ref {_mapper.Map(memberType)} {unionMember} => ref Handle->{unionMember};");
                }
                continue;
            }

            var name = Names.Identifier(Names.FieldName(field.Name));
            memberNames.Add(name);
            writer.Line();
            if (field.BitfieldWidth > 0)
            {
                writer.Open($"public uint {name}");
                writer.Line($"get => Handle->{name};");
                writer.Line($"set => Handle->{name} = value;");
                writer.Close();
            }
            else if (StructPointerTarget(field) is { } target)
            {
                writer.Open($"public {target}Ptr {name}");
                writer.Line($"get => Handle->{name};");
                writer.Line($"set => Handle->{name} = value;");
                writer.Close();
            }
            else
            {
                writer.Line($"public ref {FieldTypeOf(field)} {name} => ref Handle->{name};");
            }
        }

        var signatures = new HashSet<string>(memberNames.Select(name => "field:" + name));
        var first = false;
        foreach (var function in _functions.Where(function => function.StructName == structure.Name))
        {
            var methodName = MethodName(function);
            if (memberNames.Contains(methodName))
            {
                Skipped.Add($"{function.NativeName}: method name {methodName} is also a field of {structure.Name}");
                continue;
            }
            WriteOverloads(writer, function, isInstance: !function.IsConstructor && !function.IsStatic, signatures, ref first);
        }

        writer.Close();
    }

    private string? StructPointerTarget(CimguiField field)
    {
        var type = FieldTypeOf(field);
        if (!type.EndsWith('*') || type.EndsWith("**", StringComparison.Ordinal)) return null;
        var pointee = type[..^1];
        return _mapper.IsStruct(pointee) && !TypesWriter.MappedStructs.Contains(pointee) ? pointee : null;
    }

    private string FieldTypeOf(CimguiField field)
    {
        if (field.ArraySize > 0) return $"FixedArray{field.ArraySize}<{_mapper.Map(field.Type)}>";
        if (field.TemplateType != null && field.Type.StartsWith("ImVector_", StringComparison.Ordinal))
            return _mapper.MapVector(field.TemplateType) + new string('*', field.Type.Count(character => character == '*'));
        return _mapper.Map(field.Type);
    }

    private static string MethodName(CimguiFunction function)
    {
        if (function.IsConstructor) return "Create";
        if (function.IsDestructor) return "Destroy";
        return Names.Method(function.FunctionName);
    }

    private void WriteOverloads(CodeWriter writer, CimguiFunction function, bool isInstance, HashSet<string> signatures, ref bool first)
    {
        var arguments = function.Arguments.ToList();
        var hasSelf = isInstance;
        if (isInstance) arguments.RemoveAt(0);

        var converted = ConvertArguments(function, arguments);
        var visibleIndices = Enumerable.Range(0, converted.Count).Where(index => converted[index].ParameterType != null).ToList();
        var boundaries = new List<int> { converted.Count };
        for (var position = visibleIndices.Count - 1; position >= 0 && converted[visibleIndices[position]].OmittedExpression != null; position--)
            boundaries.Add(visibleIndices[position]);

        var (returnType, returnConversion) = ConvertReturn(function);
        var methodName = MethodName(function);

        foreach (var present in boundaries)
        {
            var visible = converted.Take(present).Where(argument => argument.ParameterType != null).ToList();
            var signature = methodName + "(" + string.Join(",", visible.Select(argument => argument.ParameterType)) + ")";
            if (!signatures.Add(signature))
            {
                Skipped.Add($"{function.NativeName}: overload {signature} already exists");
                continue;
            }

            var parameters = string.Join(", ", visible.Select(argument => $"{argument.ParameterType} {argument.ParameterName}"));
            var modifiers = isInstance ? "public" : "public static";
            if (!first) writer.Line();
            first = false;
            writer.Open($"{modifiers} {returnType} {methodName}({parameters})");

            foreach (var argument in converted.Take(present))
            {
                if (argument.Prologue != null) writer.Line(argument.Prologue);
            }

            var fixedStatements = converted.Take(present).Where(argument => argument.Fixed != null).Select(argument => argument.Fixed!).ToList();
            foreach (var statement in fixedStatements) writer.Line(statement);
            if (fixedStatements.Count > 0) writer.OpenBlock();

            var nativeArguments = new List<string>();
            if (hasSelf) nativeArguments.Add("Handle");
            for (var index = 0; index < converted.Count; index++)
                nativeArguments.Add(index < present ? converted[index].NativeExpression : converted[index].OmittedExpression ?? converted[index].NativeExpression);

            var call = $"ImGuiNative.{function.NativeName}({string.Join(", ", nativeArguments)})";
            writer.Line(returnType == "void" ? call + ";" : "return " + string.Format(CultureInfo.InvariantCulture, returnConversion, call) + ";");

            if (fixedStatements.Count > 0) writer.Close();
            writer.Close();
        }
    }

    private (string Type, string Conversion) ConvertReturn(CimguiFunction function)
    {
        var type = TypeMapper.StripConst(function.ReturnType);
        if (type == "char*") return ("string", "ImGuiUtf8.ToString({0})");
        if (type.EndsWith('*') && !type.EndsWith("**", StringComparison.Ordinal) && _mapper.IsStruct(type[..^1]) && !TypesWriter.MappedStructs.Contains(type[..^1]))
            return ($"{type[..^1]}Ptr", $"new {type[..^1]}Ptr({{0}})");
        return (_mapper.Map(function.ReturnType), "{0}");
    }

    private List<FriendlyArgument> ConvertArguments(CimguiFunction function, List<CimguiArgument> arguments)
    {
        var converted = new List<FriendlyArgument>();
        var hiddenCounts = new Dictionary<int, string>();

        for (var index = 0; index < arguments.Count; index++)
        {
            var argument = arguments[index];
            var camel = Names.Parameter(argument.Name);
            var name = Names.Identifier(camel);
            var rawType = _mapper.Map(argument.Type);
            var stripped = TypeMapper.StripConst(argument.Type);
            var isConst = TypeMapper.IsConst(argument.Type);
            var next = index + 1 < arguments.Count ? arguments[index + 1] : null;
            function.Defaults.TryGetValue(argument.Name, out var cDefault);
            var omitted = cDefault == null ? null : DefaultExpression(cDefault, rawType, function);

            if (hiddenCounts.TryGetValue(index, out var countExpression))
            {
                converted.Add(new FriendlyArgument { NativeName = name, NativeExpression = countExpression, OmittedExpression = omitted ?? countExpression });
                continue;
            }

            if (stripped == "char*" && isConst && index > 0 && argument.Name.EndsWith("_end", StringComparison.Ordinal) && TypeMapper.StripConst(arguments[index - 1].Type) == "char*")
            {
                converted.Add(new FriendlyArgument { NativeName = name, NativeExpression = "null", OmittedExpression = "null" });
                continue;
            }

            if (stripped == "char*" && isConst)
            {
                converted.Add(new FriendlyArgument
                {
                    NativeName = name,
                    ParameterType = "string",
                    ParameterName = name,
                    Prologue = $"using var {camel}Utf8 = new ImGuiUtf8String({name}, stackalloc byte[ImGuiUtf8String.StackCapacity]);",
                    NativeExpression = $"{camel}Utf8.Pointer",
                    OmittedExpression = omitted,
                });
                continue;
            }

            if (stripped == "const char* const[]" || argument.Type == "const char* const[]")
            {
                if (next != null && IsCount(next)) hiddenCounts[index + 1] = $"{name}.Length";
                converted.Add(new FriendlyArgument
                {
                    NativeName = name,
                    ParameterType = "string[]",
                    ParameterName = name,
                    Prologue = $"using var {camel}Utf8 = new ImGuiUtf8StringArray({name});",
                    NativeExpression = $"{camel}Utf8.Pointer",
                    OmittedExpression = omitted,
                });
                continue;
            }

            if (stripped == "char*" && !isConst && next != null && TypeMapper.StripConst(next.Type) == "size_t" && next.Name.Contains("size", StringComparison.Ordinal))
            {
                hiddenCounts[index + 1] = $"(nuint){name}.Length";
                converted.Add(new FriendlyArgument
                {
                    NativeName = name,
                    ParameterType = "Span<byte>",
                    ParameterName = name,
                    Fixed = $"fixed (byte* {camel}Pointer = {name})",
                    NativeExpression = $"{camel}Pointer",
                    OmittedExpression = omitted,
                });
                continue;
            }

            var arrayMatch = ArrayPattern().Match(stripped);
            if (arrayMatch.Success)
            {
                var elementType = _mapper.Map(arrayMatch.Groups[1].Value);
                var length = int.Parse(arrayMatch.Groups[2].Value, CultureInfo.InvariantCulture);
                if (elementType == "float" && length is >= 2 and <= 4)
                {
                    converted.Add(new FriendlyArgument
                    {
                        NativeName = name,
                        ParameterType = $"ref Vector{length}",
                        ParameterName = name,
                        Fixed = $"fixed (Vector{length}* {camel}Pointer = &{name})",
                        NativeExpression = $"(float*){camel}Pointer",
                        OmittedExpression = omitted,
                    });
                }
                else
                {
                    converted.Add(new FriendlyArgument
                    {
                        NativeName = name,
                        ParameterType = $"Span<{elementType}>",
                        ParameterName = name,
                        Prologue = $"if ({name}.Length < {length}) throw new ArgumentException(\"{camel} needs {length} elements.\", nameof({name}));",
                        Fixed = $"fixed ({elementType}* {camel}Pointer = {name})",
                        NativeExpression = $"{camel}Pointer",
                        OmittedExpression = omitted,
                    });
                }
                continue;
            }

            if (rawType.EndsWith('*') && !rawType.EndsWith("**", StringComparison.Ordinal))
            {
                var pointee = rawType[..^1];
                var cPointee = stripped[..^1].Trim();

                if (isConst && next != null && IsCount(next) && IsValueType(pointee) && pointee != "byte")
                {
                    hiddenCounts[index + 1] = $"{name}.Length";
                    converted.Add(new FriendlyArgument
                    {
                        NativeName = name,
                        ParameterType = $"ReadOnlySpan<{pointee}>",
                        ParameterName = name,
                        Fixed = $"fixed ({pointee}* {camel}Pointer = {name})",
                        NativeExpression = $"{camel}Pointer",
                        OmittedExpression = omitted,
                    });
                    continue;
                }

                if (!isConst && IsValueType(pointee) && pointee != "byte")
                {
                    converted.Add(new FriendlyArgument
                    {
                        NativeName = name,
                        ParameterType = $"ref {pointee}",
                        ParameterName = name,
                        Fixed = $"fixed ({pointee}* {camel}Pointer = &{name})",
                        NativeExpression = $"{camel}Pointer",
                        OmittedExpression = omitted,
                    });
                    continue;
                }

                if (_mapper.IsStruct(cPointee) && !TypesWriter.MappedStructs.Contains(cPointee))
                {
                    converted.Add(new FriendlyArgument
                    {
                        NativeName = name,
                        ParameterType = $"{cPointee}Ptr",
                        ParameterName = name,
                        NativeExpression = $"{name}.Handle",
                        OmittedExpression = omitted,
                    });
                    continue;
                }
            }

            converted.Add(new FriendlyArgument
            {
                NativeName = name,
                ParameterType = rawType,
                ParameterName = name,
                NativeExpression = name,
                OmittedExpression = omitted,
            });
        }

        return converted;
    }

    private static bool IsCount(CimguiArgument argument) =>
        TypeMapper.StripConst(argument.Type) == "int" && (argument.Name.Contains("count", StringComparison.Ordinal) || argument.Name.StartsWith("num_", StringComparison.Ordinal));

    private bool IsValueType(string csharpType) => csharpType switch
    {
        "bool" or "byte" or "sbyte" or "short" or "ushort" or "int" or "uint" or "long" or "ulong" or "float" or "double" or "nuint" => true,
        "Vector2" or "Vector4" => true,
        _ => _mapper.IsEnum(csharpType),
    };

    private string DefaultExpression(string cDefault, string rawType, CimguiFunction function)
    {
        var value = cDefault.Trim();
        if (value is "NULL" or "nullptr") return rawType.EndsWith('*') ? "null" : "default";
        if (value is "true" or "false") return value;
        if (value.StartsWith('"')) return $"ImGuiUtf8.Literal({value[..^1]}\\0\"u8)";
        if (value == "sizeof(float)") return $"({rawType})sizeof(float)";

        var vectorMatch = VectorPattern().Match(value);
        if (vectorMatch.Success)
        {
            var components = vectorMatch.Groups[2].Value.Split(',').Select(component => FloatLiteral(component.Trim()));
            return $"new Vector{vectorMatch.Groups[1].Value}({string.Join(", ", components)})";
        }

        if (rawType == "float") return FloatLiteral(value);

        var number = ParseNumber(value) ?? throw new InvalidOperationException($"Default '{cDefault}' of {function.NativeName} is not understood.");
        return rawType switch
        {
            "double" => number.ToString("R", CultureInfo.InvariantCulture) + "d",
            "int" or "short" or "sbyte" or "long" => ((long)number).ToString(CultureInfo.InvariantCulture),
            "uint" => ((ulong)number).ToString(CultureInfo.InvariantCulture) + "u",
            "ulong" => ((ulong)number).ToString(CultureInfo.InvariantCulture) + "ul",
            "nuint" or "ushort" or "byte" => $"({rawType}){(ulong)number}",
            _ when _mapper.IsEnum(rawType) => $"({rawType})({(long)number})",
            _ => throw new InvalidOperationException($"Default '{cDefault}' of {function.NativeName} has no conversion to {rawType}."),
        };
    }

    private static string FloatLiteral(string value)
    {
        var sign = "";
        if (value.StartsWith('-'))
        {
            sign = "-";
            value = value[1..];
        }
        if (value == "FLT_MAX") return sign + "float.MaxValue";
        if (value == "FLT_MIN") return sign + "1.17549435E-38f";
        var number = ParseNumber(value) ?? throw new InvalidOperationException($"Float default '{value}' is not understood.");
        return sign + number.ToString("R", CultureInfo.InvariantCulture) + "f";
    }

    private static double? ParseNumber(string value)
    {
        value = value.TrimStart('+').TrimEnd('f', 'F');
        if (value.EndsWith('.')) value += "0";
        return double.TryParse(value, NumberStyles.Float, CultureInfo.InvariantCulture, out var number) ? number : null;
    }

    [GeneratedRegex(@"\bfmt\b")]
    private static partial Regex FmtPattern();

    [GeneratedRegex(@"^(.+?)\s*\[(\d+)\]$")]
    private static partial Regex ArrayPattern();

    [GeneratedRegex(@"^ImVec([24])\((.*)\)$")]
    private static partial Regex VectorPattern();

    [GeneratedRegex(@"[\w\s\*]+?\s*(\w+)\s*;")]
    private static partial Regex UnionNamePattern();
}
