using System.Runtime.CompilerServices;
using TerraFX.Interop.DirectX;
using TerraFX.Interop.Windows;
using static TerraFX.Interop.Windows.Windows;

namespace Crystal.Graphics.D3D12;

internal static unsafe class D3D12Interop
{
    public static Guid* AddressOf(in Guid guid) => (Guid*)Unsafe.AsPointer(ref Unsafe.AsRef(in guid));

    public static void ThrowOnFailure(HRESULT result, string operation)
    {
        if (FAILED(result)) throw new GraphicsException($"{operation} failed with HRESULT 0x{result.Value:X8}.");
    }

    public static void Release<T>(ref T* pointer) where T : unmanaged, IUnknown.Interface
    {
        if (pointer == null) return;
        pointer->Release();
        pointer = null;
    }

    public static void SetName(ID3D12Object* d3d12Object, string name)
    {
        fixed (char* namePointer = "CRYSTAL_" + name) d3d12Object->SetName(namePointer);
    }
}
