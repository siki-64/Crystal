using System.Text;
using System.Text.RegularExpressions;
using Crystal.Graphics.D3D12;

namespace Crystal.ShaderCompiler;

internal static class DescriptorIndexRewriter
{
    public const int VertexSlotLimit = 16;
    public const int PixelSlotLimit = 32;
    public const int ComputeSlotLimit = 32;
    public const string IndexBlockRegister = "b0, space1";

    public static int SlotLimit(ShaderStage stage) => stage switch { ShaderStage.Vertex => VertexSlotLimit, ShaderStage.Pixel => PixelSlotLimit, _ => ComputeSlotLimit };

    public static string? Rewrite(string preprocessedSource, IReadOnlyList<D3D12ShaderResourceInfo> resources, ShaderStage stage, List<string> problems)
    {
        var slotLimit = SlotLimit(stage);
        if (resources.Count > slotLimit)
        {
            problems.Add($"The {stage} shader reads {resources.Count} resources and samplers; at most {slotLimit} fit.");
            return null;
        }

        var text = preprocessedSource;
        for (var slot = 0; slot < resources.Count; slot++)
        {
            var resource = resources[slot];
            var pattern = new Regex(
                $@"^(?<lead>[ \t]*)(?<type>(?:globallycoherent\s+)?[A-Za-z_]\w*(?:\s*<[^;{{}}]*?>)?)\s+{Regex.Escape(resource.Name)}\s*(?::\s*register\s*\([^)]*\))?\s*;",
                RegexOptions.Multiline);
            var matches = pattern.Matches(text);
            if (matches.Count != 1)
            {
                problems.Add($"'{resource.Name}' needs exactly one global declaration of the form '<Type> {resource.Name} : register(...);' but {matches.Count} were found.");
                continue;
            }

            var heap = resource.Kind == D3D12ShaderResourceKind.Sampler ? "SamplerDescriptorHeap" : "ResourceDescriptorHeap";
            var match = matches[0];
            var declaration = $"{match.Groups["lead"].Value}static {match.Groups["type"].Value} {resource.Name} = {heap}[CrystalDescriptorIndices.Slots[{slot / 4}].{"xyzw"[slot % 4]}];";
            text = string.Concat(text.AsSpan(0, match.Index), declaration, text.AsSpan(match.Index + match.Length));
        }
        if (problems.Count > 0) return null;

        var builder = new StringBuilder();
        builder.Append("struct CrystalDescriptorIndexBlock { uint4 Slots[").Append(Math.Max(1, (resources.Count + 3) / 4)).AppendLine("]; };");
        builder.Append("ConstantBuffer<CrystalDescriptorIndexBlock> CrystalDescriptorIndices : register(").Append(IndexBlockRegister).AppendLine(");");
        builder.Append(text);
        return builder.ToString();
    }
}
