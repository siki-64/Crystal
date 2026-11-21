#pragma warning disable CS0649
using System;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using static Crystal.Interop.Dxc;

namespace Crystal.Interop;

internal partial struct DxcShaderHash
{
    [NativeTypeName("UINT32")]
    public uint Flags;

    [NativeTypeName("BYTE[16]")]
    public _HashDigest_e__FixedBuffer HashDigest;

    [InlineArray(16)]
    public partial struct _HashDigest_e__FixedBuffer
    {
        public byte e0;
    }
}

[Guid("8BA5FB08-5195-40E2-AC58-0D989C3A0102")]
[NativeTypeName("struct IDxcBlob : IUnknown")]
internal unsafe partial struct IDxcBlob : IDxcBlob.Interface, INativeGuid
{
    static Guid* INativeGuid.NativeGuid => (Guid*)Unsafe.AsPointer(in IID_IDxcBlob);

    public void** lpVtbl;

    [return: NativeTypeName("HRESULT")]
    public int QueryInterface([NativeTypeName("const IID &")] Guid* riid, void** ppvObject)
    {
        return ((delegate* unmanaged[Stdcall]<IDxcBlob*, Guid*, void**, int>)(lpVtbl[0]))((IDxcBlob*)Unsafe.AsPointer(ref this), riid, ppvObject);
    }

    [return: NativeTypeName("ULONG")]
    public uint AddRef()
    {
        return ((delegate* unmanaged[Stdcall]<IDxcBlob*, uint>)(lpVtbl[1]))((IDxcBlob*)Unsafe.AsPointer(ref this));
    }

    [return: NativeTypeName("ULONG")]
    public uint Release()
    {
        return ((delegate* unmanaged[Stdcall]<IDxcBlob*, uint>)(lpVtbl[2]))((IDxcBlob*)Unsafe.AsPointer(ref this));
    }

    [return: NativeTypeName("LPVOID")]
    public void* GetBufferPointer()
    {
        return ((delegate* unmanaged[Stdcall]<IDxcBlob*, void*>)(lpVtbl[3]))((IDxcBlob*)Unsafe.AsPointer(ref this));
    }

    [return: NativeTypeName("SIZE_T")]
    public nuint GetBufferSize()
    {
        return ((delegate* unmanaged[Stdcall]<IDxcBlob*, nuint>)(lpVtbl[4]))((IDxcBlob*)Unsafe.AsPointer(ref this));
    }

    public interface Interface : IUnknown.Interface
    {
        [return: NativeTypeName("LPVOID")]
        void* GetBufferPointer();

        [return: NativeTypeName("SIZE_T")]
        nuint GetBufferSize();
    }
}

[Guid("7241D424-2646-4191-97C0-98E96E42FC68")]
[NativeTypeName("struct IDxcBlobEncoding : IDxcBlob")]
internal unsafe partial struct IDxcBlobEncoding : IDxcBlobEncoding.Interface, INativeGuid
{
    static Guid* INativeGuid.NativeGuid => (Guid*)Unsafe.AsPointer(in IID_IDxcBlobEncoding);

    public void** lpVtbl;

    [return: NativeTypeName("HRESULT")]
    public int QueryInterface([NativeTypeName("const IID &")] Guid* riid, void** ppvObject)
    {
        return ((delegate* unmanaged[Stdcall]<IDxcBlobEncoding*, Guid*, void**, int>)(lpVtbl[0]))((IDxcBlobEncoding*)Unsafe.AsPointer(ref this), riid, ppvObject);
    }

    [return: NativeTypeName("ULONG")]
    public uint AddRef()
    {
        return ((delegate* unmanaged[Stdcall]<IDxcBlobEncoding*, uint>)(lpVtbl[1]))((IDxcBlobEncoding*)Unsafe.AsPointer(ref this));
    }

    [return: NativeTypeName("ULONG")]
    public uint Release()
    {
        return ((delegate* unmanaged[Stdcall]<IDxcBlobEncoding*, uint>)(lpVtbl[2]))((IDxcBlobEncoding*)Unsafe.AsPointer(ref this));
    }

    [return: NativeTypeName("LPVOID")]
    public void* GetBufferPointer()
    {
        return ((delegate* unmanaged[Stdcall]<IDxcBlobEncoding*, void*>)(lpVtbl[3]))((IDxcBlobEncoding*)Unsafe.AsPointer(ref this));
    }

    [return: NativeTypeName("SIZE_T")]
    public nuint GetBufferSize()
    {
        return ((delegate* unmanaged[Stdcall]<IDxcBlobEncoding*, nuint>)(lpVtbl[4]))((IDxcBlobEncoding*)Unsafe.AsPointer(ref this));
    }

    [return: NativeTypeName("HRESULT")]
    public int GetEncoding([NativeTypeName("BOOL *")] int* pKnown, [NativeTypeName("UINT32 *")] uint* pCodePage)
    {
        return ((delegate* unmanaged[Stdcall]<IDxcBlobEncoding*, int*, uint*, int>)(lpVtbl[5]))((IDxcBlobEncoding*)Unsafe.AsPointer(ref this), pKnown, pCodePage);
    }

    public interface Interface : IDxcBlob.Interface
    {
        [return: NativeTypeName("HRESULT")]
        int GetEncoding([NativeTypeName("BOOL *")] int* pKnown, [NativeTypeName("UINT32 *")] uint* pCodePage);
    }
}

[Guid("A3F84EAB-0FAA-497E-A39C-EE6ED60B2D84")]
[NativeTypeName("struct IDxcBlobWide : IDxcBlobEncoding")]
internal unsafe partial struct IDxcBlobWide : IDxcBlobWide.Interface, INativeGuid
{
    static Guid* INativeGuid.NativeGuid => (Guid*)Unsafe.AsPointer(in IID_IDxcBlobWide);

    public void** lpVtbl;

    [return: NativeTypeName("HRESULT")]
    public int QueryInterface([NativeTypeName("const IID &")] Guid* riid, void** ppvObject)
    {
        return ((delegate* unmanaged[Stdcall]<IDxcBlobWide*, Guid*, void**, int>)(lpVtbl[0]))((IDxcBlobWide*)Unsafe.AsPointer(ref this), riid, ppvObject);
    }

    [return: NativeTypeName("ULONG")]
    public uint AddRef()
    {
        return ((delegate* unmanaged[Stdcall]<IDxcBlobWide*, uint>)(lpVtbl[1]))((IDxcBlobWide*)Unsafe.AsPointer(ref this));
    }

    [return: NativeTypeName("ULONG")]
    public uint Release()
    {
        return ((delegate* unmanaged[Stdcall]<IDxcBlobWide*, uint>)(lpVtbl[2]))((IDxcBlobWide*)Unsafe.AsPointer(ref this));
    }

    [return: NativeTypeName("LPVOID")]
    public void* GetBufferPointer()
    {
        return ((delegate* unmanaged[Stdcall]<IDxcBlobWide*, void*>)(lpVtbl[3]))((IDxcBlobWide*)Unsafe.AsPointer(ref this));
    }

    [return: NativeTypeName("SIZE_T")]
    public nuint GetBufferSize()
    {
        return ((delegate* unmanaged[Stdcall]<IDxcBlobWide*, nuint>)(lpVtbl[4]))((IDxcBlobWide*)Unsafe.AsPointer(ref this));
    }

    [return: NativeTypeName("HRESULT")]
    public int GetEncoding([NativeTypeName("BOOL *")] int* pKnown, [NativeTypeName("UINT32 *")] uint* pCodePage)
    {
        return ((delegate* unmanaged[Stdcall]<IDxcBlobWide*, int*, uint*, int>)(lpVtbl[5]))((IDxcBlobWide*)Unsafe.AsPointer(ref this), pKnown, pCodePage);
    }

    [return: NativeTypeName("LPCWSTR")]
    public char* GetStringPointer()
    {
        return ((delegate* unmanaged[Stdcall]<IDxcBlobWide*, char*>)(lpVtbl[6]))((IDxcBlobWide*)Unsafe.AsPointer(ref this));
    }

    [return: NativeTypeName("SIZE_T")]
    public nuint GetStringLength()
    {
        return ((delegate* unmanaged[Stdcall]<IDxcBlobWide*, nuint>)(lpVtbl[7]))((IDxcBlobWide*)Unsafe.AsPointer(ref this));
    }

    public interface Interface : IDxcBlobEncoding.Interface
    {
        [return: NativeTypeName("LPCWSTR")]
        char* GetStringPointer();

        [return: NativeTypeName("SIZE_T")]
        nuint GetStringLength();
    }
}

[Guid("3DA636C9-BA71-4024-A301-30CBF125305B")]
[NativeTypeName("struct IDxcBlobUtf8 : IDxcBlobEncoding")]
internal unsafe partial struct IDxcBlobUtf8 : IDxcBlobUtf8.Interface, INativeGuid
{
    static Guid* INativeGuid.NativeGuid => (Guid*)Unsafe.AsPointer(in IID_IDxcBlobUtf8);

    public void** lpVtbl;

    [return: NativeTypeName("HRESULT")]
    public int QueryInterface([NativeTypeName("const IID &")] Guid* riid, void** ppvObject)
    {
        return ((delegate* unmanaged[Stdcall]<IDxcBlobUtf8*, Guid*, void**, int>)(lpVtbl[0]))((IDxcBlobUtf8*)Unsafe.AsPointer(ref this), riid, ppvObject);
    }

    [return: NativeTypeName("ULONG")]
    public uint AddRef()
    {
        return ((delegate* unmanaged[Stdcall]<IDxcBlobUtf8*, uint>)(lpVtbl[1]))((IDxcBlobUtf8*)Unsafe.AsPointer(ref this));
    }

    [return: NativeTypeName("ULONG")]
    public uint Release()
    {
        return ((delegate* unmanaged[Stdcall]<IDxcBlobUtf8*, uint>)(lpVtbl[2]))((IDxcBlobUtf8*)Unsafe.AsPointer(ref this));
    }

    [return: NativeTypeName("LPVOID")]
    public void* GetBufferPointer()
    {
        return ((delegate* unmanaged[Stdcall]<IDxcBlobUtf8*, void*>)(lpVtbl[3]))((IDxcBlobUtf8*)Unsafe.AsPointer(ref this));
    }

    [return: NativeTypeName("SIZE_T")]
    public nuint GetBufferSize()
    {
        return ((delegate* unmanaged[Stdcall]<IDxcBlobUtf8*, nuint>)(lpVtbl[4]))((IDxcBlobUtf8*)Unsafe.AsPointer(ref this));
    }

    [return: NativeTypeName("HRESULT")]
    public int GetEncoding([NativeTypeName("BOOL *")] int* pKnown, [NativeTypeName("UINT32 *")] uint* pCodePage)
    {
        return ((delegate* unmanaged[Stdcall]<IDxcBlobUtf8*, int*, uint*, int>)(lpVtbl[5]))((IDxcBlobUtf8*)Unsafe.AsPointer(ref this), pKnown, pCodePage);
    }

    [return: NativeTypeName("LPCSTR")]
    public sbyte* GetStringPointer()
    {
        return ((delegate* unmanaged[Stdcall]<IDxcBlobUtf8*, sbyte*>)(lpVtbl[6]))((IDxcBlobUtf8*)Unsafe.AsPointer(ref this));
    }

    [return: NativeTypeName("SIZE_T")]
    public nuint GetStringLength()
    {
        return ((delegate* unmanaged[Stdcall]<IDxcBlobUtf8*, nuint>)(lpVtbl[7]))((IDxcBlobUtf8*)Unsafe.AsPointer(ref this));
    }

    public interface Interface : IDxcBlobEncoding.Interface
    {
        [return: NativeTypeName("LPCSTR")]
        sbyte* GetStringPointer();

        [return: NativeTypeName("SIZE_T")]
        nuint GetStringLength();
    }
}

[Guid("7F61FC7D-950D-467F-B3E3-3C02FB49187C")]
[NativeTypeName("struct IDxcIncludeHandler : IUnknown")]
internal unsafe partial struct IDxcIncludeHandler : IDxcIncludeHandler.Interface, INativeGuid
{
    static Guid* INativeGuid.NativeGuid => (Guid*)Unsafe.AsPointer(in IID_IDxcIncludeHandler);

    public void** lpVtbl;

    [return: NativeTypeName("HRESULT")]
    public int QueryInterface([NativeTypeName("const IID &")] Guid* riid, void** ppvObject)
    {
        return ((delegate* unmanaged[Stdcall]<IDxcIncludeHandler*, Guid*, void**, int>)(lpVtbl[0]))((IDxcIncludeHandler*)Unsafe.AsPointer(ref this), riid, ppvObject);
    }

    [return: NativeTypeName("ULONG")]
    public uint AddRef()
    {
        return ((delegate* unmanaged[Stdcall]<IDxcIncludeHandler*, uint>)(lpVtbl[1]))((IDxcIncludeHandler*)Unsafe.AsPointer(ref this));
    }

    [return: NativeTypeName("ULONG")]
    public uint Release()
    {
        return ((delegate* unmanaged[Stdcall]<IDxcIncludeHandler*, uint>)(lpVtbl[2]))((IDxcIncludeHandler*)Unsafe.AsPointer(ref this));
    }

    [return: NativeTypeName("HRESULT")]
    public int LoadSource([NativeTypeName("LPCWSTR")] char* pFilename, IDxcBlob** ppIncludeSource)
    {
        return ((delegate* unmanaged[Stdcall]<IDxcIncludeHandler*, char*, IDxcBlob**, int>)(lpVtbl[3]))((IDxcIncludeHandler*)Unsafe.AsPointer(ref this), pFilename, ppIncludeSource);
    }

    public interface Interface : IUnknown.Interface
    {
        [return: NativeTypeName("HRESULT")]
        int LoadSource([NativeTypeName("LPCWSTR")] char* pFilename, IDxcBlob** ppIncludeSource);
    }
}

internal unsafe partial struct DxcBuffer
{
    [NativeTypeName("LPCVOID")]
    public void* Ptr;

    [NativeTypeName("SIZE_T")]
    public nuint Size;

    public uint Encoding;
}

internal unsafe partial struct DxcDefine
{
    [NativeTypeName("LPCWSTR")]
    public char* Name;

    [NativeTypeName("LPCWSTR")]
    public char* Value;
}

[Guid("73EFFE2A-70DC-45F8-9690-EFF64C02429D")]
[NativeTypeName("struct IDxcCompilerArgs : IUnknown")]
internal unsafe partial struct IDxcCompilerArgs : IDxcCompilerArgs.Interface, INativeGuid
{
    static Guid* INativeGuid.NativeGuid => (Guid*)Unsafe.AsPointer(in IID_IDxcCompilerArgs);

    public void** lpVtbl;

    [return: NativeTypeName("HRESULT")]
    public int QueryInterface([NativeTypeName("const IID &")] Guid* riid, void** ppvObject)
    {
        return ((delegate* unmanaged[Stdcall]<IDxcCompilerArgs*, Guid*, void**, int>)(lpVtbl[0]))((IDxcCompilerArgs*)Unsafe.AsPointer(ref this), riid, ppvObject);
    }

    [return: NativeTypeName("ULONG")]
    public uint AddRef()
    {
        return ((delegate* unmanaged[Stdcall]<IDxcCompilerArgs*, uint>)(lpVtbl[1]))((IDxcCompilerArgs*)Unsafe.AsPointer(ref this));
    }

    [return: NativeTypeName("ULONG")]
    public uint Release()
    {
        return ((delegate* unmanaged[Stdcall]<IDxcCompilerArgs*, uint>)(lpVtbl[2]))((IDxcCompilerArgs*)Unsafe.AsPointer(ref this));
    }

    [return: NativeTypeName("LPCWSTR *")]
    public char** GetArguments()
    {
        return ((delegate* unmanaged[Stdcall]<IDxcCompilerArgs*, char**>)(lpVtbl[3]))((IDxcCompilerArgs*)Unsafe.AsPointer(ref this));
    }

    [return: NativeTypeName("UINT32")]
    public uint GetCount()
    {
        return ((delegate* unmanaged[Stdcall]<IDxcCompilerArgs*, uint>)(lpVtbl[4]))((IDxcCompilerArgs*)Unsafe.AsPointer(ref this));
    }

    [return: NativeTypeName("HRESULT")]
    public int AddArguments([NativeTypeName("LPCWSTR *")] char** pArguments, [NativeTypeName("UINT32")] uint argCount)
    {
        return ((delegate* unmanaged[Stdcall]<IDxcCompilerArgs*, char**, uint, int>)(lpVtbl[5]))((IDxcCompilerArgs*)Unsafe.AsPointer(ref this), pArguments, argCount);
    }

    [return: NativeTypeName("HRESULT")]
    public int AddArgumentsUTF8([NativeTypeName("LPCSTR *")] sbyte** pArguments, [NativeTypeName("UINT32")] uint argCount)
    {
        return ((delegate* unmanaged[Stdcall]<IDxcCompilerArgs*, sbyte**, uint, int>)(lpVtbl[6]))((IDxcCompilerArgs*)Unsafe.AsPointer(ref this), pArguments, argCount);
    }

    [return: NativeTypeName("HRESULT")]
    public int AddDefines([NativeTypeName("const DxcDefine *")] DxcDefine* pDefines, [NativeTypeName("UINT32")] uint defineCount)
    {
        return ((delegate* unmanaged[Stdcall]<IDxcCompilerArgs*, DxcDefine*, uint, int>)(lpVtbl[7]))((IDxcCompilerArgs*)Unsafe.AsPointer(ref this), pDefines, defineCount);
    }

    public interface Interface : IUnknown.Interface
    {
        [return: NativeTypeName("LPCWSTR *")]
        char** GetArguments();

        [return: NativeTypeName("UINT32")]
        uint GetCount();

        [return: NativeTypeName("HRESULT")]
        int AddArguments([NativeTypeName("LPCWSTR *")] char** pArguments, [NativeTypeName("UINT32")] uint argCount);

        [return: NativeTypeName("HRESULT")]
        int AddArgumentsUTF8([NativeTypeName("LPCSTR *")] sbyte** pArguments, [NativeTypeName("UINT32")] uint argCount);

        [return: NativeTypeName("HRESULT")]
        int AddDefines([NativeTypeName("const DxcDefine *")] DxcDefine* pDefines, [NativeTypeName("UINT32")] uint defineCount);
    }
}

[Guid("E5204DC7-D18C-4C3C-BDFB-851673980FE7")]
[NativeTypeName("struct IDxcLibrary : IUnknown")]
internal unsafe partial struct IDxcLibrary : IDxcLibrary.Interface, INativeGuid
{
    static Guid* INativeGuid.NativeGuid => (Guid*)Unsafe.AsPointer(in IID_IDxcLibrary);

    public void** lpVtbl;

    [return: NativeTypeName("HRESULT")]
    public int GetBlobAsUtf16(IDxcBlob* pBlob, IDxcBlobEncoding** pBlobEncoding)
    {
        return this.GetBlobAsWide(pBlob, pBlobEncoding);
    }

    [return: NativeTypeName("HRESULT")]
    public int QueryInterface([NativeTypeName("const IID &")] Guid* riid, void** ppvObject)
    {
        return ((delegate* unmanaged[Stdcall]<IDxcLibrary*, Guid*, void**, int>)(lpVtbl[0]))((IDxcLibrary*)Unsafe.AsPointer(ref this), riid, ppvObject);
    }

    [return: NativeTypeName("ULONG")]
    public uint AddRef()
    {
        return ((delegate* unmanaged[Stdcall]<IDxcLibrary*, uint>)(lpVtbl[1]))((IDxcLibrary*)Unsafe.AsPointer(ref this));
    }

    [return: NativeTypeName("ULONG")]
    public uint Release()
    {
        return ((delegate* unmanaged[Stdcall]<IDxcLibrary*, uint>)(lpVtbl[2]))((IDxcLibrary*)Unsafe.AsPointer(ref this));
    }

