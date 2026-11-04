using System.Globalization;
using System.Text.RegularExpressions;

namespace Crystal.ImGui.Generator;

internal sealed record LayoutField(string CName, string CSharpName);

internal sealed record LayoutStruct(string CName, string CSharpName, IReadOnlyList<LayoutField> Fields);

internal sealed partial class TypesWriter(CimguiModel model, TypeMapper mapper)
{
    public static readonly HashSet<string> MappedStructs = ["ImVec2", "ImVec4"];

    private readonly SortedSet<int> _fixedArraySizes = [];

    public List<LayoutStruct> Layouts { get; } = [];

    public void WriteEnums(string path)
    {
        var writer = new CodeWriter();
        writer.Line(Program.Namespace);
        foreach (var enumeration in model.Enums.OrderBy(enumeration => enumeration.Name, StringComparer.Ordinal))
        {
            var underlyingType = mapper.MapBase(enumeration.UnderlyingType);
            writer.Line();
            if (enumeration.Name.EndsWith("Flags", StringComparison.Ordinal)) writer.Line("[Flags]");
            writer.Open($"public enum {enumeration.Name} : {underlyingType}");
            var used = new HashSet<string>();
            foreach (var member in enumeration.Members)
            {
                var name = Names.EnumMember(enumeration.Name, member.Name);
                if (!used.Add(name)) throw new InvalidOperationException($"Enum member {member.Name} maps to the duplicate name {enumeration.Name}.{name}.");
                writer.Line($"{name} = {EnumValue(member.Value, underlyingType)},");
            }
            writer.Close();
        }
        writer.WriteTo(path);
    }

    public void WriteStructs(string path)
    {
        var writer = new CodeWriter();
        writer.Usings("System.Diagnostics.CodeAnalysis", "System.Numerics", "System.Runtime.CompilerServices", "System.Runtime.InteropServices");
        writer.Line(Program.Namespace);

        foreach (var structure in model.Structs.OrderBy(structure => structure.Name, StringComparer.Ordinal))
        {
            if (!MappedStructs.Contains(structure.Name)) WriteStruct(writer, structure);
        }

        foreach (var opaque in mapper.OpaqueStructs)
        {
            writer.Line();
            writer.Line("[StructLayout(LayoutKind.Sequential)]");
            writer.Open($"public struct {opaque}");
            writer.Close();
        }

        foreach (var smallStack in mapper.SmallStacks.OrderBy(stack => stack.Name, StringComparer.Ordinal))
        {
            _fixedArraySizes.Add(smallStack.Capacity);
            writer.Line();
            writer.Line("[StructLayout(LayoutKind.Sequential)]");
            writer.Open($"public struct {smallStack.Name}");
            writer.Line($"public FixedArray{smallStack.Capacity}<{smallStack.ElementType}> LocalData;");
            writer.Line("public ushort Size;");
            writer.Close();
        }

        foreach (var size in _fixedArraySizes)
        {
            writer.Line();
            writer.Line($"[InlineArray({size})]");
            writer.Open($"public struct FixedArray{size}<T> where T : unmanaged");
            writer.Line("private T _element;");
            writer.Close();
        }

        writer.WriteTo(path);
    }

    private static string EnumValue(long value, string underlyingType) => underlyingType switch
    {
        "int" when value > int.MaxValue => $"unchecked((int){value})",
        "uint" => $"{value}u",
        _ => value.ToString(CultureInfo.InvariantCulture),
    };

