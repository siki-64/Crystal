#pragma warning disable CS0649
using System.Runtime.InteropServices;

namespace Crystal.Interop;

internal static unsafe partial class D3DCompiler
{
    [DllImport("d3dcompiler_47", CallingConvention = CallingConvention.StdCall, ExactSpelling = true)]
    [return: NativeTypeName("HRESULT")]
    public static extern int D3DCompile([NativeTypeName("LPCVOID")] void* pSrcData, [NativeTypeName("SIZE_T")] nuint SrcDataSize, [NativeTypeName("LPCSTR")] sbyte* pSourceName, [NativeTypeName("const D3D_SHADER_MACRO *")] D3D_SHADER_MACRO* pDefines, ID3DInclude* pInclude, [NativeTypeName("LPCSTR")] sbyte* pEntrypoint, [NativeTypeName("LPCSTR")] sbyte* pTarget, uint Flags1, uint Flags2, [NativeTypeName("ID3DBlob **")] ID3D10Blob** ppCode, [NativeTypeName("ID3DBlob **")] ID3D10Blob** ppErrorMsgs);
}
