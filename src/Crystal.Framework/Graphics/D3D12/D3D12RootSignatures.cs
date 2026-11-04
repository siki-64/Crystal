using System.Text;
using TerraFX.Interop.DirectX;
using static Crystal.Graphics.D3D12.D3D12Interop;
using static TerraFX.Interop.DirectX.D3D_ROOT_SIGNATURE_VERSION;
using static TerraFX.Interop.DirectX.D3D12_DESCRIPTOR_RANGE_FLAGS;
using static TerraFX.Interop.DirectX.D3D12_DESCRIPTOR_RANGE_TYPE;
using static TerraFX.Interop.DirectX.D3D12_ROOT_DESCRIPTOR_FLAGS;
using static TerraFX.Interop.DirectX.D3D12_ROOT_PARAMETER_TYPE;
using static TerraFX.Interop.DirectX.D3D12_ROOT_SIGNATURE_FLAGS;
using static TerraFX.Interop.DirectX.D3D12_SHADER_VISIBILITY;
using static TerraFX.Interop.DirectX.DirectX;
using static TerraFX.Interop.Windows.Windows;

namespace Crystal.Graphics.D3D12;

internal static unsafe class D3D12RootSignatures
{
    public const int GraphicsConstantBufferRegisters = 4;
    public const int ComputeConstantBufferRegisters = 8;
    public const int VertexDescriptorSlots = 16;
    public const int PixelDescriptorSlots = 32;
    public const int ComputeDescriptorSlots = 32;
    public const uint GraphicsVertexDescriptorIndexParameter = 2 * GraphicsConstantBufferRegisters;
    public const uint GraphicsPixelDescriptorIndexParameter = GraphicsVertexDescriptorIndexParameter + 1;
    public const uint ComputeDescriptorIndexParameter = ComputeConstantBufferRegisters;
    private const uint DescriptorIndexRegister = 0;
    private const uint DescriptorIndexSpace = 1;

    private const D3D12_ROOT_SIGNATURE_FLAGS GraphicsStageDenials =
        D3D12_ROOT_SIGNATURE_FLAG_DENY_HULL_SHADER_ROOT_ACCESS
        | D3D12_ROOT_SIGNATURE_FLAG_DENY_DOMAIN_SHADER_ROOT_ACCESS
        | D3D12_ROOT_SIGNATURE_FLAG_DENY_GEOMETRY_SHADER_ROOT_ACCESS
        | D3D12_ROOT_SIGNATURE_FLAG_DENY_AMPLIFICATION_SHADER_ROOT_ACCESS
        | D3D12_ROOT_SIGNATURE_FLAG_DENY_MESH_SHADER_ROOT_ACCESS;

    public static ID3D12RootSignature* Create(D3D12Device device, D3D12ShaderProgramInfo program, string name)
    {
        var parameters = new D3D12_ROOT_PARAMETER1[program.ConstantBuffers.Length + program.Resources.Length];
        var ranges = new D3D12_DESCRIPTOR_RANGE1[program.Resources.Length];
        fixed (D3D12_ROOT_PARAMETER1* parameterPointer = parameters)
        fixed (D3D12_DESCRIPTOR_RANGE1* rangePointer = ranges)
        {
            for (var index = 0; index < program.ConstantBuffers.Length; index++)
            {
                var buffer = program.ConstantBuffers[index];
                parameterPointer[index] = ConstantBufferParameter((uint)buffer.Register, (uint)buffer.Space, ToD3D12(buffer.Visibility));
            }

            for (var index = 0; index < program.Resources.Length; index++)
            {
                var resource = program.Resources[index];
                rangePointer[index] = new D3D12_DESCRIPTOR_RANGE1
                {
                    RangeType = RangeType(resource.Kind),
                    NumDescriptors = 1,
                    BaseShaderRegister = (uint)resource.Register,
                    RegisterSpace = (uint)resource.Space,
                    Flags = D3D12_DESCRIPTOR_RANGE_FLAG_NONE,
                    OffsetInDescriptorsFromTableStart = 0,
                };
                var parameterIndex = program.ConstantBuffers.Length + index;
                parameterPointer[parameterIndex] = new D3D12_ROOT_PARAMETER1
                {
                    ParameterType = D3D12_ROOT_PARAMETER_TYPE_DESCRIPTOR_TABLE,
                    ShaderVisibility = ToD3D12(resource.Visibility),
                };
                parameterPointer[parameterIndex].DescriptorTable = new D3D12_ROOT_DESCRIPTOR_TABLE1 { NumDescriptorRanges = 1, pDescriptorRanges = rangePointer + index };
            }

            var flags = D3D12_ROOT_SIGNATURE_FLAG_ALLOW_INPUT_ASSEMBLER_INPUT_LAYOUT | GraphicsStageDenials;
            if (program.PixelShader < 0) flags |= D3D12_ROOT_SIGNATURE_FLAG_DENY_PIXEL_SHADER_ROOT_ACCESS;
            if (program.ComputeShader >= 0) flags = D3D12_ROOT_SIGNATURE_FLAG_NONE;
            return Create(device, parameterPointer, parameters.Length, flags, name, $"Program '{program.Name}'");
        }
    }