    [return: NativeTypeName("HRESULT")]
    public int SetMalloc(IMalloc* pMalloc)
    {
        return ((delegate* unmanaged[Stdcall]<IDxcLibrary*, IMalloc*, int>)(lpVtbl[3]))((IDxcLibrary*)Unsafe.AsPointer(ref this), pMalloc);
    }

    [return: NativeTypeName("HRESULT")]
    public int CreateBlobFromBlob(IDxcBlob* pBlob, [NativeTypeName("UINT32")] uint offset, [NativeTypeName("UINT32")] uint length, IDxcBlob** ppResult)
    {
        return ((delegate* unmanaged[Stdcall]<IDxcLibrary*, IDxcBlob*, uint, uint, IDxcBlob**, int>)(lpVtbl[4]))((IDxcLibrary*)Unsafe.AsPointer(ref this), pBlob, offset, length, ppResult);
    }

    [return: NativeTypeName("HRESULT")]
    public int CreateBlobFromFile([NativeTypeName("LPCWSTR")] char* pFileName, [NativeTypeName("UINT32 *")] uint* codePage, IDxcBlobEncoding** pBlobEncoding)
    {
        return ((delegate* unmanaged[Stdcall]<IDxcLibrary*, char*, uint*, IDxcBlobEncoding**, int>)(lpVtbl[5]))((IDxcLibrary*)Unsafe.AsPointer(ref this), pFileName, codePage, pBlobEncoding);
    }

    [return: NativeTypeName("HRESULT")]
    public int CreateBlobWithEncodingFromPinned([NativeTypeName("LPCVOID")] void* pText, [NativeTypeName("UINT32")] uint size, [NativeTypeName("UINT32")] uint codePage, IDxcBlobEncoding** pBlobEncoding)
    {
        return ((delegate* unmanaged[Stdcall]<IDxcLibrary*, void*, uint, uint, IDxcBlobEncoding**, int>)(lpVtbl[6]))((IDxcLibrary*)Unsafe.AsPointer(ref this), pText, size, codePage, pBlobEncoding);
    }

    [return: NativeTypeName("HRESULT")]
    public int CreateBlobWithEncodingOnHeapCopy([NativeTypeName("LPCVOID")] void* pText, [NativeTypeName("UINT32")] uint size, [NativeTypeName("UINT32")] uint codePage, IDxcBlobEncoding** pBlobEncoding)
    {
        return ((delegate* unmanaged[Stdcall]<IDxcLibrary*, void*, uint, uint, IDxcBlobEncoding**, int>)(lpVtbl[7]))((IDxcLibrary*)Unsafe.AsPointer(ref this), pText, size, codePage, pBlobEncoding);
    }

    [return: NativeTypeName("HRESULT")]
    public int CreateBlobWithEncodingOnMalloc([NativeTypeName("LPCVOID")] void* pText, IMalloc* pIMalloc, [NativeTypeName("UINT32")] uint size, [NativeTypeName("UINT32")] uint codePage, IDxcBlobEncoding** pBlobEncoding)
    {
        return ((delegate* unmanaged[Stdcall]<IDxcLibrary*, void*, IMalloc*, uint, uint, IDxcBlobEncoding**, int>)(lpVtbl[8]))((IDxcLibrary*)Unsafe.AsPointer(ref this), pText, pIMalloc, size, codePage, pBlobEncoding);
    }

    [return: NativeTypeName("HRESULT")]
    public int CreateIncludeHandler(IDxcIncludeHandler** ppResult)
    {
        return ((delegate* unmanaged[Stdcall]<IDxcLibrary*, IDxcIncludeHandler**, int>)(lpVtbl[9]))((IDxcLibrary*)Unsafe.AsPointer(ref this), ppResult);
    }

    [return: NativeTypeName("HRESULT")]
    public int CreateStreamFromBlobReadOnly(IDxcBlob* pBlob, IStream** ppStream)
    {
        return ((delegate* unmanaged[Stdcall]<IDxcLibrary*, IDxcBlob*, IStream**, int>)(lpVtbl[10]))((IDxcLibrary*)Unsafe.AsPointer(ref this), pBlob, ppStream);
    }

    [return: NativeTypeName("HRESULT")]
    public int GetBlobAsUtf8(IDxcBlob* pBlob, IDxcBlobEncoding** pBlobEncoding)
    {
        return ((delegate* unmanaged[Stdcall]<IDxcLibrary*, IDxcBlob*, IDxcBlobEncoding**, int>)(lpVtbl[11]))((IDxcLibrary*)Unsafe.AsPointer(ref this), pBlob, pBlobEncoding);
    }

    [return: NativeTypeName("HRESULT")]
    public int GetBlobAsWide(IDxcBlob* pBlob, IDxcBlobEncoding** pBlobEncoding)
    {
        return ((delegate* unmanaged[Stdcall]<IDxcLibrary*, IDxcBlob*, IDxcBlobEncoding**, int>)(lpVtbl[12]))((IDxcLibrary*)Unsafe.AsPointer(ref this), pBlob, pBlobEncoding);
    }

    public interface Interface : IUnknown.Interface
    {
        [return: NativeTypeName("HRESULT")]
        int SetMalloc(IMalloc* pMalloc);

        [return: NativeTypeName("HRESULT")]
        int CreateBlobFromBlob(IDxcBlob* pBlob, [NativeTypeName("UINT32")] uint offset, [NativeTypeName("UINT32")] uint length, IDxcBlob** ppResult);

        [return: NativeTypeName("HRESULT")]
        int CreateBlobFromFile([NativeTypeName("LPCWSTR")] char* pFileName, [NativeTypeName("UINT32 *")] uint* codePage, IDxcBlobEncoding** pBlobEncoding);

        [return: NativeTypeName("HRESULT")]
        int CreateBlobWithEncodingFromPinned([NativeTypeName("LPCVOID")] void* pText, [NativeTypeName("UINT32")] uint size, [NativeTypeName("UINT32")] uint codePage, IDxcBlobEncoding** pBlobEncoding);

        [return: NativeTypeName("HRESULT")]
        int CreateBlobWithEncodingOnHeapCopy([NativeTypeName("LPCVOID")] void* pText, [NativeTypeName("UINT32")] uint size, [NativeTypeName("UINT32")] uint codePage, IDxcBlobEncoding** pBlobEncoding);

        [return: NativeTypeName("HRESULT")]
        int CreateBlobWithEncodingOnMalloc([NativeTypeName("LPCVOID")] void* pText, IMalloc* pIMalloc, [NativeTypeName("UINT32")] uint size, [NativeTypeName("UINT32")] uint codePage, IDxcBlobEncoding** pBlobEncoding);

        [return: NativeTypeName("HRESULT")]
        int CreateIncludeHandler(IDxcIncludeHandler** ppResult);

        [return: NativeTypeName("HRESULT")]
        int CreateStreamFromBlobReadOnly(IDxcBlob* pBlob, IStream** ppStream);

        [return: NativeTypeName("HRESULT")]
        int GetBlobAsUtf8(IDxcBlob* pBlob, IDxcBlobEncoding** pBlobEncoding);

        [return: NativeTypeName("HRESULT")]
        int GetBlobAsWide(IDxcBlob* pBlob, IDxcBlobEncoding** pBlobEncoding);
    }
}

[Guid("CEDB484A-D4E9-445A-B991-CA21CA157DC2")]
[NativeTypeName("struct IDxcOperationResult : IUnknown")]
internal unsafe partial struct IDxcOperationResult : IDxcOperationResult.Interface, INativeGuid
{
    static Guid* INativeGuid.NativeGuid => (Guid*)Unsafe.AsPointer(in IID_IDxcOperationResult);

    public void** lpVtbl;

    [return: NativeTypeName("HRESULT")]
    public int QueryInterface([NativeTypeName("const IID &")] Guid* riid, void** ppvObject)
    {
        return ((delegate* unmanaged[Stdcall]<IDxcOperationResult*, Guid*, void**, int>)(lpVtbl[0]))((IDxcOperationResult*)Unsafe.AsPointer(ref this), riid, ppvObject);
    }

    [return: NativeTypeName("ULONG")]
    public uint AddRef()
    {
        return ((delegate* unmanaged[Stdcall]<IDxcOperationResult*, uint>)(lpVtbl[1]))((IDxcOperationResult*)Unsafe.AsPointer(ref this));
    }

    [return: NativeTypeName("ULONG")]
    public uint Release()
    {
        return ((delegate* unmanaged[Stdcall]<IDxcOperationResult*, uint>)(lpVtbl[2]))((IDxcOperationResult*)Unsafe.AsPointer(ref this));
    }

    [return: NativeTypeName("HRESULT")]
    public int GetStatus([NativeTypeName("HRESULT *")] int* pStatus)
    {
        return ((delegate* unmanaged[Stdcall]<IDxcOperationResult*, int*, int>)(lpVtbl[3]))((IDxcOperationResult*)Unsafe.AsPointer(ref this), pStatus);
    }

    [return: NativeTypeName("HRESULT")]
    public int GetResult(IDxcBlob** ppResult)
    {
        return ((delegate* unmanaged[Stdcall]<IDxcOperationResult*, IDxcBlob**, int>)(lpVtbl[4]))((IDxcOperationResult*)Unsafe.AsPointer(ref this), ppResult);
    }

    [return: NativeTypeName("HRESULT")]
    public int GetErrorBuffer(IDxcBlobEncoding** ppErrors)
    {
        return ((delegate* unmanaged[Stdcall]<IDxcOperationResult*, IDxcBlobEncoding**, int>)(lpVtbl[5]))((IDxcOperationResult*)Unsafe.AsPointer(ref this), ppErrors);
    }

    public interface Interface : IUnknown.Interface
    {
        [return: NativeTypeName("HRESULT")]
        int GetStatus([NativeTypeName("HRESULT *")] int* pStatus);

        [return: NativeTypeName("HRESULT")]
        int GetResult(IDxcBlob** ppResult);

        [return: NativeTypeName("HRESULT")]
        int GetErrorBuffer(IDxcBlobEncoding** ppErrors);
    }
}

[Guid("8C210BF3-011F-4422-8D70-6F9ACB8DB617")]
[NativeTypeName("struct IDxcCompiler : IUnknown")]
internal unsafe partial struct IDxcCompiler : IDxcCompiler.Interface, INativeGuid
{
    static Guid* INativeGuid.NativeGuid => (Guid*)Unsafe.AsPointer(in IID_IDxcCompiler);

    public void** lpVtbl;

    [return: NativeTypeName("HRESULT")]
    public int QueryInterface([NativeTypeName("const IID &")] Guid* riid, void** ppvObject)
    {
        return ((delegate* unmanaged[Stdcall]<IDxcCompiler*, Guid*, void**, int>)(lpVtbl[0]))((IDxcCompiler*)Unsafe.AsPointer(ref this), riid, ppvObject);
    }

    [return: NativeTypeName("ULONG")]
    public uint AddRef()
    {
        return ((delegate* unmanaged[Stdcall]<IDxcCompiler*, uint>)(lpVtbl[1]))((IDxcCompiler*)Unsafe.AsPointer(ref this));
    }

    [return: NativeTypeName("ULONG")]
    public uint Release()
    {
        return ((delegate* unmanaged[Stdcall]<IDxcCompiler*, uint>)(lpVtbl[2]))((IDxcCompiler*)Unsafe.AsPointer(ref this));
    }

    [return: NativeTypeName("HRESULT")]
    public int Compile(IDxcBlob* pSource, [NativeTypeName("LPCWSTR")] char* pSourceName, [NativeTypeName("LPCWSTR")] char* pEntryPoint, [NativeTypeName("LPCWSTR")] char* pTargetProfile, [NativeTypeName("LPCWSTR *")] char** pArguments, [NativeTypeName("UINT32")] uint argCount, [NativeTypeName("const DxcDefine *")] DxcDefine* pDefines, [NativeTypeName("UINT32")] uint defineCount, IDxcIncludeHandler* pIncludeHandler, IDxcOperationResult** ppResult)
    {
        return ((delegate* unmanaged[Stdcall]<IDxcCompiler*, IDxcBlob*, char*, char*, char*, char**, uint, DxcDefine*, uint, IDxcIncludeHandler*, IDxcOperationResult**, int>)(lpVtbl[3]))((IDxcCompiler*)Unsafe.AsPointer(ref this), pSource, pSourceName, pEntryPoint, pTargetProfile, pArguments, argCount, pDefines, defineCount, pIncludeHandler, ppResult);
    }

    [return: NativeTypeName("HRESULT")]
    public int Preprocess(IDxcBlob* pSource, [NativeTypeName("LPCWSTR")] char* pSourceName, [NativeTypeName("LPCWSTR *")] char** pArguments, [NativeTypeName("UINT32")] uint argCount, [NativeTypeName("const DxcDefine *")] DxcDefine* pDefines, [NativeTypeName("UINT32")] uint defineCount, IDxcIncludeHandler* pIncludeHandler, IDxcOperationResult** ppResult)
    {
        return ((delegate* unmanaged[Stdcall]<IDxcCompiler*, IDxcBlob*, char*, char**, uint, DxcDefine*, uint, IDxcIncludeHandler*, IDxcOperationResult**, int>)(lpVtbl[4]))((IDxcCompiler*)Unsafe.AsPointer(ref this), pSource, pSourceName, pArguments, argCount, pDefines, defineCount, pIncludeHandler, ppResult);
    }

    [return: NativeTypeName("HRESULT")]
    public int Disassemble(IDxcBlob* pSource, IDxcBlobEncoding** ppDisassembly)
    {
        return ((delegate* unmanaged[Stdcall]<IDxcCompiler*, IDxcBlob*, IDxcBlobEncoding**, int>)(lpVtbl[5]))((IDxcCompiler*)Unsafe.AsPointer(ref this), pSource, ppDisassembly);
    }

    public interface Interface : IUnknown.Interface
    {
        [return: NativeTypeName("HRESULT")]
        int Compile(IDxcBlob* pSource, [NativeTypeName("LPCWSTR")] char* pSourceName, [NativeTypeName("LPCWSTR")] char* pEntryPoint, [NativeTypeName("LPCWSTR")] char* pTargetProfile, [NativeTypeName("LPCWSTR *")] char** pArguments, [NativeTypeName("UINT32")] uint argCount, [NativeTypeName("const DxcDefine *")] DxcDefine* pDefines, [NativeTypeName("UINT32")] uint defineCount, IDxcIncludeHandler* pIncludeHandler, IDxcOperationResult** ppResult);

        [return: NativeTypeName("HRESULT")]
        int Preprocess(IDxcBlob* pSource, [NativeTypeName("LPCWSTR")] char* pSourceName, [NativeTypeName("LPCWSTR *")] char** pArguments, [NativeTypeName("UINT32")] uint argCount, [NativeTypeName("const DxcDefine *")] DxcDefine* pDefines, [NativeTypeName("UINT32")] uint defineCount, IDxcIncludeHandler* pIncludeHandler, IDxcOperationResult** ppResult);

        [return: NativeTypeName("HRESULT")]
        int Disassemble(IDxcBlob* pSource, IDxcBlobEncoding** ppDisassembly);
    }
}

[Guid("A005A9D9-B8BB-4594-B5C9-0E633BEC4D37")]
[NativeTypeName("struct IDxcCompiler2 : IDxcCompiler")]
internal unsafe partial struct IDxcCompiler2 : IDxcCompiler2.Interface, INativeGuid
{
    static Guid* INativeGuid.NativeGuid => (Guid*)Unsafe.AsPointer(in IID_IDxcCompiler2);

    public void** lpVtbl;

    [return: NativeTypeName("HRESULT")]
    public int QueryInterface([NativeTypeName("const IID &")] Guid* riid, void** ppvObject)
    {
        return ((delegate* unmanaged[Stdcall]<IDxcCompiler2*, Guid*, void**, int>)(lpVtbl[0]))((IDxcCompiler2*)Unsafe.AsPointer(ref this), riid, ppvObject);
    }

    [return: NativeTypeName("ULONG")]
    public uint AddRef()
    {
        return ((delegate* unmanaged[Stdcall]<IDxcCompiler2*, uint>)(lpVtbl[1]))((IDxcCompiler2*)Unsafe.AsPointer(ref this));
    }

    [return: NativeTypeName("ULONG")]
    public uint Release()
    {
        return ((delegate* unmanaged[Stdcall]<IDxcCompiler2*, uint>)(lpVtbl[2]))((IDxcCompiler2*)Unsafe.AsPointer(ref this));
    }

    [return: NativeTypeName("HRESULT")]
    public int Compile(IDxcBlob* pSource, [NativeTypeName("LPCWSTR")] char* pSourceName, [NativeTypeName("LPCWSTR")] char* pEntryPoint, [NativeTypeName("LPCWSTR")] char* pTargetProfile, [NativeTypeName("LPCWSTR *")] char** pArguments, [NativeTypeName("UINT32")] uint argCount, [NativeTypeName("const DxcDefine *")] DxcDefine* pDefines, [NativeTypeName("UINT32")] uint defineCount, IDxcIncludeHandler* pIncludeHandler, IDxcOperationResult** ppResult)
    {
        return ((delegate* unmanaged[Stdcall]<IDxcCompiler2*, IDxcBlob*, char*, char*, char*, char**, uint, DxcDefine*, uint, IDxcIncludeHandler*, IDxcOperationResult**, int>)(lpVtbl[3]))((IDxcCompiler2*)Unsafe.AsPointer(ref this), pSource, pSourceName, pEntryPoint, pTargetProfile, pArguments, argCount, pDefines, defineCount, pIncludeHandler, ppResult);
    }

    [return: NativeTypeName("HRESULT")]
    public int Preprocess(IDxcBlob* pSource, [NativeTypeName("LPCWSTR")] char* pSourceName, [NativeTypeName("LPCWSTR *")] char** pArguments, [NativeTypeName("UINT32")] uint argCount, [NativeTypeName("const DxcDefine *")] DxcDefine* pDefines, [NativeTypeName("UINT32")] uint defineCount, IDxcIncludeHandler* pIncludeHandler, IDxcOperationResult** ppResult)
    {
        return ((delegate* unmanaged[Stdcall]<IDxcCompiler2*, IDxcBlob*, char*, char**, uint, DxcDefine*, uint, IDxcIncludeHandler*, IDxcOperationResult**, int>)(lpVtbl[4]))((IDxcCompiler2*)Unsafe.AsPointer(ref this), pSource, pSourceName, pArguments, argCount, pDefines, defineCount, pIncludeHandler, ppResult);
    }

    [return: NativeTypeName("HRESULT")]
    public int Disassemble(IDxcBlob* pSource, IDxcBlobEncoding** ppDisassembly)
    {
        return ((delegate* unmanaged[Stdcall]<IDxcCompiler2*, IDxcBlob*, IDxcBlobEncoding**, int>)(lpVtbl[5]))((IDxcCompiler2*)Unsafe.AsPointer(ref this), pSource, ppDisassembly);
    }

    [return: NativeTypeName("HRESULT")]
    public int CompileWithDebug(IDxcBlob* pSource, [NativeTypeName("LPCWSTR")] char* pSourceName, [NativeTypeName("LPCWSTR")] char* pEntryPoint, [NativeTypeName("LPCWSTR")] char* pTargetProfile, [NativeTypeName("LPCWSTR *")] char** pArguments, [NativeTypeName("UINT32")] uint argCount, [NativeTypeName("const DxcDefine *")] DxcDefine* pDefines, [NativeTypeName("UINT32")] uint defineCount, IDxcIncludeHandler* pIncludeHandler, IDxcOperationResult** ppResult, [NativeTypeName("LPWSTR *")] char** ppDebugBlobName, IDxcBlob** ppDebugBlob)
    {
        return ((delegate* unmanaged[Stdcall]<IDxcCompiler2*, IDxcBlob*, char*, char*, char*, char**, uint, DxcDefine*, uint, IDxcIncludeHandler*, IDxcOperationResult**, char**, IDxcBlob**, int>)(lpVtbl[6]))((IDxcCompiler2*)Unsafe.AsPointer(ref this), pSource, pSourceName, pEntryPoint, pTargetProfile, pArguments, argCount, pDefines, defineCount, pIncludeHandler, ppResult, ppDebugBlobName, ppDebugBlob);
    }

