using System.Runtime.InteropServices;
using System.Text;
using System.Text.RegularExpressions;
using Crystal.Graphics.D3D12;
using TerraFX.Interop.DirectX;
using TerraFX.Interop.Windows;
using static TerraFX.Interop.DirectX.D3D_NAME;
using static TerraFX.Interop.DirectX.D3D_SHADER_INPUT_TYPE;
using static TerraFX.Interop.DirectX.D3D_SHADER_VARIABLE_CLASS;
using static TerraFX.Interop.DirectX.D3D_SRV_DIMENSION;
using static TerraFX.Interop.DirectX.DirectX;
using static TerraFX.Interop.DirectX.DXC_OUT_KIND;
using static TerraFX.Interop.Windows.Windows;

namespace Crystal.ShaderCompiler;

public enum ShaderStage
{
    Vertex,
    Pixel,
    Compute,
}

internal sealed record StageReflection(
    List<D3D12ConstantBufferInfo> ConstantBuffers, List<D3D12ShaderResourceInfo> Resources, List<D3D12VertexInputInfo> VertexInputs, List<string> Problems)
{
    public D3D12ThreadGroupSize ThreadGroupSize { get; set; }
}

internal sealed record CompiledShader(byte[] Bytecode, StageReflection Reflection);

internal sealed unsafe partial class DxcShaderCompiler : IDisposable
{
    private static readonly Guid CompilerClassId = new("73e22d93-e6ce-47f3-b5bf-f0664f39c1b0");
    private static readonly Guid UtilsClassId = new("6245d6af-66e0-48fd-80b4-4d271796748c");
    private const uint Utf8CodePage = 65001;

    private IDxcCompiler3* _compiler;
    private IDxcUtils* _utils;
    private IDxcIncludeHandler* _includeHandler;

    public DxcShaderCompiler()
    {
        var compilerClassId = CompilerClassId;
        var utilsClassId = UtilsClassId;
        IDxcCompiler3* compiler;
        IDxcUtils* utils;
        IDxcIncludeHandler* includeHandler;
        ThrowOnFailure(DxcCreateInstance(&compilerClassId, __uuidof<IDxcCompiler3>(), (void**)&compiler), "DxcCreateInstance(DxcCompiler)");
        _compiler = compiler;
        ThrowOnFailure(DxcCreateInstance(&utilsClassId, __uuidof<IDxcUtils>(), (void**)&utils), "DxcCreateInstance(DxcUtils)");
        _utils = utils;
        ThrowOnFailure(_utils->CreateDefaultIncludeHandler(&includeHandler), "IDxcUtils::CreateDefaultIncludeHandler");
        _includeHandler = includeHandler;
    }

    public CompiledShader? Compile(
        string sourcePath, byte[] source, string entryPoint, ShaderStage stage, CompilerOptions options, List<ShaderDiagnostic> diagnostics)
    {
        var arguments = new List<string>
        {
            sourcePath,
            "-E", entryPoint,
            "-T", TargetPrefix(stage) + options.ShaderModel,
            "-HV", "2021",
            options.ColumnMajorMatrices ? "-Zpc" : "-Zpr",
            "-Qstrip_reflect",
        };
        arguments.AddRange(options.DebugInfo ? ["-Zi", "-Qembed_debug"] : ["-O3"]);
        if (options.WarningsAsErrors) arguments.Add("-WX");
        foreach (var directory in options.IncludeDirectories) arguments.AddRange(["-I", directory]);
        foreach (var define in options.Defines) arguments.AddRange(["-D", define]);

        var result = Run(arguments, source);
        try
        {
            AddDiagnostics(result, sourcePath, entryPoint, diagnostics);
            HRESULT status;
            ThrowOnFailure(result->GetStatus(&status), "IDxcResult::GetStatus");
            if (FAILED(status)) return null;

            var bytecode = ReadOutput(result, DXC_OUT_OBJECT);
            var reflectionData = ReadOutput(result, DXC_OUT_REFLECTION);
            return new CompiledShader(bytecode, Reflect(reflectionData, stage));
        }
        finally
        {
            result->Release();
        }
    }

