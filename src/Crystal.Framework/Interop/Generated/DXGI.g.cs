#pragma warning disable CS0649
using System;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using static Crystal.Interop.DXGI;

namespace Crystal.Interop;

internal partial struct DXGI_RATIONAL
{
    public uint Numerator;

    public uint Denominator;
}

internal partial struct DXGI_SAMPLE_DESC
{
    public uint Count;

    public uint Quality;
}

internal enum DXGI_COLOR_SPACE_TYPE
{
    DXGI_COLOR_SPACE_RGB_FULL_G22_NONE_P709 = 0,
    DXGI_COLOR_SPACE_RGB_FULL_G10_NONE_P709 = 1,
    DXGI_COLOR_SPACE_RGB_STUDIO_G22_NONE_P709 = 2,
    DXGI_COLOR_SPACE_RGB_STUDIO_G22_NONE_P2020 = 3,
    DXGI_COLOR_SPACE_RESERVED = 4,
    DXGI_COLOR_SPACE_YCBCR_FULL_G22_NONE_P709_X601 = 5,
    DXGI_COLOR_SPACE_YCBCR_STUDIO_G22_LEFT_P601 = 6,
    DXGI_COLOR_SPACE_YCBCR_FULL_G22_LEFT_P601 = 7,
    DXGI_COLOR_SPACE_YCBCR_STUDIO_G22_LEFT_P709 = 8,
    DXGI_COLOR_SPACE_YCBCR_FULL_G22_LEFT_P709 = 9,
    DXGI_COLOR_SPACE_YCBCR_STUDIO_G22_LEFT_P2020 = 10,
    DXGI_COLOR_SPACE_YCBCR_FULL_G22_LEFT_P2020 = 11,
    DXGI_COLOR_SPACE_RGB_FULL_G2084_NONE_P2020 = 12,
    DXGI_COLOR_SPACE_YCBCR_STUDIO_G2084_LEFT_P2020 = 13,
    DXGI_COLOR_SPACE_RGB_STUDIO_G2084_NONE_P2020 = 14,
    DXGI_COLOR_SPACE_YCBCR_STUDIO_G22_TOPLEFT_P2020 = 15,
    DXGI_COLOR_SPACE_YCBCR_STUDIO_G2084_TOPLEFT_P2020 = 16,
    DXGI_COLOR_SPACE_RGB_FULL_G22_NONE_P2020 = 17,
    DXGI_COLOR_SPACE_YCBCR_STUDIO_GHLG_TOPLEFT_P2020 = 18,
    DXGI_COLOR_SPACE_YCBCR_FULL_GHLG_TOPLEFT_P2020 = 19,
    DXGI_COLOR_SPACE_RGB_STUDIO_G24_NONE_P709 = 20,
    DXGI_COLOR_SPACE_RGB_STUDIO_G24_NONE_P2020 = 21,
    DXGI_COLOR_SPACE_YCBCR_STUDIO_G24_LEFT_P709 = 22,
    DXGI_COLOR_SPACE_YCBCR_STUDIO_G24_LEFT_P2020 = 23,
    DXGI_COLOR_SPACE_YCBCR_STUDIO_G24_TOPLEFT_P2020 = 24,
    DXGI_COLOR_SPACE_CUSTOM = unchecked((int)(0xFFFFFFFF)),
}

internal partial struct DXGI_RGB
{
    public float Red;

    public float Green;

    public float Blue;
}

internal partial struct D3DCOLORVALUE
{
    public float r;

    public float g;

    public float b;

    public float a;
}

internal partial struct DXGI_GAMMA_CONTROL
{
    public DXGI_RGB Scale;

    public DXGI_RGB Offset;

    [NativeTypeName("DXGI_RGB[1025]")]
    public _GammaCurve_e__FixedBuffer GammaCurve;

    [InlineArray(1025)]
    public partial struct _GammaCurve_e__FixedBuffer
    {
        public DXGI_RGB e0;
    }
}

internal partial struct DXGI_GAMMA_CONTROL_CAPABILITIES
{
    [NativeTypeName("BOOL")]
    public int ScaleAndOffsetSupported;

    public float MaxConvertedValue;

    public float MinConvertedValue;

    public uint NumGammaControlPoints;

    [NativeTypeName("float[1025]")]
    public _ControlPointPositions_e__FixedBuffer ControlPointPositions;

    [InlineArray(1025)]
    public partial struct _ControlPointPositions_e__FixedBuffer
    {
        public float e0;
    }
}

internal enum DXGI_MODE_SCANLINE_ORDER
{
    DXGI_MODE_SCANLINE_ORDER_UNSPECIFIED = 0,
    DXGI_MODE_SCANLINE_ORDER_PROGRESSIVE = 1,
    DXGI_MODE_SCANLINE_ORDER_UPPER_FIELD_FIRST = 2,
    DXGI_MODE_SCANLINE_ORDER_LOWER_FIELD_FIRST = 3,
}

internal enum DXGI_MODE_SCALING
{
    DXGI_MODE_SCALING_UNSPECIFIED = 0,
    DXGI_MODE_SCALING_CENTERED = 1,
    DXGI_MODE_SCALING_STRETCHED = 2,
}

internal enum DXGI_MODE_ROTATION
{
    DXGI_MODE_ROTATION_UNSPECIFIED = 0,
    DXGI_MODE_ROTATION_IDENTITY = 1,
    DXGI_MODE_ROTATION_ROTATE90 = 2,
    DXGI_MODE_ROTATION_ROTATE180 = 3,
    DXGI_MODE_ROTATION_ROTATE270 = 4,
}

internal partial struct DXGI_MODE_DESC
{
    public uint Width;

    public uint Height;

    public DXGI_RATIONAL RefreshRate;

    public DXGI_FORMAT Format;

    public DXGI_MODE_SCANLINE_ORDER ScanlineOrdering;

    public DXGI_MODE_SCALING Scaling;
}

internal partial struct DXGI_JPEG_DC_HUFFMAN_TABLE
{
    [NativeTypeName("BYTE[12]")]
    public _CodeCounts_e__FixedBuffer CodeCounts;

    [NativeTypeName("BYTE[12]")]
    public _CodeValues_e__FixedBuffer CodeValues;

    [InlineArray(12)]
    public partial struct _CodeCounts_e__FixedBuffer
    {
        public byte e0;
    }

    [InlineArray(12)]
    public partial struct _CodeValues_e__FixedBuffer
    {
        public byte e0;
    }
}

internal partial struct DXGI_JPEG_AC_HUFFMAN_TABLE
{
    [NativeTypeName("BYTE[16]")]
    public _CodeCounts_e__FixedBuffer CodeCounts;

    [NativeTypeName("BYTE[162]")]
    public _CodeValues_e__FixedBuffer CodeValues;

    [InlineArray(16)]
    public partial struct _CodeCounts_e__FixedBuffer
    {
        public byte e0;
    }

    [InlineArray(162)]
    public partial struct _CodeValues_e__FixedBuffer
    {
        public byte e0;
    }
}

internal partial struct DXGI_JPEG_QUANTIZATION_TABLE
{
    [NativeTypeName("BYTE[64]")]
    public _Elements_e__FixedBuffer Elements;

    [InlineArray(64)]
    public partial struct _Elements_e__FixedBuffer
    {
        public byte e0;
    }
}

internal partial struct DXGI_FRAME_STATISTICS
{
    public uint PresentCount;

    public uint PresentRefreshCount;

    public uint SyncRefreshCount;

    public LARGE_INTEGER SyncQPCTime;

    public LARGE_INTEGER SyncGPUTime;
}

internal unsafe partial struct DXGI_MAPPED_RECT
{
    public int Pitch;

    public byte* pBits;
}

internal partial struct DXGI_ADAPTER_DESC
{
    [NativeTypeName("WCHAR[128]")]
    public _Description_e__FixedBuffer Description;

    public uint VendorId;

    public uint DeviceId;

    public uint SubSysId;

    public uint Revision;

    [NativeTypeName("SIZE_T")]
    public nuint DedicatedVideoMemory;

    [NativeTypeName("SIZE_T")]
    public nuint DedicatedSystemMemory;

    [NativeTypeName("SIZE_T")]
    public nuint SharedSystemMemory;

    public LUID AdapterLuid;

    [InlineArray(128)]
    public partial struct _Description_e__FixedBuffer
    {
        public ushort e0;
    }
}

internal unsafe partial struct DXGI_OUTPUT_DESC
{
    [NativeTypeName("WCHAR[32]")]
    public _DeviceName_e__FixedBuffer DeviceName;

    public RECT DesktopCoordinates;

    [NativeTypeName("BOOL")]
    public int AttachedToDesktop;

    public DXGI_MODE_ROTATION Rotation;

    [NativeTypeName("HMONITOR")]
    public HMONITOR__* Monitor;

    [InlineArray(32)]
    public partial struct _DeviceName_e__FixedBuffer
    {
        public ushort e0;
    }
}

internal unsafe partial struct DXGI_SHARED_RESOURCE
{
    [NativeTypeName("HANDLE")]
    public void* Handle;
}

internal enum DXGI_RESIDENCY
{
    DXGI_RESIDENCY_FULLY_RESIDENT = 1,
    DXGI_RESIDENCY_RESIDENT_IN_SHARED_MEMORY = 2,
    DXGI_RESIDENCY_EVICTED_TO_DISK = 3,
}

internal partial struct DXGI_SURFACE_DESC
{
    public uint Width;

    public uint Height;

    public DXGI_FORMAT Format;

    public DXGI_SAMPLE_DESC SampleDesc;
}

internal enum DXGI_SWAP_EFFECT
{
    DXGI_SWAP_EFFECT_DISCARD = 0,
    DXGI_SWAP_EFFECT_SEQUENTIAL = 1,
    DXGI_SWAP_EFFECT_FLIP_SEQUENTIAL = 3,
    DXGI_SWAP_EFFECT_FLIP_DISCARD = 4,
}

internal enum DXGI_SWAP_CHAIN_FLAG
{
    DXGI_SWAP_CHAIN_FLAG_NONPREROTATED = 1,
    DXGI_SWAP_CHAIN_FLAG_ALLOW_MODE_SWITCH = 2,
    DXGI_SWAP_CHAIN_FLAG_GDI_COMPATIBLE = 4,
    DXGI_SWAP_CHAIN_FLAG_RESTRICTED_CONTENT = 8,
    DXGI_SWAP_CHAIN_FLAG_RESTRICT_SHARED_RESOURCE_DRIVER = 16,
    DXGI_SWAP_CHAIN_FLAG_DISPLAY_ONLY = 32,
    DXGI_SWAP_CHAIN_FLAG_FRAME_LATENCY_WAITABLE_OBJECT = 64,
    DXGI_SWAP_CHAIN_FLAG_FOREGROUND_LAYER = 128,
    DXGI_SWAP_CHAIN_FLAG_FULLSCREEN_VIDEO = 256,
    DXGI_SWAP_CHAIN_FLAG_YUV_VIDEO = 512,
    DXGI_SWAP_CHAIN_FLAG_HW_PROTECTED = 1024,
    DXGI_SWAP_CHAIN_FLAG_ALLOW_TEARING = 2048,
    DXGI_SWAP_CHAIN_FLAG_RESTRICTED_TO_ALL_HOLOGRAPHIC_DISPLAYS = 4096,
}

internal unsafe partial struct DXGI_SWAP_CHAIN_DESC
{
    public DXGI_MODE_DESC BufferDesc;

    public DXGI_SAMPLE_DESC SampleDesc;

    [NativeTypeName("DXGI_USAGE")]
    public uint BufferUsage;

    public uint BufferCount;

    [NativeTypeName("HWND")]
    public HWND__* OutputWindow;

    [NativeTypeName("BOOL")]
    public int Windowed;

    public DXGI_SWAP_EFFECT SwapEffect;

    public uint Flags;
}

[Guid("AEC22FB8-76F3-4639-9BE0-28EB43A67A2E")]
[NativeTypeName("struct IDXGIObject : IUnknown")]
internal unsafe partial struct IDXGIObject : IDXGIObject.Interface, INativeGuid
{
    static Guid* INativeGuid.NativeGuid => (Guid*)Unsafe.AsPointer(in IID_IDXGIObject);

    public void** lpVtbl;

    [return: NativeTypeName("HRESULT")]
    public int QueryInterface([NativeTypeName("const IID &")] Guid* riid, void** ppvObject)
    {
        return ((delegate* unmanaged[Stdcall]<IDXGIObject*, Guid*, void**, int>)(lpVtbl[0]))((IDXGIObject*)Unsafe.AsPointer(ref this), riid, ppvObject);
    }

    [return: NativeTypeName("ULONG")]
    public uint AddRef()
    {
        return ((delegate* unmanaged[Stdcall]<IDXGIObject*, uint>)(lpVtbl[1]))((IDXGIObject*)Unsafe.AsPointer(ref this));
    }

    [return: NativeTypeName("ULONG")]
    public uint Release()
    {
        return ((delegate* unmanaged[Stdcall]<IDXGIObject*, uint>)(lpVtbl[2]))((IDXGIObject*)Unsafe.AsPointer(ref this));
    }

    [return: NativeTypeName("HRESULT")]
    public int SetPrivateData([NativeTypeName("const GUID &")] Guid* Name, uint DataSize, [NativeTypeName("const void *")] void* pData)
    {
        return ((delegate* unmanaged[Stdcall]<IDXGIObject*, Guid*, uint, void*, int>)(lpVtbl[3]))((IDXGIObject*)Unsafe.AsPointer(ref this), Name, DataSize, pData);
    }

    [return: NativeTypeName("HRESULT")]
    public int SetPrivateDataInterface([NativeTypeName("const GUID &")] Guid* Name, [NativeTypeName("const IUnknown *")] IUnknown* pUnknown)
    {
        return ((delegate* unmanaged[Stdcall]<IDXGIObject*, Guid*, IUnknown*, int>)(lpVtbl[4]))((IDXGIObject*)Unsafe.AsPointer(ref this), Name, pUnknown);
    }

    [return: NativeTypeName("HRESULT")]
    public int GetPrivateData([NativeTypeName("const GUID &")] Guid* Name, uint* pDataSize, void* pData)
    {
        return ((delegate* unmanaged[Stdcall]<IDXGIObject*, Guid*, uint*, void*, int>)(lpVtbl[5]))((IDXGIObject*)Unsafe.AsPointer(ref this), Name, pDataSize, pData);
    }

    [return: NativeTypeName("HRESULT")]
    public int GetParent([NativeTypeName("const IID &")] Guid* riid, void** ppParent)
    {
        return ((delegate* unmanaged[Stdcall]<IDXGIObject*, Guid*, void**, int>)(lpVtbl[6]))((IDXGIObject*)Unsafe.AsPointer(ref this), riid, ppParent);
    }

    public interface Interface : IUnknown.Interface
    {
        [return: NativeTypeName("HRESULT")]
        int SetPrivateData([NativeTypeName("const GUID &")] Guid* Name, uint DataSize, [NativeTypeName("const void *")] void* pData);

        [return: NativeTypeName("HRESULT")]
        int SetPrivateDataInterface([NativeTypeName("const GUID &")] Guid* Name, [NativeTypeName("const IUnknown *")] IUnknown* pUnknown);

        [return: NativeTypeName("HRESULT")]
        int GetPrivateData([NativeTypeName("const GUID &")] Guid* Name, uint* pDataSize, void* pData);

        [return: NativeTypeName("HRESULT")]
        int GetParent([NativeTypeName("const IID &")] Guid* riid, void** ppParent);
    }
}

[Guid("3D3E0379-F9DE-4D58-BB6C-18D62992F1A6")]
[NativeTypeName("struct IDXGIDeviceSubObject : IDXGIObject")]
internal unsafe partial struct IDXGIDeviceSubObject : IDXGIDeviceSubObject.Interface, INativeGuid
{
    static Guid* INativeGuid.NativeGuid => (Guid*)Unsafe.AsPointer(in IID_IDXGIDeviceSubObject);

    public void** lpVtbl;

    [return: NativeTypeName("HRESULT")]
    public int QueryInterface([NativeTypeName("const IID &")] Guid* riid, void** ppvObject)
    {
        return ((delegate* unmanaged[Stdcall]<IDXGIDeviceSubObject*, Guid*, void**, int>)(lpVtbl[0]))((IDXGIDeviceSubObject*)Unsafe.AsPointer(ref this), riid, ppvObject);
    }

    [return: NativeTypeName("ULONG")]
    public uint AddRef()
    {
        return ((delegate* unmanaged[Stdcall]<IDXGIDeviceSubObject*, uint>)(lpVtbl[1]))((IDXGIDeviceSubObject*)Unsafe.AsPointer(ref this));
    }

    [return: NativeTypeName("ULONG")]
    public uint Release()
    {
        return ((delegate* unmanaged[Stdcall]<IDXGIDeviceSubObject*, uint>)(lpVtbl[2]))((IDXGIDeviceSubObject*)Unsafe.AsPointer(ref this));
    }

    [return: NativeTypeName("HRESULT")]
    public int SetPrivateData([NativeTypeName("const GUID &")] Guid* Name, uint DataSize, [NativeTypeName("const void *")] void* pData)
    {
        return ((delegate* unmanaged[Stdcall]<IDXGIDeviceSubObject*, Guid*, uint, void*, int>)(lpVtbl[3]))((IDXGIDeviceSubObject*)Unsafe.AsPointer(ref this), Name, DataSize, pData);
    }

    [return: NativeTypeName("HRESULT")]
    public int SetPrivateDataInterface([NativeTypeName("const GUID &")] Guid* Name, [NativeTypeName("const IUnknown *")] IUnknown* pUnknown)
    {
        return ((delegate* unmanaged[Stdcall]<IDXGIDeviceSubObject*, Guid*, IUnknown*, int>)(lpVtbl[4]))((IDXGIDeviceSubObject*)Unsafe.AsPointer(ref this), Name, pUnknown);
    }

    [return: NativeTypeName("HRESULT")]
    public int GetPrivateData([NativeTypeName("const GUID &")] Guid* Name, uint* pDataSize, void* pData)
    {
        return ((delegate* unmanaged[Stdcall]<IDXGIDeviceSubObject*, Guid*, uint*, void*, int>)(lpVtbl[5]))((IDXGIDeviceSubObject*)Unsafe.AsPointer(ref this), Name, pDataSize, pData);
    }

    [return: NativeTypeName("HRESULT")]
    public int GetParent([NativeTypeName("const IID &")] Guid* riid, void** ppParent)
    {
        return ((delegate* unmanaged[Stdcall]<IDXGIDeviceSubObject*, Guid*, void**, int>)(lpVtbl[6]))((IDXGIDeviceSubObject*)Unsafe.AsPointer(ref this), riid, ppParent);
    }

    [return: NativeTypeName("HRESULT")]
    public int GetDevice([NativeTypeName("const IID &")] Guid* riid, void** ppDevice)
    {
        return ((delegate* unmanaged[Stdcall]<IDXGIDeviceSubObject*, Guid*, void**, int>)(lpVtbl[7]))((IDXGIDeviceSubObject*)Unsafe.AsPointer(ref this), riid, ppDevice);
    }

    public interface Interface : IDXGIObject.Interface
    {
        [return: NativeTypeName("HRESULT")]
        int GetDevice([NativeTypeName("const IID &")] Guid* riid, void** ppDevice);
    }
}

[Guid("035F3AB4-482E-4E50-B41F-8A7F8BD8960B")]
[NativeTypeName("struct IDXGIResource : IDXGIDeviceSubObject")]
internal unsafe partial struct IDXGIResource : IDXGIResource.Interface, INativeGuid
{
    static Guid* INativeGuid.NativeGuid => (Guid*)Unsafe.AsPointer(in IID_IDXGIResource);

    public void** lpVtbl;

    [return: NativeTypeName("HRESULT")]
    public int QueryInterface([NativeTypeName("const IID &")] Guid* riid, void** ppvObject)
    {
        return ((delegate* unmanaged[Stdcall]<IDXGIResource*, Guid*, void**, int>)(lpVtbl[0]))((IDXGIResource*)Unsafe.AsPointer(ref this), riid, ppvObject);
    }

    [return: NativeTypeName("ULONG")]
    public uint AddRef()
    {
        return ((delegate* unmanaged[Stdcall]<IDXGIResource*, uint>)(lpVtbl[1]))((IDXGIResource*)Unsafe.AsPointer(ref this));
    }

    [return: NativeTypeName("ULONG")]
    public uint Release()
    {
        return ((delegate* unmanaged[Stdcall]<IDXGIResource*, uint>)(lpVtbl[2]))((IDXGIResource*)Unsafe.AsPointer(ref this));
    }

    [return: NativeTypeName("HRESULT")]
    public int SetPrivateData([NativeTypeName("const GUID &")] Guid* Name, uint DataSize, [NativeTypeName("const void *")] void* pData)
    {
        return ((delegate* unmanaged[Stdcall]<IDXGIResource*, Guid*, uint, void*, int>)(lpVtbl[3]))((IDXGIResource*)Unsafe.AsPointer(ref this), Name, DataSize, pData);
    }

    [return: NativeTypeName("HRESULT")]
    public int SetPrivateDataInterface([NativeTypeName("const GUID &")] Guid* Name, [NativeTypeName("const IUnknown *")] IUnknown* pUnknown)
    {
        return ((delegate* unmanaged[Stdcall]<IDXGIResource*, Guid*, IUnknown*, int>)(lpVtbl[4]))((IDXGIResource*)Unsafe.AsPointer(ref this), Name, pUnknown);
    }

    [return: NativeTypeName("HRESULT")]
    public int GetPrivateData([NativeTypeName("const GUID &")] Guid* Name, uint* pDataSize, void* pData)
    {
        return ((delegate* unmanaged[Stdcall]<IDXGIResource*, Guid*, uint*, void*, int>)(lpVtbl[5]))((IDXGIResource*)Unsafe.AsPointer(ref this), Name, pDataSize, pData);
    }

    [return: NativeTypeName("HRESULT")]
    public int GetParent([NativeTypeName("const IID &")] Guid* riid, void** ppParent)
    {
        return ((delegate* unmanaged[Stdcall]<IDXGIResource*, Guid*, void**, int>)(lpVtbl[6]))((IDXGIResource*)Unsafe.AsPointer(ref this), riid, ppParent);
    }

    [return: NativeTypeName("HRESULT")]
    public int GetDevice([NativeTypeName("const IID &")] Guid* riid, void** ppDevice)
    {
        return ((delegate* unmanaged[Stdcall]<IDXGIResource*, Guid*, void**, int>)(lpVtbl[7]))((IDXGIResource*)Unsafe.AsPointer(ref this), riid, ppDevice);
    }

    [return: NativeTypeName("HRESULT")]
    public int GetSharedHandle([NativeTypeName("HANDLE *")] void** pSharedHandle)
    {
        return ((delegate* unmanaged[Stdcall]<IDXGIResource*, void**, int>)(lpVtbl[8]))((IDXGIResource*)Unsafe.AsPointer(ref this), pSharedHandle);
    }

    [return: NativeTypeName("HRESULT")]
    public int GetUsage([NativeTypeName("DXGI_USAGE *")] uint* pUsage)
    {
        return ((delegate* unmanaged[Stdcall]<IDXGIResource*, uint*, int>)(lpVtbl[9]))((IDXGIResource*)Unsafe.AsPointer(ref this), pUsage);
    }

    [return: NativeTypeName("HRESULT")]
    public int SetEvictionPriority(uint EvictionPriority)
    {
        return ((delegate* unmanaged[Stdcall]<IDXGIResource*, uint, int>)(lpVtbl[10]))((IDXGIResource*)Unsafe.AsPointer(ref this), EvictionPriority);
    }

    [return: NativeTypeName("HRESULT")]
    public int GetEvictionPriority(uint* pEvictionPriority)
    {
        return ((delegate* unmanaged[Stdcall]<IDXGIResource*, uint*, int>)(lpVtbl[11]))((IDXGIResource*)Unsafe.AsPointer(ref this), pEvictionPriority);
    }

    public interface Interface : IDXGIDeviceSubObject.Interface
    {
        [return: NativeTypeName("HRESULT")]
        int GetSharedHandle([NativeTypeName("HANDLE *")] void** pSharedHandle);

        [return: NativeTypeName("HRESULT")]
        int GetUsage([NativeTypeName("DXGI_USAGE *")] uint* pUsage);

        [return: NativeTypeName("HRESULT")]
        int SetEvictionPriority(uint EvictionPriority);

        [return: NativeTypeName("HRESULT")]
        int GetEvictionPriority(uint* pEvictionPriority);
    }
}

[Guid("9D8E1289-D7B3-465F-8126-250E349AF85D")]
[NativeTypeName("struct IDXGIKeyedMutex : IDXGIDeviceSubObject")]
internal unsafe partial struct IDXGIKeyedMutex : IDXGIKeyedMutex.Interface, INativeGuid
{
    static Guid* INativeGuid.NativeGuid => (Guid*)Unsafe.AsPointer(in IID_IDXGIKeyedMutex);

    public void** lpVtbl;

    [return: NativeTypeName("HRESULT")]
    public int QueryInterface([NativeTypeName("const IID &")] Guid* riid, void** ppvObject)
    {
        return ((delegate* unmanaged[Stdcall]<IDXGIKeyedMutex*, Guid*, void**, int>)(lpVtbl[0]))((IDXGIKeyedMutex*)Unsafe.AsPointer(ref this), riid, ppvObject);
    }

    [return: NativeTypeName("ULONG")]
    public uint AddRef()
    {
        return ((delegate* unmanaged[Stdcall]<IDXGIKeyedMutex*, uint>)(lpVtbl[1]))((IDXGIKeyedMutex*)Unsafe.AsPointer(ref this));
    }

    [return: NativeTypeName("ULONG")]
    public uint Release()
    {
        return ((delegate* unmanaged[Stdcall]<IDXGIKeyedMutex*, uint>)(lpVtbl[2]))((IDXGIKeyedMutex*)Unsafe.AsPointer(ref this));
    }

    [return: NativeTypeName("HRESULT")]
    public int SetPrivateData([NativeTypeName("const GUID &")] Guid* Name, uint DataSize, [NativeTypeName("const void *")] void* pData)
    {
        return ((delegate* unmanaged[Stdcall]<IDXGIKeyedMutex*, Guid*, uint, void*, int>)(lpVtbl[3]))((IDXGIKeyedMutex*)Unsafe.AsPointer(ref this), Name, DataSize, pData);
    }

    [return: NativeTypeName("HRESULT")]
    public int SetPrivateDataInterface([NativeTypeName("const GUID &")] Guid* Name, [NativeTypeName("const IUnknown *")] IUnknown* pUnknown)
    {
        return ((delegate* unmanaged[Stdcall]<IDXGIKeyedMutex*, Guid*, IUnknown*, int>)(lpVtbl[4]))((IDXGIKeyedMutex*)Unsafe.AsPointer(ref this), Name, pUnknown);
    }

    [return: NativeTypeName("HRESULT")]
    public int GetPrivateData([NativeTypeName("const GUID &")] Guid* Name, uint* pDataSize, void* pData)
    {
        return ((delegate* unmanaged[Stdcall]<IDXGIKeyedMutex*, Guid*, uint*, void*, int>)(lpVtbl[5]))((IDXGIKeyedMutex*)Unsafe.AsPointer(ref this), Name, pDataSize, pData);
    }

    [return: NativeTypeName("HRESULT")]
    public int GetParent([NativeTypeName("const IID &")] Guid* riid, void** ppParent)
    {
        return ((delegate* unmanaged[Stdcall]<IDXGIKeyedMutex*, Guid*, void**, int>)(lpVtbl[6]))((IDXGIKeyedMutex*)Unsafe.AsPointer(ref this), riid, ppParent);
    }

    [return: NativeTypeName("HRESULT")]
    public int GetDevice([NativeTypeName("const IID &")] Guid* riid, void** ppDevice)
    {
        return ((delegate* unmanaged[Stdcall]<IDXGIKeyedMutex*, Guid*, void**, int>)(lpVtbl[7]))((IDXGIKeyedMutex*)Unsafe.AsPointer(ref this), riid, ppDevice);
    }

    [return: NativeTypeName("HRESULT")]
    public int AcquireSync([NativeTypeName("UINT64")] ulong Key, [NativeTypeName("DWORD")] uint dwMilliseconds)
    {
        return ((delegate* unmanaged[Stdcall]<IDXGIKeyedMutex*, ulong, uint, int>)(lpVtbl[8]))((IDXGIKeyedMutex*)Unsafe.AsPointer(ref this), Key, dwMilliseconds);
    }

    [return: NativeTypeName("HRESULT")]
    public int ReleaseSync([NativeTypeName("UINT64")] ulong Key)
    {
        return ((delegate* unmanaged[Stdcall]<IDXGIKeyedMutex*, ulong, int>)(lpVtbl[9]))((IDXGIKeyedMutex*)Unsafe.AsPointer(ref this), Key);
    }

    public interface Interface : IDXGIDeviceSubObject.Interface
    {
        [return: NativeTypeName("HRESULT")]
        int AcquireSync([NativeTypeName("UINT64")] ulong Key, [NativeTypeName("DWORD")] uint dwMilliseconds);

        [return: NativeTypeName("HRESULT")]
        int ReleaseSync([NativeTypeName("UINT64")] ulong Key);
    }
}

[Guid("CAFCB56C-6AC3-4889-BF47-9E23BBD260EC")]
[NativeTypeName("struct IDXGISurface : IDXGIDeviceSubObject")]
internal unsafe partial struct IDXGISurface : IDXGISurface.Interface, INativeGuid
{
    static Guid* INativeGuid.NativeGuid => (Guid*)Unsafe.AsPointer(in IID_IDXGISurface);

    public void** lpVtbl;

    [return: NativeTypeName("HRESULT")]
    public int QueryInterface([NativeTypeName("const IID &")] Guid* riid, void** ppvObject)
    {
        return ((delegate* unmanaged[Stdcall]<IDXGISurface*, Guid*, void**, int>)(lpVtbl[0]))((IDXGISurface*)Unsafe.AsPointer(ref this), riid, ppvObject);
    }

    [return: NativeTypeName("ULONG")]
    public uint AddRef()
    {
        return ((delegate* unmanaged[Stdcall]<IDXGISurface*, uint>)(lpVtbl[1]))((IDXGISurface*)Unsafe.AsPointer(ref this));
    }

    [return: NativeTypeName("ULONG")]
    public uint Release()
    {
        return ((delegate* unmanaged[Stdcall]<IDXGISurface*, uint>)(lpVtbl[2]))((IDXGISurface*)Unsafe.AsPointer(ref this));
    }

    [return: NativeTypeName("HRESULT")]
    public int SetPrivateData([NativeTypeName("const GUID &")] Guid* Name, uint DataSize, [NativeTypeName("const void *")] void* pData)
    {
        return ((delegate* unmanaged[Stdcall]<IDXGISurface*, Guid*, uint, void*, int>)(lpVtbl[3]))((IDXGISurface*)Unsafe.AsPointer(ref this), Name, DataSize, pData);
    }

    [return: NativeTypeName("HRESULT")]
    public int SetPrivateDataInterface([NativeTypeName("const GUID &")] Guid* Name, [NativeTypeName("const IUnknown *")] IUnknown* pUnknown)
    {
        return ((delegate* unmanaged[Stdcall]<IDXGISurface*, Guid*, IUnknown*, int>)(lpVtbl[4]))((IDXGISurface*)Unsafe.AsPointer(ref this), Name, pUnknown);
    }

    [return: NativeTypeName("HRESULT")]
    public int GetPrivateData([NativeTypeName("const GUID &")] Guid* Name, uint* pDataSize, void* pData)
    {
        return ((delegate* unmanaged[Stdcall]<IDXGISurface*, Guid*, uint*, void*, int>)(lpVtbl[5]))((IDXGISurface*)Unsafe.AsPointer(ref this), Name, pDataSize, pData);
    }

    [return: NativeTypeName("HRESULT")]
    public int GetParent([NativeTypeName("const IID &")] Guid* riid, void** ppParent)
    {
        return ((delegate* unmanaged[Stdcall]<IDXGISurface*, Guid*, void**, int>)(lpVtbl[6]))((IDXGISurface*)Unsafe.AsPointer(ref this), riid, ppParent);
    }

    [return: NativeTypeName("HRESULT")]
    public int GetDevice([NativeTypeName("const IID &")] Guid* riid, void** ppDevice)
    {
        return ((delegate* unmanaged[Stdcall]<IDXGISurface*, Guid*, void**, int>)(lpVtbl[7]))((IDXGISurface*)Unsafe.AsPointer(ref this), riid, ppDevice);
    }

    [return: NativeTypeName("HRESULT")]
    public int GetDesc(DXGI_SURFACE_DESC* pDesc)
    {
        return ((delegate* unmanaged[Stdcall]<IDXGISurface*, DXGI_SURFACE_DESC*, int>)(lpVtbl[8]))((IDXGISurface*)Unsafe.AsPointer(ref this), pDesc);
    }

    [return: NativeTypeName("HRESULT")]
    public int Map(DXGI_MAPPED_RECT* pLockedRect, uint MapFlags)
    {
        return ((delegate* unmanaged[Stdcall]<IDXGISurface*, DXGI_MAPPED_RECT*, uint, int>)(lpVtbl[9]))((IDXGISurface*)Unsafe.AsPointer(ref this), pLockedRect, MapFlags);
    }

    [return: NativeTypeName("HRESULT")]
    public int Unmap()
    {
        return ((delegate* unmanaged[Stdcall]<IDXGISurface*, int>)(lpVtbl[10]))((IDXGISurface*)Unsafe.AsPointer(ref this));
    }

    public interface Interface : IDXGIDeviceSubObject.Interface
    {
        [return: NativeTypeName("HRESULT")]
        int GetDesc(DXGI_SURFACE_DESC* pDesc);

        [return: NativeTypeName("HRESULT")]
        int Map(DXGI_MAPPED_RECT* pLockedRect, uint MapFlags);

        [return: NativeTypeName("HRESULT")]
        int Unmap();
    }
}

[Guid("4AE63092-6327-4C1B-80AE-BFE12EA32B86")]
[NativeTypeName("struct IDXGISurface1 : IDXGISurface")]
internal unsafe partial struct IDXGISurface1 : IDXGISurface1.Interface, INativeGuid
{
    static Guid* INativeGuid.NativeGuid => (Guid*)Unsafe.AsPointer(in IID_IDXGISurface1);

    public void** lpVtbl;

    [return: NativeTypeName("HRESULT")]
    public int QueryInterface([NativeTypeName("const IID &")] Guid* riid, void** ppvObject)
    {
        return ((delegate* unmanaged[Stdcall]<IDXGISurface1*, Guid*, void**, int>)(lpVtbl[0]))((IDXGISurface1*)Unsafe.AsPointer(ref this), riid, ppvObject);
    }

    [return: NativeTypeName("ULONG")]
    public uint AddRef()
    {
        return ((delegate* unmanaged[Stdcall]<IDXGISurface1*, uint>)(lpVtbl[1]))((IDXGISurface1*)Unsafe.AsPointer(ref this));
    }

    [return: NativeTypeName("ULONG")]
    public uint Release()
    {
        return ((delegate* unmanaged[Stdcall]<IDXGISurface1*, uint>)(lpVtbl[2]))((IDXGISurface1*)Unsafe.AsPointer(ref this));
    }

    [return: NativeTypeName("HRESULT")]
    public int SetPrivateData([NativeTypeName("const GUID &")] Guid* Name, uint DataSize, [NativeTypeName("const void *")] void* pData)
    {
        return ((delegate* unmanaged[Stdcall]<IDXGISurface1*, Guid*, uint, void*, int>)(lpVtbl[3]))((IDXGISurface1*)Unsafe.AsPointer(ref this), Name, DataSize, pData);
    }

    [return: NativeTypeName("HRESULT")]
    public int SetPrivateDataInterface([NativeTypeName("const GUID &")] Guid* Name, [NativeTypeName("const IUnknown *")] IUnknown* pUnknown)
    {
        return ((delegate* unmanaged[Stdcall]<IDXGISurface1*, Guid*, IUnknown*, int>)(lpVtbl[4]))((IDXGISurface1*)Unsafe.AsPointer(ref this), Name, pUnknown);
    }

    [return: NativeTypeName("HRESULT")]
    public int GetPrivateData([NativeTypeName("const GUID &")] Guid* Name, uint* pDataSize, void* pData)
    {
        return ((delegate* unmanaged[Stdcall]<IDXGISurface1*, Guid*, uint*, void*, int>)(lpVtbl[5]))((IDXGISurface1*)Unsafe.AsPointer(ref this), Name, pDataSize, pData);
    }

    [return: NativeTypeName("HRESULT")]
    public int GetParent([NativeTypeName("const IID &")] Guid* riid, void** ppParent)
    {
        return ((delegate* unmanaged[Stdcall]<IDXGISurface1*, Guid*, void**, int>)(lpVtbl[6]))((IDXGISurface1*)Unsafe.AsPointer(ref this), riid, ppParent);
    }

    [return: NativeTypeName("HRESULT")]
    public int GetDevice([NativeTypeName("const IID &")] Guid* riid, void** ppDevice)
    {
        return ((delegate* unmanaged[Stdcall]<IDXGISurface1*, Guid*, void**, int>)(lpVtbl[7]))((IDXGISurface1*)Unsafe.AsPointer(ref this), riid, ppDevice);
    }

    [return: NativeTypeName("HRESULT")]
    public int GetDesc(DXGI_SURFACE_DESC* pDesc)
    {
        return ((delegate* unmanaged[Stdcall]<IDXGISurface1*, DXGI_SURFACE_DESC*, int>)(lpVtbl[8]))((IDXGISurface1*)Unsafe.AsPointer(ref this), pDesc);
    }

    [return: NativeTypeName("HRESULT")]
    public int Map(DXGI_MAPPED_RECT* pLockedRect, uint MapFlags)
    {
        return ((delegate* unmanaged[Stdcall]<IDXGISurface1*, DXGI_MAPPED_RECT*, uint, int>)(lpVtbl[9]))((IDXGISurface1*)Unsafe.AsPointer(ref this), pLockedRect, MapFlags);
    }

    [return: NativeTypeName("HRESULT")]
    public int Unmap()
    {
        return ((delegate* unmanaged[Stdcall]<IDXGISurface1*, int>)(lpVtbl[10]))((IDXGISurface1*)Unsafe.AsPointer(ref this));
    }

    [return: NativeTypeName("HRESULT")]
    public int GetDC([NativeTypeName("BOOL")] int Discard, [NativeTypeName("HDC *")] HDC__** phdc)
    {
        return ((delegate* unmanaged[Stdcall]<IDXGISurface1*, int, HDC__**, int>)(lpVtbl[11]))((IDXGISurface1*)Unsafe.AsPointer(ref this), Discard, phdc);
    }

    [return: NativeTypeName("HRESULT")]
    public int ReleaseDC(RECT* pDirtyRect)
    {
        return ((delegate* unmanaged[Stdcall]<IDXGISurface1*, RECT*, int>)(lpVtbl[12]))((IDXGISurface1*)Unsafe.AsPointer(ref this), pDirtyRect);
    }

    public interface Interface : IDXGISurface.Interface
    {
        [return: NativeTypeName("HRESULT")]
        int GetDC([NativeTypeName("BOOL")] int Discard, [NativeTypeName("HDC *")] HDC__** phdc);

        [return: NativeTypeName("HRESULT")]
        int ReleaseDC(RECT* pDirtyRect);
    }
}

[Guid("2411E7E1-12AC-4CCF-BD14-9798E8534DC0")]
[NativeTypeName("struct IDXGIAdapter : IDXGIObject")]
internal unsafe partial struct IDXGIAdapter : IDXGIAdapter.Interface, INativeGuid
{
    static Guid* INativeGuid.NativeGuid => (Guid*)Unsafe.AsPointer(in IID_IDXGIAdapter);

    public void** lpVtbl;

    [return: NativeTypeName("HRESULT")]
    public int QueryInterface([NativeTypeName("const IID &")] Guid* riid, void** ppvObject)
    {
        return ((delegate* unmanaged[Stdcall]<IDXGIAdapter*, Guid*, void**, int>)(lpVtbl[0]))((IDXGIAdapter*)Unsafe.AsPointer(ref this), riid, ppvObject);
    }

    [return: NativeTypeName("ULONG")]
    public uint AddRef()
    {
        return ((delegate* unmanaged[Stdcall]<IDXGIAdapter*, uint>)(lpVtbl[1]))((IDXGIAdapter*)Unsafe.AsPointer(ref this));
    }

    [return: NativeTypeName("ULONG")]
    public uint Release()
    {
        return ((delegate* unmanaged[Stdcall]<IDXGIAdapter*, uint>)(lpVtbl[2]))((IDXGIAdapter*)Unsafe.AsPointer(ref this));
    }

    [return: NativeTypeName("HRESULT")]
    public int SetPrivateData([NativeTypeName("const GUID &")] Guid* Name, uint DataSize, [NativeTypeName("const void *")] void* pData)
    {
        return ((delegate* unmanaged[Stdcall]<IDXGIAdapter*, Guid*, uint, void*, int>)(lpVtbl[3]))((IDXGIAdapter*)Unsafe.AsPointer(ref this), Name, DataSize, pData);
    }

    [return: NativeTypeName("HRESULT")]
    public int SetPrivateDataInterface([NativeTypeName("const GUID &")] Guid* Name, [NativeTypeName("const IUnknown *")] IUnknown* pUnknown)
    {
        return ((delegate* unmanaged[Stdcall]<IDXGIAdapter*, Guid*, IUnknown*, int>)(lpVtbl[4]))((IDXGIAdapter*)Unsafe.AsPointer(ref this), Name, pUnknown);
    }

    [return: NativeTypeName("HRESULT")]
    public int GetPrivateData([NativeTypeName("const GUID &")] Guid* Name, uint* pDataSize, void* pData)
    {
        return ((delegate* unmanaged[Stdcall]<IDXGIAdapter*, Guid*, uint*, void*, int>)(lpVtbl[5]))((IDXGIAdapter*)Unsafe.AsPointer(ref this), Name, pDataSize, pData);
    }

    [return: NativeTypeName("HRESULT")]
    public int GetParent([NativeTypeName("const IID &")] Guid* riid, void** ppParent)
    {
        return ((delegate* unmanaged[Stdcall]<IDXGIAdapter*, Guid*, void**, int>)(lpVtbl[6]))((IDXGIAdapter*)Unsafe.AsPointer(ref this), riid, ppParent);
    }

    [return: NativeTypeName("HRESULT")]
    public int EnumOutputs(uint Output, IDXGIOutput** ppOutput)
    {
        return ((delegate* unmanaged[Stdcall]<IDXGIAdapter*, uint, IDXGIOutput**, int>)(lpVtbl[7]))((IDXGIAdapter*)Unsafe.AsPointer(ref this), Output, ppOutput);
    }

    [return: NativeTypeName("HRESULT")]
    public int GetDesc(DXGI_ADAPTER_DESC* pDesc)
    {
        return ((delegate* unmanaged[Stdcall]<IDXGIAdapter*, DXGI_ADAPTER_DESC*, int>)(lpVtbl[8]))((IDXGIAdapter*)Unsafe.AsPointer(ref this), pDesc);
    }

    [return: NativeTypeName("HRESULT")]
    public int CheckInterfaceSupport([NativeTypeName("const GUID &")] Guid* InterfaceName, LARGE_INTEGER* pUMDVersion)
    {
        return ((delegate* unmanaged[Stdcall]<IDXGIAdapter*, Guid*, LARGE_INTEGER*, int>)(lpVtbl[9]))((IDXGIAdapter*)Unsafe.AsPointer(ref this), InterfaceName, pUMDVersion);
    }

    public interface Interface : IDXGIObject.Interface
    {
        [return: NativeTypeName("HRESULT")]
        int EnumOutputs(uint Output, IDXGIOutput** ppOutput);

        [return: NativeTypeName("HRESULT")]
        int GetDesc(DXGI_ADAPTER_DESC* pDesc);

        [return: NativeTypeName("HRESULT")]
        int CheckInterfaceSupport([NativeTypeName("const GUID &")] Guid* InterfaceName, LARGE_INTEGER* pUMDVersion);
    }
}

[Guid("AE02EEDB-C735-4690-8D52-5A8DC20213AA")]
[NativeTypeName("struct IDXGIOutput : IDXGIObject")]
internal unsafe partial struct IDXGIOutput : IDXGIOutput.Interface, INativeGuid
{
    static Guid* INativeGuid.NativeGuid => (Guid*)Unsafe.AsPointer(in IID_IDXGIOutput);

    public void** lpVtbl;

    [return: NativeTypeName("HRESULT")]
    public int QueryInterface([NativeTypeName("const IID &")] Guid* riid, void** ppvObject)
    {
        return ((delegate* unmanaged[Stdcall]<IDXGIOutput*, Guid*, void**, int>)(lpVtbl[0]))((IDXGIOutput*)Unsafe.AsPointer(ref this), riid, ppvObject);
    }

    [return: NativeTypeName("ULONG")]
    public uint AddRef()
    {
        return ((delegate* unmanaged[Stdcall]<IDXGIOutput*, uint>)(lpVtbl[1]))((IDXGIOutput*)Unsafe.AsPointer(ref this));
    }

    [return: NativeTypeName("ULONG")]
    public uint Release()
    {
        return ((delegate* unmanaged[Stdcall]<IDXGIOutput*, uint>)(lpVtbl[2]))((IDXGIOutput*)Unsafe.AsPointer(ref this));
    }

    [return: NativeTypeName("HRESULT")]
    public int SetPrivateData([NativeTypeName("const GUID &")] Guid* Name, uint DataSize, [NativeTypeName("const void *")] void* pData)
    {
        return ((delegate* unmanaged[Stdcall]<IDXGIOutput*, Guid*, uint, void*, int>)(lpVtbl[3]))((IDXGIOutput*)Unsafe.AsPointer(ref this), Name, DataSize, pData);
    }

    [return: NativeTypeName("HRESULT")]
    public int SetPrivateDataInterface([NativeTypeName("const GUID &")] Guid* Name, [NativeTypeName("const IUnknown *")] IUnknown* pUnknown)
    {
        return ((delegate* unmanaged[Stdcall]<IDXGIOutput*, Guid*, IUnknown*, int>)(lpVtbl[4]))((IDXGIOutput*)Unsafe.AsPointer(ref this), Name, pUnknown);
    }

    [return: NativeTypeName("HRESULT")]
    public int GetPrivateData([NativeTypeName("const GUID &")] Guid* Name, uint* pDataSize, void* pData)
    {
        return ((delegate* unmanaged[Stdcall]<IDXGIOutput*, Guid*, uint*, void*, int>)(lpVtbl[5]))((IDXGIOutput*)Unsafe.AsPointer(ref this), Name, pDataSize, pData);
    }

    [return: NativeTypeName("HRESULT")]
    public int GetParent([NativeTypeName("const IID &")] Guid* riid, void** ppParent)
    {
        return ((delegate* unmanaged[Stdcall]<IDXGIOutput*, Guid*, void**, int>)(lpVtbl[6]))((IDXGIOutput*)Unsafe.AsPointer(ref this), riid, ppParent);
    }

    [return: NativeTypeName("HRESULT")]
    public int GetDesc(DXGI_OUTPUT_DESC* pDesc)
    {
        return ((delegate* unmanaged[Stdcall]<IDXGIOutput*, DXGI_OUTPUT_DESC*, int>)(lpVtbl[7]))((IDXGIOutput*)Unsafe.AsPointer(ref this), pDesc);
    }

    [return: NativeTypeName("HRESULT")]
    public int GetDisplayModeList(DXGI_FORMAT EnumFormat, uint Flags, uint* pNumModes, DXGI_MODE_DESC* pDesc)
    {
        return ((delegate* unmanaged[Stdcall]<IDXGIOutput*, DXGI_FORMAT, uint, uint*, DXGI_MODE_DESC*, int>)(lpVtbl[8]))((IDXGIOutput*)Unsafe.AsPointer(ref this), EnumFormat, Flags, pNumModes, pDesc);
    }

    [return: NativeTypeName("HRESULT")]
    public int FindClosestMatchingMode([NativeTypeName("const DXGI_MODE_DESC *")] DXGI_MODE_DESC* pModeToMatch, DXGI_MODE_DESC* pClosestMatch, IUnknown* pConcernedDevice)
    {
        return ((delegate* unmanaged[Stdcall]<IDXGIOutput*, DXGI_MODE_DESC*, DXGI_MODE_DESC*, IUnknown*, int>)(lpVtbl[9]))((IDXGIOutput*)Unsafe.AsPointer(ref this), pModeToMatch, pClosestMatch, pConcernedDevice);
    }

    [return: NativeTypeName("HRESULT")]
    public int WaitForVBlank()
    {
        return ((delegate* unmanaged[Stdcall]<IDXGIOutput*, int>)(lpVtbl[10]))((IDXGIOutput*)Unsafe.AsPointer(ref this));
    }

    [return: NativeTypeName("HRESULT")]
    public int TakeOwnership(IUnknown* pDevice, [NativeTypeName("BOOL")] int Exclusive)
    {
        return ((delegate* unmanaged[Stdcall]<IDXGIOutput*, IUnknown*, int, int>)(lpVtbl[11]))((IDXGIOutput*)Unsafe.AsPointer(ref this), pDevice, Exclusive);
    }

    public void ReleaseOwnership()
    {
        ((delegate* unmanaged[Stdcall]<IDXGIOutput*, void>)(lpVtbl[12]))((IDXGIOutput*)Unsafe.AsPointer(ref this));
    }

    [return: NativeTypeName("HRESULT")]
    public int GetGammaControlCapabilities(DXGI_GAMMA_CONTROL_CAPABILITIES* pGammaCaps)
    {
        return ((delegate* unmanaged[Stdcall]<IDXGIOutput*, DXGI_GAMMA_CONTROL_CAPABILITIES*, int>)(lpVtbl[13]))((IDXGIOutput*)Unsafe.AsPointer(ref this), pGammaCaps);
    }

    [return: NativeTypeName("HRESULT")]
    public int SetGammaControl([NativeTypeName("const DXGI_GAMMA_CONTROL *")] DXGI_GAMMA_CONTROL* pArray)
    {
        return ((delegate* unmanaged[Stdcall]<IDXGIOutput*, DXGI_GAMMA_CONTROL*, int>)(lpVtbl[14]))((IDXGIOutput*)Unsafe.AsPointer(ref this), pArray);
    }

    [return: NativeTypeName("HRESULT")]
    public int GetGammaControl(DXGI_GAMMA_CONTROL* pArray)
    {
        return ((delegate* unmanaged[Stdcall]<IDXGIOutput*, DXGI_GAMMA_CONTROL*, int>)(lpVtbl[15]))((IDXGIOutput*)Unsafe.AsPointer(ref this), pArray);
    }

    [return: NativeTypeName("HRESULT")]
    public int SetDisplaySurface(IDXGISurface* pScanoutSurface)
    {
        return ((delegate* unmanaged[Stdcall]<IDXGIOutput*, IDXGISurface*, int>)(lpVtbl[16]))((IDXGIOutput*)Unsafe.AsPointer(ref this), pScanoutSurface);
    }

    [return: NativeTypeName("HRESULT")]
    public int GetDisplaySurfaceData(IDXGISurface* pDestination)
    {
        return ((delegate* unmanaged[Stdcall]<IDXGIOutput*, IDXGISurface*, int>)(lpVtbl[17]))((IDXGIOutput*)Unsafe.AsPointer(ref this), pDestination);
    }

    [return: NativeTypeName("HRESULT")]
    public int GetFrameStatistics(DXGI_FRAME_STATISTICS* pStats)
    {
        return ((delegate* unmanaged[Stdcall]<IDXGIOutput*, DXGI_FRAME_STATISTICS*, int>)(lpVtbl[18]))((IDXGIOutput*)Unsafe.AsPointer(ref this), pStats);
    }

    public interface Interface : IDXGIObject.Interface
    {
        [return: NativeTypeName("HRESULT")]
        int GetDesc(DXGI_OUTPUT_DESC* pDesc);

        [return: NativeTypeName("HRESULT")]
        int GetDisplayModeList(DXGI_FORMAT EnumFormat, uint Flags, uint* pNumModes, DXGI_MODE_DESC* pDesc);

        [return: NativeTypeName("HRESULT")]
        int FindClosestMatchingMode([NativeTypeName("const DXGI_MODE_DESC *")] DXGI_MODE_DESC* pModeToMatch, DXGI_MODE_DESC* pClosestMatch, IUnknown* pConcernedDevice);

        [return: NativeTypeName("HRESULT")]
        int WaitForVBlank();

        [return: NativeTypeName("HRESULT")]
        int TakeOwnership(IUnknown* pDevice, [NativeTypeName("BOOL")] int Exclusive);

        void ReleaseOwnership();

        [return: NativeTypeName("HRESULT")]
        int GetGammaControlCapabilities(DXGI_GAMMA_CONTROL_CAPABILITIES* pGammaCaps);

        [return: NativeTypeName("HRESULT")]
        int SetGammaControl([NativeTypeName("const DXGI_GAMMA_CONTROL *")] DXGI_GAMMA_CONTROL* pArray);

        [return: NativeTypeName("HRESULT")]
        int GetGammaControl(DXGI_GAMMA_CONTROL* pArray);

        [return: NativeTypeName("HRESULT")]
        int SetDisplaySurface(IDXGISurface* pScanoutSurface);

        [return: NativeTypeName("HRESULT")]
        int GetDisplaySurfaceData(IDXGISurface* pDestination);

        [return: NativeTypeName("HRESULT")]
        int GetFrameStatistics(DXGI_FRAME_STATISTICS* pStats);
    }
}

[Guid("310D36A0-D2E7-4C0A-AA04-6A9D23B8886A")]
[NativeTypeName("struct IDXGISwapChain : IDXGIDeviceSubObject")]
internal unsafe partial struct IDXGISwapChain : IDXGISwapChain.Interface, INativeGuid
{
    static Guid* INativeGuid.NativeGuid => (Guid*)Unsafe.AsPointer(in IID_IDXGISwapChain);

    public void** lpVtbl;

    [return: NativeTypeName("HRESULT")]
    public int QueryInterface([NativeTypeName("const IID &")] Guid* riid, void** ppvObject)
    {
        return ((delegate* unmanaged[Stdcall]<IDXGISwapChain*, Guid*, void**, int>)(lpVtbl[0]))((IDXGISwapChain*)Unsafe.AsPointer(ref this), riid, ppvObject);
    }

    [return: NativeTypeName("ULONG")]
    public uint AddRef()
    {
        return ((delegate* unmanaged[Stdcall]<IDXGISwapChain*, uint>)(lpVtbl[1]))((IDXGISwapChain*)Unsafe.AsPointer(ref this));
    }

    [return: NativeTypeName("ULONG")]
    public uint Release()
    {
        return ((delegate* unmanaged[Stdcall]<IDXGISwapChain*, uint>)(lpVtbl[2]))((IDXGISwapChain*)Unsafe.AsPointer(ref this));
    }

    [return: NativeTypeName("HRESULT")]
    public int SetPrivateData([NativeTypeName("const GUID &")] Guid* Name, uint DataSize, [NativeTypeName("const void *")] void* pData)
    {
        return ((delegate* unmanaged[Stdcall]<IDXGISwapChain*, Guid*, uint, void*, int>)(lpVtbl[3]))((IDXGISwapChain*)Unsafe.AsPointer(ref this), Name, DataSize, pData);
    }

    [return: NativeTypeName("HRESULT")]
    public int SetPrivateDataInterface([NativeTypeName("const GUID &")] Guid* Name, [NativeTypeName("const IUnknown *")] IUnknown* pUnknown)
    {
        return ((delegate* unmanaged[Stdcall]<IDXGISwapChain*, Guid*, IUnknown*, int>)(lpVtbl[4]))((IDXGISwapChain*)Unsafe.AsPointer(ref this), Name, pUnknown);
    }

    [return: NativeTypeName("HRESULT")]
    public int GetPrivateData([NativeTypeName("const GUID &")] Guid* Name, uint* pDataSize, void* pData)
    {
        return ((delegate* unmanaged[Stdcall]<IDXGISwapChain*, Guid*, uint*, void*, int>)(lpVtbl[5]))((IDXGISwapChain*)Unsafe.AsPointer(ref this), Name, pDataSize, pData);
    }

    [return: NativeTypeName("HRESULT")]
    public int GetParent([NativeTypeName("const IID &")] Guid* riid, void** ppParent)
    {
        return ((delegate* unmanaged[Stdcall]<IDXGISwapChain*, Guid*, void**, int>)(lpVtbl[6]))((IDXGISwapChain*)Unsafe.AsPointer(ref this), riid, ppParent);
    }

    [return: NativeTypeName("HRESULT")]
    public int GetDevice([NativeTypeName("const IID &")] Guid* riid, void** ppDevice)
    {
        return ((delegate* unmanaged[Stdcall]<IDXGISwapChain*, Guid*, void**, int>)(lpVtbl[7]))((IDXGISwapChain*)Unsafe.AsPointer(ref this), riid, ppDevice);
    }

    [return: NativeTypeName("HRESULT")]
    public int Present(uint SyncInterval, uint Flags)
    {
        return ((delegate* unmanaged[Stdcall]<IDXGISwapChain*, uint, uint, int>)(lpVtbl[8]))((IDXGISwapChain*)Unsafe.AsPointer(ref this), SyncInterval, Flags);
    }

    [return: NativeTypeName("HRESULT")]
    public int GetBuffer(uint Buffer, [NativeTypeName("const IID &")] Guid* riid, void** ppSurface)
    {
        return ((delegate* unmanaged[Stdcall]<IDXGISwapChain*, uint, Guid*, void**, int>)(lpVtbl[9]))((IDXGISwapChain*)Unsafe.AsPointer(ref this), Buffer, riid, ppSurface);
    }

    [return: NativeTypeName("HRESULT")]
    public int SetFullscreenState([NativeTypeName("BOOL")] int Fullscreen, IDXGIOutput* pTarget)
    {
        return ((delegate* unmanaged[Stdcall]<IDXGISwapChain*, int, IDXGIOutput*, int>)(lpVtbl[10]))((IDXGISwapChain*)Unsafe.AsPointer(ref this), Fullscreen, pTarget);
    }

    [return: NativeTypeName("HRESULT")]
    public int GetFullscreenState([NativeTypeName("BOOL *")] int* pFullscreen, IDXGIOutput** ppTarget)
    {
        return ((delegate* unmanaged[Stdcall]<IDXGISwapChain*, int*, IDXGIOutput**, int>)(lpVtbl[11]))((IDXGISwapChain*)Unsafe.AsPointer(ref this), pFullscreen, ppTarget);
    }

    [return: NativeTypeName("HRESULT")]
    public int GetDesc(DXGI_SWAP_CHAIN_DESC* pDesc)
    {
        return ((delegate* unmanaged[Stdcall]<IDXGISwapChain*, DXGI_SWAP_CHAIN_DESC*, int>)(lpVtbl[12]))((IDXGISwapChain*)Unsafe.AsPointer(ref this), pDesc);
    }

    [return: NativeTypeName("HRESULT")]
    public int ResizeBuffers(uint BufferCount, uint Width, uint Height, DXGI_FORMAT NewFormat, uint SwapChainFlags)
    {
        return ((delegate* unmanaged[Stdcall]<IDXGISwapChain*, uint, uint, uint, DXGI_FORMAT, uint, int>)(lpVtbl[13]))((IDXGISwapChain*)Unsafe.AsPointer(ref this), BufferCount, Width, Height, NewFormat, SwapChainFlags);
    }

    [return: NativeTypeName("HRESULT")]
    public int ResizeTarget([NativeTypeName("const DXGI_MODE_DESC *")] DXGI_MODE_DESC* pNewTargetParameters)
    {
        return ((delegate* unmanaged[Stdcall]<IDXGISwapChain*, DXGI_MODE_DESC*, int>)(lpVtbl[14]))((IDXGISwapChain*)Unsafe.AsPointer(ref this), pNewTargetParameters);
    }

    [return: NativeTypeName("HRESULT")]
    public int GetContainingOutput(IDXGIOutput** ppOutput)
    {
        return ((delegate* unmanaged[Stdcall]<IDXGISwapChain*, IDXGIOutput**, int>)(lpVtbl[15]))((IDXGISwapChain*)Unsafe.AsPointer(ref this), ppOutput);
    }

    [return: NativeTypeName("HRESULT")]
    public int GetFrameStatistics(DXGI_FRAME_STATISTICS* pStats)
    {
        return ((delegate* unmanaged[Stdcall]<IDXGISwapChain*, DXGI_FRAME_STATISTICS*, int>)(lpVtbl[16]))((IDXGISwapChain*)Unsafe.AsPointer(ref this), pStats);
    }

    [return: NativeTypeName("HRESULT")]
    public int GetLastPresentCount(uint* pLastPresentCount)
    {
        return ((delegate* unmanaged[Stdcall]<IDXGISwapChain*, uint*, int>)(lpVtbl[17]))((IDXGISwapChain*)Unsafe.AsPointer(ref this), pLastPresentCount);
    }

    public interface Interface : IDXGIDeviceSubObject.Interface
    {
        [return: NativeTypeName("HRESULT")]
        int Present(uint SyncInterval, uint Flags);

        [return: NativeTypeName("HRESULT")]
        int GetBuffer(uint Buffer, [NativeTypeName("const IID &")] Guid* riid, void** ppSurface);

        [return: NativeTypeName("HRESULT")]
        int SetFullscreenState([NativeTypeName("BOOL")] int Fullscreen, IDXGIOutput* pTarget);

        [return: NativeTypeName("HRESULT")]
        int GetFullscreenState([NativeTypeName("BOOL *")] int* pFullscreen, IDXGIOutput** ppTarget);

        [return: NativeTypeName("HRESULT")]
        int GetDesc(DXGI_SWAP_CHAIN_DESC* pDesc);

        [return: NativeTypeName("HRESULT")]
        int ResizeBuffers(uint BufferCount, uint Width, uint Height, DXGI_FORMAT NewFormat, uint SwapChainFlags);

        [return: NativeTypeName("HRESULT")]
        int ResizeTarget([NativeTypeName("const DXGI_MODE_DESC *")] DXGI_MODE_DESC* pNewTargetParameters);

        [return: NativeTypeName("HRESULT")]
        int GetContainingOutput(IDXGIOutput** ppOutput);

        [return: NativeTypeName("HRESULT")]
        int GetFrameStatistics(DXGI_FRAME_STATISTICS* pStats);

        [return: NativeTypeName("HRESULT")]
        int GetLastPresentCount(uint* pLastPresentCount);
    }
}

[Guid("7B7166EC-21C7-44AE-B21A-C9AE321AE369")]
[NativeTypeName("struct IDXGIFactory : IDXGIObject")]
internal unsafe partial struct IDXGIFactory : IDXGIFactory.Interface, INativeGuid
{
    static Guid* INativeGuid.NativeGuid => (Guid*)Unsafe.AsPointer(in IID_IDXGIFactory);

    public void** lpVtbl;

    [return: NativeTypeName("HRESULT")]
    public int QueryInterface([NativeTypeName("const IID &")] Guid* riid, void** ppvObject)
    {
        return ((delegate* unmanaged[Stdcall]<IDXGIFactory*, Guid*, void**, int>)(lpVtbl[0]))((IDXGIFactory*)Unsafe.AsPointer(ref this), riid, ppvObject);
    }

    [return: NativeTypeName("ULONG")]
    public uint AddRef()
    {
        return ((delegate* unmanaged[Stdcall]<IDXGIFactory*, uint>)(lpVtbl[1]))((IDXGIFactory*)Unsafe.AsPointer(ref this));
    }

    [return: NativeTypeName("ULONG")]
    public uint Release()
    {
        return ((delegate* unmanaged[Stdcall]<IDXGIFactory*, uint>)(lpVtbl[2]))((IDXGIFactory*)Unsafe.AsPointer(ref this));
    }

    [return: NativeTypeName("HRESULT")]
    public int SetPrivateData([NativeTypeName("const GUID &")] Guid* Name, uint DataSize, [NativeTypeName("const void *")] void* pData)
    {
        return ((delegate* unmanaged[Stdcall]<IDXGIFactory*, Guid*, uint, void*, int>)(lpVtbl[3]))((IDXGIFactory*)Unsafe.AsPointer(ref this), Name, DataSize, pData);
    }

    [return: NativeTypeName("HRESULT")]
    public int SetPrivateDataInterface([NativeTypeName("const GUID &")] Guid* Name, [NativeTypeName("const IUnknown *")] IUnknown* pUnknown)
    {
        return ((delegate* unmanaged[Stdcall]<IDXGIFactory*, Guid*, IUnknown*, int>)(lpVtbl[4]))((IDXGIFactory*)Unsafe.AsPointer(ref this), Name, pUnknown);
    }

    [return: NativeTypeName("HRESULT")]
    public int GetPrivateData([NativeTypeName("const GUID &")] Guid* Name, uint* pDataSize, void* pData)
    {
        return ((delegate* unmanaged[Stdcall]<IDXGIFactory*, Guid*, uint*, void*, int>)(lpVtbl[5]))((IDXGIFactory*)Unsafe.AsPointer(ref this), Name, pDataSize, pData);
    }

    [return: NativeTypeName("HRESULT")]
    public int GetParent([NativeTypeName("const IID &")] Guid* riid, void** ppParent)
    {
        return ((delegate* unmanaged[Stdcall]<IDXGIFactory*, Guid*, void**, int>)(lpVtbl[6]))((IDXGIFactory*)Unsafe.AsPointer(ref this), riid, ppParent);
    }

    [return: NativeTypeName("HRESULT")]
    public int EnumAdapters(uint Adapter, IDXGIAdapter** ppAdapter)
    {
        return ((delegate* unmanaged[Stdcall]<IDXGIFactory*, uint, IDXGIAdapter**, int>)(lpVtbl[7]))((IDXGIFactory*)Unsafe.AsPointer(ref this), Adapter, ppAdapter);
    }

    [return: NativeTypeName("HRESULT")]
    public int MakeWindowAssociation([NativeTypeName("HWND")] HWND__* WindowHandle, uint Flags)
    {
        return ((delegate* unmanaged[Stdcall]<IDXGIFactory*, HWND__*, uint, int>)(lpVtbl[8]))((IDXGIFactory*)Unsafe.AsPointer(ref this), WindowHandle, Flags);
    }

    [return: NativeTypeName("HRESULT")]
    public int GetWindowAssociation([NativeTypeName("HWND *")] HWND__** pWindowHandle)
    {
        return ((delegate* unmanaged[Stdcall]<IDXGIFactory*, HWND__**, int>)(lpVtbl[9]))((IDXGIFactory*)Unsafe.AsPointer(ref this), pWindowHandle);
    }

    [return: NativeTypeName("HRESULT")]
    public int CreateSwapChain(IUnknown* pDevice, DXGI_SWAP_CHAIN_DESC* pDesc, IDXGISwapChain** ppSwapChain)
    {
        return ((delegate* unmanaged[Stdcall]<IDXGIFactory*, IUnknown*, DXGI_SWAP_CHAIN_DESC*, IDXGISwapChain**, int>)(lpVtbl[10]))((IDXGIFactory*)Unsafe.AsPointer(ref this), pDevice, pDesc, ppSwapChain);
    }

    [return: NativeTypeName("HRESULT")]
    public int CreateSoftwareAdapter([NativeTypeName("HMODULE")] HINSTANCE__* Module, IDXGIAdapter** ppAdapter)
    {
        return ((delegate* unmanaged[Stdcall]<IDXGIFactory*, HINSTANCE__*, IDXGIAdapter**, int>)(lpVtbl[11]))((IDXGIFactory*)Unsafe.AsPointer(ref this), Module, ppAdapter);
    }

    public interface Interface : IDXGIObject.Interface
    {
        [return: NativeTypeName("HRESULT")]
        int EnumAdapters(uint Adapter, IDXGIAdapter** ppAdapter);

        [return: NativeTypeName("HRESULT")]
        int MakeWindowAssociation([NativeTypeName("HWND")] HWND__* WindowHandle, uint Flags);

        [return: NativeTypeName("HRESULT")]
        int GetWindowAssociation([NativeTypeName("HWND *")] HWND__** pWindowHandle);

        [return: NativeTypeName("HRESULT")]
        int CreateSwapChain(IUnknown* pDevice, DXGI_SWAP_CHAIN_DESC* pDesc, IDXGISwapChain** ppSwapChain);

        [return: NativeTypeName("HRESULT")]
        int CreateSoftwareAdapter([NativeTypeName("HMODULE")] HINSTANCE__* Module, IDXGIAdapter** ppAdapter);
    }
}

[Guid("54EC77FA-1377-44E6-8C32-88FD5F44C84C")]
[NativeTypeName("struct IDXGIDevice : IDXGIObject")]
internal unsafe partial struct IDXGIDevice : IDXGIDevice.Interface, INativeGuid
{
    static Guid* INativeGuid.NativeGuid => (Guid*)Unsafe.AsPointer(in IID_IDXGIDevice);

    public void** lpVtbl;

    [return: NativeTypeName("HRESULT")]
    public int QueryInterface([NativeTypeName("const IID &")] Guid* riid, void** ppvObject)
    {
        return ((delegate* unmanaged[Stdcall]<IDXGIDevice*, Guid*, void**, int>)(lpVtbl[0]))((IDXGIDevice*)Unsafe.AsPointer(ref this), riid, ppvObject);
    }

    [return: NativeTypeName("ULONG")]
    public uint AddRef()
    {
        return ((delegate* unmanaged[Stdcall]<IDXGIDevice*, uint>)(lpVtbl[1]))((IDXGIDevice*)Unsafe.AsPointer(ref this));
    }

    [return: NativeTypeName("ULONG")]
    public uint Release()
    {
        return ((delegate* unmanaged[Stdcall]<IDXGIDevice*, uint>)(lpVtbl[2]))((IDXGIDevice*)Unsafe.AsPointer(ref this));
    }

    [return: NativeTypeName("HRESULT")]
    public int SetPrivateData([NativeTypeName("const GUID &")] Guid* Name, uint DataSize, [NativeTypeName("const void *")] void* pData)
    {
        return ((delegate* unmanaged[Stdcall]<IDXGIDevice*, Guid*, uint, void*, int>)(lpVtbl[3]))((IDXGIDevice*)Unsafe.AsPointer(ref this), Name, DataSize, pData);
    }

    [return: NativeTypeName("HRESULT")]
    public int SetPrivateDataInterface([NativeTypeName("const GUID &")] Guid* Name, [NativeTypeName("const IUnknown *")] IUnknown* pUnknown)
    {
        return ((delegate* unmanaged[Stdcall]<IDXGIDevice*, Guid*, IUnknown*, int>)(lpVtbl[4]))((IDXGIDevice*)Unsafe.AsPointer(ref this), Name, pUnknown);
    }

    [return: NativeTypeName("HRESULT")]
    public int GetPrivateData([NativeTypeName("const GUID &")] Guid* Name, uint* pDataSize, void* pData)
    {
        return ((delegate* unmanaged[Stdcall]<IDXGIDevice*, Guid*, uint*, void*, int>)(lpVtbl[5]))((IDXGIDevice*)Unsafe.AsPointer(ref this), Name, pDataSize, pData);
    }

    [return: NativeTypeName("HRESULT")]
    public int GetParent([NativeTypeName("const IID &")] Guid* riid, void** ppParent)
    {
        return ((delegate* unmanaged[Stdcall]<IDXGIDevice*, Guid*, void**, int>)(lpVtbl[6]))((IDXGIDevice*)Unsafe.AsPointer(ref this), riid, ppParent);
    }

    [return: NativeTypeName("HRESULT")]
    public int GetAdapter(IDXGIAdapter** pAdapter)
    {
        return ((delegate* unmanaged[Stdcall]<IDXGIDevice*, IDXGIAdapter**, int>)(lpVtbl[7]))((IDXGIDevice*)Unsafe.AsPointer(ref this), pAdapter);
    }

    [return: NativeTypeName("HRESULT")]
    public int CreateSurface([NativeTypeName("const DXGI_SURFACE_DESC *")] DXGI_SURFACE_DESC* pDesc, uint NumSurfaces, [NativeTypeName("DXGI_USAGE")] uint Usage, [NativeTypeName("const DXGI_SHARED_RESOURCE *")] DXGI_SHARED_RESOURCE* pSharedResource, IDXGISurface** ppSurface)
    {
        return ((delegate* unmanaged[Stdcall]<IDXGIDevice*, DXGI_SURFACE_DESC*, uint, uint, DXGI_SHARED_RESOURCE*, IDXGISurface**, int>)(lpVtbl[8]))((IDXGIDevice*)Unsafe.AsPointer(ref this), pDesc, NumSurfaces, Usage, pSharedResource, ppSurface);
    }

    [return: NativeTypeName("HRESULT")]
    public int QueryResourceResidency([NativeTypeName("IUnknown *const *")] IUnknown** ppResources, DXGI_RESIDENCY* pResidencyStatus, uint NumResources)
    {
        return ((delegate* unmanaged[Stdcall]<IDXGIDevice*, IUnknown**, DXGI_RESIDENCY*, uint, int>)(lpVtbl[9]))((IDXGIDevice*)Unsafe.AsPointer(ref this), ppResources, pResidencyStatus, NumResources);
    }

    [return: NativeTypeName("HRESULT")]
    public int SetGPUThreadPriority(int Priority)
    {
        return ((delegate* unmanaged[Stdcall]<IDXGIDevice*, int, int>)(lpVtbl[10]))((IDXGIDevice*)Unsafe.AsPointer(ref this), Priority);
    }

    [return: NativeTypeName("HRESULT")]
    public int GetGPUThreadPriority(int* pPriority)
    {
        return ((delegate* unmanaged[Stdcall]<IDXGIDevice*, int*, int>)(lpVtbl[11]))((IDXGIDevice*)Unsafe.AsPointer(ref this), pPriority);
    }

    public interface Interface : IDXGIObject.Interface
    {
        [return: NativeTypeName("HRESULT")]
        int GetAdapter(IDXGIAdapter** pAdapter);

        [return: NativeTypeName("HRESULT")]
        int CreateSurface([NativeTypeName("const DXGI_SURFACE_DESC *")] DXGI_SURFACE_DESC* pDesc, uint NumSurfaces, [NativeTypeName("DXGI_USAGE")] uint Usage, [NativeTypeName("const DXGI_SHARED_RESOURCE *")] DXGI_SHARED_RESOURCE* pSharedResource, IDXGISurface** ppSurface);

        [return: NativeTypeName("HRESULT")]
        int QueryResourceResidency([NativeTypeName("IUnknown *const *")] IUnknown** ppResources, DXGI_RESIDENCY* pResidencyStatus, uint NumResources);

        [return: NativeTypeName("HRESULT")]
        int SetGPUThreadPriority(int Priority);

        [return: NativeTypeName("HRESULT")]
        int GetGPUThreadPriority(int* pPriority);
    }
}

internal enum DXGI_ADAPTER_FLAG
{
    DXGI_ADAPTER_FLAG_NONE = 0,
    DXGI_ADAPTER_FLAG_REMOTE = 1,
    DXGI_ADAPTER_FLAG_SOFTWARE = 2,
    DXGI_ADAPTER_FLAG_FORCE_DWORD = unchecked((int)(0xffffffff)),
}

internal partial struct DXGI_ADAPTER_DESC1
{
    [NativeTypeName("WCHAR[128]")]
    public _Description_e__FixedBuffer Description;

    public uint VendorId;

    public uint DeviceId;

    public uint SubSysId;

    public uint Revision;

    [NativeTypeName("SIZE_T")]
    public nuint DedicatedVideoMemory;

    [NativeTypeName("SIZE_T")]
    public nuint DedicatedSystemMemory;

    [NativeTypeName("SIZE_T")]
    public nuint SharedSystemMemory;

    public LUID AdapterLuid;

    public uint Flags;

    [InlineArray(128)]
    public partial struct _Description_e__FixedBuffer
    {
        public ushort e0;
    }
}

internal partial struct DXGI_DISPLAY_COLOR_SPACE
{
    [NativeTypeName("FLOAT[8][2]")]
    public _PrimaryCoordinates_e__FixedBuffer PrimaryCoordinates;

    [NativeTypeName("FLOAT[16][2]")]
    public _WhitePoints_e__FixedBuffer WhitePoints;

    [InlineArray(8 * 2)]
    public partial struct _PrimaryCoordinates_e__FixedBuffer
    {
        public float e0_0;
    }

    [InlineArray(16 * 2)]
    public partial struct _WhitePoints_e__FixedBuffer
    {
        public float e0_0;
    }
}

[Guid("770AAE78-F26F-4DBA-A829-253C83D1B387")]
[NativeTypeName("struct IDXGIFactory1 : IDXGIFactory")]
internal unsafe partial struct IDXGIFactory1 : IDXGIFactory1.Interface, INativeGuid
{
    static Guid* INativeGuid.NativeGuid => (Guid*)Unsafe.AsPointer(in IID_IDXGIFactory1);

    public void** lpVtbl;

    [return: NativeTypeName("HRESULT")]
    public int QueryInterface([NativeTypeName("const IID &")] Guid* riid, void** ppvObject)
    {
        return ((delegate* unmanaged[Stdcall]<IDXGIFactory1*, Guid*, void**, int>)(lpVtbl[0]))((IDXGIFactory1*)Unsafe.AsPointer(ref this), riid, ppvObject);
    }

    [return: NativeTypeName("ULONG")]
    public uint AddRef()
    {
        return ((delegate* unmanaged[Stdcall]<IDXGIFactory1*, uint>)(lpVtbl[1]))((IDXGIFactory1*)Unsafe.AsPointer(ref this));
    }

    [return: NativeTypeName("ULONG")]
    public uint Release()
    {
        return ((delegate* unmanaged[Stdcall]<IDXGIFactory1*, uint>)(lpVtbl[2]))((IDXGIFactory1*)Unsafe.AsPointer(ref this));
    }

    [return: NativeTypeName("HRESULT")]
    public int SetPrivateData([NativeTypeName("const GUID &")] Guid* Name, uint DataSize, [NativeTypeName("const void *")] void* pData)
    {
        return ((delegate* unmanaged[Stdcall]<IDXGIFactory1*, Guid*, uint, void*, int>)(lpVtbl[3]))((IDXGIFactory1*)Unsafe.AsPointer(ref this), Name, DataSize, pData);
    }

    [return: NativeTypeName("HRESULT")]
    public int SetPrivateDataInterface([NativeTypeName("const GUID &")] Guid* Name, [NativeTypeName("const IUnknown *")] IUnknown* pUnknown)
    {
        return ((delegate* unmanaged[Stdcall]<IDXGIFactory1*, Guid*, IUnknown*, int>)(lpVtbl[4]))((IDXGIFactory1*)Unsafe.AsPointer(ref this), Name, pUnknown);
    }

    [return: NativeTypeName("HRESULT")]
    public int GetPrivateData([NativeTypeName("const GUID &")] Guid* Name, uint* pDataSize, void* pData)
    {
        return ((delegate* unmanaged[Stdcall]<IDXGIFactory1*, Guid*, uint*, void*, int>)(lpVtbl[5]))((IDXGIFactory1*)Unsafe.AsPointer(ref this), Name, pDataSize, pData);
    }

    [return: NativeTypeName("HRESULT")]
    public int GetParent([NativeTypeName("const IID &")] Guid* riid, void** ppParent)
    {
        return ((delegate* unmanaged[Stdcall]<IDXGIFactory1*, Guid*, void**, int>)(lpVtbl[6]))((IDXGIFactory1*)Unsafe.AsPointer(ref this), riid, ppParent);
    }

    [return: NativeTypeName("HRESULT")]
    public int EnumAdapters(uint Adapter, IDXGIAdapter** ppAdapter)
    {
        return ((delegate* unmanaged[Stdcall]<IDXGIFactory1*, uint, IDXGIAdapter**, int>)(lpVtbl[7]))((IDXGIFactory1*)Unsafe.AsPointer(ref this), Adapter, ppAdapter);
    }

    [return: NativeTypeName("HRESULT")]
    public int MakeWindowAssociation([NativeTypeName("HWND")] HWND__* WindowHandle, uint Flags)
    {
        return ((delegate* unmanaged[Stdcall]<IDXGIFactory1*, HWND__*, uint, int>)(lpVtbl[8]))((IDXGIFactory1*)Unsafe.AsPointer(ref this), WindowHandle, Flags);
    }

    [return: NativeTypeName("HRESULT")]
    public int GetWindowAssociation([NativeTypeName("HWND *")] HWND__** pWindowHandle)
    {
        return ((delegate* unmanaged[Stdcall]<IDXGIFactory1*, HWND__**, int>)(lpVtbl[9]))((IDXGIFactory1*)Unsafe.AsPointer(ref this), pWindowHandle);
    }

    [return: NativeTypeName("HRESULT")]
    public int CreateSwapChain(IUnknown* pDevice, DXGI_SWAP_CHAIN_DESC* pDesc, IDXGISwapChain** ppSwapChain)
    {
        return ((delegate* unmanaged[Stdcall]<IDXGIFactory1*, IUnknown*, DXGI_SWAP_CHAIN_DESC*, IDXGISwapChain**, int>)(lpVtbl[10]))((IDXGIFactory1*)Unsafe.AsPointer(ref this), pDevice, pDesc, ppSwapChain);
    }

    [return: NativeTypeName("HRESULT")]
    public int CreateSoftwareAdapter([NativeTypeName("HMODULE")] HINSTANCE__* Module, IDXGIAdapter** ppAdapter)
    {
        return ((delegate* unmanaged[Stdcall]<IDXGIFactory1*, HINSTANCE__*, IDXGIAdapter**, int>)(lpVtbl[11]))((IDXGIFactory1*)Unsafe.AsPointer(ref this), Module, ppAdapter);
    }

    [return: NativeTypeName("HRESULT")]
    public int EnumAdapters1(uint Adapter, IDXGIAdapter1** ppAdapter)
    {
        return ((delegate* unmanaged[Stdcall]<IDXGIFactory1*, uint, IDXGIAdapter1**, int>)(lpVtbl[12]))((IDXGIFactory1*)Unsafe.AsPointer(ref this), Adapter, ppAdapter);
    }

    [return: NativeTypeName("BOOL")]
    public int IsCurrent()
    {
        return ((delegate* unmanaged[Stdcall]<IDXGIFactory1*, int>)(lpVtbl[13]))((IDXGIFactory1*)Unsafe.AsPointer(ref this));
    }

    public interface Interface : IDXGIFactory.Interface
    {
        [return: NativeTypeName("HRESULT")]
        int EnumAdapters1(uint Adapter, IDXGIAdapter1** ppAdapter);

        [return: NativeTypeName("BOOL")]
        int IsCurrent();
    }
}

[Guid("29038F61-3839-4626-91FD-086879011A05")]
[NativeTypeName("struct IDXGIAdapter1 : IDXGIAdapter")]
internal unsafe partial struct IDXGIAdapter1 : IDXGIAdapter1.Interface, INativeGuid
{
    static Guid* INativeGuid.NativeGuid => (Guid*)Unsafe.AsPointer(in IID_IDXGIAdapter1);

    public void** lpVtbl;

    [return: NativeTypeName("HRESULT")]
    public int QueryInterface([NativeTypeName("const IID &")] Guid* riid, void** ppvObject)
    {
        return ((delegate* unmanaged[Stdcall]<IDXGIAdapter1*, Guid*, void**, int>)(lpVtbl[0]))((IDXGIAdapter1*)Unsafe.AsPointer(ref this), riid, ppvObject);
    }

    [return: NativeTypeName("ULONG")]
    public uint AddRef()
    {
        return ((delegate* unmanaged[Stdcall]<IDXGIAdapter1*, uint>)(lpVtbl[1]))((IDXGIAdapter1*)Unsafe.AsPointer(ref this));
    }

    [return: NativeTypeName("ULONG")]
    public uint Release()
    {
        return ((delegate* unmanaged[Stdcall]<IDXGIAdapter1*, uint>)(lpVtbl[2]))((IDXGIAdapter1*)Unsafe.AsPointer(ref this));
    }

    [return: NativeTypeName("HRESULT")]
    public int SetPrivateData([NativeTypeName("const GUID &")] Guid* Name, uint DataSize, [NativeTypeName("const void *")] void* pData)
    {
        return ((delegate* unmanaged[Stdcall]<IDXGIAdapter1*, Guid*, uint, void*, int>)(lpVtbl[3]))((IDXGIAdapter1*)Unsafe.AsPointer(ref this), Name, DataSize, pData);
    }

    [return: NativeTypeName("HRESULT")]
    public int SetPrivateDataInterface([NativeTypeName("const GUID &")] Guid* Name, [NativeTypeName("const IUnknown *")] IUnknown* pUnknown)
    {
        return ((delegate* unmanaged[Stdcall]<IDXGIAdapter1*, Guid*, IUnknown*, int>)(lpVtbl[4]))((IDXGIAdapter1*)Unsafe.AsPointer(ref this), Name, pUnknown);
    }

    [return: NativeTypeName("HRESULT")]
    public int GetPrivateData([NativeTypeName("const GUID &")] Guid* Name, uint* pDataSize, void* pData)
    {
        return ((delegate* unmanaged[Stdcall]<IDXGIAdapter1*, Guid*, uint*, void*, int>)(lpVtbl[5]))((IDXGIAdapter1*)Unsafe.AsPointer(ref this), Name, pDataSize, pData);
    }

    [return: NativeTypeName("HRESULT")]
    public int GetParent([NativeTypeName("const IID &")] Guid* riid, void** ppParent)
    {
        return ((delegate* unmanaged[Stdcall]<IDXGIAdapter1*, Guid*, void**, int>)(lpVtbl[6]))((IDXGIAdapter1*)Unsafe.AsPointer(ref this), riid, ppParent);
    }

    [return: NativeTypeName("HRESULT")]
    public int EnumOutputs(uint Output, IDXGIOutput** ppOutput)
    {
        return ((delegate* unmanaged[Stdcall]<IDXGIAdapter1*, uint, IDXGIOutput**, int>)(lpVtbl[7]))((IDXGIAdapter1*)Unsafe.AsPointer(ref this), Output, ppOutput);
    }

    [return: NativeTypeName("HRESULT")]
    public int GetDesc(DXGI_ADAPTER_DESC* pDesc)
    {
        return ((delegate* unmanaged[Stdcall]<IDXGIAdapter1*, DXGI_ADAPTER_DESC*, int>)(lpVtbl[8]))((IDXGIAdapter1*)Unsafe.AsPointer(ref this), pDesc);
    }

    [return: NativeTypeName("HRESULT")]
    public int CheckInterfaceSupport([NativeTypeName("const GUID &")] Guid* InterfaceName, LARGE_INTEGER* pUMDVersion)
    {
        return ((delegate* unmanaged[Stdcall]<IDXGIAdapter1*, Guid*, LARGE_INTEGER*, int>)(lpVtbl[9]))((IDXGIAdapter1*)Unsafe.AsPointer(ref this), InterfaceName, pUMDVersion);
    }

    [return: NativeTypeName("HRESULT")]
    public int GetDesc1(DXGI_ADAPTER_DESC1* pDesc)
    {
        return ((delegate* unmanaged[Stdcall]<IDXGIAdapter1*, DXGI_ADAPTER_DESC1*, int>)(lpVtbl[10]))((IDXGIAdapter1*)Unsafe.AsPointer(ref this), pDesc);
    }

    public interface Interface : IDXGIAdapter.Interface
    {
        [return: NativeTypeName("HRESULT")]
        int GetDesc1(DXGI_ADAPTER_DESC1* pDesc);
    }
}

[Guid("77DB970F-6276-48BA-BA28-070143B4392C")]
[NativeTypeName("struct IDXGIDevice1 : IDXGIDevice")]
internal unsafe partial struct IDXGIDevice1 : IDXGIDevice1.Interface, INativeGuid
{
    static Guid* INativeGuid.NativeGuid => (Guid*)Unsafe.AsPointer(in IID_IDXGIDevice1);

    public void** lpVtbl;

    [return: NativeTypeName("HRESULT")]
    public int QueryInterface([NativeTypeName("const IID &")] Guid* riid, void** ppvObject)
    {
        return ((delegate* unmanaged[Stdcall]<IDXGIDevice1*, Guid*, void**, int>)(lpVtbl[0]))((IDXGIDevice1*)Unsafe.AsPointer(ref this), riid, ppvObject);
    }

    [return: NativeTypeName("ULONG")]
    public uint AddRef()
    {
        return ((delegate* unmanaged[Stdcall]<IDXGIDevice1*, uint>)(lpVtbl[1]))((IDXGIDevice1*)Unsafe.AsPointer(ref this));
    }

    [return: NativeTypeName("ULONG")]
    public uint Release()
    {
        return ((delegate* unmanaged[Stdcall]<IDXGIDevice1*, uint>)(lpVtbl[2]))((IDXGIDevice1*)Unsafe.AsPointer(ref this));
    }

    [return: NativeTypeName("HRESULT")]
    public int SetPrivateData([NativeTypeName("const GUID &")] Guid* Name, uint DataSize, [NativeTypeName("const void *")] void* pData)
    {
        return ((delegate* unmanaged[Stdcall]<IDXGIDevice1*, Guid*, uint, void*, int>)(lpVtbl[3]))((IDXGIDevice1*)Unsafe.AsPointer(ref this), Name, DataSize, pData);
    }

    [return: NativeTypeName("HRESULT")]
    public int SetPrivateDataInterface([NativeTypeName("const GUID &")] Guid* Name, [NativeTypeName("const IUnknown *")] IUnknown* pUnknown)
    {
        return ((delegate* unmanaged[Stdcall]<IDXGIDevice1*, Guid*, IUnknown*, int>)(lpVtbl[4]))((IDXGIDevice1*)Unsafe.AsPointer(ref this), Name, pUnknown);
    }

    [return: NativeTypeName("HRESULT")]
    public int GetPrivateData([NativeTypeName("const GUID &")] Guid* Name, uint* pDataSize, void* pData)
    {
        return ((delegate* unmanaged[Stdcall]<IDXGIDevice1*, Guid*, uint*, void*, int>)(lpVtbl[5]))((IDXGIDevice1*)Unsafe.AsPointer(ref this), Name, pDataSize, pData);
    }

    [return: NativeTypeName("HRESULT")]
    public int GetParent([NativeTypeName("const IID &")] Guid* riid, void** ppParent)
    {
        return ((delegate* unmanaged[Stdcall]<IDXGIDevice1*, Guid*, void**, int>)(lpVtbl[6]))((IDXGIDevice1*)Unsafe.AsPointer(ref this), riid, ppParent);
    }

    [return: NativeTypeName("HRESULT")]
    public int GetAdapter(IDXGIAdapter** pAdapter)
    {
        return ((delegate* unmanaged[Stdcall]<IDXGIDevice1*, IDXGIAdapter**, int>)(lpVtbl[7]))((IDXGIDevice1*)Unsafe.AsPointer(ref this), pAdapter);
    }

    [return: NativeTypeName("HRESULT")]
    public int CreateSurface([NativeTypeName("const DXGI_SURFACE_DESC *")] DXGI_SURFACE_DESC* pDesc, uint NumSurfaces, [NativeTypeName("DXGI_USAGE")] uint Usage, [NativeTypeName("const DXGI_SHARED_RESOURCE *")] DXGI_SHARED_RESOURCE* pSharedResource, IDXGISurface** ppSurface)
    {
        return ((delegate* unmanaged[Stdcall]<IDXGIDevice1*, DXGI_SURFACE_DESC*, uint, uint, DXGI_SHARED_RESOURCE*, IDXGISurface**, int>)(lpVtbl[8]))((IDXGIDevice1*)Unsafe.AsPointer(ref this), pDesc, NumSurfaces, Usage, pSharedResource, ppSurface);
    }

    [return: NativeTypeName("HRESULT")]
    public int QueryResourceResidency([NativeTypeName("IUnknown *const *")] IUnknown** ppResources, DXGI_RESIDENCY* pResidencyStatus, uint NumResources)
    {
        return ((delegate* unmanaged[Stdcall]<IDXGIDevice1*, IUnknown**, DXGI_RESIDENCY*, uint, int>)(lpVtbl[9]))((IDXGIDevice1*)Unsafe.AsPointer(ref this), ppResources, pResidencyStatus, NumResources);
    }

    [return: NativeTypeName("HRESULT")]
    public int SetGPUThreadPriority(int Priority)
    {
        return ((delegate* unmanaged[Stdcall]<IDXGIDevice1*, int, int>)(lpVtbl[10]))((IDXGIDevice1*)Unsafe.AsPointer(ref this), Priority);
    }

    [return: NativeTypeName("HRESULT")]
    public int GetGPUThreadPriority(int* pPriority)
    {
        return ((delegate* unmanaged[Stdcall]<IDXGIDevice1*, int*, int>)(lpVtbl[11]))((IDXGIDevice1*)Unsafe.AsPointer(ref this), pPriority);
    }

    [return: NativeTypeName("HRESULT")]
    public int SetMaximumFrameLatency(uint MaxLatency)
    {
        return ((delegate* unmanaged[Stdcall]<IDXGIDevice1*, uint, int>)(lpVtbl[12]))((IDXGIDevice1*)Unsafe.AsPointer(ref this), MaxLatency);
    }

    [return: NativeTypeName("HRESULT")]
    public int GetMaximumFrameLatency(uint* pMaxLatency)
    {
        return ((delegate* unmanaged[Stdcall]<IDXGIDevice1*, uint*, int>)(lpVtbl[13]))((IDXGIDevice1*)Unsafe.AsPointer(ref this), pMaxLatency);
    }

    public interface Interface : IDXGIDevice.Interface
    {
        [return: NativeTypeName("HRESULT")]
        int SetMaximumFrameLatency(uint MaxLatency);

        [return: NativeTypeName("HRESULT")]
        int GetMaximumFrameLatency(uint* pMaxLatency);
    }
}

[Guid("EA9DBF1A-C88E-4486-854A-98AA0138F30C")]
[NativeTypeName("struct IDXGIDisplayControl : IUnknown")]
internal unsafe partial struct IDXGIDisplayControl : IDXGIDisplayControl.Interface, INativeGuid
{
    static Guid* INativeGuid.NativeGuid => (Guid*)Unsafe.AsPointer(in IID_IDXGIDisplayControl);

    public void** lpVtbl;

    [return: NativeTypeName("HRESULT")]
    public int QueryInterface([NativeTypeName("const IID &")] Guid* riid, void** ppvObject)
    {
        return ((delegate* unmanaged[Stdcall]<IDXGIDisplayControl*, Guid*, void**, int>)(lpVtbl[0]))((IDXGIDisplayControl*)Unsafe.AsPointer(ref this), riid, ppvObject);
    }

    [return: NativeTypeName("ULONG")]
    public uint AddRef()
    {
        return ((delegate* unmanaged[Stdcall]<IDXGIDisplayControl*, uint>)(lpVtbl[1]))((IDXGIDisplayControl*)Unsafe.AsPointer(ref this));
    }

    [return: NativeTypeName("ULONG")]
    public uint Release()
    {
        return ((delegate* unmanaged[Stdcall]<IDXGIDisplayControl*, uint>)(lpVtbl[2]))((IDXGIDisplayControl*)Unsafe.AsPointer(ref this));
    }

    [return: NativeTypeName("BOOL")]
    public int IsStereoEnabled()
    {
        return ((delegate* unmanaged[Stdcall]<IDXGIDisplayControl*, int>)(lpVtbl[3]))((IDXGIDisplayControl*)Unsafe.AsPointer(ref this));
    }

    public void SetStereoEnabled([NativeTypeName("BOOL")] int enabled)
    {
        ((delegate* unmanaged[Stdcall]<IDXGIDisplayControl*, int, void>)(lpVtbl[4]))((IDXGIDisplayControl*)Unsafe.AsPointer(ref this), enabled);
    }

    public interface Interface : IUnknown.Interface
    {
        [return: NativeTypeName("BOOL")]
        int IsStereoEnabled();

        void SetStereoEnabled([NativeTypeName("BOOL")] int enabled);
    }
}

internal partial struct DXGI_OUTDUPL_MOVE_RECT
{
    public POINT SourcePoint;

    public RECT DestinationRect;
}

internal partial struct DXGI_OUTDUPL_DESC
{
    public DXGI_MODE_DESC ModeDesc;

    public DXGI_MODE_ROTATION Rotation;

    [NativeTypeName("BOOL")]
    public int DesktopImageInSystemMemory;
}

internal partial struct DXGI_OUTDUPL_POINTER_POSITION
{
    public POINT Position;

    [NativeTypeName("BOOL")]
    public int Visible;
}

internal enum DXGI_OUTDUPL_POINTER_SHAPE_TYPE
{
    DXGI_OUTDUPL_POINTER_SHAPE_TYPE_MONOCHROME = 0x1,
    DXGI_OUTDUPL_POINTER_SHAPE_TYPE_COLOR = 0x2,
    DXGI_OUTDUPL_POINTER_SHAPE_TYPE_MASKED_COLOR = 0x4,
}

internal partial struct DXGI_OUTDUPL_POINTER_SHAPE_INFO
{
    public uint Type;

    public uint Width;

    public uint Height;

    public uint Pitch;

    public POINT HotSpot;
}

internal partial struct DXGI_OUTDUPL_FRAME_INFO
{
    public LARGE_INTEGER LastPresentTime;

    public LARGE_INTEGER LastMouseUpdateTime;

    public uint AccumulatedFrames;

    [NativeTypeName("BOOL")]
    public int RectsCoalesced;

    [NativeTypeName("BOOL")]
    public int ProtectedContentMaskedOut;

    public DXGI_OUTDUPL_POINTER_POSITION PointerPosition;

    public uint TotalMetadataBufferSize;

    public uint PointerShapeBufferSize;
}

[Guid("191CFAC3-A341-470D-B26E-A864F428319C")]
[NativeTypeName("struct IDXGIOutputDuplication : IDXGIObject")]
internal unsafe partial struct IDXGIOutputDuplication : IDXGIOutputDuplication.Interface, INativeGuid
{
    static Guid* INativeGuid.NativeGuid => (Guid*)Unsafe.AsPointer(in IID_IDXGIOutputDuplication);

    public void** lpVtbl;

    [return: NativeTypeName("HRESULT")]
    public int QueryInterface([NativeTypeName("const IID &")] Guid* riid, void** ppvObject)
    {
        return ((delegate* unmanaged[Stdcall]<IDXGIOutputDuplication*, Guid*, void**, int>)(lpVtbl[0]))((IDXGIOutputDuplication*)Unsafe.AsPointer(ref this), riid, ppvObject);
    }

    [return: NativeTypeName("ULONG")]
    public uint AddRef()
    {
        return ((delegate* unmanaged[Stdcall]<IDXGIOutputDuplication*, uint>)(lpVtbl[1]))((IDXGIOutputDuplication*)Unsafe.AsPointer(ref this));
    }

    [return: NativeTypeName("ULONG")]
    public uint Release()
    {
        return ((delegate* unmanaged[Stdcall]<IDXGIOutputDuplication*, uint>)(lpVtbl[2]))((IDXGIOutputDuplication*)Unsafe.AsPointer(ref this));
    }

    [return: NativeTypeName("HRESULT")]
    public int SetPrivateData([NativeTypeName("const GUID &")] Guid* Name, uint DataSize, [NativeTypeName("const void *")] void* pData)
    {
        return ((delegate* unmanaged[Stdcall]<IDXGIOutputDuplication*, Guid*, uint, void*, int>)(lpVtbl[3]))((IDXGIOutputDuplication*)Unsafe.AsPointer(ref this), Name, DataSize, pData);
    }

    [return: NativeTypeName("HRESULT")]
    public int SetPrivateDataInterface([NativeTypeName("const GUID &")] Guid* Name, [NativeTypeName("const IUnknown *")] IUnknown* pUnknown)
    {
        return ((delegate* unmanaged[Stdcall]<IDXGIOutputDuplication*, Guid*, IUnknown*, int>)(lpVtbl[4]))((IDXGIOutputDuplication*)Unsafe.AsPointer(ref this), Name, pUnknown);
    }

    [return: NativeTypeName("HRESULT")]
    public int GetPrivateData([NativeTypeName("const GUID &")] Guid* Name, uint* pDataSize, void* pData)
    {
        return ((delegate* unmanaged[Stdcall]<IDXGIOutputDuplication*, Guid*, uint*, void*, int>)(lpVtbl[5]))((IDXGIOutputDuplication*)Unsafe.AsPointer(ref this), Name, pDataSize, pData);
    }

    [return: NativeTypeName("HRESULT")]
    public int GetParent([NativeTypeName("const IID &")] Guid* riid, void** ppParent)
    {
        return ((delegate* unmanaged[Stdcall]<IDXGIOutputDuplication*, Guid*, void**, int>)(lpVtbl[6]))((IDXGIOutputDuplication*)Unsafe.AsPointer(ref this), riid, ppParent);
    }

    public void GetDesc(DXGI_OUTDUPL_DESC* pDesc)
    {
        ((delegate* unmanaged[Stdcall]<IDXGIOutputDuplication*, DXGI_OUTDUPL_DESC*, void>)(lpVtbl[7]))((IDXGIOutputDuplication*)Unsafe.AsPointer(ref this), pDesc);
    }

    [return: NativeTypeName("HRESULT")]
    public int AcquireNextFrame(uint TimeoutInMilliseconds, DXGI_OUTDUPL_FRAME_INFO* pFrameInfo, IDXGIResource** ppDesktopResource)
    {
        return ((delegate* unmanaged[Stdcall]<IDXGIOutputDuplication*, uint, DXGI_OUTDUPL_FRAME_INFO*, IDXGIResource**, int>)(lpVtbl[8]))((IDXGIOutputDuplication*)Unsafe.AsPointer(ref this), TimeoutInMilliseconds, pFrameInfo, ppDesktopResource);
    }

    [return: NativeTypeName("HRESULT")]
    public int GetFrameDirtyRects(uint DirtyRectsBufferSize, RECT* pDirtyRectsBuffer, uint* pDirtyRectsBufferSizeRequired)
    {
        return ((delegate* unmanaged[Stdcall]<IDXGIOutputDuplication*, uint, RECT*, uint*, int>)(lpVtbl[9]))((IDXGIOutputDuplication*)Unsafe.AsPointer(ref this), DirtyRectsBufferSize, pDirtyRectsBuffer, pDirtyRectsBufferSizeRequired);
    }

    [return: NativeTypeName("HRESULT")]
    public int GetFrameMoveRects(uint MoveRectsBufferSize, DXGI_OUTDUPL_MOVE_RECT* pMoveRectBuffer, uint* pMoveRectsBufferSizeRequired)
    {
        return ((delegate* unmanaged[Stdcall]<IDXGIOutputDuplication*, uint, DXGI_OUTDUPL_MOVE_RECT*, uint*, int>)(lpVtbl[10]))((IDXGIOutputDuplication*)Unsafe.AsPointer(ref this), MoveRectsBufferSize, pMoveRectBuffer, pMoveRectsBufferSizeRequired);
    }

    [return: NativeTypeName("HRESULT")]
    public int GetFramePointerShape(uint PointerShapeBufferSize, void* pPointerShapeBuffer, uint* pPointerShapeBufferSizeRequired, DXGI_OUTDUPL_POINTER_SHAPE_INFO* pPointerShapeInfo)
    {
        return ((delegate* unmanaged[Stdcall]<IDXGIOutputDuplication*, uint, void*, uint*, DXGI_OUTDUPL_POINTER_SHAPE_INFO*, int>)(lpVtbl[11]))((IDXGIOutputDuplication*)Unsafe.AsPointer(ref this), PointerShapeBufferSize, pPointerShapeBuffer, pPointerShapeBufferSizeRequired, pPointerShapeInfo);
    }

    [return: NativeTypeName("HRESULT")]
    public int MapDesktopSurface(DXGI_MAPPED_RECT* pLockedRect)
    {
        return ((delegate* unmanaged[Stdcall]<IDXGIOutputDuplication*, DXGI_MAPPED_RECT*, int>)(lpVtbl[12]))((IDXGIOutputDuplication*)Unsafe.AsPointer(ref this), pLockedRect);
    }

    [return: NativeTypeName("HRESULT")]
    public int UnMapDesktopSurface()
    {
        return ((delegate* unmanaged[Stdcall]<IDXGIOutputDuplication*, int>)(lpVtbl[13]))((IDXGIOutputDuplication*)Unsafe.AsPointer(ref this));
    }

    [return: NativeTypeName("HRESULT")]
    public int ReleaseFrame()
    {
        return ((delegate* unmanaged[Stdcall]<IDXGIOutputDuplication*, int>)(lpVtbl[14]))((IDXGIOutputDuplication*)Unsafe.AsPointer(ref this));
    }

    public interface Interface : IDXGIObject.Interface
    {
        void GetDesc(DXGI_OUTDUPL_DESC* pDesc);

        [return: NativeTypeName("HRESULT")]
        int AcquireNextFrame(uint TimeoutInMilliseconds, DXGI_OUTDUPL_FRAME_INFO* pFrameInfo, IDXGIResource** ppDesktopResource);

        [return: NativeTypeName("HRESULT")]
        int GetFrameDirtyRects(uint DirtyRectsBufferSize, RECT* pDirtyRectsBuffer, uint* pDirtyRectsBufferSizeRequired);

        [return: NativeTypeName("HRESULT")]
        int GetFrameMoveRects(uint MoveRectsBufferSize, DXGI_OUTDUPL_MOVE_RECT* pMoveRectBuffer, uint* pMoveRectsBufferSizeRequired);

        [return: NativeTypeName("HRESULT")]
        int GetFramePointerShape(uint PointerShapeBufferSize, void* pPointerShapeBuffer, uint* pPointerShapeBufferSizeRequired, DXGI_OUTDUPL_POINTER_SHAPE_INFO* pPointerShapeInfo);

        [return: NativeTypeName("HRESULT")]
        int MapDesktopSurface(DXGI_MAPPED_RECT* pLockedRect);

        [return: NativeTypeName("HRESULT")]
        int UnMapDesktopSurface();

        [return: NativeTypeName("HRESULT")]
        int ReleaseFrame();
    }
}

internal enum DXGI_ALPHA_MODE
{
    DXGI_ALPHA_MODE_UNSPECIFIED = 0,
    DXGI_ALPHA_MODE_PREMULTIPLIED = 1,
    DXGI_ALPHA_MODE_STRAIGHT = 2,
    DXGI_ALPHA_MODE_IGNORE = 3,
    DXGI_ALPHA_MODE_FORCE_DWORD = unchecked((int)(0xffffffff)),
}

[Guid("ABA496DD-B617-4CB8-A866-BC44D7EB1FA2")]
[NativeTypeName("struct IDXGISurface2 : IDXGISurface1")]
internal unsafe partial struct IDXGISurface2 : IDXGISurface2.Interface, INativeGuid
{
    static Guid* INativeGuid.NativeGuid => (Guid*)Unsafe.AsPointer(in IID_IDXGISurface2);

    public void** lpVtbl;

    [return: NativeTypeName("HRESULT")]
    public int QueryInterface([NativeTypeName("const IID &")] Guid* riid, void** ppvObject)
    {
        return ((delegate* unmanaged[Stdcall]<IDXGISurface2*, Guid*, void**, int>)(lpVtbl[0]))((IDXGISurface2*)Unsafe.AsPointer(ref this), riid, ppvObject);
    }

    [return: NativeTypeName("ULONG")]
    public uint AddRef()
    {
        return ((delegate* unmanaged[Stdcall]<IDXGISurface2*, uint>)(lpVtbl[1]))((IDXGISurface2*)Unsafe.AsPointer(ref this));
    }

    [return: NativeTypeName("ULONG")]
    public uint Release()
    {
        return ((delegate* unmanaged[Stdcall]<IDXGISurface2*, uint>)(lpVtbl[2]))((IDXGISurface2*)Unsafe.AsPointer(ref this));
    }

    [return: NativeTypeName("HRESULT")]
    public int SetPrivateData([NativeTypeName("const GUID &")] Guid* Name, uint DataSize, [NativeTypeName("const void *")] void* pData)
    {
        return ((delegate* unmanaged[Stdcall]<IDXGISurface2*, Guid*, uint, void*, int>)(lpVtbl[3]))((IDXGISurface2*)Unsafe.AsPointer(ref this), Name, DataSize, pData);
    }

    [return: NativeTypeName("HRESULT")]
    public int SetPrivateDataInterface([NativeTypeName("const GUID &")] Guid* Name, [NativeTypeName("const IUnknown *")] IUnknown* pUnknown)
    {
        return ((delegate* unmanaged[Stdcall]<IDXGISurface2*, Guid*, IUnknown*, int>)(lpVtbl[4]))((IDXGISurface2*)Unsafe.AsPointer(ref this), Name, pUnknown);
    }

    [return: NativeTypeName("HRESULT")]
    public int GetPrivateData([NativeTypeName("const GUID &")] Guid* Name, uint* pDataSize, void* pData)
    {
        return ((delegate* unmanaged[Stdcall]<IDXGISurface2*, Guid*, uint*, void*, int>)(lpVtbl[5]))((IDXGISurface2*)Unsafe.AsPointer(ref this), Name, pDataSize, pData);
    }

    [return: NativeTypeName("HRESULT")]
    public int GetParent([NativeTypeName("const IID &")] Guid* riid, void** ppParent)
    {
        return ((delegate* unmanaged[Stdcall]<IDXGISurface2*, Guid*, void**, int>)(lpVtbl[6]))((IDXGISurface2*)Unsafe.AsPointer(ref this), riid, ppParent);
    }

    [return: NativeTypeName("HRESULT")]
    public int GetDevice([NativeTypeName("const IID &")] Guid* riid, void** ppDevice)
    {
        return ((delegate* unmanaged[Stdcall]<IDXGISurface2*, Guid*, void**, int>)(lpVtbl[7]))((IDXGISurface2*)Unsafe.AsPointer(ref this), riid, ppDevice);
    }

    [return: NativeTypeName("HRESULT")]
    public int GetDesc(DXGI_SURFACE_DESC* pDesc)
    {
        return ((delegate* unmanaged[Stdcall]<IDXGISurface2*, DXGI_SURFACE_DESC*, int>)(lpVtbl[8]))((IDXGISurface2*)Unsafe.AsPointer(ref this), pDesc);
    }

    [return: NativeTypeName("HRESULT")]
    public int Map(DXGI_MAPPED_RECT* pLockedRect, uint MapFlags)
    {
        return ((delegate* unmanaged[Stdcall]<IDXGISurface2*, DXGI_MAPPED_RECT*, uint, int>)(lpVtbl[9]))((IDXGISurface2*)Unsafe.AsPointer(ref this), pLockedRect, MapFlags);
    }

    [return: NativeTypeName("HRESULT")]
    public int Unmap()
    {
        return ((delegate* unmanaged[Stdcall]<IDXGISurface2*, int>)(lpVtbl[10]))((IDXGISurface2*)Unsafe.AsPointer(ref this));
    }

    [return: NativeTypeName("HRESULT")]
    public int GetDC([NativeTypeName("BOOL")] int Discard, [NativeTypeName("HDC *")] HDC__** phdc)
    {
        return ((delegate* unmanaged[Stdcall]<IDXGISurface2*, int, HDC__**, int>)(lpVtbl[11]))((IDXGISurface2*)Unsafe.AsPointer(ref this), Discard, phdc);
    }

    [return: NativeTypeName("HRESULT")]
    public int ReleaseDC(RECT* pDirtyRect)
    {
        return ((delegate* unmanaged[Stdcall]<IDXGISurface2*, RECT*, int>)(lpVtbl[12]))((IDXGISurface2*)Unsafe.AsPointer(ref this), pDirtyRect);
    }

    [return: NativeTypeName("HRESULT")]
    public int GetResource([NativeTypeName("const IID &")] Guid* riid, void** ppParentResource, uint* pSubresourceIndex)
    {
        return ((delegate* unmanaged[Stdcall]<IDXGISurface2*, Guid*, void**, uint*, int>)(lpVtbl[13]))((IDXGISurface2*)Unsafe.AsPointer(ref this), riid, ppParentResource, pSubresourceIndex);
    }

    public interface Interface : IDXGISurface1.Interface
    {
        [return: NativeTypeName("HRESULT")]
        int GetResource([NativeTypeName("const IID &")] Guid* riid, void** ppParentResource, uint* pSubresourceIndex);
    }
}

[Guid("30961379-4609-4A41-998E-54FE567EE0C1")]
[NativeTypeName("struct IDXGIResource1 : IDXGIResource")]
internal unsafe partial struct IDXGIResource1 : IDXGIResource1.Interface, INativeGuid
{
    static Guid* INativeGuid.NativeGuid => (Guid*)Unsafe.AsPointer(in IID_IDXGIResource1);

    public void** lpVtbl;

    [return: NativeTypeName("HRESULT")]
    public int QueryInterface([NativeTypeName("const IID &")] Guid* riid, void** ppvObject)
    {
        return ((delegate* unmanaged[Stdcall]<IDXGIResource1*, Guid*, void**, int>)(lpVtbl[0]))((IDXGIResource1*)Unsafe.AsPointer(ref this), riid, ppvObject);
    }

    [return: NativeTypeName("ULONG")]
    public uint AddRef()
    {
        return ((delegate* unmanaged[Stdcall]<IDXGIResource1*, uint>)(lpVtbl[1]))((IDXGIResource1*)Unsafe.AsPointer(ref this));
    }

    [return: NativeTypeName("ULONG")]
    public uint Release()
    {
        return ((delegate* unmanaged[Stdcall]<IDXGIResource1*, uint>)(lpVtbl[2]))((IDXGIResource1*)Unsafe.AsPointer(ref this));
    }

    [return: NativeTypeName("HRESULT")]
    public int SetPrivateData([NativeTypeName("const GUID &")] Guid* Name, uint DataSize, [NativeTypeName("const void *")] void* pData)
    {
        return ((delegate* unmanaged[Stdcall]<IDXGIResource1*, Guid*, uint, void*, int>)(lpVtbl[3]))((IDXGIResource1*)Unsafe.AsPointer(ref this), Name, DataSize, pData);
    }

    [return: NativeTypeName("HRESULT")]
    public int SetPrivateDataInterface([NativeTypeName("const GUID &")] Guid* Name, [NativeTypeName("const IUnknown *")] IUnknown* pUnknown)
    {
        return ((delegate* unmanaged[Stdcall]<IDXGIResource1*, Guid*, IUnknown*, int>)(lpVtbl[4]))((IDXGIResource1*)Unsafe.AsPointer(ref this), Name, pUnknown);
    }

    [return: NativeTypeName("HRESULT")]
    public int GetPrivateData([NativeTypeName("const GUID &")] Guid* Name, uint* pDataSize, void* pData)
    {
        return ((delegate* unmanaged[Stdcall]<IDXGIResource1*, Guid*, uint*, void*, int>)(lpVtbl[5]))((IDXGIResource1*)Unsafe.AsPointer(ref this), Name, pDataSize, pData);
    }

    [return: NativeTypeName("HRESULT")]
    public int GetParent([NativeTypeName("const IID &")] Guid* riid, void** ppParent)
    {
        return ((delegate* unmanaged[Stdcall]<IDXGIResource1*, Guid*, void**, int>)(lpVtbl[6]))((IDXGIResource1*)Unsafe.AsPointer(ref this), riid, ppParent);
    }

    [return: NativeTypeName("HRESULT")]
    public int GetDevice([NativeTypeName("const IID &")] Guid* riid, void** ppDevice)
    {
        return ((delegate* unmanaged[Stdcall]<IDXGIResource1*, Guid*, void**, int>)(lpVtbl[7]))((IDXGIResource1*)Unsafe.AsPointer(ref this), riid, ppDevice);
    }

    [return: NativeTypeName("HRESULT")]
    public int GetSharedHandle([NativeTypeName("HANDLE *")] void** pSharedHandle)
    {
        return ((delegate* unmanaged[Stdcall]<IDXGIResource1*, void**, int>)(lpVtbl[8]))((IDXGIResource1*)Unsafe.AsPointer(ref this), pSharedHandle);
    }

    [return: NativeTypeName("HRESULT")]
    public int GetUsage([NativeTypeName("DXGI_USAGE *")] uint* pUsage)
    {
        return ((delegate* unmanaged[Stdcall]<IDXGIResource1*, uint*, int>)(lpVtbl[9]))((IDXGIResource1*)Unsafe.AsPointer(ref this), pUsage);
    }

    [return: NativeTypeName("HRESULT")]
    public int SetEvictionPriority(uint EvictionPriority)
    {
        return ((delegate* unmanaged[Stdcall]<IDXGIResource1*, uint, int>)(lpVtbl[10]))((IDXGIResource1*)Unsafe.AsPointer(ref this), EvictionPriority);
    }

    [return: NativeTypeName("HRESULT")]
    public int GetEvictionPriority(uint* pEvictionPriority)
    {
        return ((delegate* unmanaged[Stdcall]<IDXGIResource1*, uint*, int>)(lpVtbl[11]))((IDXGIResource1*)Unsafe.AsPointer(ref this), pEvictionPriority);
    }

    [return: NativeTypeName("HRESULT")]
    public int CreateSubresourceSurface(uint index, IDXGISurface2** ppSurface)
    {
        return ((delegate* unmanaged[Stdcall]<IDXGIResource1*, uint, IDXGISurface2**, int>)(lpVtbl[12]))((IDXGIResource1*)Unsafe.AsPointer(ref this), index, ppSurface);
    }

    [return: NativeTypeName("HRESULT")]
    public int CreateSharedHandle([NativeTypeName("const SECURITY_ATTRIBUTES *")] SECURITY_ATTRIBUTES* pAttributes, [NativeTypeName("DWORD")] uint dwAccess, [NativeTypeName("LPCWSTR")] char* lpName, [NativeTypeName("HANDLE *")] void** pHandle)
    {
        return ((delegate* unmanaged[Stdcall]<IDXGIResource1*, SECURITY_ATTRIBUTES*, uint, char*, void**, int>)(lpVtbl[13]))((IDXGIResource1*)Unsafe.AsPointer(ref this), pAttributes, dwAccess, lpName, pHandle);
    }

    public interface Interface : IDXGIResource.Interface
    {
        [return: NativeTypeName("HRESULT")]
        int CreateSubresourceSurface(uint index, IDXGISurface2** ppSurface);

        [return: NativeTypeName("HRESULT")]
        int CreateSharedHandle([NativeTypeName("const SECURITY_ATTRIBUTES *")] SECURITY_ATTRIBUTES* pAttributes, [NativeTypeName("DWORD")] uint dwAccess, [NativeTypeName("LPCWSTR")] char* lpName, [NativeTypeName("HANDLE *")] void** pHandle);
    }
}

internal enum DXGI_OFFER_RESOURCE_PRIORITY
{
    DXGI_OFFER_RESOURCE_PRIORITY_LOW = 1,
    DXGI_OFFER_RESOURCE_PRIORITY_NORMAL = (DXGI_OFFER_RESOURCE_PRIORITY_LOW + 1),
    DXGI_OFFER_RESOURCE_PRIORITY_HIGH = (DXGI_OFFER_RESOURCE_PRIORITY_NORMAL + 1),
}

[Guid("05008617-FBFD-4051-A790-144884B4F6A9")]
[NativeTypeName("struct IDXGIDevice2 : IDXGIDevice1")]
internal unsafe partial struct IDXGIDevice2 : IDXGIDevice2.Interface, INativeGuid
{
    static Guid* INativeGuid.NativeGuid => (Guid*)Unsafe.AsPointer(in IID_IDXGIDevice2);

    public void** lpVtbl;

    [return: NativeTypeName("HRESULT")]
    public int QueryInterface([NativeTypeName("const IID &")] Guid* riid, void** ppvObject)
    {
        return ((delegate* unmanaged[Stdcall]<IDXGIDevice2*, Guid*, void**, int>)(lpVtbl[0]))((IDXGIDevice2*)Unsafe.AsPointer(ref this), riid, ppvObject);
    }

    [return: NativeTypeName("ULONG")]
    public uint AddRef()
    {
        return ((delegate* unmanaged[Stdcall]<IDXGIDevice2*, uint>)(lpVtbl[1]))((IDXGIDevice2*)Unsafe.AsPointer(ref this));
    }

    [return: NativeTypeName("ULONG")]
    public uint Release()
    {
        return ((delegate* unmanaged[Stdcall]<IDXGIDevice2*, uint>)(lpVtbl[2]))((IDXGIDevice2*)Unsafe.AsPointer(ref this));
    }

    [return: NativeTypeName("HRESULT")]
    public int SetPrivateData([NativeTypeName("const GUID &")] Guid* Name, uint DataSize, [NativeTypeName("const void *")] void* pData)
    {
        return ((delegate* unmanaged[Stdcall]<IDXGIDevice2*, Guid*, uint, void*, int>)(lpVtbl[3]))((IDXGIDevice2*)Unsafe.AsPointer(ref this), Name, DataSize, pData);
    }

    [return: NativeTypeName("HRESULT")]
    public int SetPrivateDataInterface([NativeTypeName("const GUID &")] Guid* Name, [NativeTypeName("const IUnknown *")] IUnknown* pUnknown)
    {
        return ((delegate* unmanaged[Stdcall]<IDXGIDevice2*, Guid*, IUnknown*, int>)(lpVtbl[4]))((IDXGIDevice2*)Unsafe.AsPointer(ref this), Name, pUnknown);
    }

    [return: NativeTypeName("HRESULT")]
    public int GetPrivateData([NativeTypeName("const GUID &")] Guid* Name, uint* pDataSize, void* pData)
    {
        return ((delegate* unmanaged[Stdcall]<IDXGIDevice2*, Guid*, uint*, void*, int>)(lpVtbl[5]))((IDXGIDevice2*)Unsafe.AsPointer(ref this), Name, pDataSize, pData);
    }

    [return: NativeTypeName("HRESULT")]
    public int GetParent([NativeTypeName("const IID &")] Guid* riid, void** ppParent)
    {
        return ((delegate* unmanaged[Stdcall]<IDXGIDevice2*, Guid*, void**, int>)(lpVtbl[6]))((IDXGIDevice2*)Unsafe.AsPointer(ref this), riid, ppParent);
    }

    [return: NativeTypeName("HRESULT")]
    public int GetAdapter(IDXGIAdapter** pAdapter)
    {
        return ((delegate* unmanaged[Stdcall]<IDXGIDevice2*, IDXGIAdapter**, int>)(lpVtbl[7]))((IDXGIDevice2*)Unsafe.AsPointer(ref this), pAdapter);
    }

    [return: NativeTypeName("HRESULT")]
    public int CreateSurface([NativeTypeName("const DXGI_SURFACE_DESC *")] DXGI_SURFACE_DESC* pDesc, uint NumSurfaces, [NativeTypeName("DXGI_USAGE")] uint Usage, [NativeTypeName("const DXGI_SHARED_RESOURCE *")] DXGI_SHARED_RESOURCE* pSharedResource, IDXGISurface** ppSurface)
    {
        return ((delegate* unmanaged[Stdcall]<IDXGIDevice2*, DXGI_SURFACE_DESC*, uint, uint, DXGI_SHARED_RESOURCE*, IDXGISurface**, int>)(lpVtbl[8]))((IDXGIDevice2*)Unsafe.AsPointer(ref this), pDesc, NumSurfaces, Usage, pSharedResource, ppSurface);
    }

    [return: NativeTypeName("HRESULT")]
    public int QueryResourceResidency([NativeTypeName("IUnknown *const *")] IUnknown** ppResources, DXGI_RESIDENCY* pResidencyStatus, uint NumResources)
    {
        return ((delegate* unmanaged[Stdcall]<IDXGIDevice2*, IUnknown**, DXGI_RESIDENCY*, uint, int>)(lpVtbl[9]))((IDXGIDevice2*)Unsafe.AsPointer(ref this), ppResources, pResidencyStatus, NumResources);
    }

    [return: NativeTypeName("HRESULT")]
    public int SetGPUThreadPriority(int Priority)
    {
        return ((delegate* unmanaged[Stdcall]<IDXGIDevice2*, int, int>)(lpVtbl[10]))((IDXGIDevice2*)Unsafe.AsPointer(ref this), Priority);
    }

    [return: NativeTypeName("HRESULT")]
    public int GetGPUThreadPriority(int* pPriority)
    {
        return ((delegate* unmanaged[Stdcall]<IDXGIDevice2*, int*, int>)(lpVtbl[11]))((IDXGIDevice2*)Unsafe.AsPointer(ref this), pPriority);
    }

    [return: NativeTypeName("HRESULT")]
    public int SetMaximumFrameLatency(uint MaxLatency)
    {
        return ((delegate* unmanaged[Stdcall]<IDXGIDevice2*, uint, int>)(lpVtbl[12]))((IDXGIDevice2*)Unsafe.AsPointer(ref this), MaxLatency);
    }

    [return: NativeTypeName("HRESULT")]
    public int GetMaximumFrameLatency(uint* pMaxLatency)
    {
        return ((delegate* unmanaged[Stdcall]<IDXGIDevice2*, uint*, int>)(lpVtbl[13]))((IDXGIDevice2*)Unsafe.AsPointer(ref this), pMaxLatency);
    }

    [return: NativeTypeName("HRESULT")]
    public int OfferResources(uint NumResources, [NativeTypeName("IDXGIResource *const *")] IDXGIResource** ppResources, DXGI_OFFER_RESOURCE_PRIORITY Priority)
    {
        return ((delegate* unmanaged[Stdcall]<IDXGIDevice2*, uint, IDXGIResource**, DXGI_OFFER_RESOURCE_PRIORITY, int>)(lpVtbl[14]))((IDXGIDevice2*)Unsafe.AsPointer(ref this), NumResources, ppResources, Priority);
    }

    [return: NativeTypeName("HRESULT")]
    public int ReclaimResources(uint NumResources, [NativeTypeName("IDXGIResource *const *")] IDXGIResource** ppResources, [NativeTypeName("BOOL *")] int* pDiscarded)
    {
        return ((delegate* unmanaged[Stdcall]<IDXGIDevice2*, uint, IDXGIResource**, int*, int>)(lpVtbl[15]))((IDXGIDevice2*)Unsafe.AsPointer(ref this), NumResources, ppResources, pDiscarded);
    }

    [return: NativeTypeName("HRESULT")]
    public int EnqueueSetEvent([NativeTypeName("HANDLE")] void* hEvent)
    {
        return ((delegate* unmanaged[Stdcall]<IDXGIDevice2*, void*, int>)(lpVtbl[16]))((IDXGIDevice2*)Unsafe.AsPointer(ref this), hEvent);
    }

    public interface Interface : IDXGIDevice1.Interface
    {
        [return: NativeTypeName("HRESULT")]
        int OfferResources(uint NumResources, [NativeTypeName("IDXGIResource *const *")] IDXGIResource** ppResources, DXGI_OFFER_RESOURCE_PRIORITY Priority);

        [return: NativeTypeName("HRESULT")]
        int ReclaimResources(uint NumResources, [NativeTypeName("IDXGIResource *const *")] IDXGIResource** ppResources, [NativeTypeName("BOOL *")] int* pDiscarded);

        [return: NativeTypeName("HRESULT")]
        int EnqueueSetEvent([NativeTypeName("HANDLE")] void* hEvent);
    }
}

internal partial struct DXGI_MODE_DESC1
{
    public uint Width;

    public uint Height;

    public DXGI_RATIONAL RefreshRate;

    public DXGI_FORMAT Format;

    public DXGI_MODE_SCANLINE_ORDER ScanlineOrdering;

    public DXGI_MODE_SCALING Scaling;

    [NativeTypeName("BOOL")]
    public int Stereo;
}

internal enum DXGI_SCALING
{
    DXGI_SCALING_STRETCH = 0,
    DXGI_SCALING_NONE = 1,
    DXGI_SCALING_ASPECT_RATIO_STRETCH = 2,
}

internal partial struct DXGI_SWAP_CHAIN_DESC1
{
    public uint Width;

    public uint Height;

    public DXGI_FORMAT Format;

    [NativeTypeName("BOOL")]
    public int Stereo;

    public DXGI_SAMPLE_DESC SampleDesc;

    [NativeTypeName("DXGI_USAGE")]
    public uint BufferUsage;

    public uint BufferCount;

    public DXGI_SCALING Scaling;

    public DXGI_SWAP_EFFECT SwapEffect;

    public DXGI_ALPHA_MODE AlphaMode;

    public uint Flags;
}

internal partial struct DXGI_SWAP_CHAIN_FULLSCREEN_DESC
{
    public DXGI_RATIONAL RefreshRate;

    public DXGI_MODE_SCANLINE_ORDER ScanlineOrdering;

    public DXGI_MODE_SCALING Scaling;

    [NativeTypeName("BOOL")]
    public int Windowed;
}

internal unsafe partial struct DXGI_PRESENT_PARAMETERS
{
    public uint DirtyRectsCount;

    public RECT* pDirtyRects;

    public RECT* pScrollRect;

    public POINT* pScrollOffset;
}

[Guid("790A45F7-0D42-4876-983A-0A55CFE6F4AA")]
[NativeTypeName("struct IDXGISwapChain1 : IDXGISwapChain")]
internal unsafe partial struct IDXGISwapChain1 : IDXGISwapChain1.Interface, INativeGuid
{
    static Guid* INativeGuid.NativeGuid => (Guid*)Unsafe.AsPointer(in IID_IDXGISwapChain1);

    public void** lpVtbl;

    [return: NativeTypeName("HRESULT")]
    public int QueryInterface([NativeTypeName("const IID &")] Guid* riid, void** ppvObject)
    {
        return ((delegate* unmanaged[Stdcall]<IDXGISwapChain1*, Guid*, void**, int>)(lpVtbl[0]))((IDXGISwapChain1*)Unsafe.AsPointer(ref this), riid, ppvObject);
    }

    [return: NativeTypeName("ULONG")]
    public uint AddRef()
    {
        return ((delegate* unmanaged[Stdcall]<IDXGISwapChain1*, uint>)(lpVtbl[1]))((IDXGISwapChain1*)Unsafe.AsPointer(ref this));
    }

    [return: NativeTypeName("ULONG")]
    public uint Release()
    {
        return ((delegate* unmanaged[Stdcall]<IDXGISwapChain1*, uint>)(lpVtbl[2]))((IDXGISwapChain1*)Unsafe.AsPointer(ref this));
    }

    [return: NativeTypeName("HRESULT")]
    public int SetPrivateData([NativeTypeName("const GUID &")] Guid* Name, uint DataSize, [NativeTypeName("const void *")] void* pData)
    {
        return ((delegate* unmanaged[Stdcall]<IDXGISwapChain1*, Guid*, uint, void*, int>)(lpVtbl[3]))((IDXGISwapChain1*)Unsafe.AsPointer(ref this), Name, DataSize, pData);
    }

    [return: NativeTypeName("HRESULT")]
    public int SetPrivateDataInterface([NativeTypeName("const GUID &")] Guid* Name, [NativeTypeName("const IUnknown *")] IUnknown* pUnknown)
    {
        return ((delegate* unmanaged[Stdcall]<IDXGISwapChain1*, Guid*, IUnknown*, int>)(lpVtbl[4]))((IDXGISwapChain1*)Unsafe.AsPointer(ref this), Name, pUnknown);
    }

    [return: NativeTypeName("HRESULT")]
    public int GetPrivateData([NativeTypeName("const GUID &")] Guid* Name, uint* pDataSize, void* pData)
    {
        return ((delegate* unmanaged[Stdcall]<IDXGISwapChain1*, Guid*, uint*, void*, int>)(lpVtbl[5]))((IDXGISwapChain1*)Unsafe.AsPointer(ref this), Name, pDataSize, pData);
    }

    [return: NativeTypeName("HRESULT")]
    public int GetParent([NativeTypeName("const IID &")] Guid* riid, void** ppParent)
    {
        return ((delegate* unmanaged[Stdcall]<IDXGISwapChain1*, Guid*, void**, int>)(lpVtbl[6]))((IDXGISwapChain1*)Unsafe.AsPointer(ref this), riid, ppParent);
    }

    [return: NativeTypeName("HRESULT")]
    public int GetDevice([NativeTypeName("const IID &")] Guid* riid, void** ppDevice)
    {
        return ((delegate* unmanaged[Stdcall]<IDXGISwapChain1*, Guid*, void**, int>)(lpVtbl[7]))((IDXGISwapChain1*)Unsafe.AsPointer(ref this), riid, ppDevice);
    }

    [return: NativeTypeName("HRESULT")]
    public int Present(uint SyncInterval, uint Flags)
    {
        return ((delegate* unmanaged[Stdcall]<IDXGISwapChain1*, uint, uint, int>)(lpVtbl[8]))((IDXGISwapChain1*)Unsafe.AsPointer(ref this), SyncInterval, Flags);
    }

    [return: NativeTypeName("HRESULT")]
    public int GetBuffer(uint Buffer, [NativeTypeName("const IID &")] Guid* riid, void** ppSurface)
    {
        return ((delegate* unmanaged[Stdcall]<IDXGISwapChain1*, uint, Guid*, void**, int>)(lpVtbl[9]))((IDXGISwapChain1*)Unsafe.AsPointer(ref this), Buffer, riid, ppSurface);
    }

    [return: NativeTypeName("HRESULT")]
    public int SetFullscreenState([NativeTypeName("BOOL")] int Fullscreen, IDXGIOutput* pTarget)
    {
        return ((delegate* unmanaged[Stdcall]<IDXGISwapChain1*, int, IDXGIOutput*, int>)(lpVtbl[10]))((IDXGISwapChain1*)Unsafe.AsPointer(ref this), Fullscreen, pTarget);
    }

    [return: NativeTypeName("HRESULT")]
    public int GetFullscreenState([NativeTypeName("BOOL *")] int* pFullscreen, IDXGIOutput** ppTarget)
    {
        return ((delegate* unmanaged[Stdcall]<IDXGISwapChain1*, int*, IDXGIOutput**, int>)(lpVtbl[11]))((IDXGISwapChain1*)Unsafe.AsPointer(ref this), pFullscreen, ppTarget);
    }

    [return: NativeTypeName("HRESULT")]
    public int GetDesc(DXGI_SWAP_CHAIN_DESC* pDesc)
    {
        return ((delegate* unmanaged[Stdcall]<IDXGISwapChain1*, DXGI_SWAP_CHAIN_DESC*, int>)(lpVtbl[12]))((IDXGISwapChain1*)Unsafe.AsPointer(ref this), pDesc);
    }

    [return: NativeTypeName("HRESULT")]
    public int ResizeBuffers(uint BufferCount, uint Width, uint Height, DXGI_FORMAT NewFormat, uint SwapChainFlags)
    {
        return ((delegate* unmanaged[Stdcall]<IDXGISwapChain1*, uint, uint, uint, DXGI_FORMAT, uint, int>)(lpVtbl[13]))((IDXGISwapChain1*)Unsafe.AsPointer(ref this), BufferCount, Width, Height, NewFormat, SwapChainFlags);
    }

    [return: NativeTypeName("HRESULT")]
    public int ResizeTarget([NativeTypeName("const DXGI_MODE_DESC *")] DXGI_MODE_DESC* pNewTargetParameters)
    {
        return ((delegate* unmanaged[Stdcall]<IDXGISwapChain1*, DXGI_MODE_DESC*, int>)(lpVtbl[14]))((IDXGISwapChain1*)Unsafe.AsPointer(ref this), pNewTargetParameters);
    }

    [return: NativeTypeName("HRESULT")]
    public int GetContainingOutput(IDXGIOutput** ppOutput)
    {
        return ((delegate* unmanaged[Stdcall]<IDXGISwapChain1*, IDXGIOutput**, int>)(lpVtbl[15]))((IDXGISwapChain1*)Unsafe.AsPointer(ref this), ppOutput);
    }

    [return: NativeTypeName("HRESULT")]
    public int GetFrameStatistics(DXGI_FRAME_STATISTICS* pStats)
    {
        return ((delegate* unmanaged[Stdcall]<IDXGISwapChain1*, DXGI_FRAME_STATISTICS*, int>)(lpVtbl[16]))((IDXGISwapChain1*)Unsafe.AsPointer(ref this), pStats);
    }

    [return: NativeTypeName("HRESULT")]
    public int GetLastPresentCount(uint* pLastPresentCount)
    {
        return ((delegate* unmanaged[Stdcall]<IDXGISwapChain1*, uint*, int>)(lpVtbl[17]))((IDXGISwapChain1*)Unsafe.AsPointer(ref this), pLastPresentCount);
    }

    [return: NativeTypeName("HRESULT")]
    public int GetDesc1(DXGI_SWAP_CHAIN_DESC1* pDesc)
    {
        return ((delegate* unmanaged[Stdcall]<IDXGISwapChain1*, DXGI_SWAP_CHAIN_DESC1*, int>)(lpVtbl[18]))((IDXGISwapChain1*)Unsafe.AsPointer(ref this), pDesc);
    }

    [return: NativeTypeName("HRESULT")]
    public int GetFullscreenDesc(DXGI_SWAP_CHAIN_FULLSCREEN_DESC* pDesc)
    {
        return ((delegate* unmanaged[Stdcall]<IDXGISwapChain1*, DXGI_SWAP_CHAIN_FULLSCREEN_DESC*, int>)(lpVtbl[19]))((IDXGISwapChain1*)Unsafe.AsPointer(ref this), pDesc);
    }

    [return: NativeTypeName("HRESULT")]
    public int GetHwnd([NativeTypeName("HWND *")] HWND__** pHwnd)
    {
        return ((delegate* unmanaged[Stdcall]<IDXGISwapChain1*, HWND__**, int>)(lpVtbl[20]))((IDXGISwapChain1*)Unsafe.AsPointer(ref this), pHwnd);
    }

    [return: NativeTypeName("HRESULT")]
    public int GetCoreWindow([NativeTypeName("const IID &")] Guid* refiid, void** ppUnk)
    {
        return ((delegate* unmanaged[Stdcall]<IDXGISwapChain1*, Guid*, void**, int>)(lpVtbl[21]))((IDXGISwapChain1*)Unsafe.AsPointer(ref this), refiid, ppUnk);
    }

    [return: NativeTypeName("HRESULT")]
    public int Present1(uint SyncInterval, uint PresentFlags, [NativeTypeName("const DXGI_PRESENT_PARAMETERS *")] DXGI_PRESENT_PARAMETERS* pPresentParameters)
    {
        return ((delegate* unmanaged[Stdcall]<IDXGISwapChain1*, uint, uint, DXGI_PRESENT_PARAMETERS*, int>)(lpVtbl[22]))((IDXGISwapChain1*)Unsafe.AsPointer(ref this), SyncInterval, PresentFlags, pPresentParameters);
    }

    [return: NativeTypeName("BOOL")]
    public int IsTemporaryMonoSupported()
    {
        return ((delegate* unmanaged[Stdcall]<IDXGISwapChain1*, int>)(lpVtbl[23]))((IDXGISwapChain1*)Unsafe.AsPointer(ref this));
    }

    [return: NativeTypeName("HRESULT")]
    public int GetRestrictToOutput(IDXGIOutput** ppRestrictToOutput)
    {
        return ((delegate* unmanaged[Stdcall]<IDXGISwapChain1*, IDXGIOutput**, int>)(lpVtbl[24]))((IDXGISwapChain1*)Unsafe.AsPointer(ref this), ppRestrictToOutput);
    }

    [return: NativeTypeName("HRESULT")]
    public int SetBackgroundColor([NativeTypeName("const DXGI_RGBA *")] D3DCOLORVALUE* pColor)
    {
        return ((delegate* unmanaged[Stdcall]<IDXGISwapChain1*, D3DCOLORVALUE*, int>)(lpVtbl[25]))((IDXGISwapChain1*)Unsafe.AsPointer(ref this), pColor);
    }

    [return: NativeTypeName("HRESULT")]
    public int GetBackgroundColor([NativeTypeName("DXGI_RGBA *")] D3DCOLORVALUE* pColor)
    {
        return ((delegate* unmanaged[Stdcall]<IDXGISwapChain1*, D3DCOLORVALUE*, int>)(lpVtbl[26]))((IDXGISwapChain1*)Unsafe.AsPointer(ref this), pColor);
    }

    [return: NativeTypeName("HRESULT")]
    public int SetRotation(DXGI_MODE_ROTATION Rotation)
    {
        return ((delegate* unmanaged[Stdcall]<IDXGISwapChain1*, DXGI_MODE_ROTATION, int>)(lpVtbl[27]))((IDXGISwapChain1*)Unsafe.AsPointer(ref this), Rotation);
    }

    [return: NativeTypeName("HRESULT")]
    public int GetRotation(DXGI_MODE_ROTATION* pRotation)
    {
        return ((delegate* unmanaged[Stdcall]<IDXGISwapChain1*, DXGI_MODE_ROTATION*, int>)(lpVtbl[28]))((IDXGISwapChain1*)Unsafe.AsPointer(ref this), pRotation);
    }

    public interface Interface : IDXGISwapChain.Interface
    {
        [return: NativeTypeName("HRESULT")]
        int GetDesc1(DXGI_SWAP_CHAIN_DESC1* pDesc);

        [return: NativeTypeName("HRESULT")]
        int GetFullscreenDesc(DXGI_SWAP_CHAIN_FULLSCREEN_DESC* pDesc);

        [return: NativeTypeName("HRESULT")]
        int GetHwnd([NativeTypeName("HWND *")] HWND__** pHwnd);

        [return: NativeTypeName("HRESULT")]
        int GetCoreWindow([NativeTypeName("const IID &")] Guid* refiid, void** ppUnk);

        [return: NativeTypeName("HRESULT")]
        int Present1(uint SyncInterval, uint PresentFlags, [NativeTypeName("const DXGI_PRESENT_PARAMETERS *")] DXGI_PRESENT_PARAMETERS* pPresentParameters);

        [return: NativeTypeName("BOOL")]
        int IsTemporaryMonoSupported();

        [return: NativeTypeName("HRESULT")]
        int GetRestrictToOutput(IDXGIOutput** ppRestrictToOutput);

        [return: NativeTypeName("HRESULT")]
        int SetBackgroundColor([NativeTypeName("const DXGI_RGBA *")] D3DCOLORVALUE* pColor);

        [return: NativeTypeName("HRESULT")]
        int GetBackgroundColor([NativeTypeName("DXGI_RGBA *")] D3DCOLORVALUE* pColor);

        [return: NativeTypeName("HRESULT")]
        int SetRotation(DXGI_MODE_ROTATION Rotation);

        [return: NativeTypeName("HRESULT")]
        int GetRotation(DXGI_MODE_ROTATION* pRotation);
    }
}

[Guid("50C83A1C-E072-4C48-87B0-3630FA36A6D0")]
[NativeTypeName("struct IDXGIFactory2 : IDXGIFactory1")]
internal unsafe partial struct IDXGIFactory2 : IDXGIFactory2.Interface, INativeGuid
{
    static Guid* INativeGuid.NativeGuid => (Guid*)Unsafe.AsPointer(in IID_IDXGIFactory2);

    public void** lpVtbl;

    [return: NativeTypeName("HRESULT")]
    public int QueryInterface([NativeTypeName("const IID &")] Guid* riid, void** ppvObject)
    {
        return ((delegate* unmanaged[Stdcall]<IDXGIFactory2*, Guid*, void**, int>)(lpVtbl[0]))((IDXGIFactory2*)Unsafe.AsPointer(ref this), riid, ppvObject);
    }

    [return: NativeTypeName("ULONG")]
    public uint AddRef()
    {
        return ((delegate* unmanaged[Stdcall]<IDXGIFactory2*, uint>)(lpVtbl[1]))((IDXGIFactory2*)Unsafe.AsPointer(ref this));
    }

    [return: NativeTypeName("ULONG")]
    public uint Release()
    {
        return ((delegate* unmanaged[Stdcall]<IDXGIFactory2*, uint>)(lpVtbl[2]))((IDXGIFactory2*)Unsafe.AsPointer(ref this));
    }

    [return: NativeTypeName("HRESULT")]
    public int SetPrivateData([NativeTypeName("const GUID &")] Guid* Name, uint DataSize, [NativeTypeName("const void *")] void* pData)
    {
        return ((delegate* unmanaged[Stdcall]<IDXGIFactory2*, Guid*, uint, void*, int>)(lpVtbl[3]))((IDXGIFactory2*)Unsafe.AsPointer(ref this), Name, DataSize, pData);
    }

    [return: NativeTypeName("HRESULT")]
    public int SetPrivateDataInterface([NativeTypeName("const GUID &")] Guid* Name, [NativeTypeName("const IUnknown *")] IUnknown* pUnknown)
    {
        return ((delegate* unmanaged[Stdcall]<IDXGIFactory2*, Guid*, IUnknown*, int>)(lpVtbl[4]))((IDXGIFactory2*)Unsafe.AsPointer(ref this), Name, pUnknown);
    }

    [return: NativeTypeName("HRESULT")]
    public int GetPrivateData([NativeTypeName("const GUID &")] Guid* Name, uint* pDataSize, void* pData)
    {
        return ((delegate* unmanaged[Stdcall]<IDXGIFactory2*, Guid*, uint*, void*, int>)(lpVtbl[5]))((IDXGIFactory2*)Unsafe.AsPointer(ref this), Name, pDataSize, pData);
    }

    [return: NativeTypeName("HRESULT")]
    public int GetParent([NativeTypeName("const IID &")] Guid* riid, void** ppParent)
    {
        return ((delegate* unmanaged[Stdcall]<IDXGIFactory2*, Guid*, void**, int>)(lpVtbl[6]))((IDXGIFactory2*)Unsafe.AsPointer(ref this), riid, ppParent);
    }

    [return: NativeTypeName("HRESULT")]
    public int EnumAdapters(uint Adapter, IDXGIAdapter** ppAdapter)
    {
        return ((delegate* unmanaged[Stdcall]<IDXGIFactory2*, uint, IDXGIAdapter**, int>)(lpVtbl[7]))((IDXGIFactory2*)Unsafe.AsPointer(ref this), Adapter, ppAdapter);
    }

    [return: NativeTypeName("HRESULT")]
    public int MakeWindowAssociation([NativeTypeName("HWND")] HWND__* WindowHandle, uint Flags)
    {
        return ((delegate* unmanaged[Stdcall]<IDXGIFactory2*, HWND__*, uint, int>)(lpVtbl[8]))((IDXGIFactory2*)Unsafe.AsPointer(ref this), WindowHandle, Flags);
    }

    [return: NativeTypeName("HRESULT")]
    public int GetWindowAssociation([NativeTypeName("HWND *")] HWND__** pWindowHandle)
    {
        return ((delegate* unmanaged[Stdcall]<IDXGIFactory2*, HWND__**, int>)(lpVtbl[9]))((IDXGIFactory2*)Unsafe.AsPointer(ref this), pWindowHandle);
    }

    [return: NativeTypeName("HRESULT")]
    public int CreateSwapChain(IUnknown* pDevice, DXGI_SWAP_CHAIN_DESC* pDesc, IDXGISwapChain** ppSwapChain)
    {
        return ((delegate* unmanaged[Stdcall]<IDXGIFactory2*, IUnknown*, DXGI_SWAP_CHAIN_DESC*, IDXGISwapChain**, int>)(lpVtbl[10]))((IDXGIFactory2*)Unsafe.AsPointer(ref this), pDevice, pDesc, ppSwapChain);
    }

    [return: NativeTypeName("HRESULT")]
    public int CreateSoftwareAdapter([NativeTypeName("HMODULE")] HINSTANCE__* Module, IDXGIAdapter** ppAdapter)
    {
        return ((delegate* unmanaged[Stdcall]<IDXGIFactory2*, HINSTANCE__*, IDXGIAdapter**, int>)(lpVtbl[11]))((IDXGIFactory2*)Unsafe.AsPointer(ref this), Module, ppAdapter);
    }

    [return: NativeTypeName("HRESULT")]
    public int EnumAdapters1(uint Adapter, IDXGIAdapter1** ppAdapter)
    {
        return ((delegate* unmanaged[Stdcall]<IDXGIFactory2*, uint, IDXGIAdapter1**, int>)(lpVtbl[12]))((IDXGIFactory2*)Unsafe.AsPointer(ref this), Adapter, ppAdapter);
    }

    [return: NativeTypeName("BOOL")]
    public int IsCurrent()
    {
        return ((delegate* unmanaged[Stdcall]<IDXGIFactory2*, int>)(lpVtbl[13]))((IDXGIFactory2*)Unsafe.AsPointer(ref this));
    }

    [return: NativeTypeName("BOOL")]
    public int IsWindowedStereoEnabled()
    {
        return ((delegate* unmanaged[Stdcall]<IDXGIFactory2*, int>)(lpVtbl[14]))((IDXGIFactory2*)Unsafe.AsPointer(ref this));
    }

    [return: NativeTypeName("HRESULT")]
    public int CreateSwapChainForHwnd(IUnknown* pDevice, [NativeTypeName("HWND")] HWND__* hWnd, [NativeTypeName("const DXGI_SWAP_CHAIN_DESC1 *")] DXGI_SWAP_CHAIN_DESC1* pDesc, [NativeTypeName("const DXGI_SWAP_CHAIN_FULLSCREEN_DESC *")] DXGI_SWAP_CHAIN_FULLSCREEN_DESC* pFullscreenDesc, IDXGIOutput* pRestrictToOutput, IDXGISwapChain1** ppSwapChain)
    {
        return ((delegate* unmanaged[Stdcall]<IDXGIFactory2*, IUnknown*, HWND__*, DXGI_SWAP_CHAIN_DESC1*, DXGI_SWAP_CHAIN_FULLSCREEN_DESC*, IDXGIOutput*, IDXGISwapChain1**, int>)(lpVtbl[15]))((IDXGIFactory2*)Unsafe.AsPointer(ref this), pDevice, hWnd, pDesc, pFullscreenDesc, pRestrictToOutput, ppSwapChain);
    }

    [return: NativeTypeName("HRESULT")]
    public int CreateSwapChainForCoreWindow(IUnknown* pDevice, IUnknown* pWindow, [NativeTypeName("const DXGI_SWAP_CHAIN_DESC1 *")] DXGI_SWAP_CHAIN_DESC1* pDesc, IDXGIOutput* pRestrictToOutput, IDXGISwapChain1** ppSwapChain)
    {
        return ((delegate* unmanaged[Stdcall]<IDXGIFactory2*, IUnknown*, IUnknown*, DXGI_SWAP_CHAIN_DESC1*, IDXGIOutput*, IDXGISwapChain1**, int>)(lpVtbl[16]))((IDXGIFactory2*)Unsafe.AsPointer(ref this), pDevice, pWindow, pDesc, pRestrictToOutput, ppSwapChain);
    }

    [return: NativeTypeName("HRESULT")]
    public int GetSharedResourceAdapterLuid([NativeTypeName("HANDLE")] void* hResource, LUID* pLuid)
    {
        return ((delegate* unmanaged[Stdcall]<IDXGIFactory2*, void*, LUID*, int>)(lpVtbl[17]))((IDXGIFactory2*)Unsafe.AsPointer(ref this), hResource, pLuid);
    }

    [return: NativeTypeName("HRESULT")]
    public int RegisterStereoStatusWindow([NativeTypeName("HWND")] HWND__* WindowHandle, uint wMsg, [NativeTypeName("DWORD *")] uint* pdwCookie)
    {
        return ((delegate* unmanaged[Stdcall]<IDXGIFactory2*, HWND__*, uint, uint*, int>)(lpVtbl[18]))((IDXGIFactory2*)Unsafe.AsPointer(ref this), WindowHandle, wMsg, pdwCookie);
    }

    [return: NativeTypeName("HRESULT")]
    public int RegisterStereoStatusEvent([NativeTypeName("HANDLE")] void* hEvent, [NativeTypeName("DWORD *")] uint* pdwCookie)
    {
        return ((delegate* unmanaged[Stdcall]<IDXGIFactory2*, void*, uint*, int>)(lpVtbl[19]))((IDXGIFactory2*)Unsafe.AsPointer(ref this), hEvent, pdwCookie);
    }

    public void UnregisterStereoStatus([NativeTypeName("DWORD")] uint dwCookie)
    {
        ((delegate* unmanaged[Stdcall]<IDXGIFactory2*, uint, void>)(lpVtbl[20]))((IDXGIFactory2*)Unsafe.AsPointer(ref this), dwCookie);
    }

    [return: NativeTypeName("HRESULT")]
    public int RegisterOcclusionStatusWindow([NativeTypeName("HWND")] HWND__* WindowHandle, uint wMsg, [NativeTypeName("DWORD *")] uint* pdwCookie)
    {
        return ((delegate* unmanaged[Stdcall]<IDXGIFactory2*, HWND__*, uint, uint*, int>)(lpVtbl[21]))((IDXGIFactory2*)Unsafe.AsPointer(ref this), WindowHandle, wMsg, pdwCookie);
    }

    [return: NativeTypeName("HRESULT")]
    public int RegisterOcclusionStatusEvent([NativeTypeName("HANDLE")] void* hEvent, [NativeTypeName("DWORD *")] uint* pdwCookie)
    {
        return ((delegate* unmanaged[Stdcall]<IDXGIFactory2*, void*, uint*, int>)(lpVtbl[22]))((IDXGIFactory2*)Unsafe.AsPointer(ref this), hEvent, pdwCookie);
    }

    public void UnregisterOcclusionStatus([NativeTypeName("DWORD")] uint dwCookie)
    {
        ((delegate* unmanaged[Stdcall]<IDXGIFactory2*, uint, void>)(lpVtbl[23]))((IDXGIFactory2*)Unsafe.AsPointer(ref this), dwCookie);
    }

    [return: NativeTypeName("HRESULT")]
    public int CreateSwapChainForComposition(IUnknown* pDevice, [NativeTypeName("const DXGI_SWAP_CHAIN_DESC1 *")] DXGI_SWAP_CHAIN_DESC1* pDesc, IDXGIOutput* pRestrictToOutput, IDXGISwapChain1** ppSwapChain)
    {
        return ((delegate* unmanaged[Stdcall]<IDXGIFactory2*, IUnknown*, DXGI_SWAP_CHAIN_DESC1*, IDXGIOutput*, IDXGISwapChain1**, int>)(lpVtbl[24]))((IDXGIFactory2*)Unsafe.AsPointer(ref this), pDevice, pDesc, pRestrictToOutput, ppSwapChain);
    }

    public interface Interface : IDXGIFactory1.Interface
    {
        [return: NativeTypeName("BOOL")]
        int IsWindowedStereoEnabled();

        [return: NativeTypeName("HRESULT")]
        int CreateSwapChainForHwnd(IUnknown* pDevice, [NativeTypeName("HWND")] HWND__* hWnd, [NativeTypeName("const DXGI_SWAP_CHAIN_DESC1 *")] DXGI_SWAP_CHAIN_DESC1* pDesc, [NativeTypeName("const DXGI_SWAP_CHAIN_FULLSCREEN_DESC *")] DXGI_SWAP_CHAIN_FULLSCREEN_DESC* pFullscreenDesc, IDXGIOutput* pRestrictToOutput, IDXGISwapChain1** ppSwapChain);

        [return: NativeTypeName("HRESULT")]
        int CreateSwapChainForCoreWindow(IUnknown* pDevice, IUnknown* pWindow, [NativeTypeName("const DXGI_SWAP_CHAIN_DESC1 *")] DXGI_SWAP_CHAIN_DESC1* pDesc, IDXGIOutput* pRestrictToOutput, IDXGISwapChain1** ppSwapChain);

        [return: NativeTypeName("HRESULT")]
        int GetSharedResourceAdapterLuid([NativeTypeName("HANDLE")] void* hResource, LUID* pLuid);

        [return: NativeTypeName("HRESULT")]
        int RegisterStereoStatusWindow([NativeTypeName("HWND")] HWND__* WindowHandle, uint wMsg, [NativeTypeName("DWORD *")] uint* pdwCookie);

        [return: NativeTypeName("HRESULT")]
        int RegisterStereoStatusEvent([NativeTypeName("HANDLE")] void* hEvent, [NativeTypeName("DWORD *")] uint* pdwCookie);

        void UnregisterStereoStatus([NativeTypeName("DWORD")] uint dwCookie);

        [return: NativeTypeName("HRESULT")]
        int RegisterOcclusionStatusWindow([NativeTypeName("HWND")] HWND__* WindowHandle, uint wMsg, [NativeTypeName("DWORD *")] uint* pdwCookie);

        [return: NativeTypeName("HRESULT")]
        int RegisterOcclusionStatusEvent([NativeTypeName("HANDLE")] void* hEvent, [NativeTypeName("DWORD *")] uint* pdwCookie);

        void UnregisterOcclusionStatus([NativeTypeName("DWORD")] uint dwCookie);

        [return: NativeTypeName("HRESULT")]
        int CreateSwapChainForComposition(IUnknown* pDevice, [NativeTypeName("const DXGI_SWAP_CHAIN_DESC1 *")] DXGI_SWAP_CHAIN_DESC1* pDesc, IDXGIOutput* pRestrictToOutput, IDXGISwapChain1** ppSwapChain);
    }
}

internal enum DXGI_GRAPHICS_PREEMPTION_GRANULARITY
{
    DXGI_GRAPHICS_PREEMPTION_DMA_BUFFER_BOUNDARY = 0,
    DXGI_GRAPHICS_PREEMPTION_PRIMITIVE_BOUNDARY = 1,
    DXGI_GRAPHICS_PREEMPTION_TRIANGLE_BOUNDARY = 2,
    DXGI_GRAPHICS_PREEMPTION_PIXEL_BOUNDARY = 3,
    DXGI_GRAPHICS_PREEMPTION_INSTRUCTION_BOUNDARY = 4,
}

internal enum DXGI_COMPUTE_PREEMPTION_GRANULARITY
{
    DXGI_COMPUTE_PREEMPTION_DMA_BUFFER_BOUNDARY = 0,
    DXGI_COMPUTE_PREEMPTION_DISPATCH_BOUNDARY = 1,
    DXGI_COMPUTE_PREEMPTION_THREAD_GROUP_BOUNDARY = 2,
    DXGI_COMPUTE_PREEMPTION_THREAD_BOUNDARY = 3,
    DXGI_COMPUTE_PREEMPTION_INSTRUCTION_BOUNDARY = 4,
}

internal partial struct DXGI_ADAPTER_DESC2
{
    [NativeTypeName("WCHAR[128]")]
    public _Description_e__FixedBuffer Description;

    public uint VendorId;

    public uint DeviceId;

    public uint SubSysId;

    public uint Revision;

    [NativeTypeName("SIZE_T")]
    public nuint DedicatedVideoMemory;

    [NativeTypeName("SIZE_T")]
    public nuint DedicatedSystemMemory;

    [NativeTypeName("SIZE_T")]
    public nuint SharedSystemMemory;

    public LUID AdapterLuid;

    public uint Flags;

    public DXGI_GRAPHICS_PREEMPTION_GRANULARITY GraphicsPreemptionGranularity;

    public DXGI_COMPUTE_PREEMPTION_GRANULARITY ComputePreemptionGranularity;

    [InlineArray(128)]
    public partial struct _Description_e__FixedBuffer
    {
        public ushort e0;
    }
}

[Guid("0AA1AE0A-FA0E-4B84-8644-E05FF8E5ACB5")]
[NativeTypeName("struct IDXGIAdapter2 : IDXGIAdapter1")]
internal unsafe partial struct IDXGIAdapter2 : IDXGIAdapter2.Interface, INativeGuid
{
    static Guid* INativeGuid.NativeGuid => (Guid*)Unsafe.AsPointer(in IID_IDXGIAdapter2);

    public void** lpVtbl;

    [return: NativeTypeName("HRESULT")]
    public int QueryInterface([NativeTypeName("const IID &")] Guid* riid, void** ppvObject)
    {
        return ((delegate* unmanaged[Stdcall]<IDXGIAdapter2*, Guid*, void**, int>)(lpVtbl[0]))((IDXGIAdapter2*)Unsafe.AsPointer(ref this), riid, ppvObject);
    }

    [return: NativeTypeName("ULONG")]
    public uint AddRef()
    {
        return ((delegate* unmanaged[Stdcall]<IDXGIAdapter2*, uint>)(lpVtbl[1]))((IDXGIAdapter2*)Unsafe.AsPointer(ref this));
    }

    [return: NativeTypeName("ULONG")]
    public uint Release()
    {
        return ((delegate* unmanaged[Stdcall]<IDXGIAdapter2*, uint>)(lpVtbl[2]))((IDXGIAdapter2*)Unsafe.AsPointer(ref this));
    }

    [return: NativeTypeName("HRESULT")]
    public int SetPrivateData([NativeTypeName("const GUID &")] Guid* Name, uint DataSize, [NativeTypeName("const void *")] void* pData)
    {
        return ((delegate* unmanaged[Stdcall]<IDXGIAdapter2*, Guid*, uint, void*, int>)(lpVtbl[3]))((IDXGIAdapter2*)Unsafe.AsPointer(ref this), Name, DataSize, pData);
    }

    [return: NativeTypeName("HRESULT")]
    public int SetPrivateDataInterface([NativeTypeName("const GUID &")] Guid* Name, [NativeTypeName("const IUnknown *")] IUnknown* pUnknown)
    {
        return ((delegate* unmanaged[Stdcall]<IDXGIAdapter2*, Guid*, IUnknown*, int>)(lpVtbl[4]))((IDXGIAdapter2*)Unsafe.AsPointer(ref this), Name, pUnknown);
    }

    [return: NativeTypeName("HRESULT")]
    public int GetPrivateData([NativeTypeName("const GUID &")] Guid* Name, uint* pDataSize, void* pData)
    {
        return ((delegate* unmanaged[Stdcall]<IDXGIAdapter2*, Guid*, uint*, void*, int>)(lpVtbl[5]))((IDXGIAdapter2*)Unsafe.AsPointer(ref this), Name, pDataSize, pData);
    }

    [return: NativeTypeName("HRESULT")]
    public int GetParent([NativeTypeName("const IID &")] Guid* riid, void** ppParent)
    {
        return ((delegate* unmanaged[Stdcall]<IDXGIAdapter2*, Guid*, void**, int>)(lpVtbl[6]))((IDXGIAdapter2*)Unsafe.AsPointer(ref this), riid, ppParent);
    }

    [return: NativeTypeName("HRESULT")]
    public int EnumOutputs(uint Output, IDXGIOutput** ppOutput)
    {
        return ((delegate* unmanaged[Stdcall]<IDXGIAdapter2*, uint, IDXGIOutput**, int>)(lpVtbl[7]))((IDXGIAdapter2*)Unsafe.AsPointer(ref this), Output, ppOutput);
    }

    [return: NativeTypeName("HRESULT")]
    public int GetDesc(DXGI_ADAPTER_DESC* pDesc)
    {
        return ((delegate* unmanaged[Stdcall]<IDXGIAdapter2*, DXGI_ADAPTER_DESC*, int>)(lpVtbl[8]))((IDXGIAdapter2*)Unsafe.AsPointer(ref this), pDesc);
    }

    [return: NativeTypeName("HRESULT")]
    public int CheckInterfaceSupport([NativeTypeName("const GUID &")] Guid* InterfaceName, LARGE_INTEGER* pUMDVersion)
    {
        return ((delegate* unmanaged[Stdcall]<IDXGIAdapter2*, Guid*, LARGE_INTEGER*, int>)(lpVtbl[9]))((IDXGIAdapter2*)Unsafe.AsPointer(ref this), InterfaceName, pUMDVersion);
    }

    [return: NativeTypeName("HRESULT")]
    public int GetDesc1(DXGI_ADAPTER_DESC1* pDesc)
    {
        return ((delegate* unmanaged[Stdcall]<IDXGIAdapter2*, DXGI_ADAPTER_DESC1*, int>)(lpVtbl[10]))((IDXGIAdapter2*)Unsafe.AsPointer(ref this), pDesc);
    }

    [return: NativeTypeName("HRESULT")]
    public int GetDesc2(DXGI_ADAPTER_DESC2* pDesc)
    {
        return ((delegate* unmanaged[Stdcall]<IDXGIAdapter2*, DXGI_ADAPTER_DESC2*, int>)(lpVtbl[11]))((IDXGIAdapter2*)Unsafe.AsPointer(ref this), pDesc);
    }

    public interface Interface : IDXGIAdapter1.Interface
    {
        [return: NativeTypeName("HRESULT")]
        int GetDesc2(DXGI_ADAPTER_DESC2* pDesc);
    }
}

[Guid("00CDDEA8-939B-4B83-A340-A685226666CC")]
[NativeTypeName("struct IDXGIOutput1 : IDXGIOutput")]
internal unsafe partial struct IDXGIOutput1 : IDXGIOutput1.Interface, INativeGuid
{
    static Guid* INativeGuid.NativeGuid => (Guid*)Unsafe.AsPointer(in IID_IDXGIOutput1);

    public void** lpVtbl;

    [return: NativeTypeName("HRESULT")]
    public int QueryInterface([NativeTypeName("const IID &")] Guid* riid, void** ppvObject)
    {
        return ((delegate* unmanaged[Stdcall]<IDXGIOutput1*, Guid*, void**, int>)(lpVtbl[0]))((IDXGIOutput1*)Unsafe.AsPointer(ref this), riid, ppvObject);
    }

    [return: NativeTypeName("ULONG")]
    public uint AddRef()
    {
        return ((delegate* unmanaged[Stdcall]<IDXGIOutput1*, uint>)(lpVtbl[1]))((IDXGIOutput1*)Unsafe.AsPointer(ref this));
    }

    [return: NativeTypeName("ULONG")]
    public uint Release()
    {
        return ((delegate* unmanaged[Stdcall]<IDXGIOutput1*, uint>)(lpVtbl[2]))((IDXGIOutput1*)Unsafe.AsPointer(ref this));
    }

    [return: NativeTypeName("HRESULT")]
    public int SetPrivateData([NativeTypeName("const GUID &")] Guid* Name, uint DataSize, [NativeTypeName("const void *")] void* pData)
    {
        return ((delegate* unmanaged[Stdcall]<IDXGIOutput1*, Guid*, uint, void*, int>)(lpVtbl[3]))((IDXGIOutput1*)Unsafe.AsPointer(ref this), Name, DataSize, pData);
    }

    [return: NativeTypeName("HRESULT")]
    public int SetPrivateDataInterface([NativeTypeName("const GUID &")] Guid* Name, [NativeTypeName("const IUnknown *")] IUnknown* pUnknown)
    {
        return ((delegate* unmanaged[Stdcall]<IDXGIOutput1*, Guid*, IUnknown*, int>)(lpVtbl[4]))((IDXGIOutput1*)Unsafe.AsPointer(ref this), Name, pUnknown);
    }

    [return: NativeTypeName("HRESULT")]
    public int GetPrivateData([NativeTypeName("const GUID &")] Guid* Name, uint* pDataSize, void* pData)
    {
        return ((delegate* unmanaged[Stdcall]<IDXGIOutput1*, Guid*, uint*, void*, int>)(lpVtbl[5]))((IDXGIOutput1*)Unsafe.AsPointer(ref this), Name, pDataSize, pData);
    }

    [return: NativeTypeName("HRESULT")]
    public int GetParent([NativeTypeName("const IID &")] Guid* riid, void** ppParent)
    {
        return ((delegate* unmanaged[Stdcall]<IDXGIOutput1*, Guid*, void**, int>)(lpVtbl[6]))((IDXGIOutput1*)Unsafe.AsPointer(ref this), riid, ppParent);
    }

    [return: NativeTypeName("HRESULT")]
    public int GetDesc(DXGI_OUTPUT_DESC* pDesc)
    {
        return ((delegate* unmanaged[Stdcall]<IDXGIOutput1*, DXGI_OUTPUT_DESC*, int>)(lpVtbl[7]))((IDXGIOutput1*)Unsafe.AsPointer(ref this), pDesc);
    }

    [return: NativeTypeName("HRESULT")]
    public int GetDisplayModeList(DXGI_FORMAT EnumFormat, uint Flags, uint* pNumModes, DXGI_MODE_DESC* pDesc)
    {
        return ((delegate* unmanaged[Stdcall]<IDXGIOutput1*, DXGI_FORMAT, uint, uint*, DXGI_MODE_DESC*, int>)(lpVtbl[8]))((IDXGIOutput1*)Unsafe.AsPointer(ref this), EnumFormat, Flags, pNumModes, pDesc);
    }

    [return: NativeTypeName("HRESULT")]
    public int FindClosestMatchingMode([NativeTypeName("const DXGI_MODE_DESC *")] DXGI_MODE_DESC* pModeToMatch, DXGI_MODE_DESC* pClosestMatch, IUnknown* pConcernedDevice)
    {
        return ((delegate* unmanaged[Stdcall]<IDXGIOutput1*, DXGI_MODE_DESC*, DXGI_MODE_DESC*, IUnknown*, int>)(lpVtbl[9]))((IDXGIOutput1*)Unsafe.AsPointer(ref this), pModeToMatch, pClosestMatch, pConcernedDevice);
    }

    [return: NativeTypeName("HRESULT")]
    public int WaitForVBlank()
    {
        return ((delegate* unmanaged[Stdcall]<IDXGIOutput1*, int>)(lpVtbl[10]))((IDXGIOutput1*)Unsafe.AsPointer(ref this));
    }

    [return: NativeTypeName("HRESULT")]
    public int TakeOwnership(IUnknown* pDevice, [NativeTypeName("BOOL")] int Exclusive)
    {
        return ((delegate* unmanaged[Stdcall]<IDXGIOutput1*, IUnknown*, int, int>)(lpVtbl[11]))((IDXGIOutput1*)Unsafe.AsPointer(ref this), pDevice, Exclusive);
    }

    public void ReleaseOwnership()
    {
        ((delegate* unmanaged[Stdcall]<IDXGIOutput1*, void>)(lpVtbl[12]))((IDXGIOutput1*)Unsafe.AsPointer(ref this));
    }

    [return: NativeTypeName("HRESULT")]
    public int GetGammaControlCapabilities(DXGI_GAMMA_CONTROL_CAPABILITIES* pGammaCaps)
    {
        return ((delegate* unmanaged[Stdcall]<IDXGIOutput1*, DXGI_GAMMA_CONTROL_CAPABILITIES*, int>)(lpVtbl[13]))((IDXGIOutput1*)Unsafe.AsPointer(ref this), pGammaCaps);
    }

    [return: NativeTypeName("HRESULT")]
    public int SetGammaControl([NativeTypeName("const DXGI_GAMMA_CONTROL *")] DXGI_GAMMA_CONTROL* pArray)
    {
        return ((delegate* unmanaged[Stdcall]<IDXGIOutput1*, DXGI_GAMMA_CONTROL*, int>)(lpVtbl[14]))((IDXGIOutput1*)Unsafe.AsPointer(ref this), pArray);
    }

    [return: NativeTypeName("HRESULT")]
    public int GetGammaControl(DXGI_GAMMA_CONTROL* pArray)
    {
        return ((delegate* unmanaged[Stdcall]<IDXGIOutput1*, DXGI_GAMMA_CONTROL*, int>)(lpVtbl[15]))((IDXGIOutput1*)Unsafe.AsPointer(ref this), pArray);
    }

    [return: NativeTypeName("HRESULT")]
    public int SetDisplaySurface(IDXGISurface* pScanoutSurface)
    {
        return ((delegate* unmanaged[Stdcall]<IDXGIOutput1*, IDXGISurface*, int>)(lpVtbl[16]))((IDXGIOutput1*)Unsafe.AsPointer(ref this), pScanoutSurface);
    }

    [return: NativeTypeName("HRESULT")]
    public int GetDisplaySurfaceData(IDXGISurface* pDestination)
    {
        return ((delegate* unmanaged[Stdcall]<IDXGIOutput1*, IDXGISurface*, int>)(lpVtbl[17]))((IDXGIOutput1*)Unsafe.AsPointer(ref this), pDestination);
    }

    [return: NativeTypeName("HRESULT")]
    public int GetFrameStatistics(DXGI_FRAME_STATISTICS* pStats)
    {
        return ((delegate* unmanaged[Stdcall]<IDXGIOutput1*, DXGI_FRAME_STATISTICS*, int>)(lpVtbl[18]))((IDXGIOutput1*)Unsafe.AsPointer(ref this), pStats);
    }

    [return: NativeTypeName("HRESULT")]
    public int GetDisplayModeList1(DXGI_FORMAT EnumFormat, uint Flags, uint* pNumModes, DXGI_MODE_DESC1* pDesc)
    {
        return ((delegate* unmanaged[Stdcall]<IDXGIOutput1*, DXGI_FORMAT, uint, uint*, DXGI_MODE_DESC1*, int>)(lpVtbl[19]))((IDXGIOutput1*)Unsafe.AsPointer(ref this), EnumFormat, Flags, pNumModes, pDesc);
    }

    [return: NativeTypeName("HRESULT")]
    public int FindClosestMatchingMode1([NativeTypeName("const DXGI_MODE_DESC1 *")] DXGI_MODE_DESC1* pModeToMatch, DXGI_MODE_DESC1* pClosestMatch, IUnknown* pConcernedDevice)
    {
        return ((delegate* unmanaged[Stdcall]<IDXGIOutput1*, DXGI_MODE_DESC1*, DXGI_MODE_DESC1*, IUnknown*, int>)(lpVtbl[20]))((IDXGIOutput1*)Unsafe.AsPointer(ref this), pModeToMatch, pClosestMatch, pConcernedDevice);
    }

    [return: NativeTypeName("HRESULT")]
    public int GetDisplaySurfaceData1(IDXGIResource* pDestination)
    {
        return ((delegate* unmanaged[Stdcall]<IDXGIOutput1*, IDXGIResource*, int>)(lpVtbl[21]))((IDXGIOutput1*)Unsafe.AsPointer(ref this), pDestination);
    }

    [return: NativeTypeName("HRESULT")]
    public int DuplicateOutput(IUnknown* pDevice, IDXGIOutputDuplication** ppOutputDuplication)
    {
        return ((delegate* unmanaged[Stdcall]<IDXGIOutput1*, IUnknown*, IDXGIOutputDuplication**, int>)(lpVtbl[22]))((IDXGIOutput1*)Unsafe.AsPointer(ref this), pDevice, ppOutputDuplication);
    }

    public interface Interface : IDXGIOutput.Interface
    {
        [return: NativeTypeName("HRESULT")]
        int GetDisplayModeList1(DXGI_FORMAT EnumFormat, uint Flags, uint* pNumModes, DXGI_MODE_DESC1* pDesc);

        [return: NativeTypeName("HRESULT")]
        int FindClosestMatchingMode1([NativeTypeName("const DXGI_MODE_DESC1 *")] DXGI_MODE_DESC1* pModeToMatch, DXGI_MODE_DESC1* pClosestMatch, IUnknown* pConcernedDevice);

        [return: NativeTypeName("HRESULT")]
        int GetDisplaySurfaceData1(IDXGIResource* pDestination);

        [return: NativeTypeName("HRESULT")]
        int DuplicateOutput(IUnknown* pDevice, IDXGIOutputDuplication** ppOutputDuplication);
    }
}

[Guid("6007896C-3244-4AFD-BF18-A6D3BEDA5023")]
[NativeTypeName("struct IDXGIDevice3 : IDXGIDevice2")]
internal unsafe partial struct IDXGIDevice3 : IDXGIDevice3.Interface, INativeGuid
{
    static Guid* INativeGuid.NativeGuid => (Guid*)Unsafe.AsPointer(in IID_IDXGIDevice3);

    public void** lpVtbl;

    [return: NativeTypeName("HRESULT")]
    public int QueryInterface([NativeTypeName("const IID &")] Guid* riid, void** ppvObject)
    {
        return ((delegate* unmanaged[Stdcall]<IDXGIDevice3*, Guid*, void**, int>)(lpVtbl[0]))((IDXGIDevice3*)Unsafe.AsPointer(ref this), riid, ppvObject);
    }

    [return: NativeTypeName("ULONG")]
    public uint AddRef()
    {
        return ((delegate* unmanaged[Stdcall]<IDXGIDevice3*, uint>)(lpVtbl[1]))((IDXGIDevice3*)Unsafe.AsPointer(ref this));
    }

    [return: NativeTypeName("ULONG")]
    public uint Release()
    {
        return ((delegate* unmanaged[Stdcall]<IDXGIDevice3*, uint>)(lpVtbl[2]))((IDXGIDevice3*)Unsafe.AsPointer(ref this));
    }

    [return: NativeTypeName("HRESULT")]
    public int SetPrivateData([NativeTypeName("const GUID &")] Guid* Name, uint DataSize, [NativeTypeName("const void *")] void* pData)
    {
        return ((delegate* unmanaged[Stdcall]<IDXGIDevice3*, Guid*, uint, void*, int>)(lpVtbl[3]))((IDXGIDevice3*)Unsafe.AsPointer(ref this), Name, DataSize, pData);
    }

    [return: NativeTypeName("HRESULT")]
    public int SetPrivateDataInterface([NativeTypeName("const GUID &")] Guid* Name, [NativeTypeName("const IUnknown *")] IUnknown* pUnknown)
    {
        return ((delegate* unmanaged[Stdcall]<IDXGIDevice3*, Guid*, IUnknown*, int>)(lpVtbl[4]))((IDXGIDevice3*)Unsafe.AsPointer(ref this), Name, pUnknown);
    }

    [return: NativeTypeName("HRESULT")]
    public int GetPrivateData([NativeTypeName("const GUID &")] Guid* Name, uint* pDataSize, void* pData)
    {
        return ((delegate* unmanaged[Stdcall]<IDXGIDevice3*, Guid*, uint*, void*, int>)(lpVtbl[5]))((IDXGIDevice3*)Unsafe.AsPointer(ref this), Name, pDataSize, pData);
    }

    [return: NativeTypeName("HRESULT")]
    public int GetParent([NativeTypeName("const IID &")] Guid* riid, void** ppParent)
    {
        return ((delegate* unmanaged[Stdcall]<IDXGIDevice3*, Guid*, void**, int>)(lpVtbl[6]))((IDXGIDevice3*)Unsafe.AsPointer(ref this), riid, ppParent);
    }

    [return: NativeTypeName("HRESULT")]
    public int GetAdapter(IDXGIAdapter** pAdapter)
    {
        return ((delegate* unmanaged[Stdcall]<IDXGIDevice3*, IDXGIAdapter**, int>)(lpVtbl[7]))((IDXGIDevice3*)Unsafe.AsPointer(ref this), pAdapter);
    }

    [return: NativeTypeName("HRESULT")]
    public int CreateSurface([NativeTypeName("const DXGI_SURFACE_DESC *")] DXGI_SURFACE_DESC* pDesc, uint NumSurfaces, [NativeTypeName("DXGI_USAGE")] uint Usage, [NativeTypeName("const DXGI_SHARED_RESOURCE *")] DXGI_SHARED_RESOURCE* pSharedResource, IDXGISurface** ppSurface)
    {
        return ((delegate* unmanaged[Stdcall]<IDXGIDevice3*, DXGI_SURFACE_DESC*, uint, uint, DXGI_SHARED_RESOURCE*, IDXGISurface**, int>)(lpVtbl[8]))((IDXGIDevice3*)Unsafe.AsPointer(ref this), pDesc, NumSurfaces, Usage, pSharedResource, ppSurface);
    }

    [return: NativeTypeName("HRESULT")]
    public int QueryResourceResidency([NativeTypeName("IUnknown *const *")] IUnknown** ppResources, DXGI_RESIDENCY* pResidencyStatus, uint NumResources)
    {
        return ((delegate* unmanaged[Stdcall]<IDXGIDevice3*, IUnknown**, DXGI_RESIDENCY*, uint, int>)(lpVtbl[9]))((IDXGIDevice3*)Unsafe.AsPointer(ref this), ppResources, pResidencyStatus, NumResources);
    }

    [return: NativeTypeName("HRESULT")]
    public int SetGPUThreadPriority(int Priority)
    {
        return ((delegate* unmanaged[Stdcall]<IDXGIDevice3*, int, int>)(lpVtbl[10]))((IDXGIDevice3*)Unsafe.AsPointer(ref this), Priority);
    }

    [return: NativeTypeName("HRESULT")]
    public int GetGPUThreadPriority(int* pPriority)
    {
        return ((delegate* unmanaged[Stdcall]<IDXGIDevice3*, int*, int>)(lpVtbl[11]))((IDXGIDevice3*)Unsafe.AsPointer(ref this), pPriority);
    }

    [return: NativeTypeName("HRESULT")]
    public int SetMaximumFrameLatency(uint MaxLatency)
    {
        return ((delegate* unmanaged[Stdcall]<IDXGIDevice3*, uint, int>)(lpVtbl[12]))((IDXGIDevice3*)Unsafe.AsPointer(ref this), MaxLatency);
    }

    [return: NativeTypeName("HRESULT")]
    public int GetMaximumFrameLatency(uint* pMaxLatency)
    {
        return ((delegate* unmanaged[Stdcall]<IDXGIDevice3*, uint*, int>)(lpVtbl[13]))((IDXGIDevice3*)Unsafe.AsPointer(ref this), pMaxLatency);
    }

    [return: NativeTypeName("HRESULT")]
    public int OfferResources(uint NumResources, [NativeTypeName("IDXGIResource *const *")] IDXGIResource** ppResources, DXGI_OFFER_RESOURCE_PRIORITY Priority)
    {
        return ((delegate* unmanaged[Stdcall]<IDXGIDevice3*, uint, IDXGIResource**, DXGI_OFFER_RESOURCE_PRIORITY, int>)(lpVtbl[14]))((IDXGIDevice3*)Unsafe.AsPointer(ref this), NumResources, ppResources, Priority);
    }

    [return: NativeTypeName("HRESULT")]
    public int ReclaimResources(uint NumResources, [NativeTypeName("IDXGIResource *const *")] IDXGIResource** ppResources, [NativeTypeName("BOOL *")] int* pDiscarded)
    {
        return ((delegate* unmanaged[Stdcall]<IDXGIDevice3*, uint, IDXGIResource**, int*, int>)(lpVtbl[15]))((IDXGIDevice3*)Unsafe.AsPointer(ref this), NumResources, ppResources, pDiscarded);
    }

    [return: NativeTypeName("HRESULT")]
    public int EnqueueSetEvent([NativeTypeName("HANDLE")] void* hEvent)
    {
        return ((delegate* unmanaged[Stdcall]<IDXGIDevice3*, void*, int>)(lpVtbl[16]))((IDXGIDevice3*)Unsafe.AsPointer(ref this), hEvent);
    }

    public void Trim()
    {
        ((delegate* unmanaged[Stdcall]<IDXGIDevice3*, void>)(lpVtbl[17]))((IDXGIDevice3*)Unsafe.AsPointer(ref this));
    }

    public interface Interface : IDXGIDevice2.Interface
    {
        void Trim();
    }
}

internal partial struct DXGI_MATRIX_3X2_F
{
    public float _11;

    public float _12;

    public float _21;

    public float _22;

    public float _31;

    public float _32;
}

[Guid("A8BE2AC4-199F-4946-B331-79599FB98DE7")]
[NativeTypeName("struct IDXGISwapChain2 : IDXGISwapChain1")]
internal unsafe partial struct IDXGISwapChain2 : IDXGISwapChain2.Interface, INativeGuid
{
    static Guid* INativeGuid.NativeGuid => (Guid*)Unsafe.AsPointer(in IID_IDXGISwapChain2);

    public void** lpVtbl;

    [return: NativeTypeName("HRESULT")]
    public int QueryInterface([NativeTypeName("const IID &")] Guid* riid, void** ppvObject)
    {
        return ((delegate* unmanaged[Stdcall]<IDXGISwapChain2*, Guid*, void**, int>)(lpVtbl[0]))((IDXGISwapChain2*)Unsafe.AsPointer(ref this), riid, ppvObject);
    }

    [return: NativeTypeName("ULONG")]
    public uint AddRef()
    {
        return ((delegate* unmanaged[Stdcall]<IDXGISwapChain2*, uint>)(lpVtbl[1]))((IDXGISwapChain2*)Unsafe.AsPointer(ref this));
    }

    [return: NativeTypeName("ULONG")]
    public uint Release()
    {
        return ((delegate* unmanaged[Stdcall]<IDXGISwapChain2*, uint>)(lpVtbl[2]))((IDXGISwapChain2*)Unsafe.AsPointer(ref this));
    }

    [return: NativeTypeName("HRESULT")]
    public int SetPrivateData([NativeTypeName("const GUID &")] Guid* Name, uint DataSize, [NativeTypeName("const void *")] void* pData)
    {
        return ((delegate* unmanaged[Stdcall]<IDXGISwapChain2*, Guid*, uint, void*, int>)(lpVtbl[3]))((IDXGISwapChain2*)Unsafe.AsPointer(ref this), Name, DataSize, pData);
    }

    [return: NativeTypeName("HRESULT")]
    public int SetPrivateDataInterface([NativeTypeName("const GUID &")] Guid* Name, [NativeTypeName("const IUnknown *")] IUnknown* pUnknown)
    {
        return ((delegate* unmanaged[Stdcall]<IDXGISwapChain2*, Guid*, IUnknown*, int>)(lpVtbl[4]))((IDXGISwapChain2*)Unsafe.AsPointer(ref this), Name, pUnknown);
    }

    [return: NativeTypeName("HRESULT")]
    public int GetPrivateData([NativeTypeName("const GUID &")] Guid* Name, uint* pDataSize, void* pData)
    {
        return ((delegate* unmanaged[Stdcall]<IDXGISwapChain2*, Guid*, uint*, void*, int>)(lpVtbl[5]))((IDXGISwapChain2*)Unsafe.AsPointer(ref this), Name, pDataSize, pData);
    }

    [return: NativeTypeName("HRESULT")]
    public int GetParent([NativeTypeName("const IID &")] Guid* riid, void** ppParent)
    {
        return ((delegate* unmanaged[Stdcall]<IDXGISwapChain2*, Guid*, void**, int>)(lpVtbl[6]))((IDXGISwapChain2*)Unsafe.AsPointer(ref this), riid, ppParent);
    }

    [return: NativeTypeName("HRESULT")]
    public int GetDevice([NativeTypeName("const IID &")] Guid* riid, void** ppDevice)
    {
        return ((delegate* unmanaged[Stdcall]<IDXGISwapChain2*, Guid*, void**, int>)(lpVtbl[7]))((IDXGISwapChain2*)Unsafe.AsPointer(ref this), riid, ppDevice);
    }

    [return: NativeTypeName("HRESULT")]
    public int Present(uint SyncInterval, uint Flags)
    {
        return ((delegate* unmanaged[Stdcall]<IDXGISwapChain2*, uint, uint, int>)(lpVtbl[8]))((IDXGISwapChain2*)Unsafe.AsPointer(ref this), SyncInterval, Flags);
    }

    [return: NativeTypeName("HRESULT")]
    public int GetBuffer(uint Buffer, [NativeTypeName("const IID &")] Guid* riid, void** ppSurface)
    {
        return ((delegate* unmanaged[Stdcall]<IDXGISwapChain2*, uint, Guid*, void**, int>)(lpVtbl[9]))((IDXGISwapChain2*)Unsafe.AsPointer(ref this), Buffer, riid, ppSurface);
    }

    [return: NativeTypeName("HRESULT")]
    public int SetFullscreenState([NativeTypeName("BOOL")] int Fullscreen, IDXGIOutput* pTarget)
    {
        return ((delegate* unmanaged[Stdcall]<IDXGISwapChain2*, int, IDXGIOutput*, int>)(lpVtbl[10]))((IDXGISwapChain2*)Unsafe.AsPointer(ref this), Fullscreen, pTarget);
    }

    [return: NativeTypeName("HRESULT")]
    public int GetFullscreenState([NativeTypeName("BOOL *")] int* pFullscreen, IDXGIOutput** ppTarget)
    {
        return ((delegate* unmanaged[Stdcall]<IDXGISwapChain2*, int*, IDXGIOutput**, int>)(lpVtbl[11]))((IDXGISwapChain2*)Unsafe.AsPointer(ref this), pFullscreen, ppTarget);
    }

    [return: NativeTypeName("HRESULT")]
    public int GetDesc(DXGI_SWAP_CHAIN_DESC* pDesc)
    {
        return ((delegate* unmanaged[Stdcall]<IDXGISwapChain2*, DXGI_SWAP_CHAIN_DESC*, int>)(lpVtbl[12]))((IDXGISwapChain2*)Unsafe.AsPointer(ref this), pDesc);
    }

    [return: NativeTypeName("HRESULT")]
    public int ResizeBuffers(uint BufferCount, uint Width, uint Height, DXGI_FORMAT NewFormat, uint SwapChainFlags)
    {
        return ((delegate* unmanaged[Stdcall]<IDXGISwapChain2*, uint, uint, uint, DXGI_FORMAT, uint, int>)(lpVtbl[13]))((IDXGISwapChain2*)Unsafe.AsPointer(ref this), BufferCount, Width, Height, NewFormat, SwapChainFlags);
    }

    [return: NativeTypeName("HRESULT")]
    public int ResizeTarget([NativeTypeName("const DXGI_MODE_DESC *")] DXGI_MODE_DESC* pNewTargetParameters)
    {
        return ((delegate* unmanaged[Stdcall]<IDXGISwapChain2*, DXGI_MODE_DESC*, int>)(lpVtbl[14]))((IDXGISwapChain2*)Unsafe.AsPointer(ref this), pNewTargetParameters);
    }

    [return: NativeTypeName("HRESULT")]
    public int GetContainingOutput(IDXGIOutput** ppOutput)
    {
        return ((delegate* unmanaged[Stdcall]<IDXGISwapChain2*, IDXGIOutput**, int>)(lpVtbl[15]))((IDXGISwapChain2*)Unsafe.AsPointer(ref this), ppOutput);
    }

    [return: NativeTypeName("HRESULT")]
    public int GetFrameStatistics(DXGI_FRAME_STATISTICS* pStats)
    {
        return ((delegate* unmanaged[Stdcall]<IDXGISwapChain2*, DXGI_FRAME_STATISTICS*, int>)(lpVtbl[16]))((IDXGISwapChain2*)Unsafe.AsPointer(ref this), pStats);
    }

    [return: NativeTypeName("HRESULT")]
    public int GetLastPresentCount(uint* pLastPresentCount)
    {
        return ((delegate* unmanaged[Stdcall]<IDXGISwapChain2*, uint*, int>)(lpVtbl[17]))((IDXGISwapChain2*)Unsafe.AsPointer(ref this), pLastPresentCount);
    }

    [return: NativeTypeName("HRESULT")]
    public int GetDesc1(DXGI_SWAP_CHAIN_DESC1* pDesc)
    {
        return ((delegate* unmanaged[Stdcall]<IDXGISwapChain2*, DXGI_SWAP_CHAIN_DESC1*, int>)(lpVtbl[18]))((IDXGISwapChain2*)Unsafe.AsPointer(ref this), pDesc);
    }

    [return: NativeTypeName("HRESULT")]
    public int GetFullscreenDesc(DXGI_SWAP_CHAIN_FULLSCREEN_DESC* pDesc)
    {
        return ((delegate* unmanaged[Stdcall]<IDXGISwapChain2*, DXGI_SWAP_CHAIN_FULLSCREEN_DESC*, int>)(lpVtbl[19]))((IDXGISwapChain2*)Unsafe.AsPointer(ref this), pDesc);
    }

    [return: NativeTypeName("HRESULT")]
    public int GetHwnd([NativeTypeName("HWND *")] HWND__** pHwnd)
    {
        return ((delegate* unmanaged[Stdcall]<IDXGISwapChain2*, HWND__**, int>)(lpVtbl[20]))((IDXGISwapChain2*)Unsafe.AsPointer(ref this), pHwnd);
    }

    [return: NativeTypeName("HRESULT")]
    public int GetCoreWindow([NativeTypeName("const IID &")] Guid* refiid, void** ppUnk)
    {
        return ((delegate* unmanaged[Stdcall]<IDXGISwapChain2*, Guid*, void**, int>)(lpVtbl[21]))((IDXGISwapChain2*)Unsafe.AsPointer(ref this), refiid, ppUnk);
    }

    [return: NativeTypeName("HRESULT")]
    public int Present1(uint SyncInterval, uint PresentFlags, [NativeTypeName("const DXGI_PRESENT_PARAMETERS *")] DXGI_PRESENT_PARAMETERS* pPresentParameters)
    {
        return ((delegate* unmanaged[Stdcall]<IDXGISwapChain2*, uint, uint, DXGI_PRESENT_PARAMETERS*, int>)(lpVtbl[22]))((IDXGISwapChain2*)Unsafe.AsPointer(ref this), SyncInterval, PresentFlags, pPresentParameters);
    }

    [return: NativeTypeName("BOOL")]
    public int IsTemporaryMonoSupported()
    {
        return ((delegate* unmanaged[Stdcall]<IDXGISwapChain2*, int>)(lpVtbl[23]))((IDXGISwapChain2*)Unsafe.AsPointer(ref this));
    }

    [return: NativeTypeName("HRESULT")]
    public int GetRestrictToOutput(IDXGIOutput** ppRestrictToOutput)
    {
        return ((delegate* unmanaged[Stdcall]<IDXGISwapChain2*, IDXGIOutput**, int>)(lpVtbl[24]))((IDXGISwapChain2*)Unsafe.AsPointer(ref this), ppRestrictToOutput);
    }

    [return: NativeTypeName("HRESULT")]
    public int SetBackgroundColor([NativeTypeName("const DXGI_RGBA *")] D3DCOLORVALUE* pColor)
    {
        return ((delegate* unmanaged[Stdcall]<IDXGISwapChain2*, D3DCOLORVALUE*, int>)(lpVtbl[25]))((IDXGISwapChain2*)Unsafe.AsPointer(ref this), pColor);
    }

    [return: NativeTypeName("HRESULT")]
    public int GetBackgroundColor([NativeTypeName("DXGI_RGBA *")] D3DCOLORVALUE* pColor)
    {
        return ((delegate* unmanaged[Stdcall]<IDXGISwapChain2*, D3DCOLORVALUE*, int>)(lpVtbl[26]))((IDXGISwapChain2*)Unsafe.AsPointer(ref this), pColor);
    }

    [return: NativeTypeName("HRESULT")]
    public int SetRotation(DXGI_MODE_ROTATION Rotation)
    {
        return ((delegate* unmanaged[Stdcall]<IDXGISwapChain2*, DXGI_MODE_ROTATION, int>)(lpVtbl[27]))((IDXGISwapChain2*)Unsafe.AsPointer(ref this), Rotation);
    }

    [return: NativeTypeName("HRESULT")]
    public int GetRotation(DXGI_MODE_ROTATION* pRotation)
    {
        return ((delegate* unmanaged[Stdcall]<IDXGISwapChain2*, DXGI_MODE_ROTATION*, int>)(lpVtbl[28]))((IDXGISwapChain2*)Unsafe.AsPointer(ref this), pRotation);
    }

    [return: NativeTypeName("HRESULT")]
    public int SetSourceSize(uint Width, uint Height)
    {
        return ((delegate* unmanaged[Stdcall]<IDXGISwapChain2*, uint, uint, int>)(lpVtbl[29]))((IDXGISwapChain2*)Unsafe.AsPointer(ref this), Width, Height);
    }

    [return: NativeTypeName("HRESULT")]
    public int GetSourceSize(uint* pWidth, uint* pHeight)
    {
        return ((delegate* unmanaged[Stdcall]<IDXGISwapChain2*, uint*, uint*, int>)(lpVtbl[30]))((IDXGISwapChain2*)Unsafe.AsPointer(ref this), pWidth, pHeight);
    }

    [return: NativeTypeName("HRESULT")]
    public int SetMaximumFrameLatency(uint MaxLatency)
    {
        return ((delegate* unmanaged[Stdcall]<IDXGISwapChain2*, uint, int>)(lpVtbl[31]))((IDXGISwapChain2*)Unsafe.AsPointer(ref this), MaxLatency);
    }

    [return: NativeTypeName("HRESULT")]
    public int GetMaximumFrameLatency(uint* pMaxLatency)
    {
        return ((delegate* unmanaged[Stdcall]<IDXGISwapChain2*, uint*, int>)(lpVtbl[32]))((IDXGISwapChain2*)Unsafe.AsPointer(ref this), pMaxLatency);
    }

    [return: NativeTypeName("HANDLE")]
    public void* GetFrameLatencyWaitableObject()
    {
        return ((delegate* unmanaged[Stdcall]<IDXGISwapChain2*, void*>)(lpVtbl[33]))((IDXGISwapChain2*)Unsafe.AsPointer(ref this));
    }

    [return: NativeTypeName("HRESULT")]
    public int SetMatrixTransform([NativeTypeName("const DXGI_MATRIX_3X2_F *")] DXGI_MATRIX_3X2_F* pMatrix)
    {
        return ((delegate* unmanaged[Stdcall]<IDXGISwapChain2*, DXGI_MATRIX_3X2_F*, int>)(lpVtbl[34]))((IDXGISwapChain2*)Unsafe.AsPointer(ref this), pMatrix);
    }

    [return: NativeTypeName("HRESULT")]
    public int GetMatrixTransform(DXGI_MATRIX_3X2_F* pMatrix)
    {
        return ((delegate* unmanaged[Stdcall]<IDXGISwapChain2*, DXGI_MATRIX_3X2_F*, int>)(lpVtbl[35]))((IDXGISwapChain2*)Unsafe.AsPointer(ref this), pMatrix);
    }

    public interface Interface : IDXGISwapChain1.Interface
    {
        [return: NativeTypeName("HRESULT")]
        int SetSourceSize(uint Width, uint Height);

        [return: NativeTypeName("HRESULT")]
        int GetSourceSize(uint* pWidth, uint* pHeight);

        [return: NativeTypeName("HRESULT")]
        int SetMaximumFrameLatency(uint MaxLatency);

        [return: NativeTypeName("HRESULT")]
        int GetMaximumFrameLatency(uint* pMaxLatency);

        [return: NativeTypeName("HANDLE")]
        void* GetFrameLatencyWaitableObject();

        [return: NativeTypeName("HRESULT")]
        int SetMatrixTransform([NativeTypeName("const DXGI_MATRIX_3X2_F *")] DXGI_MATRIX_3X2_F* pMatrix);

        [return: NativeTypeName("HRESULT")]
        int GetMatrixTransform(DXGI_MATRIX_3X2_F* pMatrix);
    }
}

[Guid("595E39D1-2724-4663-99B1-DA969DE28364")]
[NativeTypeName("struct IDXGIOutput2 : IDXGIOutput1")]
internal unsafe partial struct IDXGIOutput2 : IDXGIOutput2.Interface, INativeGuid
{
    static Guid* INativeGuid.NativeGuid => (Guid*)Unsafe.AsPointer(in IID_IDXGIOutput2);

    public void** lpVtbl;

    [return: NativeTypeName("HRESULT")]
    public int QueryInterface([NativeTypeName("const IID &")] Guid* riid, void** ppvObject)
    {
        return ((delegate* unmanaged[Stdcall]<IDXGIOutput2*, Guid*, void**, int>)(lpVtbl[0]))((IDXGIOutput2*)Unsafe.AsPointer(ref this), riid, ppvObject);
    }

    [return: NativeTypeName("ULONG")]
    public uint AddRef()
    {
        return ((delegate* unmanaged[Stdcall]<IDXGIOutput2*, uint>)(lpVtbl[1]))((IDXGIOutput2*)Unsafe.AsPointer(ref this));
    }

    [return: NativeTypeName("ULONG")]
    public uint Release()
    {
        return ((delegate* unmanaged[Stdcall]<IDXGIOutput2*, uint>)(lpVtbl[2]))((IDXGIOutput2*)Unsafe.AsPointer(ref this));
    }

    [return: NativeTypeName("HRESULT")]
    public int SetPrivateData([NativeTypeName("const GUID &")] Guid* Name, uint DataSize, [NativeTypeName("const void *")] void* pData)
    {
        return ((delegate* unmanaged[Stdcall]<IDXGIOutput2*, Guid*, uint, void*, int>)(lpVtbl[3]))((IDXGIOutput2*)Unsafe.AsPointer(ref this), Name, DataSize, pData);
    }

    [return: NativeTypeName("HRESULT")]
    public int SetPrivateDataInterface([NativeTypeName("const GUID &")] Guid* Name, [NativeTypeName("const IUnknown *")] IUnknown* pUnknown)
    {
        return ((delegate* unmanaged[Stdcall]<IDXGIOutput2*, Guid*, IUnknown*, int>)(lpVtbl[4]))((IDXGIOutput2*)Unsafe.AsPointer(ref this), Name, pUnknown);
    }

    [return: NativeTypeName("HRESULT")]
    public int GetPrivateData([NativeTypeName("const GUID &")] Guid* Name, uint* pDataSize, void* pData)
    {
        return ((delegate* unmanaged[Stdcall]<IDXGIOutput2*, Guid*, uint*, void*, int>)(lpVtbl[5]))((IDXGIOutput2*)Unsafe.AsPointer(ref this), Name, pDataSize, pData);
    }

    [return: NativeTypeName("HRESULT")]
    public int GetParent([NativeTypeName("const IID &")] Guid* riid, void** ppParent)
    {
        return ((delegate* unmanaged[Stdcall]<IDXGIOutput2*, Guid*, void**, int>)(lpVtbl[6]))((IDXGIOutput2*)Unsafe.AsPointer(ref this), riid, ppParent);
    }

    [return: NativeTypeName("HRESULT")]
    public int GetDesc(DXGI_OUTPUT_DESC* pDesc)
    {
        return ((delegate* unmanaged[Stdcall]<IDXGIOutput2*, DXGI_OUTPUT_DESC*, int>)(lpVtbl[7]))((IDXGIOutput2*)Unsafe.AsPointer(ref this), pDesc);
    }

    [return: NativeTypeName("HRESULT")]
    public int GetDisplayModeList(DXGI_FORMAT EnumFormat, uint Flags, uint* pNumModes, DXGI_MODE_DESC* pDesc)
    {
        return ((delegate* unmanaged[Stdcall]<IDXGIOutput2*, DXGI_FORMAT, uint, uint*, DXGI_MODE_DESC*, int>)(lpVtbl[8]))((IDXGIOutput2*)Unsafe.AsPointer(ref this), EnumFormat, Flags, pNumModes, pDesc);
    }

    [return: NativeTypeName("HRESULT")]
    public int FindClosestMatchingMode([NativeTypeName("const DXGI_MODE_DESC *")] DXGI_MODE_DESC* pModeToMatch, DXGI_MODE_DESC* pClosestMatch, IUnknown* pConcernedDevice)
    {
        return ((delegate* unmanaged[Stdcall]<IDXGIOutput2*, DXGI_MODE_DESC*, DXGI_MODE_DESC*, IUnknown*, int>)(lpVtbl[9]))((IDXGIOutput2*)Unsafe.AsPointer(ref this), pModeToMatch, pClosestMatch, pConcernedDevice);
    }

    [return: NativeTypeName("HRESULT")]
    public int WaitForVBlank()
    {
        return ((delegate* unmanaged[Stdcall]<IDXGIOutput2*, int>)(lpVtbl[10]))((IDXGIOutput2*)Unsafe.AsPointer(ref this));
    }

    [return: NativeTypeName("HRESULT")]
    public int TakeOwnership(IUnknown* pDevice, [NativeTypeName("BOOL")] int Exclusive)
    {
        return ((delegate* unmanaged[Stdcall]<IDXGIOutput2*, IUnknown*, int, int>)(lpVtbl[11]))((IDXGIOutput2*)Unsafe.AsPointer(ref this), pDevice, Exclusive);
    }

    public void ReleaseOwnership()
    {
        ((delegate* unmanaged[Stdcall]<IDXGIOutput2*, void>)(lpVtbl[12]))((IDXGIOutput2*)Unsafe.AsPointer(ref this));
    }

    [return: NativeTypeName("HRESULT")]
    public int GetGammaControlCapabilities(DXGI_GAMMA_CONTROL_CAPABILITIES* pGammaCaps)
    {
        return ((delegate* unmanaged[Stdcall]<IDXGIOutput2*, DXGI_GAMMA_CONTROL_CAPABILITIES*, int>)(lpVtbl[13]))((IDXGIOutput2*)Unsafe.AsPointer(ref this), pGammaCaps);
    }

    [return: NativeTypeName("HRESULT")]
    public int SetGammaControl([NativeTypeName("const DXGI_GAMMA_CONTROL *")] DXGI_GAMMA_CONTROL* pArray)
    {
        return ((delegate* unmanaged[Stdcall]<IDXGIOutput2*, DXGI_GAMMA_CONTROL*, int>)(lpVtbl[14]))((IDXGIOutput2*)Unsafe.AsPointer(ref this), pArray);
    }

    [return: NativeTypeName("HRESULT")]
    public int GetGammaControl(DXGI_GAMMA_CONTROL* pArray)
    {
        return ((delegate* unmanaged[Stdcall]<IDXGIOutput2*, DXGI_GAMMA_CONTROL*, int>)(lpVtbl[15]))((IDXGIOutput2*)Unsafe.AsPointer(ref this), pArray);
    }

    [return: NativeTypeName("HRESULT")]
    public int SetDisplaySurface(IDXGISurface* pScanoutSurface)
    {
        return ((delegate* unmanaged[Stdcall]<IDXGIOutput2*, IDXGISurface*, int>)(lpVtbl[16]))((IDXGIOutput2*)Unsafe.AsPointer(ref this), pScanoutSurface);
    }

    [return: NativeTypeName("HRESULT")]
    public int GetDisplaySurfaceData(IDXGISurface* pDestination)
    {
        return ((delegate* unmanaged[Stdcall]<IDXGIOutput2*, IDXGISurface*, int>)(lpVtbl[17]))((IDXGIOutput2*)Unsafe.AsPointer(ref this), pDestination);
    }

    [return: NativeTypeName("HRESULT")]
    public int GetFrameStatistics(DXGI_FRAME_STATISTICS* pStats)
    {
        return ((delegate* unmanaged[Stdcall]<IDXGIOutput2*, DXGI_FRAME_STATISTICS*, int>)(lpVtbl[18]))((IDXGIOutput2*)Unsafe.AsPointer(ref this), pStats);
    }

    [return: NativeTypeName("HRESULT")]
    public int GetDisplayModeList1(DXGI_FORMAT EnumFormat, uint Flags, uint* pNumModes, DXGI_MODE_DESC1* pDesc)
    {
        return ((delegate* unmanaged[Stdcall]<IDXGIOutput2*, DXGI_FORMAT, uint, uint*, DXGI_MODE_DESC1*, int>)(lpVtbl[19]))((IDXGIOutput2*)Unsafe.AsPointer(ref this), EnumFormat, Flags, pNumModes, pDesc);
    }

    [return: NativeTypeName("HRESULT")]
    public int FindClosestMatchingMode1([NativeTypeName("const DXGI_MODE_DESC1 *")] DXGI_MODE_DESC1* pModeToMatch, DXGI_MODE_DESC1* pClosestMatch, IUnknown* pConcernedDevice)
    {
        return ((delegate* unmanaged[Stdcall]<IDXGIOutput2*, DXGI_MODE_DESC1*, DXGI_MODE_DESC1*, IUnknown*, int>)(lpVtbl[20]))((IDXGIOutput2*)Unsafe.AsPointer(ref this), pModeToMatch, pClosestMatch, pConcernedDevice);
    }

    [return: NativeTypeName("HRESULT")]
    public int GetDisplaySurfaceData1(IDXGIResource* pDestination)
    {
        return ((delegate* unmanaged[Stdcall]<IDXGIOutput2*, IDXGIResource*, int>)(lpVtbl[21]))((IDXGIOutput2*)Unsafe.AsPointer(ref this), pDestination);
    }

    [return: NativeTypeName("HRESULT")]
    public int DuplicateOutput(IUnknown* pDevice, IDXGIOutputDuplication** ppOutputDuplication)
    {
        return ((delegate* unmanaged[Stdcall]<IDXGIOutput2*, IUnknown*, IDXGIOutputDuplication**, int>)(lpVtbl[22]))((IDXGIOutput2*)Unsafe.AsPointer(ref this), pDevice, ppOutputDuplication);
    }

    [return: NativeTypeName("BOOL")]
    public int SupportsOverlays()
    {
        return ((delegate* unmanaged[Stdcall]<IDXGIOutput2*, int>)(lpVtbl[23]))((IDXGIOutput2*)Unsafe.AsPointer(ref this));
    }

    public interface Interface : IDXGIOutput1.Interface
    {
        [return: NativeTypeName("BOOL")]
        int SupportsOverlays();
    }
}

[Guid("25483823-CD46-4C7D-86CA-47AA95B837BD")]
[NativeTypeName("struct IDXGIFactory3 : IDXGIFactory2")]
internal unsafe partial struct IDXGIFactory3 : IDXGIFactory3.Interface, INativeGuid
{
    static Guid* INativeGuid.NativeGuid => (Guid*)Unsafe.AsPointer(in IID_IDXGIFactory3);

    public void** lpVtbl;

    [return: NativeTypeName("HRESULT")]
    public int QueryInterface([NativeTypeName("const IID &")] Guid* riid, void** ppvObject)
    {
        return ((delegate* unmanaged[Stdcall]<IDXGIFactory3*, Guid*, void**, int>)(lpVtbl[0]))((IDXGIFactory3*)Unsafe.AsPointer(ref this), riid, ppvObject);
    }

    [return: NativeTypeName("ULONG")]
    public uint AddRef()
    {
        return ((delegate* unmanaged[Stdcall]<IDXGIFactory3*, uint>)(lpVtbl[1]))((IDXGIFactory3*)Unsafe.AsPointer(ref this));
    }

    [return: NativeTypeName("ULONG")]
    public uint Release()
    {
        return ((delegate* unmanaged[Stdcall]<IDXGIFactory3*, uint>)(lpVtbl[2]))((IDXGIFactory3*)Unsafe.AsPointer(ref this));
    }

    [return: NativeTypeName("HRESULT")]
    public int SetPrivateData([NativeTypeName("const GUID &")] Guid* Name, uint DataSize, [NativeTypeName("const void *")] void* pData)
    {
        return ((delegate* unmanaged[Stdcall]<IDXGIFactory3*, Guid*, uint, void*, int>)(lpVtbl[3]))((IDXGIFactory3*)Unsafe.AsPointer(ref this), Name, DataSize, pData);
    }

    [return: NativeTypeName("HRESULT")]
    public int SetPrivateDataInterface([NativeTypeName("const GUID &")] Guid* Name, [NativeTypeName("const IUnknown *")] IUnknown* pUnknown)
    {
        return ((delegate* unmanaged[Stdcall]<IDXGIFactory3*, Guid*, IUnknown*, int>)(lpVtbl[4]))((IDXGIFactory3*)Unsafe.AsPointer(ref this), Name, pUnknown);
    }

    [return: NativeTypeName("HRESULT")]
    public int GetPrivateData([NativeTypeName("const GUID &")] Guid* Name, uint* pDataSize, void* pData)
    {
        return ((delegate* unmanaged[Stdcall]<IDXGIFactory3*, Guid*, uint*, void*, int>)(lpVtbl[5]))((IDXGIFactory3*)Unsafe.AsPointer(ref this), Name, pDataSize, pData);
    }

    [return: NativeTypeName("HRESULT")]
    public int GetParent([NativeTypeName("const IID &")] Guid* riid, void** ppParent)
    {
        return ((delegate* unmanaged[Stdcall]<IDXGIFactory3*, Guid*, void**, int>)(lpVtbl[6]))((IDXGIFactory3*)Unsafe.AsPointer(ref this), riid, ppParent);
    }

    [return: NativeTypeName("HRESULT")]
    public int EnumAdapters(uint Adapter, IDXGIAdapter** ppAdapter)
    {
        return ((delegate* unmanaged[Stdcall]<IDXGIFactory3*, uint, IDXGIAdapter**, int>)(lpVtbl[7]))((IDXGIFactory3*)Unsafe.AsPointer(ref this), Adapter, ppAdapter);
    }

    [return: NativeTypeName("HRESULT")]
    public int MakeWindowAssociation([NativeTypeName("HWND")] HWND__* WindowHandle, uint Flags)
    {
        return ((delegate* unmanaged[Stdcall]<IDXGIFactory3*, HWND__*, uint, int>)(lpVtbl[8]))((IDXGIFactory3*)Unsafe.AsPointer(ref this), WindowHandle, Flags);
    }

    [return: NativeTypeName("HRESULT")]
    public int GetWindowAssociation([NativeTypeName("HWND *")] HWND__** pWindowHandle)
    {
        return ((delegate* unmanaged[Stdcall]<IDXGIFactory3*, HWND__**, int>)(lpVtbl[9]))((IDXGIFactory3*)Unsafe.AsPointer(ref this), pWindowHandle);
    }

    [return: NativeTypeName("HRESULT")]
    public int CreateSwapChain(IUnknown* pDevice, DXGI_SWAP_CHAIN_DESC* pDesc, IDXGISwapChain** ppSwapChain)
    {
        return ((delegate* unmanaged[Stdcall]<IDXGIFactory3*, IUnknown*, DXGI_SWAP_CHAIN_DESC*, IDXGISwapChain**, int>)(lpVtbl[10]))((IDXGIFactory3*)Unsafe.AsPointer(ref this), pDevice, pDesc, ppSwapChain);
    }

    [return: NativeTypeName("HRESULT")]
    public int CreateSoftwareAdapter([NativeTypeName("HMODULE")] HINSTANCE__* Module, IDXGIAdapter** ppAdapter)
    {
        return ((delegate* unmanaged[Stdcall]<IDXGIFactory3*, HINSTANCE__*, IDXGIAdapter**, int>)(lpVtbl[11]))((IDXGIFactory3*)Unsafe.AsPointer(ref this), Module, ppAdapter);
    }

    [return: NativeTypeName("HRESULT")]
    public int EnumAdapters1(uint Adapter, IDXGIAdapter1** ppAdapter)
    {
        return ((delegate* unmanaged[Stdcall]<IDXGIFactory3*, uint, IDXGIAdapter1**, int>)(lpVtbl[12]))((IDXGIFactory3*)Unsafe.AsPointer(ref this), Adapter, ppAdapter);
    }

    [return: NativeTypeName("BOOL")]
    public int IsCurrent()
    {
        return ((delegate* unmanaged[Stdcall]<IDXGIFactory3*, int>)(lpVtbl[13]))((IDXGIFactory3*)Unsafe.AsPointer(ref this));
    }

    [return: NativeTypeName("BOOL")]
    public int IsWindowedStereoEnabled()
    {
        return ((delegate* unmanaged[Stdcall]<IDXGIFactory3*, int>)(lpVtbl[14]))((IDXGIFactory3*)Unsafe.AsPointer(ref this));
    }

    [return: NativeTypeName("HRESULT")]
    public int CreateSwapChainForHwnd(IUnknown* pDevice, [NativeTypeName("HWND")] HWND__* hWnd, [NativeTypeName("const DXGI_SWAP_CHAIN_DESC1 *")] DXGI_SWAP_CHAIN_DESC1* pDesc, [NativeTypeName("const DXGI_SWAP_CHAIN_FULLSCREEN_DESC *")] DXGI_SWAP_CHAIN_FULLSCREEN_DESC* pFullscreenDesc, IDXGIOutput* pRestrictToOutput, IDXGISwapChain1** ppSwapChain)
    {
        return ((delegate* unmanaged[Stdcall]<IDXGIFactory3*, IUnknown*, HWND__*, DXGI_SWAP_CHAIN_DESC1*, DXGI_SWAP_CHAIN_FULLSCREEN_DESC*, IDXGIOutput*, IDXGISwapChain1**, int>)(lpVtbl[15]))((IDXGIFactory3*)Unsafe.AsPointer(ref this), pDevice, hWnd, pDesc, pFullscreenDesc, pRestrictToOutput, ppSwapChain);
    }

    [return: NativeTypeName("HRESULT")]
    public int CreateSwapChainForCoreWindow(IUnknown* pDevice, IUnknown* pWindow, [NativeTypeName("const DXGI_SWAP_CHAIN_DESC1 *")] DXGI_SWAP_CHAIN_DESC1* pDesc, IDXGIOutput* pRestrictToOutput, IDXGISwapChain1** ppSwapChain)
    {
        return ((delegate* unmanaged[Stdcall]<IDXGIFactory3*, IUnknown*, IUnknown*, DXGI_SWAP_CHAIN_DESC1*, IDXGIOutput*, IDXGISwapChain1**, int>)(lpVtbl[16]))((IDXGIFactory3*)Unsafe.AsPointer(ref this), pDevice, pWindow, pDesc, pRestrictToOutput, ppSwapChain);
    }

    [return: NativeTypeName("HRESULT")]
    public int GetSharedResourceAdapterLuid([NativeTypeName("HANDLE")] void* hResource, LUID* pLuid)
    {
        return ((delegate* unmanaged[Stdcall]<IDXGIFactory3*, void*, LUID*, int>)(lpVtbl[17]))((IDXGIFactory3*)Unsafe.AsPointer(ref this), hResource, pLuid);
    }

    [return: NativeTypeName("HRESULT")]
    public int RegisterStereoStatusWindow([NativeTypeName("HWND")] HWND__* WindowHandle, uint wMsg, [NativeTypeName("DWORD *")] uint* pdwCookie)
    {
        return ((delegate* unmanaged[Stdcall]<IDXGIFactory3*, HWND__*, uint, uint*, int>)(lpVtbl[18]))((IDXGIFactory3*)Unsafe.AsPointer(ref this), WindowHandle, wMsg, pdwCookie);
    }

    [return: NativeTypeName("HRESULT")]
    public int RegisterStereoStatusEvent([NativeTypeName("HANDLE")] void* hEvent, [NativeTypeName("DWORD *")] uint* pdwCookie)
    {
        return ((delegate* unmanaged[Stdcall]<IDXGIFactory3*, void*, uint*, int>)(lpVtbl[19]))((IDXGIFactory3*)Unsafe.AsPointer(ref this), hEvent, pdwCookie);
    }

    public void UnregisterStereoStatus([NativeTypeName("DWORD")] uint dwCookie)
    {
        ((delegate* unmanaged[Stdcall]<IDXGIFactory3*, uint, void>)(lpVtbl[20]))((IDXGIFactory3*)Unsafe.AsPointer(ref this), dwCookie);
    }

    [return: NativeTypeName("HRESULT")]
    public int RegisterOcclusionStatusWindow([NativeTypeName("HWND")] HWND__* WindowHandle, uint wMsg, [NativeTypeName("DWORD *")] uint* pdwCookie)
    {
        return ((delegate* unmanaged[Stdcall]<IDXGIFactory3*, HWND__*, uint, uint*, int>)(lpVtbl[21]))((IDXGIFactory3*)Unsafe.AsPointer(ref this), WindowHandle, wMsg, pdwCookie);
    }

    [return: NativeTypeName("HRESULT")]
    public int RegisterOcclusionStatusEvent([NativeTypeName("HANDLE")] void* hEvent, [NativeTypeName("DWORD *")] uint* pdwCookie)
    {
        return ((delegate* unmanaged[Stdcall]<IDXGIFactory3*, void*, uint*, int>)(lpVtbl[22]))((IDXGIFactory3*)Unsafe.AsPointer(ref this), hEvent, pdwCookie);
    }

    public void UnregisterOcclusionStatus([NativeTypeName("DWORD")] uint dwCookie)
    {
        ((delegate* unmanaged[Stdcall]<IDXGIFactory3*, uint, void>)(lpVtbl[23]))((IDXGIFactory3*)Unsafe.AsPointer(ref this), dwCookie);
    }

    [return: NativeTypeName("HRESULT")]
    public int CreateSwapChainForComposition(IUnknown* pDevice, [NativeTypeName("const DXGI_SWAP_CHAIN_DESC1 *")] DXGI_SWAP_CHAIN_DESC1* pDesc, IDXGIOutput* pRestrictToOutput, IDXGISwapChain1** ppSwapChain)
    {
        return ((delegate* unmanaged[Stdcall]<IDXGIFactory3*, IUnknown*, DXGI_SWAP_CHAIN_DESC1*, IDXGIOutput*, IDXGISwapChain1**, int>)(lpVtbl[24]))((IDXGIFactory3*)Unsafe.AsPointer(ref this), pDevice, pDesc, pRestrictToOutput, ppSwapChain);
    }

    public uint GetCreationFlags()
    {
        return ((delegate* unmanaged[Stdcall]<IDXGIFactory3*, uint>)(lpVtbl[25]))((IDXGIFactory3*)Unsafe.AsPointer(ref this));
    }

    public interface Interface : IDXGIFactory2.Interface
    {
        uint GetCreationFlags();
    }
}

internal partial struct DXGI_DECODE_SWAP_CHAIN_DESC
{
    public uint Flags;
}

internal enum DXGI_MULTIPLANE_OVERLAY_YCbCr_FLAGS
{
    DXGI_MULTIPLANE_OVERLAY_YCbCr_FLAG_NOMINAL_RANGE = 0x1,
    DXGI_MULTIPLANE_OVERLAY_YCbCr_FLAG_BT709 = 0x2,
    DXGI_MULTIPLANE_OVERLAY_YCbCr_FLAG_xvYCC = 0x4,
}

[Guid("2633066B-4514-4C7A-8FD8-12EA98059D18")]
[NativeTypeName("struct IDXGIDecodeSwapChain : IUnknown")]
internal unsafe partial struct IDXGIDecodeSwapChain : IDXGIDecodeSwapChain.Interface, INativeGuid
{
    static Guid* INativeGuid.NativeGuid => (Guid*)Unsafe.AsPointer(in IID_IDXGIDecodeSwapChain);

    public void** lpVtbl;

    [return: NativeTypeName("HRESULT")]
    public int QueryInterface([NativeTypeName("const IID &")] Guid* riid, void** ppvObject)
    {
        return ((delegate* unmanaged[Stdcall]<IDXGIDecodeSwapChain*, Guid*, void**, int>)(lpVtbl[0]))((IDXGIDecodeSwapChain*)Unsafe.AsPointer(ref this), riid, ppvObject);
    }

    [return: NativeTypeName("ULONG")]
    public uint AddRef()
    {
        return ((delegate* unmanaged[Stdcall]<IDXGIDecodeSwapChain*, uint>)(lpVtbl[1]))((IDXGIDecodeSwapChain*)Unsafe.AsPointer(ref this));
    }

    [return: NativeTypeName("ULONG")]
    public uint Release()
    {
        return ((delegate* unmanaged[Stdcall]<IDXGIDecodeSwapChain*, uint>)(lpVtbl[2]))((IDXGIDecodeSwapChain*)Unsafe.AsPointer(ref this));
    }

    [return: NativeTypeName("HRESULT")]
    public int PresentBuffer(uint BufferToPresent, uint SyncInterval, uint Flags)
    {
        return ((delegate* unmanaged[Stdcall]<IDXGIDecodeSwapChain*, uint, uint, uint, int>)(lpVtbl[3]))((IDXGIDecodeSwapChain*)Unsafe.AsPointer(ref this), BufferToPresent, SyncInterval, Flags);
    }

    [return: NativeTypeName("HRESULT")]
    public int SetSourceRect([NativeTypeName("const RECT *")] RECT* pRect)
    {
        return ((delegate* unmanaged[Stdcall]<IDXGIDecodeSwapChain*, RECT*, int>)(lpVtbl[4]))((IDXGIDecodeSwapChain*)Unsafe.AsPointer(ref this), pRect);
    }

    [return: NativeTypeName("HRESULT")]
    public int SetTargetRect([NativeTypeName("const RECT *")] RECT* pRect)
    {
        return ((delegate* unmanaged[Stdcall]<IDXGIDecodeSwapChain*, RECT*, int>)(lpVtbl[5]))((IDXGIDecodeSwapChain*)Unsafe.AsPointer(ref this), pRect);
    }

    [return: NativeTypeName("HRESULT")]
    public int SetDestSize(uint Width, uint Height)
    {
        return ((delegate* unmanaged[Stdcall]<IDXGIDecodeSwapChain*, uint, uint, int>)(lpVtbl[6]))((IDXGIDecodeSwapChain*)Unsafe.AsPointer(ref this), Width, Height);
    }

    [return: NativeTypeName("HRESULT")]
    public int GetSourceRect(RECT* pRect)
    {
        return ((delegate* unmanaged[Stdcall]<IDXGIDecodeSwapChain*, RECT*, int>)(lpVtbl[7]))((IDXGIDecodeSwapChain*)Unsafe.AsPointer(ref this), pRect);
    }

    [return: NativeTypeName("HRESULT")]
    public int GetTargetRect(RECT* pRect)
    {
        return ((delegate* unmanaged[Stdcall]<IDXGIDecodeSwapChain*, RECT*, int>)(lpVtbl[8]))((IDXGIDecodeSwapChain*)Unsafe.AsPointer(ref this), pRect);
    }

    [return: NativeTypeName("HRESULT")]
    public int GetDestSize(uint* pWidth, uint* pHeight)
    {
        return ((delegate* unmanaged[Stdcall]<IDXGIDecodeSwapChain*, uint*, uint*, int>)(lpVtbl[9]))((IDXGIDecodeSwapChain*)Unsafe.AsPointer(ref this), pWidth, pHeight);
    }

    [return: NativeTypeName("HRESULT")]
    public int SetColorSpace(DXGI_MULTIPLANE_OVERLAY_YCbCr_FLAGS ColorSpace)
    {
        return ((delegate* unmanaged[Stdcall]<IDXGIDecodeSwapChain*, DXGI_MULTIPLANE_OVERLAY_YCbCr_FLAGS, int>)(lpVtbl[10]))((IDXGIDecodeSwapChain*)Unsafe.AsPointer(ref this), ColorSpace);
    }

    public DXGI_MULTIPLANE_OVERLAY_YCbCr_FLAGS GetColorSpace()
    {
        return ((delegate* unmanaged[Stdcall]<IDXGIDecodeSwapChain*, DXGI_MULTIPLANE_OVERLAY_YCbCr_FLAGS>)(lpVtbl[11]))((IDXGIDecodeSwapChain*)Unsafe.AsPointer(ref this));
    }

    public interface Interface : IUnknown.Interface
    {
        [return: NativeTypeName("HRESULT")]
        int PresentBuffer(uint BufferToPresent, uint SyncInterval, uint Flags);

        [return: NativeTypeName("HRESULT")]
        int SetSourceRect([NativeTypeName("const RECT *")] RECT* pRect);

        [return: NativeTypeName("HRESULT")]
        int SetTargetRect([NativeTypeName("const RECT *")] RECT* pRect);

        [return: NativeTypeName("HRESULT")]
        int SetDestSize(uint Width, uint Height);

        [return: NativeTypeName("HRESULT")]
        int GetSourceRect(RECT* pRect);

        [return: NativeTypeName("HRESULT")]
        int GetTargetRect(RECT* pRect);

        [return: NativeTypeName("HRESULT")]
        int GetDestSize(uint* pWidth, uint* pHeight);

        [return: NativeTypeName("HRESULT")]
        int SetColorSpace(DXGI_MULTIPLANE_OVERLAY_YCbCr_FLAGS ColorSpace);

        DXGI_MULTIPLANE_OVERLAY_YCbCr_FLAGS GetColorSpace();
    }
}

[Guid("41E7D1F2-A591-4F7B-A2E5-FA9C843E1C12")]
[NativeTypeName("struct IDXGIFactoryMedia : IUnknown")]
internal unsafe partial struct IDXGIFactoryMedia : IDXGIFactoryMedia.Interface, INativeGuid
{
    static Guid* INativeGuid.NativeGuid => (Guid*)Unsafe.AsPointer(in IID_IDXGIFactoryMedia);

    public void** lpVtbl;

    [return: NativeTypeName("HRESULT")]
    public int QueryInterface([NativeTypeName("const IID &")] Guid* riid, void** ppvObject)
    {
        return ((delegate* unmanaged[Stdcall]<IDXGIFactoryMedia*, Guid*, void**, int>)(lpVtbl[0]))((IDXGIFactoryMedia*)Unsafe.AsPointer(ref this), riid, ppvObject);
    }

    [return: NativeTypeName("ULONG")]
    public uint AddRef()
    {
        return ((delegate* unmanaged[Stdcall]<IDXGIFactoryMedia*, uint>)(lpVtbl[1]))((IDXGIFactoryMedia*)Unsafe.AsPointer(ref this));
    }

    [return: NativeTypeName("ULONG")]
    public uint Release()
    {
        return ((delegate* unmanaged[Stdcall]<IDXGIFactoryMedia*, uint>)(lpVtbl[2]))((IDXGIFactoryMedia*)Unsafe.AsPointer(ref this));
    }

    [return: NativeTypeName("HRESULT")]
    public int CreateSwapChainForCompositionSurfaceHandle(IUnknown* pDevice, [NativeTypeName("HANDLE")] void* hSurface, [NativeTypeName("const DXGI_SWAP_CHAIN_DESC1 *")] DXGI_SWAP_CHAIN_DESC1* pDesc, IDXGIOutput* pRestrictToOutput, IDXGISwapChain1** ppSwapChain)
    {
        return ((delegate* unmanaged[Stdcall]<IDXGIFactoryMedia*, IUnknown*, void*, DXGI_SWAP_CHAIN_DESC1*, IDXGIOutput*, IDXGISwapChain1**, int>)(lpVtbl[3]))((IDXGIFactoryMedia*)Unsafe.AsPointer(ref this), pDevice, hSurface, pDesc, pRestrictToOutput, ppSwapChain);
    }

    [return: NativeTypeName("HRESULT")]
    public int CreateDecodeSwapChainForCompositionSurfaceHandle(IUnknown* pDevice, [NativeTypeName("HANDLE")] void* hSurface, DXGI_DECODE_SWAP_CHAIN_DESC* pDesc, IDXGIResource* pYuvDecodeBuffers, IDXGIOutput* pRestrictToOutput, IDXGIDecodeSwapChain** ppSwapChain)
    {
        return ((delegate* unmanaged[Stdcall]<IDXGIFactoryMedia*, IUnknown*, void*, DXGI_DECODE_SWAP_CHAIN_DESC*, IDXGIResource*, IDXGIOutput*, IDXGIDecodeSwapChain**, int>)(lpVtbl[4]))((IDXGIFactoryMedia*)Unsafe.AsPointer(ref this), pDevice, hSurface, pDesc, pYuvDecodeBuffers, pRestrictToOutput, ppSwapChain);
    }

    public interface Interface : IUnknown.Interface
    {
        [return: NativeTypeName("HRESULT")]
        int CreateSwapChainForCompositionSurfaceHandle(IUnknown* pDevice, [NativeTypeName("HANDLE")] void* hSurface, [NativeTypeName("const DXGI_SWAP_CHAIN_DESC1 *")] DXGI_SWAP_CHAIN_DESC1* pDesc, IDXGIOutput* pRestrictToOutput, IDXGISwapChain1** ppSwapChain);

        [return: NativeTypeName("HRESULT")]
        int CreateDecodeSwapChainForCompositionSurfaceHandle(IUnknown* pDevice, [NativeTypeName("HANDLE")] void* hSurface, DXGI_DECODE_SWAP_CHAIN_DESC* pDesc, IDXGIResource* pYuvDecodeBuffers, IDXGIOutput* pRestrictToOutput, IDXGIDecodeSwapChain** ppSwapChain);
    }
}

internal enum DXGI_FRAME_PRESENTATION_MODE
{
    DXGI_FRAME_PRESENTATION_MODE_COMPOSED = 0,
    DXGI_FRAME_PRESENTATION_MODE_OVERLAY = 1,
    DXGI_FRAME_PRESENTATION_MODE_NONE = 2,
    DXGI_FRAME_PRESENTATION_MODE_COMPOSITION_FAILURE = 3,
}

internal partial struct DXGI_FRAME_STATISTICS_MEDIA
{
    public uint PresentCount;

    public uint PresentRefreshCount;

    public uint SyncRefreshCount;

    public LARGE_INTEGER SyncQPCTime;

    public LARGE_INTEGER SyncGPUTime;

    public DXGI_FRAME_PRESENTATION_MODE CompositionMode;

    public uint ApprovedPresentDuration;
}

[Guid("DD95B90B-F05F-4F6A-BD65-25BFB264BD84")]
[NativeTypeName("struct IDXGISwapChainMedia : IUnknown")]
internal unsafe partial struct IDXGISwapChainMedia : IDXGISwapChainMedia.Interface, INativeGuid
{
    static Guid* INativeGuid.NativeGuid => (Guid*)Unsafe.AsPointer(in IID_IDXGISwapChainMedia);

    public void** lpVtbl;

    [return: NativeTypeName("HRESULT")]
    public int QueryInterface([NativeTypeName("const IID &")] Guid* riid, void** ppvObject)
    {
        return ((delegate* unmanaged[Stdcall]<IDXGISwapChainMedia*, Guid*, void**, int>)(lpVtbl[0]))((IDXGISwapChainMedia*)Unsafe.AsPointer(ref this), riid, ppvObject);
    }

    [return: NativeTypeName("ULONG")]
    public uint AddRef()
    {
        return ((delegate* unmanaged[Stdcall]<IDXGISwapChainMedia*, uint>)(lpVtbl[1]))((IDXGISwapChainMedia*)Unsafe.AsPointer(ref this));
    }

    [return: NativeTypeName("ULONG")]
    public uint Release()
    {
        return ((delegate* unmanaged[Stdcall]<IDXGISwapChainMedia*, uint>)(lpVtbl[2]))((IDXGISwapChainMedia*)Unsafe.AsPointer(ref this));
    }

    [return: NativeTypeName("HRESULT")]
    public int GetFrameStatisticsMedia(DXGI_FRAME_STATISTICS_MEDIA* pStats)
    {
        return ((delegate* unmanaged[Stdcall]<IDXGISwapChainMedia*, DXGI_FRAME_STATISTICS_MEDIA*, int>)(lpVtbl[3]))((IDXGISwapChainMedia*)Unsafe.AsPointer(ref this), pStats);
    }

    [return: NativeTypeName("HRESULT")]
    public int SetPresentDuration(uint Duration)
    {
        return ((delegate* unmanaged[Stdcall]<IDXGISwapChainMedia*, uint, int>)(lpVtbl[4]))((IDXGISwapChainMedia*)Unsafe.AsPointer(ref this), Duration);
    }

    [return: NativeTypeName("HRESULT")]
    public int CheckPresentDurationSupport(uint DesiredPresentDuration, uint* pClosestSmallerPresentDuration, uint* pClosestLargerPresentDuration)
    {
        return ((delegate* unmanaged[Stdcall]<IDXGISwapChainMedia*, uint, uint*, uint*, int>)(lpVtbl[5]))((IDXGISwapChainMedia*)Unsafe.AsPointer(ref this), DesiredPresentDuration, pClosestSmallerPresentDuration, pClosestLargerPresentDuration);
    }

    public interface Interface : IUnknown.Interface
    {
        [return: NativeTypeName("HRESULT")]
        int GetFrameStatisticsMedia(DXGI_FRAME_STATISTICS_MEDIA* pStats);

        [return: NativeTypeName("HRESULT")]
        int SetPresentDuration(uint Duration);

        [return: NativeTypeName("HRESULT")]
        int CheckPresentDurationSupport(uint DesiredPresentDuration, uint* pClosestSmallerPresentDuration, uint* pClosestLargerPresentDuration);
    }
}

internal enum DXGI_OVERLAY_SUPPORT_FLAG
{
    DXGI_OVERLAY_SUPPORT_FLAG_DIRECT = 0x1,
    DXGI_OVERLAY_SUPPORT_FLAG_SCALING = 0x2,
}

[Guid("8A6BB301-7E7E-41F4-A8E0-5B32F7F99B18")]
[NativeTypeName("struct IDXGIOutput3 : IDXGIOutput2")]
internal unsafe partial struct IDXGIOutput3 : IDXGIOutput3.Interface, INativeGuid
{
    static Guid* INativeGuid.NativeGuid => (Guid*)Unsafe.AsPointer(in IID_IDXGIOutput3);

    public void** lpVtbl;

    [return: NativeTypeName("HRESULT")]
    public int QueryInterface([NativeTypeName("const IID &")] Guid* riid, void** ppvObject)
    {
        return ((delegate* unmanaged[Stdcall]<IDXGIOutput3*, Guid*, void**, int>)(lpVtbl[0]))((IDXGIOutput3*)Unsafe.AsPointer(ref this), riid, ppvObject);
    }

    [return: NativeTypeName("ULONG")]
    public uint AddRef()
    {
        return ((delegate* unmanaged[Stdcall]<IDXGIOutput3*, uint>)(lpVtbl[1]))((IDXGIOutput3*)Unsafe.AsPointer(ref this));
    }

    [return: NativeTypeName("ULONG")]
    public uint Release()
    {
        return ((delegate* unmanaged[Stdcall]<IDXGIOutput3*, uint>)(lpVtbl[2]))((IDXGIOutput3*)Unsafe.AsPointer(ref this));
    }

    [return: NativeTypeName("HRESULT")]
    public int SetPrivateData([NativeTypeName("const GUID &")] Guid* Name, uint DataSize, [NativeTypeName("const void *")] void* pData)
    {
        return ((delegate* unmanaged[Stdcall]<IDXGIOutput3*, Guid*, uint, void*, int>)(lpVtbl[3]))((IDXGIOutput3*)Unsafe.AsPointer(ref this), Name, DataSize, pData);
    }

    [return: NativeTypeName("HRESULT")]
    public int SetPrivateDataInterface([NativeTypeName("const GUID &")] Guid* Name, [NativeTypeName("const IUnknown *")] IUnknown* pUnknown)
    {
        return ((delegate* unmanaged[Stdcall]<IDXGIOutput3*, Guid*, IUnknown*, int>)(lpVtbl[4]))((IDXGIOutput3*)Unsafe.AsPointer(ref this), Name, pUnknown);
    }

    [return: NativeTypeName("HRESULT")]
    public int GetPrivateData([NativeTypeName("const GUID &")] Guid* Name, uint* pDataSize, void* pData)
    {
        return ((delegate* unmanaged[Stdcall]<IDXGIOutput3*, Guid*, uint*, void*, int>)(lpVtbl[5]))((IDXGIOutput3*)Unsafe.AsPointer(ref this), Name, pDataSize, pData);
    }

    [return: NativeTypeName("HRESULT")]
    public int GetParent([NativeTypeName("const IID &")] Guid* riid, void** ppParent)
    {
        return ((delegate* unmanaged[Stdcall]<IDXGIOutput3*, Guid*, void**, int>)(lpVtbl[6]))((IDXGIOutput3*)Unsafe.AsPointer(ref this), riid, ppParent);
    }

    [return: NativeTypeName("HRESULT")]
    public int GetDesc(DXGI_OUTPUT_DESC* pDesc)
    {
        return ((delegate* unmanaged[Stdcall]<IDXGIOutput3*, DXGI_OUTPUT_DESC*, int>)(lpVtbl[7]))((IDXGIOutput3*)Unsafe.AsPointer(ref this), pDesc);
    }

    [return: NativeTypeName("HRESULT")]
    public int GetDisplayModeList(DXGI_FORMAT EnumFormat, uint Flags, uint* pNumModes, DXGI_MODE_DESC* pDesc)
    {
        return ((delegate* unmanaged[Stdcall]<IDXGIOutput3*, DXGI_FORMAT, uint, uint*, DXGI_MODE_DESC*, int>)(lpVtbl[8]))((IDXGIOutput3*)Unsafe.AsPointer(ref this), EnumFormat, Flags, pNumModes, pDesc);
    }

    [return: NativeTypeName("HRESULT")]
    public int FindClosestMatchingMode([NativeTypeName("const DXGI_MODE_DESC *")] DXGI_MODE_DESC* pModeToMatch, DXGI_MODE_DESC* pClosestMatch, IUnknown* pConcernedDevice)
    {
        return ((delegate* unmanaged[Stdcall]<IDXGIOutput3*, DXGI_MODE_DESC*, DXGI_MODE_DESC*, IUnknown*, int>)(lpVtbl[9]))((IDXGIOutput3*)Unsafe.AsPointer(ref this), pModeToMatch, pClosestMatch, pConcernedDevice);
    }

    [return: NativeTypeName("HRESULT")]
    public int WaitForVBlank()
    {
        return ((delegate* unmanaged[Stdcall]<IDXGIOutput3*, int>)(lpVtbl[10]))((IDXGIOutput3*)Unsafe.AsPointer(ref this));
    }

    [return: NativeTypeName("HRESULT")]
    public int TakeOwnership(IUnknown* pDevice, [NativeTypeName("BOOL")] int Exclusive)
    {
        return ((delegate* unmanaged[Stdcall]<IDXGIOutput3*, IUnknown*, int, int>)(lpVtbl[11]))((IDXGIOutput3*)Unsafe.AsPointer(ref this), pDevice, Exclusive);
    }

    public void ReleaseOwnership()
    {
        ((delegate* unmanaged[Stdcall]<IDXGIOutput3*, void>)(lpVtbl[12]))((IDXGIOutput3*)Unsafe.AsPointer(ref this));
    }

    [return: NativeTypeName("HRESULT")]
    public int GetGammaControlCapabilities(DXGI_GAMMA_CONTROL_CAPABILITIES* pGammaCaps)
    {
        return ((delegate* unmanaged[Stdcall]<IDXGIOutput3*, DXGI_GAMMA_CONTROL_CAPABILITIES*, int>)(lpVtbl[13]))((IDXGIOutput3*)Unsafe.AsPointer(ref this), pGammaCaps);
    }

    [return: NativeTypeName("HRESULT")]
    public int SetGammaControl([NativeTypeName("const DXGI_GAMMA_CONTROL *")] DXGI_GAMMA_CONTROL* pArray)
    {
        return ((delegate* unmanaged[Stdcall]<IDXGIOutput3*, DXGI_GAMMA_CONTROL*, int>)(lpVtbl[14]))((IDXGIOutput3*)Unsafe.AsPointer(ref this), pArray);
    }

    [return: NativeTypeName("HRESULT")]
    public int GetGammaControl(DXGI_GAMMA_CONTROL* pArray)
    {
        return ((delegate* unmanaged[Stdcall]<IDXGIOutput3*, DXGI_GAMMA_CONTROL*, int>)(lpVtbl[15]))((IDXGIOutput3*)Unsafe.AsPointer(ref this), pArray);
    }

    [return: NativeTypeName("HRESULT")]
    public int SetDisplaySurface(IDXGISurface* pScanoutSurface)
    {
        return ((delegate* unmanaged[Stdcall]<IDXGIOutput3*, IDXGISurface*, int>)(lpVtbl[16]))((IDXGIOutput3*)Unsafe.AsPointer(ref this), pScanoutSurface);
    }

    [return: NativeTypeName("HRESULT")]
    public int GetDisplaySurfaceData(IDXGISurface* pDestination)
    {
        return ((delegate* unmanaged[Stdcall]<IDXGIOutput3*, IDXGISurface*, int>)(lpVtbl[17]))((IDXGIOutput3*)Unsafe.AsPointer(ref this), pDestination);
    }

    [return: NativeTypeName("HRESULT")]
    public int GetFrameStatistics(DXGI_FRAME_STATISTICS* pStats)
    {
        return ((delegate* unmanaged[Stdcall]<IDXGIOutput3*, DXGI_FRAME_STATISTICS*, int>)(lpVtbl[18]))((IDXGIOutput3*)Unsafe.AsPointer(ref this), pStats);
    }

    [return: NativeTypeName("HRESULT")]
    public int GetDisplayModeList1(DXGI_FORMAT EnumFormat, uint Flags, uint* pNumModes, DXGI_MODE_DESC1* pDesc)
    {
        return ((delegate* unmanaged[Stdcall]<IDXGIOutput3*, DXGI_FORMAT, uint, uint*, DXGI_MODE_DESC1*, int>)(lpVtbl[19]))((IDXGIOutput3*)Unsafe.AsPointer(ref this), EnumFormat, Flags, pNumModes, pDesc);
    }

    [return: NativeTypeName("HRESULT")]
    public int FindClosestMatchingMode1([NativeTypeName("const DXGI_MODE_DESC1 *")] DXGI_MODE_DESC1* pModeToMatch, DXGI_MODE_DESC1* pClosestMatch, IUnknown* pConcernedDevice)
    {
        return ((delegate* unmanaged[Stdcall]<IDXGIOutput3*, DXGI_MODE_DESC1*, DXGI_MODE_DESC1*, IUnknown*, int>)(lpVtbl[20]))((IDXGIOutput3*)Unsafe.AsPointer(ref this), pModeToMatch, pClosestMatch, pConcernedDevice);
    }

    [return: NativeTypeName("HRESULT")]
    public int GetDisplaySurfaceData1(IDXGIResource* pDestination)
    {
        return ((delegate* unmanaged[Stdcall]<IDXGIOutput3*, IDXGIResource*, int>)(lpVtbl[21]))((IDXGIOutput3*)Unsafe.AsPointer(ref this), pDestination);
    }

    [return: NativeTypeName("HRESULT")]
    public int DuplicateOutput(IUnknown* pDevice, IDXGIOutputDuplication** ppOutputDuplication)
    {
        return ((delegate* unmanaged[Stdcall]<IDXGIOutput3*, IUnknown*, IDXGIOutputDuplication**, int>)(lpVtbl[22]))((IDXGIOutput3*)Unsafe.AsPointer(ref this), pDevice, ppOutputDuplication);
    }

    [return: NativeTypeName("BOOL")]
    public int SupportsOverlays()
    {
        return ((delegate* unmanaged[Stdcall]<IDXGIOutput3*, int>)(lpVtbl[23]))((IDXGIOutput3*)Unsafe.AsPointer(ref this));
    }

    [return: NativeTypeName("HRESULT")]
    public int CheckOverlaySupport(DXGI_FORMAT EnumFormat, IUnknown* pConcernedDevice, uint* pFlags)
    {
        return ((delegate* unmanaged[Stdcall]<IDXGIOutput3*, DXGI_FORMAT, IUnknown*, uint*, int>)(lpVtbl[24]))((IDXGIOutput3*)Unsafe.AsPointer(ref this), EnumFormat, pConcernedDevice, pFlags);
    }

    public interface Interface : IDXGIOutput2.Interface
    {
        [return: NativeTypeName("HRESULT")]
        int CheckOverlaySupport(DXGI_FORMAT EnumFormat, IUnknown* pConcernedDevice, uint* pFlags);
    }
}

internal enum DXGI_SWAP_CHAIN_COLOR_SPACE_SUPPORT_FLAG
{
    DXGI_SWAP_CHAIN_COLOR_SPACE_SUPPORT_FLAG_PRESENT = 0x1,
    DXGI_SWAP_CHAIN_COLOR_SPACE_SUPPORT_FLAG_OVERLAY_PRESENT = 0x2,
}

[Guid("94D99BDB-F1F8-4AB0-B236-7DA0170EDAB1")]
[NativeTypeName("struct IDXGISwapChain3 : IDXGISwapChain2")]
internal unsafe partial struct IDXGISwapChain3 : IDXGISwapChain3.Interface, INativeGuid
{
    static Guid* INativeGuid.NativeGuid => (Guid*)Unsafe.AsPointer(in IID_IDXGISwapChain3);

    public void** lpVtbl;

    [return: NativeTypeName("HRESULT")]
    public int QueryInterface([NativeTypeName("const IID &")] Guid* riid, void** ppvObject)
    {
        return ((delegate* unmanaged[Stdcall]<IDXGISwapChain3*, Guid*, void**, int>)(lpVtbl[0]))((IDXGISwapChain3*)Unsafe.AsPointer(ref this), riid, ppvObject);
    }

    [return: NativeTypeName("ULONG")]
    public uint AddRef()
    {
        return ((delegate* unmanaged[Stdcall]<IDXGISwapChain3*, uint>)(lpVtbl[1]))((IDXGISwapChain3*)Unsafe.AsPointer(ref this));
    }

    [return: NativeTypeName("ULONG")]
    public uint Release()
    {
        return ((delegate* unmanaged[Stdcall]<IDXGISwapChain3*, uint>)(lpVtbl[2]))((IDXGISwapChain3*)Unsafe.AsPointer(ref this));
    }

    [return: NativeTypeName("HRESULT")]
    public int SetPrivateData([NativeTypeName("const GUID &")] Guid* Name, uint DataSize, [NativeTypeName("const void *")] void* pData)
    {
        return ((delegate* unmanaged[Stdcall]<IDXGISwapChain3*, Guid*, uint, void*, int>)(lpVtbl[3]))((IDXGISwapChain3*)Unsafe.AsPointer(ref this), Name, DataSize, pData);
    }

    [return: NativeTypeName("HRESULT")]
    public int SetPrivateDataInterface([NativeTypeName("const GUID &")] Guid* Name, [NativeTypeName("const IUnknown *")] IUnknown* pUnknown)
    {
        return ((delegate* unmanaged[Stdcall]<IDXGISwapChain3*, Guid*, IUnknown*, int>)(lpVtbl[4]))((IDXGISwapChain3*)Unsafe.AsPointer(ref this), Name, pUnknown);
    }

    [return: NativeTypeName("HRESULT")]
    public int GetPrivateData([NativeTypeName("const GUID &")] Guid* Name, uint* pDataSize, void* pData)
    {
        return ((delegate* unmanaged[Stdcall]<IDXGISwapChain3*, Guid*, uint*, void*, int>)(lpVtbl[5]))((IDXGISwapChain3*)Unsafe.AsPointer(ref this), Name, pDataSize, pData);
    }

    [return: NativeTypeName("HRESULT")]
    public int GetParent([NativeTypeName("const IID &")] Guid* riid, void** ppParent)
    {
        return ((delegate* unmanaged[Stdcall]<IDXGISwapChain3*, Guid*, void**, int>)(lpVtbl[6]))((IDXGISwapChain3*)Unsafe.AsPointer(ref this), riid, ppParent);
    }

    [return: NativeTypeName("HRESULT")]
    public int GetDevice([NativeTypeName("const IID &")] Guid* riid, void** ppDevice)
    {
        return ((delegate* unmanaged[Stdcall]<IDXGISwapChain3*, Guid*, void**, int>)(lpVtbl[7]))((IDXGISwapChain3*)Unsafe.AsPointer(ref this), riid, ppDevice);
    }

    [return: NativeTypeName("HRESULT")]
    public int Present(uint SyncInterval, uint Flags)
    {
        return ((delegate* unmanaged[Stdcall]<IDXGISwapChain3*, uint, uint, int>)(lpVtbl[8]))((IDXGISwapChain3*)Unsafe.AsPointer(ref this), SyncInterval, Flags);
    }

    [return: NativeTypeName("HRESULT")]
    public int GetBuffer(uint Buffer, [NativeTypeName("const IID &")] Guid* riid, void** ppSurface)
    {
        return ((delegate* unmanaged[Stdcall]<IDXGISwapChain3*, uint, Guid*, void**, int>)(lpVtbl[9]))((IDXGISwapChain3*)Unsafe.AsPointer(ref this), Buffer, riid, ppSurface);
    }

    [return: NativeTypeName("HRESULT")]
    public int SetFullscreenState([NativeTypeName("BOOL")] int Fullscreen, IDXGIOutput* pTarget)
    {
        return ((delegate* unmanaged[Stdcall]<IDXGISwapChain3*, int, IDXGIOutput*, int>)(lpVtbl[10]))((IDXGISwapChain3*)Unsafe.AsPointer(ref this), Fullscreen, pTarget);
    }

    [return: NativeTypeName("HRESULT")]
    public int GetFullscreenState([NativeTypeName("BOOL *")] int* pFullscreen, IDXGIOutput** ppTarget)
    {
        return ((delegate* unmanaged[Stdcall]<IDXGISwapChain3*, int*, IDXGIOutput**, int>)(lpVtbl[11]))((IDXGISwapChain3*)Unsafe.AsPointer(ref this), pFullscreen, ppTarget);
    }

    [return: NativeTypeName("HRESULT")]
    public int GetDesc(DXGI_SWAP_CHAIN_DESC* pDesc)
    {
        return ((delegate* unmanaged[Stdcall]<IDXGISwapChain3*, DXGI_SWAP_CHAIN_DESC*, int>)(lpVtbl[12]))((IDXGISwapChain3*)Unsafe.AsPointer(ref this), pDesc);
    }

    [return: NativeTypeName("HRESULT")]
    public int ResizeBuffers(uint BufferCount, uint Width, uint Height, DXGI_FORMAT NewFormat, uint SwapChainFlags)
    {
        return ((delegate* unmanaged[Stdcall]<IDXGISwapChain3*, uint, uint, uint, DXGI_FORMAT, uint, int>)(lpVtbl[13]))((IDXGISwapChain3*)Unsafe.AsPointer(ref this), BufferCount, Width, Height, NewFormat, SwapChainFlags);
    }

    [return: NativeTypeName("HRESULT")]
    public int ResizeTarget([NativeTypeName("const DXGI_MODE_DESC *")] DXGI_MODE_DESC* pNewTargetParameters)
    {
        return ((delegate* unmanaged[Stdcall]<IDXGISwapChain3*, DXGI_MODE_DESC*, int>)(lpVtbl[14]))((IDXGISwapChain3*)Unsafe.AsPointer(ref this), pNewTargetParameters);
    }

    [return: NativeTypeName("HRESULT")]
    public int GetContainingOutput(IDXGIOutput** ppOutput)
    {
        return ((delegate* unmanaged[Stdcall]<IDXGISwapChain3*, IDXGIOutput**, int>)(lpVtbl[15]))((IDXGISwapChain3*)Unsafe.AsPointer(ref this), ppOutput);
    }

    [return: NativeTypeName("HRESULT")]
    public int GetFrameStatistics(DXGI_FRAME_STATISTICS* pStats)
    {
        return ((delegate* unmanaged[Stdcall]<IDXGISwapChain3*, DXGI_FRAME_STATISTICS*, int>)(lpVtbl[16]))((IDXGISwapChain3*)Unsafe.AsPointer(ref this), pStats);
    }

    [return: NativeTypeName("HRESULT")]
    public int GetLastPresentCount(uint* pLastPresentCount)
    {
        return ((delegate* unmanaged[Stdcall]<IDXGISwapChain3*, uint*, int>)(lpVtbl[17]))((IDXGISwapChain3*)Unsafe.AsPointer(ref this), pLastPresentCount);
    }

    [return: NativeTypeName("HRESULT")]
    public int GetDesc1(DXGI_SWAP_CHAIN_DESC1* pDesc)
    {
        return ((delegate* unmanaged[Stdcall]<IDXGISwapChain3*, DXGI_SWAP_CHAIN_DESC1*, int>)(lpVtbl[18]))((IDXGISwapChain3*)Unsafe.AsPointer(ref this), pDesc);
    }

    [return: NativeTypeName("HRESULT")]
    public int GetFullscreenDesc(DXGI_SWAP_CHAIN_FULLSCREEN_DESC* pDesc)
    {
        return ((delegate* unmanaged[Stdcall]<IDXGISwapChain3*, DXGI_SWAP_CHAIN_FULLSCREEN_DESC*, int>)(lpVtbl[19]))((IDXGISwapChain3*)Unsafe.AsPointer(ref this), pDesc);
    }

    [return: NativeTypeName("HRESULT")]
    public int GetHwnd([NativeTypeName("HWND *")] HWND__** pHwnd)
    {
        return ((delegate* unmanaged[Stdcall]<IDXGISwapChain3*, HWND__**, int>)(lpVtbl[20]))((IDXGISwapChain3*)Unsafe.AsPointer(ref this), pHwnd);
    }

    [return: NativeTypeName("HRESULT")]
    public int GetCoreWindow([NativeTypeName("const IID &")] Guid* refiid, void** ppUnk)
    {
        return ((delegate* unmanaged[Stdcall]<IDXGISwapChain3*, Guid*, void**, int>)(lpVtbl[21]))((IDXGISwapChain3*)Unsafe.AsPointer(ref this), refiid, ppUnk);
    }

    [return: NativeTypeName("HRESULT")]
    public int Present1(uint SyncInterval, uint PresentFlags, [NativeTypeName("const DXGI_PRESENT_PARAMETERS *")] DXGI_PRESENT_PARAMETERS* pPresentParameters)
    {
        return ((delegate* unmanaged[Stdcall]<IDXGISwapChain3*, uint, uint, DXGI_PRESENT_PARAMETERS*, int>)(lpVtbl[22]))((IDXGISwapChain3*)Unsafe.AsPointer(ref this), SyncInterval, PresentFlags, pPresentParameters);
    }

    [return: NativeTypeName("BOOL")]
    public int IsTemporaryMonoSupported()
    {
        return ((delegate* unmanaged[Stdcall]<IDXGISwapChain3*, int>)(lpVtbl[23]))((IDXGISwapChain3*)Unsafe.AsPointer(ref this));
    }

    [return: NativeTypeName("HRESULT")]
    public int GetRestrictToOutput(IDXGIOutput** ppRestrictToOutput)
    {
        return ((delegate* unmanaged[Stdcall]<IDXGISwapChain3*, IDXGIOutput**, int>)(lpVtbl[24]))((IDXGISwapChain3*)Unsafe.AsPointer(ref this), ppRestrictToOutput);
    }

    [return: NativeTypeName("HRESULT")]
    public int SetBackgroundColor([NativeTypeName("const DXGI_RGBA *")] D3DCOLORVALUE* pColor)
    {
        return ((delegate* unmanaged[Stdcall]<IDXGISwapChain3*, D3DCOLORVALUE*, int>)(lpVtbl[25]))((IDXGISwapChain3*)Unsafe.AsPointer(ref this), pColor);
    }

    [return: NativeTypeName("HRESULT")]
    public int GetBackgroundColor([NativeTypeName("DXGI_RGBA *")] D3DCOLORVALUE* pColor)
    {
        return ((delegate* unmanaged[Stdcall]<IDXGISwapChain3*, D3DCOLORVALUE*, int>)(lpVtbl[26]))((IDXGISwapChain3*)Unsafe.AsPointer(ref this), pColor);
    }

    [return: NativeTypeName("HRESULT")]
    public int SetRotation(DXGI_MODE_ROTATION Rotation)
    {
        return ((delegate* unmanaged[Stdcall]<IDXGISwapChain3*, DXGI_MODE_ROTATION, int>)(lpVtbl[27]))((IDXGISwapChain3*)Unsafe.AsPointer(ref this), Rotation);
    }

    [return: NativeTypeName("HRESULT")]
    public int GetRotation(DXGI_MODE_ROTATION* pRotation)
    {
        return ((delegate* unmanaged[Stdcall]<IDXGISwapChain3*, DXGI_MODE_ROTATION*, int>)(lpVtbl[28]))((IDXGISwapChain3*)Unsafe.AsPointer(ref this), pRotation);
    }

    [return: NativeTypeName("HRESULT")]
    public int SetSourceSize(uint Width, uint Height)
    {
        return ((delegate* unmanaged[Stdcall]<IDXGISwapChain3*, uint, uint, int>)(lpVtbl[29]))((IDXGISwapChain3*)Unsafe.AsPointer(ref this), Width, Height);
    }

    [return: NativeTypeName("HRESULT")]
    public int GetSourceSize(uint* pWidth, uint* pHeight)
    {
        return ((delegate* unmanaged[Stdcall]<IDXGISwapChain3*, uint*, uint*, int>)(lpVtbl[30]))((IDXGISwapChain3*)Unsafe.AsPointer(ref this), pWidth, pHeight);
    }

    [return: NativeTypeName("HRESULT")]
    public int SetMaximumFrameLatency(uint MaxLatency)
    {
        return ((delegate* unmanaged[Stdcall]<IDXGISwapChain3*, uint, int>)(lpVtbl[31]))((IDXGISwapChain3*)Unsafe.AsPointer(ref this), MaxLatency);
    }

    [return: NativeTypeName("HRESULT")]
    public int GetMaximumFrameLatency(uint* pMaxLatency)
    {
        return ((delegate* unmanaged[Stdcall]<IDXGISwapChain3*, uint*, int>)(lpVtbl[32]))((IDXGISwapChain3*)Unsafe.AsPointer(ref this), pMaxLatency);
    }

    [return: NativeTypeName("HANDLE")]
    public void* GetFrameLatencyWaitableObject()
    {
        return ((delegate* unmanaged[Stdcall]<IDXGISwapChain3*, void*>)(lpVtbl[33]))((IDXGISwapChain3*)Unsafe.AsPointer(ref this));
    }

    [return: NativeTypeName("HRESULT")]
    public int SetMatrixTransform([NativeTypeName("const DXGI_MATRIX_3X2_F *")] DXGI_MATRIX_3X2_F* pMatrix)
    {
        return ((delegate* unmanaged[Stdcall]<IDXGISwapChain3*, DXGI_MATRIX_3X2_F*, int>)(lpVtbl[34]))((IDXGISwapChain3*)Unsafe.AsPointer(ref this), pMatrix);
    }

    [return: NativeTypeName("HRESULT")]
    public int GetMatrixTransform(DXGI_MATRIX_3X2_F* pMatrix)
    {
        return ((delegate* unmanaged[Stdcall]<IDXGISwapChain3*, DXGI_MATRIX_3X2_F*, int>)(lpVtbl[35]))((IDXGISwapChain3*)Unsafe.AsPointer(ref this), pMatrix);
    }

    public uint GetCurrentBackBufferIndex()
    {
        return ((delegate* unmanaged[Stdcall]<IDXGISwapChain3*, uint>)(lpVtbl[36]))((IDXGISwapChain3*)Unsafe.AsPointer(ref this));
    }

    [return: NativeTypeName("HRESULT")]
    public int CheckColorSpaceSupport(DXGI_COLOR_SPACE_TYPE ColorSpace, uint* pColorSpaceSupport)
    {
        return ((delegate* unmanaged[Stdcall]<IDXGISwapChain3*, DXGI_COLOR_SPACE_TYPE, uint*, int>)(lpVtbl[37]))((IDXGISwapChain3*)Unsafe.AsPointer(ref this), ColorSpace, pColorSpaceSupport);
    }

    [return: NativeTypeName("HRESULT")]
    public int SetColorSpace1(DXGI_COLOR_SPACE_TYPE ColorSpace)
    {
        return ((delegate* unmanaged[Stdcall]<IDXGISwapChain3*, DXGI_COLOR_SPACE_TYPE, int>)(lpVtbl[38]))((IDXGISwapChain3*)Unsafe.AsPointer(ref this), ColorSpace);
    }

    [return: NativeTypeName("HRESULT")]
    public int ResizeBuffers1(uint BufferCount, uint Width, uint Height, DXGI_FORMAT Format, uint SwapChainFlags, [NativeTypeName("const UINT *")] uint* pCreationNodeMask, [NativeTypeName("IUnknown *const *")] IUnknown** ppPresentQueue)
    {
        return ((delegate* unmanaged[Stdcall]<IDXGISwapChain3*, uint, uint, uint, DXGI_FORMAT, uint, uint*, IUnknown**, int>)(lpVtbl[39]))((IDXGISwapChain3*)Unsafe.AsPointer(ref this), BufferCount, Width, Height, Format, SwapChainFlags, pCreationNodeMask, ppPresentQueue);
    }

    public interface Interface : IDXGISwapChain2.Interface
    {
        uint GetCurrentBackBufferIndex();

        [return: NativeTypeName("HRESULT")]
        int CheckColorSpaceSupport(DXGI_COLOR_SPACE_TYPE ColorSpace, uint* pColorSpaceSupport);

        [return: NativeTypeName("HRESULT")]
        int SetColorSpace1(DXGI_COLOR_SPACE_TYPE ColorSpace);

        [return: NativeTypeName("HRESULT")]
        int ResizeBuffers1(uint BufferCount, uint Width, uint Height, DXGI_FORMAT Format, uint SwapChainFlags, [NativeTypeName("const UINT *")] uint* pCreationNodeMask, [NativeTypeName("IUnknown *const *")] IUnknown** ppPresentQueue);
    }
}

internal enum DXGI_OVERLAY_COLOR_SPACE_SUPPORT_FLAG
{
    DXGI_OVERLAY_COLOR_SPACE_SUPPORT_FLAG_PRESENT = 0x1,
}

[Guid("DC7DCA35-2196-414D-9F53-617884032A60")]
[NativeTypeName("struct IDXGIOutput4 : IDXGIOutput3")]
internal unsafe partial struct IDXGIOutput4 : IDXGIOutput4.Interface, INativeGuid
{
    static Guid* INativeGuid.NativeGuid => (Guid*)Unsafe.AsPointer(in IID_IDXGIOutput4);

    public void** lpVtbl;

    [return: NativeTypeName("HRESULT")]
    public int QueryInterface([NativeTypeName("const IID &")] Guid* riid, void** ppvObject)
    {
        return ((delegate* unmanaged[Stdcall]<IDXGIOutput4*, Guid*, void**, int>)(lpVtbl[0]))((IDXGIOutput4*)Unsafe.AsPointer(ref this), riid, ppvObject);
    }

    [return: NativeTypeName("ULONG")]
    public uint AddRef()
    {
        return ((delegate* unmanaged[Stdcall]<IDXGIOutput4*, uint>)(lpVtbl[1]))((IDXGIOutput4*)Unsafe.AsPointer(ref this));
    }

    [return: NativeTypeName("ULONG")]
    public uint Release()
    {
        return ((delegate* unmanaged[Stdcall]<IDXGIOutput4*, uint>)(lpVtbl[2]))((IDXGIOutput4*)Unsafe.AsPointer(ref this));
    }

    [return: NativeTypeName("HRESULT")]
    public int SetPrivateData([NativeTypeName("const GUID &")] Guid* Name, uint DataSize, [NativeTypeName("const void *")] void* pData)
    {
        return ((delegate* unmanaged[Stdcall]<IDXGIOutput4*, Guid*, uint, void*, int>)(lpVtbl[3]))((IDXGIOutput4*)Unsafe.AsPointer(ref this), Name, DataSize, pData);
    }

    [return: NativeTypeName("HRESULT")]
    public int SetPrivateDataInterface([NativeTypeName("const GUID &")] Guid* Name, [NativeTypeName("const IUnknown *")] IUnknown* pUnknown)
    {
        return ((delegate* unmanaged[Stdcall]<IDXGIOutput4*, Guid*, IUnknown*, int>)(lpVtbl[4]))((IDXGIOutput4*)Unsafe.AsPointer(ref this), Name, pUnknown);
    }

    [return: NativeTypeName("HRESULT")]
    public int GetPrivateData([NativeTypeName("const GUID &")] Guid* Name, uint* pDataSize, void* pData)
    {
        return ((delegate* unmanaged[Stdcall]<IDXGIOutput4*, Guid*, uint*, void*, int>)(lpVtbl[5]))((IDXGIOutput4*)Unsafe.AsPointer(ref this), Name, pDataSize, pData);
    }

    [return: NativeTypeName("HRESULT")]
    public int GetParent([NativeTypeName("const IID &")] Guid* riid, void** ppParent)
    {
        return ((delegate* unmanaged[Stdcall]<IDXGIOutput4*, Guid*, void**, int>)(lpVtbl[6]))((IDXGIOutput4*)Unsafe.AsPointer(ref this), riid, ppParent);
    }

    [return: NativeTypeName("HRESULT")]
    public int GetDesc(DXGI_OUTPUT_DESC* pDesc)
    {
        return ((delegate* unmanaged[Stdcall]<IDXGIOutput4*, DXGI_OUTPUT_DESC*, int>)(lpVtbl[7]))((IDXGIOutput4*)Unsafe.AsPointer(ref this), pDesc);
    }

    [return: NativeTypeName("HRESULT")]
    public int GetDisplayModeList(DXGI_FORMAT EnumFormat, uint Flags, uint* pNumModes, DXGI_MODE_DESC* pDesc)
    {
        return ((delegate* unmanaged[Stdcall]<IDXGIOutput4*, DXGI_FORMAT, uint, uint*, DXGI_MODE_DESC*, int>)(lpVtbl[8]))((IDXGIOutput4*)Unsafe.AsPointer(ref this), EnumFormat, Flags, pNumModes, pDesc);
    }

    [return: NativeTypeName("HRESULT")]
    public int FindClosestMatchingMode([NativeTypeName("const DXGI_MODE_DESC *")] DXGI_MODE_DESC* pModeToMatch, DXGI_MODE_DESC* pClosestMatch, IUnknown* pConcernedDevice)
    {
        return ((delegate* unmanaged[Stdcall]<IDXGIOutput4*, DXGI_MODE_DESC*, DXGI_MODE_DESC*, IUnknown*, int>)(lpVtbl[9]))((IDXGIOutput4*)Unsafe.AsPointer(ref this), pModeToMatch, pClosestMatch, pConcernedDevice);
    }

    [return: NativeTypeName("HRESULT")]
    public int WaitForVBlank()
    {
        return ((delegate* unmanaged[Stdcall]<IDXGIOutput4*, int>)(lpVtbl[10]))((IDXGIOutput4*)Unsafe.AsPointer(ref this));
    }

    [return: NativeTypeName("HRESULT")]
    public int TakeOwnership(IUnknown* pDevice, [NativeTypeName("BOOL")] int Exclusive)
    {
        return ((delegate* unmanaged[Stdcall]<IDXGIOutput4*, IUnknown*, int, int>)(lpVtbl[11]))((IDXGIOutput4*)Unsafe.AsPointer(ref this), pDevice, Exclusive);
    }

    public void ReleaseOwnership()
    {
        ((delegate* unmanaged[Stdcall]<IDXGIOutput4*, void>)(lpVtbl[12]))((IDXGIOutput4*)Unsafe.AsPointer(ref this));
    }

    [return: NativeTypeName("HRESULT")]
    public int GetGammaControlCapabilities(DXGI_GAMMA_CONTROL_CAPABILITIES* pGammaCaps)
    {
        return ((delegate* unmanaged[Stdcall]<IDXGIOutput4*, DXGI_GAMMA_CONTROL_CAPABILITIES*, int>)(lpVtbl[13]))((IDXGIOutput4*)Unsafe.AsPointer(ref this), pGammaCaps);
    }

    [return: NativeTypeName("HRESULT")]
    public int SetGammaControl([NativeTypeName("const DXGI_GAMMA_CONTROL *")] DXGI_GAMMA_CONTROL* pArray)
    {
        return ((delegate* unmanaged[Stdcall]<IDXGIOutput4*, DXGI_GAMMA_CONTROL*, int>)(lpVtbl[14]))((IDXGIOutput4*)Unsafe.AsPointer(ref this), pArray);
    }

    [return: NativeTypeName("HRESULT")]
    public int GetGammaControl(DXGI_GAMMA_CONTROL* pArray)
    {
        return ((delegate* unmanaged[Stdcall]<IDXGIOutput4*, DXGI_GAMMA_CONTROL*, int>)(lpVtbl[15]))((IDXGIOutput4*)Unsafe.AsPointer(ref this), pArray);
    }

    [return: NativeTypeName("HRESULT")]
    public int SetDisplaySurface(IDXGISurface* pScanoutSurface)
    {
        return ((delegate* unmanaged[Stdcall]<IDXGIOutput4*, IDXGISurface*, int>)(lpVtbl[16]))((IDXGIOutput4*)Unsafe.AsPointer(ref this), pScanoutSurface);
    }

    [return: NativeTypeName("HRESULT")]
    public int GetDisplaySurfaceData(IDXGISurface* pDestination)
    {
        return ((delegate* unmanaged[Stdcall]<IDXGIOutput4*, IDXGISurface*, int>)(lpVtbl[17]))((IDXGIOutput4*)Unsafe.AsPointer(ref this), pDestination);
    }

    [return: NativeTypeName("HRESULT")]
    public int GetFrameStatistics(DXGI_FRAME_STATISTICS* pStats)
    {
        return ((delegate* unmanaged[Stdcall]<IDXGIOutput4*, DXGI_FRAME_STATISTICS*, int>)(lpVtbl[18]))((IDXGIOutput4*)Unsafe.AsPointer(ref this), pStats);
    }

    [return: NativeTypeName("HRESULT")]
    public int GetDisplayModeList1(DXGI_FORMAT EnumFormat, uint Flags, uint* pNumModes, DXGI_MODE_DESC1* pDesc)
    {
        return ((delegate* unmanaged[Stdcall]<IDXGIOutput4*, DXGI_FORMAT, uint, uint*, DXGI_MODE_DESC1*, int>)(lpVtbl[19]))((IDXGIOutput4*)Unsafe.AsPointer(ref this), EnumFormat, Flags, pNumModes, pDesc);
    }

    [return: NativeTypeName("HRESULT")]
    public int FindClosestMatchingMode1([NativeTypeName("const DXGI_MODE_DESC1 *")] DXGI_MODE_DESC1* pModeToMatch, DXGI_MODE_DESC1* pClosestMatch, IUnknown* pConcernedDevice)
    {
        return ((delegate* unmanaged[Stdcall]<IDXGIOutput4*, DXGI_MODE_DESC1*, DXGI_MODE_DESC1*, IUnknown*, int>)(lpVtbl[20]))((IDXGIOutput4*)Unsafe.AsPointer(ref this), pModeToMatch, pClosestMatch, pConcernedDevice);
    }

    [return: NativeTypeName("HRESULT")]
    public int GetDisplaySurfaceData1(IDXGIResource* pDestination)
    {
        return ((delegate* unmanaged[Stdcall]<IDXGIOutput4*, IDXGIResource*, int>)(lpVtbl[21]))((IDXGIOutput4*)Unsafe.AsPointer(ref this), pDestination);
    }

    [return: NativeTypeName("HRESULT")]
    public int DuplicateOutput(IUnknown* pDevice, IDXGIOutputDuplication** ppOutputDuplication)
    {
        return ((delegate* unmanaged[Stdcall]<IDXGIOutput4*, IUnknown*, IDXGIOutputDuplication**, int>)(lpVtbl[22]))((IDXGIOutput4*)Unsafe.AsPointer(ref this), pDevice, ppOutputDuplication);
    }

    [return: NativeTypeName("BOOL")]
    public int SupportsOverlays()
    {
        return ((delegate* unmanaged[Stdcall]<IDXGIOutput4*, int>)(lpVtbl[23]))((IDXGIOutput4*)Unsafe.AsPointer(ref this));
    }

    [return: NativeTypeName("HRESULT")]
    public int CheckOverlaySupport(DXGI_FORMAT EnumFormat, IUnknown* pConcernedDevice, uint* pFlags)
    {
        return ((delegate* unmanaged[Stdcall]<IDXGIOutput4*, DXGI_FORMAT, IUnknown*, uint*, int>)(lpVtbl[24]))((IDXGIOutput4*)Unsafe.AsPointer(ref this), EnumFormat, pConcernedDevice, pFlags);
    }

    [return: NativeTypeName("HRESULT")]
    public int CheckOverlayColorSpaceSupport(DXGI_FORMAT Format, DXGI_COLOR_SPACE_TYPE ColorSpace, IUnknown* pConcernedDevice, uint* pFlags)
    {
        return ((delegate* unmanaged[Stdcall]<IDXGIOutput4*, DXGI_FORMAT, DXGI_COLOR_SPACE_TYPE, IUnknown*, uint*, int>)(lpVtbl[25]))((IDXGIOutput4*)Unsafe.AsPointer(ref this), Format, ColorSpace, pConcernedDevice, pFlags);
    }

    public interface Interface : IDXGIOutput3.Interface
    {
        [return: NativeTypeName("HRESULT")]
        int CheckOverlayColorSpaceSupport(DXGI_FORMAT Format, DXGI_COLOR_SPACE_TYPE ColorSpace, IUnknown* pConcernedDevice, uint* pFlags);
    }
}

[Guid("1BC6EA02-EF36-464F-BF0C-21CA39E5168A")]
[NativeTypeName("struct IDXGIFactory4 : IDXGIFactory3")]
internal unsafe partial struct IDXGIFactory4 : IDXGIFactory4.Interface, INativeGuid
{
    static Guid* INativeGuid.NativeGuid => (Guid*)Unsafe.AsPointer(in IID_IDXGIFactory4);

    public void** lpVtbl;

    [return: NativeTypeName("HRESULT")]
    public int QueryInterface([NativeTypeName("const IID &")] Guid* riid, void** ppvObject)
    {
        return ((delegate* unmanaged[Stdcall]<IDXGIFactory4*, Guid*, void**, int>)(lpVtbl[0]))((IDXGIFactory4*)Unsafe.AsPointer(ref this), riid, ppvObject);
    }

    [return: NativeTypeName("ULONG")]
    public uint AddRef()
    {
        return ((delegate* unmanaged[Stdcall]<IDXGIFactory4*, uint>)(lpVtbl[1]))((IDXGIFactory4*)Unsafe.AsPointer(ref this));
    }

    [return: NativeTypeName("ULONG")]
    public uint Release()
    {
        return ((delegate* unmanaged[Stdcall]<IDXGIFactory4*, uint>)(lpVtbl[2]))((IDXGIFactory4*)Unsafe.AsPointer(ref this));
    }

    [return: NativeTypeName("HRESULT")]
    public int SetPrivateData([NativeTypeName("const GUID &")] Guid* Name, uint DataSize, [NativeTypeName("const void *")] void* pData)
    {
        return ((delegate* unmanaged[Stdcall]<IDXGIFactory4*, Guid*, uint, void*, int>)(lpVtbl[3]))((IDXGIFactory4*)Unsafe.AsPointer(ref this), Name, DataSize, pData);
    }

    [return: NativeTypeName("HRESULT")]
    public int SetPrivateDataInterface([NativeTypeName("const GUID &")] Guid* Name, [NativeTypeName("const IUnknown *")] IUnknown* pUnknown)
    {
        return ((delegate* unmanaged[Stdcall]<IDXGIFactory4*, Guid*, IUnknown*, int>)(lpVtbl[4]))((IDXGIFactory4*)Unsafe.AsPointer(ref this), Name, pUnknown);
    }

    [return: NativeTypeName("HRESULT")]
    public int GetPrivateData([NativeTypeName("const GUID &")] Guid* Name, uint* pDataSize, void* pData)
    {
        return ((delegate* unmanaged[Stdcall]<IDXGIFactory4*, Guid*, uint*, void*, int>)(lpVtbl[5]))((IDXGIFactory4*)Unsafe.AsPointer(ref this), Name, pDataSize, pData);
    }

    [return: NativeTypeName("HRESULT")]
    public int GetParent([NativeTypeName("const IID &")] Guid* riid, void** ppParent)
    {
        return ((delegate* unmanaged[Stdcall]<IDXGIFactory4*, Guid*, void**, int>)(lpVtbl[6]))((IDXGIFactory4*)Unsafe.AsPointer(ref this), riid, ppParent);
    }

    [return: NativeTypeName("HRESULT")]
    public int EnumAdapters(uint Adapter, IDXGIAdapter** ppAdapter)
    {
        return ((delegate* unmanaged[Stdcall]<IDXGIFactory4*, uint, IDXGIAdapter**, int>)(lpVtbl[7]))((IDXGIFactory4*)Unsafe.AsPointer(ref this), Adapter, ppAdapter);
    }

    [return: NativeTypeName("HRESULT")]
    public int MakeWindowAssociation([NativeTypeName("HWND")] HWND__* WindowHandle, uint Flags)
    {
        return ((delegate* unmanaged[Stdcall]<IDXGIFactory4*, HWND__*, uint, int>)(lpVtbl[8]))((IDXGIFactory4*)Unsafe.AsPointer(ref this), WindowHandle, Flags);
    }

    [return: NativeTypeName("HRESULT")]
    public int GetWindowAssociation([NativeTypeName("HWND *")] HWND__** pWindowHandle)
    {
        return ((delegate* unmanaged[Stdcall]<IDXGIFactory4*, HWND__**, int>)(lpVtbl[9]))((IDXGIFactory4*)Unsafe.AsPointer(ref this), pWindowHandle);
    }

    [return: NativeTypeName("HRESULT")]
    public int CreateSwapChain(IUnknown* pDevice, DXGI_SWAP_CHAIN_DESC* pDesc, IDXGISwapChain** ppSwapChain)
    {
        return ((delegate* unmanaged[Stdcall]<IDXGIFactory4*, IUnknown*, DXGI_SWAP_CHAIN_DESC*, IDXGISwapChain**, int>)(lpVtbl[10]))((IDXGIFactory4*)Unsafe.AsPointer(ref this), pDevice, pDesc, ppSwapChain);
    }

    [return: NativeTypeName("HRESULT")]
    public int CreateSoftwareAdapter([NativeTypeName("HMODULE")] HINSTANCE__* Module, IDXGIAdapter** ppAdapter)
    {
        return ((delegate* unmanaged[Stdcall]<IDXGIFactory4*, HINSTANCE__*, IDXGIAdapter**, int>)(lpVtbl[11]))((IDXGIFactory4*)Unsafe.AsPointer(ref this), Module, ppAdapter);
    }

    [return: NativeTypeName("HRESULT")]
    public int EnumAdapters1(uint Adapter, IDXGIAdapter1** ppAdapter)
    {
        return ((delegate* unmanaged[Stdcall]<IDXGIFactory4*, uint, IDXGIAdapter1**, int>)(lpVtbl[12]))((IDXGIFactory4*)Unsafe.AsPointer(ref this), Adapter, ppAdapter);
    }

    [return: NativeTypeName("BOOL")]
    public int IsCurrent()
    {
        return ((delegate* unmanaged[Stdcall]<IDXGIFactory4*, int>)(lpVtbl[13]))((IDXGIFactory4*)Unsafe.AsPointer(ref this));
    }

    [return: NativeTypeName("BOOL")]
    public int IsWindowedStereoEnabled()
    {
        return ((delegate* unmanaged[Stdcall]<IDXGIFactory4*, int>)(lpVtbl[14]))((IDXGIFactory4*)Unsafe.AsPointer(ref this));
    }

    [return: NativeTypeName("HRESULT")]
    public int CreateSwapChainForHwnd(IUnknown* pDevice, [NativeTypeName("HWND")] HWND__* hWnd, [NativeTypeName("const DXGI_SWAP_CHAIN_DESC1 *")] DXGI_SWAP_CHAIN_DESC1* pDesc, [NativeTypeName("const DXGI_SWAP_CHAIN_FULLSCREEN_DESC *")] DXGI_SWAP_CHAIN_FULLSCREEN_DESC* pFullscreenDesc, IDXGIOutput* pRestrictToOutput, IDXGISwapChain1** ppSwapChain)
    {
        return ((delegate* unmanaged[Stdcall]<IDXGIFactory4*, IUnknown*, HWND__*, DXGI_SWAP_CHAIN_DESC1*, DXGI_SWAP_CHAIN_FULLSCREEN_DESC*, IDXGIOutput*, IDXGISwapChain1**, int>)(lpVtbl[15]))((IDXGIFactory4*)Unsafe.AsPointer(ref this), pDevice, hWnd, pDesc, pFullscreenDesc, pRestrictToOutput, ppSwapChain);
    }

    [return: NativeTypeName("HRESULT")]
    public int CreateSwapChainForCoreWindow(IUnknown* pDevice, IUnknown* pWindow, [NativeTypeName("const DXGI_SWAP_CHAIN_DESC1 *")] DXGI_SWAP_CHAIN_DESC1* pDesc, IDXGIOutput* pRestrictToOutput, IDXGISwapChain1** ppSwapChain)
    {
        return ((delegate* unmanaged[Stdcall]<IDXGIFactory4*, IUnknown*, IUnknown*, DXGI_SWAP_CHAIN_DESC1*, IDXGIOutput*, IDXGISwapChain1**, int>)(lpVtbl[16]))((IDXGIFactory4*)Unsafe.AsPointer(ref this), pDevice, pWindow, pDesc, pRestrictToOutput, ppSwapChain);
    }

    [return: NativeTypeName("HRESULT")]
    public int GetSharedResourceAdapterLuid([NativeTypeName("HANDLE")] void* hResource, LUID* pLuid)
    {
        return ((delegate* unmanaged[Stdcall]<IDXGIFactory4*, void*, LUID*, int>)(lpVtbl[17]))((IDXGIFactory4*)Unsafe.AsPointer(ref this), hResource, pLuid);
    }

    [return: NativeTypeName("HRESULT")]
    public int RegisterStereoStatusWindow([NativeTypeName("HWND")] HWND__* WindowHandle, uint wMsg, [NativeTypeName("DWORD *")] uint* pdwCookie)
    {
        return ((delegate* unmanaged[Stdcall]<IDXGIFactory4*, HWND__*, uint, uint*, int>)(lpVtbl[18]))((IDXGIFactory4*)Unsafe.AsPointer(ref this), WindowHandle, wMsg, pdwCookie);
    }

    [return: NativeTypeName("HRESULT")]
    public int RegisterStereoStatusEvent([NativeTypeName("HANDLE")] void* hEvent, [NativeTypeName("DWORD *")] uint* pdwCookie)
    {
        return ((delegate* unmanaged[Stdcall]<IDXGIFactory4*, void*, uint*, int>)(lpVtbl[19]))((IDXGIFactory4*)Unsafe.AsPointer(ref this), hEvent, pdwCookie);
    }

    public void UnregisterStereoStatus([NativeTypeName("DWORD")] uint dwCookie)
    {
        ((delegate* unmanaged[Stdcall]<IDXGIFactory4*, uint, void>)(lpVtbl[20]))((IDXGIFactory4*)Unsafe.AsPointer(ref this), dwCookie);
    }

    [return: NativeTypeName("HRESULT")]
    public int RegisterOcclusionStatusWindow([NativeTypeName("HWND")] HWND__* WindowHandle, uint wMsg, [NativeTypeName("DWORD *")] uint* pdwCookie)
    {
        return ((delegate* unmanaged[Stdcall]<IDXGIFactory4*, HWND__*, uint, uint*, int>)(lpVtbl[21]))((IDXGIFactory4*)Unsafe.AsPointer(ref this), WindowHandle, wMsg, pdwCookie);
    }

    [return: NativeTypeName("HRESULT")]
    public int RegisterOcclusionStatusEvent([NativeTypeName("HANDLE")] void* hEvent, [NativeTypeName("DWORD *")] uint* pdwCookie)
    {
        return ((delegate* unmanaged[Stdcall]<IDXGIFactory4*, void*, uint*, int>)(lpVtbl[22]))((IDXGIFactory4*)Unsafe.AsPointer(ref this), hEvent, pdwCookie);
    }

    public void UnregisterOcclusionStatus([NativeTypeName("DWORD")] uint dwCookie)
    {
        ((delegate* unmanaged[Stdcall]<IDXGIFactory4*, uint, void>)(lpVtbl[23]))((IDXGIFactory4*)Unsafe.AsPointer(ref this), dwCookie);
    }

    [return: NativeTypeName("HRESULT")]
    public int CreateSwapChainForComposition(IUnknown* pDevice, [NativeTypeName("const DXGI_SWAP_CHAIN_DESC1 *")] DXGI_SWAP_CHAIN_DESC1* pDesc, IDXGIOutput* pRestrictToOutput, IDXGISwapChain1** ppSwapChain)
    {
        return ((delegate* unmanaged[Stdcall]<IDXGIFactory4*, IUnknown*, DXGI_SWAP_CHAIN_DESC1*, IDXGIOutput*, IDXGISwapChain1**, int>)(lpVtbl[24]))((IDXGIFactory4*)Unsafe.AsPointer(ref this), pDevice, pDesc, pRestrictToOutput, ppSwapChain);
    }

    public uint GetCreationFlags()
    {
        return ((delegate* unmanaged[Stdcall]<IDXGIFactory4*, uint>)(lpVtbl[25]))((IDXGIFactory4*)Unsafe.AsPointer(ref this));
    }

    [return: NativeTypeName("HRESULT")]
    public int EnumAdapterByLuid(LUID AdapterLuid, [NativeTypeName("const IID &")] Guid* riid, void** ppvAdapter)
    {
        return ((delegate* unmanaged[Stdcall]<IDXGIFactory4*, LUID, Guid*, void**, int>)(lpVtbl[26]))((IDXGIFactory4*)Unsafe.AsPointer(ref this), AdapterLuid, riid, ppvAdapter);
    }

    [return: NativeTypeName("HRESULT")]
    public int EnumWarpAdapter([NativeTypeName("const IID &")] Guid* riid, void** ppvAdapter)
    {
        return ((delegate* unmanaged[Stdcall]<IDXGIFactory4*, Guid*, void**, int>)(lpVtbl[27]))((IDXGIFactory4*)Unsafe.AsPointer(ref this), riid, ppvAdapter);
    }

    public interface Interface : IDXGIFactory3.Interface
    {
        [return: NativeTypeName("HRESULT")]
        int EnumAdapterByLuid(LUID AdapterLuid, [NativeTypeName("const IID &")] Guid* riid, void** ppvAdapter);

        [return: NativeTypeName("HRESULT")]
        int EnumWarpAdapter([NativeTypeName("const IID &")] Guid* riid, void** ppvAdapter);
    }
}

internal enum DXGI_MEMORY_SEGMENT_GROUP
{
    DXGI_MEMORY_SEGMENT_GROUP_LOCAL = 0,
    DXGI_MEMORY_SEGMENT_GROUP_NON_LOCAL = 1,
}

internal partial struct DXGI_QUERY_VIDEO_MEMORY_INFO
{
    [NativeTypeName("UINT64")]
    public ulong Budget;

    [NativeTypeName("UINT64")]
    public ulong CurrentUsage;

    [NativeTypeName("UINT64")]
    public ulong AvailableForReservation;

    [NativeTypeName("UINT64")]
    public ulong CurrentReservation;
}

[Guid("645967A4-1392-4310-A798-8053CE3E93FD")]
[NativeTypeName("struct IDXGIAdapter3 : IDXGIAdapter2")]
internal unsafe partial struct IDXGIAdapter3 : IDXGIAdapter3.Interface, INativeGuid
{
    static Guid* INativeGuid.NativeGuid => (Guid*)Unsafe.AsPointer(in IID_IDXGIAdapter3);

    public void** lpVtbl;

    [return: NativeTypeName("HRESULT")]
    public int QueryInterface([NativeTypeName("const IID &")] Guid* riid, void** ppvObject)
    {
        return ((delegate* unmanaged[Stdcall]<IDXGIAdapter3*, Guid*, void**, int>)(lpVtbl[0]))((IDXGIAdapter3*)Unsafe.AsPointer(ref this), riid, ppvObject);
    }

    [return: NativeTypeName("ULONG")]
    public uint AddRef()
    {
        return ((delegate* unmanaged[Stdcall]<IDXGIAdapter3*, uint>)(lpVtbl[1]))((IDXGIAdapter3*)Unsafe.AsPointer(ref this));
    }

    [return: NativeTypeName("ULONG")]
    public uint Release()
    {
        return ((delegate* unmanaged[Stdcall]<IDXGIAdapter3*, uint>)(lpVtbl[2]))((IDXGIAdapter3*)Unsafe.AsPointer(ref this));
    }

    [return: NativeTypeName("HRESULT")]
    public int SetPrivateData([NativeTypeName("const GUID &")] Guid* Name, uint DataSize, [NativeTypeName("const void *")] void* pData)
    {
        return ((delegate* unmanaged[Stdcall]<IDXGIAdapter3*, Guid*, uint, void*, int>)(lpVtbl[3]))((IDXGIAdapter3*)Unsafe.AsPointer(ref this), Name, DataSize, pData);
    }

    [return: NativeTypeName("HRESULT")]
    public int SetPrivateDataInterface([NativeTypeName("const GUID &")] Guid* Name, [NativeTypeName("const IUnknown *")] IUnknown* pUnknown)
    {
        return ((delegate* unmanaged[Stdcall]<IDXGIAdapter3*, Guid*, IUnknown*, int>)(lpVtbl[4]))((IDXGIAdapter3*)Unsafe.AsPointer(ref this), Name, pUnknown);
    }

    [return: NativeTypeName("HRESULT")]
    public int GetPrivateData([NativeTypeName("const GUID &")] Guid* Name, uint* pDataSize, void* pData)
    {
        return ((delegate* unmanaged[Stdcall]<IDXGIAdapter3*, Guid*, uint*, void*, int>)(lpVtbl[5]))((IDXGIAdapter3*)Unsafe.AsPointer(ref this), Name, pDataSize, pData);
    }

    [return: NativeTypeName("HRESULT")]
    public int GetParent([NativeTypeName("const IID &")] Guid* riid, void** ppParent)
    {
        return ((delegate* unmanaged[Stdcall]<IDXGIAdapter3*, Guid*, void**, int>)(lpVtbl[6]))((IDXGIAdapter3*)Unsafe.AsPointer(ref this), riid, ppParent);
    }

    [return: NativeTypeName("HRESULT")]
    public int EnumOutputs(uint Output, IDXGIOutput** ppOutput)
    {
        return ((delegate* unmanaged[Stdcall]<IDXGIAdapter3*, uint, IDXGIOutput**, int>)(lpVtbl[7]))((IDXGIAdapter3*)Unsafe.AsPointer(ref this), Output, ppOutput);
    }

    [return: NativeTypeName("HRESULT")]
    public int GetDesc(DXGI_ADAPTER_DESC* pDesc)
    {
        return ((delegate* unmanaged[Stdcall]<IDXGIAdapter3*, DXGI_ADAPTER_DESC*, int>)(lpVtbl[8]))((IDXGIAdapter3*)Unsafe.AsPointer(ref this), pDesc);
    }

    [return: NativeTypeName("HRESULT")]
    public int CheckInterfaceSupport([NativeTypeName("const GUID &")] Guid* InterfaceName, LARGE_INTEGER* pUMDVersion)
    {
        return ((delegate* unmanaged[Stdcall]<IDXGIAdapter3*, Guid*, LARGE_INTEGER*, int>)(lpVtbl[9]))((IDXGIAdapter3*)Unsafe.AsPointer(ref this), InterfaceName, pUMDVersion);
    }

    [return: NativeTypeName("HRESULT")]
    public int GetDesc1(DXGI_ADAPTER_DESC1* pDesc)
    {
        return ((delegate* unmanaged[Stdcall]<IDXGIAdapter3*, DXGI_ADAPTER_DESC1*, int>)(lpVtbl[10]))((IDXGIAdapter3*)Unsafe.AsPointer(ref this), pDesc);
    }

    [return: NativeTypeName("HRESULT")]
    public int GetDesc2(DXGI_ADAPTER_DESC2* pDesc)
    {
        return ((delegate* unmanaged[Stdcall]<IDXGIAdapter3*, DXGI_ADAPTER_DESC2*, int>)(lpVtbl[11]))((IDXGIAdapter3*)Unsafe.AsPointer(ref this), pDesc);
    }

    [return: NativeTypeName("HRESULT")]
    public int RegisterHardwareContentProtectionTeardownStatusEvent([NativeTypeName("HANDLE")] void* hEvent, [NativeTypeName("DWORD *")] uint* pdwCookie)
    {
        return ((delegate* unmanaged[Stdcall]<IDXGIAdapter3*, void*, uint*, int>)(lpVtbl[12]))((IDXGIAdapter3*)Unsafe.AsPointer(ref this), hEvent, pdwCookie);
    }

    public void UnregisterHardwareContentProtectionTeardownStatus([NativeTypeName("DWORD")] uint dwCookie)
    {
        ((delegate* unmanaged[Stdcall]<IDXGIAdapter3*, uint, void>)(lpVtbl[13]))((IDXGIAdapter3*)Unsafe.AsPointer(ref this), dwCookie);
    }

    [return: NativeTypeName("HRESULT")]
    public int QueryVideoMemoryInfo(uint NodeIndex, DXGI_MEMORY_SEGMENT_GROUP MemorySegmentGroup, DXGI_QUERY_VIDEO_MEMORY_INFO* pVideoMemoryInfo)
    {
        return ((delegate* unmanaged[Stdcall]<IDXGIAdapter3*, uint, DXGI_MEMORY_SEGMENT_GROUP, DXGI_QUERY_VIDEO_MEMORY_INFO*, int>)(lpVtbl[14]))((IDXGIAdapter3*)Unsafe.AsPointer(ref this), NodeIndex, MemorySegmentGroup, pVideoMemoryInfo);
    }

    [return: NativeTypeName("HRESULT")]
    public int SetVideoMemoryReservation(uint NodeIndex, DXGI_MEMORY_SEGMENT_GROUP MemorySegmentGroup, [NativeTypeName("UINT64")] ulong Reservation)
    {
        return ((delegate* unmanaged[Stdcall]<IDXGIAdapter3*, uint, DXGI_MEMORY_SEGMENT_GROUP, ulong, int>)(lpVtbl[15]))((IDXGIAdapter3*)Unsafe.AsPointer(ref this), NodeIndex, MemorySegmentGroup, Reservation);
    }

    [return: NativeTypeName("HRESULT")]
    public int RegisterVideoMemoryBudgetChangeNotificationEvent([NativeTypeName("HANDLE")] void* hEvent, [NativeTypeName("DWORD *")] uint* pdwCookie)
    {
        return ((delegate* unmanaged[Stdcall]<IDXGIAdapter3*, void*, uint*, int>)(lpVtbl[16]))((IDXGIAdapter3*)Unsafe.AsPointer(ref this), hEvent, pdwCookie);
    }

    public void UnregisterVideoMemoryBudgetChangeNotification([NativeTypeName("DWORD")] uint dwCookie)
    {
        ((delegate* unmanaged[Stdcall]<IDXGIAdapter3*, uint, void>)(lpVtbl[17]))((IDXGIAdapter3*)Unsafe.AsPointer(ref this), dwCookie);
    }

    public interface Interface : IDXGIAdapter2.Interface
    {
        [return: NativeTypeName("HRESULT")]
        int RegisterHardwareContentProtectionTeardownStatusEvent([NativeTypeName("HANDLE")] void* hEvent, [NativeTypeName("DWORD *")] uint* pdwCookie);

        void UnregisterHardwareContentProtectionTeardownStatus([NativeTypeName("DWORD")] uint dwCookie);

        [return: NativeTypeName("HRESULT")]
        int QueryVideoMemoryInfo(uint NodeIndex, DXGI_MEMORY_SEGMENT_GROUP MemorySegmentGroup, DXGI_QUERY_VIDEO_MEMORY_INFO* pVideoMemoryInfo);

        [return: NativeTypeName("HRESULT")]
        int SetVideoMemoryReservation(uint NodeIndex, DXGI_MEMORY_SEGMENT_GROUP MemorySegmentGroup, [NativeTypeName("UINT64")] ulong Reservation);

        [return: NativeTypeName("HRESULT")]
        int RegisterVideoMemoryBudgetChangeNotificationEvent([NativeTypeName("HANDLE")] void* hEvent, [NativeTypeName("DWORD *")] uint* pdwCookie);

        void UnregisterVideoMemoryBudgetChangeNotification([NativeTypeName("DWORD")] uint dwCookie);
    }
}

internal enum DXGI_OUTDUPL_FLAG
{
    DXGI_OUTDUPL_COMPOSITED_UI_CAPTURE_ONLY = 1,
}

[Guid("80A07424-AB52-42EB-833C-0C42FD282D98")]
[NativeTypeName("struct IDXGIOutput5 : IDXGIOutput4")]
internal unsafe partial struct IDXGIOutput5 : IDXGIOutput5.Interface, INativeGuid
{
    static Guid* INativeGuid.NativeGuid => (Guid*)Unsafe.AsPointer(in IID_IDXGIOutput5);

    public void** lpVtbl;

    [return: NativeTypeName("HRESULT")]
    public int QueryInterface([NativeTypeName("const IID &")] Guid* riid, void** ppvObject)
    {
        return ((delegate* unmanaged[Stdcall]<IDXGIOutput5*, Guid*, void**, int>)(lpVtbl[0]))((IDXGIOutput5*)Unsafe.AsPointer(ref this), riid, ppvObject);
    }

    [return: NativeTypeName("ULONG")]
    public uint AddRef()
    {
        return ((delegate* unmanaged[Stdcall]<IDXGIOutput5*, uint>)(lpVtbl[1]))((IDXGIOutput5*)Unsafe.AsPointer(ref this));
    }

    [return: NativeTypeName("ULONG")]
    public uint Release()
    {
        return ((delegate* unmanaged[Stdcall]<IDXGIOutput5*, uint>)(lpVtbl[2]))((IDXGIOutput5*)Unsafe.AsPointer(ref this));
    }

    [return: NativeTypeName("HRESULT")]
    public int SetPrivateData([NativeTypeName("const GUID &")] Guid* Name, uint DataSize, [NativeTypeName("const void *")] void* pData)
    {
        return ((delegate* unmanaged[Stdcall]<IDXGIOutput5*, Guid*, uint, void*, int>)(lpVtbl[3]))((IDXGIOutput5*)Unsafe.AsPointer(ref this), Name, DataSize, pData);
    }

    [return: NativeTypeName("HRESULT")]
    public int SetPrivateDataInterface([NativeTypeName("const GUID &")] Guid* Name, [NativeTypeName("const IUnknown *")] IUnknown* pUnknown)
    {
        return ((delegate* unmanaged[Stdcall]<IDXGIOutput5*, Guid*, IUnknown*, int>)(lpVtbl[4]))((IDXGIOutput5*)Unsafe.AsPointer(ref this), Name, pUnknown);
    }

    [return: NativeTypeName("HRESULT")]
    public int GetPrivateData([NativeTypeName("const GUID &")] Guid* Name, uint* pDataSize, void* pData)
    {
        return ((delegate* unmanaged[Stdcall]<IDXGIOutput5*, Guid*, uint*, void*, int>)(lpVtbl[5]))((IDXGIOutput5*)Unsafe.AsPointer(ref this), Name, pDataSize, pData);
    }

    [return: NativeTypeName("HRESULT")]
    public int GetParent([NativeTypeName("const IID &")] Guid* riid, void** ppParent)
    {
        return ((delegate* unmanaged[Stdcall]<IDXGIOutput5*, Guid*, void**, int>)(lpVtbl[6]))((IDXGIOutput5*)Unsafe.AsPointer(ref this), riid, ppParent);
    }

    [return: NativeTypeName("HRESULT")]
    public int GetDesc(DXGI_OUTPUT_DESC* pDesc)
    {
        return ((delegate* unmanaged[Stdcall]<IDXGIOutput5*, DXGI_OUTPUT_DESC*, int>)(lpVtbl[7]))((IDXGIOutput5*)Unsafe.AsPointer(ref this), pDesc);
    }

    [return: NativeTypeName("HRESULT")]
    public int GetDisplayModeList(DXGI_FORMAT EnumFormat, uint Flags, uint* pNumModes, DXGI_MODE_DESC* pDesc)
    {
        return ((delegate* unmanaged[Stdcall]<IDXGIOutput5*, DXGI_FORMAT, uint, uint*, DXGI_MODE_DESC*, int>)(lpVtbl[8]))((IDXGIOutput5*)Unsafe.AsPointer(ref this), EnumFormat, Flags, pNumModes, pDesc);
    }

    [return: NativeTypeName("HRESULT")]
    public int FindClosestMatchingMode([NativeTypeName("const DXGI_MODE_DESC *")] DXGI_MODE_DESC* pModeToMatch, DXGI_MODE_DESC* pClosestMatch, IUnknown* pConcernedDevice)
    {
        return ((delegate* unmanaged[Stdcall]<IDXGIOutput5*, DXGI_MODE_DESC*, DXGI_MODE_DESC*, IUnknown*, int>)(lpVtbl[9]))((IDXGIOutput5*)Unsafe.AsPointer(ref this), pModeToMatch, pClosestMatch, pConcernedDevice);
    }

    [return: NativeTypeName("HRESULT")]
    public int WaitForVBlank()
    {
        return ((delegate* unmanaged[Stdcall]<IDXGIOutput5*, int>)(lpVtbl[10]))((IDXGIOutput5*)Unsafe.AsPointer(ref this));
    }

    [return: NativeTypeName("HRESULT")]
    public int TakeOwnership(IUnknown* pDevice, [NativeTypeName("BOOL")] int Exclusive)
    {
        return ((delegate* unmanaged[Stdcall]<IDXGIOutput5*, IUnknown*, int, int>)(lpVtbl[11]))((IDXGIOutput5*)Unsafe.AsPointer(ref this), pDevice, Exclusive);
    }

    public void ReleaseOwnership()
    {
        ((delegate* unmanaged[Stdcall]<IDXGIOutput5*, void>)(lpVtbl[12]))((IDXGIOutput5*)Unsafe.AsPointer(ref this));
    }

    [return: NativeTypeName("HRESULT")]
    public int GetGammaControlCapabilities(DXGI_GAMMA_CONTROL_CAPABILITIES* pGammaCaps)
    {
        return ((delegate* unmanaged[Stdcall]<IDXGIOutput5*, DXGI_GAMMA_CONTROL_CAPABILITIES*, int>)(lpVtbl[13]))((IDXGIOutput5*)Unsafe.AsPointer(ref this), pGammaCaps);
    }

    [return: NativeTypeName("HRESULT")]
    public int SetGammaControl([NativeTypeName("const DXGI_GAMMA_CONTROL *")] DXGI_GAMMA_CONTROL* pArray)
    {
        return ((delegate* unmanaged[Stdcall]<IDXGIOutput5*, DXGI_GAMMA_CONTROL*, int>)(lpVtbl[14]))((IDXGIOutput5*)Unsafe.AsPointer(ref this), pArray);
    }

    [return: NativeTypeName("HRESULT")]
    public int GetGammaControl(DXGI_GAMMA_CONTROL* pArray)
    {
        return ((delegate* unmanaged[Stdcall]<IDXGIOutput5*, DXGI_GAMMA_CONTROL*, int>)(lpVtbl[15]))((IDXGIOutput5*)Unsafe.AsPointer(ref this), pArray);
    }

    [return: NativeTypeName("HRESULT")]
    public int SetDisplaySurface(IDXGISurface* pScanoutSurface)
    {
        return ((delegate* unmanaged[Stdcall]<IDXGIOutput5*, IDXGISurface*, int>)(lpVtbl[16]))((IDXGIOutput5*)Unsafe.AsPointer(ref this), pScanoutSurface);
    }

    [return: NativeTypeName("HRESULT")]
    public int GetDisplaySurfaceData(IDXGISurface* pDestination)
    {
        return ((delegate* unmanaged[Stdcall]<IDXGIOutput5*, IDXGISurface*, int>)(lpVtbl[17]))((IDXGIOutput5*)Unsafe.AsPointer(ref this), pDestination);
    }

    [return: NativeTypeName("HRESULT")]
    public int GetFrameStatistics(DXGI_FRAME_STATISTICS* pStats)
    {
        return ((delegate* unmanaged[Stdcall]<IDXGIOutput5*, DXGI_FRAME_STATISTICS*, int>)(lpVtbl[18]))((IDXGIOutput5*)Unsafe.AsPointer(ref this), pStats);
    }

    [return: NativeTypeName("HRESULT")]
    public int GetDisplayModeList1(DXGI_FORMAT EnumFormat, uint Flags, uint* pNumModes, DXGI_MODE_DESC1* pDesc)
    {
        return ((delegate* unmanaged[Stdcall]<IDXGIOutput5*, DXGI_FORMAT, uint, uint*, DXGI_MODE_DESC1*, int>)(lpVtbl[19]))((IDXGIOutput5*)Unsafe.AsPointer(ref this), EnumFormat, Flags, pNumModes, pDesc);
    }

    [return: NativeTypeName("HRESULT")]
    public int FindClosestMatchingMode1([NativeTypeName("const DXGI_MODE_DESC1 *")] DXGI_MODE_DESC1* pModeToMatch, DXGI_MODE_DESC1* pClosestMatch, IUnknown* pConcernedDevice)
    {
        return ((delegate* unmanaged[Stdcall]<IDXGIOutput5*, DXGI_MODE_DESC1*, DXGI_MODE_DESC1*, IUnknown*, int>)(lpVtbl[20]))((IDXGIOutput5*)Unsafe.AsPointer(ref this), pModeToMatch, pClosestMatch, pConcernedDevice);
    }

    [return: NativeTypeName("HRESULT")]
    public int GetDisplaySurfaceData1(IDXGIResource* pDestination)
    {
        return ((delegate* unmanaged[Stdcall]<IDXGIOutput5*, IDXGIResource*, int>)(lpVtbl[21]))((IDXGIOutput5*)Unsafe.AsPointer(ref this), pDestination);
    }

    [return: NativeTypeName("HRESULT")]
    public int DuplicateOutput(IUnknown* pDevice, IDXGIOutputDuplication** ppOutputDuplication)
    {
        return ((delegate* unmanaged[Stdcall]<IDXGIOutput5*, IUnknown*, IDXGIOutputDuplication**, int>)(lpVtbl[22]))((IDXGIOutput5*)Unsafe.AsPointer(ref this), pDevice, ppOutputDuplication);
    }

    [return: NativeTypeName("BOOL")]
    public int SupportsOverlays()
    {
        return ((delegate* unmanaged[Stdcall]<IDXGIOutput5*, int>)(lpVtbl[23]))((IDXGIOutput5*)Unsafe.AsPointer(ref this));
    }

    [return: NativeTypeName("HRESULT")]
    public int CheckOverlaySupport(DXGI_FORMAT EnumFormat, IUnknown* pConcernedDevice, uint* pFlags)
    {
        return ((delegate* unmanaged[Stdcall]<IDXGIOutput5*, DXGI_FORMAT, IUnknown*, uint*, int>)(lpVtbl[24]))((IDXGIOutput5*)Unsafe.AsPointer(ref this), EnumFormat, pConcernedDevice, pFlags);
    }

    [return: NativeTypeName("HRESULT")]
    public int CheckOverlayColorSpaceSupport(DXGI_FORMAT Format, DXGI_COLOR_SPACE_TYPE ColorSpace, IUnknown* pConcernedDevice, uint* pFlags)
    {
        return ((delegate* unmanaged[Stdcall]<IDXGIOutput5*, DXGI_FORMAT, DXGI_COLOR_SPACE_TYPE, IUnknown*, uint*, int>)(lpVtbl[25]))((IDXGIOutput5*)Unsafe.AsPointer(ref this), Format, ColorSpace, pConcernedDevice, pFlags);
    }

    [return: NativeTypeName("HRESULT")]
    public int DuplicateOutput1(IUnknown* pDevice, uint Flags, uint SupportedFormatsCount, [NativeTypeName("const DXGI_FORMAT *")] DXGI_FORMAT* pSupportedFormats, IDXGIOutputDuplication** ppOutputDuplication)
    {
        return ((delegate* unmanaged[Stdcall]<IDXGIOutput5*, IUnknown*, uint, uint, DXGI_FORMAT*, IDXGIOutputDuplication**, int>)(lpVtbl[26]))((IDXGIOutput5*)Unsafe.AsPointer(ref this), pDevice, Flags, SupportedFormatsCount, pSupportedFormats, ppOutputDuplication);
    }

    public interface Interface : IDXGIOutput4.Interface
    {
        [return: NativeTypeName("HRESULT")]
        int DuplicateOutput1(IUnknown* pDevice, uint Flags, uint SupportedFormatsCount, [NativeTypeName("const DXGI_FORMAT *")] DXGI_FORMAT* pSupportedFormats, IDXGIOutputDuplication** ppOutputDuplication);
    }
}

internal enum DXGI_HDR_METADATA_TYPE
{
    DXGI_HDR_METADATA_TYPE_NONE = 0,
    DXGI_HDR_METADATA_TYPE_HDR10 = 1,
    DXGI_HDR_METADATA_TYPE_HDR10PLUS = 2,
}

internal partial struct DXGI_HDR_METADATA_HDR10
{
    [NativeTypeName("UINT16[2]")]
    public _RedPrimary_e__FixedBuffer RedPrimary;

    [NativeTypeName("UINT16[2]")]
    public _GreenPrimary_e__FixedBuffer GreenPrimary;

    [NativeTypeName("UINT16[2]")]
    public _BluePrimary_e__FixedBuffer BluePrimary;

    [NativeTypeName("UINT16[2]")]
    public _WhitePoint_e__FixedBuffer WhitePoint;

    public uint MaxMasteringLuminance;

    public uint MinMasteringLuminance;

    [NativeTypeName("UINT16")]
    public ushort MaxContentLightLevel;

    [NativeTypeName("UINT16")]
    public ushort MaxFrameAverageLightLevel;

    [InlineArray(2)]
    public partial struct _RedPrimary_e__FixedBuffer
    {
        public ushort e0;
    }

    [InlineArray(2)]
    public partial struct _GreenPrimary_e__FixedBuffer
    {
        public ushort e0;
    }

    [InlineArray(2)]
    public partial struct _BluePrimary_e__FixedBuffer
    {
        public ushort e0;
    }

    [InlineArray(2)]
    public partial struct _WhitePoint_e__FixedBuffer
    {
        public ushort e0;
    }
}

internal partial struct DXGI_HDR_METADATA_HDR10PLUS
{
    [NativeTypeName("BYTE[72]")]
    public _Data_e__FixedBuffer Data;

    [InlineArray(72)]
    public partial struct _Data_e__FixedBuffer
    {
        public byte e0;
    }
}

[Guid("3D585D5A-BD4A-489E-B1F4-3DBCB6452FFB")]
[NativeTypeName("struct IDXGISwapChain4 : IDXGISwapChain3")]
internal unsafe partial struct IDXGISwapChain4 : IDXGISwapChain4.Interface, INativeGuid
{
    static Guid* INativeGuid.NativeGuid => (Guid*)Unsafe.AsPointer(in IID_IDXGISwapChain4);

    public void** lpVtbl;

    [return: NativeTypeName("HRESULT")]
    public int QueryInterface([NativeTypeName("const IID &")] Guid* riid, void** ppvObject)
    {
        return ((delegate* unmanaged[Stdcall]<IDXGISwapChain4*, Guid*, void**, int>)(lpVtbl[0]))((IDXGISwapChain4*)Unsafe.AsPointer(ref this), riid, ppvObject);
    }

    [return: NativeTypeName("ULONG")]
    public uint AddRef()
    {
        return ((delegate* unmanaged[Stdcall]<IDXGISwapChain4*, uint>)(lpVtbl[1]))((IDXGISwapChain4*)Unsafe.AsPointer(ref this));
    }

    [return: NativeTypeName("ULONG")]
    public uint Release()
    {
        return ((delegate* unmanaged[Stdcall]<IDXGISwapChain4*, uint>)(lpVtbl[2]))((IDXGISwapChain4*)Unsafe.AsPointer(ref this));
    }

    [return: NativeTypeName("HRESULT")]
    public int SetPrivateData([NativeTypeName("const GUID &")] Guid* Name, uint DataSize, [NativeTypeName("const void *")] void* pData)
    {
        return ((delegate* unmanaged[Stdcall]<IDXGISwapChain4*, Guid*, uint, void*, int>)(lpVtbl[3]))((IDXGISwapChain4*)Unsafe.AsPointer(ref this), Name, DataSize, pData);
    }

    [return: NativeTypeName("HRESULT")]
    public int SetPrivateDataInterface([NativeTypeName("const GUID &")] Guid* Name, [NativeTypeName("const IUnknown *")] IUnknown* pUnknown)
    {
        return ((delegate* unmanaged[Stdcall]<IDXGISwapChain4*, Guid*, IUnknown*, int>)(lpVtbl[4]))((IDXGISwapChain4*)Unsafe.AsPointer(ref this), Name, pUnknown);
    }

    [return: NativeTypeName("HRESULT")]
    public int GetPrivateData([NativeTypeName("const GUID &")] Guid* Name, uint* pDataSize, void* pData)
    {
        return ((delegate* unmanaged[Stdcall]<IDXGISwapChain4*, Guid*, uint*, void*, int>)(lpVtbl[5]))((IDXGISwapChain4*)Unsafe.AsPointer(ref this), Name, pDataSize, pData);
    }

    [return: NativeTypeName("HRESULT")]
    public int GetParent([NativeTypeName("const IID &")] Guid* riid, void** ppParent)
    {
        return ((delegate* unmanaged[Stdcall]<IDXGISwapChain4*, Guid*, void**, int>)(lpVtbl[6]))((IDXGISwapChain4*)Unsafe.AsPointer(ref this), riid, ppParent);
    }

    [return: NativeTypeName("HRESULT")]
    public int GetDevice([NativeTypeName("const IID &")] Guid* riid, void** ppDevice)
    {
        return ((delegate* unmanaged[Stdcall]<IDXGISwapChain4*, Guid*, void**, int>)(lpVtbl[7]))((IDXGISwapChain4*)Unsafe.AsPointer(ref this), riid, ppDevice);
    }

    [return: NativeTypeName("HRESULT")]
    public int Present(uint SyncInterval, uint Flags)
    {
        return ((delegate* unmanaged[Stdcall]<IDXGISwapChain4*, uint, uint, int>)(lpVtbl[8]))((IDXGISwapChain4*)Unsafe.AsPointer(ref this), SyncInterval, Flags);
    }

    [return: NativeTypeName("HRESULT")]
    public int GetBuffer(uint Buffer, [NativeTypeName("const IID &")] Guid* riid, void** ppSurface)
    {
        return ((delegate* unmanaged[Stdcall]<IDXGISwapChain4*, uint, Guid*, void**, int>)(lpVtbl[9]))((IDXGISwapChain4*)Unsafe.AsPointer(ref this), Buffer, riid, ppSurface);
    }

    [return: NativeTypeName("HRESULT")]
    public int SetFullscreenState([NativeTypeName("BOOL")] int Fullscreen, IDXGIOutput* pTarget)
    {
        return ((delegate* unmanaged[Stdcall]<IDXGISwapChain4*, int, IDXGIOutput*, int>)(lpVtbl[10]))((IDXGISwapChain4*)Unsafe.AsPointer(ref this), Fullscreen, pTarget);
    }

    [return: NativeTypeName("HRESULT")]
    public int GetFullscreenState([NativeTypeName("BOOL *")] int* pFullscreen, IDXGIOutput** ppTarget)
    {
        return ((delegate* unmanaged[Stdcall]<IDXGISwapChain4*, int*, IDXGIOutput**, int>)(lpVtbl[11]))((IDXGISwapChain4*)Unsafe.AsPointer(ref this), pFullscreen, ppTarget);
    }

    [return: NativeTypeName("HRESULT")]
    public int GetDesc(DXGI_SWAP_CHAIN_DESC* pDesc)
    {
        return ((delegate* unmanaged[Stdcall]<IDXGISwapChain4*, DXGI_SWAP_CHAIN_DESC*, int>)(lpVtbl[12]))((IDXGISwapChain4*)Unsafe.AsPointer(ref this), pDesc);
    }

    [return: NativeTypeName("HRESULT")]
    public int ResizeBuffers(uint BufferCount, uint Width, uint Height, DXGI_FORMAT NewFormat, uint SwapChainFlags)
    {
        return ((delegate* unmanaged[Stdcall]<IDXGISwapChain4*, uint, uint, uint, DXGI_FORMAT, uint, int>)(lpVtbl[13]))((IDXGISwapChain4*)Unsafe.AsPointer(ref this), BufferCount, Width, Height, NewFormat, SwapChainFlags);
    }

    [return: NativeTypeName("HRESULT")]
    public int ResizeTarget([NativeTypeName("const DXGI_MODE_DESC *")] DXGI_MODE_DESC* pNewTargetParameters)
    {
        return ((delegate* unmanaged[Stdcall]<IDXGISwapChain4*, DXGI_MODE_DESC*, int>)(lpVtbl[14]))((IDXGISwapChain4*)Unsafe.AsPointer(ref this), pNewTargetParameters);
    }

    [return: NativeTypeName("HRESULT")]
    public int GetContainingOutput(IDXGIOutput** ppOutput)
    {
        return ((delegate* unmanaged[Stdcall]<IDXGISwapChain4*, IDXGIOutput**, int>)(lpVtbl[15]))((IDXGISwapChain4*)Unsafe.AsPointer(ref this), ppOutput);
    }

    [return: NativeTypeName("HRESULT")]
    public int GetFrameStatistics(DXGI_FRAME_STATISTICS* pStats)
    {
        return ((delegate* unmanaged[Stdcall]<IDXGISwapChain4*, DXGI_FRAME_STATISTICS*, int>)(lpVtbl[16]))((IDXGISwapChain4*)Unsafe.AsPointer(ref this), pStats);
    }

    [return: NativeTypeName("HRESULT")]
    public int GetLastPresentCount(uint* pLastPresentCount)
    {
        return ((delegate* unmanaged[Stdcall]<IDXGISwapChain4*, uint*, int>)(lpVtbl[17]))((IDXGISwapChain4*)Unsafe.AsPointer(ref this), pLastPresentCount);
    }

    [return: NativeTypeName("HRESULT")]
    public int GetDesc1(DXGI_SWAP_CHAIN_DESC1* pDesc)
    {
        return ((delegate* unmanaged[Stdcall]<IDXGISwapChain4*, DXGI_SWAP_CHAIN_DESC1*, int>)(lpVtbl[18]))((IDXGISwapChain4*)Unsafe.AsPointer(ref this), pDesc);
    }

    [return: NativeTypeName("HRESULT")]
    public int GetFullscreenDesc(DXGI_SWAP_CHAIN_FULLSCREEN_DESC* pDesc)
    {
        return ((delegate* unmanaged[Stdcall]<IDXGISwapChain4*, DXGI_SWAP_CHAIN_FULLSCREEN_DESC*, int>)(lpVtbl[19]))((IDXGISwapChain4*)Unsafe.AsPointer(ref this), pDesc);
    }

    [return: NativeTypeName("HRESULT")]
    public int GetHwnd([NativeTypeName("HWND *")] HWND__** pHwnd)
    {
        return ((delegate* unmanaged[Stdcall]<IDXGISwapChain4*, HWND__**, int>)(lpVtbl[20]))((IDXGISwapChain4*)Unsafe.AsPointer(ref this), pHwnd);
    }

    [return: NativeTypeName("HRESULT")]
    public int GetCoreWindow([NativeTypeName("const IID &")] Guid* refiid, void** ppUnk)
    {
        return ((delegate* unmanaged[Stdcall]<IDXGISwapChain4*, Guid*, void**, int>)(lpVtbl[21]))((IDXGISwapChain4*)Unsafe.AsPointer(ref this), refiid, ppUnk);
    }

    [return: NativeTypeName("HRESULT")]
    public int Present1(uint SyncInterval, uint PresentFlags, [NativeTypeName("const DXGI_PRESENT_PARAMETERS *")] DXGI_PRESENT_PARAMETERS* pPresentParameters)
    {
        return ((delegate* unmanaged[Stdcall]<IDXGISwapChain4*, uint, uint, DXGI_PRESENT_PARAMETERS*, int>)(lpVtbl[22]))((IDXGISwapChain4*)Unsafe.AsPointer(ref this), SyncInterval, PresentFlags, pPresentParameters);
    }

    [return: NativeTypeName("BOOL")]
    public int IsTemporaryMonoSupported()
    {
        return ((delegate* unmanaged[Stdcall]<IDXGISwapChain4*, int>)(lpVtbl[23]))((IDXGISwapChain4*)Unsafe.AsPointer(ref this));
    }

    [return: NativeTypeName("HRESULT")]
    public int GetRestrictToOutput(IDXGIOutput** ppRestrictToOutput)
    {
        return ((delegate* unmanaged[Stdcall]<IDXGISwapChain4*, IDXGIOutput**, int>)(lpVtbl[24]))((IDXGISwapChain4*)Unsafe.AsPointer(ref this), ppRestrictToOutput);
    }

    [return: NativeTypeName("HRESULT")]
    public int SetBackgroundColor([NativeTypeName("const DXGI_RGBA *")] D3DCOLORVALUE* pColor)
    {
        return ((delegate* unmanaged[Stdcall]<IDXGISwapChain4*, D3DCOLORVALUE*, int>)(lpVtbl[25]))((IDXGISwapChain4*)Unsafe.AsPointer(ref this), pColor);
    }

    [return: NativeTypeName("HRESULT")]
    public int GetBackgroundColor([NativeTypeName("DXGI_RGBA *")] D3DCOLORVALUE* pColor)
    {
        return ((delegate* unmanaged[Stdcall]<IDXGISwapChain4*, D3DCOLORVALUE*, int>)(lpVtbl[26]))((IDXGISwapChain4*)Unsafe.AsPointer(ref this), pColor);
    }

    [return: NativeTypeName("HRESULT")]
    public int SetRotation(DXGI_MODE_ROTATION Rotation)
    {
        return ((delegate* unmanaged[Stdcall]<IDXGISwapChain4*, DXGI_MODE_ROTATION, int>)(lpVtbl[27]))((IDXGISwapChain4*)Unsafe.AsPointer(ref this), Rotation);
    }

    [return: NativeTypeName("HRESULT")]
    public int GetRotation(DXGI_MODE_ROTATION* pRotation)
    {
        return ((delegate* unmanaged[Stdcall]<IDXGISwapChain4*, DXGI_MODE_ROTATION*, int>)(lpVtbl[28]))((IDXGISwapChain4*)Unsafe.AsPointer(ref this), pRotation);
    }

    [return: NativeTypeName("HRESULT")]
    public int SetSourceSize(uint Width, uint Height)
    {
        return ((delegate* unmanaged[Stdcall]<IDXGISwapChain4*, uint, uint, int>)(lpVtbl[29]))((IDXGISwapChain4*)Unsafe.AsPointer(ref this), Width, Height);
    }

    [return: NativeTypeName("HRESULT")]
    public int GetSourceSize(uint* pWidth, uint* pHeight)
    {
        return ((delegate* unmanaged[Stdcall]<IDXGISwapChain4*, uint*, uint*, int>)(lpVtbl[30]))((IDXGISwapChain4*)Unsafe.AsPointer(ref this), pWidth, pHeight);
    }

    [return: NativeTypeName("HRESULT")]
    public int SetMaximumFrameLatency(uint MaxLatency)
    {
        return ((delegate* unmanaged[Stdcall]<IDXGISwapChain4*, uint, int>)(lpVtbl[31]))((IDXGISwapChain4*)Unsafe.AsPointer(ref this), MaxLatency);
    }

    [return: NativeTypeName("HRESULT")]
    public int GetMaximumFrameLatency(uint* pMaxLatency)
    {
        return ((delegate* unmanaged[Stdcall]<IDXGISwapChain4*, uint*, int>)(lpVtbl[32]))((IDXGISwapChain4*)Unsafe.AsPointer(ref this), pMaxLatency);
    }

    [return: NativeTypeName("HANDLE")]
    public void* GetFrameLatencyWaitableObject()
    {
        return ((delegate* unmanaged[Stdcall]<IDXGISwapChain4*, void*>)(lpVtbl[33]))((IDXGISwapChain4*)Unsafe.AsPointer(ref this));
    }

    [return: NativeTypeName("HRESULT")]
    public int SetMatrixTransform([NativeTypeName("const DXGI_MATRIX_3X2_F *")] DXGI_MATRIX_3X2_F* pMatrix)
    {
        return ((delegate* unmanaged[Stdcall]<IDXGISwapChain4*, DXGI_MATRIX_3X2_F*, int>)(lpVtbl[34]))((IDXGISwapChain4*)Unsafe.AsPointer(ref this), pMatrix);
    }

    [return: NativeTypeName("HRESULT")]
    public int GetMatrixTransform(DXGI_MATRIX_3X2_F* pMatrix)
    {
        return ((delegate* unmanaged[Stdcall]<IDXGISwapChain4*, DXGI_MATRIX_3X2_F*, int>)(lpVtbl[35]))((IDXGISwapChain4*)Unsafe.AsPointer(ref this), pMatrix);
    }

    public uint GetCurrentBackBufferIndex()
    {
        return ((delegate* unmanaged[Stdcall]<IDXGISwapChain4*, uint>)(lpVtbl[36]))((IDXGISwapChain4*)Unsafe.AsPointer(ref this));
    }

    [return: NativeTypeName("HRESULT")]
    public int CheckColorSpaceSupport(DXGI_COLOR_SPACE_TYPE ColorSpace, uint* pColorSpaceSupport)
    {
        return ((delegate* unmanaged[Stdcall]<IDXGISwapChain4*, DXGI_COLOR_SPACE_TYPE, uint*, int>)(lpVtbl[37]))((IDXGISwapChain4*)Unsafe.AsPointer(ref this), ColorSpace, pColorSpaceSupport);
    }

    [return: NativeTypeName("HRESULT")]
    public int SetColorSpace1(DXGI_COLOR_SPACE_TYPE ColorSpace)
    {
        return ((delegate* unmanaged[Stdcall]<IDXGISwapChain4*, DXGI_COLOR_SPACE_TYPE, int>)(lpVtbl[38]))((IDXGISwapChain4*)Unsafe.AsPointer(ref this), ColorSpace);
    }

    [return: NativeTypeName("HRESULT")]
    public int ResizeBuffers1(uint BufferCount, uint Width, uint Height, DXGI_FORMAT Format, uint SwapChainFlags, [NativeTypeName("const UINT *")] uint* pCreationNodeMask, [NativeTypeName("IUnknown *const *")] IUnknown** ppPresentQueue)
    {
        return ((delegate* unmanaged[Stdcall]<IDXGISwapChain4*, uint, uint, uint, DXGI_FORMAT, uint, uint*, IUnknown**, int>)(lpVtbl[39]))((IDXGISwapChain4*)Unsafe.AsPointer(ref this), BufferCount, Width, Height, Format, SwapChainFlags, pCreationNodeMask, ppPresentQueue);
    }

    [return: NativeTypeName("HRESULT")]
    public int SetHDRMetaData(DXGI_HDR_METADATA_TYPE Type, uint Size, void* pMetaData)
    {
        return ((delegate* unmanaged[Stdcall]<IDXGISwapChain4*, DXGI_HDR_METADATA_TYPE, uint, void*, int>)(lpVtbl[40]))((IDXGISwapChain4*)Unsafe.AsPointer(ref this), Type, Size, pMetaData);
    }

    public interface Interface : IDXGISwapChain3.Interface
    {
        [return: NativeTypeName("HRESULT")]
        int SetHDRMetaData(DXGI_HDR_METADATA_TYPE Type, uint Size, void* pMetaData);
    }
}

internal enum DXGI_OFFER_RESOURCE_FLAGS
{
    DXGI_OFFER_RESOURCE_FLAG_ALLOW_DECOMMIT = 0x1,
}

internal enum DXGI_RECLAIM_RESOURCE_RESULTS
{
    DXGI_RECLAIM_RESOURCE_RESULT_OK = 0,
    DXGI_RECLAIM_RESOURCE_RESULT_DISCARDED = 1,
    DXGI_RECLAIM_RESOURCE_RESULT_NOT_COMMITTED = 2,
}

[Guid("95B4F95F-D8DA-4CA4-9EE6-3B76D5968A10")]
[NativeTypeName("struct IDXGIDevice4 : IDXGIDevice3")]
internal unsafe partial struct IDXGIDevice4 : IDXGIDevice4.Interface, INativeGuid
{
    static Guid* INativeGuid.NativeGuid => (Guid*)Unsafe.AsPointer(in IID_IDXGIDevice4);

    public void** lpVtbl;

    [return: NativeTypeName("HRESULT")]
    public int QueryInterface([NativeTypeName("const IID &")] Guid* riid, void** ppvObject)
    {
        return ((delegate* unmanaged[Stdcall]<IDXGIDevice4*, Guid*, void**, int>)(lpVtbl[0]))((IDXGIDevice4*)Unsafe.AsPointer(ref this), riid, ppvObject);
    }

    [return: NativeTypeName("ULONG")]
    public uint AddRef()
    {
        return ((delegate* unmanaged[Stdcall]<IDXGIDevice4*, uint>)(lpVtbl[1]))((IDXGIDevice4*)Unsafe.AsPointer(ref this));
    }

    [return: NativeTypeName("ULONG")]
    public uint Release()
    {
        return ((delegate* unmanaged[Stdcall]<IDXGIDevice4*, uint>)(lpVtbl[2]))((IDXGIDevice4*)Unsafe.AsPointer(ref this));
    }

    [return: NativeTypeName("HRESULT")]
    public int SetPrivateData([NativeTypeName("const GUID &")] Guid* Name, uint DataSize, [NativeTypeName("const void *")] void* pData)
    {
        return ((delegate* unmanaged[Stdcall]<IDXGIDevice4*, Guid*, uint, void*, int>)(lpVtbl[3]))((IDXGIDevice4*)Unsafe.AsPointer(ref this), Name, DataSize, pData);
    }

    [return: NativeTypeName("HRESULT")]
    public int SetPrivateDataInterface([NativeTypeName("const GUID &")] Guid* Name, [NativeTypeName("const IUnknown *")] IUnknown* pUnknown)
    {
        return ((delegate* unmanaged[Stdcall]<IDXGIDevice4*, Guid*, IUnknown*, int>)(lpVtbl[4]))((IDXGIDevice4*)Unsafe.AsPointer(ref this), Name, pUnknown);
    }

    [return: NativeTypeName("HRESULT")]
    public int GetPrivateData([NativeTypeName("const GUID &")] Guid* Name, uint* pDataSize, void* pData)
    {
        return ((delegate* unmanaged[Stdcall]<IDXGIDevice4*, Guid*, uint*, void*, int>)(lpVtbl[5]))((IDXGIDevice4*)Unsafe.AsPointer(ref this), Name, pDataSize, pData);
    }

    [return: NativeTypeName("HRESULT")]
    public int GetParent([NativeTypeName("const IID &")] Guid* riid, void** ppParent)
    {
        return ((delegate* unmanaged[Stdcall]<IDXGIDevice4*, Guid*, void**, int>)(lpVtbl[6]))((IDXGIDevice4*)Unsafe.AsPointer(ref this), riid, ppParent);
    }

    [return: NativeTypeName("HRESULT")]
    public int GetAdapter(IDXGIAdapter** pAdapter)
    {
        return ((delegate* unmanaged[Stdcall]<IDXGIDevice4*, IDXGIAdapter**, int>)(lpVtbl[7]))((IDXGIDevice4*)Unsafe.AsPointer(ref this), pAdapter);
    }

    [return: NativeTypeName("HRESULT")]
    public int CreateSurface([NativeTypeName("const DXGI_SURFACE_DESC *")] DXGI_SURFACE_DESC* pDesc, uint NumSurfaces, [NativeTypeName("DXGI_USAGE")] uint Usage, [NativeTypeName("const DXGI_SHARED_RESOURCE *")] DXGI_SHARED_RESOURCE* pSharedResource, IDXGISurface** ppSurface)
    {
        return ((delegate* unmanaged[Stdcall]<IDXGIDevice4*, DXGI_SURFACE_DESC*, uint, uint, DXGI_SHARED_RESOURCE*, IDXGISurface**, int>)(lpVtbl[8]))((IDXGIDevice4*)Unsafe.AsPointer(ref this), pDesc, NumSurfaces, Usage, pSharedResource, ppSurface);
    }

    [return: NativeTypeName("HRESULT")]
    public int QueryResourceResidency([NativeTypeName("IUnknown *const *")] IUnknown** ppResources, DXGI_RESIDENCY* pResidencyStatus, uint NumResources)
    {
        return ((delegate* unmanaged[Stdcall]<IDXGIDevice4*, IUnknown**, DXGI_RESIDENCY*, uint, int>)(lpVtbl[9]))((IDXGIDevice4*)Unsafe.AsPointer(ref this), ppResources, pResidencyStatus, NumResources);
    }

    [return: NativeTypeName("HRESULT")]
    public int SetGPUThreadPriority(int Priority)
    {
        return ((delegate* unmanaged[Stdcall]<IDXGIDevice4*, int, int>)(lpVtbl[10]))((IDXGIDevice4*)Unsafe.AsPointer(ref this), Priority);
    }

    [return: NativeTypeName("HRESULT")]
    public int GetGPUThreadPriority(int* pPriority)
    {
        return ((delegate* unmanaged[Stdcall]<IDXGIDevice4*, int*, int>)(lpVtbl[11]))((IDXGIDevice4*)Unsafe.AsPointer(ref this), pPriority);
    }

    [return: NativeTypeName("HRESULT")]
    public int SetMaximumFrameLatency(uint MaxLatency)
    {
        return ((delegate* unmanaged[Stdcall]<IDXGIDevice4*, uint, int>)(lpVtbl[12]))((IDXGIDevice4*)Unsafe.AsPointer(ref this), MaxLatency);
    }

    [return: NativeTypeName("HRESULT")]
    public int GetMaximumFrameLatency(uint* pMaxLatency)
    {
        return ((delegate* unmanaged[Stdcall]<IDXGIDevice4*, uint*, int>)(lpVtbl[13]))((IDXGIDevice4*)Unsafe.AsPointer(ref this), pMaxLatency);
    }

    [return: NativeTypeName("HRESULT")]
    public int OfferResources(uint NumResources, [NativeTypeName("IDXGIResource *const *")] IDXGIResource** ppResources, DXGI_OFFER_RESOURCE_PRIORITY Priority)
    {
        return ((delegate* unmanaged[Stdcall]<IDXGIDevice4*, uint, IDXGIResource**, DXGI_OFFER_RESOURCE_PRIORITY, int>)(lpVtbl[14]))((IDXGIDevice4*)Unsafe.AsPointer(ref this), NumResources, ppResources, Priority);
    }

    [return: NativeTypeName("HRESULT")]
    public int ReclaimResources(uint NumResources, [NativeTypeName("IDXGIResource *const *")] IDXGIResource** ppResources, [NativeTypeName("BOOL *")] int* pDiscarded)
    {
        return ((delegate* unmanaged[Stdcall]<IDXGIDevice4*, uint, IDXGIResource**, int*, int>)(lpVtbl[15]))((IDXGIDevice4*)Unsafe.AsPointer(ref this), NumResources, ppResources, pDiscarded);
    }

    [return: NativeTypeName("HRESULT")]
    public int EnqueueSetEvent([NativeTypeName("HANDLE")] void* hEvent)
    {
        return ((delegate* unmanaged[Stdcall]<IDXGIDevice4*, void*, int>)(lpVtbl[16]))((IDXGIDevice4*)Unsafe.AsPointer(ref this), hEvent);
    }

    public void Trim()
    {
        ((delegate* unmanaged[Stdcall]<IDXGIDevice4*, void>)(lpVtbl[17]))((IDXGIDevice4*)Unsafe.AsPointer(ref this));
    }

    [return: NativeTypeName("HRESULT")]
    public int OfferResources1(uint NumResources, [NativeTypeName("IDXGIResource *const *")] IDXGIResource** ppResources, DXGI_OFFER_RESOURCE_PRIORITY Priority, uint Flags)
    {
        return ((delegate* unmanaged[Stdcall]<IDXGIDevice4*, uint, IDXGIResource**, DXGI_OFFER_RESOURCE_PRIORITY, uint, int>)(lpVtbl[18]))((IDXGIDevice4*)Unsafe.AsPointer(ref this), NumResources, ppResources, Priority, Flags);
    }

    [return: NativeTypeName("HRESULT")]
    public int ReclaimResources1(uint NumResources, [NativeTypeName("IDXGIResource *const *")] IDXGIResource** ppResources, DXGI_RECLAIM_RESOURCE_RESULTS* pResults)
    {
        return ((delegate* unmanaged[Stdcall]<IDXGIDevice4*, uint, IDXGIResource**, DXGI_RECLAIM_RESOURCE_RESULTS*, int>)(lpVtbl[19]))((IDXGIDevice4*)Unsafe.AsPointer(ref this), NumResources, ppResources, pResults);
    }

    public interface Interface : IDXGIDevice3.Interface
    {
        [return: NativeTypeName("HRESULT")]
        int OfferResources1(uint NumResources, [NativeTypeName("IDXGIResource *const *")] IDXGIResource** ppResources, DXGI_OFFER_RESOURCE_PRIORITY Priority, uint Flags);

        [return: NativeTypeName("HRESULT")]
        int ReclaimResources1(uint NumResources, [NativeTypeName("IDXGIResource *const *")] IDXGIResource** ppResources, DXGI_RECLAIM_RESOURCE_RESULTS* pResults);
    }
}

internal enum DXGI_FEATURE
{
    DXGI_FEATURE_PRESENT_ALLOW_TEARING = 0,
}

[Guid("7632E1F5-EE65-4DCA-87FD-84CD75F8838D")]
[NativeTypeName("struct IDXGIFactory5 : IDXGIFactory4")]
internal unsafe partial struct IDXGIFactory5 : IDXGIFactory5.Interface, INativeGuid
{
    static Guid* INativeGuid.NativeGuid => (Guid*)Unsafe.AsPointer(in IID_IDXGIFactory5);

    public void** lpVtbl;

    [return: NativeTypeName("HRESULT")]
    public int QueryInterface([NativeTypeName("const IID &")] Guid* riid, void** ppvObject)
    {
        return ((delegate* unmanaged[Stdcall]<IDXGIFactory5*, Guid*, void**, int>)(lpVtbl[0]))((IDXGIFactory5*)Unsafe.AsPointer(ref this), riid, ppvObject);
    }

    [return: NativeTypeName("ULONG")]
    public uint AddRef()
    {
        return ((delegate* unmanaged[Stdcall]<IDXGIFactory5*, uint>)(lpVtbl[1]))((IDXGIFactory5*)Unsafe.AsPointer(ref this));
    }

    [return: NativeTypeName("ULONG")]
    public uint Release()
    {
        return ((delegate* unmanaged[Stdcall]<IDXGIFactory5*, uint>)(lpVtbl[2]))((IDXGIFactory5*)Unsafe.AsPointer(ref this));
    }

    [return: NativeTypeName("HRESULT")]
    public int SetPrivateData([NativeTypeName("const GUID &")] Guid* Name, uint DataSize, [NativeTypeName("const void *")] void* pData)
    {
        return ((delegate* unmanaged[Stdcall]<IDXGIFactory5*, Guid*, uint, void*, int>)(lpVtbl[3]))((IDXGIFactory5*)Unsafe.AsPointer(ref this), Name, DataSize, pData);
    }

    [return: NativeTypeName("HRESULT")]
    public int SetPrivateDataInterface([NativeTypeName("const GUID &")] Guid* Name, [NativeTypeName("const IUnknown *")] IUnknown* pUnknown)
    {
        return ((delegate* unmanaged[Stdcall]<IDXGIFactory5*, Guid*, IUnknown*, int>)(lpVtbl[4]))((IDXGIFactory5*)Unsafe.AsPointer(ref this), Name, pUnknown);
    }

    [return: NativeTypeName("HRESULT")]
    public int GetPrivateData([NativeTypeName("const GUID &")] Guid* Name, uint* pDataSize, void* pData)
    {
        return ((delegate* unmanaged[Stdcall]<IDXGIFactory5*, Guid*, uint*, void*, int>)(lpVtbl[5]))((IDXGIFactory5*)Unsafe.AsPointer(ref this), Name, pDataSize, pData);
    }

    [return: NativeTypeName("HRESULT")]
    public int GetParent([NativeTypeName("const IID &")] Guid* riid, void** ppParent)
    {
        return ((delegate* unmanaged[Stdcall]<IDXGIFactory5*, Guid*, void**, int>)(lpVtbl[6]))((IDXGIFactory5*)Unsafe.AsPointer(ref this), riid, ppParent);
    }

    [return: NativeTypeName("HRESULT")]
    public int EnumAdapters(uint Adapter, IDXGIAdapter** ppAdapter)
    {
        return ((delegate* unmanaged[Stdcall]<IDXGIFactory5*, uint, IDXGIAdapter**, int>)(lpVtbl[7]))((IDXGIFactory5*)Unsafe.AsPointer(ref this), Adapter, ppAdapter);
    }

    [return: NativeTypeName("HRESULT")]
    public int MakeWindowAssociation([NativeTypeName("HWND")] HWND__* WindowHandle, uint Flags)
    {
        return ((delegate* unmanaged[Stdcall]<IDXGIFactory5*, HWND__*, uint, int>)(lpVtbl[8]))((IDXGIFactory5*)Unsafe.AsPointer(ref this), WindowHandle, Flags);
    }

    [return: NativeTypeName("HRESULT")]
    public int GetWindowAssociation([NativeTypeName("HWND *")] HWND__** pWindowHandle)
    {
        return ((delegate* unmanaged[Stdcall]<IDXGIFactory5*, HWND__**, int>)(lpVtbl[9]))((IDXGIFactory5*)Unsafe.AsPointer(ref this), pWindowHandle);
    }

    [return: NativeTypeName("HRESULT")]
    public int CreateSwapChain(IUnknown* pDevice, DXGI_SWAP_CHAIN_DESC* pDesc, IDXGISwapChain** ppSwapChain)
    {
        return ((delegate* unmanaged[Stdcall]<IDXGIFactory5*, IUnknown*, DXGI_SWAP_CHAIN_DESC*, IDXGISwapChain**, int>)(lpVtbl[10]))((IDXGIFactory5*)Unsafe.AsPointer(ref this), pDevice, pDesc, ppSwapChain);
    }

    [return: NativeTypeName("HRESULT")]
    public int CreateSoftwareAdapter([NativeTypeName("HMODULE")] HINSTANCE__* Module, IDXGIAdapter** ppAdapter)
    {
        return ((delegate* unmanaged[Stdcall]<IDXGIFactory5*, HINSTANCE__*, IDXGIAdapter**, int>)(lpVtbl[11]))((IDXGIFactory5*)Unsafe.AsPointer(ref this), Module, ppAdapter);
    }

    [return: NativeTypeName("HRESULT")]
    public int EnumAdapters1(uint Adapter, IDXGIAdapter1** ppAdapter)
    {
        return ((delegate* unmanaged[Stdcall]<IDXGIFactory5*, uint, IDXGIAdapter1**, int>)(lpVtbl[12]))((IDXGIFactory5*)Unsafe.AsPointer(ref this), Adapter, ppAdapter);
    }

    [return: NativeTypeName("BOOL")]
    public int IsCurrent()
    {
        return ((delegate* unmanaged[Stdcall]<IDXGIFactory5*, int>)(lpVtbl[13]))((IDXGIFactory5*)Unsafe.AsPointer(ref this));
    }

    [return: NativeTypeName("BOOL")]
    public int IsWindowedStereoEnabled()
    {
        return ((delegate* unmanaged[Stdcall]<IDXGIFactory5*, int>)(lpVtbl[14]))((IDXGIFactory5*)Unsafe.AsPointer(ref this));
    }

    [return: NativeTypeName("HRESULT")]
    public int CreateSwapChainForHwnd(IUnknown* pDevice, [NativeTypeName("HWND")] HWND__* hWnd, [NativeTypeName("const DXGI_SWAP_CHAIN_DESC1 *")] DXGI_SWAP_CHAIN_DESC1* pDesc, [NativeTypeName("const DXGI_SWAP_CHAIN_FULLSCREEN_DESC *")] DXGI_SWAP_CHAIN_FULLSCREEN_DESC* pFullscreenDesc, IDXGIOutput* pRestrictToOutput, IDXGISwapChain1** ppSwapChain)
    {
        return ((delegate* unmanaged[Stdcall]<IDXGIFactory5*, IUnknown*, HWND__*, DXGI_SWAP_CHAIN_DESC1*, DXGI_SWAP_CHAIN_FULLSCREEN_DESC*, IDXGIOutput*, IDXGISwapChain1**, int>)(lpVtbl[15]))((IDXGIFactory5*)Unsafe.AsPointer(ref this), pDevice, hWnd, pDesc, pFullscreenDesc, pRestrictToOutput, ppSwapChain);
    }

    [return: NativeTypeName("HRESULT")]
    public int CreateSwapChainForCoreWindow(IUnknown* pDevice, IUnknown* pWindow, [NativeTypeName("const DXGI_SWAP_CHAIN_DESC1 *")] DXGI_SWAP_CHAIN_DESC1* pDesc, IDXGIOutput* pRestrictToOutput, IDXGISwapChain1** ppSwapChain)
    {
        return ((delegate* unmanaged[Stdcall]<IDXGIFactory5*, IUnknown*, IUnknown*, DXGI_SWAP_CHAIN_DESC1*, IDXGIOutput*, IDXGISwapChain1**, int>)(lpVtbl[16]))((IDXGIFactory5*)Unsafe.AsPointer(ref this), pDevice, pWindow, pDesc, pRestrictToOutput, ppSwapChain);
    }

    [return: NativeTypeName("HRESULT")]
    public int GetSharedResourceAdapterLuid([NativeTypeName("HANDLE")] void* hResource, LUID* pLuid)
    {
        return ((delegate* unmanaged[Stdcall]<IDXGIFactory5*, void*, LUID*, int>)(lpVtbl[17]))((IDXGIFactory5*)Unsafe.AsPointer(ref this), hResource, pLuid);
    }

    [return: NativeTypeName("HRESULT")]
    public int RegisterStereoStatusWindow([NativeTypeName("HWND")] HWND__* WindowHandle, uint wMsg, [NativeTypeName("DWORD *")] uint* pdwCookie)
    {
        return ((delegate* unmanaged[Stdcall]<IDXGIFactory5*, HWND__*, uint, uint*, int>)(lpVtbl[18]))((IDXGIFactory5*)Unsafe.AsPointer(ref this), WindowHandle, wMsg, pdwCookie);
    }

    [return: NativeTypeName("HRESULT")]
    public int RegisterStereoStatusEvent([NativeTypeName("HANDLE")] void* hEvent, [NativeTypeName("DWORD *")] uint* pdwCookie)
    {
        return ((delegate* unmanaged[Stdcall]<IDXGIFactory5*, void*, uint*, int>)(lpVtbl[19]))((IDXGIFactory5*)Unsafe.AsPointer(ref this), hEvent, pdwCookie);
    }

    public void UnregisterStereoStatus([NativeTypeName("DWORD")] uint dwCookie)
    {
        ((delegate* unmanaged[Stdcall]<IDXGIFactory5*, uint, void>)(lpVtbl[20]))((IDXGIFactory5*)Unsafe.AsPointer(ref this), dwCookie);
    }

    [return: NativeTypeName("HRESULT")]
    public int RegisterOcclusionStatusWindow([NativeTypeName("HWND")] HWND__* WindowHandle, uint wMsg, [NativeTypeName("DWORD *")] uint* pdwCookie)
    {
        return ((delegate* unmanaged[Stdcall]<IDXGIFactory5*, HWND__*, uint, uint*, int>)(lpVtbl[21]))((IDXGIFactory5*)Unsafe.AsPointer(ref this), WindowHandle, wMsg, pdwCookie);
    }

    [return: NativeTypeName("HRESULT")]
    public int RegisterOcclusionStatusEvent([NativeTypeName("HANDLE")] void* hEvent, [NativeTypeName("DWORD *")] uint* pdwCookie)
    {
        return ((delegate* unmanaged[Stdcall]<IDXGIFactory5*, void*, uint*, int>)(lpVtbl[22]))((IDXGIFactory5*)Unsafe.AsPointer(ref this), hEvent, pdwCookie);
    }

    public void UnregisterOcclusionStatus([NativeTypeName("DWORD")] uint dwCookie)
    {
        ((delegate* unmanaged[Stdcall]<IDXGIFactory5*, uint, void>)(lpVtbl[23]))((IDXGIFactory5*)Unsafe.AsPointer(ref this), dwCookie);
    }

    [return: NativeTypeName("HRESULT")]
    public int CreateSwapChainForComposition(IUnknown* pDevice, [NativeTypeName("const DXGI_SWAP_CHAIN_DESC1 *")] DXGI_SWAP_CHAIN_DESC1* pDesc, IDXGIOutput* pRestrictToOutput, IDXGISwapChain1** ppSwapChain)
    {
        return ((delegate* unmanaged[Stdcall]<IDXGIFactory5*, IUnknown*, DXGI_SWAP_CHAIN_DESC1*, IDXGIOutput*, IDXGISwapChain1**, int>)(lpVtbl[24]))((IDXGIFactory5*)Unsafe.AsPointer(ref this), pDevice, pDesc, pRestrictToOutput, ppSwapChain);
    }

    public uint GetCreationFlags()
    {
        return ((delegate* unmanaged[Stdcall]<IDXGIFactory5*, uint>)(lpVtbl[25]))((IDXGIFactory5*)Unsafe.AsPointer(ref this));
    }

    [return: NativeTypeName("HRESULT")]
    public int EnumAdapterByLuid(LUID AdapterLuid, [NativeTypeName("const IID &")] Guid* riid, void** ppvAdapter)
    {
        return ((delegate* unmanaged[Stdcall]<IDXGIFactory5*, LUID, Guid*, void**, int>)(lpVtbl[26]))((IDXGIFactory5*)Unsafe.AsPointer(ref this), AdapterLuid, riid, ppvAdapter);
    }

    [return: NativeTypeName("HRESULT")]
    public int EnumWarpAdapter([NativeTypeName("const IID &")] Guid* riid, void** ppvAdapter)
    {
        return ((delegate* unmanaged[Stdcall]<IDXGIFactory5*, Guid*, void**, int>)(lpVtbl[27]))((IDXGIFactory5*)Unsafe.AsPointer(ref this), riid, ppvAdapter);
    }

    [return: NativeTypeName("HRESULT")]
    public int CheckFeatureSupport(DXGI_FEATURE Feature, void* pFeatureSupportData, uint FeatureSupportDataSize)
    {
        return ((delegate* unmanaged[Stdcall]<IDXGIFactory5*, DXGI_FEATURE, void*, uint, int>)(lpVtbl[28]))((IDXGIFactory5*)Unsafe.AsPointer(ref this), Feature, pFeatureSupportData, FeatureSupportDataSize);
    }

    public interface Interface : IDXGIFactory4.Interface
    {
        [return: NativeTypeName("HRESULT")]
        int CheckFeatureSupport(DXGI_FEATURE Feature, void* pFeatureSupportData, uint FeatureSupportDataSize);
    }
}

internal enum DXGI_ADAPTER_FLAG3
{
    DXGI_ADAPTER_FLAG3_NONE = 0,
    DXGI_ADAPTER_FLAG3_REMOTE = 1,
    DXGI_ADAPTER_FLAG3_SOFTWARE = 2,
    DXGI_ADAPTER_FLAG3_ACG_COMPATIBLE = 4,
    DXGI_ADAPTER_FLAG3_SUPPORT_MONITORED_FENCES = 8,
    DXGI_ADAPTER_FLAG3_SUPPORT_NON_MONITORED_FENCES = 0x10,
    DXGI_ADAPTER_FLAG3_KEYED_MUTEX_CONFORMANCE = 0x20,
    DXGI_ADAPTER_FLAG3_FORCE_DWORD = unchecked((int)(0xffffffff)),
}

internal partial struct DXGI_ADAPTER_DESC3
{
    [NativeTypeName("WCHAR[128]")]
    public _Description_e__FixedBuffer Description;

    public uint VendorId;

    public uint DeviceId;

    public uint SubSysId;

    public uint Revision;

    [NativeTypeName("SIZE_T")]
    public nuint DedicatedVideoMemory;

    [NativeTypeName("SIZE_T")]
    public nuint DedicatedSystemMemory;

    [NativeTypeName("SIZE_T")]
    public nuint SharedSystemMemory;

    public LUID AdapterLuid;

    public DXGI_ADAPTER_FLAG3 Flags;

    public DXGI_GRAPHICS_PREEMPTION_GRANULARITY GraphicsPreemptionGranularity;

    public DXGI_COMPUTE_PREEMPTION_GRANULARITY ComputePreemptionGranularity;

    [InlineArray(128)]
    public partial struct _Description_e__FixedBuffer
    {
        public ushort e0;
    }
}

[Guid("3C8D99D1-4FBF-4181-A82C-AF66BF7BD24E")]
[NativeTypeName("struct IDXGIAdapter4 : IDXGIAdapter3")]
internal unsafe partial struct IDXGIAdapter4 : IDXGIAdapter4.Interface, INativeGuid
{
    static Guid* INativeGuid.NativeGuid => (Guid*)Unsafe.AsPointer(in IID_IDXGIAdapter4);

    public void** lpVtbl;

    [return: NativeTypeName("HRESULT")]
    public int QueryInterface([NativeTypeName("const IID &")] Guid* riid, void** ppvObject)
    {
        return ((delegate* unmanaged[Stdcall]<IDXGIAdapter4*, Guid*, void**, int>)(lpVtbl[0]))((IDXGIAdapter4*)Unsafe.AsPointer(ref this), riid, ppvObject);
    }

    [return: NativeTypeName("ULONG")]
    public uint AddRef()
    {
        return ((delegate* unmanaged[Stdcall]<IDXGIAdapter4*, uint>)(lpVtbl[1]))((IDXGIAdapter4*)Unsafe.AsPointer(ref this));
    }

    [return: NativeTypeName("ULONG")]
    public uint Release()
    {
        return ((delegate* unmanaged[Stdcall]<IDXGIAdapter4*, uint>)(lpVtbl[2]))((IDXGIAdapter4*)Unsafe.AsPointer(ref this));
    }

    [return: NativeTypeName("HRESULT")]
    public int SetPrivateData([NativeTypeName("const GUID &")] Guid* Name, uint DataSize, [NativeTypeName("const void *")] void* pData)
    {
        return ((delegate* unmanaged[Stdcall]<IDXGIAdapter4*, Guid*, uint, void*, int>)(lpVtbl[3]))((IDXGIAdapter4*)Unsafe.AsPointer(ref this), Name, DataSize, pData);
    }

    [return: NativeTypeName("HRESULT")]
    public int SetPrivateDataInterface([NativeTypeName("const GUID &")] Guid* Name, [NativeTypeName("const IUnknown *")] IUnknown* pUnknown)
    {
        return ((delegate* unmanaged[Stdcall]<IDXGIAdapter4*, Guid*, IUnknown*, int>)(lpVtbl[4]))((IDXGIAdapter4*)Unsafe.AsPointer(ref this), Name, pUnknown);
    }

    [return: NativeTypeName("HRESULT")]
    public int GetPrivateData([NativeTypeName("const GUID &")] Guid* Name, uint* pDataSize, void* pData)
    {
        return ((delegate* unmanaged[Stdcall]<IDXGIAdapter4*, Guid*, uint*, void*, int>)(lpVtbl[5]))((IDXGIAdapter4*)Unsafe.AsPointer(ref this), Name, pDataSize, pData);
    }

    [return: NativeTypeName("HRESULT")]
    public int GetParent([NativeTypeName("const IID &")] Guid* riid, void** ppParent)
    {
        return ((delegate* unmanaged[Stdcall]<IDXGIAdapter4*, Guid*, void**, int>)(lpVtbl[6]))((IDXGIAdapter4*)Unsafe.AsPointer(ref this), riid, ppParent);
    }

    [return: NativeTypeName("HRESULT")]
    public int EnumOutputs(uint Output, IDXGIOutput** ppOutput)
    {
        return ((delegate* unmanaged[Stdcall]<IDXGIAdapter4*, uint, IDXGIOutput**, int>)(lpVtbl[7]))((IDXGIAdapter4*)Unsafe.AsPointer(ref this), Output, ppOutput);
    }

    [return: NativeTypeName("HRESULT")]
    public int GetDesc(DXGI_ADAPTER_DESC* pDesc)
    {
        return ((delegate* unmanaged[Stdcall]<IDXGIAdapter4*, DXGI_ADAPTER_DESC*, int>)(lpVtbl[8]))((IDXGIAdapter4*)Unsafe.AsPointer(ref this), pDesc);
    }

    [return: NativeTypeName("HRESULT")]
    public int CheckInterfaceSupport([NativeTypeName("const GUID &")] Guid* InterfaceName, LARGE_INTEGER* pUMDVersion)
    {
        return ((delegate* unmanaged[Stdcall]<IDXGIAdapter4*, Guid*, LARGE_INTEGER*, int>)(lpVtbl[9]))((IDXGIAdapter4*)Unsafe.AsPointer(ref this), InterfaceName, pUMDVersion);
    }

    [return: NativeTypeName("HRESULT")]
    public int GetDesc1(DXGI_ADAPTER_DESC1* pDesc)
    {
        return ((delegate* unmanaged[Stdcall]<IDXGIAdapter4*, DXGI_ADAPTER_DESC1*, int>)(lpVtbl[10]))((IDXGIAdapter4*)Unsafe.AsPointer(ref this), pDesc);
    }

    [return: NativeTypeName("HRESULT")]
    public int GetDesc2(DXGI_ADAPTER_DESC2* pDesc)
    {
        return ((delegate* unmanaged[Stdcall]<IDXGIAdapter4*, DXGI_ADAPTER_DESC2*, int>)(lpVtbl[11]))((IDXGIAdapter4*)Unsafe.AsPointer(ref this), pDesc);
    }

    [return: NativeTypeName("HRESULT")]
    public int RegisterHardwareContentProtectionTeardownStatusEvent([NativeTypeName("HANDLE")] void* hEvent, [NativeTypeName("DWORD *")] uint* pdwCookie)
    {
        return ((delegate* unmanaged[Stdcall]<IDXGIAdapter4*, void*, uint*, int>)(lpVtbl[12]))((IDXGIAdapter4*)Unsafe.AsPointer(ref this), hEvent, pdwCookie);
    }

    public void UnregisterHardwareContentProtectionTeardownStatus([NativeTypeName("DWORD")] uint dwCookie)
    {
        ((delegate* unmanaged[Stdcall]<IDXGIAdapter4*, uint, void>)(lpVtbl[13]))((IDXGIAdapter4*)Unsafe.AsPointer(ref this), dwCookie);
    }

    [return: NativeTypeName("HRESULT")]
    public int QueryVideoMemoryInfo(uint NodeIndex, DXGI_MEMORY_SEGMENT_GROUP MemorySegmentGroup, DXGI_QUERY_VIDEO_MEMORY_INFO* pVideoMemoryInfo)
    {
        return ((delegate* unmanaged[Stdcall]<IDXGIAdapter4*, uint, DXGI_MEMORY_SEGMENT_GROUP, DXGI_QUERY_VIDEO_MEMORY_INFO*, int>)(lpVtbl[14]))((IDXGIAdapter4*)Unsafe.AsPointer(ref this), NodeIndex, MemorySegmentGroup, pVideoMemoryInfo);
    }

    [return: NativeTypeName("HRESULT")]
    public int SetVideoMemoryReservation(uint NodeIndex, DXGI_MEMORY_SEGMENT_GROUP MemorySegmentGroup, [NativeTypeName("UINT64")] ulong Reservation)
    {
        return ((delegate* unmanaged[Stdcall]<IDXGIAdapter4*, uint, DXGI_MEMORY_SEGMENT_GROUP, ulong, int>)(lpVtbl[15]))((IDXGIAdapter4*)Unsafe.AsPointer(ref this), NodeIndex, MemorySegmentGroup, Reservation);
    }

    [return: NativeTypeName("HRESULT")]
    public int RegisterVideoMemoryBudgetChangeNotificationEvent([NativeTypeName("HANDLE")] void* hEvent, [NativeTypeName("DWORD *")] uint* pdwCookie)
    {
        return ((delegate* unmanaged[Stdcall]<IDXGIAdapter4*, void*, uint*, int>)(lpVtbl[16]))((IDXGIAdapter4*)Unsafe.AsPointer(ref this), hEvent, pdwCookie);
    }

    public void UnregisterVideoMemoryBudgetChangeNotification([NativeTypeName("DWORD")] uint dwCookie)
    {
        ((delegate* unmanaged[Stdcall]<IDXGIAdapter4*, uint, void>)(lpVtbl[17]))((IDXGIAdapter4*)Unsafe.AsPointer(ref this), dwCookie);
    }

    [return: NativeTypeName("HRESULT")]
    public int GetDesc3(DXGI_ADAPTER_DESC3* pDesc)
    {
        return ((delegate* unmanaged[Stdcall]<IDXGIAdapter4*, DXGI_ADAPTER_DESC3*, int>)(lpVtbl[18]))((IDXGIAdapter4*)Unsafe.AsPointer(ref this), pDesc);
    }

    public interface Interface : IDXGIAdapter3.Interface
    {
        [return: NativeTypeName("HRESULT")]
        int GetDesc3(DXGI_ADAPTER_DESC3* pDesc);
    }
}

internal unsafe partial struct DXGI_OUTPUT_DESC1
{
    [NativeTypeName("WCHAR[32]")]
    public _DeviceName_e__FixedBuffer DeviceName;

    public RECT DesktopCoordinates;

    [NativeTypeName("BOOL")]
    public int AttachedToDesktop;

    public DXGI_MODE_ROTATION Rotation;

    [NativeTypeName("HMONITOR")]
    public HMONITOR__* Monitor;

    public uint BitsPerColor;

    public DXGI_COLOR_SPACE_TYPE ColorSpace;

    [NativeTypeName("FLOAT[2]")]
    public _RedPrimary_e__FixedBuffer RedPrimary;

    [NativeTypeName("FLOAT[2]")]
    public _GreenPrimary_e__FixedBuffer GreenPrimary;

    [NativeTypeName("FLOAT[2]")]
    public _BluePrimary_e__FixedBuffer BluePrimary;

    [NativeTypeName("FLOAT[2]")]
    public _WhitePoint_e__FixedBuffer WhitePoint;

    public float MinLuminance;

    public float MaxLuminance;

    public float MaxFullFrameLuminance;

    [InlineArray(32)]
    public partial struct _DeviceName_e__FixedBuffer
    {
        public ushort e0;
    }

    [InlineArray(2)]
    public partial struct _RedPrimary_e__FixedBuffer
    {
        public float e0;
    }

    [InlineArray(2)]
    public partial struct _GreenPrimary_e__FixedBuffer
    {
        public float e0;
    }

    [InlineArray(2)]
    public partial struct _BluePrimary_e__FixedBuffer
    {
        public float e0;
    }

    [InlineArray(2)]
    public partial struct _WhitePoint_e__FixedBuffer
    {
        public float e0;
    }
}

internal enum DXGI_HARDWARE_COMPOSITION_SUPPORT_FLAGS
{
    DXGI_HARDWARE_COMPOSITION_SUPPORT_FLAG_FULLSCREEN = 1,
    DXGI_HARDWARE_COMPOSITION_SUPPORT_FLAG_WINDOWED = 2,
    DXGI_HARDWARE_COMPOSITION_SUPPORT_FLAG_CURSOR_STRETCHED = 4,
}

[Guid("068346E8-AAEC-4B84-ADD7-137F513F77A1")]
[NativeTypeName("struct IDXGIOutput6 : IDXGIOutput5")]
internal unsafe partial struct IDXGIOutput6 : IDXGIOutput6.Interface, INativeGuid
{
    static Guid* INativeGuid.NativeGuid => (Guid*)Unsafe.AsPointer(in IID_IDXGIOutput6);

    public void** lpVtbl;

    [return: NativeTypeName("HRESULT")]
    public int QueryInterface([NativeTypeName("const IID &")] Guid* riid, void** ppvObject)
    {
        return ((delegate* unmanaged[Stdcall]<IDXGIOutput6*, Guid*, void**, int>)(lpVtbl[0]))((IDXGIOutput6*)Unsafe.AsPointer(ref this), riid, ppvObject);
    }

    [return: NativeTypeName("ULONG")]
    public uint AddRef()
    {
        return ((delegate* unmanaged[Stdcall]<IDXGIOutput6*, uint>)(lpVtbl[1]))((IDXGIOutput6*)Unsafe.AsPointer(ref this));
    }

    [return: NativeTypeName("ULONG")]
    public uint Release()
    {
        return ((delegate* unmanaged[Stdcall]<IDXGIOutput6*, uint>)(lpVtbl[2]))((IDXGIOutput6*)Unsafe.AsPointer(ref this));
    }

    [return: NativeTypeName("HRESULT")]
    public int SetPrivateData([NativeTypeName("const GUID &")] Guid* Name, uint DataSize, [NativeTypeName("const void *")] void* pData)
    {
        return ((delegate* unmanaged[Stdcall]<IDXGIOutput6*, Guid*, uint, void*, int>)(lpVtbl[3]))((IDXGIOutput6*)Unsafe.AsPointer(ref this), Name, DataSize, pData);
    }

    [return: NativeTypeName("HRESULT")]
    public int SetPrivateDataInterface([NativeTypeName("const GUID &")] Guid* Name, [NativeTypeName("const IUnknown *")] IUnknown* pUnknown)
    {
        return ((delegate* unmanaged[Stdcall]<IDXGIOutput6*, Guid*, IUnknown*, int>)(lpVtbl[4]))((IDXGIOutput6*)Unsafe.AsPointer(ref this), Name, pUnknown);
    }

    [return: NativeTypeName("HRESULT")]
    public int GetPrivateData([NativeTypeName("const GUID &")] Guid* Name, uint* pDataSize, void* pData)
    {
        return ((delegate* unmanaged[Stdcall]<IDXGIOutput6*, Guid*, uint*, void*, int>)(lpVtbl[5]))((IDXGIOutput6*)Unsafe.AsPointer(ref this), Name, pDataSize, pData);
    }

    [return: NativeTypeName("HRESULT")]
    public int GetParent([NativeTypeName("const IID &")] Guid* riid, void** ppParent)
    {
        return ((delegate* unmanaged[Stdcall]<IDXGIOutput6*, Guid*, void**, int>)(lpVtbl[6]))((IDXGIOutput6*)Unsafe.AsPointer(ref this), riid, ppParent);
    }

    [return: NativeTypeName("HRESULT")]
    public int GetDesc(DXGI_OUTPUT_DESC* pDesc)
    {
        return ((delegate* unmanaged[Stdcall]<IDXGIOutput6*, DXGI_OUTPUT_DESC*, int>)(lpVtbl[7]))((IDXGIOutput6*)Unsafe.AsPointer(ref this), pDesc);
    }

    [return: NativeTypeName("HRESULT")]
    public int GetDisplayModeList(DXGI_FORMAT EnumFormat, uint Flags, uint* pNumModes, DXGI_MODE_DESC* pDesc)
    {
        return ((delegate* unmanaged[Stdcall]<IDXGIOutput6*, DXGI_FORMAT, uint, uint*, DXGI_MODE_DESC*, int>)(lpVtbl[8]))((IDXGIOutput6*)Unsafe.AsPointer(ref this), EnumFormat, Flags, pNumModes, pDesc);
    }

    [return: NativeTypeName("HRESULT")]
    public int FindClosestMatchingMode([NativeTypeName("const DXGI_MODE_DESC *")] DXGI_MODE_DESC* pModeToMatch, DXGI_MODE_DESC* pClosestMatch, IUnknown* pConcernedDevice)
    {
        return ((delegate* unmanaged[Stdcall]<IDXGIOutput6*, DXGI_MODE_DESC*, DXGI_MODE_DESC*, IUnknown*, int>)(lpVtbl[9]))((IDXGIOutput6*)Unsafe.AsPointer(ref this), pModeToMatch, pClosestMatch, pConcernedDevice);
    }

    [return: NativeTypeName("HRESULT")]
    public int WaitForVBlank()
    {
        return ((delegate* unmanaged[Stdcall]<IDXGIOutput6*, int>)(lpVtbl[10]))((IDXGIOutput6*)Unsafe.AsPointer(ref this));
    }

    [return: NativeTypeName("HRESULT")]
    public int TakeOwnership(IUnknown* pDevice, [NativeTypeName("BOOL")] int Exclusive)
    {
        return ((delegate* unmanaged[Stdcall]<IDXGIOutput6*, IUnknown*, int, int>)(lpVtbl[11]))((IDXGIOutput6*)Unsafe.AsPointer(ref this), pDevice, Exclusive);
    }

    public void ReleaseOwnership()
    {
        ((delegate* unmanaged[Stdcall]<IDXGIOutput6*, void>)(lpVtbl[12]))((IDXGIOutput6*)Unsafe.AsPointer(ref this));
    }

    [return: NativeTypeName("HRESULT")]
    public int GetGammaControlCapabilities(DXGI_GAMMA_CONTROL_CAPABILITIES* pGammaCaps)
    {
        return ((delegate* unmanaged[Stdcall]<IDXGIOutput6*, DXGI_GAMMA_CONTROL_CAPABILITIES*, int>)(lpVtbl[13]))((IDXGIOutput6*)Unsafe.AsPointer(ref this), pGammaCaps);
    }

    [return: NativeTypeName("HRESULT")]
    public int SetGammaControl([NativeTypeName("const DXGI_GAMMA_CONTROL *")] DXGI_GAMMA_CONTROL* pArray)
    {
        return ((delegate* unmanaged[Stdcall]<IDXGIOutput6*, DXGI_GAMMA_CONTROL*, int>)(lpVtbl[14]))((IDXGIOutput6*)Unsafe.AsPointer(ref this), pArray);
    }

    [return: NativeTypeName("HRESULT")]
    public int GetGammaControl(DXGI_GAMMA_CONTROL* pArray)
    {
        return ((delegate* unmanaged[Stdcall]<IDXGIOutput6*, DXGI_GAMMA_CONTROL*, int>)(lpVtbl[15]))((IDXGIOutput6*)Unsafe.AsPointer(ref this), pArray);
    }

    [return: NativeTypeName("HRESULT")]
    public int SetDisplaySurface(IDXGISurface* pScanoutSurface)
    {
        return ((delegate* unmanaged[Stdcall]<IDXGIOutput6*, IDXGISurface*, int>)(lpVtbl[16]))((IDXGIOutput6*)Unsafe.AsPointer(ref this), pScanoutSurface);
    }

    [return: NativeTypeName("HRESULT")]
    public int GetDisplaySurfaceData(IDXGISurface* pDestination)
    {
        return ((delegate* unmanaged[Stdcall]<IDXGIOutput6*, IDXGISurface*, int>)(lpVtbl[17]))((IDXGIOutput6*)Unsafe.AsPointer(ref this), pDestination);
    }

    [return: NativeTypeName("HRESULT")]
    public int GetFrameStatistics(DXGI_FRAME_STATISTICS* pStats)
    {
        return ((delegate* unmanaged[Stdcall]<IDXGIOutput6*, DXGI_FRAME_STATISTICS*, int>)(lpVtbl[18]))((IDXGIOutput6*)Unsafe.AsPointer(ref this), pStats);
    }

    [return: NativeTypeName("HRESULT")]
    public int GetDisplayModeList1(DXGI_FORMAT EnumFormat, uint Flags, uint* pNumModes, DXGI_MODE_DESC1* pDesc)
    {
        return ((delegate* unmanaged[Stdcall]<IDXGIOutput6*, DXGI_FORMAT, uint, uint*, DXGI_MODE_DESC1*, int>)(lpVtbl[19]))((IDXGIOutput6*)Unsafe.AsPointer(ref this), EnumFormat, Flags, pNumModes, pDesc);
    }

    [return: NativeTypeName("HRESULT")]
    public int FindClosestMatchingMode1([NativeTypeName("const DXGI_MODE_DESC1 *")] DXGI_MODE_DESC1* pModeToMatch, DXGI_MODE_DESC1* pClosestMatch, IUnknown* pConcernedDevice)
    {
        return ((delegate* unmanaged[Stdcall]<IDXGIOutput6*, DXGI_MODE_DESC1*, DXGI_MODE_DESC1*, IUnknown*, int>)(lpVtbl[20]))((IDXGIOutput6*)Unsafe.AsPointer(ref this), pModeToMatch, pClosestMatch, pConcernedDevice);
    }

    [return: NativeTypeName("HRESULT")]
    public int GetDisplaySurfaceData1(IDXGIResource* pDestination)
    {
        return ((delegate* unmanaged[Stdcall]<IDXGIOutput6*, IDXGIResource*, int>)(lpVtbl[21]))((IDXGIOutput6*)Unsafe.AsPointer(ref this), pDestination);
    }

    [return: NativeTypeName("HRESULT")]
    public int DuplicateOutput(IUnknown* pDevice, IDXGIOutputDuplication** ppOutputDuplication)
    {
        return ((delegate* unmanaged[Stdcall]<IDXGIOutput6*, IUnknown*, IDXGIOutputDuplication**, int>)(lpVtbl[22]))((IDXGIOutput6*)Unsafe.AsPointer(ref this), pDevice, ppOutputDuplication);
    }

    [return: NativeTypeName("BOOL")]
    public int SupportsOverlays()
    {
        return ((delegate* unmanaged[Stdcall]<IDXGIOutput6*, int>)(lpVtbl[23]))((IDXGIOutput6*)Unsafe.AsPointer(ref this));
    }

    [return: NativeTypeName("HRESULT")]
    public int CheckOverlaySupport(DXGI_FORMAT EnumFormat, IUnknown* pConcernedDevice, uint* pFlags)
    {
        return ((delegate* unmanaged[Stdcall]<IDXGIOutput6*, DXGI_FORMAT, IUnknown*, uint*, int>)(lpVtbl[24]))((IDXGIOutput6*)Unsafe.AsPointer(ref this), EnumFormat, pConcernedDevice, pFlags);
    }

    [return: NativeTypeName("HRESULT")]
    public int CheckOverlayColorSpaceSupport(DXGI_FORMAT Format, DXGI_COLOR_SPACE_TYPE ColorSpace, IUnknown* pConcernedDevice, uint* pFlags)
    {
        return ((delegate* unmanaged[Stdcall]<IDXGIOutput6*, DXGI_FORMAT, DXGI_COLOR_SPACE_TYPE, IUnknown*, uint*, int>)(lpVtbl[25]))((IDXGIOutput6*)Unsafe.AsPointer(ref this), Format, ColorSpace, pConcernedDevice, pFlags);
    }

    [return: NativeTypeName("HRESULT")]
    public int DuplicateOutput1(IUnknown* pDevice, uint Flags, uint SupportedFormatsCount, [NativeTypeName("const DXGI_FORMAT *")] DXGI_FORMAT* pSupportedFormats, IDXGIOutputDuplication** ppOutputDuplication)
    {
        return ((delegate* unmanaged[Stdcall]<IDXGIOutput6*, IUnknown*, uint, uint, DXGI_FORMAT*, IDXGIOutputDuplication**, int>)(lpVtbl[26]))((IDXGIOutput6*)Unsafe.AsPointer(ref this), pDevice, Flags, SupportedFormatsCount, pSupportedFormats, ppOutputDuplication);
    }

    [return: NativeTypeName("HRESULT")]
    public int GetDesc1(DXGI_OUTPUT_DESC1* pDesc)
    {
        return ((delegate* unmanaged[Stdcall]<IDXGIOutput6*, DXGI_OUTPUT_DESC1*, int>)(lpVtbl[27]))((IDXGIOutput6*)Unsafe.AsPointer(ref this), pDesc);
    }

    [return: NativeTypeName("HRESULT")]
    public int CheckHardwareCompositionSupport(uint* pFlags)
    {
        return ((delegate* unmanaged[Stdcall]<IDXGIOutput6*, uint*, int>)(lpVtbl[28]))((IDXGIOutput6*)Unsafe.AsPointer(ref this), pFlags);
    }

    public interface Interface : IDXGIOutput5.Interface
    {
        [return: NativeTypeName("HRESULT")]
        int GetDesc1(DXGI_OUTPUT_DESC1* pDesc);

        [return: NativeTypeName("HRESULT")]
        int CheckHardwareCompositionSupport(uint* pFlags);
    }
}

internal enum DXGI_GPU_PREFERENCE
{
    DXGI_GPU_PREFERENCE_UNSPECIFIED = 0,
    DXGI_GPU_PREFERENCE_MINIMUM_POWER = (DXGI_GPU_PREFERENCE_UNSPECIFIED + 1),
    DXGI_GPU_PREFERENCE_HIGH_PERFORMANCE = (DXGI_GPU_PREFERENCE_MINIMUM_POWER + 1),
}

[Guid("C1B6694F-FF09-44A9-B03C-77900A0A1D17")]
[NativeTypeName("struct IDXGIFactory6 : IDXGIFactory5")]
internal unsafe partial struct IDXGIFactory6 : IDXGIFactory6.Interface, INativeGuid
{
    static Guid* INativeGuid.NativeGuid => (Guid*)Unsafe.AsPointer(in IID_IDXGIFactory6);

    public void** lpVtbl;

    [return: NativeTypeName("HRESULT")]
    public int QueryInterface([NativeTypeName("const IID &")] Guid* riid, void** ppvObject)
    {
        return ((delegate* unmanaged[Stdcall]<IDXGIFactory6*, Guid*, void**, int>)(lpVtbl[0]))((IDXGIFactory6*)Unsafe.AsPointer(ref this), riid, ppvObject);
    }

    [return: NativeTypeName("ULONG")]
    public uint AddRef()
    {
        return ((delegate* unmanaged[Stdcall]<IDXGIFactory6*, uint>)(lpVtbl[1]))((IDXGIFactory6*)Unsafe.AsPointer(ref this));
    }

    [return: NativeTypeName("ULONG")]
    public uint Release()
    {
        return ((delegate* unmanaged[Stdcall]<IDXGIFactory6*, uint>)(lpVtbl[2]))((IDXGIFactory6*)Unsafe.AsPointer(ref this));
    }

    [return: NativeTypeName("HRESULT")]
    public int SetPrivateData([NativeTypeName("const GUID &")] Guid* Name, uint DataSize, [NativeTypeName("const void *")] void* pData)
    {
        return ((delegate* unmanaged[Stdcall]<IDXGIFactory6*, Guid*, uint, void*, int>)(lpVtbl[3]))((IDXGIFactory6*)Unsafe.AsPointer(ref this), Name, DataSize, pData);
    }

    [return: NativeTypeName("HRESULT")]
    public int SetPrivateDataInterface([NativeTypeName("const GUID &")] Guid* Name, [NativeTypeName("const IUnknown *")] IUnknown* pUnknown)
    {
        return ((delegate* unmanaged[Stdcall]<IDXGIFactory6*, Guid*, IUnknown*, int>)(lpVtbl[4]))((IDXGIFactory6*)Unsafe.AsPointer(ref this), Name, pUnknown);
    }

    [return: NativeTypeName("HRESULT")]
    public int GetPrivateData([NativeTypeName("const GUID &")] Guid* Name, uint* pDataSize, void* pData)
    {
        return ((delegate* unmanaged[Stdcall]<IDXGIFactory6*, Guid*, uint*, void*, int>)(lpVtbl[5]))((IDXGIFactory6*)Unsafe.AsPointer(ref this), Name, pDataSize, pData);
    }

    [return: NativeTypeName("HRESULT")]
    public int GetParent([NativeTypeName("const IID &")] Guid* riid, void** ppParent)
    {
        return ((delegate* unmanaged[Stdcall]<IDXGIFactory6*, Guid*, void**, int>)(lpVtbl[6]))((IDXGIFactory6*)Unsafe.AsPointer(ref this), riid, ppParent);
    }

    [return: NativeTypeName("HRESULT")]
    public int EnumAdapters(uint Adapter, IDXGIAdapter** ppAdapter)
    {
        return ((delegate* unmanaged[Stdcall]<IDXGIFactory6*, uint, IDXGIAdapter**, int>)(lpVtbl[7]))((IDXGIFactory6*)Unsafe.AsPointer(ref this), Adapter, ppAdapter);
    }

    [return: NativeTypeName("HRESULT")]
    public int MakeWindowAssociation([NativeTypeName("HWND")] HWND__* WindowHandle, uint Flags)
    {
        return ((delegate* unmanaged[Stdcall]<IDXGIFactory6*, HWND__*, uint, int>)(lpVtbl[8]))((IDXGIFactory6*)Unsafe.AsPointer(ref this), WindowHandle, Flags);
    }

    [return: NativeTypeName("HRESULT")]
    public int GetWindowAssociation([NativeTypeName("HWND *")] HWND__** pWindowHandle)
    {
        return ((delegate* unmanaged[Stdcall]<IDXGIFactory6*, HWND__**, int>)(lpVtbl[9]))((IDXGIFactory6*)Unsafe.AsPointer(ref this), pWindowHandle);
    }

    [return: NativeTypeName("HRESULT")]
    public int CreateSwapChain(IUnknown* pDevice, DXGI_SWAP_CHAIN_DESC* pDesc, IDXGISwapChain** ppSwapChain)
    {
        return ((delegate* unmanaged[Stdcall]<IDXGIFactory6*, IUnknown*, DXGI_SWAP_CHAIN_DESC*, IDXGISwapChain**, int>)(lpVtbl[10]))((IDXGIFactory6*)Unsafe.AsPointer(ref this), pDevice, pDesc, ppSwapChain);
    }

    [return: NativeTypeName("HRESULT")]
    public int CreateSoftwareAdapter([NativeTypeName("HMODULE")] HINSTANCE__* Module, IDXGIAdapter** ppAdapter)
    {
        return ((delegate* unmanaged[Stdcall]<IDXGIFactory6*, HINSTANCE__*, IDXGIAdapter**, int>)(lpVtbl[11]))((IDXGIFactory6*)Unsafe.AsPointer(ref this), Module, ppAdapter);
    }

    [return: NativeTypeName("HRESULT")]
    public int EnumAdapters1(uint Adapter, IDXGIAdapter1** ppAdapter)
    {
        return ((delegate* unmanaged[Stdcall]<IDXGIFactory6*, uint, IDXGIAdapter1**, int>)(lpVtbl[12]))((IDXGIFactory6*)Unsafe.AsPointer(ref this), Adapter, ppAdapter);
    }

    [return: NativeTypeName("BOOL")]
    public int IsCurrent()
    {
        return ((delegate* unmanaged[Stdcall]<IDXGIFactory6*, int>)(lpVtbl[13]))((IDXGIFactory6*)Unsafe.AsPointer(ref this));
    }

    [return: NativeTypeName("BOOL")]
    public int IsWindowedStereoEnabled()
    {
        return ((delegate* unmanaged[Stdcall]<IDXGIFactory6*, int>)(lpVtbl[14]))((IDXGIFactory6*)Unsafe.AsPointer(ref this));
    }

    [return: NativeTypeName("HRESULT")]
    public int CreateSwapChainForHwnd(IUnknown* pDevice, [NativeTypeName("HWND")] HWND__* hWnd, [NativeTypeName("const DXGI_SWAP_CHAIN_DESC1 *")] DXGI_SWAP_CHAIN_DESC1* pDesc, [NativeTypeName("const DXGI_SWAP_CHAIN_FULLSCREEN_DESC *")] DXGI_SWAP_CHAIN_FULLSCREEN_DESC* pFullscreenDesc, IDXGIOutput* pRestrictToOutput, IDXGISwapChain1** ppSwapChain)
    {
        return ((delegate* unmanaged[Stdcall]<IDXGIFactory6*, IUnknown*, HWND__*, DXGI_SWAP_CHAIN_DESC1*, DXGI_SWAP_CHAIN_FULLSCREEN_DESC*, IDXGIOutput*, IDXGISwapChain1**, int>)(lpVtbl[15]))((IDXGIFactory6*)Unsafe.AsPointer(ref this), pDevice, hWnd, pDesc, pFullscreenDesc, pRestrictToOutput, ppSwapChain);
    }

    [return: NativeTypeName("HRESULT")]
    public int CreateSwapChainForCoreWindow(IUnknown* pDevice, IUnknown* pWindow, [NativeTypeName("const DXGI_SWAP_CHAIN_DESC1 *")] DXGI_SWAP_CHAIN_DESC1* pDesc, IDXGIOutput* pRestrictToOutput, IDXGISwapChain1** ppSwapChain)
    {
        return ((delegate* unmanaged[Stdcall]<IDXGIFactory6*, IUnknown*, IUnknown*, DXGI_SWAP_CHAIN_DESC1*, IDXGIOutput*, IDXGISwapChain1**, int>)(lpVtbl[16]))((IDXGIFactory6*)Unsafe.AsPointer(ref this), pDevice, pWindow, pDesc, pRestrictToOutput, ppSwapChain);
    }

    [return: NativeTypeName("HRESULT")]
    public int GetSharedResourceAdapterLuid([NativeTypeName("HANDLE")] void* hResource, LUID* pLuid)
    {
        return ((delegate* unmanaged[Stdcall]<IDXGIFactory6*, void*, LUID*, int>)(lpVtbl[17]))((IDXGIFactory6*)Unsafe.AsPointer(ref this), hResource, pLuid);
    }

    [return: NativeTypeName("HRESULT")]
    public int RegisterStereoStatusWindow([NativeTypeName("HWND")] HWND__* WindowHandle, uint wMsg, [NativeTypeName("DWORD *")] uint* pdwCookie)
    {
        return ((delegate* unmanaged[Stdcall]<IDXGIFactory6*, HWND__*, uint, uint*, int>)(lpVtbl[18]))((IDXGIFactory6*)Unsafe.AsPointer(ref this), WindowHandle, wMsg, pdwCookie);
    }

    [return: NativeTypeName("HRESULT")]
    public int RegisterStereoStatusEvent([NativeTypeName("HANDLE")] void* hEvent, [NativeTypeName("DWORD *")] uint* pdwCookie)
    {
        return ((delegate* unmanaged[Stdcall]<IDXGIFactory6*, void*, uint*, int>)(lpVtbl[19]))((IDXGIFactory6*)Unsafe.AsPointer(ref this), hEvent, pdwCookie);
    }

    public void UnregisterStereoStatus([NativeTypeName("DWORD")] uint dwCookie)
    {
        ((delegate* unmanaged[Stdcall]<IDXGIFactory6*, uint, void>)(lpVtbl[20]))((IDXGIFactory6*)Unsafe.AsPointer(ref this), dwCookie);
    }

    [return: NativeTypeName("HRESULT")]
    public int RegisterOcclusionStatusWindow([NativeTypeName("HWND")] HWND__* WindowHandle, uint wMsg, [NativeTypeName("DWORD *")] uint* pdwCookie)
    {
        return ((delegate* unmanaged[Stdcall]<IDXGIFactory6*, HWND__*, uint, uint*, int>)(lpVtbl[21]))((IDXGIFactory6*)Unsafe.AsPointer(ref this), WindowHandle, wMsg, pdwCookie);
    }

    [return: NativeTypeName("HRESULT")]
    public int RegisterOcclusionStatusEvent([NativeTypeName("HANDLE")] void* hEvent, [NativeTypeName("DWORD *")] uint* pdwCookie)
    {
        return ((delegate* unmanaged[Stdcall]<IDXGIFactory6*, void*, uint*, int>)(lpVtbl[22]))((IDXGIFactory6*)Unsafe.AsPointer(ref this), hEvent, pdwCookie);
    }

    public void UnregisterOcclusionStatus([NativeTypeName("DWORD")] uint dwCookie)
    {
        ((delegate* unmanaged[Stdcall]<IDXGIFactory6*, uint, void>)(lpVtbl[23]))((IDXGIFactory6*)Unsafe.AsPointer(ref this), dwCookie);
    }

    [return: NativeTypeName("HRESULT")]
    public int CreateSwapChainForComposition(IUnknown* pDevice, [NativeTypeName("const DXGI_SWAP_CHAIN_DESC1 *")] DXGI_SWAP_CHAIN_DESC1* pDesc, IDXGIOutput* pRestrictToOutput, IDXGISwapChain1** ppSwapChain)
    {
        return ((delegate* unmanaged[Stdcall]<IDXGIFactory6*, IUnknown*, DXGI_SWAP_CHAIN_DESC1*, IDXGIOutput*, IDXGISwapChain1**, int>)(lpVtbl[24]))((IDXGIFactory6*)Unsafe.AsPointer(ref this), pDevice, pDesc, pRestrictToOutput, ppSwapChain);
    }

    public uint GetCreationFlags()
    {
        return ((delegate* unmanaged[Stdcall]<IDXGIFactory6*, uint>)(lpVtbl[25]))((IDXGIFactory6*)Unsafe.AsPointer(ref this));
    }

    [return: NativeTypeName("HRESULT")]
    public int EnumAdapterByLuid(LUID AdapterLuid, [NativeTypeName("const IID &")] Guid* riid, void** ppvAdapter)
    {
        return ((delegate* unmanaged[Stdcall]<IDXGIFactory6*, LUID, Guid*, void**, int>)(lpVtbl[26]))((IDXGIFactory6*)Unsafe.AsPointer(ref this), AdapterLuid, riid, ppvAdapter);
    }

    [return: NativeTypeName("HRESULT")]
    public int EnumWarpAdapter([NativeTypeName("const IID &")] Guid* riid, void** ppvAdapter)
    {
        return ((delegate* unmanaged[Stdcall]<IDXGIFactory6*, Guid*, void**, int>)(lpVtbl[27]))((IDXGIFactory6*)Unsafe.AsPointer(ref this), riid, ppvAdapter);
    }

    [return: NativeTypeName("HRESULT")]
    public int CheckFeatureSupport(DXGI_FEATURE Feature, void* pFeatureSupportData, uint FeatureSupportDataSize)
    {
        return ((delegate* unmanaged[Stdcall]<IDXGIFactory6*, DXGI_FEATURE, void*, uint, int>)(lpVtbl[28]))((IDXGIFactory6*)Unsafe.AsPointer(ref this), Feature, pFeatureSupportData, FeatureSupportDataSize);
    }

    [return: NativeTypeName("HRESULT")]
    public int EnumAdapterByGpuPreference(uint Adapter, DXGI_GPU_PREFERENCE GpuPreference, [NativeTypeName("const IID &")] Guid* riid, void** ppvAdapter)
    {
        return ((delegate* unmanaged[Stdcall]<IDXGIFactory6*, uint, DXGI_GPU_PREFERENCE, Guid*, void**, int>)(lpVtbl[29]))((IDXGIFactory6*)Unsafe.AsPointer(ref this), Adapter, GpuPreference, riid, ppvAdapter);
    }

    public interface Interface : IDXGIFactory5.Interface
    {
        [return: NativeTypeName("HRESULT")]
        int EnumAdapterByGpuPreference(uint Adapter, DXGI_GPU_PREFERENCE GpuPreference, [NativeTypeName("const IID &")] Guid* riid, void** ppvAdapter);
    }
}

[Guid("A4966EED-76DB-44DA-84C1-EE9A7AFB20A8")]
[NativeTypeName("struct IDXGIFactory7 : IDXGIFactory6")]
internal unsafe partial struct IDXGIFactory7 : IDXGIFactory7.Interface, INativeGuid
{
    static Guid* INativeGuid.NativeGuid => (Guid*)Unsafe.AsPointer(in IID_IDXGIFactory7);

    public void** lpVtbl;

    [return: NativeTypeName("HRESULT")]
    public int QueryInterface([NativeTypeName("const IID &")] Guid* riid, void** ppvObject)
    {
        return ((delegate* unmanaged[Stdcall]<IDXGIFactory7*, Guid*, void**, int>)(lpVtbl[0]))((IDXGIFactory7*)Unsafe.AsPointer(ref this), riid, ppvObject);
    }

    [return: NativeTypeName("ULONG")]
    public uint AddRef()
    {
        return ((delegate* unmanaged[Stdcall]<IDXGIFactory7*, uint>)(lpVtbl[1]))((IDXGIFactory7*)Unsafe.AsPointer(ref this));
    }

    [return: NativeTypeName("ULONG")]
    public uint Release()
    {
        return ((delegate* unmanaged[Stdcall]<IDXGIFactory7*, uint>)(lpVtbl[2]))((IDXGIFactory7*)Unsafe.AsPointer(ref this));
    }

    [return: NativeTypeName("HRESULT")]
    public int SetPrivateData([NativeTypeName("const GUID &")] Guid* Name, uint DataSize, [NativeTypeName("const void *")] void* pData)
    {
        return ((delegate* unmanaged[Stdcall]<IDXGIFactory7*, Guid*, uint, void*, int>)(lpVtbl[3]))((IDXGIFactory7*)Unsafe.AsPointer(ref this), Name, DataSize, pData);
    }

    [return: NativeTypeName("HRESULT")]
    public int SetPrivateDataInterface([NativeTypeName("const GUID &")] Guid* Name, [NativeTypeName("const IUnknown *")] IUnknown* pUnknown)
    {
        return ((delegate* unmanaged[Stdcall]<IDXGIFactory7*, Guid*, IUnknown*, int>)(lpVtbl[4]))((IDXGIFactory7*)Unsafe.AsPointer(ref this), Name, pUnknown);
    }

    [return: NativeTypeName("HRESULT")]
    public int GetPrivateData([NativeTypeName("const GUID &")] Guid* Name, uint* pDataSize, void* pData)
    {
        return ((delegate* unmanaged[Stdcall]<IDXGIFactory7*, Guid*, uint*, void*, int>)(lpVtbl[5]))((IDXGIFactory7*)Unsafe.AsPointer(ref this), Name, pDataSize, pData);
    }

    [return: NativeTypeName("HRESULT")]
    public int GetParent([NativeTypeName("const IID &")] Guid* riid, void** ppParent)
    {
        return ((delegate* unmanaged[Stdcall]<IDXGIFactory7*, Guid*, void**, int>)(lpVtbl[6]))((IDXGIFactory7*)Unsafe.AsPointer(ref this), riid, ppParent);
    }

    [return: NativeTypeName("HRESULT")]
    public int EnumAdapters(uint Adapter, IDXGIAdapter** ppAdapter)
    {
        return ((delegate* unmanaged[Stdcall]<IDXGIFactory7*, uint, IDXGIAdapter**, int>)(lpVtbl[7]))((IDXGIFactory7*)Unsafe.AsPointer(ref this), Adapter, ppAdapter);
    }

    [return: NativeTypeName("HRESULT")]
    public int MakeWindowAssociation([NativeTypeName("HWND")] HWND__* WindowHandle, uint Flags)
    {
        return ((delegate* unmanaged[Stdcall]<IDXGIFactory7*, HWND__*, uint, int>)(lpVtbl[8]))((IDXGIFactory7*)Unsafe.AsPointer(ref this), WindowHandle, Flags);
    }

    [return: NativeTypeName("HRESULT")]
    public int GetWindowAssociation([NativeTypeName("HWND *")] HWND__** pWindowHandle)
    {
        return ((delegate* unmanaged[Stdcall]<IDXGIFactory7*, HWND__**, int>)(lpVtbl[9]))((IDXGIFactory7*)Unsafe.AsPointer(ref this), pWindowHandle);
    }

    [return: NativeTypeName("HRESULT")]
    public int CreateSwapChain(IUnknown* pDevice, DXGI_SWAP_CHAIN_DESC* pDesc, IDXGISwapChain** ppSwapChain)
    {
        return ((delegate* unmanaged[Stdcall]<IDXGIFactory7*, IUnknown*, DXGI_SWAP_CHAIN_DESC*, IDXGISwapChain**, int>)(lpVtbl[10]))((IDXGIFactory7*)Unsafe.AsPointer(ref this), pDevice, pDesc, ppSwapChain);
    }

    [return: NativeTypeName("HRESULT")]
    public int CreateSoftwareAdapter([NativeTypeName("HMODULE")] HINSTANCE__* Module, IDXGIAdapter** ppAdapter)
    {
        return ((delegate* unmanaged[Stdcall]<IDXGIFactory7*, HINSTANCE__*, IDXGIAdapter**, int>)(lpVtbl[11]))((IDXGIFactory7*)Unsafe.AsPointer(ref this), Module, ppAdapter);
    }

    [return: NativeTypeName("HRESULT")]
    public int EnumAdapters1(uint Adapter, IDXGIAdapter1** ppAdapter)
    {
        return ((delegate* unmanaged[Stdcall]<IDXGIFactory7*, uint, IDXGIAdapter1**, int>)(lpVtbl[12]))((IDXGIFactory7*)Unsafe.AsPointer(ref this), Adapter, ppAdapter);
    }

    [return: NativeTypeName("BOOL")]
    public int IsCurrent()
    {
        return ((delegate* unmanaged[Stdcall]<IDXGIFactory7*, int>)(lpVtbl[13]))((IDXGIFactory7*)Unsafe.AsPointer(ref this));
    }

    [return: NativeTypeName("BOOL")]
    public int IsWindowedStereoEnabled()
    {
        return ((delegate* unmanaged[Stdcall]<IDXGIFactory7*, int>)(lpVtbl[14]))((IDXGIFactory7*)Unsafe.AsPointer(ref this));
    }

    [return: NativeTypeName("HRESULT")]
    public int CreateSwapChainForHwnd(IUnknown* pDevice, [NativeTypeName("HWND")] HWND__* hWnd, [NativeTypeName("const DXGI_SWAP_CHAIN_DESC1 *")] DXGI_SWAP_CHAIN_DESC1* pDesc, [NativeTypeName("const DXGI_SWAP_CHAIN_FULLSCREEN_DESC *")] DXGI_SWAP_CHAIN_FULLSCREEN_DESC* pFullscreenDesc, IDXGIOutput* pRestrictToOutput, IDXGISwapChain1** ppSwapChain)
    {
        return ((delegate* unmanaged[Stdcall]<IDXGIFactory7*, IUnknown*, HWND__*, DXGI_SWAP_CHAIN_DESC1*, DXGI_SWAP_CHAIN_FULLSCREEN_DESC*, IDXGIOutput*, IDXGISwapChain1**, int>)(lpVtbl[15]))((IDXGIFactory7*)Unsafe.AsPointer(ref this), pDevice, hWnd, pDesc, pFullscreenDesc, pRestrictToOutput, ppSwapChain);
    }

    [return: NativeTypeName("HRESULT")]
    public int CreateSwapChainForCoreWindow(IUnknown* pDevice, IUnknown* pWindow, [NativeTypeName("const DXGI_SWAP_CHAIN_DESC1 *")] DXGI_SWAP_CHAIN_DESC1* pDesc, IDXGIOutput* pRestrictToOutput, IDXGISwapChain1** ppSwapChain)
    {
        return ((delegate* unmanaged[Stdcall]<IDXGIFactory7*, IUnknown*, IUnknown*, DXGI_SWAP_CHAIN_DESC1*, IDXGIOutput*, IDXGISwapChain1**, int>)(lpVtbl[16]))((IDXGIFactory7*)Unsafe.AsPointer(ref this), pDevice, pWindow, pDesc, pRestrictToOutput, ppSwapChain);
    }

    [return: NativeTypeName("HRESULT")]
    public int GetSharedResourceAdapterLuid([NativeTypeName("HANDLE")] void* hResource, LUID* pLuid)
    {
        return ((delegate* unmanaged[Stdcall]<IDXGIFactory7*, void*, LUID*, int>)(lpVtbl[17]))((IDXGIFactory7*)Unsafe.AsPointer(ref this), hResource, pLuid);
    }

    [return: NativeTypeName("HRESULT")]
    public int RegisterStereoStatusWindow([NativeTypeName("HWND")] HWND__* WindowHandle, uint wMsg, [NativeTypeName("DWORD *")] uint* pdwCookie)
    {
        return ((delegate* unmanaged[Stdcall]<IDXGIFactory7*, HWND__*, uint, uint*, int>)(lpVtbl[18]))((IDXGIFactory7*)Unsafe.AsPointer(ref this), WindowHandle, wMsg, pdwCookie);
    }

    [return: NativeTypeName("HRESULT")]
    public int RegisterStereoStatusEvent([NativeTypeName("HANDLE")] void* hEvent, [NativeTypeName("DWORD *")] uint* pdwCookie)
    {
        return ((delegate* unmanaged[Stdcall]<IDXGIFactory7*, void*, uint*, int>)(lpVtbl[19]))((IDXGIFactory7*)Unsafe.AsPointer(ref this), hEvent, pdwCookie);
    }

    public void UnregisterStereoStatus([NativeTypeName("DWORD")] uint dwCookie)
    {
        ((delegate* unmanaged[Stdcall]<IDXGIFactory7*, uint, void>)(lpVtbl[20]))((IDXGIFactory7*)Unsafe.AsPointer(ref this), dwCookie);
    }

    [return: NativeTypeName("HRESULT")]
    public int RegisterOcclusionStatusWindow([NativeTypeName("HWND")] HWND__* WindowHandle, uint wMsg, [NativeTypeName("DWORD *")] uint* pdwCookie)
    {
        return ((delegate* unmanaged[Stdcall]<IDXGIFactory7*, HWND__*, uint, uint*, int>)(lpVtbl[21]))((IDXGIFactory7*)Unsafe.AsPointer(ref this), WindowHandle, wMsg, pdwCookie);
    }

    [return: NativeTypeName("HRESULT")]
    public int RegisterOcclusionStatusEvent([NativeTypeName("HANDLE")] void* hEvent, [NativeTypeName("DWORD *")] uint* pdwCookie)
    {
        return ((delegate* unmanaged[Stdcall]<IDXGIFactory7*, void*, uint*, int>)(lpVtbl[22]))((IDXGIFactory7*)Unsafe.AsPointer(ref this), hEvent, pdwCookie);
    }

    public void UnregisterOcclusionStatus([NativeTypeName("DWORD")] uint dwCookie)
    {
        ((delegate* unmanaged[Stdcall]<IDXGIFactory7*, uint, void>)(lpVtbl[23]))((IDXGIFactory7*)Unsafe.AsPointer(ref this), dwCookie);
    }

    [return: NativeTypeName("HRESULT")]
    public int CreateSwapChainForComposition(IUnknown* pDevice, [NativeTypeName("const DXGI_SWAP_CHAIN_DESC1 *")] DXGI_SWAP_CHAIN_DESC1* pDesc, IDXGIOutput* pRestrictToOutput, IDXGISwapChain1** ppSwapChain)
    {
        return ((delegate* unmanaged[Stdcall]<IDXGIFactory7*, IUnknown*, DXGI_SWAP_CHAIN_DESC1*, IDXGIOutput*, IDXGISwapChain1**, int>)(lpVtbl[24]))((IDXGIFactory7*)Unsafe.AsPointer(ref this), pDevice, pDesc, pRestrictToOutput, ppSwapChain);
    }

    public uint GetCreationFlags()
    {
        return ((delegate* unmanaged[Stdcall]<IDXGIFactory7*, uint>)(lpVtbl[25]))((IDXGIFactory7*)Unsafe.AsPointer(ref this));
    }

    [return: NativeTypeName("HRESULT")]
    public int EnumAdapterByLuid(LUID AdapterLuid, [NativeTypeName("const IID &")] Guid* riid, void** ppvAdapter)
    {
        return ((delegate* unmanaged[Stdcall]<IDXGIFactory7*, LUID, Guid*, void**, int>)(lpVtbl[26]))((IDXGIFactory7*)Unsafe.AsPointer(ref this), AdapterLuid, riid, ppvAdapter);
    }

    [return: NativeTypeName("HRESULT")]
    public int EnumWarpAdapter([NativeTypeName("const IID &")] Guid* riid, void** ppvAdapter)
    {
        return ((delegate* unmanaged[Stdcall]<IDXGIFactory7*, Guid*, void**, int>)(lpVtbl[27]))((IDXGIFactory7*)Unsafe.AsPointer(ref this), riid, ppvAdapter);
    }

    [return: NativeTypeName("HRESULT")]
    public int CheckFeatureSupport(DXGI_FEATURE Feature, void* pFeatureSupportData, uint FeatureSupportDataSize)
    {
        return ((delegate* unmanaged[Stdcall]<IDXGIFactory7*, DXGI_FEATURE, void*, uint, int>)(lpVtbl[28]))((IDXGIFactory7*)Unsafe.AsPointer(ref this), Feature, pFeatureSupportData, FeatureSupportDataSize);
    }

    [return: NativeTypeName("HRESULT")]
    public int EnumAdapterByGpuPreference(uint Adapter, DXGI_GPU_PREFERENCE GpuPreference, [NativeTypeName("const IID &")] Guid* riid, void** ppvAdapter)
    {
        return ((delegate* unmanaged[Stdcall]<IDXGIFactory7*, uint, DXGI_GPU_PREFERENCE, Guid*, void**, int>)(lpVtbl[29]))((IDXGIFactory7*)Unsafe.AsPointer(ref this), Adapter, GpuPreference, riid, ppvAdapter);
    }

    [return: NativeTypeName("HRESULT")]
    public int RegisterAdaptersChangedEvent([NativeTypeName("HANDLE")] void* hEvent, [NativeTypeName("DWORD *")] uint* pdwCookie)
    {
        return ((delegate* unmanaged[Stdcall]<IDXGIFactory7*, void*, uint*, int>)(lpVtbl[30]))((IDXGIFactory7*)Unsafe.AsPointer(ref this), hEvent, pdwCookie);
    }

    [return: NativeTypeName("HRESULT")]
    public int UnregisterAdaptersChangedEvent([NativeTypeName("DWORD")] uint dwCookie)
    {
        return ((delegate* unmanaged[Stdcall]<IDXGIFactory7*, uint, int>)(lpVtbl[31]))((IDXGIFactory7*)Unsafe.AsPointer(ref this), dwCookie);
    }

    public interface Interface : IDXGIFactory6.Interface
    {
        [return: NativeTypeName("HRESULT")]
        int RegisterAdaptersChangedEvent([NativeTypeName("HANDLE")] void* hEvent, [NativeTypeName("DWORD *")] uint* pdwCookie);

        [return: NativeTypeName("HRESULT")]
        int UnregisterAdaptersChangedEvent([NativeTypeName("DWORD")] uint dwCookie);
    }
}

internal static unsafe partial class DXGI
{
    [DllImport("dxgi", CallingConvention = CallingConvention.StdCall, ExactSpelling = true)]
    [return: NativeTypeName("HRESULT")]
    public static extern int CreateDXGIFactory([NativeTypeName("const IID &")] Guid* riid, void** ppFactory);

    [DllImport("dxgi", CallingConvention = CallingConvention.StdCall, ExactSpelling = true)]
    [return: NativeTypeName("HRESULT")]
    public static extern int CreateDXGIFactory1([NativeTypeName("const IID &")] Guid* riid, void** ppFactory);

    [NativeTypeName("const GUID")]
    public static ref readonly Guid IID_IDXGIObject
    {
        get
        {
            ReadOnlySpan<byte> data = [
                0xB8, 0x2F, 0xC2, 0xAE,
                0xF3, 0x76,
                0x39, 0x46,
                0x9B,
                0xE0,
                0x28,
                0xEB,
                0x43,
                0xA6,
                0x7A,
                0x2E
            ];

            Debug.Assert(data.Length == Unsafe.SizeOf<Guid>());
            return ref Unsafe.As<byte, Guid>(ref MemoryMarshal.GetReference(data));
        }
    }

    [NativeTypeName("const GUID")]
    public static ref readonly Guid IID_IDXGIDeviceSubObject
    {
        get
        {
            ReadOnlySpan<byte> data = [
                0x79, 0x03, 0x3E, 0x3D,
                0xDE, 0xF9,
                0x58, 0x4D,
                0xBB,
                0x6C,
                0x18,
                0xD6,
                0x29,
                0x92,
                0xF1,
                0xA6
            ];

            Debug.Assert(data.Length == Unsafe.SizeOf<Guid>());
            return ref Unsafe.As<byte, Guid>(ref MemoryMarshal.GetReference(data));
        }
    }

    [NativeTypeName("const GUID")]
    public static ref readonly Guid IID_IDXGIResource
    {
        get
        {
            ReadOnlySpan<byte> data = [
                0xB4, 0x3A, 0x5F, 0x03,
                0x2E, 0x48,
                0x50, 0x4E,
                0xB4,
                0x1F,
                0x8A,
                0x7F,
                0x8B,
                0xD8,
                0x96,
                0x0B
            ];

            Debug.Assert(data.Length == Unsafe.SizeOf<Guid>());
            return ref Unsafe.As<byte, Guid>(ref MemoryMarshal.GetReference(data));
        }
    }

    [NativeTypeName("const GUID")]
    public static ref readonly Guid IID_IDXGIKeyedMutex
    {
        get
        {
            ReadOnlySpan<byte> data = [
                0x89, 0x12, 0x8E, 0x9D,
                0xB3, 0xD7,
                0x5F, 0x46,
                0x81,
                0x26,
                0x25,
                0x0E,
                0x34,
                0x9A,
                0xF8,
                0x5D
            ];

            Debug.Assert(data.Length == Unsafe.SizeOf<Guid>());
            return ref Unsafe.As<byte, Guid>(ref MemoryMarshal.GetReference(data));
        }
    }

    [NativeTypeName("const GUID")]
    public static ref readonly Guid IID_IDXGISurface
    {
        get
        {
            ReadOnlySpan<byte> data = [
                0x6C, 0xB5, 0xFC, 0xCA,
                0xC3, 0x6A,
                0x89, 0x48,
                0xBF,
                0x47,
                0x9E,
                0x23,
                0xBB,
                0xD2,
                0x60,
                0xEC
            ];

            Debug.Assert(data.Length == Unsafe.SizeOf<Guid>());
            return ref Unsafe.As<byte, Guid>(ref MemoryMarshal.GetReference(data));
        }
    }

    [NativeTypeName("const GUID")]
    public static ref readonly Guid IID_IDXGISurface1
    {
        get
        {
            ReadOnlySpan<byte> data = [
                0x92, 0x30, 0xE6, 0x4A,
                0x27, 0x63,
                0x1B, 0x4C,
                0x80,
                0xAE,
                0xBF,
                0xE1,
                0x2E,
                0xA3,
                0x2B,
                0x86
            ];

            Debug.Assert(data.Length == Unsafe.SizeOf<Guid>());
            return ref Unsafe.As<byte, Guid>(ref MemoryMarshal.GetReference(data));
        }
    }

    [NativeTypeName("const GUID")]
    public static ref readonly Guid IID_IDXGIAdapter
    {
        get
        {
            ReadOnlySpan<byte> data = [
                0xE1, 0xE7, 0x11, 0x24,
                0xAC, 0x12,
                0xCF, 0x4C,
                0xBD,
                0x14,
                0x97,
                0x98,
                0xE8,
                0x53,
                0x4D,
                0xC0
            ];

            Debug.Assert(data.Length == Unsafe.SizeOf<Guid>());
            return ref Unsafe.As<byte, Guid>(ref MemoryMarshal.GetReference(data));
        }
    }

    [NativeTypeName("const GUID")]
    public static ref readonly Guid IID_IDXGIOutput
    {
        get
        {
            ReadOnlySpan<byte> data = [
                0xDB, 0xEE, 0x02, 0xAE,
                0x35, 0xC7,
                0x90, 0x46,
                0x8D,
                0x52,
                0x5A,
                0x8D,
                0xC2,
                0x02,
                0x13,
                0xAA
            ];

            Debug.Assert(data.Length == Unsafe.SizeOf<Guid>());
            return ref Unsafe.As<byte, Guid>(ref MemoryMarshal.GetReference(data));
        }
    }

    [NativeTypeName("const GUID")]
    public static ref readonly Guid IID_IDXGISwapChain
    {
        get
        {
            ReadOnlySpan<byte> data = [
                0xA0, 0x36, 0x0D, 0x31,
                0xE7, 0xD2,
                0x0A, 0x4C,
                0xAA,
                0x04,
                0x6A,
                0x9D,
                0x23,
                0xB8,
                0x88,
                0x6A
            ];

            Debug.Assert(data.Length == Unsafe.SizeOf<Guid>());
            return ref Unsafe.As<byte, Guid>(ref MemoryMarshal.GetReference(data));
        }
    }

    [NativeTypeName("const GUID")]
    public static ref readonly Guid IID_IDXGIFactory
    {
        get
        {
            ReadOnlySpan<byte> data = [
                0xEC, 0x66, 0x71, 0x7B,
                0xC7, 0x21,
                0xAE, 0x44,
                0xB2,
                0x1A,
                0xC9,
                0xAE,
                0x32,
                0x1A,
                0xE3,
                0x69
            ];

            Debug.Assert(data.Length == Unsafe.SizeOf<Guid>());
            return ref Unsafe.As<byte, Guid>(ref MemoryMarshal.GetReference(data));
        }
    }

    [NativeTypeName("const GUID")]
    public static ref readonly Guid IID_IDXGIDevice
    {
        get
        {
            ReadOnlySpan<byte> data = [
                0xFA, 0x77, 0xEC, 0x54,
                0x77, 0x13,
                0xE6, 0x44,
                0x8C,
                0x32,
                0x88,
                0xFD,
                0x5F,
                0x44,
                0xC8,
                0x4C
            ];

            Debug.Assert(data.Length == Unsafe.SizeOf<Guid>());
            return ref Unsafe.As<byte, Guid>(ref MemoryMarshal.GetReference(data));
        }
    }

    [NativeTypeName("const GUID")]
    public static ref readonly Guid IID_IDXGIFactory1
    {
        get
        {
            ReadOnlySpan<byte> data = [
                0x78, 0xAE, 0x0A, 0x77,
                0x6F, 0xF2,
                0xBA, 0x4D,
                0xA8,
                0x29,
                0x25,
                0x3C,
                0x83,
                0xD1,
                0xB3,
                0x87
            ];

            Debug.Assert(data.Length == Unsafe.SizeOf<Guid>());
            return ref Unsafe.As<byte, Guid>(ref MemoryMarshal.GetReference(data));
        }
    }

    [NativeTypeName("const GUID")]
    public static ref readonly Guid IID_IDXGIAdapter1
    {
        get
        {
            ReadOnlySpan<byte> data = [
                0x61, 0x8F, 0x03, 0x29,
                0x39, 0x38,
                0x26, 0x46,
                0x91,
                0xFD,
                0x08,
                0x68,
                0x79,
                0x01,
                0x1A,
                0x05
            ];

            Debug.Assert(data.Length == Unsafe.SizeOf<Guid>());
            return ref Unsafe.As<byte, Guid>(ref MemoryMarshal.GetReference(data));
        }
    }

    [NativeTypeName("const GUID")]
    public static ref readonly Guid IID_IDXGIDevice1
    {
        get
        {
            ReadOnlySpan<byte> data = [
                0x0F, 0x97, 0xDB, 0x77,
                0x76, 0x62,
                0xBA, 0x48,
                0xBA,
                0x28,
                0x07,
                0x01,
                0x43,
                0xB4,
                0x39,
                0x2C
            ];

            Debug.Assert(data.Length == Unsafe.SizeOf<Guid>());
            return ref Unsafe.As<byte, Guid>(ref MemoryMarshal.GetReference(data));
        }
    }

    [NativeTypeName("const GUID")]
    public static ref readonly Guid IID_IDXGIDisplayControl
    {
        get
        {
            ReadOnlySpan<byte> data = [
                0x1A, 0xBF, 0x9D, 0xEA,
                0x8E, 0xC8,
                0x86, 0x44,
                0x85,
                0x4A,
                0x98,
                0xAA,
                0x01,
                0x38,
                0xF3,
                0x0C
            ];

            Debug.Assert(data.Length == Unsafe.SizeOf<Guid>());
            return ref Unsafe.As<byte, Guid>(ref MemoryMarshal.GetReference(data));
        }
    }

    [NativeTypeName("const GUID")]
    public static ref readonly Guid IID_IDXGIOutputDuplication
    {
        get
        {
            ReadOnlySpan<byte> data = [
                0xC3, 0xFA, 0x1C, 0x19,
                0x41, 0xA3,
                0x0D, 0x47,
                0xB2,
                0x6E,
                0xA8,
                0x64,
                0xF4,
                0x28,
                0x31,
                0x9C
            ];

            Debug.Assert(data.Length == Unsafe.SizeOf<Guid>());
            return ref Unsafe.As<byte, Guid>(ref MemoryMarshal.GetReference(data));
        }
    }

    [NativeTypeName("const GUID")]
    public static ref readonly Guid IID_IDXGISurface2
    {
        get
        {
            ReadOnlySpan<byte> data = [
                0xDD, 0x96, 0xA4, 0xAB,
                0x17, 0xB6,
                0xB8, 0x4C,
                0xA8,
                0x66,
                0xBC,
                0x44,
                0xD7,
                0xEB,
                0x1F,
                0xA2
            ];

            Debug.Assert(data.Length == Unsafe.SizeOf<Guid>());
            return ref Unsafe.As<byte, Guid>(ref MemoryMarshal.GetReference(data));
        }
    }

    [NativeTypeName("const GUID")]
    public static ref readonly Guid IID_IDXGIResource1
    {
        get
        {
            ReadOnlySpan<byte> data = [
                0x79, 0x13, 0x96, 0x30,
                0x09, 0x46,
                0x41, 0x4A,
                0x99,
                0x8E,
                0x54,
                0xFE,
                0x56,
                0x7E,
                0xE0,
                0xC1
            ];

            Debug.Assert(data.Length == Unsafe.SizeOf<Guid>());
            return ref Unsafe.As<byte, Guid>(ref MemoryMarshal.GetReference(data));
        }
    }

    [NativeTypeName("const GUID")]
    public static ref readonly Guid IID_IDXGIDevice2
    {
        get
        {
            ReadOnlySpan<byte> data = [
                0x17, 0x86, 0x00, 0x05,
                0xFD, 0xFB,
                0x51, 0x40,
                0xA7,
                0x90,
                0x14,
                0x48,
                0x84,
                0xB4,
                0xF6,
                0xA9
            ];

            Debug.Assert(data.Length == Unsafe.SizeOf<Guid>());
            return ref Unsafe.As<byte, Guid>(ref MemoryMarshal.GetReference(data));
        }
    }

    [NativeTypeName("const GUID")]
    public static ref readonly Guid IID_IDXGISwapChain1
    {
        get
        {
            ReadOnlySpan<byte> data = [
                0xF7, 0x45, 0x0A, 0x79,
                0x42, 0x0D,
                0x76, 0x48,
                0x98,
                0x3A,
                0x0A,
                0x55,
                0xCF,
                0xE6,
                0xF4,
                0xAA
            ];

            Debug.Assert(data.Length == Unsafe.SizeOf<Guid>());
            return ref Unsafe.As<byte, Guid>(ref MemoryMarshal.GetReference(data));
        }
    }

    [NativeTypeName("const GUID")]
    public static ref readonly Guid IID_IDXGIFactory2
    {
        get
        {
            ReadOnlySpan<byte> data = [
                0x1C, 0x3A, 0xC8, 0x50,
                0x72, 0xE0,
                0x48, 0x4C,
                0x87,
                0xB0,
                0x36,
                0x30,
                0xFA,
                0x36,
                0xA6,
                0xD0
            ];

            Debug.Assert(data.Length == Unsafe.SizeOf<Guid>());
            return ref Unsafe.As<byte, Guid>(ref MemoryMarshal.GetReference(data));
        }
    }

    [NativeTypeName("const GUID")]
    public static ref readonly Guid IID_IDXGIAdapter2
    {
        get
        {
            ReadOnlySpan<byte> data = [
                0x0A, 0xAE, 0xA1, 0x0A,
                0x0E, 0xFA,
                0x84, 0x4B,
                0x86,
                0x44,
                0xE0,
                0x5F,
                0xF8,
                0xE5,
                0xAC,
                0xB5
            ];

            Debug.Assert(data.Length == Unsafe.SizeOf<Guid>());
            return ref Unsafe.As<byte, Guid>(ref MemoryMarshal.GetReference(data));
        }
    }

    [NativeTypeName("const GUID")]
    public static ref readonly Guid IID_IDXGIOutput1
    {
        get
        {
            ReadOnlySpan<byte> data = [
                0xA8, 0xDE, 0xCD, 0x00,
                0x9B, 0x93,
                0x83, 0x4B,
                0xA3,
                0x40,
                0xA6,
                0x85,
                0x22,
                0x66,
                0x66,
                0xCC
            ];

            Debug.Assert(data.Length == Unsafe.SizeOf<Guid>());
            return ref Unsafe.As<byte, Guid>(ref MemoryMarshal.GetReference(data));
        }
    }

    [DllImport("dxgi", CallingConvention = CallingConvention.StdCall, ExactSpelling = true)]
    [return: NativeTypeName("HRESULT")]
    public static extern int CreateDXGIFactory2(uint Flags, [NativeTypeName("const IID &")] Guid* riid, void** ppFactory);

    [DllImport("dxgi", CallingConvention = CallingConvention.StdCall, ExactSpelling = true)]
    [return: NativeTypeName("HRESULT")]
    public static extern int DXGIGetDebugInterface1(uint Flags, [NativeTypeName("const IID &")] Guid* riid, void** pDebug);

    [NativeTypeName("const GUID")]
    public static ref readonly Guid IID_IDXGIDevice3
    {
        get
        {
            ReadOnlySpan<byte> data = [
                0x6C, 0x89, 0x07, 0x60,
                0x44, 0x32,
                0xFD, 0x4A,
                0xBF,
                0x18,
                0xA6,
                0xD3,
                0xBE,
                0xDA,
                0x50,
                0x23
            ];

            Debug.Assert(data.Length == Unsafe.SizeOf<Guid>());
            return ref Unsafe.As<byte, Guid>(ref MemoryMarshal.GetReference(data));
        }
    }

    [NativeTypeName("const GUID")]
    public static ref readonly Guid IID_IDXGISwapChain2
    {
        get
        {
            ReadOnlySpan<byte> data = [
                0xC4, 0x2A, 0xBE, 0xA8,
                0x9F, 0x19,
                0x46, 0x49,
                0xB3,
                0x31,
                0x79,
                0x59,
                0x9F,
                0xB9,
                0x8D,
                0xE7
            ];

            Debug.Assert(data.Length == Unsafe.SizeOf<Guid>());
            return ref Unsafe.As<byte, Guid>(ref MemoryMarshal.GetReference(data));
        }
    }

    [NativeTypeName("const GUID")]
    public static ref readonly Guid IID_IDXGIOutput2
    {
        get
        {
            ReadOnlySpan<byte> data = [
                0xD1, 0x39, 0x5E, 0x59,
                0x24, 0x27,
                0x63, 0x46,
                0x99,
                0xB1,
                0xDA,
                0x96,
                0x9D,
                0xE2,
                0x83,
                0x64
            ];

            Debug.Assert(data.Length == Unsafe.SizeOf<Guid>());
            return ref Unsafe.As<byte, Guid>(ref MemoryMarshal.GetReference(data));
        }
    }

    [NativeTypeName("const GUID")]
    public static ref readonly Guid IID_IDXGIFactory3
    {
        get
        {
            ReadOnlySpan<byte> data = [
                0x23, 0x38, 0x48, 0x25,
                0x46, 0xCD,
                0x7D, 0x4C,
                0x86,
                0xCA,
                0x47,
                0xAA,
                0x95,
                0xB8,
                0x37,
                0xBD
            ];

            Debug.Assert(data.Length == Unsafe.SizeOf<Guid>());
            return ref Unsafe.As<byte, Guid>(ref MemoryMarshal.GetReference(data));
        }
    }

    [NativeTypeName("const GUID")]
    public static ref readonly Guid IID_IDXGIDecodeSwapChain
    {
        get
        {
            ReadOnlySpan<byte> data = [
                0x6B, 0x06, 0x33, 0x26,
                0x14, 0x45,
                0x7A, 0x4C,
                0x8F,
                0xD8,
                0x12,
                0xEA,
                0x98,
                0x05,
                0x9D,
                0x18
            ];

            Debug.Assert(data.Length == Unsafe.SizeOf<Guid>());
            return ref Unsafe.As<byte, Guid>(ref MemoryMarshal.GetReference(data));
        }
    }

    [NativeTypeName("const GUID")]
    public static ref readonly Guid IID_IDXGIFactoryMedia
    {
        get
        {
            ReadOnlySpan<byte> data = [
                0xF2, 0xD1, 0xE7, 0x41,
                0x91, 0xA5,
                0x7B, 0x4F,
                0xA2,
                0xE5,
                0xFA,
                0x9C,
                0x84,
                0x3E,
                0x1C,
                0x12
            ];

            Debug.Assert(data.Length == Unsafe.SizeOf<Guid>());
            return ref Unsafe.As<byte, Guid>(ref MemoryMarshal.GetReference(data));
        }
    }

    [NativeTypeName("const GUID")]
    public static ref readonly Guid IID_IDXGISwapChainMedia
    {
        get
        {
            ReadOnlySpan<byte> data = [
                0x0B, 0xB9, 0x95, 0xDD,
                0x5F, 0xF0,
                0x6A, 0x4F,
                0xBD,
                0x65,
                0x25,
                0xBF,
                0xB2,
                0x64,
                0xBD,
                0x84
            ];

            Debug.Assert(data.Length == Unsafe.SizeOf<Guid>());
            return ref Unsafe.As<byte, Guid>(ref MemoryMarshal.GetReference(data));
        }
    }

    [NativeTypeName("const GUID")]
    public static ref readonly Guid IID_IDXGIOutput3
    {
        get
        {
            ReadOnlySpan<byte> data = [
                0x01, 0xB3, 0x6B, 0x8A,
                0x7E, 0x7E,
                0xF4, 0x41,
                0xA8,
                0xE0,
                0x5B,
                0x32,
                0xF7,
                0xF9,
                0x9B,
                0x18
            ];

            Debug.Assert(data.Length == Unsafe.SizeOf<Guid>());
            return ref Unsafe.As<byte, Guid>(ref MemoryMarshal.GetReference(data));
        }
    }

    [NativeTypeName("const GUID")]
    public static ref readonly Guid IID_IDXGISwapChain3
    {
        get
        {
            ReadOnlySpan<byte> data = [
                0xDB, 0x9B, 0xD9, 0x94,
                0xF8, 0xF1,
                0xB0, 0x4A,
                0xB2,
                0x36,
                0x7D,
                0xA0,
                0x17,
                0x0E,
                0xDA,
                0xB1
            ];

            Debug.Assert(data.Length == Unsafe.SizeOf<Guid>());
            return ref Unsafe.As<byte, Guid>(ref MemoryMarshal.GetReference(data));
        }
    }

    [NativeTypeName("const GUID")]
    public static ref readonly Guid IID_IDXGIOutput4
    {
        get
        {
            ReadOnlySpan<byte> data = [
                0x35, 0xCA, 0x7D, 0xDC,
                0x96, 0x21,
                0x4D, 0x41,
                0x9F,
                0x53,
                0x61,
                0x78,
                0x84,
                0x03,
                0x2A,
                0x60
            ];

            Debug.Assert(data.Length == Unsafe.SizeOf<Guid>());
            return ref Unsafe.As<byte, Guid>(ref MemoryMarshal.GetReference(data));
        }
    }

    [NativeTypeName("const GUID")]
    public static ref readonly Guid IID_IDXGIFactory4
    {
        get
        {
            ReadOnlySpan<byte> data = [
                0x02, 0xEA, 0xC6, 0x1B,
                0x36, 0xEF,
                0x4F, 0x46,
                0xBF,
                0x0C,
                0x21,
                0xCA,
                0x39,
                0xE5,
                0x16,
                0x8A
            ];

            Debug.Assert(data.Length == Unsafe.SizeOf<Guid>());
            return ref Unsafe.As<byte, Guid>(ref MemoryMarshal.GetReference(data));
        }
    }

    [NativeTypeName("const GUID")]
    public static ref readonly Guid IID_IDXGIAdapter3
    {
        get
        {
            ReadOnlySpan<byte> data = [
                0xA4, 0x67, 0x59, 0x64,
                0x92, 0x13,
                0x10, 0x43,
                0xA7,
                0x98,
                0x80,
                0x53,
                0xCE,
                0x3E,
                0x93,
                0xFD
            ];

            Debug.Assert(data.Length == Unsafe.SizeOf<Guid>());
            return ref Unsafe.As<byte, Guid>(ref MemoryMarshal.GetReference(data));
        }
    }

    [NativeTypeName("const GUID")]
    public static ref readonly Guid IID_IDXGIOutput5
    {
        get
        {
            ReadOnlySpan<byte> data = [
                0x24, 0x74, 0xA0, 0x80,
                0x52, 0xAB,
                0xEB, 0x42,
                0x83,
                0x3C,
                0x0C,
                0x42,
                0xFD,
                0x28,
                0x2D,
                0x98
            ];

            Debug.Assert(data.Length == Unsafe.SizeOf<Guid>());
            return ref Unsafe.As<byte, Guid>(ref MemoryMarshal.GetReference(data));
        }
    }

    [NativeTypeName("const GUID")]
    public static ref readonly Guid IID_IDXGISwapChain4
    {
        get
        {
            ReadOnlySpan<byte> data = [
                0x5A, 0x5D, 0x58, 0x3D,
                0x4A, 0xBD,
                0x9E, 0x48,
                0xB1,
                0xF4,
                0x3D,
                0xBC,
                0xB6,
                0x45,
                0x2F,
                0xFB
            ];

            Debug.Assert(data.Length == Unsafe.SizeOf<Guid>());
            return ref Unsafe.As<byte, Guid>(ref MemoryMarshal.GetReference(data));
        }
    }

    [NativeTypeName("const GUID")]
    public static ref readonly Guid IID_IDXGIDevice4
    {
        get
        {
            ReadOnlySpan<byte> data = [
                0x5F, 0xF9, 0xB4, 0x95,
                0xDA, 0xD8,
                0xA4, 0x4C,
                0x9E,
                0xE6,
                0x3B,
                0x76,
                0xD5,
                0x96,
                0x8A,
                0x10
            ];

            Debug.Assert(data.Length == Unsafe.SizeOf<Guid>());
            return ref Unsafe.As<byte, Guid>(ref MemoryMarshal.GetReference(data));
        }
    }

    [NativeTypeName("const GUID")]
    public static ref readonly Guid IID_IDXGIFactory5
    {
        get
        {
            ReadOnlySpan<byte> data = [
                0xF5, 0xE1, 0x32, 0x76,
                0x65, 0xEE,
                0xCA, 0x4D,
                0x87,
                0xFD,
                0x84,
                0xCD,
                0x75,
                0xF8,
                0x83,
                0x8D
            ];

            Debug.Assert(data.Length == Unsafe.SizeOf<Guid>());
            return ref Unsafe.As<byte, Guid>(ref MemoryMarshal.GetReference(data));
        }
    }

    [DllImport("dxgi", CallingConvention = CallingConvention.StdCall, ExactSpelling = true)]
    [return: NativeTypeName("HRESULT")]
    public static extern int DXGIDeclareAdapterRemovalSupport();

    [DllImport("dxgi", CallingConvention = CallingConvention.StdCall, ExactSpelling = true)]
    [return: NativeTypeName("HRESULT")]
    public static extern int DXGIDisableVBlankVirtualization();

    [NativeTypeName("const GUID")]
    public static ref readonly Guid IID_IDXGIAdapter4
    {
        get
        {
            ReadOnlySpan<byte> data = [
                0xD1, 0x99, 0x8D, 0x3C,
                0xBF, 0x4F,
                0x81, 0x41,
                0xA8,
                0x2C,
                0xAF,
                0x66,
                0xBF,
                0x7B,
                0xD2,
                0x4E
            ];

            Debug.Assert(data.Length == Unsafe.SizeOf<Guid>());
            return ref Unsafe.As<byte, Guid>(ref MemoryMarshal.GetReference(data));
        }
    }

    [NativeTypeName("const GUID")]
    public static ref readonly Guid IID_IDXGIOutput6
    {
        get
        {
            ReadOnlySpan<byte> data = [
                0xE8, 0x46, 0x83, 0x06,
                0xEC, 0xAA,
                0x84, 0x4B,
                0xAD,
                0xD7,
                0x13,
                0x7F,
                0x51,
                0x3F,
                0x77,
                0xA1
            ];

            Debug.Assert(data.Length == Unsafe.SizeOf<Guid>());
            return ref Unsafe.As<byte, Guid>(ref MemoryMarshal.GetReference(data));
        }
    }

    [NativeTypeName("const GUID")]
    public static ref readonly Guid IID_IDXGIFactory6
    {
        get
        {
            ReadOnlySpan<byte> data = [
                0x4F, 0x69, 0xB6, 0xC1,
                0x09, 0xFF,
                0xA9, 0x44,
                0xB0,
                0x3C,
                0x77,
                0x90,
                0x0A,
                0x0A,
                0x1D,
                0x17
            ];

            Debug.Assert(data.Length == Unsafe.SizeOf<Guid>());
            return ref Unsafe.As<byte, Guid>(ref MemoryMarshal.GetReference(data));
        }
    }

    [NativeTypeName("const GUID")]
    public static ref readonly Guid IID_IDXGIFactory7
    {
        get
        {
            ReadOnlySpan<byte> data = [
                0xED, 0x6E, 0x96, 0xA4,
                0xDB, 0x76,
                0xDA, 0x44,
                0x84,
                0xC1,
                0xEE,
                0x9A,
                0x7A,
                0xFB,
                0x20,
                0xA8
            ];

            Debug.Assert(data.Length == Unsafe.SizeOf<Guid>());
            return ref Unsafe.As<byte, Guid>(ref MemoryMarshal.GetReference(data));
        }
    }

    [NativeTypeName("#define __REQUIRED_RPCNDR_H_VERSION__ 501")]
    public const int __REQUIRED_RPCNDR_H_VERSION__ = 501;

    [NativeTypeName("#define __REQUIRED_RPCSAL_H_VERSION__ 100")]
    public const int __REQUIRED_RPCSAL_H_VERSION__ = 100;

    [NativeTypeName("#define DXGI_STANDARD_MULTISAMPLE_QUALITY_PATTERN 0xffffffff")]
    public const uint DXGI_STANDARD_MULTISAMPLE_QUALITY_PATTERN = 0xffffffff;

    [NativeTypeName("#define DXGI_CENTER_MULTISAMPLE_QUALITY_PATTERN 0xfffffffe")]
    public const uint DXGI_CENTER_MULTISAMPLE_QUALITY_PATTERN = 0xfffffffe;

    [NativeTypeName("#define _FACDXGI 0x87a")]
    public const int _FACDXGI = 0x87a;

    [NativeTypeName("#define DXGI_CPU_ACCESS_NONE ( 0 )")]
    public const int DXGI_CPU_ACCESS_NONE = (0);

    [NativeTypeName("#define DXGI_CPU_ACCESS_DYNAMIC ( 1 )")]
    public const int DXGI_CPU_ACCESS_DYNAMIC = (1);

    [NativeTypeName("#define DXGI_CPU_ACCESS_READ_WRITE ( 2 )")]
    public const int DXGI_CPU_ACCESS_READ_WRITE = (2);

    [NativeTypeName("#define DXGI_CPU_ACCESS_SCRATCH ( 3 )")]
    public const int DXGI_CPU_ACCESS_SCRATCH = (3);

    [NativeTypeName("#define DXGI_CPU_ACCESS_FIELD 15")]
    public const int DXGI_CPU_ACCESS_FIELD = 15;

    [NativeTypeName("#define DXGI_USAGE_SHADER_INPUT 0x00000010UL")]
    public const uint DXGI_USAGE_SHADER_INPUT = 0x00000010U;

    [NativeTypeName("#define DXGI_USAGE_RENDER_TARGET_OUTPUT 0x00000020UL")]
    public const uint DXGI_USAGE_RENDER_TARGET_OUTPUT = 0x00000020U;

    [NativeTypeName("#define DXGI_USAGE_BACK_BUFFER 0x00000040UL")]
    public const uint DXGI_USAGE_BACK_BUFFER = 0x00000040U;

    [NativeTypeName("#define DXGI_USAGE_SHARED 0x00000080UL")]
    public const uint DXGI_USAGE_SHARED = 0x00000080U;

    [NativeTypeName("#define DXGI_USAGE_READ_ONLY 0x00000100UL")]
    public const uint DXGI_USAGE_READ_ONLY = 0x00000100U;

    [NativeTypeName("#define DXGI_USAGE_DISCARD_ON_PRESENT 0x00000200UL")]
    public const uint DXGI_USAGE_DISCARD_ON_PRESENT = 0x00000200U;

    [NativeTypeName("#define DXGI_USAGE_UNORDERED_ACCESS 0x00000400UL")]
    public const uint DXGI_USAGE_UNORDERED_ACCESS = 0x00000400U;

    [NativeTypeName("#define DXGI_RESOURCE_PRIORITY_MINIMUM ( 0x28000000 )")]
    public const int DXGI_RESOURCE_PRIORITY_MINIMUM = (0x28000000);

    [NativeTypeName("#define DXGI_RESOURCE_PRIORITY_LOW ( 0x50000000 )")]
    public const int DXGI_RESOURCE_PRIORITY_LOW = (0x50000000);

    [NativeTypeName("#define DXGI_RESOURCE_PRIORITY_NORMAL ( 0x78000000 )")]
    public const int DXGI_RESOURCE_PRIORITY_NORMAL = (0x78000000);

    [NativeTypeName("#define DXGI_RESOURCE_PRIORITY_HIGH ( 0xa0000000 )")]
    public const uint DXGI_RESOURCE_PRIORITY_HIGH = (0xa0000000);

    [NativeTypeName("#define DXGI_RESOURCE_PRIORITY_MAXIMUM ( 0xc8000000 )")]
    public const uint DXGI_RESOURCE_PRIORITY_MAXIMUM = (0xc8000000);

    [NativeTypeName("#define DXGI_MAP_READ ( 1UL )")]
    public const uint DXGI_MAP_READ = (1U);

    [NativeTypeName("#define DXGI_MAP_WRITE ( 2UL )")]
    public const uint DXGI_MAP_WRITE = (2U);

    [NativeTypeName("#define DXGI_MAP_DISCARD ( 4UL )")]
    public const uint DXGI_MAP_DISCARD = (4U);

    [NativeTypeName("#define DXGI_ENUM_MODES_INTERLACED ( 1UL )")]
    public const uint DXGI_ENUM_MODES_INTERLACED = (1U);

    [NativeTypeName("#define DXGI_ENUM_MODES_SCALING ( 2UL )")]
    public const uint DXGI_ENUM_MODES_SCALING = (2U);

    [NativeTypeName("#define DXGI_MAX_SWAP_CHAIN_BUFFERS ( 16 )")]
    public const int DXGI_MAX_SWAP_CHAIN_BUFFERS = (16);

    [NativeTypeName("#define DXGI_PRESENT_TEST 0x00000001UL")]
    public const uint DXGI_PRESENT_TEST = 0x00000001U;

    [NativeTypeName("#define DXGI_PRESENT_DO_NOT_SEQUENCE 0x00000002UL")]
    public const uint DXGI_PRESENT_DO_NOT_SEQUENCE = 0x00000002U;

    [NativeTypeName("#define DXGI_PRESENT_RESTART 0x00000004UL")]
    public const uint DXGI_PRESENT_RESTART = 0x00000004U;

    [NativeTypeName("#define DXGI_PRESENT_DO_NOT_WAIT 0x00000008UL")]
    public const uint DXGI_PRESENT_DO_NOT_WAIT = 0x00000008U;

    [NativeTypeName("#define DXGI_PRESENT_STEREO_PREFER_RIGHT 0x00000010UL")]
    public const uint DXGI_PRESENT_STEREO_PREFER_RIGHT = 0x00000010U;

    [NativeTypeName("#define DXGI_PRESENT_STEREO_TEMPORARY_MONO 0x00000020UL")]
    public const uint DXGI_PRESENT_STEREO_TEMPORARY_MONO = 0x00000020U;

    [NativeTypeName("#define DXGI_PRESENT_RESTRICT_TO_OUTPUT 0x00000040UL")]
    public const uint DXGI_PRESENT_RESTRICT_TO_OUTPUT = 0x00000040U;

    [NativeTypeName("#define DXGI_PRESENT_USE_DURATION 0x00000100UL")]
    public const uint DXGI_PRESENT_USE_DURATION = 0x00000100U;

    [NativeTypeName("#define DXGI_PRESENT_ALLOW_TEARING 0x00000200UL")]
    public const uint DXGI_PRESENT_ALLOW_TEARING = 0x00000200U;

    [NativeTypeName("#define DXGI_MWA_NO_WINDOW_CHANGES ( 1 << 0 )")]
    public const int DXGI_MWA_NO_WINDOW_CHANGES = (1 << 0);

    [NativeTypeName("#define DXGI_MWA_NO_ALT_ENTER ( 1 << 1 )")]
    public const int DXGI_MWA_NO_ALT_ENTER = (1 << 1);

    [NativeTypeName("#define DXGI_MWA_NO_PRINT_SCREEN ( 1 << 2 )")]
    public const int DXGI_MWA_NO_PRINT_SCREEN = (1 << 2);

    [NativeTypeName("#define DXGI_MWA_VALID ( 0x7 )")]
    public const int DXGI_MWA_VALID = (0x7);

    [NativeTypeName("#define DXGI_ENUM_MODES_STEREO ( 4UL )")]
    public const uint DXGI_ENUM_MODES_STEREO = (4U);

    [NativeTypeName("#define DXGI_ENUM_MODES_DISABLED_STEREO ( 8UL )")]
    public const uint DXGI_ENUM_MODES_DISABLED_STEREO = (8U);

    [NativeTypeName("#define DXGI_SHARED_RESOURCE_READ ( 0x80000000L )")]
    public const uint DXGI_SHARED_RESOURCE_READ = (0x80000000);

    [NativeTypeName("#define DXGI_SHARED_RESOURCE_WRITE ( 1 )")]
    public const int DXGI_SHARED_RESOURCE_WRITE = (1);

    [NativeTypeName("#define DXGI_CREATE_FACTORY_DEBUG 0x1")]
    public const int DXGI_CREATE_FACTORY_DEBUG = 0x1;
}