    public interface Interface : IDxcCompiler.Interface
    {
        [return: NativeTypeName("HRESULT")]
        int CompileWithDebug(IDxcBlob* pSource, [NativeTypeName("LPCWSTR")] char* pSourceName, [NativeTypeName("LPCWSTR")] char* pEntryPoint, [NativeTypeName("LPCWSTR")] char* pTargetProfile, [NativeTypeName("LPCWSTR *")] char** pArguments, [NativeTypeName("UINT32")] uint argCount, [NativeTypeName("const DxcDefine *")] DxcDefine* pDefines, [NativeTypeName("UINT32")] uint defineCount, IDxcIncludeHandler* pIncludeHandler, IDxcOperationResult** ppResult, [NativeTypeName("LPWSTR *")] char** ppDebugBlobName, IDxcBlob** ppDebugBlob);
    }
}

[Guid("F1B5BE2A-62DD-4327-A1C2-42AC1E1E78E6")]
[NativeTypeName("struct IDxcLinker : IUnknown")]
internal unsafe partial struct IDxcLinker : IDxcLinker.Interface, INativeGuid
{
    static Guid* INativeGuid.NativeGuid => (Guid*)Unsafe.AsPointer(in IID_IDxcLinker);

    public void** lpVtbl;

    [return: NativeTypeName("HRESULT")]
    public int QueryInterface([NativeTypeName("const IID &")] Guid* riid, void** ppvObject)
    {
        return ((delegate* unmanaged[Stdcall]<IDxcLinker*, Guid*, void**, int>)(lpVtbl[0]))((IDxcLinker*)Unsafe.AsPointer(ref this), riid, ppvObject);
    }

    [return: NativeTypeName("ULONG")]
    public uint AddRef()
    {
        return ((delegate* unmanaged[Stdcall]<IDxcLinker*, uint>)(lpVtbl[1]))((IDxcLinker*)Unsafe.AsPointer(ref this));
    }

    [return: NativeTypeName("ULONG")]
    public uint Release()
    {
        return ((delegate* unmanaged[Stdcall]<IDxcLinker*, uint>)(lpVtbl[2]))((IDxcLinker*)Unsafe.AsPointer(ref this));
    }

    [return: NativeTypeName("HRESULT")]
    public int RegisterLibrary([NativeTypeName("LPCWSTR")] char* pLibName, IDxcBlob* pLib)
    {
        return ((delegate* unmanaged[Thiscall]<IDxcLinker*, char*, IDxcBlob*, int>)(lpVtbl[3]))((IDxcLinker*)Unsafe.AsPointer(ref this), pLibName, pLib);
    }

    [return: NativeTypeName("HRESULT")]
    public int Link([NativeTypeName("LPCWSTR")] char* pEntryName, [NativeTypeName("LPCWSTR")] char* pTargetProfile, [NativeTypeName("const LPCWSTR *")] char** pLibNames, [NativeTypeName("UINT32")] uint libCount, [NativeTypeName("const LPCWSTR *")] char** pArguments, [NativeTypeName("UINT32")] uint argCount, IDxcOperationResult** ppResult)
    {
        return ((delegate* unmanaged[Stdcall]<IDxcLinker*, char*, char*, char**, uint, char**, uint, IDxcOperationResult**, int>)(lpVtbl[4]))((IDxcLinker*)Unsafe.AsPointer(ref this), pEntryName, pTargetProfile, pLibNames, libCount, pArguments, argCount, ppResult);
    }

    public interface Interface : IUnknown.Interface
    {
        [return: NativeTypeName("HRESULT")]
        int RegisterLibrary([NativeTypeName("LPCWSTR")] char* pLibName, IDxcBlob* pLib);

        [return: NativeTypeName("HRESULT")]
        int Link([NativeTypeName("LPCWSTR")] char* pEntryName, [NativeTypeName("LPCWSTR")] char* pTargetProfile, [NativeTypeName("const LPCWSTR *")] char** pLibNames, [NativeTypeName("UINT32")] uint libCount, [NativeTypeName("const LPCWSTR *")] char** pArguments, [NativeTypeName("UINT32")] uint argCount, IDxcOperationResult** ppResult);
    }
}

[Guid("4605C4CB-2019-492A-ADA4-65F20BB7D67F")]
[NativeTypeName("struct IDxcUtils : IUnknown")]
internal unsafe partial struct IDxcUtils : IDxcUtils.Interface, INativeGuid
{
    static Guid* INativeGuid.NativeGuid => (Guid*)Unsafe.AsPointer(in IID_IDxcUtils);

    public void** lpVtbl;

    [return: NativeTypeName("HRESULT")]
    public int GetBlobAsUtf16(IDxcBlob* pBlob, IDxcBlobWide** ppBlobEncoding)
    {
        return this.GetBlobAsWide(pBlob, ppBlobEncoding);
    }

    [return: NativeTypeName("HRESULT")]
    public int QueryInterface([NativeTypeName("const IID &")] Guid* riid, void** ppvObject)
    {
        return ((delegate* unmanaged[Stdcall]<IDxcUtils*, Guid*, void**, int>)(lpVtbl[0]))((IDxcUtils*)Unsafe.AsPointer(ref this), riid, ppvObject);
    }

    [return: NativeTypeName("ULONG")]
    public uint AddRef()
    {
        return ((delegate* unmanaged[Stdcall]<IDxcUtils*, uint>)(lpVtbl[1]))((IDxcUtils*)Unsafe.AsPointer(ref this));
    }

    [return: NativeTypeName("ULONG")]
    public uint Release()
    {
        return ((delegate* unmanaged[Stdcall]<IDxcUtils*, uint>)(lpVtbl[2]))((IDxcUtils*)Unsafe.AsPointer(ref this));
    }

    [return: NativeTypeName("HRESULT")]
    public int CreateBlobFromBlob(IDxcBlob* pBlob, [NativeTypeName("UINT32")] uint offset, [NativeTypeName("UINT32")] uint length, IDxcBlob** ppResult)
    {
        return ((delegate* unmanaged[Stdcall]<IDxcUtils*, IDxcBlob*, uint, uint, IDxcBlob**, int>)(lpVtbl[3]))((IDxcUtils*)Unsafe.AsPointer(ref this), pBlob, offset, length, ppResult);
    }

    [return: NativeTypeName("HRESULT")]
    public int CreateBlobFromPinned([NativeTypeName("LPCVOID")] void* pData, [NativeTypeName("UINT32")] uint size, [NativeTypeName("UINT32")] uint codePage, IDxcBlobEncoding** ppBlobEncoding)
    {
        return ((delegate* unmanaged[Stdcall]<IDxcUtils*, void*, uint, uint, IDxcBlobEncoding**, int>)(lpVtbl[4]))((IDxcUtils*)Unsafe.AsPointer(ref this), pData, size, codePage, ppBlobEncoding);
    }

    [return: NativeTypeName("HRESULT")]
    public int MoveToBlob([NativeTypeName("LPCVOID")] void* pData, IMalloc* pIMalloc, [NativeTypeName("UINT32")] uint size, [NativeTypeName("UINT32")] uint codePage, IDxcBlobEncoding** ppBlobEncoding)
    {
        return ((delegate* unmanaged[Stdcall]<IDxcUtils*, void*, IMalloc*, uint, uint, IDxcBlobEncoding**, int>)(lpVtbl[5]))((IDxcUtils*)Unsafe.AsPointer(ref this), pData, pIMalloc, size, codePage, ppBlobEncoding);
    }

    [return: NativeTypeName("HRESULT")]
    public int CreateBlob([NativeTypeName("LPCVOID")] void* pData, [NativeTypeName("UINT32")] uint size, [NativeTypeName("UINT32")] uint codePage, IDxcBlobEncoding** ppBlobEncoding)
    {
        return ((delegate* unmanaged[Stdcall]<IDxcUtils*, void*, uint, uint, IDxcBlobEncoding**, int>)(lpVtbl[6]))((IDxcUtils*)Unsafe.AsPointer(ref this), pData, size, codePage, ppBlobEncoding);
    }

    [return: NativeTypeName("HRESULT")]
    public int LoadFile([NativeTypeName("LPCWSTR")] char* pFileName, [NativeTypeName("UINT32 *")] uint* pCodePage, IDxcBlobEncoding** ppBlobEncoding)
    {
        return ((delegate* unmanaged[Stdcall]<IDxcUtils*, char*, uint*, IDxcBlobEncoding**, int>)(lpVtbl[7]))((IDxcUtils*)Unsafe.AsPointer(ref this), pFileName, pCodePage, ppBlobEncoding);
    }

    [return: NativeTypeName("HRESULT")]
    public int CreateReadOnlyStreamFromBlob(IDxcBlob* pBlob, IStream** ppStream)
    {
        return ((delegate* unmanaged[Stdcall]<IDxcUtils*, IDxcBlob*, IStream**, int>)(lpVtbl[8]))((IDxcUtils*)Unsafe.AsPointer(ref this), pBlob, ppStream);
    }

    [return: NativeTypeName("HRESULT")]
    public int CreateDefaultIncludeHandler(IDxcIncludeHandler** ppResult)
    {
        return ((delegate* unmanaged[Stdcall]<IDxcUtils*, IDxcIncludeHandler**, int>)(lpVtbl[9]))((IDxcUtils*)Unsafe.AsPointer(ref this), ppResult);
    }

    [return: NativeTypeName("HRESULT")]
    public int GetBlobAsUtf8(IDxcBlob* pBlob, IDxcBlobUtf8** ppBlobEncoding)
    {
        return ((delegate* unmanaged[Stdcall]<IDxcUtils*, IDxcBlob*, IDxcBlobUtf8**, int>)(lpVtbl[10]))((IDxcUtils*)Unsafe.AsPointer(ref this), pBlob, ppBlobEncoding);
    }

    [return: NativeTypeName("HRESULT")]
    public int GetBlobAsWide(IDxcBlob* pBlob, IDxcBlobWide** ppBlobEncoding)
    {
        return ((delegate* unmanaged[Stdcall]<IDxcUtils*, IDxcBlob*, IDxcBlobWide**, int>)(lpVtbl[11]))((IDxcUtils*)Unsafe.AsPointer(ref this), pBlob, ppBlobEncoding);
    }

    [return: NativeTypeName("HRESULT")]
    public int GetDxilContainerPart([NativeTypeName("const DxcBuffer *")] DxcBuffer* pShader, [NativeTypeName("UINT32")] uint DxcPart, void** ppPartData, [NativeTypeName("UINT32 *")] uint* pPartSizeInBytes)
    {
        return ((delegate* unmanaged[Stdcall]<IDxcUtils*, DxcBuffer*, uint, void**, uint*, int>)(lpVtbl[12]))((IDxcUtils*)Unsafe.AsPointer(ref this), pShader, DxcPart, ppPartData, pPartSizeInBytes);
    }

    [return: NativeTypeName("HRESULT")]
    public int CreateReflection([NativeTypeName("const DxcBuffer *")] DxcBuffer* pData, [NativeTypeName("const IID &")] Guid* iid, void** ppvReflection)
    {
        return ((delegate* unmanaged[Stdcall]<IDxcUtils*, DxcBuffer*, Guid*, void**, int>)(lpVtbl[13]))((IDxcUtils*)Unsafe.AsPointer(ref this), pData, iid, ppvReflection);
    }

    [return: NativeTypeName("HRESULT")]
    public int BuildArguments([NativeTypeName("LPCWSTR")] char* pSourceName, [NativeTypeName("LPCWSTR")] char* pEntryPoint, [NativeTypeName("LPCWSTR")] char* pTargetProfile, [NativeTypeName("LPCWSTR *")] char** pArguments, [NativeTypeName("UINT32")] uint argCount, [NativeTypeName("const DxcDefine *")] DxcDefine* pDefines, [NativeTypeName("UINT32")] uint defineCount, IDxcCompilerArgs** ppArgs)
    {
        return ((delegate* unmanaged[Stdcall]<IDxcUtils*, char*, char*, char*, char**, uint, DxcDefine*, uint, IDxcCompilerArgs**, int>)(lpVtbl[14]))((IDxcUtils*)Unsafe.AsPointer(ref this), pSourceName, pEntryPoint, pTargetProfile, pArguments, argCount, pDefines, defineCount, ppArgs);
    }

    [return: NativeTypeName("HRESULT")]
    public int GetPDBContents(IDxcBlob* pPDBBlob, IDxcBlob** ppHash, IDxcBlob** ppContainer)
    {
        return ((delegate* unmanaged[Stdcall]<IDxcUtils*, IDxcBlob*, IDxcBlob**, IDxcBlob**, int>)(lpVtbl[15]))((IDxcUtils*)Unsafe.AsPointer(ref this), pPDBBlob, ppHash, ppContainer);
    }

    public interface Interface : IUnknown.Interface
    {
        [return: NativeTypeName("HRESULT")]
        int CreateBlobFromBlob(IDxcBlob* pBlob, [NativeTypeName("UINT32")] uint offset, [NativeTypeName("UINT32")] uint length, IDxcBlob** ppResult);

        [return: NativeTypeName("HRESULT")]
        int CreateBlobFromPinned([NativeTypeName("LPCVOID")] void* pData, [NativeTypeName("UINT32")] uint size, [NativeTypeName("UINT32")] uint codePage, IDxcBlobEncoding** ppBlobEncoding);

        [return: NativeTypeName("HRESULT")]
        int MoveToBlob([NativeTypeName("LPCVOID")] void* pData, IMalloc* pIMalloc, [NativeTypeName("UINT32")] uint size, [NativeTypeName("UINT32")] uint codePage, IDxcBlobEncoding** ppBlobEncoding);

        [return: NativeTypeName("HRESULT")]
        int CreateBlob([NativeTypeName("LPCVOID")] void* pData, [NativeTypeName("UINT32")] uint size, [NativeTypeName("UINT32")] uint codePage, IDxcBlobEncoding** ppBlobEncoding);

        [return: NativeTypeName("HRESULT")]
        int LoadFile([NativeTypeName("LPCWSTR")] char* pFileName, [NativeTypeName("UINT32 *")] uint* pCodePage, IDxcBlobEncoding** ppBlobEncoding);

        [return: NativeTypeName("HRESULT")]
        int CreateReadOnlyStreamFromBlob(IDxcBlob* pBlob, IStream** ppStream);

        [return: NativeTypeName("HRESULT")]
        int CreateDefaultIncludeHandler(IDxcIncludeHandler** ppResult);

        [return: NativeTypeName("HRESULT")]
        int GetBlobAsUtf8(IDxcBlob* pBlob, IDxcBlobUtf8** ppBlobEncoding);

        [return: NativeTypeName("HRESULT")]
        int GetBlobAsWide(IDxcBlob* pBlob, IDxcBlobWide** ppBlobEncoding);

        [return: NativeTypeName("HRESULT")]
        int GetDxilContainerPart([NativeTypeName("const DxcBuffer *")] DxcBuffer* pShader, [NativeTypeName("UINT32")] uint DxcPart, void** ppPartData, [NativeTypeName("UINT32 *")] uint* pPartSizeInBytes);

        [return: NativeTypeName("HRESULT")]
        int CreateReflection([NativeTypeName("const DxcBuffer *")] DxcBuffer* pData, [NativeTypeName("const IID &")] Guid* iid, void** ppvReflection);

        [return: NativeTypeName("HRESULT")]
        int BuildArguments([NativeTypeName("LPCWSTR")] char* pSourceName, [NativeTypeName("LPCWSTR")] char* pEntryPoint, [NativeTypeName("LPCWSTR")] char* pTargetProfile, [NativeTypeName("LPCWSTR *")] char** pArguments, [NativeTypeName("UINT32")] uint argCount, [NativeTypeName("const DxcDefine *")] DxcDefine* pDefines, [NativeTypeName("UINT32")] uint defineCount, IDxcCompilerArgs** ppArgs);

        [return: NativeTypeName("HRESULT")]
        int GetPDBContents(IDxcBlob* pPDBBlob, IDxcBlob** ppHash, IDxcBlob** ppContainer);
    }
}

internal enum DXC_OUT_KIND
{
    DXC_OUT_NONE = 0,
    DXC_OUT_OBJECT = 1,
    DXC_OUT_ERRORS = 2,
    DXC_OUT_PDB = 3,
    DXC_OUT_SHADER_HASH = 4,
    DXC_OUT_DISASSEMBLY = 5,
    DXC_OUT_HLSL = 6,
    DXC_OUT_TEXT = 7,
    DXC_OUT_REFLECTION = 8,
    DXC_OUT_ROOT_SIGNATURE = 9,
    DXC_OUT_EXTRA_OUTPUTS = 10,
    DXC_OUT_REMARKS = 11,
    DXC_OUT_TIME_REPORT = 12,
    DXC_OUT_TIME_TRACE = 13,
    DXC_OUT_LAST = DXC_OUT_TIME_TRACE,
    DXC_OUT_NUM_ENUMS,
    DXC_OUT_FORCE_DWORD = unchecked((int)(0xFFFFFFFF)),
}

[Guid("58346CDA-DDE7-4497-9461-6F87AF5E0659")]
[NativeTypeName("struct IDxcResult : IDxcOperationResult")]
internal unsafe partial struct IDxcResult : IDxcResult.Interface, INativeGuid
{
    static Guid* INativeGuid.NativeGuid => (Guid*)Unsafe.AsPointer(in IID_IDxcResult);

    public void** lpVtbl;

    [return: NativeTypeName("HRESULT")]
    public int QueryInterface([NativeTypeName("const IID &")] Guid* riid, void** ppvObject)
    {
        return ((delegate* unmanaged[Stdcall]<IDxcResult*, Guid*, void**, int>)(lpVtbl[0]))((IDxcResult*)Unsafe.AsPointer(ref this), riid, ppvObject);
    }

    [return: NativeTypeName("ULONG")]
    public uint AddRef()
    {
        return ((delegate* unmanaged[Stdcall]<IDxcResult*, uint>)(lpVtbl[1]))((IDxcResult*)Unsafe.AsPointer(ref this));
    }

    [return: NativeTypeName("ULONG")]
    public uint Release()
    {
        return ((delegate* unmanaged[Stdcall]<IDxcResult*, uint>)(lpVtbl[2]))((IDxcResult*)Unsafe.AsPointer(ref this));
    }

    [return: NativeTypeName("HRESULT")]
    public int GetStatus([NativeTypeName("HRESULT *")] int* pStatus)
    {
        return ((delegate* unmanaged[Stdcall]<IDxcResult*, int*, int>)(lpVtbl[3]))((IDxcResult*)Unsafe.AsPointer(ref this), pStatus);
    }

    [return: NativeTypeName("HRESULT")]
    public int GetResult(IDxcBlob** ppResult)
    {
        return ((delegate* unmanaged[Stdcall]<IDxcResult*, IDxcBlob**, int>)(lpVtbl[4]))((IDxcResult*)Unsafe.AsPointer(ref this), ppResult);
    }

    [return: NativeTypeName("HRESULT")]
    public int GetErrorBuffer(IDxcBlobEncoding** ppErrors)
    {
        return ((delegate* unmanaged[Stdcall]<IDxcResult*, IDxcBlobEncoding**, int>)(lpVtbl[5]))((IDxcResult*)Unsafe.AsPointer(ref this), ppErrors);
    }