    public string? Preprocess(string sourcePath, byte[] source, CompilerOptions options, List<ShaderDiagnostic> diagnostics)
    {
        var arguments = new List<string> { sourcePath, "-P", "-HV", "2021" };
        foreach (var directory in options.IncludeDirectories) arguments.AddRange(["-I", directory]);
        foreach (var define in options.Defines) arguments.AddRange(["-D", define]);
        var result = Run(arguments, source);
        try
        {
            AddDiagnostics(result, sourcePath, "preprocessor", diagnostics);
            HRESULT status;
            ThrowOnFailure(result->GetStatus(&status), "IDxcResult::GetStatus");
            if (FAILED(status)) return null;
            return Encoding.UTF8.GetString(ReadOutput(result, DXC_OUT_HLSL)).TrimEnd('\0');
        }
        finally
        {
            result->Release();
        }
    }

    private IDxcResult* Run(List<string> arguments, byte[] source)
    {
        var argumentPointers = new nint[arguments.Count];
        try
        {
            for (var index = 0; index < arguments.Count; index++) argumentPointers[index] = Marshal.StringToHGlobalUni(arguments[index]);
            fixed (byte* sourcePointer = source)
            fixed (nint* argumentsPointer = argumentPointers)
            {
                var buffer = new DxcBuffer { Ptr = sourcePointer, Size = (nuint)source.Length, Encoding = Utf8CodePage };
                IDxcResult* result;
                ThrowOnFailure(
                    _compiler->Compile(&buffer, (char**)argumentsPointer, (uint)arguments.Count, _includeHandler, __uuidof<IDxcResult>(), (void**)&result),
                    "IDxcCompiler3::Compile");
                return result;
            }
        }
        finally
        {
            foreach (var pointer in argumentPointers) Marshal.FreeHGlobal(pointer);
        }
    }

    public void Dispose()
    {
        Release(ref _includeHandler);
        Release(ref _utils);
        Release(ref _compiler);
    }

    private static void AddDiagnostics(IDxcResult* result, string sourcePath, string entryPoint, List<ShaderDiagnostic> diagnostics)
    {
        IDxcBlobUtf8* errors = null;
        if (FAILED(result->GetOutput(DXC_OUT_ERRORS, __uuidof<IDxcBlobUtf8>(), (void**)&errors, null)) || errors == null) return;
        try
        {
            if (errors->GetStringLength() == 0) return;
            var text = Encoding.UTF8.GetString((byte*)errors->GetStringPointer(), (int)errors->GetStringLength());
            var added = false;
            foreach (var line in text.Split('\n'))
            {
                var match = DiagnosticPattern().Match(line.TrimEnd('\r'));
                if (!match.Success) continue;
                var diagnostic = new ShaderDiagnostic(
                    match.Groups["file"].Value,
                    int.Parse(match.Groups["line"].Value),
                    int.Parse(match.Groups["column"].Value),
                    match.Groups["severity"].Value != "warning",
                    "DXC",
                    match.Groups["message"].Value);
                if (!diagnostics.Contains(diagnostic)) diagnostics.Add(diagnostic);
                added = true;
            }
            if (!added) diagnostics.Add(new ShaderDiagnostic(sourcePath, 1, 1, IsError: true, "DXC", $"{entryPoint}: {text.Trim()}"));
        }
        finally
        {
            errors->Release();
        }
    }

    private static byte[] ReadOutput(IDxcResult* result, DXC_OUT_KIND kind)
    {
        IDxcBlob* blob = null;
        ThrowOnFailure(result->GetOutput(kind, __uuidof<IDxcBlob>(), (void**)&blob, null), $"IDxcResult::GetOutput({kind})");
        try
        {
            return new ReadOnlySpan<byte>(blob->GetBufferPointer(), (int)blob->GetBufferSize()).ToArray();
        }
        finally
        {
            blob->Release();
        }
    }

