namespace Crystal.ImGui.Generator;

internal static class Program
{
    public const string Namespace = "namespace Crystal.ImGui;";

    public static int Main(string[] args)
    {
        var repositoryRoot = args.Length > 0 ? Path.GetFullPath(args[0]) : FindRepositoryRoot(AppContext.BaseDirectory);
        var nativeDirectory = Path.Combine(repositoryRoot, "src", "Crystal.ImGui", "Native");
        var generatedDirectory = Path.Combine(repositoryRoot, "src", "Crystal.ImGui", "Generated");
        var testsDirectory = Path.Combine(repositoryRoot, "tests", "Crystal.Tests", "ImGui");

        var model = CimguiModel.Load(nativeDirectory);
        var mapper = new TypeMapper(model);

        var types = new TypesWriter(model, mapper);
        types.WriteEnums(Path.Combine(generatedDirectory, "ImGuiEnums.g.cs"));

        var functions = new FunctionsWriter(model, mapper);
        functions.WriteNative(Path.Combine(generatedDirectory, "ImGuiNative.g.cs"));
        functions.WriteFriendly(Path.Combine(generatedDirectory, "ImGui.g.cs"), Path.Combine(generatedDirectory, "ImGuiPointers.g.cs"));

        types.WriteStructs(Path.Combine(generatedDirectory, "ImGuiStructs.g.cs"));

        var layout = new LayoutWriter(types.Layouts);
        layout.WriteTest(Path.Combine(testsDirectory, "ImGuiStructLayoutTests.g.cs"));

        var shims = new CodeWriter();
        shims.Line("#include \"./imgui/imgui.h\"");
        shims.Line("#include \"./imgui/imgui_internal.h\"");
        shims.Line("#include \"cimgui.h\"");
        shims.Line("#include <stddef.h>");
        shims.Line("#include <string.h>");
        shims.Line();
        functions.WriteTextShims(shims);
        layout.WriteNative(shims);
        shims.WriteTo(Path.Combine(nativeDirectory, "CrystalImGuiShims.cpp"));

        foreach (var skipped in functions.Skipped) Console.WriteLine("skipped " + skipped);
        Console.WriteLine($"{model.Functions.Count} functions, {model.Structs.Count} structs, {model.Enums.Count} enums, {functions.Skipped.Count} skipped");
        return 0;
    }

    private static string FindRepositoryRoot(string start)
    {
        for (var directory = new DirectoryInfo(start); directory != null; directory = directory.Parent)
        {
            if (File.Exists(Path.Combine(directory.FullName, "Crystal.slnx"))) return directory.FullName;
        }
        throw new DirectoryNotFoundException("Run from inside the Crystal repository or pass its root as the first argument.");
    }
}