    [return: NativeTypeName("BOOL")]
    public int HasOutput(DXC_OUT_KIND dxcOutKind)
    {
        return ((delegate* unmanaged[Stdcall]<IDxcResult*, DXC_OUT_KIND, int>)(lpVtbl[6]))((IDxcResult*)Unsafe.AsPointer(ref this), dxcOutKind);
    }

    [return: NativeTypeName("HRESULT")]
    public int GetOutput(DXC_OUT_KIND dxcOutKind, [NativeTypeName("const IID &")] Guid* iid, void** ppvObject, IDxcBlobWide** ppOutputName)
    {
        return ((delegate* unmanaged[Stdcall]<IDxcResult*, DXC_OUT_KIND, Guid*, void**, IDxcBlobWide**, int>)(lpVtbl[7]))((IDxcResult*)Unsafe.AsPointer(ref this), dxcOutKind, iid, ppvObject, ppOutputName);
    }

    [return: NativeTypeName("UINT32")]
    public uint GetNumOutputs()
    {
        return ((delegate* unmanaged[Thiscall]<IDxcResult*, uint>)(lpVtbl[8]))((IDxcResult*)Unsafe.AsPointer(ref this));
    }

    public DXC_OUT_KIND GetOutputByIndex([NativeTypeName("UINT32")] uint Index)
    {
        return ((delegate* unmanaged[Thiscall]<IDxcResult*, uint, DXC_OUT_KIND>)(lpVtbl[9]))((IDxcResult*)Unsafe.AsPointer(ref this), Index);
    }

    public DXC_OUT_KIND PrimaryOutput()
    {
        return ((delegate* unmanaged[Thiscall]<IDxcResult*, DXC_OUT_KIND>)(lpVtbl[10]))((IDxcResult*)Unsafe.AsPointer(ref this));
    }

    public interface Interface : IDxcOperationResult.Interface
    {
        [return: NativeTypeName("BOOL")]
        int HasOutput(DXC_OUT_KIND dxcOutKind);

        [return: NativeTypeName("HRESULT")]
        int GetOutput(DXC_OUT_KIND dxcOutKind, [NativeTypeName("const IID &")] Guid* iid, void** ppvObject, IDxcBlobWide** ppOutputName);

        [return: NativeTypeName("UINT32")]
        uint GetNumOutputs();

        DXC_OUT_KIND GetOutputByIndex([NativeTypeName("UINT32")] uint Index);

        DXC_OUT_KIND PrimaryOutput();
    }
}

[Guid("319B37A2-A5C2-494A-A5DE-4801B2FAF989")]
[NativeTypeName("struct IDxcExtraOutputs : IUnknown")]
internal unsafe partial struct IDxcExtraOutputs : IDxcExtraOutputs.Interface, INativeGuid
{
    static Guid* INativeGuid.NativeGuid => (Guid*)Unsafe.AsPointer(in IID_IDxcExtraOutputs);

    public void** lpVtbl;

    [return: NativeTypeName("HRESULT")]
    public int QueryInterface([NativeTypeName("const IID &")] Guid* riid, void** ppvObject)
    {
        return ((delegate* unmanaged[Stdcall]<IDxcExtraOutputs*, Guid*, void**, int>)(lpVtbl[0]))((IDxcExtraOutputs*)Unsafe.AsPointer(ref this), riid, ppvObject);
    }

    [return: NativeTypeName("ULONG")]
    public uint AddRef()
    {
        return ((delegate* unmanaged[Stdcall]<IDxcExtraOutputs*, uint>)(lpVtbl[1]))((IDxcExtraOutputs*)Unsafe.AsPointer(ref this));
    }

    [return: NativeTypeName("ULONG")]
    public uint Release()
    {
        return ((delegate* unmanaged[Stdcall]<IDxcExtraOutputs*, uint>)(lpVtbl[2]))((IDxcExtraOutputs*)Unsafe.AsPointer(ref this));
    }

    [return: NativeTypeName("UINT32")]
    public uint GetOutputCount()
    {
        return ((delegate* unmanaged[Stdcall]<IDxcExtraOutputs*, uint>)(lpVtbl[3]))((IDxcExtraOutputs*)Unsafe.AsPointer(ref this));
    }

    [return: NativeTypeName("HRESULT")]
    public int GetOutput([NativeTypeName("UINT32")] uint uIndex, [NativeTypeName("const IID &")] Guid* iid, void** ppvObject, IDxcBlobWide** ppOutputType, IDxcBlobWide** ppOutputName)
    {
        return ((delegate* unmanaged[Stdcall]<IDxcExtraOutputs*, uint, Guid*, void**, IDxcBlobWide**, IDxcBlobWide**, int>)(lpVtbl[4]))((IDxcExtraOutputs*)Unsafe.AsPointer(ref this), uIndex, iid, ppvObject, ppOutputType, ppOutputName);
    }

    public interface Interface : IUnknown.Interface
    {
        [return: NativeTypeName("UINT32")]
        uint GetOutputCount();

        [return: NativeTypeName("HRESULT")]
        int GetOutput([NativeTypeName("UINT32")] uint uIndex, [NativeTypeName("const IID &")] Guid* iid, void** ppvObject, IDxcBlobWide** ppOutputType, IDxcBlobWide** ppOutputName);
    }
}

[Guid("228B4687-5A6A-4730-900C-9702B2203F54")]
[NativeTypeName("struct IDxcCompiler3 : IUnknown")]
internal unsafe partial struct IDxcCompiler3 : IDxcCompiler3.Interface, INativeGuid
{
    static Guid* INativeGuid.NativeGuid => (Guid*)Unsafe.AsPointer(in IID_IDxcCompiler3);

    public void** lpVtbl;

    [return: NativeTypeName("HRESULT")]
    public int QueryInterface([NativeTypeName("const IID &")] Guid* riid, void** ppvObject)
    {
        return ((delegate* unmanaged[Stdcall]<IDxcCompiler3*, Guid*, void**, int>)(lpVtbl[0]))((IDxcCompiler3*)Unsafe.AsPointer(ref this), riid, ppvObject);
    }

    [return: NativeTypeName("ULONG")]
    public uint AddRef()
    {
        return ((delegate* unmanaged[Stdcall]<IDxcCompiler3*, uint>)(lpVtbl[1]))((IDxcCompiler3*)Unsafe.AsPointer(ref this));
    }

    [return: NativeTypeName("ULONG")]
    public uint Release()
    {
        return ((delegate* unmanaged[Stdcall]<IDxcCompiler3*, uint>)(lpVtbl[2]))((IDxcCompiler3*)Unsafe.AsPointer(ref this));
    }

    [return: NativeTypeName("HRESULT")]
    public int Compile([NativeTypeName("const DxcBuffer *")] DxcBuffer* pSource, [NativeTypeName("LPCWSTR *")] char** pArguments, [NativeTypeName("UINT32")] uint argCount, IDxcIncludeHandler* pIncludeHandler, [NativeTypeName("const IID &")] Guid* riid, [NativeTypeName("LPVOID *")] void** ppResult)
    {
        return ((delegate* unmanaged[Stdcall]<IDxcCompiler3*, DxcBuffer*, char**, uint, IDxcIncludeHandler*, Guid*, void**, int>)(lpVtbl[3]))((IDxcCompiler3*)Unsafe.AsPointer(ref this), pSource, pArguments, argCount, pIncludeHandler, riid, ppResult);
    }

    [return: NativeTypeName("HRESULT")]
    public int Disassemble([NativeTypeName("const DxcBuffer *")] DxcBuffer* pObject, [NativeTypeName("const IID &")] Guid* riid, [NativeTypeName("LPVOID *")] void** ppResult)
    {
        return ((delegate* unmanaged[Stdcall]<IDxcCompiler3*, DxcBuffer*, Guid*, void**, int>)(lpVtbl[4]))((IDxcCompiler3*)Unsafe.AsPointer(ref this), pObject, riid, ppResult);
    }

    public interface Interface : IUnknown.Interface
    {
        [return: NativeTypeName("HRESULT")]
        int Compile([NativeTypeName("const DxcBuffer *")] DxcBuffer* pSource, [NativeTypeName("LPCWSTR *")] char** pArguments, [NativeTypeName("UINT32")] uint argCount, IDxcIncludeHandler* pIncludeHandler, [NativeTypeName("const IID &")] Guid* riid, [NativeTypeName("LPVOID *")] void** ppResult);

        [return: NativeTypeName("HRESULT")]
        int Disassemble([NativeTypeName("const DxcBuffer *")] DxcBuffer* pObject, [NativeTypeName("const IID &")] Guid* riid, [NativeTypeName("LPVOID *")] void** ppResult);
    }
}

[Guid("A6E82BD2-1FD7-4826-9811-2857E797F49A")]
[NativeTypeName("struct IDxcValidator : IUnknown")]
internal unsafe partial struct IDxcValidator : IDxcValidator.Interface, INativeGuid
{
    static Guid* INativeGuid.NativeGuid => (Guid*)Unsafe.AsPointer(in IID_IDxcValidator);

    public void** lpVtbl;

    [return: NativeTypeName("HRESULT")]
    public int QueryInterface([NativeTypeName("const IID &")] Guid* riid, void** ppvObject)
    {
        return ((delegate* unmanaged[Stdcall]<IDxcValidator*, Guid*, void**, int>)(lpVtbl[0]))((IDxcValidator*)Unsafe.AsPointer(ref this), riid, ppvObject);
    }

    [return: NativeTypeName("ULONG")]
    public uint AddRef()
    {
        return ((delegate* unmanaged[Stdcall]<IDxcValidator*, uint>)(lpVtbl[1]))((IDxcValidator*)Unsafe.AsPointer(ref this));
    }

    [return: NativeTypeName("ULONG")]
    public uint Release()
    {
        return ((delegate* unmanaged[Stdcall]<IDxcValidator*, uint>)(lpVtbl[2]))((IDxcValidator*)Unsafe.AsPointer(ref this));
    }

    [return: NativeTypeName("HRESULT")]
    public int Validate(IDxcBlob* pShader, [NativeTypeName("UINT32")] uint Flags, IDxcOperationResult** ppResult)
    {
        return ((delegate* unmanaged[Stdcall]<IDxcValidator*, IDxcBlob*, uint, IDxcOperationResult**, int>)(lpVtbl[3]))((IDxcValidator*)Unsafe.AsPointer(ref this), pShader, Flags, ppResult);
    }

    public interface Interface : IUnknown.Interface
    {
        [return: NativeTypeName("HRESULT")]
        int Validate(IDxcBlob* pShader, [NativeTypeName("UINT32")] uint Flags, IDxcOperationResult** ppResult);
    }
}

[Guid("458E1FD1-B1B2-4750-A6E1-9C10F03BED92")]
[NativeTypeName("struct IDxcValidator2 : IDxcValidator")]
internal unsafe partial struct IDxcValidator2 : IDxcValidator2.Interface, INativeGuid
{
    static Guid* INativeGuid.NativeGuid => (Guid*)Unsafe.AsPointer(in IID_IDxcValidator2);

    public void** lpVtbl;

    [return: NativeTypeName("HRESULT")]
    public int QueryInterface([NativeTypeName("const IID &")] Guid* riid, void** ppvObject)
    {
        return ((delegate* unmanaged[Stdcall]<IDxcValidator2*, Guid*, void**, int>)(lpVtbl[0]))((IDxcValidator2*)Unsafe.AsPointer(ref this), riid, ppvObject);
    }

    [return: NativeTypeName("ULONG")]
    public uint AddRef()
    {
        return ((delegate* unmanaged[Stdcall]<IDxcValidator2*, uint>)(lpVtbl[1]))((IDxcValidator2*)Unsafe.AsPointer(ref this));
    }

    [return: NativeTypeName("ULONG")]
    public uint Release()
    {
        return ((delegate* unmanaged[Stdcall]<IDxcValidator2*, uint>)(lpVtbl[2]))((IDxcValidator2*)Unsafe.AsPointer(ref this));
    }

    [return: NativeTypeName("HRESULT")]
    public int Validate(IDxcBlob* pShader, [NativeTypeName("UINT32")] uint Flags, IDxcOperationResult** ppResult)
    {
        return ((delegate* unmanaged[Stdcall]<IDxcValidator2*, IDxcBlob*, uint, IDxcOperationResult**, int>)(lpVtbl[3]))((IDxcValidator2*)Unsafe.AsPointer(ref this), pShader, Flags, ppResult);
    }

    [return: NativeTypeName("HRESULT")]
    public int ValidateWithDebug(IDxcBlob* pShader, [NativeTypeName("UINT32")] uint Flags, DxcBuffer* pOptDebugBitcode, IDxcOperationResult** ppResult)
    {
        return ((delegate* unmanaged[Stdcall]<IDxcValidator2*, IDxcBlob*, uint, DxcBuffer*, IDxcOperationResult**, int>)(lpVtbl[4]))((IDxcValidator2*)Unsafe.AsPointer(ref this), pShader, Flags, pOptDebugBitcode, ppResult);
    }

    public interface Interface : IDxcValidator.Interface
    {
        [return: NativeTypeName("HRESULT")]
        int ValidateWithDebug(IDxcBlob* pShader, [NativeTypeName("UINT32")] uint Flags, DxcBuffer* pOptDebugBitcode, IDxcOperationResult** ppResult);
    }
}

[Guid("334B1F50-2292-4B35-99A1-25588D8C17FE")]
[NativeTypeName("struct IDxcContainerBuilder : IUnknown")]
internal unsafe partial struct IDxcContainerBuilder : IDxcContainerBuilder.Interface, INativeGuid
{
    static Guid* INativeGuid.NativeGuid => (Guid*)Unsafe.AsPointer(in IID_IDxcContainerBuilder);

    public void** lpVtbl;

    [return: NativeTypeName("HRESULT")]
    public int QueryInterface([NativeTypeName("const IID &")] Guid* riid, void** ppvObject)
    {
        return ((delegate* unmanaged[Stdcall]<IDxcContainerBuilder*, Guid*, void**, int>)(lpVtbl[0]))((IDxcContainerBuilder*)Unsafe.AsPointer(ref this), riid, ppvObject);
    }

    [return: NativeTypeName("ULONG")]
    public uint AddRef()
    {
        return ((delegate* unmanaged[Stdcall]<IDxcContainerBuilder*, uint>)(lpVtbl[1]))((IDxcContainerBuilder*)Unsafe.AsPointer(ref this));
    }

    [return: NativeTypeName("ULONG")]
    public uint Release()
    {
        return ((delegate* unmanaged[Stdcall]<IDxcContainerBuilder*, uint>)(lpVtbl[2]))((IDxcContainerBuilder*)Unsafe.AsPointer(ref this));
    }

    [return: NativeTypeName("HRESULT")]
    public int Load(IDxcBlob* pDxilContainerHeader)
    {
        return ((delegate* unmanaged[Stdcall]<IDxcContainerBuilder*, IDxcBlob*, int>)(lpVtbl[3]))((IDxcContainerBuilder*)Unsafe.AsPointer(ref this), pDxilContainerHeader);
    }

    [return: NativeTypeName("HRESULT")]
    public int AddPart([NativeTypeName("UINT32")] uint fourCC, IDxcBlob* pSource)
    {
        return ((delegate* unmanaged[Stdcall]<IDxcContainerBuilder*, uint, IDxcBlob*, int>)(lpVtbl[4]))((IDxcContainerBuilder*)Unsafe.AsPointer(ref this), fourCC, pSource);
    }

    [return: NativeTypeName("HRESULT")]
    public int RemovePart([NativeTypeName("UINT32")] uint fourCC)
    {
        return ((delegate* unmanaged[Stdcall]<IDxcContainerBuilder*, uint, int>)(lpVtbl[5]))((IDxcContainerBuilder*)Unsafe.AsPointer(ref this), fourCC);
    }

    [return: NativeTypeName("HRESULT")]
    public int SerializeContainer(IDxcOperationResult** ppResult)
    {
        return ((delegate* unmanaged[Stdcall]<IDxcContainerBuilder*, IDxcOperationResult**, int>)(lpVtbl[6]))((IDxcContainerBuilder*)Unsafe.AsPointer(ref this), ppResult);
    }

    public interface Interface : IUnknown.Interface
    {
        [return: NativeTypeName("HRESULT")]
        int Load(IDxcBlob* pDxilContainerHeader);

        [return: NativeTypeName("HRESULT")]
        int AddPart([NativeTypeName("UINT32")] uint fourCC, IDxcBlob* pSource);

        [return: NativeTypeName("HRESULT")]
        int RemovePart([NativeTypeName("UINT32")] uint fourCC);

        [return: NativeTypeName("HRESULT")]
        int SerializeContainer(IDxcOperationResult** ppResult);
    }
}

[Guid("091F7A26-1C1F-4948-904B-E6E3A8A771D5")]
[NativeTypeName("struct IDxcAssembler : IUnknown")]
internal unsafe partial struct IDxcAssembler : IDxcAssembler.Interface, INativeGuid
{
    static Guid* INativeGuid.NativeGuid => (Guid*)Unsafe.AsPointer(in IID_IDxcAssembler);

    public void** lpVtbl;

    [return: NativeTypeName("HRESULT")]
    public int QueryInterface([NativeTypeName("const IID &")] Guid* riid, void** ppvObject)
    {
        return ((delegate* unmanaged[Stdcall]<IDxcAssembler*, Guid*, void**, int>)(lpVtbl[0]))((IDxcAssembler*)Unsafe.AsPointer(ref this), riid, ppvObject);
    }

    [return: NativeTypeName("ULONG")]
    public uint AddRef()
    {
        return ((delegate* unmanaged[Stdcall]<IDxcAssembler*, uint>)(lpVtbl[1]))((IDxcAssembler*)Unsafe.AsPointer(ref this));
    }

    [return: NativeTypeName("ULONG")]
    public uint Release()
    {
        return ((delegate* unmanaged[Stdcall]<IDxcAssembler*, uint>)(lpVtbl[2]))((IDxcAssembler*)Unsafe.AsPointer(ref this));
    }

    [return: NativeTypeName("HRESULT")]
    public int AssembleToContainer(IDxcBlob* pShader, IDxcOperationResult** ppResult)
    {
        return ((delegate* unmanaged[Stdcall]<IDxcAssembler*, IDxcBlob*, IDxcOperationResult**, int>)(lpVtbl[3]))((IDxcAssembler*)Unsafe.AsPointer(ref this), pShader, ppResult);
    }

    public interface Interface : IUnknown.Interface
    {
        [return: NativeTypeName("HRESULT")]
        int AssembleToContainer(IDxcBlob* pShader, IDxcOperationResult** ppResult);
    }
}

[Guid("D2C21B26-8350-4BDC-976A-331CE6F4C54C")]
[NativeTypeName("struct IDxcContainerReflection : IUnknown")]
internal unsafe partial struct IDxcContainerReflection : IDxcContainerReflection.Interface, INativeGuid
{
    static Guid* INativeGuid.NativeGuid => (Guid*)Unsafe.AsPointer(in IID_IDxcContainerReflection);

    public void** lpVtbl;

    [return: NativeTypeName("HRESULT")]
    public int QueryInterface([NativeTypeName("const IID &")] Guid* riid, void** ppvObject)
    {
        return ((delegate* unmanaged[Stdcall]<IDxcContainerReflection*, Guid*, void**, int>)(lpVtbl[0]))((IDxcContainerReflection*)Unsafe.AsPointer(ref this), riid, ppvObject);
    }

    [return: NativeTypeName("ULONG")]
    public uint AddRef()
    {
        return ((delegate* unmanaged[Stdcall]<IDxcContainerReflection*, uint>)(lpVtbl[1]))((IDxcContainerReflection*)Unsafe.AsPointer(ref this));
    }

    [return: NativeTypeName("ULONG")]
    public uint Release()
    {
        return ((delegate* unmanaged[Stdcall]<IDxcContainerReflection*, uint>)(lpVtbl[2]))((IDxcContainerReflection*)Unsafe.AsPointer(ref this));
    }