    public static ID3D12RootSignature* CreateDescriptorIndexedGraphics(D3D12Device device)
    {
        const int parameterCount = 2 * GraphicsConstantBufferRegisters + 2;
        var parameters = stackalloc D3D12_ROOT_PARAMETER1[parameterCount];
        for (var register = 0u; register < GraphicsConstantBufferRegisters; register++)
        {
            parameters[register] = ConstantBufferParameter(register, 0, D3D12_SHADER_VISIBILITY_VERTEX);
            parameters[GraphicsConstantBufferRegisters + register] = ConstantBufferParameter(register, 0, D3D12_SHADER_VISIBILITY_PIXEL);
        }
        parameters[GraphicsVertexDescriptorIndexParameter] = DescriptorIndexParameter(VertexDescriptorSlots, D3D12_SHADER_VISIBILITY_VERTEX);
        parameters[GraphicsPixelDescriptorIndexParameter] = DescriptorIndexParameter(PixelDescriptorSlots, D3D12_SHADER_VISIBILITY_PIXEL);
        var flags = D3D12_ROOT_SIGNATURE_FLAG_ALLOW_INPUT_ASSEMBLER_INPUT_LAYOUT | GraphicsStageDenials
            | D3D12_ROOT_SIGNATURE_FLAG_CBV_SRV_UAV_HEAP_DIRECTLY_INDEXED | D3D12_ROOT_SIGNATURE_FLAG_SAMPLER_HEAP_DIRECTLY_INDEXED;
        return Create(device, parameters, parameterCount, flags, "CRYSTAL_DescriptorIndexedGraphicsRootSignature", "The descriptor-indexed graphics layout");
    }

    public static ID3D12RootSignature* CreateDescriptorIndexedCompute(D3D12Device device)
    {
        const int parameterCount = ComputeConstantBufferRegisters + 1;
        var parameters = stackalloc D3D12_ROOT_PARAMETER1[parameterCount];
        for (var register = 0u; register < ComputeConstantBufferRegisters; register++)
            parameters[register] = ConstantBufferParameter(register, 0, D3D12_SHADER_VISIBILITY_ALL);
        parameters[ComputeDescriptorIndexParameter] = DescriptorIndexParameter(ComputeDescriptorSlots, D3D12_SHADER_VISIBILITY_ALL);
        var flags = D3D12_ROOT_SIGNATURE_FLAG_CBV_SRV_UAV_HEAP_DIRECTLY_INDEXED | D3D12_ROOT_SIGNATURE_FLAG_SAMPLER_HEAP_DIRECTLY_INDEXED;
        return Create(device, parameters, parameterCount, flags, "CRYSTAL_DescriptorIndexedComputeRootSignature", "The descriptor-indexed compute layout");
    }

