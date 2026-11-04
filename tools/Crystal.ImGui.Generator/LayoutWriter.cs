namespace Crystal.ImGui.Generator;

internal sealed class LayoutWriter(IReadOnlyList<LayoutStruct> layouts)
{
    public const string StructSizeFunction = FunctionsWriter.ShimPrefix + "ImGuiStructSize";
    public const string FieldOffsetFunction = FunctionsWriter.ShimPrefix + "ImGuiFieldOffset";

    public void WriteNative(CodeWriter writer)
    {
        writer.Line($"CIMGUI_API int {StructSizeFunction}(const char* struct_name)");
        writer.Line("{");
        foreach (var layout in layouts)
            writer.Line($"    if (strcmp(struct_name, \"{layout.CName}\") == 0) return (int)sizeof({layout.CName});");
        writer.Line("    return -1;");
        writer.Line("}");
        writer.Line();
        writer.Line($"CIMGUI_API int {FieldOffsetFunction}(const char* struct_name, const char* field_name)");
        writer.Line("{");
        foreach (var layout in layouts)
        {
            if (layout.Fields.Count == 0) continue;
            writer.Line($"    if (strcmp(struct_name, \"{layout.CName}\") == 0)");
            writer.Line("    {");
            foreach (var field in layout.Fields)
                writer.Line($"        if (strcmp(field_name, \"{field.CName}\") == 0) return (int)offsetof({layout.CName}, {field.CName});");
            writer.Line("        return -1;");
            writer.Line("    }");
        }
        writer.Line("    return -1;");
        writer.Line("}");
    }

    public void WriteTest(string path)
    {
        var writer = new CodeWriter();
        writer.Usings("System.Runtime.InteropServices", "Crystal.ImGui");
        writer.Line("namespace Crystal.Tests;");
        writer.Line();
        writer.Line("[TestClass]");
        writer.Open("public sealed unsafe partial class ImGuiStructLayoutTests");
        writer.Line("[LibraryImport(ImGuiNative.LibraryName, StringMarshalling = StringMarshalling.Utf8)]");
        writer.Line($"private static partial int {StructSizeFunction}(string structName);");
        writer.Line();
        writer.Line("[LibraryImport(ImGuiNative.LibraryName, StringMarshalling = StringMarshalling.Utf8)]");
        writer.Line($"private static partial int {FieldOffsetFunction}(string structName, string fieldName);");

        foreach (var layout in layouts)
        {
            writer.Line();
            writer.Line("[TestMethod]");
            writer.Open($"public void {layout.CSharpName}MatchesNativeLayout()");
            writer.Line($"{layout.CSharpName} value = default;");
            writer.Line("var start = (byte*)&value;");
            writer.Line($"Assert.AreEqual({StructSizeFunction}(\"{layout.CName}\"), sizeof({layout.CSharpName}), \"sizeof\");");
            foreach (var field in layout.Fields)
                writer.Line($"Assert.AreEqual({FieldOffsetFunction}(\"{layout.CName}\", \"{field.CName}\"), (int)((byte*)&value.{field.CSharpName} - start), \"{field.CName}\");");
            writer.Close();
        }

        writer.Close();
        writer.WriteTo(path);
    }
}