    [return: NativeTypeName("HRESULT")]
    public int Load(IDxcBlob* pContainer)
    {
        return ((delegate* unmanaged[Stdcall]<IDxcContainerReflection*, IDxcBlob*, int>)(lpVtbl[3]))((IDxcContainerReflection*)Unsafe.AsPointer(ref this), pContainer);
    }

    [return: NativeTypeName("HRESULT")]
    public int GetPartCount([NativeTypeName("UINT32 *")] uint* pResult)
    {
        return ((delegate* unmanaged[Stdcall]<IDxcContainerReflection*, uint*, int>)(lpVtbl[4]))((IDxcContainerReflection*)Unsafe.AsPointer(ref this), pResult);
    }

    [return: NativeTypeName("HRESULT")]
    public int GetPartKind([NativeTypeName("UINT32")] uint idx, [NativeTypeName("UINT32 *")] uint* pResult)
    {
        return ((delegate* unmanaged[Stdcall]<IDxcContainerReflection*, uint, uint*, int>)(lpVtbl[5]))((IDxcContainerReflection*)Unsafe.AsPointer(ref this), idx, pResult);
    }

    [return: NativeTypeName("HRESULT")]
    public int GetPartContent([NativeTypeName("UINT32")] uint idx, IDxcBlob** ppResult)
    {
        return ((delegate* unmanaged[Stdcall]<IDxcContainerReflection*, uint, IDxcBlob**, int>)(lpVtbl[6]))((IDxcContainerReflection*)Unsafe.AsPointer(ref this), idx, ppResult);
    }

    [return: NativeTypeName("HRESULT")]
    public int FindFirstPartKind([NativeTypeName("UINT32")] uint kind, [NativeTypeName("UINT32 *")] uint* pResult)
    {
        return ((delegate* unmanaged[Stdcall]<IDxcContainerReflection*, uint, uint*, int>)(lpVtbl[7]))((IDxcContainerReflection*)Unsafe.AsPointer(ref this), kind, pResult);
    }

    [return: NativeTypeName("HRESULT")]
    public int GetPartReflection([NativeTypeName("UINT32")] uint idx, [NativeTypeName("const IID &")] Guid* iid, void** ppvObject)
    {
        return ((delegate* unmanaged[Stdcall]<IDxcContainerReflection*, uint, Guid*, void**, int>)(lpVtbl[8]))((IDxcContainerReflection*)Unsafe.AsPointer(ref this), idx, iid, ppvObject);
    }

    public interface Interface : IUnknown.Interface
    {
        [return: NativeTypeName("HRESULT")]
        int Load(IDxcBlob* pContainer);

        [return: NativeTypeName("HRESULT")]
        int GetPartCount([NativeTypeName("UINT32 *")] uint* pResult);

        [return: NativeTypeName("HRESULT")]
        int GetPartKind([NativeTypeName("UINT32")] uint idx, [NativeTypeName("UINT32 *")] uint* pResult);

        [return: NativeTypeName("HRESULT")]
        int GetPartContent([NativeTypeName("UINT32")] uint idx, IDxcBlob** ppResult);

        [return: NativeTypeName("HRESULT")]
        int FindFirstPartKind([NativeTypeName("UINT32")] uint kind, [NativeTypeName("UINT32 *")] uint* pResult);

        [return: NativeTypeName("HRESULT")]
        int GetPartReflection([NativeTypeName("UINT32")] uint idx, [NativeTypeName("const IID &")] Guid* iid, void** ppvObject);
    }
}

[Guid("AE2CD79F-CC22-453F-9B6B-B124E7A5204C")]
[NativeTypeName("struct IDxcOptimizerPass : IUnknown")]
internal unsafe partial struct IDxcOptimizerPass : IDxcOptimizerPass.Interface, INativeGuid
{
    static Guid* INativeGuid.NativeGuid => (Guid*)Unsafe.AsPointer(in IID_IDxcOptimizerPass);

    public void** lpVtbl;

    [return: NativeTypeName("HRESULT")]
    public int QueryInterface([NativeTypeName("const IID &")] Guid* riid, void** ppvObject)
    {
        return ((delegate* unmanaged[Stdcall]<IDxcOptimizerPass*, Guid*, void**, int>)(lpVtbl[0]))((IDxcOptimizerPass*)Unsafe.AsPointer(ref this), riid, ppvObject);
    }

    [return: NativeTypeName("ULONG")]
    public uint AddRef()
    {
        return ((delegate* unmanaged[Stdcall]<IDxcOptimizerPass*, uint>)(lpVtbl[1]))((IDxcOptimizerPass*)Unsafe.AsPointer(ref this));
    }

    [return: NativeTypeName("ULONG")]
    public uint Release()
    {
        return ((delegate* unmanaged[Stdcall]<IDxcOptimizerPass*, uint>)(lpVtbl[2]))((IDxcOptimizerPass*)Unsafe.AsPointer(ref this));
    }

    [return: NativeTypeName("HRESULT")]
    public int GetOptionName([NativeTypeName("LPWSTR *")] char** ppResult)
    {
        return ((delegate* unmanaged[Stdcall]<IDxcOptimizerPass*, char**, int>)(lpVtbl[3]))((IDxcOptimizerPass*)Unsafe.AsPointer(ref this), ppResult);
    }

    [return: NativeTypeName("HRESULT")]
    public int GetDescription([NativeTypeName("LPWSTR *")] char** ppResult)
    {
        return ((delegate* unmanaged[Stdcall]<IDxcOptimizerPass*, char**, int>)(lpVtbl[4]))((IDxcOptimizerPass*)Unsafe.AsPointer(ref this), ppResult);
    }

    [return: NativeTypeName("HRESULT")]
    public int GetOptionArgCount([NativeTypeName("UINT32 *")] uint* pCount)
    {
        return ((delegate* unmanaged[Stdcall]<IDxcOptimizerPass*, uint*, int>)(lpVtbl[5]))((IDxcOptimizerPass*)Unsafe.AsPointer(ref this), pCount);
    }

    [return: NativeTypeName("HRESULT")]
    public int GetOptionArgName([NativeTypeName("UINT32")] uint argIndex, [NativeTypeName("LPWSTR *")] char** ppResult)
    {
        return ((delegate* unmanaged[Stdcall]<IDxcOptimizerPass*, uint, char**, int>)(lpVtbl[6]))((IDxcOptimizerPass*)Unsafe.AsPointer(ref this), argIndex, ppResult);
    }

    [return: NativeTypeName("HRESULT")]
    public int GetOptionArgDescription([NativeTypeName("UINT32")] uint argIndex, [NativeTypeName("LPWSTR *")] char** ppResult)
    {
        return ((delegate* unmanaged[Stdcall]<IDxcOptimizerPass*, uint, char**, int>)(lpVtbl[7]))((IDxcOptimizerPass*)Unsafe.AsPointer(ref this), argIndex, ppResult);
    }

    public interface Interface : IUnknown.Interface
    {
        [return: NativeTypeName("HRESULT")]
        int GetOptionName([NativeTypeName("LPWSTR *")] char** ppResult);

        [return: NativeTypeName("HRESULT")]
        int GetDescription([NativeTypeName("LPWSTR *")] char** ppResult);

        [return: NativeTypeName("HRESULT")]
        int GetOptionArgCount([NativeTypeName("UINT32 *")] uint* pCount);

        [return: NativeTypeName("HRESULT")]
        int GetOptionArgName([NativeTypeName("UINT32")] uint argIndex, [NativeTypeName("LPWSTR *")] char** ppResult);

        [return: NativeTypeName("HRESULT")]
        int GetOptionArgDescription([NativeTypeName("UINT32")] uint argIndex, [NativeTypeName("LPWSTR *")] char** ppResult);
    }
}

[Guid("25740E2E-9CBA-401B-9119-4FB42F39F270")]
[NativeTypeName("struct IDxcOptimizer : IUnknown")]
internal unsafe partial struct IDxcOptimizer : IDxcOptimizer.Interface, INativeGuid
{
    static Guid* INativeGuid.NativeGuid => (Guid*)Unsafe.AsPointer(in IID_IDxcOptimizer);

    public void** lpVtbl;

    [return: NativeTypeName("HRESULT")]
    public int QueryInterface([NativeTypeName("const IID &")] Guid* riid, void** ppvObject)
    {
        return ((delegate* unmanaged[Stdcall]<IDxcOptimizer*, Guid*, void**, int>)(lpVtbl[0]))((IDxcOptimizer*)Unsafe.AsPointer(ref this), riid, ppvObject);
    }

    [return: NativeTypeName("ULONG")]
    public uint AddRef()
    {
        return ((delegate* unmanaged[Stdcall]<IDxcOptimizer*, uint>)(lpVtbl[1]))((IDxcOptimizer*)Unsafe.AsPointer(ref this));
    }

    [return: NativeTypeName("ULONG")]
    public uint Release()
    {
        return ((delegate* unmanaged[Stdcall]<IDxcOptimizer*, uint>)(lpVtbl[2]))((IDxcOptimizer*)Unsafe.AsPointer(ref this));
    }

    [return: NativeTypeName("HRESULT")]
    public int GetAvailablePassCount([NativeTypeName("UINT32 *")] uint* pCount)
    {
        return ((delegate* unmanaged[Stdcall]<IDxcOptimizer*, uint*, int>)(lpVtbl[3]))((IDxcOptimizer*)Unsafe.AsPointer(ref this), pCount);
    }

    [return: NativeTypeName("HRESULT")]
    public int GetAvailablePass([NativeTypeName("UINT32")] uint index, IDxcOptimizerPass** ppResult)
    {
        return ((delegate* unmanaged[Stdcall]<IDxcOptimizer*, uint, IDxcOptimizerPass**, int>)(lpVtbl[4]))((IDxcOptimizer*)Unsafe.AsPointer(ref this), index, ppResult);
    }

    [return: NativeTypeName("HRESULT")]
    public int RunOptimizer(IDxcBlob* pBlob, [NativeTypeName("LPCWSTR *")] char** ppOptions, [NativeTypeName("UINT32")] uint optionCount, IDxcBlob** pOutputModule, IDxcBlobEncoding** ppOutputText)
    {
        return ((delegate* unmanaged[Stdcall]<IDxcOptimizer*, IDxcBlob*, char**, uint, IDxcBlob**, IDxcBlobEncoding**, int>)(lpVtbl[5]))((IDxcOptimizer*)Unsafe.AsPointer(ref this), pBlob, ppOptions, optionCount, pOutputModule, ppOutputText);
    }

    public interface Interface : IUnknown.Interface
    {
        [return: NativeTypeName("HRESULT")]
        int GetAvailablePassCount([NativeTypeName("UINT32 *")] uint* pCount);

        [return: NativeTypeName("HRESULT")]
        int GetAvailablePass([NativeTypeName("UINT32")] uint index, IDxcOptimizerPass** ppResult);

        [return: NativeTypeName("HRESULT")]
        int RunOptimizer(IDxcBlob* pBlob, [NativeTypeName("LPCWSTR *")] char** ppOptions, [NativeTypeName("UINT32")] uint optionCount, IDxcBlob** pOutputModule, IDxcBlobEncoding** ppOutputText);
    }
}

[Guid("B04F5B50-2059-4F12-A8FF-A1E0CDE1CC7E")]
[NativeTypeName("struct IDxcVersionInfo : IUnknown")]
internal unsafe partial struct IDxcVersionInfo : IDxcVersionInfo.Interface, INativeGuid
{
    static Guid* INativeGuid.NativeGuid => (Guid*)Unsafe.AsPointer(in IID_IDxcVersionInfo);

    public void** lpVtbl;

    [return: NativeTypeName("HRESULT")]
    public int QueryInterface([NativeTypeName("const IID &")] Guid* riid, void** ppvObject)
    {
        return ((delegate* unmanaged[Stdcall]<IDxcVersionInfo*, Guid*, void**, int>)(lpVtbl[0]))((IDxcVersionInfo*)Unsafe.AsPointer(ref this), riid, ppvObject);
    }

    [return: NativeTypeName("ULONG")]
    public uint AddRef()
    {
        return ((delegate* unmanaged[Stdcall]<IDxcVersionInfo*, uint>)(lpVtbl[1]))((IDxcVersionInfo*)Unsafe.AsPointer(ref this));
    }

    [return: NativeTypeName("ULONG")]
    public uint Release()
    {
        return ((delegate* unmanaged[Stdcall]<IDxcVersionInfo*, uint>)(lpVtbl[2]))((IDxcVersionInfo*)Unsafe.AsPointer(ref this));
    }

    [return: NativeTypeName("HRESULT")]
    public int GetVersion([NativeTypeName("UINT32 *")] uint* pMajor, [NativeTypeName("UINT32 *")] uint* pMinor)
    {
        return ((delegate* unmanaged[Stdcall]<IDxcVersionInfo*, uint*, uint*, int>)(lpVtbl[3]))((IDxcVersionInfo*)Unsafe.AsPointer(ref this), pMajor, pMinor);
    }

    [return: NativeTypeName("HRESULT")]
    public int GetFlags([NativeTypeName("UINT32 *")] uint* pFlags)
    {
        return ((delegate* unmanaged[Stdcall]<IDxcVersionInfo*, uint*, int>)(lpVtbl[4]))((IDxcVersionInfo*)Unsafe.AsPointer(ref this), pFlags);
    }

    public interface Interface : IUnknown.Interface
    {
        [return: NativeTypeName("HRESULT")]
        int GetVersion([NativeTypeName("UINT32 *")] uint* pMajor, [NativeTypeName("UINT32 *")] uint* pMinor);

        [return: NativeTypeName("HRESULT")]
        int GetFlags([NativeTypeName("UINT32 *")] uint* pFlags);
    }
}

[Guid("FB6904C4-42F0-4B62-9C46-983AF7DA7C83")]
[NativeTypeName("struct IDxcVersionInfo2 : IDxcVersionInfo")]
internal unsafe partial struct IDxcVersionInfo2 : IDxcVersionInfo2.Interface, INativeGuid
{
    static Guid* INativeGuid.NativeGuid => (Guid*)Unsafe.AsPointer(in IID_IDxcVersionInfo2);

    public void** lpVtbl;

    [return: NativeTypeName("HRESULT")]
    public int QueryInterface([NativeTypeName("const IID &")] Guid* riid, void** ppvObject)
    {
        return ((delegate* unmanaged[Stdcall]<IDxcVersionInfo2*, Guid*, void**, int>)(lpVtbl[0]))((IDxcVersionInfo2*)Unsafe.AsPointer(ref this), riid, ppvObject);
    }

    [return: NativeTypeName("ULONG")]
    public uint AddRef()
    {
        return ((delegate* unmanaged[Stdcall]<IDxcVersionInfo2*, uint>)(lpVtbl[1]))((IDxcVersionInfo2*)Unsafe.AsPointer(ref this));
    }

    [return: NativeTypeName("ULONG")]
    public uint Release()
    {
        return ((delegate* unmanaged[Stdcall]<IDxcVersionInfo2*, uint>)(lpVtbl[2]))((IDxcVersionInfo2*)Unsafe.AsPointer(ref this));
    }

    [return: NativeTypeName("HRESULT")]
    public int GetVersion([NativeTypeName("UINT32 *")] uint* pMajor, [NativeTypeName("UINT32 *")] uint* pMinor)
    {
        return ((delegate* unmanaged[Stdcall]<IDxcVersionInfo2*, uint*, uint*, int>)(lpVtbl[3]))((IDxcVersionInfo2*)Unsafe.AsPointer(ref this), pMajor, pMinor);
    }

    [return: NativeTypeName("HRESULT")]
    public int GetFlags([NativeTypeName("UINT32 *")] uint* pFlags)
    {
        return ((delegate* unmanaged[Stdcall]<IDxcVersionInfo2*, uint*, int>)(lpVtbl[4]))((IDxcVersionInfo2*)Unsafe.AsPointer(ref this), pFlags);
    }

    [return: NativeTypeName("HRESULT")]
    public int GetCommitInfo([NativeTypeName("UINT32 *")] uint* pCommitCount, [NativeTypeName("char **")] sbyte** pCommitHash)
    {
        return ((delegate* unmanaged[Stdcall]<IDxcVersionInfo2*, uint*, sbyte**, int>)(lpVtbl[5]))((IDxcVersionInfo2*)Unsafe.AsPointer(ref this), pCommitCount, pCommitHash);
    }

    public interface Interface : IDxcVersionInfo.Interface
    {
        [return: NativeTypeName("HRESULT")]
        int GetCommitInfo([NativeTypeName("UINT32 *")] uint* pCommitCount, [NativeTypeName("char **")] sbyte** pCommitHash);
    }
}

[Guid("5E13E843-9D25-473C-9AD2-03B2D0B44B1E")]
[NativeTypeName("struct IDxcVersionInfo3 : IUnknown")]
internal unsafe partial struct IDxcVersionInfo3 : IDxcVersionInfo3.Interface, INativeGuid
{
    static Guid* INativeGuid.NativeGuid => (Guid*)Unsafe.AsPointer(in IID_IDxcVersionInfo3);

    public void** lpVtbl;

    [return: NativeTypeName("HRESULT")]
    public int QueryInterface([NativeTypeName("const IID &")] Guid* riid, void** ppvObject)
    {
        return ((delegate* unmanaged[Stdcall]<IDxcVersionInfo3*, Guid*, void**, int>)(lpVtbl[0]))((IDxcVersionInfo3*)Unsafe.AsPointer(ref this), riid, ppvObject);
    }

    [return: NativeTypeName("ULONG")]
    public uint AddRef()
    {
        return ((delegate* unmanaged[Stdcall]<IDxcVersionInfo3*, uint>)(lpVtbl[1]))((IDxcVersionInfo3*)Unsafe.AsPointer(ref this));
    }

    [return: NativeTypeName("ULONG")]
    public uint Release()
    {
        return ((delegate* unmanaged[Stdcall]<IDxcVersionInfo3*, uint>)(lpVtbl[2]))((IDxcVersionInfo3*)Unsafe.AsPointer(ref this));
    }

    [return: NativeTypeName("HRESULT")]
    public int GetCustomVersionString([NativeTypeName("char **")] sbyte** pVersionString)
    {
        return ((delegate* unmanaged[Stdcall]<IDxcVersionInfo3*, sbyte**, int>)(lpVtbl[3]))((IDxcVersionInfo3*)Unsafe.AsPointer(ref this), pVersionString);
    }

    public interface Interface : IUnknown.Interface
    {
        [return: NativeTypeName("HRESULT")]
        int GetCustomVersionString([NativeTypeName("char **")] sbyte** pVersionString);
    }
}

internal unsafe partial struct DxcArgPair
{
    [NativeTypeName("const WCHAR *")]
    public char* pName;

    [NativeTypeName("const WCHAR *")]
    public char* pValue;
}

[Guid("E6C9647E-9D6A-4C3B-B94C-524B5A6C343D")]
[NativeTypeName("struct IDxcPdbUtils : IUnknown")]
internal unsafe partial struct IDxcPdbUtils : IDxcPdbUtils.Interface, INativeGuid
{
    static Guid* INativeGuid.NativeGuid => (Guid*)Unsafe.AsPointer(in IID_IDxcPdbUtils);

    public void** lpVtbl;

    [return: NativeTypeName("HRESULT")]
    public int QueryInterface([NativeTypeName("const IID &")] Guid* riid, void** ppvObject)
    {
        return ((delegate* unmanaged[Stdcall]<IDxcPdbUtils*, Guid*, void**, int>)(lpVtbl[0]))((IDxcPdbUtils*)Unsafe.AsPointer(ref this), riid, ppvObject);
    }

    [return: NativeTypeName("ULONG")]
    public uint AddRef()
    {
        return ((delegate* unmanaged[Stdcall]<IDxcPdbUtils*, uint>)(lpVtbl[1]))((IDxcPdbUtils*)Unsafe.AsPointer(ref this));
    }