    private void WriteStruct(CodeWriter writer, CimguiStruct structure)
    {
        var layoutFields = new List<LayoutField>();
        var unions = new List<(string TypeName, List<(string Type, string Name)> Members)>();

        writer.Line();
        writer.Line("[StructLayout(LayoutKind.Sequential)]");
        writer.Open($"public unsafe partial struct {structure.Name}");

        var bitfieldIndex = 0;
        var fieldIndex = 0;
        while (fieldIndex < structure.Fields.Count)
        {
            var field = structure.Fields[fieldIndex];

            if (field.BitfieldWidth > 0)
            {
                fieldIndex = WriteBitfieldGroup(writer, structure, fieldIndex, bitfieldIndex++);
                continue;
            }

            if (field.Type.StartsWith("union", StringComparison.Ordinal))
            {
                var typeName = $"{structure.Name}Union{unions.Count}";
                var members = UnionMemberPattern().Matches(field.Type[(field.Type.IndexOf('{') + 1)..field.Type.LastIndexOf('}')])
                    .Select(match => (Type: mapper.Map(match.Groups[1].Value), Name: match.Groups[2].Value))
                    .ToList();
                unions.Add((typeName, members));
                var unionField = $"_union{unions.Count - 1}";
                writer.Line($"private {typeName} {unionField};");
                foreach (var member in members)
                    writer.Line($"[UnscopedRef] public ref {member.Type} {Names.Identifier(member.Name)} => ref {unionField}.{Names.Identifier(member.Name)};");
                fieldIndex++;
                continue;
            }

            var name = Names.Identifier(Names.FieldName(field.Name));
            if (name == structure.Name) throw new InvalidOperationException($"Field {structure.Name}.{field.Name} has the name of its struct.");
            writer.Line($"public {FieldType(field)} {name};");
            layoutFields.Add(new LayoutField(Names.FieldName(field.Name), name));
            fieldIndex++;
        }

        writer.Close();

        foreach (var union in unions)
        {
            writer.Line();
            writer.Line("[StructLayout(LayoutKind.Explicit)]");
            writer.Open($"public unsafe struct {union.TypeName}");
            foreach (var member in union.Members) writer.Line($"[FieldOffset(0)] public {member.Type} {Names.Identifier(member.Name)};");
            writer.Close();
        }

        Layouts.Add(new LayoutStruct(structure.Name, structure.Name, layoutFields));
    }

    private int WriteBitfieldGroup(CodeWriter writer, CimguiStruct structure, int firstField, int groupIndex)
    {
        var storageType = mapper.Map(structure.Fields[firstField].Type);
        if (storageType != "uint") throw new InvalidOperationException($"Bitfields of type {storageType} in {structure.Name} are not supported.");
        var storageName = $"_bitfield{groupIndex}";
        writer.Line($"private {storageType} {storageName};");

        var shift = 0;
        var index = firstField;
        while (index < structure.Fields.Count && structure.Fields[index].BitfieldWidth > 0 && mapper.Map(structure.Fields[index].Type) == storageType)
        {
            var field = structure.Fields[index];
            var mask = (1UL << field.BitfieldWidth) - 1;
            writer.Open($"public {storageType} {Names.Identifier(field.Name)}");
            writer.Line($"readonly get => ({storageName} >> {shift}) & 0x{mask:X}u;");
            writer.Line($"set => {storageName} = ({storageName} & ~(0x{mask:X}u << {shift})) | ((value & 0x{mask:X}u) << {shift});");
            writer.Close();
            shift += field.BitfieldWidth;
            index++;
        }

        if (shift > 32) throw new InvalidOperationException($"Bitfields in {structure.Name} need more than 32 bits.");
        return index;
    }

    private string FieldType(CimguiField field)
    {
        if (field.ArraySize > 0)
        {
            _fixedArraySizes.Add(field.ArraySize);
            return $"FixedArray{field.ArraySize}<{mapper.Map(field.Type)}>";
        }
        if (field.TemplateType != null && field.Type.StartsWith("ImVector_", StringComparison.Ordinal))
            return mapper.MapVector(field.TemplateType) + new string('*', field.Type.Count(character => character == '*'));
        return mapper.Map(field.Type);
    }

    [GeneratedRegex(@"([\w\s\*]+?)\s*(\w+)\s*;")]
    private static partial Regex UnionMemberPattern();
}