    private StageReflection Reflect(byte[] reflectionData, ShaderStage stage)
    {
        var visibility = stage switch
        {
            ShaderStage.Vertex => D3D12ShaderVisibility.Vertex,
            ShaderStage.Pixel => D3D12ShaderVisibility.Pixel,
            _ => D3D12ShaderVisibility.Compute,
        };
        var reflection = new StageReflection([], [], [], []);
        ID3D12ShaderReflection* shaderReflection = null;
        fixed (byte* data = reflectionData)
        {
            var buffer = new DxcBuffer { Ptr = data, Size = (nuint)reflectionData.Length };
            ThrowOnFailure(_utils->CreateReflection(&buffer, __uuidof<ID3D12ShaderReflection>(), (void**)&shaderReflection), "IDxcUtils::CreateReflection");
        }

        try
        {
            D3D12_SHADER_DESC description;
            ThrowOnFailure(shaderReflection->GetDesc(&description), "ID3D12ShaderReflection::GetDesc");

            for (var index = 0u; index < description.BoundResources; index++)
            {
                D3D12_SHADER_INPUT_BIND_DESC binding;
                ThrowOnFailure(shaderReflection->GetResourceBindingDesc(index, &binding), "ID3D12ShaderReflection::GetResourceBindingDesc");
                var name = Marshal.PtrToStringUTF8((nint)binding.Name)!;
                if (binding.BindCount != 1)
                {
                    reflection.Problems.Add($"'{name}' is an array of resources. Resource arrays are not supported yet; declare each resource separately.");
                    continue;
                }

                if (binding.Type == D3D_SIT_CBUFFER)
                {
                    reflection.ConstantBuffers.Add(ReflectConstantBuffer(shaderReflection, binding, name, visibility));
                    continue;
                }

                D3D12ShaderResourceKind kind;
                switch (binding.Type)
                {
                    case D3D_SIT_TEXTURE when binding.Dimension != D3D_SRV_DIMENSION_BUFFER:
                        kind = D3D12ShaderResourceKind.Texture;
                        break;
                    case D3D_SIT_UAV_RWTYPED when binding.Dimension != D3D_SRV_DIMENSION_BUFFER:
                        kind = D3D12ShaderResourceKind.ReadWriteTexture;
                        break;
                    case D3D_SIT_STRUCTURED:
                        kind = D3D12ShaderResourceKind.StructuredBuffer;
                        break;
                    case D3D_SIT_BYTEADDRESS:
                        kind = D3D12ShaderResourceKind.ByteAddressBuffer;
                        break;
                    case D3D_SIT_UAV_RWSTRUCTURED:
                        kind = D3D12ShaderResourceKind.ReadWriteStructuredBuffer;
                        break;
                    case D3D_SIT_UAV_RWBYTEADDRESS:
                        kind = D3D12ShaderResourceKind.ReadWriteByteAddressBuffer;
                        break;
                    case D3D_SIT_SAMPLER:
                        kind = D3D12ShaderResourceKind.Sampler;
                        break;
                    default:
                        reflection.Problems.Add($"'{name}' has an unsupported resource type ({binding.Type}). Use textures, StructuredBuffer, ByteAddressBuffer, their RW forms, or samplers.");
                        continue;
                }
                reflection.Resources.Add(new D3D12ShaderResourceInfo(name, kind, (int)binding.Dimension, (int)binding.BindPoint, (int)binding.Space, visibility));
            }

            if (stage == ShaderStage.Compute)
            {
                uint x, y, z;
                shaderReflection->GetThreadGroupSize(&x, &y, &z);
                reflection.ThreadGroupSize = new D3D12ThreadGroupSize((int)x, (int)y, (int)z);
            }

            if (stage == ShaderStage.Vertex)
            {
                for (var index = 0u; index < description.InputParameters; index++)
                {
                    D3D12_SIGNATURE_PARAMETER_DESC parameter;
                    ThrowOnFailure(shaderReflection->GetInputParameterDesc(index, &parameter), "ID3D12ShaderReflection::GetInputParameterDesc");
                    if (parameter.SystemValueType != D3D_NAME_UNDEFINED) continue;
                    reflection.VertexInputs.Add(new D3D12VertexInputInfo(
                        Marshal.PtrToStringUTF8((nint)parameter.SemanticName)!.ToUpperInvariant(), (int)parameter.SemanticIndex, (int)parameter.Register));
                }
            }
        }
        finally
        {
            shaderReflection->Release();
        }
        return reflection;
    }