    [return: NativeTypeName("ULONG")]
    public uint Release()
    {
        return ((delegate* unmanaged[Stdcall]<IDxcPdbUtils*, uint>)(lpVtbl[2]))((IDxcPdbUtils*)Unsafe.AsPointer(ref this));
    }

    [return: NativeTypeName("HRESULT")]
    public int Load(IDxcBlob* pPdbOrDxil)
    {
        return ((delegate* unmanaged[Stdcall]<IDxcPdbUtils*, IDxcBlob*, int>)(lpVtbl[3]))((IDxcPdbUtils*)Unsafe.AsPointer(ref this), pPdbOrDxil);
    }

    [return: NativeTypeName("HRESULT")]
    public int GetSourceCount([NativeTypeName("UINT32 *")] uint* pCount)
    {
        return ((delegate* unmanaged[Stdcall]<IDxcPdbUtils*, uint*, int>)(lpVtbl[4]))((IDxcPdbUtils*)Unsafe.AsPointer(ref this), pCount);
    }

    [return: NativeTypeName("HRESULT")]
    public int GetSource([NativeTypeName("UINT32")] uint uIndex, IDxcBlobEncoding** ppResult)
    {
        return ((delegate* unmanaged[Stdcall]<IDxcPdbUtils*, uint, IDxcBlobEncoding**, int>)(lpVtbl[5]))((IDxcPdbUtils*)Unsafe.AsPointer(ref this), uIndex, ppResult);
    }

    [return: NativeTypeName("HRESULT")]
    public int GetSourceName([NativeTypeName("UINT32")] uint uIndex, [NativeTypeName("BSTR *")] char** pResult)
    {
        return ((delegate* unmanaged[Stdcall]<IDxcPdbUtils*, uint, char**, int>)(lpVtbl[6]))((IDxcPdbUtils*)Unsafe.AsPointer(ref this), uIndex, pResult);
    }

    [return: NativeTypeName("HRESULT")]
    public int GetFlagCount([NativeTypeName("UINT32 *")] uint* pCount)
    {
        return ((delegate* unmanaged[Stdcall]<IDxcPdbUtils*, uint*, int>)(lpVtbl[7]))((IDxcPdbUtils*)Unsafe.AsPointer(ref this), pCount);
    }

    [return: NativeTypeName("HRESULT")]
    public int GetFlag([NativeTypeName("UINT32")] uint uIndex, [NativeTypeName("BSTR *")] char** pResult)
    {
        return ((delegate* unmanaged[Stdcall]<IDxcPdbUtils*, uint, char**, int>)(lpVtbl[8]))((IDxcPdbUtils*)Unsafe.AsPointer(ref this), uIndex, pResult);
    }

    [return: NativeTypeName("HRESULT")]
    public int GetArgCount([NativeTypeName("UINT32 *")] uint* pCount)
    {
        return ((delegate* unmanaged[Stdcall]<IDxcPdbUtils*, uint*, int>)(lpVtbl[9]))((IDxcPdbUtils*)Unsafe.AsPointer(ref this), pCount);
    }

    [return: NativeTypeName("HRESULT")]
    public int GetArg([NativeTypeName("UINT32")] uint uIndex, [NativeTypeName("BSTR *")] char** pResult)
    {
        return ((delegate* unmanaged[Stdcall]<IDxcPdbUtils*, uint, char**, int>)(lpVtbl[10]))((IDxcPdbUtils*)Unsafe.AsPointer(ref this), uIndex, pResult);
    }

    [return: NativeTypeName("HRESULT")]
    public int GetArgPairCount([NativeTypeName("UINT32 *")] uint* pCount)
    {
        return ((delegate* unmanaged[Stdcall]<IDxcPdbUtils*, uint*, int>)(lpVtbl[11]))((IDxcPdbUtils*)Unsafe.AsPointer(ref this), pCount);
    }

    [return: NativeTypeName("HRESULT")]
    public int GetArgPair([NativeTypeName("UINT32")] uint uIndex, [NativeTypeName("BSTR *")] char** pName, [NativeTypeName("BSTR *")] char** pValue)
    {
        return ((delegate* unmanaged[Stdcall]<IDxcPdbUtils*, uint, char**, char**, int>)(lpVtbl[12]))((IDxcPdbUtils*)Unsafe.AsPointer(ref this), uIndex, pName, pValue);
    }

    [return: NativeTypeName("HRESULT")]
    public int GetDefineCount([NativeTypeName("UINT32 *")] uint* pCount)
    {
        return ((delegate* unmanaged[Stdcall]<IDxcPdbUtils*, uint*, int>)(lpVtbl[13]))((IDxcPdbUtils*)Unsafe.AsPointer(ref this), pCount);
    }

    [return: NativeTypeName("HRESULT")]
    public int GetDefine([NativeTypeName("UINT32")] uint uIndex, [NativeTypeName("BSTR *")] char** pResult)
    {
        return ((delegate* unmanaged[Stdcall]<IDxcPdbUtils*, uint, char**, int>)(lpVtbl[14]))((IDxcPdbUtils*)Unsafe.AsPointer(ref this), uIndex, pResult);
    }

    [return: NativeTypeName("HRESULT")]
    public int GetTargetProfile([NativeTypeName("BSTR *")] char** pResult)
    {
        return ((delegate* unmanaged[Stdcall]<IDxcPdbUtils*, char**, int>)(lpVtbl[15]))((IDxcPdbUtils*)Unsafe.AsPointer(ref this), pResult);
    }

    [return: NativeTypeName("HRESULT")]
    public int GetEntryPoint([NativeTypeName("BSTR *")] char** pResult)
    {
        return ((delegate* unmanaged[Stdcall]<IDxcPdbUtils*, char**, int>)(lpVtbl[16]))((IDxcPdbUtils*)Unsafe.AsPointer(ref this), pResult);
    }

    [return: NativeTypeName("HRESULT")]
    public int GetMainFileName([NativeTypeName("BSTR *")] char** pResult)
    {
        return ((delegate* unmanaged[Stdcall]<IDxcPdbUtils*, char**, int>)(lpVtbl[17]))((IDxcPdbUtils*)Unsafe.AsPointer(ref this), pResult);
    }

    [return: NativeTypeName("HRESULT")]
    public int GetHash(IDxcBlob** ppResult)
    {
        return ((delegate* unmanaged[Stdcall]<IDxcPdbUtils*, IDxcBlob**, int>)(lpVtbl[18]))((IDxcPdbUtils*)Unsafe.AsPointer(ref this), ppResult);
    }

    [return: NativeTypeName("HRESULT")]
    public int GetName([NativeTypeName("BSTR *")] char** pResult)
    {
        return ((delegate* unmanaged[Stdcall]<IDxcPdbUtils*, char**, int>)(lpVtbl[19]))((IDxcPdbUtils*)Unsafe.AsPointer(ref this), pResult);
    }

    [return: NativeTypeName("BOOL")]
    public int IsFullPDB()
    {
        return ((delegate* unmanaged[Stdcall]<IDxcPdbUtils*, int>)(lpVtbl[20]))((IDxcPdbUtils*)Unsafe.AsPointer(ref this));
    }

    [return: NativeTypeName("HRESULT")]
    public int GetFullPDB(IDxcBlob** ppFullPDB)
    {
        return ((delegate* unmanaged[Stdcall]<IDxcPdbUtils*, IDxcBlob**, int>)(lpVtbl[21]))((IDxcPdbUtils*)Unsafe.AsPointer(ref this), ppFullPDB);
    }

    [return: NativeTypeName("HRESULT")]
    public int GetVersionInfo(IDxcVersionInfo** ppVersionInfo)
    {
        return ((delegate* unmanaged[Stdcall]<IDxcPdbUtils*, IDxcVersionInfo**, int>)(lpVtbl[22]))((IDxcPdbUtils*)Unsafe.AsPointer(ref this), ppVersionInfo);
    }

    [return: NativeTypeName("HRESULT")]
    public int SetCompiler(IDxcCompiler3* pCompiler)
    {
        return ((delegate* unmanaged[Stdcall]<IDxcPdbUtils*, IDxcCompiler3*, int>)(lpVtbl[23]))((IDxcPdbUtils*)Unsafe.AsPointer(ref this), pCompiler);
    }

    [return: NativeTypeName("HRESULT")]
    public int CompileForFullPDB(IDxcResult** ppResult)
    {
        return ((delegate* unmanaged[Stdcall]<IDxcPdbUtils*, IDxcResult**, int>)(lpVtbl[24]))((IDxcPdbUtils*)Unsafe.AsPointer(ref this), ppResult);
    }

    [return: NativeTypeName("HRESULT")]
    public int OverrideArgs(DxcArgPair* pArgPairs, [NativeTypeName("UINT32")] uint uNumArgPairs)
    {
        return ((delegate* unmanaged[Stdcall]<IDxcPdbUtils*, DxcArgPair*, uint, int>)(lpVtbl[25]))((IDxcPdbUtils*)Unsafe.AsPointer(ref this), pArgPairs, uNumArgPairs);
    }

    [return: NativeTypeName("HRESULT")]
    public int OverrideRootSignature([NativeTypeName("const WCHAR *")] char* pRootSignature)
    {
        return ((delegate* unmanaged[Stdcall]<IDxcPdbUtils*, char*, int>)(lpVtbl[26]))((IDxcPdbUtils*)Unsafe.AsPointer(ref this), pRootSignature);
    }

    public interface Interface : IUnknown.Interface
    {
        [return: NativeTypeName("HRESULT")]
        int Load(IDxcBlob* pPdbOrDxil);

        [return: NativeTypeName("HRESULT")]
        int GetSourceCount([NativeTypeName("UINT32 *")] uint* pCount);

        [return: NativeTypeName("HRESULT")]
        int GetSource([NativeTypeName("UINT32")] uint uIndex, IDxcBlobEncoding** ppResult);

        [return: NativeTypeName("HRESULT")]
        int GetSourceName([NativeTypeName("UINT32")] uint uIndex, [NativeTypeName("BSTR *")] char** pResult);

        [return: NativeTypeName("HRESULT")]
        int GetFlagCount([NativeTypeName("UINT32 *")] uint* pCount);

        [return: NativeTypeName("HRESULT")]
        int GetFlag([NativeTypeName("UINT32")] uint uIndex, [NativeTypeName("BSTR *")] char** pResult);

        [return: NativeTypeName("HRESULT")]
        int GetArgCount([NativeTypeName("UINT32 *")] uint* pCount);

        [return: NativeTypeName("HRESULT")]
        int GetArg([NativeTypeName("UINT32")] uint uIndex, [NativeTypeName("BSTR *")] char** pResult);

        [return: NativeTypeName("HRESULT")]
        int GetArgPairCount([NativeTypeName("UINT32 *")] uint* pCount);

        [return: NativeTypeName("HRESULT")]
        int GetArgPair([NativeTypeName("UINT32")] uint uIndex, [NativeTypeName("BSTR *")] char** pName, [NativeTypeName("BSTR *")] char** pValue);

        [return: NativeTypeName("HRESULT")]
        int GetDefineCount([NativeTypeName("UINT32 *")] uint* pCount);

        [return: NativeTypeName("HRESULT")]
        int GetDefine([NativeTypeName("UINT32")] uint uIndex, [NativeTypeName("BSTR *")] char** pResult);

        [return: NativeTypeName("HRESULT")]
        int GetTargetProfile([NativeTypeName("BSTR *")] char** pResult);

        [return: NativeTypeName("HRESULT")]
        int GetEntryPoint([NativeTypeName("BSTR *")] char** pResult);

        [return: NativeTypeName("HRESULT")]
        int GetMainFileName([NativeTypeName("BSTR *")] char** pResult);

        [return: NativeTypeName("HRESULT")]
        int GetHash(IDxcBlob** ppResult);

        [return: NativeTypeName("HRESULT")]
        int GetName([NativeTypeName("BSTR *")] char** pResult);

        [return: NativeTypeName("BOOL")]
        int IsFullPDB();

        [return: NativeTypeName("HRESULT")]
        int GetFullPDB(IDxcBlob** ppFullPDB);

        [return: NativeTypeName("HRESULT")]
        int GetVersionInfo(IDxcVersionInfo** ppVersionInfo);

        [return: NativeTypeName("HRESULT")]
        int SetCompiler(IDxcCompiler3* pCompiler);

        [return: NativeTypeName("HRESULT")]
        int CompileForFullPDB(IDxcResult** ppResult);

        [return: NativeTypeName("HRESULT")]
        int OverrideArgs(DxcArgPair* pArgPairs, [NativeTypeName("UINT32")] uint uNumArgPairs);

        [return: NativeTypeName("HRESULT")]
        int OverrideRootSignature([NativeTypeName("const WCHAR *")] char* pRootSignature);
    }
}

[Guid("4315D938-F369-4F93-95A2-252017CC3807")]
[NativeTypeName("struct IDxcPdbUtils2 : IUnknown")]
internal unsafe partial struct IDxcPdbUtils2 : IDxcPdbUtils2.Interface, INativeGuid
{
    static Guid* INativeGuid.NativeGuid => (Guid*)Unsafe.AsPointer(in IID_IDxcPdbUtils2);

    public void** lpVtbl;

    [return: NativeTypeName("HRESULT")]
    public int QueryInterface([NativeTypeName("const IID &")] Guid* riid, void** ppvObject)
    {
        return ((delegate* unmanaged[Stdcall]<IDxcPdbUtils2*, Guid*, void**, int>)(lpVtbl[0]))((IDxcPdbUtils2*)Unsafe.AsPointer(ref this), riid, ppvObject);
    }

    [return: NativeTypeName("ULONG")]
    public uint AddRef()
    {
        return ((delegate* unmanaged[Stdcall]<IDxcPdbUtils2*, uint>)(lpVtbl[1]))((IDxcPdbUtils2*)Unsafe.AsPointer(ref this));
    }

    [return: NativeTypeName("ULONG")]
    public uint Release()
    {
        return ((delegate* unmanaged[Stdcall]<IDxcPdbUtils2*, uint>)(lpVtbl[2]))((IDxcPdbUtils2*)Unsafe.AsPointer(ref this));
    }

    [return: NativeTypeName("HRESULT")]
    public int Load(IDxcBlob* pPdbOrDxil)
    {
        return ((delegate* unmanaged[Stdcall]<IDxcPdbUtils2*, IDxcBlob*, int>)(lpVtbl[3]))((IDxcPdbUtils2*)Unsafe.AsPointer(ref this), pPdbOrDxil);
    }

    [return: NativeTypeName("HRESULT")]
    public int GetSourceCount([NativeTypeName("UINT32 *")] uint* pCount)
    {
        return ((delegate* unmanaged[Stdcall]<IDxcPdbUtils2*, uint*, int>)(lpVtbl[4]))((IDxcPdbUtils2*)Unsafe.AsPointer(ref this), pCount);
    }

    [return: NativeTypeName("HRESULT")]
    public int GetSource([NativeTypeName("UINT32")] uint uIndex, IDxcBlobEncoding** ppResult)
    {
        return ((delegate* unmanaged[Stdcall]<IDxcPdbUtils2*, uint, IDxcBlobEncoding**, int>)(lpVtbl[5]))((IDxcPdbUtils2*)Unsafe.AsPointer(ref this), uIndex, ppResult);
    }

    [return: NativeTypeName("HRESULT")]
    public int GetSourceName([NativeTypeName("UINT32")] uint uIndex, IDxcBlobWide** ppResult)
    {
        return ((delegate* unmanaged[Stdcall]<IDxcPdbUtils2*, uint, IDxcBlobWide**, int>)(lpVtbl[6]))((IDxcPdbUtils2*)Unsafe.AsPointer(ref this), uIndex, ppResult);
    }

    [return: NativeTypeName("HRESULT")]
    public int GetLibraryPDBCount([NativeTypeName("UINT32 *")] uint* pCount)
    {
        return ((delegate* unmanaged[Stdcall]<IDxcPdbUtils2*, uint*, int>)(lpVtbl[7]))((IDxcPdbUtils2*)Unsafe.AsPointer(ref this), pCount);
    }

    [return: NativeTypeName("HRESULT")]
    public int GetLibraryPDB([NativeTypeName("UINT32")] uint uIndex, IDxcPdbUtils2** ppOutPdbUtils, IDxcBlobWide** ppLibraryName)
    {
        return ((delegate* unmanaged[Stdcall]<IDxcPdbUtils2*, uint, IDxcPdbUtils2**, IDxcBlobWide**, int>)(lpVtbl[8]))((IDxcPdbUtils2*)Unsafe.AsPointer(ref this), uIndex, ppOutPdbUtils, ppLibraryName);
    }

    [return: NativeTypeName("HRESULT")]
    public int GetFlagCount([NativeTypeName("UINT32 *")] uint* pCount)
    {
        return ((delegate* unmanaged[Stdcall]<IDxcPdbUtils2*, uint*, int>)(lpVtbl[9]))((IDxcPdbUtils2*)Unsafe.AsPointer(ref this), pCount);
    }

    [return: NativeTypeName("HRESULT")]
    public int GetFlag([NativeTypeName("UINT32")] uint uIndex, IDxcBlobWide** ppResult)
    {
        return ((delegate* unmanaged[Stdcall]<IDxcPdbUtils2*, uint, IDxcBlobWide**, int>)(lpVtbl[10]))((IDxcPdbUtils2*)Unsafe.AsPointer(ref this), uIndex, ppResult);
    }

    [return: NativeTypeName("HRESULT")]
    public int GetArgCount([NativeTypeName("UINT32 *")] uint* pCount)
    {
        return ((delegate* unmanaged[Stdcall]<IDxcPdbUtils2*, uint*, int>)(lpVtbl[11]))((IDxcPdbUtils2*)Unsafe.AsPointer(ref this), pCount);
    }

    [return: NativeTypeName("HRESULT")]
    public int GetArg([NativeTypeName("UINT32")] uint uIndex, IDxcBlobWide** ppResult)
    {
        return ((delegate* unmanaged[Stdcall]<IDxcPdbUtils2*, uint, IDxcBlobWide**, int>)(lpVtbl[12]))((IDxcPdbUtils2*)Unsafe.AsPointer(ref this), uIndex, ppResult);
    }

    [return: NativeTypeName("HRESULT")]
    public int GetArgPairCount([NativeTypeName("UINT32 *")] uint* pCount)
    {
        return ((delegate* unmanaged[Stdcall]<IDxcPdbUtils2*, uint*, int>)(lpVtbl[13]))((IDxcPdbUtils2*)Unsafe.AsPointer(ref this), pCount);
    }

    [return: NativeTypeName("HRESULT")]
    public int GetArgPair([NativeTypeName("UINT32")] uint uIndex, IDxcBlobWide** ppName, IDxcBlobWide** ppValue)
    {
        return ((delegate* unmanaged[Stdcall]<IDxcPdbUtils2*, uint, IDxcBlobWide**, IDxcBlobWide**, int>)(lpVtbl[14]))((IDxcPdbUtils2*)Unsafe.AsPointer(ref this), uIndex, ppName, ppValue);
    }

    [return: NativeTypeName("HRESULT")]
    public int GetDefineCount([NativeTypeName("UINT32 *")] uint* pCount)
    {
        return ((delegate* unmanaged[Stdcall]<IDxcPdbUtils2*, uint*, int>)(lpVtbl[15]))((IDxcPdbUtils2*)Unsafe.AsPointer(ref this), pCount);
    }

    [return: NativeTypeName("HRESULT")]
    public int GetDefine([NativeTypeName("UINT32")] uint uIndex, IDxcBlobWide** ppResult)
    {
        return ((delegate* unmanaged[Stdcall]<IDxcPdbUtils2*, uint, IDxcBlobWide**, int>)(lpVtbl[16]))((IDxcPdbUtils2*)Unsafe.AsPointer(ref this), uIndex, ppResult);
    }