    private static D3D12_ROOT_PARAMETER1 ConstantBufferParameter(uint register, uint space, D3D12_SHADER_VISIBILITY visibility)
    {
        var parameter = new D3D12_ROOT_PARAMETER1 { ParameterType = D3D12_ROOT_PARAMETER_TYPE_CBV, ShaderVisibility = visibility };
        parameter.Descriptor = new D3D12_ROOT_DESCRIPTOR1 { ShaderRegister = register, RegisterSpace = space, Flags = D3D12_ROOT_DESCRIPTOR_FLAG_DATA_STATIC_WHILE_SET_AT_EXECUTE };
        return parameter;
    }

    private static D3D12_ROOT_PARAMETER1 DescriptorIndexParameter(int slots, D3D12_SHADER_VISIBILITY visibility)
    {
        var parameter = new D3D12_ROOT_PARAMETER1 { ParameterType = D3D12_ROOT_PARAMETER_TYPE_32BIT_CONSTANTS, ShaderVisibility = visibility };
        parameter.Constants = new D3D12_ROOT_CONSTANTS { ShaderRegister = DescriptorIndexRegister, RegisterSpace = DescriptorIndexSpace, Num32BitValues = (uint)slots };
        return parameter;
    }

    private static ID3D12RootSignature* Create(
        D3D12Device device, D3D12_ROOT_PARAMETER1* parameters, int parameterCount, D3D12_ROOT_SIGNATURE_FLAGS flags, string name, string owner)
    {
        ID3DBlob* serialized = null;
        ID3DBlob* errors = null;
        try
        {
            var description = new D3D12_VERSIONED_ROOT_SIGNATURE_DESC { Version = D3D_ROOT_SIGNATURE_VERSION_1_1 };
            description.Desc_1_1 = new D3D12_ROOT_SIGNATURE_DESC1 { NumParameters = (uint)parameterCount, pParameters = parameters, Flags = flags };
            var result = D3D12SerializeVersionedRootSignature(&description, &serialized, &errors);
            if (FAILED(result))
            {
                var message = errors == null ? "" : Encoding.UTF8.GetString((byte*)errors->GetBufferPointer(), (int)errors->GetBufferSize()).TrimEnd('\0', '\n');
                throw new GraphicsException($"{owner} has an invalid root signature (HRESULT 0x{result.Value:X8}): {message}");
            }

            ID3D12RootSignature* rootSignature;
            ThrowOnFailure(
                device.Device->CreateRootSignature(0, serialized->GetBufferPointer(), serialized->GetBufferSize(), __uuidof<ID3D12RootSignature>(), (void**)&rootSignature),
                $"ID3D12Device::CreateRootSignature({name})");
            SetName((ID3D12Object*)rootSignature, name);
            return rootSignature;
        }
        finally
        {
            Release(ref serialized);
            Release(ref errors);
        }
    }

    private static D3D12_DESCRIPTOR_RANGE_TYPE RangeType(D3D12ShaderResourceKind kind) => kind switch
    {
        D3D12ShaderResourceKind.Sampler => D3D12_DESCRIPTOR_RANGE_TYPE_SAMPLER,
        D3D12ShaderResourceKind.ReadWriteTexture or D3D12ShaderResourceKind.ReadWriteStructuredBuffer or D3D12ShaderResourceKind.ReadWriteByteAddressBuffer
            => D3D12_DESCRIPTOR_RANGE_TYPE_UAV,
        _ => D3D12_DESCRIPTOR_RANGE_TYPE_SRV,
    };

    private static D3D12_SHADER_VISIBILITY ToD3D12(D3D12ShaderVisibility visibility) => visibility switch
    {
        D3D12ShaderVisibility.Vertex => D3D12_SHADER_VISIBILITY_VERTEX,
        D3D12ShaderVisibility.Pixel => D3D12_SHADER_VISIBILITY_PIXEL,
        _ => D3D12_SHADER_VISIBILITY_ALL,
    };
}