    private static string TargetPrefix(ShaderStage stage) => stage switch
    {
        ShaderStage.Vertex => "vs_",
        ShaderStage.Pixel => "ps_",
        _ => "cs_",
    };

    private static D3D12ConstantBufferInfo ReflectConstantBuffer(
        ID3D12ShaderReflection* shaderReflection, in D3D12_SHADER_INPUT_BIND_DESC binding, string name, D3D12ShaderVisibility visibility)
    {
        var buffer = shaderReflection->GetConstantBufferByName(binding.Name);
        D3D12_SHADER_BUFFER_DESC bufferDescription;
        ThrowOnFailure(buffer->GetDesc(&bufferDescription), "ID3D12ShaderReflectionConstantBuffer::GetDesc");

        var variables = new List<D3D12ShaderVariableInfo>();
        for (var index = 0u; index < bufferDescription.Variables; index++)
        {
            var variable = buffer->GetVariableByIndex(index);
            D3D12_SHADER_VARIABLE_DESC variableDescription;
            ThrowOnFailure(variable->GetDesc(&variableDescription), "ID3D12ShaderReflectionVariable::GetDesc");
            D3D12_SHADER_TYPE_DESC typeDescription;
            ThrowOnFailure(variable->GetType()->GetDesc(&typeDescription), "ID3D12ShaderReflectionType::GetDesc");
            var variableClass = typeDescription.Class switch
            {
                D3D_SVC_SCALAR => D3D12ShaderVariableClass.Scalar,
                D3D_SVC_VECTOR => D3D12ShaderVariableClass.Vector,
                D3D_SVC_MATRIX_ROWS => D3D12ShaderVariableClass.MatrixRows,
                D3D_SVC_MATRIX_COLUMNS => D3D12ShaderVariableClass.MatrixColumns,
                _ => D3D12ShaderVariableClass.Struct,
            };
            variables.Add(new D3D12ShaderVariableInfo(
                Marshal.PtrToStringUTF8((nint)variableDescription.Name)!,
                (int)variableDescription.StartOffset,
                (int)variableDescription.Size,
                variableClass,
                (int)typeDescription.Rows,
                (int)typeDescription.Columns,
                (int)typeDescription.Elements));
        }
        return new D3D12ConstantBufferInfo(name, (int)binding.BindPoint, (int)binding.Space, (int)bufferDescription.Size, visibility, [.. variables]);
    }

    private static void ThrowOnFailure(HRESULT result, string operation)
    {
        if (FAILED(result)) throw new InvalidOperationException($"{operation} failed with HRESULT 0x{result.Value:X8}.");
    }

    private static void Release<T>(ref T* pointer) where T : unmanaged, IUnknown.Interface
    {
        if (pointer == null) return;
        pointer->Release();
        pointer = null;
    }

    [GeneratedRegex(@"^(?<file>.+?):(?<line>\d+):(?<column>\d+): (?<severity>error|warning|fatal error): (?<message>.*)$")]
    private static partial Regex DiagnosticPattern();
}