    [return: NativeTypeName("HRESULT")]
    public int GetTargetProfile(IDxcBlobWide** ppResult)
    {
        return ((delegate* unmanaged[Stdcall]<IDxcPdbUtils2*, IDxcBlobWide**, int>)(lpVtbl[17]))((IDxcPdbUtils2*)Unsafe.AsPointer(ref this), ppResult);
    }

    [return: NativeTypeName("HRESULT")]
    public int GetEntryPoint(IDxcBlobWide** ppResult)
    {
        return ((delegate* unmanaged[Stdcall]<IDxcPdbUtils2*, IDxcBlobWide**, int>)(lpVtbl[18]))((IDxcPdbUtils2*)Unsafe.AsPointer(ref this), ppResult);
    }

    [return: NativeTypeName("HRESULT")]
    public int GetMainFileName(IDxcBlobWide** ppResult)
    {
        return ((delegate* unmanaged[Stdcall]<IDxcPdbUtils2*, IDxcBlobWide**, int>)(lpVtbl[19]))((IDxcPdbUtils2*)Unsafe.AsPointer(ref this), ppResult);
    }

    [return: NativeTypeName("HRESULT")]
    public int GetHash(IDxcBlob** ppResult)
    {
        return ((delegate* unmanaged[Stdcall]<IDxcPdbUtils2*, IDxcBlob**, int>)(lpVtbl[20]))((IDxcPdbUtils2*)Unsafe.AsPointer(ref this), ppResult);
    }

    [return: NativeTypeName("HRESULT")]
    public int GetName(IDxcBlobWide** ppResult)
    {
        return ((delegate* unmanaged[Stdcall]<IDxcPdbUtils2*, IDxcBlobWide**, int>)(lpVtbl[21]))((IDxcPdbUtils2*)Unsafe.AsPointer(ref this), ppResult);
    }

    [return: NativeTypeName("HRESULT")]
    public int GetVersionInfo(IDxcVersionInfo** ppVersionInfo)
    {
        return ((delegate* unmanaged[Stdcall]<IDxcPdbUtils2*, IDxcVersionInfo**, int>)(lpVtbl[22]))((IDxcPdbUtils2*)Unsafe.AsPointer(ref this), ppVersionInfo);
    }

    [return: NativeTypeName("HRESULT")]
    public int GetCustomToolchainID([NativeTypeName("UINT32 *")] uint* pID)
    {
        return ((delegate* unmanaged[Stdcall]<IDxcPdbUtils2*, uint*, int>)(lpVtbl[23]))((IDxcPdbUtils2*)Unsafe.AsPointer(ref this), pID);
    }

    [return: NativeTypeName("HRESULT")]
    public int GetCustomToolchainData(IDxcBlob** ppBlob)
    {
        return ((delegate* unmanaged[Stdcall]<IDxcPdbUtils2*, IDxcBlob**, int>)(lpVtbl[24]))((IDxcPdbUtils2*)Unsafe.AsPointer(ref this), ppBlob);
    }

    [return: NativeTypeName("HRESULT")]
    public int GetWholeDxil(IDxcBlob** ppResult)
    {
        return ((delegate* unmanaged[Stdcall]<IDxcPdbUtils2*, IDxcBlob**, int>)(lpVtbl[25]))((IDxcPdbUtils2*)Unsafe.AsPointer(ref this), ppResult);
    }

    [return: NativeTypeName("BOOL")]
    public int IsFullPDB()
    {
        return ((delegate* unmanaged[Stdcall]<IDxcPdbUtils2*, int>)(lpVtbl[26]))((IDxcPdbUtils2*)Unsafe.AsPointer(ref this));
    }

    [return: NativeTypeName("BOOL")]
    public int IsPDBRef()
    {
        return ((delegate* unmanaged[Stdcall]<IDxcPdbUtils2*, int>)(lpVtbl[27]))((IDxcPdbUtils2*)Unsafe.AsPointer(ref this));
    }

    public interface Interface : IUnknown.Interface
    {
        [return: NativeTypeName("HRESULT")]
        int Load(IDxcBlob* pPdbOrDxil);

        [return: NativeTypeName("HRESULT")]
        int GetSourceCount([NativeTypeName("UINT32 *")] uint* pCount);

        [return: NativeTypeName("HRESULT")]
        int GetSource([NativeTypeName("UINT32")] uint uIndex, IDxcBlobEncoding** ppResult);

        [return: NativeTypeName("HRESULT")]
        int GetSourceName([NativeTypeName("UINT32")] uint uIndex, IDxcBlobWide** ppResult);

        [return: NativeTypeName("HRESULT")]
        int GetLibraryPDBCount([NativeTypeName("UINT32 *")] uint* pCount);

        [return: NativeTypeName("HRESULT")]
        int GetLibraryPDB([NativeTypeName("UINT32")] uint uIndex, IDxcPdbUtils2** ppOutPdbUtils, IDxcBlobWide** ppLibraryName);

        [return: NativeTypeName("HRESULT")]
        int GetFlagCount([NativeTypeName("UINT32 *")] uint* pCount);

        [return: NativeTypeName("HRESULT")]
        int GetFlag([NativeTypeName("UINT32")] uint uIndex, IDxcBlobWide** ppResult);

        [return: NativeTypeName("HRESULT")]
        int GetArgCount([NativeTypeName("UINT32 *")] uint* pCount);

        [return: NativeTypeName("HRESULT")]
        int GetArg([NativeTypeName("UINT32")] uint uIndex, IDxcBlobWide** ppResult);

        [return: NativeTypeName("HRESULT")]
        int GetArgPairCount([NativeTypeName("UINT32 *")] uint* pCount);

        [return: NativeTypeName("HRESULT")]
        int GetArgPair([NativeTypeName("UINT32")] uint uIndex, IDxcBlobWide** ppName, IDxcBlobWide** ppValue);

        [return: NativeTypeName("HRESULT")]
        int GetDefineCount([NativeTypeName("UINT32 *")] uint* pCount);

        [return: NativeTypeName("HRESULT")]
        int GetDefine([NativeTypeName("UINT32")] uint uIndex, IDxcBlobWide** ppResult);

        [return: NativeTypeName("HRESULT")]
        int GetTargetProfile(IDxcBlobWide** ppResult);

        [return: NativeTypeName("HRESULT")]
        int GetEntryPoint(IDxcBlobWide** ppResult);

        [return: NativeTypeName("HRESULT")]
        int GetMainFileName(IDxcBlobWide** ppResult);

        [return: NativeTypeName("HRESULT")]
        int GetHash(IDxcBlob** ppResult);

        [return: NativeTypeName("HRESULT")]
        int GetName(IDxcBlobWide** ppResult);

        [return: NativeTypeName("HRESULT")]
        int GetVersionInfo(IDxcVersionInfo** ppVersionInfo);

        [return: NativeTypeName("HRESULT")]
        int GetCustomToolchainID([NativeTypeName("UINT32 *")] uint* pID);

        [return: NativeTypeName("HRESULT")]
        int GetCustomToolchainData(IDxcBlob** ppBlob);

        [return: NativeTypeName("HRESULT")]
        int GetWholeDxil(IDxcBlob** ppResult);

        [return: NativeTypeName("BOOL")]
        int IsFullPDB();

        [return: NativeTypeName("BOOL")]
        int IsPDBRef();
    }
}

internal static unsafe partial class Dxc
{
    [DllImport("dxcompiler", CallingConvention = CallingConvention.StdCall, ExactSpelling = true)]
    [return: NativeTypeName("HRESULT")]
    public static extern int DxcCreateInstance([NativeTypeName("const IID &")] Guid* rclsid, [NativeTypeName("const IID &")] Guid* riid, [NativeTypeName("LPVOID *")] void** ppv);

    [DllImport("dxcompiler", CallingConvention = CallingConvention.StdCall, ExactSpelling = true)]
    [return: NativeTypeName("HRESULT")]
    public static extern int DxcCreateInstance2(IMalloc* pMalloc, [NativeTypeName("const IID &")] Guid* rclsid, [NativeTypeName("const IID &")] Guid* riid, [NativeTypeName("LPVOID *")] void** ppv);

    [NativeTypeName("const UINT32")]
    public const uint DxcValidatorFlags_Default = 0;

    [NativeTypeName("const UINT32")]
    public const uint DxcValidatorFlags_InPlaceEdit = 1;

    [NativeTypeName("const UINT32")]
    public const uint DxcValidatorFlags_RootSignatureOnly = 2;

    [NativeTypeName("const UINT32")]
    public const uint DxcValidatorFlags_ModuleOnly = 4;

    [NativeTypeName("const UINT32")]
    public const uint DxcValidatorFlags_ValidMask = 0x7;

    [NativeTypeName("const UINT32")]
    public const uint DxcVersionInfoFlags_None = 0;

    [NativeTypeName("const UINT32")]
    public const uint DxcVersionInfoFlags_Debug = 1;

    [NativeTypeName("const UINT32")]
    public const uint DxcVersionInfoFlags_Internal = 2;

    [NativeTypeName("const CLSID")]
    public static ref readonly Guid CLSID_DxcCompiler
    {
        get
        {
            ReadOnlySpan<byte> data = [
                0x93, 0x2D, 0xE2, 0x73,
                0xCE, 0xE6,
                0xF3, 0x47,
                0xB5,
                0xBF,
                0xF0,
                0x66,
                0x4F,
                0x39,
                0xC1,
                0xB0
            ];

            Debug.Assert(data.Length == Unsafe.SizeOf<Guid>());
            return ref Unsafe.As<byte, Guid>(ref MemoryMarshal.GetReference(data));
        }
    }

    [NativeTypeName("const GUID")]
    public static ref readonly Guid CLSID_DxcLinker
    {
        get
        {
            ReadOnlySpan<byte> data = [
                0x87, 0x80, 0x6A, 0xEF,
                0xEA, 0xB0,
                0x56, 0x4D,
                0x9E,
                0x45,
                0xD0,
                0x7E,
                0x1A,
                0x8B,
                0x78,
                0x06
            ];

            Debug.Assert(data.Length == Unsafe.SizeOf<Guid>());
            return ref Unsafe.As<byte, Guid>(ref MemoryMarshal.GetReference(data));
        }
    }

    [NativeTypeName("const CLSID")]
    public static ref readonly Guid CLSID_DxcDiaDataSource
    {
        get
        {
            ReadOnlySpan<byte> data = [
                0x73, 0x6B, 0x1F, 0xCD,
                0xB0, 0x2A,
                0x4D, 0x48,
                0x8E,
                0xDC,
                0xEB,
                0xE7,
                0xA4,
                0x3C,
                0xA0,
                0x9F
            ];

            Debug.Assert(data.Length == Unsafe.SizeOf<Guid>());
            return ref Unsafe.As<byte, Guid>(ref MemoryMarshal.GetReference(data));
        }
    }

    [NativeTypeName("const CLSID")]
    public static ref readonly Guid CLSID_DxcCompilerArgs
    {
        get
        {
            ReadOnlySpan<byte> data = [
                0x82, 0xAE, 0x56, 0x3E,
                0x4D, 0x22,
                0x0F, 0x47,
                0xA1,
                0xA1,
                0xFE,
                0x30,
                0x16,
                0xEE,
                0x9F,
                0x9D
            ];

            Debug.Assert(data.Length == Unsafe.SizeOf<Guid>());
            return ref Unsafe.As<byte, Guid>(ref MemoryMarshal.GetReference(data));
        }
    }

    [NativeTypeName("const GUID")]
    public static ref readonly Guid CLSID_DxcLibrary
    {
        get
        {
            ReadOnlySpan<byte> data = [
                0xAF, 0xD6, 0x45, 0x62,
                0xE0, 0x66,
                0xFD, 0x48,
                0x80,
                0xB4,
                0x4D,
                0x27,
                0x17,
                0x96,
                0x74,
                0x8C
            ];

            Debug.Assert(data.Length == Unsafe.SizeOf<Guid>());
            return ref Unsafe.As<byte, Guid>(ref MemoryMarshal.GetReference(data));
        }
    }

    [NativeTypeName("const GUID")]
    public static ref readonly Guid CLSID_DxcUtils => ref CLSID_DxcLibrary;

    [NativeTypeName("const GUID")]
    public static ref readonly Guid CLSID_DxcValidator
    {
        get
        {
            ReadOnlySpan<byte> data = [
                0x15, 0xE2, 0xA3, 0x8C,
                0x28, 0xF7,
                0xF3, 0x4C,
                0x8C,
                0xDD,
                0x88,
                0xAF,
                0x91,
                0x75,
                0x87,
                0xA1
            ];

            Debug.Assert(data.Length == Unsafe.SizeOf<Guid>());
            return ref Unsafe.As<byte, Guid>(ref MemoryMarshal.GetReference(data));
        }
    }

    [NativeTypeName("const GUID")]
    public static ref readonly Guid CLSID_DxcAssembler
    {
        get
        {
            ReadOnlySpan<byte> data = [
                0x68, 0xDB, 0x28, 0xD7,
                0x03, 0xF9,
                0x80, 0x4F,
                0x94,
                0xCD,
                0xDC,
                0xCF,
                0x76,
                0xEC,
                0x71,
                0x51
            ];

            Debug.Assert(data.Length == Unsafe.SizeOf<Guid>());
            return ref Unsafe.As<byte, Guid>(ref MemoryMarshal.GetReference(data));
        }
    }

    [NativeTypeName("const GUID")]
    public static ref readonly Guid CLSID_DxcContainerReflection
    {
        get
        {
            ReadOnlySpan<byte> data = [
                0x89, 0x44, 0xF5, 0xB9,
                0xB8, 0x55,
                0x0C, 0x40,
                0xBA,
                0x3A,
                0x16,
                0x75,
                0xE4,
                0x72,
                0x8B,
                0x91
            ];

            Debug.Assert(data.Length == Unsafe.SizeOf<Guid>());
            return ref Unsafe.As<byte, Guid>(ref MemoryMarshal.GetReference(data));
        }
    }

    [NativeTypeName("const GUID")]
    public static ref readonly Guid CLSID_DxcOptimizer
    {
        get
        {
            ReadOnlySpan<byte> data = [
                0x9F, 0xD7, 0x2C, 0xAE,
                0x22, 0xCC,
                0x3F, 0x45,
                0x9B,
                0x6B,
                0xB1,
                0x24,
                0xE7,
                0xA5,
                0x20,
                0x4C
            ];

            Debug.Assert(data.Length == Unsafe.SizeOf<Guid>());
            return ref Unsafe.As<byte, Guid>(ref MemoryMarshal.GetReference(data));
        }
    }

    [NativeTypeName("const GUID")]
    public static ref readonly Guid CLSID_DxcContainerBuilder
    {
        get
        {
            ReadOnlySpan<byte> data = [
                0x94, 0x42, 0x13, 0x94,
                0x1F, 0x41,
                0x74, 0x45,
                0xB4,
                0xD0,
                0x87,
                0x41,
                0xE2,
                0x52,
                0x40,
                0xD2
            ];

            Debug.Assert(data.Length == Unsafe.SizeOf<Guid>());
            return ref Unsafe.As<byte, Guid>(ref MemoryMarshal.GetReference(data));
        }
    }

    [NativeTypeName("const GUID")]
    public static ref readonly Guid CLSID_DxcPdbUtils
    {
        get
        {
            ReadOnlySpan<byte> data = [
                0xFB, 0x1D, 0x62, 0x54,
                0xCE, 0xF2,
                0x7E, 0x45,
                0xAE,
                0x8C,
                0xEC,
                0x35,
                0x5F,
                0xAE,
                0xEC,
                0x7C
            ];

            Debug.Assert(data.Length == Unsafe.SizeOf<Guid>());
            return ref Unsafe.As<byte, Guid>(ref MemoryMarshal.GetReference(data));
        }
    }

    [NativeTypeName("#define DXC_CP_UTF8 65001")]
    public const int DXC_CP_UTF8 = 65001;

    [NativeTypeName("#define DXC_CP_UTF16 1200")]
    public const int DXC_CP_UTF16 = 1200;

    [NativeTypeName("#define DXC_CP_UTF32 12000")]
    public const int DXC_CP_UTF32 = 12000;

    [NativeTypeName("#define DXC_CP_ACP 0")]
    public const int DXC_CP_ACP = 0;

    [NativeTypeName("#define DXC_CP_WIDE DXC_CP_UTF16")]
    public const int DXC_CP_WIDE = 1200;

    [NativeTypeName("#define DXC_HASHFLAG_INCLUDES_SOURCE 1")]
    public const int DXC_HASHFLAG_INCLUDES_SOURCE = 1;

    [NativeTypeName("#define DXC_PART_PDB DXC_FOURCC('I', 'L', 'D', 'B')")]
    public const uint DXC_PART_PDB = ((uint)((byte)('I')) | (uint)((byte)('L')) << 8 | (uint)((byte)('D')) << 16 | (uint)((byte)('B')) << 24);

    [NativeTypeName("#define DXC_PART_PDB_NAME DXC_FOURCC('I', 'L', 'D', 'N')")]
    public const uint DXC_PART_PDB_NAME = ((uint)((byte)('I')) | (uint)((byte)('L')) << 8 | (uint)((byte)('D')) << 16 | (uint)((byte)('N')) << 24);

    [NativeTypeName("#define DXC_PART_PRIVATE_DATA DXC_FOURCC('P', 'R', 'I', 'V')")]
    public const uint DXC_PART_PRIVATE_DATA = ((uint)((byte)('P')) | (uint)((byte)('R')) << 8 | (uint)((byte)('I')) << 16 | (uint)((byte)('V')) << 24);

    [NativeTypeName("#define DXC_PART_ROOT_SIGNATURE DXC_FOURCC('R', 'T', 'S', '0')")]
    public const uint DXC_PART_ROOT_SIGNATURE = ((uint)((byte)('R')) | (uint)((byte)('T')) << 8 | (uint)((byte)('S')) << 16 | (uint)((byte)('0')) << 24);

    [NativeTypeName("#define DXC_PART_DXIL DXC_FOURCC('D', 'X', 'I', 'L')")]
    public const uint DXC_PART_DXIL = ((uint)((byte)('D')) | (uint)((byte)('X')) << 8 | (uint)((byte)('I')) << 16 | (uint)((byte)('L')) << 24);

    [NativeTypeName("#define DXC_PART_REFLECTION_DATA DXC_FOURCC('S', 'T', 'A', 'T')")]
    public const uint DXC_PART_REFLECTION_DATA = ((uint)((byte)('S')) | (uint)((byte)('T')) << 8 | (uint)((byte)('A')) << 16 | (uint)((byte)('T')) << 24);

    [NativeTypeName("#define DXC_PART_SHADER_HASH DXC_FOURCC('H', 'A', 'S', 'H')")]
    public const uint DXC_PART_SHADER_HASH = ((uint)((byte)('H')) | (uint)((byte)('A')) << 8 | (uint)((byte)('S')) << 16 | (uint)((byte)('H')) << 24);

    [NativeTypeName("#define DXC_PART_INPUT_SIGNATURE DXC_FOURCC('I', 'S', 'G', '1')")]
    public const uint DXC_PART_INPUT_SIGNATURE = ((uint)((byte)('I')) | (uint)((byte)('S')) << 8 | (uint)((byte)('G')) << 16 | (uint)((byte)('1')) << 24);

    [NativeTypeName("#define DXC_PART_OUTPUT_SIGNATURE DXC_FOURCC('O', 'S', 'G', '1')")]
    public const uint DXC_PART_OUTPUT_SIGNATURE = ((uint)((byte)('O')) | (uint)((byte)('S')) << 8 | (uint)((byte)('G')) << 16 | (uint)((byte)('1')) << 24);

    [NativeTypeName("#define DXC_PART_PATCH_CONSTANT_SIGNATURE DXC_FOURCC('P', 'S', 'G', '1')")]
    public const uint DXC_PART_PATCH_CONSTANT_SIGNATURE = ((uint)((byte)('P')) | (uint)((byte)('S')) << 8 | (uint)((byte)('G')) << 16 | (uint)((byte)('1')) << 24);

    [NativeTypeName("#define DXC_ARG_DEBUG L\"-Zi\"")]
    public const string DXC_ARG_DEBUG = "-Zi";

    [NativeTypeName("#define DXC_ARG_SKIP_VALIDATION L\"-Vd\"")]
    public const string DXC_ARG_SKIP_VALIDATION = "-Vd";

    [NativeTypeName("#define DXC_ARG_SKIP_OPTIMIZATIONS L\"-Od\"")]
    public const string DXC_ARG_SKIP_OPTIMIZATIONS = "-Od";

    [NativeTypeName("#define DXC_ARG_PACK_MATRIX_ROW_MAJOR L\"-Zpr\"")]
    public const string DXC_ARG_PACK_MATRIX_ROW_MAJOR = "-Zpr";

    [NativeTypeName("#define DXC_ARG_PACK_MATRIX_COLUMN_MAJOR L\"-Zpc\"")]
    public const string DXC_ARG_PACK_MATRIX_COLUMN_MAJOR = "-Zpc";

    [NativeTypeName("#define DXC_ARG_AVOID_FLOW_CONTROL L\"-Gfa\"")]
    public const string DXC_ARG_AVOID_FLOW_CONTROL = "-Gfa";

    [NativeTypeName("#define DXC_ARG_PREFER_FLOW_CONTROL L\"-Gfp\"")]
    public const string DXC_ARG_PREFER_FLOW_CONTROL = "-Gfp";

    [NativeTypeName("#define DXC_ARG_ENABLE_STRICTNESS L\"-Ges\"")]
    public const string DXC_ARG_ENABLE_STRICTNESS = "-Ges";

    [NativeTypeName("#define DXC_ARG_ENABLE_BACKWARDS_COMPATIBILITY L\"-Gec\"")]
    public const string DXC_ARG_ENABLE_BACKWARDS_COMPATIBILITY = "-Gec";

    [NativeTypeName("#define DXC_ARG_IEEE_STRICTNESS L\"-Gis\"")]
    public const string DXC_ARG_IEEE_STRICTNESS = "-Gis";

    [NativeTypeName("#define DXC_ARG_OPTIMIZATION_LEVEL0 L\"-O0\"")]
    public const string DXC_ARG_OPTIMIZATION_LEVEL0 = "-O0";

    [NativeTypeName("#define DXC_ARG_OPTIMIZATION_LEVEL1 L\"-O1\"")]
    public const string DXC_ARG_OPTIMIZATION_LEVEL1 = "-O1";

    [NativeTypeName("#define DXC_ARG_OPTIMIZATION_LEVEL2 L\"-O2\"")]
    public const string DXC_ARG_OPTIMIZATION_LEVEL2 = "-O2";

    [NativeTypeName("#define DXC_ARG_OPTIMIZATION_LEVEL3 L\"-O3\"")]
    public const string DXC_ARG_OPTIMIZATION_LEVEL3 = "-O3";

    [NativeTypeName("#define DXC_ARG_WARNINGS_ARE_ERRORS L\"-WX\"")]
    public const string DXC_ARG_WARNINGS_ARE_ERRORS = "-WX";

    [NativeTypeName("#define DXC_ARG_RESOURCES_MAY_ALIAS L\"-res_may_alias\"")]
    public const string DXC_ARG_RESOURCES_MAY_ALIAS = "-res_may_alias";

    [NativeTypeName("#define DXC_ARG_ALL_RESOURCES_BOUND L\"-all_resources_bound\"")]
    public const string DXC_ARG_ALL_RESOURCES_BOUND = "-all_resources_bound";

    [NativeTypeName("#define DXC_ARG_DEBUG_NAME_FOR_SOURCE L\"-Zss\"")]
    public const string DXC_ARG_DEBUG_NAME_FOR_SOURCE = "-Zss";

    [NativeTypeName("#define DXC_ARG_DEBUG_NAME_FOR_BINARY L\"-Zsb\"")]
    public const string DXC_ARG_DEBUG_NAME_FOR_BINARY = "-Zsb";

    [NativeTypeName("#define DXC_EXTRA_OUTPUT_NAME_STDOUT L\"*stdout*\"")]
    public const string DXC_EXTRA_OUTPUT_NAME_STDOUT = "*stdout*";

    [NativeTypeName("#define DXC_EXTRA_OUTPUT_NAME_STDERR L\"*stderr*\"")]
    public const string DXC_EXTRA_OUTPUT_NAME_STDERR = "*stderr*";

    public static ref readonly Guid IID_IDxcBlob
    {
        get
        {
            ReadOnlySpan<byte> data = [
                0x08, 0xFB, 0xA5, 0x8B,
                0x95, 0x51,
                0xE2, 0x40,
                0xAC,
                0x58,
                0x0D,
                0x98,
                0x9C,
                0x3A,
                0x01,
                0x02
            ];

            Debug.Assert(data.Length == Unsafe.SizeOf<Guid>());
            return ref Unsafe.As<byte, Guid>(ref MemoryMarshal.GetReference(data));
        }
    }

    public static ref readonly Guid IID_IDxcBlobEncoding
    {
        get
        {
            ReadOnlySpan<byte> data = [
                0x24, 0xD4, 0x41, 0x72,
                0x46, 0x26,
                0x91, 0x41,
                0x97,
                0xC0,
                0x98,
                0xE9,
                0x6E,
                0x42,
                0xFC,
                0x68
            ];

            Debug.Assert(data.Length == Unsafe.SizeOf<Guid>());
            return ref Unsafe.As<byte, Guid>(ref MemoryMarshal.GetReference(data));
        }
    }

    public static ref readonly Guid IID_IDxcBlobWide
    {
        get
        {
            ReadOnlySpan<byte> data = [
                0xAB, 0x4E, 0xF8, 0xA3,
                0xAA, 0x0F,
                0x7E, 0x49,
                0xA3,
                0x9C,
                0xEE,
                0x6E,
                0xD6,
                0x0B,
                0x2D,
                0x84
            ];

            Debug.Assert(data.Length == Unsafe.SizeOf<Guid>());
            return ref Unsafe.As<byte, Guid>(ref MemoryMarshal.GetReference(data));
        }
    }

    public static ref readonly Guid IID_IDxcBlobUtf8
    {
        get
        {
            ReadOnlySpan<byte> data = [
                0xC9, 0x36, 0xA6, 0x3D,
                0x71, 0xBA,
                0x24, 0x40,
                0xA3,
                0x01,
                0x30,
                0xCB,
                0xF1,
                0x25,
                0x30,
                0x5B
            ];

            Debug.Assert(data.Length == Unsafe.SizeOf<Guid>());
            return ref Unsafe.As<byte, Guid>(ref MemoryMarshal.GetReference(data));
        }
    }

    public static ref readonly Guid IID_IDxcIncludeHandler
    {
        get
        {
            ReadOnlySpan<byte> data = [
                0x7D, 0xFC, 0x61, 0x7F,
                0x0D, 0x95,
                0x7F, 0x46,
                0xB3,
                0xE3,
                0x3C,
                0x02,
                0xFB,
                0x49,
                0x18,
                0x7C
            ];

            Debug.Assert(data.Length == Unsafe.SizeOf<Guid>());
            return ref Unsafe.As<byte, Guid>(ref MemoryMarshal.GetReference(data));
        }
    }

    public static ref readonly Guid IID_IDxcCompilerArgs
    {
        get
        {
            ReadOnlySpan<byte> data = [
                0x2A, 0xFE, 0xEF, 0x73,
                0xDC, 0x70,
                0xF8, 0x45,
                0x96,
                0x90,
                0xEF,
                0xF6,
                0x4C,
                0x02,
                0x42,
                0x9D
            ];

            Debug.Assert(data.Length == Unsafe.SizeOf<Guid>());
            return ref Unsafe.As<byte, Guid>(ref MemoryMarshal.GetReference(data));
        }
    }

    public static ref readonly Guid IID_IDxcLibrary
    {
        get
        {
            ReadOnlySpan<byte> data = [
                0xC7, 0x4D, 0x20, 0xE5,
                0x8C, 0xD1,
                0x3C, 0x4C,
                0xBD,
                0xFB,
                0x85,
                0x16,
                0x73,
                0x98,
                0x0F,
                0xE7
            ];

            Debug.Assert(data.Length == Unsafe.SizeOf<Guid>());
            return ref Unsafe.As<byte, Guid>(ref MemoryMarshal.GetReference(data));
        }
    }

    public static ref readonly Guid IID_IDxcOperationResult
    {
        get
        {
            ReadOnlySpan<byte> data = [
                0x4A, 0x48, 0xDB, 0xCE,
                0xE9, 0xD4,
                0x5A, 0x44,
                0xB9,
                0x91,
                0xCA,
                0x21,
                0xCA,
                0x15,
                0x7D,
                0xC2
            ];

            Debug.Assert(data.Length == Unsafe.SizeOf<Guid>());
            return ref Unsafe.As<byte, Guid>(ref MemoryMarshal.GetReference(data));
        }
    }

    public static ref readonly Guid IID_IDxcCompiler
    {
        get
        {
            ReadOnlySpan<byte> data = [
                0xF3, 0x0B, 0x21, 0x8C,
                0x1F, 0x01,
                0x22, 0x44,
                0x8D,
                0x70,
                0x6F,
                0x9A,
                0xCB,
                0x8D,
                0xB6,
                0x17
            ];

            Debug.Assert(data.Length == Unsafe.SizeOf<Guid>());
            return ref Unsafe.As<byte, Guid>(ref MemoryMarshal.GetReference(data));
        }
    }

    public static ref readonly Guid IID_IDxcCompiler2
    {
        get
        {
            ReadOnlySpan<byte> data = [
                0xD9, 0xA9, 0x05, 0xA0,
                0xBB, 0xB8,
                0x94, 0x45,
                0xB5,
                0xC9,
                0x0E,
                0x63,
                0x3B,
                0xEC,
                0x4D,
                0x37
            ];

            Debug.Assert(data.Length == Unsafe.SizeOf<Guid>());
            return ref Unsafe.As<byte, Guid>(ref MemoryMarshal.GetReference(data));
        }
    }

    public static ref readonly Guid IID_IDxcLinker
    {
        get
        {
            ReadOnlySpan<byte> data = [
                0x2A, 0xBE, 0xB5, 0xF1,
                0xDD, 0x62,
                0x27, 0x43,
                0xA1,
                0xC2,
                0x42,
                0xAC,
                0x1E,
                0x1E,
                0x78,
                0xE6
            ];

            Debug.Assert(data.Length == Unsafe.SizeOf<Guid>());
            return ref Unsafe.As<byte, Guid>(ref MemoryMarshal.GetReference(data));
        }
    }

    public static ref readonly Guid IID_IDxcUtils
    {
        get
        {
            ReadOnlySpan<byte> data = [
                0xCB, 0xC4, 0x05, 0x46,
                0x19, 0x20,
                0x2A, 0x49,
                0xAD,
                0xA4,
                0x65,
                0xF2,
                0x0B,
                0xB7,
                0xD6,
                0x7F
            ];

            Debug.Assert(data.Length == Unsafe.SizeOf<Guid>());
            return ref Unsafe.As<byte, Guid>(ref MemoryMarshal.GetReference(data));
        }
    }

    public static ref readonly Guid IID_IDxcResult
    {
        get
        {
            ReadOnlySpan<byte> data = [
                0xDA, 0x6C, 0x34, 0x58,
                0xE7, 0xDD,
                0x97, 0x44,
                0x94,
                0x61,
                0x6F,
                0x87,
                0xAF,
                0x5E,
                0x06,
                0x59
            ];

            Debug.Assert(data.Length == Unsafe.SizeOf<Guid>());
            return ref Unsafe.As<byte, Guid>(ref MemoryMarshal.GetReference(data));
        }
    }

    public static ref readonly Guid IID_IDxcExtraOutputs
    {
        get
        {
            ReadOnlySpan<byte> data = [
                0xA2, 0x37, 0x9B, 0x31,
                0xC2, 0xA5,
                0x4A, 0x49,
                0xA5,
                0xDE,
                0x48,
                0x01,
                0xB2,
                0xFA,
                0xF9,
                0x89
            ];

            Debug.Assert(data.Length == Unsafe.SizeOf<Guid>());
            return ref Unsafe.As<byte, Guid>(ref MemoryMarshal.GetReference(data));
        }
    }

    public static ref readonly Guid IID_IDxcCompiler3
    {
        get
        {
            ReadOnlySpan<byte> data = [
                0x87, 0x46, 0x8B, 0x22,
                0x6A, 0x5A,
                0x30, 0x47,
                0x90,
                0x0C,
                0x97,
                0x02,
                0xB2,
                0x20,
                0x3F,
                0x54
            ];

            Debug.Assert(data.Length == Unsafe.SizeOf<Guid>());
            return ref Unsafe.As<byte, Guid>(ref MemoryMarshal.GetReference(data));
        }
    }

    public static ref readonly Guid IID_IDxcValidator
    {
        get
        {
            ReadOnlySpan<byte> data = [
                0xD2, 0x2B, 0xE8, 0xA6,
                0xD7, 0x1F,
                0x26, 0x48,
                0x98,
                0x11,
                0x28,
                0x57,
                0xE7,
                0x97,
                0xF4,
                0x9A
            ];

            Debug.Assert(data.Length == Unsafe.SizeOf<Guid>());
            return ref Unsafe.As<byte, Guid>(ref MemoryMarshal.GetReference(data));
        }
    }

    public static ref readonly Guid IID_IDxcValidator2
    {
        get
        {
            ReadOnlySpan<byte> data = [
                0xD1, 0x1F, 0x8E, 0x45,
                0xB2, 0xB1,
                0x50, 0x47,
                0xA6,
                0xE1,
                0x9C,
                0x10,
                0xF0,
                0x3B,
                0xED,
                0x92
            ];

            Debug.Assert(data.Length == Unsafe.SizeOf<Guid>());
            return ref Unsafe.As<byte, Guid>(ref MemoryMarshal.GetReference(data));
        }
    }

    public static ref readonly Guid IID_IDxcContainerBuilder
    {
        get
        {
            ReadOnlySpan<byte> data = [
                0x50, 0x1F, 0x4B, 0x33,
                0x92, 0x22,
                0x35, 0x4B,
                0x99,
                0xA1,
                0x25,
                0x58,
                0x8D,
                0x8C,
                0x17,
                0xFE
            ];

            Debug.Assert(data.Length == Unsafe.SizeOf<Guid>());
            return ref Unsafe.As<byte, Guid>(ref MemoryMarshal.GetReference(data));
        }
    }

    public static ref readonly Guid IID_IDxcAssembler
    {
        get
        {
            ReadOnlySpan<byte> data = [
                0x26, 0x7A, 0x1F, 0x09,
                0x1F, 0x1C,
                0x48, 0x49,
                0x90,
                0x4B,
                0xE6,
                0xE3,
                0xA8,
                0xA7,
                0x71,
                0xD5
            ];

            Debug.Assert(data.Length == Unsafe.SizeOf<Guid>());
            return ref Unsafe.As<byte, Guid>(ref MemoryMarshal.GetReference(data));
        }
    }

    public static ref readonly Guid IID_IDxcContainerReflection
    {
        get
        {
            ReadOnlySpan<byte> data = [
                0x26, 0x1B, 0xC2, 0xD2,
                0x50, 0x83,
                0xDC, 0x4B,
                0x97,
                0x6A,
                0x33,
                0x1C,
                0xE6,
                0xF4,
                0xC5,
                0x4C
            ];

            Debug.Assert(data.Length == Unsafe.SizeOf<Guid>());
            return ref Unsafe.As<byte, Guid>(ref MemoryMarshal.GetReference(data));
        }
    }

    public static ref readonly Guid IID_IDxcOptimizerPass
    {
        get
        {
            ReadOnlySpan<byte> data = [
                0x9F, 0xD7, 0x2C, 0xAE,
                0x22, 0xCC,
                0x3F, 0x45,
                0x9B,
                0x6B,
                0xB1,
                0x24,
                0xE7,
                0xA5,
                0x20,
                0x4C
            ];

            Debug.Assert(data.Length == Unsafe.SizeOf<Guid>());
            return ref Unsafe.As<byte, Guid>(ref MemoryMarshal.GetReference(data));
        }
    }

    public static ref readonly Guid IID_IDxcOptimizer
    {
        get
        {
            ReadOnlySpan<byte> data = [
                0x2E, 0x0E, 0x74, 0x25,
                0xBA, 0x9C,
                0x1B, 0x40,
                0x91,
                0x19,
                0x4F,
                0xB4,
                0x2F,
                0x39,
                0xF2,
                0x70
            ];

            Debug.Assert(data.Length == Unsafe.SizeOf<Guid>());
            return ref Unsafe.As<byte, Guid>(ref MemoryMarshal.GetReference(data));
        }
    }

    public static ref readonly Guid IID_IDxcVersionInfo
    {
        get
        {
            ReadOnlySpan<byte> data = [
                0x50, 0x5B, 0x4F, 0xB0,
                0x59, 0x20,
                0x12, 0x4F,
                0xA8,
                0xFF,
                0xA1,
                0xE0,
                0xCD,
                0xE1,
                0xCC,
                0x7E
            ];

            Debug.Assert(data.Length == Unsafe.SizeOf<Guid>());
            return ref Unsafe.As<byte, Guid>(ref MemoryMarshal.GetReference(data));
        }
    }

    public static ref readonly Guid IID_IDxcVersionInfo2
    {
        get
        {
            ReadOnlySpan<byte> data = [
                0xC4, 0x04, 0x69, 0xFB,
                0xF0, 0x42,
                0x62, 0x4B,
                0x9C,
                0x46,
                0x98,
                0x3A,
                0xF7,
                0xDA,
                0x7C,
                0x83
            ];

            Debug.Assert(data.Length == Unsafe.SizeOf<Guid>());
            return ref Unsafe.As<byte, Guid>(ref MemoryMarshal.GetReference(data));
        }
    }

    public static ref readonly Guid IID_IDxcVersionInfo3
    {
        get
        {
            ReadOnlySpan<byte> data = [
                0x43, 0xE8, 0x13, 0x5E,
                0x25, 0x9D,
                0x3C, 0x47,
                0x9A,
                0xD2,
                0x03,
                0xB2,
                0xD0,
                0xB4,
                0x4B,
                0x1E
            ];

            Debug.Assert(data.Length == Unsafe.SizeOf<Guid>());
            return ref Unsafe.As<byte, Guid>(ref MemoryMarshal.GetReference(data));
        }
    }

    public static ref readonly Guid IID_IDxcPdbUtils
    {
        get
        {
            ReadOnlySpan<byte> data = [
                0x7E, 0x64, 0xC9, 0xE6,
                0x6A, 0x9D,
                0x3B, 0x4C,
                0xB9,
                0x4C,
                0x52,
                0x4B,
                0x5A,
                0x6C,
                0x34,
                0x3D
            ];

            Debug.Assert(data.Length == Unsafe.SizeOf<Guid>());
            return ref Unsafe.As<byte, Guid>(ref MemoryMarshal.GetReference(data));
        }
    }

    public static ref readonly Guid IID_IDxcPdbUtils2
    {
        get
        {
            ReadOnlySpan<byte> data = [
                0x38, 0xD9, 0x15, 0x43,
                0x69, 0xF3,
                0x93, 0x4F,
                0x95,
                0xA2,
                0x25,
                0x20,
                0x17,
                0xCC,
                0x38,
                0x07
            ];

            Debug.Assert(data.Length == Unsafe.SizeOf<Guid>());
            return ref Unsafe.As<byte, Guid>(ref MemoryMarshal.GetReference(data));
        }
    }
}
