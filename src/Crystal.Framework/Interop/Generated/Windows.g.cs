#pragma warning disable CS0649
using System;
using System.Diagnostics;
using System.Diagnostics.CodeAnalysis;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using static Crystal.Interop.Windows;

namespace Crystal.Interop;

[StructLayout(LayoutKind.Explicit)]
internal partial struct LARGE_INTEGER
{
    [FieldOffset(0)]
    [NativeTypeName("__AnonymousRecord_winnt_L929_C5")]
    public _Anonymous_e__Struct Anonymous;

    [FieldOffset(0)]
    [NativeTypeName("__AnonymousRecord_winnt_L933_C5")]
    public _u_e__Struct u;

    [FieldOffset(0)]
    [NativeTypeName("LONGLONG")]
    public long QuadPart;

    [UnscopedRef]
    internal ref uint LowPart
    {
        get
        {
            return ref Anonymous.LowPart;
        }
    }

    [UnscopedRef]
    internal ref int HighPart
    {
        get
        {
            return ref Anonymous.HighPart;
        }
    }

    internal partial struct _Anonymous_e__Struct
    {
        [NativeTypeName("DWORD")]
        public uint LowPart;

        [NativeTypeName("LONG")]
        public int HighPart;
    }

    internal partial struct _u_e__Struct
    {
        [NativeTypeName("DWORD")]
        public uint LowPart;

        [NativeTypeName("LONG")]
        public int HighPart;
    }
}

[StructLayout(LayoutKind.Explicit)]
internal partial struct ULARGE_INTEGER
{
    [FieldOffset(0)]
    [NativeTypeName("__AnonymousRecord_winnt_L949_C5")]
    public _Anonymous_e__Struct Anonymous;

    [FieldOffset(0)]
    [NativeTypeName("__AnonymousRecord_winnt_L953_C5")]
    public _u_e__Struct u;

    [FieldOffset(0)]
    [NativeTypeName("ULONGLONG")]
    public ulong QuadPart;

    [UnscopedRef]
    internal ref uint LowPart
    {
        get
        {
            return ref Anonymous.LowPart;
        }
    }

    [UnscopedRef]
    internal ref uint HighPart
    {
        get
        {
            return ref Anonymous.HighPart;
        }
    }

    internal partial struct _Anonymous_e__Struct
    {
        [NativeTypeName("DWORD")]
        public uint LowPart;

        [NativeTypeName("DWORD")]
        public uint HighPart;
    }

    internal partial struct _u_e__Struct
    {
        [NativeTypeName("DWORD")]
        public uint LowPart;

        [NativeTypeName("DWORD")]
        public uint HighPart;
    }
}

internal partial struct LUID
{
    [NativeTypeName("DWORD")]
    public uint LowPart;

    [NativeTypeName("LONG")]
    public int HighPart;
}

internal partial struct HINSTANCE__
{
    public int unused;
}

internal partial struct FILETIME
{
    [NativeTypeName("DWORD")]
    public uint dwLowDateTime;

    [NativeTypeName("DWORD")]
    public uint dwHighDateTime;
}

internal partial struct HWND__
{
    public int unused;
}

internal partial struct HDC__
{
    public int unused;
}

internal partial struct HMONITOR__
{
    public int unused;
}

internal partial struct RECT
{
    [NativeTypeName("LONG")]
    public int left;

    [NativeTypeName("LONG")]
    public int top;

    [NativeTypeName("LONG")]
    public int right;

    [NativeTypeName("LONG")]
    public int bottom;
}

internal partial struct POINT
{
    [NativeTypeName("LONG")]
    public int x;

    [NativeTypeName("LONG")]
    public int y;
}

internal unsafe partial struct SECURITY_ATTRIBUTES
{
    [NativeTypeName("DWORD")]
    public uint nLength;

    [NativeTypeName("LPVOID")]
    public void* lpSecurityDescriptor;

    [NativeTypeName("BOOL")]
    public int bInheritHandle;
}

internal partial struct REASON_CONTEXT
{
    [NativeTypeName("ULONG")]
    public uint Version;

    [NativeTypeName("DWORD")]
    public uint Flags;

    [NativeTypeName("__AnonymousRecord_minwinbase_L276_C5")]
    public _Reason_e__Union Reason;

    [StructLayout(LayoutKind.Explicit)]
    internal unsafe partial struct _Reason_e__Union
    {
        [FieldOffset(0)]
        [NativeTypeName("__AnonymousRecord_minwinbase_L277_C9")]
        public _Detailed_e__Struct Detailed;

        [FieldOffset(0)]
        [NativeTypeName("LPWSTR")]
        public char* SimpleReasonString;

        internal unsafe partial struct _Detailed_e__Struct
        {
            [NativeTypeName("HMODULE")]
            public HINSTANCE__* LocalizedReasonModule;

            [NativeTypeName("ULONG")]
            public uint LocalizedReasonId;

            [NativeTypeName("ULONG")]
            public uint ReasonStringCount;

            [NativeTypeName("LPWSTR *")]
            public char** ReasonStrings;
        }
    }
}

[Guid("00000000-0000-0000-C000-000000000046")]
internal unsafe partial struct IUnknown : IUnknown.Interface, INativeGuid
{
    static Guid* INativeGuid.NativeGuid => (Guid*)Unsafe.AsPointer(in IID_IUnknown);

    public void** lpVtbl;

    [return: NativeTypeName("HRESULT")]
    public int QueryInterface([NativeTypeName("const IID &")] Guid* riid, void** ppvObject)
    {
        return ((delegate* unmanaged[Stdcall]<IUnknown*, Guid*, void**, int>)(lpVtbl[0]))((IUnknown*)Unsafe.AsPointer(ref this), riid, ppvObject);
    }

    [return: NativeTypeName("ULONG")]
    public uint AddRef()
    {
        return ((delegate* unmanaged[Stdcall]<IUnknown*, uint>)(lpVtbl[1]))((IUnknown*)Unsafe.AsPointer(ref this));
    }

    [return: NativeTypeName("ULONG")]
    public uint Release()
    {
        return ((delegate* unmanaged[Stdcall]<IUnknown*, uint>)(lpVtbl[2]))((IUnknown*)Unsafe.AsPointer(ref this));
    }

    public interface Interface
    {
        [return: NativeTypeName("HRESULT")]
        int QueryInterface([NativeTypeName("const IID &")] Guid* riid, void** ppvObject);

        [return: NativeTypeName("ULONG")]
        uint AddRef();

        [return: NativeTypeName("ULONG")]
        uint Release();
    }
}

[Guid("00000002-0000-0000-C000-000000000046")]
[NativeTypeName("struct IMalloc : IUnknown")]
internal unsafe partial struct IMalloc : IMalloc.Interface, INativeGuid
{
    static Guid* INativeGuid.NativeGuid => (Guid*)Unsafe.AsPointer(in IID_IMalloc);

    public void** lpVtbl;

    [return: NativeTypeName("HRESULT")]
    public int QueryInterface([NativeTypeName("const IID &")] Guid* riid, void** ppvObject)
    {
        return ((delegate* unmanaged[Stdcall]<IMalloc*, Guid*, void**, int>)(lpVtbl[0]))((IMalloc*)Unsafe.AsPointer(ref this), riid, ppvObject);
    }

    [return: NativeTypeName("ULONG")]
    public uint AddRef()
    {
        return ((delegate* unmanaged[Stdcall]<IMalloc*, uint>)(lpVtbl[1]))((IMalloc*)Unsafe.AsPointer(ref this));
    }

    [return: NativeTypeName("ULONG")]
    public uint Release()
    {
        return ((delegate* unmanaged[Stdcall]<IMalloc*, uint>)(lpVtbl[2]))((IMalloc*)Unsafe.AsPointer(ref this));
    }

    public void* Alloc([NativeTypeName("SIZE_T")] nuint cb)
    {
        return ((delegate* unmanaged[Stdcall]<IMalloc*, nuint, void*>)(lpVtbl[3]))((IMalloc*)Unsafe.AsPointer(ref this), cb);
    }

    public void* Realloc(void* pv, [NativeTypeName("SIZE_T")] nuint cb)
    {
        return ((delegate* unmanaged[Stdcall]<IMalloc*, void*, nuint, void*>)(lpVtbl[4]))((IMalloc*)Unsafe.AsPointer(ref this), pv, cb);
    }

    public void Free(void* pv)
    {
        ((delegate* unmanaged[Stdcall]<IMalloc*, void*, void>)(lpVtbl[5]))((IMalloc*)Unsafe.AsPointer(ref this), pv);
    }

    [return: NativeTypeName("SIZE_T")]
    public nuint GetSize(void* pv)
    {
        return ((delegate* unmanaged[Stdcall]<IMalloc*, void*, nuint>)(lpVtbl[6]))((IMalloc*)Unsafe.AsPointer(ref this), pv);
    }

    public int DidAlloc(void* pv)
    {
        return ((delegate* unmanaged[Stdcall]<IMalloc*, void*, int>)(lpVtbl[7]))((IMalloc*)Unsafe.AsPointer(ref this), pv);
    }

    public void HeapMinimize()
    {
        ((delegate* unmanaged[Stdcall]<IMalloc*, void>)(lpVtbl[8]))((IMalloc*)Unsafe.AsPointer(ref this));
    }

    public interface Interface : IUnknown.Interface
    {
        void* Alloc([NativeTypeName("SIZE_T")] nuint cb);

        void* Realloc(void* pv, [NativeTypeName("SIZE_T")] nuint cb);

        void Free(void* pv);

        [return: NativeTypeName("SIZE_T")]
        nuint GetSize(void* pv);

        int DidAlloc(void* pv);

        void HeapMinimize();
    }
}

[Guid("0C733A30-2A1C-11CE-ADE5-00AA0044773D")]
[NativeTypeName("struct ISequentialStream : IUnknown")]
internal unsafe partial struct ISequentialStream : ISequentialStream.Interface, INativeGuid
{
    static Guid* INativeGuid.NativeGuid => (Guid*)Unsafe.AsPointer(in IID_ISequentialStream);

    public void** lpVtbl;

    [return: NativeTypeName("HRESULT")]
    public int QueryInterface([NativeTypeName("const IID &")] Guid* riid, void** ppvObject)
    {
        return ((delegate* unmanaged[Stdcall]<ISequentialStream*, Guid*, void**, int>)(lpVtbl[0]))((ISequentialStream*)Unsafe.AsPointer(ref this), riid, ppvObject);
    }

    [return: NativeTypeName("ULONG")]
    public uint AddRef()
    {
        return ((delegate* unmanaged[Stdcall]<ISequentialStream*, uint>)(lpVtbl[1]))((ISequentialStream*)Unsafe.AsPointer(ref this));
    }

    [return: NativeTypeName("ULONG")]
    public uint Release()
    {
        return ((delegate* unmanaged[Stdcall]<ISequentialStream*, uint>)(lpVtbl[2]))((ISequentialStream*)Unsafe.AsPointer(ref this));
    }

    [return: NativeTypeName("HRESULT")]
    public int Read(void* pv, [NativeTypeName("ULONG")] uint cb, [NativeTypeName("ULONG *")] uint* pcbRead)
    {
        return ((delegate* unmanaged[Stdcall]<ISequentialStream*, void*, uint, uint*, int>)(lpVtbl[3]))((ISequentialStream*)Unsafe.AsPointer(ref this), pv, cb, pcbRead);
    }

    [return: NativeTypeName("HRESULT")]
    public int Write([NativeTypeName("const void *")] void* pv, [NativeTypeName("ULONG")] uint cb, [NativeTypeName("ULONG *")] uint* pcbWritten)
    {
        return ((delegate* unmanaged[Stdcall]<ISequentialStream*, void*, uint, uint*, int>)(lpVtbl[4]))((ISequentialStream*)Unsafe.AsPointer(ref this), pv, cb, pcbWritten);
    }

    public interface Interface : IUnknown.Interface
    {
        [return: NativeTypeName("HRESULT")]
        int Read(void* pv, [NativeTypeName("ULONG")] uint cb, [NativeTypeName("ULONG *")] uint* pcbRead);

        [return: NativeTypeName("HRESULT")]
        int Write([NativeTypeName("const void *")] void* pv, [NativeTypeName("ULONG")] uint cb, [NativeTypeName("ULONG *")] uint* pcbWritten);
    }
}

internal unsafe partial struct STATSTG
{
    [NativeTypeName("LPOLESTR")]
    public char* pwcsName;

    [NativeTypeName("DWORD")]
    public uint type;

    public ULARGE_INTEGER cbSize;

    public FILETIME mtime;

    public FILETIME ctime;

    public FILETIME atime;

    [NativeTypeName("DWORD")]
    public uint grfMode;

    [NativeTypeName("DWORD")]
    public uint grfLocksSupported;

    [NativeTypeName("CLSID")]
    public Guid clsid;

    [NativeTypeName("DWORD")]
    public uint grfStateBits;

    [NativeTypeName("DWORD")]
    public uint reserved;
}

[Guid("0000000C-0000-0000-C000-000000000046")]
[NativeTypeName("struct IStream : ISequentialStream")]
internal unsafe partial struct IStream : IStream.Interface, INativeGuid
{
    static Guid* INativeGuid.NativeGuid => (Guid*)Unsafe.AsPointer(in IID_IStream);

    public void** lpVtbl;

    [return: NativeTypeName("HRESULT")]
    public int QueryInterface([NativeTypeName("const IID &")] Guid* riid, void** ppvObject)
    {
        return ((delegate* unmanaged[Stdcall]<IStream*, Guid*, void**, int>)(lpVtbl[0]))((IStream*)Unsafe.AsPointer(ref this), riid, ppvObject);
    }

    [return: NativeTypeName("ULONG")]
    public uint AddRef()
    {
        return ((delegate* unmanaged[Stdcall]<IStream*, uint>)(lpVtbl[1]))((IStream*)Unsafe.AsPointer(ref this));
    }

    [return: NativeTypeName("ULONG")]
    public uint Release()
    {
        return ((delegate* unmanaged[Stdcall]<IStream*, uint>)(lpVtbl[2]))((IStream*)Unsafe.AsPointer(ref this));
    }

    [return: NativeTypeName("HRESULT")]
    public int Read(void* pv, [NativeTypeName("ULONG")] uint cb, [NativeTypeName("ULONG *")] uint* pcbRead)
    {
        return ((delegate* unmanaged[Stdcall]<IStream*, void*, uint, uint*, int>)(lpVtbl[3]))((IStream*)Unsafe.AsPointer(ref this), pv, cb, pcbRead);
    }

    [return: NativeTypeName("HRESULT")]
    public int Write([NativeTypeName("const void *")] void* pv, [NativeTypeName("ULONG")] uint cb, [NativeTypeName("ULONG *")] uint* pcbWritten)
    {
        return ((delegate* unmanaged[Stdcall]<IStream*, void*, uint, uint*, int>)(lpVtbl[4]))((IStream*)Unsafe.AsPointer(ref this), pv, cb, pcbWritten);
    }

    [return: NativeTypeName("HRESULT")]
    public int Seek(LARGE_INTEGER dlibMove, [NativeTypeName("DWORD")] uint dwOrigin, ULARGE_INTEGER* plibNewPosition)
    {
        return ((delegate* unmanaged[Stdcall]<IStream*, LARGE_INTEGER, uint, ULARGE_INTEGER*, int>)(lpVtbl[5]))((IStream*)Unsafe.AsPointer(ref this), dlibMove, dwOrigin, plibNewPosition);
    }

    [return: NativeTypeName("HRESULT")]
    public int SetSize(ULARGE_INTEGER libNewSize)
    {
        return ((delegate* unmanaged[Stdcall]<IStream*, ULARGE_INTEGER, int>)(lpVtbl[6]))((IStream*)Unsafe.AsPointer(ref this), libNewSize);
    }

    [return: NativeTypeName("HRESULT")]
    public int CopyTo(IStream* pstm, ULARGE_INTEGER cb, ULARGE_INTEGER* pcbRead, ULARGE_INTEGER* pcbWritten)
    {
        return ((delegate* unmanaged[Stdcall]<IStream*, IStream*, ULARGE_INTEGER, ULARGE_INTEGER*, ULARGE_INTEGER*, int>)(lpVtbl[7]))((IStream*)Unsafe.AsPointer(ref this), pstm, cb, pcbRead, pcbWritten);
    }

    [return: NativeTypeName("HRESULT")]
    public int Commit([NativeTypeName("DWORD")] uint grfCommitFlags)
    {
        return ((delegate* unmanaged[Stdcall]<IStream*, uint, int>)(lpVtbl[8]))((IStream*)Unsafe.AsPointer(ref this), grfCommitFlags);
    }

    [return: NativeTypeName("HRESULT")]
    public int Revert()
    {
        return ((delegate* unmanaged[Stdcall]<IStream*, int>)(lpVtbl[9]))((IStream*)Unsafe.AsPointer(ref this));
    }

    [return: NativeTypeName("HRESULT")]
    public int LockRegion(ULARGE_INTEGER libOffset, ULARGE_INTEGER cb, [NativeTypeName("DWORD")] uint dwLockType)
    {
        return ((delegate* unmanaged[Stdcall]<IStream*, ULARGE_INTEGER, ULARGE_INTEGER, uint, int>)(lpVtbl[10]))((IStream*)Unsafe.AsPointer(ref this), libOffset, cb, dwLockType);
    }

    [return: NativeTypeName("HRESULT")]
    public int UnlockRegion(ULARGE_INTEGER libOffset, ULARGE_INTEGER cb, [NativeTypeName("DWORD")] uint dwLockType)
    {
        return ((delegate* unmanaged[Stdcall]<IStream*, ULARGE_INTEGER, ULARGE_INTEGER, uint, int>)(lpVtbl[11]))((IStream*)Unsafe.AsPointer(ref this), libOffset, cb, dwLockType);
    }

    [return: NativeTypeName("HRESULT")]
    public int Stat(STATSTG* pstatstg, [NativeTypeName("DWORD")] uint grfStatFlag)
    {
        return ((delegate* unmanaged[Stdcall]<IStream*, STATSTG*, uint, int>)(lpVtbl[12]))((IStream*)Unsafe.AsPointer(ref this), pstatstg, grfStatFlag);
    }

    [return: NativeTypeName("HRESULT")]
    public int Clone(IStream** ppstm)
    {
        return ((delegate* unmanaged[Stdcall]<IStream*, IStream**, int>)(lpVtbl[13]))((IStream*)Unsafe.AsPointer(ref this), ppstm);
    }

    public interface Interface : ISequentialStream.Interface
    {
        [return: NativeTypeName("HRESULT")]
        int Seek(LARGE_INTEGER dlibMove, [NativeTypeName("DWORD")] uint dwOrigin, ULARGE_INTEGER* plibNewPosition);

        [return: NativeTypeName("HRESULT")]
        int SetSize(ULARGE_INTEGER libNewSize);

        [return: NativeTypeName("HRESULT")]
        int CopyTo(IStream* pstm, ULARGE_INTEGER cb, ULARGE_INTEGER* pcbRead, ULARGE_INTEGER* pcbWritten);

        [return: NativeTypeName("HRESULT")]
        int Commit([NativeTypeName("DWORD")] uint grfCommitFlags);

        [return: NativeTypeName("HRESULT")]
        int Revert();

        [return: NativeTypeName("HRESULT")]
        int LockRegion(ULARGE_INTEGER libOffset, ULARGE_INTEGER cb, [NativeTypeName("DWORD")] uint dwLockType);

        [return: NativeTypeName("HRESULT")]
        int UnlockRegion(ULARGE_INTEGER libOffset, ULARGE_INTEGER cb, [NativeTypeName("DWORD")] uint dwLockType);

        [return: NativeTypeName("HRESULT")]
        int Stat(STATSTG* pstatstg, [NativeTypeName("DWORD")] uint grfStatFlag);

        [return: NativeTypeName("HRESULT")]
        int Clone(IStream** ppstm);
    }
}

[StructLayout(LayoutKind.Sequential, Pack = 1)]
internal partial struct WAVEFORMAT
{
    [NativeTypeName("WORD")]
    public ushort wFormatTag;

    [NativeTypeName("WORD")]
    public ushort nChannels;

    [NativeTypeName("DWORD")]
    public uint nSamplesPerSec;

    [NativeTypeName("DWORD")]
    public uint nAvgBytesPerSec;

    [NativeTypeName("WORD")]
    public ushort nBlockAlign;
}

[StructLayout(LayoutKind.Sequential, Pack = 1)]
internal partial struct PCMWAVEFORMAT
{
    public WAVEFORMAT wf;

    [NativeTypeName("WORD")]
    public ushort wBitsPerSample;
}

[StructLayout(LayoutKind.Sequential, Pack = 1)]
internal partial struct WAVEFORMATEX
{
    [NativeTypeName("WORD")]
    public ushort wFormatTag;

    [NativeTypeName("WORD")]
    public ushort nChannels;

    [NativeTypeName("DWORD")]
    public uint nSamplesPerSec;

    [NativeTypeName("DWORD")]
    public uint nAvgBytesPerSec;

    [NativeTypeName("WORD")]
    public ushort nBlockAlign;

    [NativeTypeName("WORD")]
    public ushort wBitsPerSample;

    [NativeTypeName("WORD")]
    public ushort cbSize;
}

[Guid("00000001-0000-0010-8000-00AA00389B71")]
internal unsafe partial struct KSDATAFORMAT_SUBTYPE_PCM : INativeGuid
{
    static Guid* INativeGuid.NativeGuid => (Guid*)Unsafe.AsPointer(in IID_KSDATAFORMAT_SUBTYPE_PCM);
}

[Guid("00000003-0000-0010-8000-00AA00389B71")]
internal unsafe partial struct KSDATAFORMAT_SUBTYPE_IEEE_FLOAT : INativeGuid
{
    static Guid* INativeGuid.NativeGuid => (Guid*)Unsafe.AsPointer(in IID_KSDATAFORMAT_SUBTYPE_IEEE_FLOAT);
}

[StructLayout(LayoutKind.Sequential, Pack = 1)]
internal partial struct WAVEFORMATEXTENSIBLE
{
    public WAVEFORMATEX Format;

    [NativeTypeName("__AnonymousRecord_mmreg_L2525_C5")]
    public _Samples_e__Union Samples;

    [NativeTypeName("DWORD")]
    public uint dwChannelMask;

    public Guid SubFormat;

    [StructLayout(LayoutKind.Explicit, Pack = 1)]
    internal partial struct _Samples_e__Union
    {
        [FieldOffset(0)]
        [NativeTypeName("WORD")]
        public ushort wValidBitsPerSample;

        [FieldOffset(0)]
        [NativeTypeName("WORD")]
        public ushort wSamplesPerBlock;

        [FieldOffset(0)]
        [NativeTypeName("WORD")]
        public ushort wReserved;
    }
}

internal static unsafe partial class Windows
{
    [DllImport("kernel32", CallingConvention = CallingConvention.StdCall, ExactSpelling = true)]
    [return: NativeTypeName("BOOL")]
    public static extern int CloseHandle([NativeTypeName("HANDLE")] void* hObject);

    [DllImport("kernel32", CallingConvention = CallingConvention.StdCall, ExactSpelling = true)]
    [return: NativeTypeName("DWORD")]
    public static extern uint WaitForSingleObject([NativeTypeName("HANDLE")] void* hHandle, [NativeTypeName("DWORD")] uint dwMilliseconds);

    [DllImport("kernel32", CallingConvention = CallingConvention.StdCall, ExactSpelling = true)]
    [return: NativeTypeName("DWORD")]
    public static extern uint WaitForSingleObjectEx([NativeTypeName("HANDLE")] void* hHandle, [NativeTypeName("DWORD")] uint dwMilliseconds, [NativeTypeName("BOOL")] int bAlertable);

    [DllImport("kernel32", CallingConvention = CallingConvention.StdCall, ExactSpelling = true)]
    [return: NativeTypeName("BOOL")]
    public static extern int SetWaitableTimerEx([NativeTypeName("HANDLE")] void* hTimer, [NativeTypeName("const LARGE_INTEGER *")] LARGE_INTEGER* lpDueTime, [NativeTypeName("LONG")] int lPeriod, [NativeTypeName("PTIMERAPCROUTINE")] delegate* unmanaged[Stdcall]<void*, uint, uint, void> pfnCompletionRoutine, [NativeTypeName("LPVOID")] void* lpArgToCompletionRoutine, [NativeTypeName("PREASON_CONTEXT")] REASON_CONTEXT* WakeContext, [NativeTypeName("ULONG")] uint TolerableDelay);

    [DllImport("kernel32", CallingConvention = CallingConvention.StdCall, ExactSpelling = true)]
    [return: NativeTypeName("HANDLE")]
    public static extern void* CreateWaitableTimerExW([NativeTypeName("LPSECURITY_ATTRIBUTES")] SECURITY_ATTRIBUTES* lpTimerAttributes, [NativeTypeName("LPCWSTR")] char* lpTimerName, [NativeTypeName("DWORD")] uint dwFlags, [NativeTypeName("DWORD")] uint dwDesiredAccess);

    [DllImport("user32", CallingConvention = CallingConvention.StdCall, ExactSpelling = true)]
    [return: NativeTypeName("BOOL")]
    public static extern int ShowWindow([NativeTypeName("HWND")] HWND__* hWnd, int nCmdShow);

    [DllImport("user32", CallingConvention = CallingConvention.StdCall, ExactSpelling = true)]
    [return: NativeTypeName("BOOL")]
    public static extern int IsIconic([NativeTypeName("HWND")] HWND__* hWnd);

    [DllImport("user32", CallingConvention = CallingConvention.StdCall, ExactSpelling = true)]
    [return: NativeTypeName("BOOL")]
    public static extern int IsZoomed([NativeTypeName("HWND")] HWND__* hWnd);

    [DllImport("user32", CallingConvention = CallingConvention.StdCall, ExactSpelling = true)]
    public static extern short GetAsyncKeyState(int vKey);

    [DllImport("user32", CallingConvention = CallingConvention.StdCall, ExactSpelling = true)]
    [return: NativeTypeName("HWND")]
    public static extern HWND__* GetForegroundWindow();

    [DllImport("user32", CallingConvention = CallingConvention.StdCall, ExactSpelling = true)]
    [return: NativeTypeName("BOOL")]
    public static extern int GetClientRect([NativeTypeName("HWND")] HWND__* hWnd, [NativeTypeName("LPRECT")] RECT* lpRect);

    [DllImport("user32", CallingConvention = CallingConvention.StdCall, ExactSpelling = true)]
    [return: NativeTypeName("BOOL")]
    public static extern int GetCursorPos([NativeTypeName("LPPOINT")] POINT* lpPoint);

    [DllImport("user32", CallingConvention = CallingConvention.StdCall, ExactSpelling = true)]
    [return: NativeTypeName("BOOL")]
    public static extern int ScreenToClient([NativeTypeName("HWND")] HWND__* hWnd, [NativeTypeName("LPPOINT")] POINT* lpPoint);

    [NativeTypeName("#define FALSE 0")]
    public const int FALSE = 0;

    [NativeTypeName("#define TRUE 1")]
    public const int TRUE = 1;

    [NativeTypeName("#define TIMER_ALL_ACCESS (STANDARD_RIGHTS_REQUIRED|SYNCHRONIZE|\\\r\n                          TIMER_QUERY_STATE|TIMER_MODIFY_STATE)")]
    public const int TIMER_ALL_ACCESS = ((0x000F0000) | (0x00100000) | 0x0001 | 0x0002);

    [NativeTypeName("#define CREATE_WAITABLE_TIMER_HIGH_RESOLUTION 0x00000002")]
    public const int CREATE_WAITABLE_TIMER_HIGH_RESOLUTION = 0x00000002;

    [NativeTypeName("#define INFINITE 0xFFFFFFFF")]
    public const uint INFINITE = 0xFFFFFFFF;

    [NativeTypeName("#define DXGI_ERROR_INVALID_CALL _HRESULT_TYPEDEF_(0x887A0001L)")]
    public const int DXGI_ERROR_INVALID_CALL = unchecked((int)(0x887A0001));

    [NativeTypeName("#define DXGI_ERROR_NOT_FOUND _HRESULT_TYPEDEF_(0x887A0002L)")]
    public const int DXGI_ERROR_NOT_FOUND = unchecked((int)(0x887A0002));

    [NativeTypeName("#define DXGI_ERROR_MORE_DATA _HRESULT_TYPEDEF_(0x887A0003L)")]
    public const int DXGI_ERROR_MORE_DATA = unchecked((int)(0x887A0003));

    [NativeTypeName("#define DXGI_ERROR_UNSUPPORTED _HRESULT_TYPEDEF_(0x887A0004L)")]
    public const int DXGI_ERROR_UNSUPPORTED = unchecked((int)(0x887A0004));

    [NativeTypeName("#define DXGI_ERROR_DEVICE_REMOVED _HRESULT_TYPEDEF_(0x887A0005L)")]
    public const int DXGI_ERROR_DEVICE_REMOVED = unchecked((int)(0x887A0005));

    [NativeTypeName("#define DXGI_ERROR_DEVICE_HUNG _HRESULT_TYPEDEF_(0x887A0006L)")]
    public const int DXGI_ERROR_DEVICE_HUNG = unchecked((int)(0x887A0006));

    [NativeTypeName("#define DXGI_ERROR_DEVICE_RESET _HRESULT_TYPEDEF_(0x887A0007L)")]
    public const int DXGI_ERROR_DEVICE_RESET = unchecked((int)(0x887A0007));

    [NativeTypeName("#define DXGI_ERROR_WAS_STILL_DRAWING _HRESULT_TYPEDEF_(0x887A000AL)")]
    public const int DXGI_ERROR_WAS_STILL_DRAWING = unchecked((int)(0x887A000A));

    [NativeTypeName("#define DXGI_ERROR_FRAME_STATISTICS_DISJOINT _HRESULT_TYPEDEF_(0x887A000BL)")]
    public const int DXGI_ERROR_FRAME_STATISTICS_DISJOINT = unchecked((int)(0x887A000B));

    [NativeTypeName("#define DXGI_ERROR_GRAPHICS_VIDPN_SOURCE_IN_USE _HRESULT_TYPEDEF_(0x887A000CL)")]
    public const int DXGI_ERROR_GRAPHICS_VIDPN_SOURCE_IN_USE = unchecked((int)(0x887A000C));

    [NativeTypeName("#define DXGI_ERROR_DRIVER_INTERNAL_ERROR _HRESULT_TYPEDEF_(0x887A0020L)")]
    public const int DXGI_ERROR_DRIVER_INTERNAL_ERROR = unchecked((int)(0x887A0020));

    [NativeTypeName("#define DXGI_ERROR_NONEXCLUSIVE _HRESULT_TYPEDEF_(0x887A0021L)")]
    public const int DXGI_ERROR_NONEXCLUSIVE = unchecked((int)(0x887A0021));

    [NativeTypeName("#define DXGI_ERROR_NOT_CURRENTLY_AVAILABLE _HRESULT_TYPEDEF_(0x887A0022L)")]
    public const int DXGI_ERROR_NOT_CURRENTLY_AVAILABLE = unchecked((int)(0x887A0022));

    [NativeTypeName("#define DXGI_ERROR_REMOTE_CLIENT_DISCONNECTED _HRESULT_TYPEDEF_(0x887A0023L)")]
    public const int DXGI_ERROR_REMOTE_CLIENT_DISCONNECTED = unchecked((int)(0x887A0023));

    [NativeTypeName("#define DXGI_ERROR_REMOTE_OUTOFMEMORY _HRESULT_TYPEDEF_(0x887A0024L)")]
    public const int DXGI_ERROR_REMOTE_OUTOFMEMORY = unchecked((int)(0x887A0024));

    [NativeTypeName("#define DXGI_ERROR_ACCESS_LOST _HRESULT_TYPEDEF_(0x887A0026L)")]
    public const int DXGI_ERROR_ACCESS_LOST = unchecked((int)(0x887A0026));

    [NativeTypeName("#define DXGI_ERROR_WAIT_TIMEOUT _HRESULT_TYPEDEF_(0x887A0027L)")]
    public const int DXGI_ERROR_WAIT_TIMEOUT = unchecked((int)(0x887A0027));

    [NativeTypeName("#define DXGI_ERROR_SESSION_DISCONNECTED _HRESULT_TYPEDEF_(0x887A0028L)")]
    public const int DXGI_ERROR_SESSION_DISCONNECTED = unchecked((int)(0x887A0028));

    [NativeTypeName("#define DXGI_ERROR_RESTRICT_TO_OUTPUT_STALE _HRESULT_TYPEDEF_(0x887A0029L)")]
    public const int DXGI_ERROR_RESTRICT_TO_OUTPUT_STALE = unchecked((int)(0x887A0029));

    [NativeTypeName("#define DXGI_ERROR_CANNOT_PROTECT_CONTENT _HRESULT_TYPEDEF_(0x887A002AL)")]
    public const int DXGI_ERROR_CANNOT_PROTECT_CONTENT = unchecked((int)(0x887A002A));

    [NativeTypeName("#define DXGI_ERROR_ACCESS_DENIED _HRESULT_TYPEDEF_(0x887A002BL)")]
    public const int DXGI_ERROR_ACCESS_DENIED = unchecked((int)(0x887A002B));

    [NativeTypeName("#define DXGI_ERROR_NAME_ALREADY_EXISTS _HRESULT_TYPEDEF_(0x887A002CL)")]
    public const int DXGI_ERROR_NAME_ALREADY_EXISTS = unchecked((int)(0x887A002C));

    [NativeTypeName("#define DXGI_ERROR_SDK_COMPONENT_MISSING _HRESULT_TYPEDEF_(0x887A002DL)")]
    public const int DXGI_ERROR_SDK_COMPONENT_MISSING = unchecked((int)(0x887A002D));

    [NativeTypeName("#define DXGI_ERROR_NOT_CURRENT _HRESULT_TYPEDEF_(0x887A002EL)")]
    public const int DXGI_ERROR_NOT_CURRENT = unchecked((int)(0x887A002E));

    [NativeTypeName("#define DXGI_ERROR_HW_PROTECTION_OUTOFMEMORY _HRESULT_TYPEDEF_(0x887A0030L)")]
    public const int DXGI_ERROR_HW_PROTECTION_OUTOFMEMORY = unchecked((int)(0x887A0030));

    [NativeTypeName("#define DXGI_ERROR_DYNAMIC_CODE_POLICY_VIOLATION _HRESULT_TYPEDEF_(0x887A0031L)")]
    public const int DXGI_ERROR_DYNAMIC_CODE_POLICY_VIOLATION = unchecked((int)(0x887A0031));

    [NativeTypeName("#define DXGI_ERROR_NON_COMPOSITED_UI _HRESULT_TYPEDEF_(0x887A0032L)")]
    public const int DXGI_ERROR_NON_COMPOSITED_UI = unchecked((int)(0x887A0032));

    [NativeTypeName("#define DXGI_ERROR_MODE_CHANGE_IN_PROGRESS _HRESULT_TYPEDEF_(0x887A0025L)")]
    public const int DXGI_ERROR_MODE_CHANGE_IN_PROGRESS = unchecked((int)(0x887A0025));

    [NativeTypeName("#define DXGI_ERROR_CACHE_CORRUPT _HRESULT_TYPEDEF_(0x887A0033L)")]
    public const int DXGI_ERROR_CACHE_CORRUPT = unchecked((int)(0x887A0033));

    [NativeTypeName("#define DXGI_ERROR_CACHE_FULL _HRESULT_TYPEDEF_(0x887A0034L)")]
    public const int DXGI_ERROR_CACHE_FULL = unchecked((int)(0x887A0034));

    [NativeTypeName("#define DXGI_ERROR_CACHE_HASH_COLLISION _HRESULT_TYPEDEF_(0x887A0035L)")]
    public const int DXGI_ERROR_CACHE_HASH_COLLISION = unchecked((int)(0x887A0035));

    [NativeTypeName("#define DXGI_ERROR_ALREADY_EXISTS _HRESULT_TYPEDEF_(0x887A0036L)")]
    public const int DXGI_ERROR_ALREADY_EXISTS = unchecked((int)(0x887A0036));

    [NativeTypeName("#define DXGI_ERROR_MPO_UNPINNED _HRESULT_TYPEDEF_(0x887A0064L)")]
    public const int DXGI_ERROR_MPO_UNPINNED = unchecked((int)(0x887A0064));

    [NativeTypeName("#define DXGI_ERROR_SETDISPLAYMODE_REQUIRED _HRESULT_TYPEDEF_(0x887A0065L)")]
    public const int DXGI_ERROR_SETDISPLAYMODE_REQUIRED = unchecked((int)(0x887A0065));

    [NativeTypeName("#define SW_HIDE 0")]
    public const int SW_HIDE = 0;

    [NativeTypeName("#define SW_SHOWNORMAL 1")]
    public const int SW_SHOWNORMAL = 1;

    [NativeTypeName("#define SW_NORMAL 1")]
    public const int SW_NORMAL = 1;

    [NativeTypeName("#define SW_SHOWMINIMIZED 2")]
    public const int SW_SHOWMINIMIZED = 2;

    [NativeTypeName("#define SW_SHOWMAXIMIZED 3")]
    public const int SW_SHOWMAXIMIZED = 3;

    [NativeTypeName("#define SW_MAXIMIZE 3")]
    public const int SW_MAXIMIZE = 3;

    [NativeTypeName("#define SW_SHOWNOACTIVATE 4")]
    public const int SW_SHOWNOACTIVATE = 4;

    [NativeTypeName("#define SW_SHOW 5")]
    public const int SW_SHOW = 5;

    [NativeTypeName("#define SW_MINIMIZE 6")]
    public const int SW_MINIMIZE = 6;

    [NativeTypeName("#define SW_SHOWMINNOACTIVE 7")]
    public const int SW_SHOWMINNOACTIVE = 7;

    [NativeTypeName("#define SW_SHOWNA 8")]
    public const int SW_SHOWNA = 8;

    [NativeTypeName("#define SW_RESTORE 9")]
    public const int SW_RESTORE = 9;

    [NativeTypeName("#define SW_SHOWDEFAULT 10")]
    public const int SW_SHOWDEFAULT = 10;

    [NativeTypeName("#define SW_FORCEMINIMIZE 11")]
    public const int SW_FORCEMINIMIZE = 11;

    [NativeTypeName("#define SW_MAX 11")]
    public const int SW_MAX = 11;

    [NativeTypeName("#define SW_PARENTCLOSING 1")]
    public const int SW_PARENTCLOSING = 1;

    [NativeTypeName("#define SW_OTHERZOOM 2")]
    public const int SW_OTHERZOOM = 2;

    [NativeTypeName("#define SW_PARENTOPENING 3")]
    public const int SW_PARENTOPENING = 3;

    [NativeTypeName("#define SW_OTHERUNZOOM 4")]
    public const int SW_OTHERUNZOOM = 4;

    [NativeTypeName("#define VK_LBUTTON 0x01")]
    public const int VK_LBUTTON = 0x01;

    [NativeTypeName("#define VK_RBUTTON 0x02")]
    public const int VK_RBUTTON = 0x02;

    [NativeTypeName("#define VK_CANCEL 0x03")]
    public const int VK_CANCEL = 0x03;

    [NativeTypeName("#define VK_MBUTTON 0x04")]
    public const int VK_MBUTTON = 0x04;

    [NativeTypeName("#define VK_XBUTTON1 0x05")]
    public const int VK_XBUTTON1 = 0x05;

    [NativeTypeName("#define VK_XBUTTON2 0x06")]
    public const int VK_XBUTTON2 = 0x06;

    [NativeTypeName("#define VK_BACK 0x08")]
    public const int VK_BACK = 0x08;

    [NativeTypeName("#define VK_TAB 0x09")]
    public const int VK_TAB = 0x09;

    [NativeTypeName("#define VK_CLEAR 0x0C")]
    public const int VK_CLEAR = 0x0C;

    [NativeTypeName("#define VK_RETURN 0x0D")]
    public const int VK_RETURN = 0x0D;

    [NativeTypeName("#define VK_SHIFT 0x10")]
    public const int VK_SHIFT = 0x10;

    [NativeTypeName("#define VK_CONTROL 0x11")]
    public const int VK_CONTROL = 0x11;

    [NativeTypeName("#define VK_MENU 0x12")]
    public const int VK_MENU = 0x12;

    [NativeTypeName("#define VK_PAUSE 0x13")]
    public const int VK_PAUSE = 0x13;

    [NativeTypeName("#define VK_CAPITAL 0x14")]
    public const int VK_CAPITAL = 0x14;

    [NativeTypeName("#define VK_KANA 0x15")]
    public const int VK_KANA = 0x15;

    [NativeTypeName("#define VK_HANGEUL 0x15")]
    public const int VK_HANGEUL = 0x15;

    [NativeTypeName("#define VK_HANGUL 0x15")]
    public const int VK_HANGUL = 0x15;

    [NativeTypeName("#define VK_IME_ON 0x16")]
    public const int VK_IME_ON = 0x16;

    [NativeTypeName("#define VK_JUNJA 0x17")]
    public const int VK_JUNJA = 0x17;

    [NativeTypeName("#define VK_FINAL 0x18")]
    public const int VK_FINAL = 0x18;

    [NativeTypeName("#define VK_HANJA 0x19")]
    public const int VK_HANJA = 0x19;

    [NativeTypeName("#define VK_KANJI 0x19")]
    public const int VK_KANJI = 0x19;

    [NativeTypeName("#define VK_IME_OFF 0x1A")]
    public const int VK_IME_OFF = 0x1A;

    [NativeTypeName("#define VK_ESCAPE 0x1B")]
    public const int VK_ESCAPE = 0x1B;

    [NativeTypeName("#define VK_CONVERT 0x1C")]
    public const int VK_CONVERT = 0x1C;

    [NativeTypeName("#define VK_NONCONVERT 0x1D")]
    public const int VK_NONCONVERT = 0x1D;

    [NativeTypeName("#define VK_ACCEPT 0x1E")]
    public const int VK_ACCEPT = 0x1E;

    [NativeTypeName("#define VK_MODECHANGE 0x1F")]
    public const int VK_MODECHANGE = 0x1F;

    [NativeTypeName("#define VK_SPACE 0x20")]
    public const int VK_SPACE = 0x20;

    [NativeTypeName("#define VK_PRIOR 0x21")]
    public const int VK_PRIOR = 0x21;

    [NativeTypeName("#define VK_NEXT 0x22")]
    public const int VK_NEXT = 0x22;

    [NativeTypeName("#define VK_END 0x23")]
    public const int VK_END = 0x23;

    [NativeTypeName("#define VK_HOME 0x24")]
    public const int VK_HOME = 0x24;

    [NativeTypeName("#define VK_LEFT 0x25")]
    public const int VK_LEFT = 0x25;

    [NativeTypeName("#define VK_UP 0x26")]
    public const int VK_UP = 0x26;

    [NativeTypeName("#define VK_RIGHT 0x27")]
    public const int VK_RIGHT = 0x27;

    [NativeTypeName("#define VK_DOWN 0x28")]
    public const int VK_DOWN = 0x28;

    [NativeTypeName("#define VK_SELECT 0x29")]
    public const int VK_SELECT = 0x29;

    [NativeTypeName("#define VK_PRINT 0x2A")]
    public const int VK_PRINT = 0x2A;

    [NativeTypeName("#define VK_EXECUTE 0x2B")]
    public const int VK_EXECUTE = 0x2B;

    [NativeTypeName("#define VK_SNAPSHOT 0x2C")]
    public const int VK_SNAPSHOT = 0x2C;

    [NativeTypeName("#define VK_INSERT 0x2D")]
    public const int VK_INSERT = 0x2D;

    [NativeTypeName("#define VK_DELETE 0x2E")]
    public const int VK_DELETE = 0x2E;

    [NativeTypeName("#define VK_HELP 0x2F")]
    public const int VK_HELP = 0x2F;

    [NativeTypeName("#define VK_LWIN 0x5B")]
    public const int VK_LWIN = 0x5B;

    [NativeTypeName("#define VK_RWIN 0x5C")]
    public const int VK_RWIN = 0x5C;

    [NativeTypeName("#define VK_APPS 0x5D")]
    public const int VK_APPS = 0x5D;

    [NativeTypeName("#define VK_SLEEP 0x5F")]
    public const int VK_SLEEP = 0x5F;

    [NativeTypeName("#define VK_NUMPAD0 0x60")]
    public const int VK_NUMPAD0 = 0x60;

    [NativeTypeName("#define VK_NUMPAD1 0x61")]
    public const int VK_NUMPAD1 = 0x61;

    [NativeTypeName("#define VK_NUMPAD2 0x62")]
    public const int VK_NUMPAD2 = 0x62;

    [NativeTypeName("#define VK_NUMPAD3 0x63")]
    public const int VK_NUMPAD3 = 0x63;

    [NativeTypeName("#define VK_NUMPAD4 0x64")]
    public const int VK_NUMPAD4 = 0x64;

    [NativeTypeName("#define VK_NUMPAD5 0x65")]
    public const int VK_NUMPAD5 = 0x65;

    [NativeTypeName("#define VK_NUMPAD6 0x66")]
    public const int VK_NUMPAD6 = 0x66;

    [NativeTypeName("#define VK_NUMPAD7 0x67")]
    public const int VK_NUMPAD7 = 0x67;

    [NativeTypeName("#define VK_NUMPAD8 0x68")]
    public const int VK_NUMPAD8 = 0x68;

    [NativeTypeName("#define VK_NUMPAD9 0x69")]
    public const int VK_NUMPAD9 = 0x69;

    [NativeTypeName("#define VK_MULTIPLY 0x6A")]
    public const int VK_MULTIPLY = 0x6A;

    [NativeTypeName("#define VK_ADD 0x6B")]
    public const int VK_ADD = 0x6B;

    [NativeTypeName("#define VK_SEPARATOR 0x6C")]
    public const int VK_SEPARATOR = 0x6C;

    [NativeTypeName("#define VK_SUBTRACT 0x6D")]
    public const int VK_SUBTRACT = 0x6D;

    [NativeTypeName("#define VK_DECIMAL 0x6E")]
    public const int VK_DECIMAL = 0x6E;

    [NativeTypeName("#define VK_DIVIDE 0x6F")]
    public const int VK_DIVIDE = 0x6F;

    [NativeTypeName("#define VK_F1 0x70")]
    public const int VK_F1 = 0x70;

    [NativeTypeName("#define VK_F2 0x71")]
    public const int VK_F2 = 0x71;

    [NativeTypeName("#define VK_F3 0x72")]
    public const int VK_F3 = 0x72;

    [NativeTypeName("#define VK_F4 0x73")]
    public const int VK_F4 = 0x73;

    [NativeTypeName("#define VK_F5 0x74")]
    public const int VK_F5 = 0x74;

    [NativeTypeName("#define VK_F6 0x75")]
    public const int VK_F6 = 0x75;

    [NativeTypeName("#define VK_F7 0x76")]
    public const int VK_F7 = 0x76;

    [NativeTypeName("#define VK_F8 0x77")]
    public const int VK_F8 = 0x77;

    [NativeTypeName("#define VK_F9 0x78")]
    public const int VK_F9 = 0x78;

    [NativeTypeName("#define VK_F10 0x79")]
    public const int VK_F10 = 0x79;

    [NativeTypeName("#define VK_F11 0x7A")]
    public const int VK_F11 = 0x7A;

    [NativeTypeName("#define VK_F12 0x7B")]
    public const int VK_F12 = 0x7B;

    [NativeTypeName("#define VK_F13 0x7C")]
    public const int VK_F13 = 0x7C;

    [NativeTypeName("#define VK_F14 0x7D")]
    public const int VK_F14 = 0x7D;

    [NativeTypeName("#define VK_F15 0x7E")]
    public const int VK_F15 = 0x7E;

    [NativeTypeName("#define VK_F16 0x7F")]
    public const int VK_F16 = 0x7F;

    [NativeTypeName("#define VK_F17 0x80")]
    public const int VK_F17 = 0x80;

    [NativeTypeName("#define VK_F18 0x81")]
    public const int VK_F18 = 0x81;

    [NativeTypeName("#define VK_F19 0x82")]
    public const int VK_F19 = 0x82;

    [NativeTypeName("#define VK_F20 0x83")]
    public const int VK_F20 = 0x83;

    [NativeTypeName("#define VK_F21 0x84")]
    public const int VK_F21 = 0x84;

    [NativeTypeName("#define VK_F22 0x85")]
    public const int VK_F22 = 0x85;

    [NativeTypeName("#define VK_F23 0x86")]
    public const int VK_F23 = 0x86;

    [NativeTypeName("#define VK_F24 0x87")]
    public const int VK_F24 = 0x87;

    [NativeTypeName("#define VK_NAVIGATION_VIEW 0x88")]
    public const int VK_NAVIGATION_VIEW = 0x88;

    [NativeTypeName("#define VK_NAVIGATION_MENU 0x89")]
    public const int VK_NAVIGATION_MENU = 0x89;

    [NativeTypeName("#define VK_NAVIGATION_UP 0x8A")]
    public const int VK_NAVIGATION_UP = 0x8A;

    [NativeTypeName("#define VK_NAVIGATION_DOWN 0x8B")]
    public const int VK_NAVIGATION_DOWN = 0x8B;

    [NativeTypeName("#define VK_NAVIGATION_LEFT 0x8C")]
    public const int VK_NAVIGATION_LEFT = 0x8C;

    [NativeTypeName("#define VK_NAVIGATION_RIGHT 0x8D")]
    public const int VK_NAVIGATION_RIGHT = 0x8D;

    [NativeTypeName("#define VK_NAVIGATION_ACCEPT 0x8E")]
    public const int VK_NAVIGATION_ACCEPT = 0x8E;

    [NativeTypeName("#define VK_NAVIGATION_CANCEL 0x8F")]
    public const int VK_NAVIGATION_CANCEL = 0x8F;

    [NativeTypeName("#define VK_NUMLOCK 0x90")]
    public const int VK_NUMLOCK = 0x90;

    [NativeTypeName("#define VK_SCROLL 0x91")]
    public const int VK_SCROLL = 0x91;

    [NativeTypeName("#define VK_OEM_NEC_EQUAL 0x92")]
    public const int VK_OEM_NEC_EQUAL = 0x92;

    [NativeTypeName("#define VK_OEM_FJ_JISHO 0x92")]
    public const int VK_OEM_FJ_JISHO = 0x92;

    [NativeTypeName("#define VK_OEM_FJ_MASSHOU 0x93")]
    public const int VK_OEM_FJ_MASSHOU = 0x93;

    [NativeTypeName("#define VK_OEM_FJ_TOUROKU 0x94")]
    public const int VK_OEM_FJ_TOUROKU = 0x94;

    [NativeTypeName("#define VK_OEM_FJ_LOYA 0x95")]
    public const int VK_OEM_FJ_LOYA = 0x95;

    [NativeTypeName("#define VK_OEM_FJ_ROYA 0x96")]
    public const int VK_OEM_FJ_ROYA = 0x96;

    [NativeTypeName("#define VK_LSHIFT 0xA0")]
    public const int VK_LSHIFT = 0xA0;

    [NativeTypeName("#define VK_RSHIFT 0xA1")]
    public const int VK_RSHIFT = 0xA1;

    [NativeTypeName("#define VK_LCONTROL 0xA2")]
    public const int VK_LCONTROL = 0xA2;

    [NativeTypeName("#define VK_RCONTROL 0xA3")]
    public const int VK_RCONTROL = 0xA3;

    [NativeTypeName("#define VK_LMENU 0xA4")]
    public const int VK_LMENU = 0xA4;

    [NativeTypeName("#define VK_RMENU 0xA5")]
    public const int VK_RMENU = 0xA5;

    [NativeTypeName("#define VK_BROWSER_BACK 0xA6")]
    public const int VK_BROWSER_BACK = 0xA6;

    [NativeTypeName("#define VK_BROWSER_FORWARD 0xA7")]
    public const int VK_BROWSER_FORWARD = 0xA7;

    [NativeTypeName("#define VK_BROWSER_REFRESH 0xA8")]
    public const int VK_BROWSER_REFRESH = 0xA8;

    [NativeTypeName("#define VK_BROWSER_STOP 0xA9")]
    public const int VK_BROWSER_STOP = 0xA9;

    [NativeTypeName("#define VK_BROWSER_SEARCH 0xAA")]
    public const int VK_BROWSER_SEARCH = 0xAA;

    [NativeTypeName("#define VK_BROWSER_FAVORITES 0xAB")]
    public const int VK_BROWSER_FAVORITES = 0xAB;

    [NativeTypeName("#define VK_BROWSER_HOME 0xAC")]
    public const int VK_BROWSER_HOME = 0xAC;

    [NativeTypeName("#define VK_VOLUME_MUTE 0xAD")]
    public const int VK_VOLUME_MUTE = 0xAD;

    [NativeTypeName("#define VK_VOLUME_DOWN 0xAE")]
    public const int VK_VOLUME_DOWN = 0xAE;

    [NativeTypeName("#define VK_VOLUME_UP 0xAF")]
    public const int VK_VOLUME_UP = 0xAF;

    [NativeTypeName("#define VK_MEDIA_NEXT_TRACK 0xB0")]
    public const int VK_MEDIA_NEXT_TRACK = 0xB0;

    [NativeTypeName("#define VK_MEDIA_PREV_TRACK 0xB1")]
    public const int VK_MEDIA_PREV_TRACK = 0xB1;

    [NativeTypeName("#define VK_MEDIA_STOP 0xB2")]
    public const int VK_MEDIA_STOP = 0xB2;

    [NativeTypeName("#define VK_MEDIA_PLAY_PAUSE 0xB3")]
    public const int VK_MEDIA_PLAY_PAUSE = 0xB3;

    [NativeTypeName("#define VK_LAUNCH_MAIL 0xB4")]
    public const int VK_LAUNCH_MAIL = 0xB4;

    [NativeTypeName("#define VK_LAUNCH_MEDIA_SELECT 0xB5")]
    public const int VK_LAUNCH_MEDIA_SELECT = 0xB5;

    [NativeTypeName("#define VK_LAUNCH_APP1 0xB6")]
    public const int VK_LAUNCH_APP1 = 0xB6;

    [NativeTypeName("#define VK_LAUNCH_APP2 0xB7")]
    public const int VK_LAUNCH_APP2 = 0xB7;

    [NativeTypeName("#define VK_OEM_1 0xBA")]
    public const int VK_OEM_1 = 0xBA;

    [NativeTypeName("#define VK_OEM_PLUS 0xBB")]
    public const int VK_OEM_PLUS = 0xBB;

    [NativeTypeName("#define VK_OEM_COMMA 0xBC")]
    public const int VK_OEM_COMMA = 0xBC;

    [NativeTypeName("#define VK_OEM_MINUS 0xBD")]
    public const int VK_OEM_MINUS = 0xBD;

    [NativeTypeName("#define VK_OEM_PERIOD 0xBE")]
    public const int VK_OEM_PERIOD = 0xBE;

    [NativeTypeName("#define VK_OEM_2 0xBF")]
    public const int VK_OEM_2 = 0xBF;

    [NativeTypeName("#define VK_OEM_3 0xC0")]
    public const int VK_OEM_3 = 0xC0;

    [NativeTypeName("#define VK_GAMEPAD_A 0xC3")]
    public const int VK_GAMEPAD_A = 0xC3;

    [NativeTypeName("#define VK_GAMEPAD_B 0xC4")]
    public const int VK_GAMEPAD_B = 0xC4;

    [NativeTypeName("#define VK_GAMEPAD_X 0xC5")]
    public const int VK_GAMEPAD_X = 0xC5;

    [NativeTypeName("#define VK_GAMEPAD_Y 0xC6")]
    public const int VK_GAMEPAD_Y = 0xC6;

    [NativeTypeName("#define VK_GAMEPAD_RIGHT_SHOULDER 0xC7")]
    public const int VK_GAMEPAD_RIGHT_SHOULDER = 0xC7;

    [NativeTypeName("#define VK_GAMEPAD_LEFT_SHOULDER 0xC8")]
    public const int VK_GAMEPAD_LEFT_SHOULDER = 0xC8;

    [NativeTypeName("#define VK_GAMEPAD_LEFT_TRIGGER 0xC9")]
    public const int VK_GAMEPAD_LEFT_TRIGGER = 0xC9;

    [NativeTypeName("#define VK_GAMEPAD_RIGHT_TRIGGER 0xCA")]
    public const int VK_GAMEPAD_RIGHT_TRIGGER = 0xCA;

    [NativeTypeName("#define VK_GAMEPAD_DPAD_UP 0xCB")]
    public const int VK_GAMEPAD_DPAD_UP = 0xCB;

    [NativeTypeName("#define VK_GAMEPAD_DPAD_DOWN 0xCC")]
    public const int VK_GAMEPAD_DPAD_DOWN = 0xCC;

    [NativeTypeName("#define VK_GAMEPAD_DPAD_LEFT 0xCD")]
    public const int VK_GAMEPAD_DPAD_LEFT = 0xCD;

    [NativeTypeName("#define VK_GAMEPAD_DPAD_RIGHT 0xCE")]
    public const int VK_GAMEPAD_DPAD_RIGHT = 0xCE;

    [NativeTypeName("#define VK_GAMEPAD_MENU 0xCF")]
    public const int VK_GAMEPAD_MENU = 0xCF;

    [NativeTypeName("#define VK_GAMEPAD_VIEW 0xD0")]
    public const int VK_GAMEPAD_VIEW = 0xD0;

    [NativeTypeName("#define VK_GAMEPAD_LEFT_THUMBSTICK_BUTTON 0xD1")]
    public const int VK_GAMEPAD_LEFT_THUMBSTICK_BUTTON = 0xD1;

    [NativeTypeName("#define VK_GAMEPAD_RIGHT_THUMBSTICK_BUTTON 0xD2")]
    public const int VK_GAMEPAD_RIGHT_THUMBSTICK_BUTTON = 0xD2;

    [NativeTypeName("#define VK_GAMEPAD_LEFT_THUMBSTICK_UP 0xD3")]
    public const int VK_GAMEPAD_LEFT_THUMBSTICK_UP = 0xD3;

    [NativeTypeName("#define VK_GAMEPAD_LEFT_THUMBSTICK_DOWN 0xD4")]
    public const int VK_GAMEPAD_LEFT_THUMBSTICK_DOWN = 0xD4;

    [NativeTypeName("#define VK_GAMEPAD_LEFT_THUMBSTICK_RIGHT 0xD5")]
    public const int VK_GAMEPAD_LEFT_THUMBSTICK_RIGHT = 0xD5;

    [NativeTypeName("#define VK_GAMEPAD_LEFT_THUMBSTICK_LEFT 0xD6")]
    public const int VK_GAMEPAD_LEFT_THUMBSTICK_LEFT = 0xD6;

    [NativeTypeName("#define VK_GAMEPAD_RIGHT_THUMBSTICK_UP 0xD7")]
    public const int VK_GAMEPAD_RIGHT_THUMBSTICK_UP = 0xD7;

    [NativeTypeName("#define VK_GAMEPAD_RIGHT_THUMBSTICK_DOWN 0xD8")]
    public const int VK_GAMEPAD_RIGHT_THUMBSTICK_DOWN = 0xD8;

    [NativeTypeName("#define VK_GAMEPAD_RIGHT_THUMBSTICK_RIGHT 0xD9")]
    public const int VK_GAMEPAD_RIGHT_THUMBSTICK_RIGHT = 0xD9;

    [NativeTypeName("#define VK_GAMEPAD_RIGHT_THUMBSTICK_LEFT 0xDA")]
    public const int VK_GAMEPAD_RIGHT_THUMBSTICK_LEFT = 0xDA;

    [NativeTypeName("#define VK_OEM_4 0xDB")]
    public const int VK_OEM_4 = 0xDB;

    [NativeTypeName("#define VK_OEM_5 0xDC")]
    public const int VK_OEM_5 = 0xDC;

    [NativeTypeName("#define VK_OEM_6 0xDD")]
    public const int VK_OEM_6 = 0xDD;

    [NativeTypeName("#define VK_OEM_7 0xDE")]
    public const int VK_OEM_7 = 0xDE;

    [NativeTypeName("#define VK_OEM_8 0xDF")]
    public const int VK_OEM_8 = 0xDF;

    [NativeTypeName("#define VK_OEM_AX 0xE1")]
    public const int VK_OEM_AX = 0xE1;

    [NativeTypeName("#define VK_OEM_102 0xE2")]
    public const int VK_OEM_102 = 0xE2;

    [NativeTypeName("#define VK_ICO_HELP 0xE3")]
    public const int VK_ICO_HELP = 0xE3;

    [NativeTypeName("#define VK_ICO_00 0xE4")]
    public const int VK_ICO_00 = 0xE4;

    [NativeTypeName("#define VK_PROCESSKEY 0xE5")]
    public const int VK_PROCESSKEY = 0xE5;

    [NativeTypeName("#define VK_ICO_CLEAR 0xE6")]
    public const int VK_ICO_CLEAR = 0xE6;

    [NativeTypeName("#define VK_PACKET 0xE7")]
    public const int VK_PACKET = 0xE7;

    [NativeTypeName("#define VK_OEM_RESET 0xE9")]
    public const int VK_OEM_RESET = 0xE9;

    [NativeTypeName("#define VK_OEM_JUMP 0xEA")]
    public const int VK_OEM_JUMP = 0xEA;

    [NativeTypeName("#define VK_OEM_PA1 0xEB")]
    public const int VK_OEM_PA1 = 0xEB;

    [NativeTypeName("#define VK_OEM_PA2 0xEC")]
    public const int VK_OEM_PA2 = 0xEC;

    [NativeTypeName("#define VK_OEM_PA3 0xED")]
    public const int VK_OEM_PA3 = 0xED;

    [NativeTypeName("#define VK_OEM_WSCTRL 0xEE")]
    public const int VK_OEM_WSCTRL = 0xEE;

    [NativeTypeName("#define VK_OEM_CUSEL 0xEF")]
    public const int VK_OEM_CUSEL = 0xEF;

    [NativeTypeName("#define VK_OEM_ATTN 0xF0")]
    public const int VK_OEM_ATTN = 0xF0;

    [NativeTypeName("#define VK_OEM_FINISH 0xF1")]
    public const int VK_OEM_FINISH = 0xF1;

    [NativeTypeName("#define VK_OEM_COPY 0xF2")]
    public const int VK_OEM_COPY = 0xF2;

    [NativeTypeName("#define VK_OEM_AUTO 0xF3")]
    public const int VK_OEM_AUTO = 0xF3;

    [NativeTypeName("#define VK_OEM_ENLW 0xF4")]
    public const int VK_OEM_ENLW = 0xF4;

    [NativeTypeName("#define VK_OEM_BACKTAB 0xF5")]
    public const int VK_OEM_BACKTAB = 0xF5;

    [NativeTypeName("#define VK_ATTN 0xF6")]
    public const int VK_ATTN = 0xF6;

    [NativeTypeName("#define VK_CRSEL 0xF7")]
    public const int VK_CRSEL = 0xF7;

    [NativeTypeName("#define VK_EXSEL 0xF8")]
    public const int VK_EXSEL = 0xF8;

    [NativeTypeName("#define VK_EREOF 0xF9")]
    public const int VK_EREOF = 0xF9;

    [NativeTypeName("#define VK_PLAY 0xFA")]
    public const int VK_PLAY = 0xFA;

    [NativeTypeName("#define VK_ZOOM 0xFB")]
    public const int VK_ZOOM = 0xFB;

    [NativeTypeName("#define VK_NONAME 0xFC")]
    public const int VK_NONAME = 0xFC;

    [NativeTypeName("#define VK_PA1 0xFD")]
    public const int VK_PA1 = 0xFD;

    [NativeTypeName("#define VK_OEM_CLEAR 0xFE")]
    public const int VK_OEM_CLEAR = 0xFE;

    [NativeTypeName("#define WHEEL_DELTA 120")]
    public const int WHEEL_DELTA = 120;

    [NativeTypeName("#define SW_SCROLLCHILDREN 0x0001")]
    public const int SW_SCROLLCHILDREN = 0x0001;

    [NativeTypeName("#define SW_INVALIDATE 0x0002")]
    public const int SW_INVALIDATE = 0x0002;

    [NativeTypeName("#define SW_ERASE 0x0004")]
    public const int SW_ERASE = 0x0004;

    [NativeTypeName("#define SW_SMOOTHSCROLL 0x0010")]
    public const int SW_SMOOTHSCROLL = 0x0010;

    [NativeTypeName("#define WAVE_FORMAT_IEEE_FLOAT 0x0003")]
    public const int WAVE_FORMAT_IEEE_FLOAT = 0x0003;

    [NativeTypeName("#define WAVE_FORMAT_EXTENSIBLE 0xFFFE")]
    public const int WAVE_FORMAT_EXTENSIBLE = 0xFFFE;

    [NativeTypeName("#define WAVE_FORMAT_PCM 1")]
    public const int WAVE_FORMAT_PCM = 1;

    public static ref readonly Guid IID_IUnknown
    {
        get
        {
            ReadOnlySpan<byte> data = [
                0x00, 0x00, 0x00, 0x00,
                0x00, 0x00,
                0x00, 0x00,
                0xC0,
                0x00,
                0x00,
                0x00,
                0x00,
                0x00,
                0x00,
                0x46
            ];

            Debug.Assert(data.Length == Unsafe.SizeOf<Guid>());
            return ref Unsafe.As<byte, Guid>(ref MemoryMarshal.GetReference(data));
        }
    }

    public static ref readonly Guid IID_IMalloc
    {
        get
        {
            ReadOnlySpan<byte> data = [
                0x02, 0x00, 0x00, 0x00,
                0x00, 0x00,
                0x00, 0x00,
                0xC0,
                0x00,
                0x00,
                0x00,
                0x00,
                0x00,
                0x00,
                0x46
            ];

            Debug.Assert(data.Length == Unsafe.SizeOf<Guid>());
            return ref Unsafe.As<byte, Guid>(ref MemoryMarshal.GetReference(data));
        }
    }

    public static ref readonly Guid IID_ISequentialStream
    {
        get
        {
            ReadOnlySpan<byte> data = [
                0x30, 0x3A, 0x73, 0x0C,
                0x1C, 0x2A,
                0xCE, 0x11,
                0xAD,
                0xE5,
                0x00,
                0xAA,
                0x00,
                0x44,
                0x77,
                0x3D
            ];

            Debug.Assert(data.Length == Unsafe.SizeOf<Guid>());
            return ref Unsafe.As<byte, Guid>(ref MemoryMarshal.GetReference(data));
        }
    }

    public static ref readonly Guid IID_IStream
    {
        get
        {
            ReadOnlySpan<byte> data = [
                0x0C, 0x00, 0x00, 0x00,
                0x00, 0x00,
                0x00, 0x00,
                0xC0,
                0x00,
                0x00,
                0x00,
                0x00,
                0x00,
                0x00,
                0x46
            ];

            Debug.Assert(data.Length == Unsafe.SizeOf<Guid>());
            return ref Unsafe.As<byte, Guid>(ref MemoryMarshal.GetReference(data));
        }
    }

    public static ref readonly Guid IID_KSDATAFORMAT_SUBTYPE_PCM
    {
        get
        {
            ReadOnlySpan<byte> data = [
                0x01, 0x00, 0x00, 0x00,
                0x00, 0x00,
                0x10, 0x00,
                0x80,
                0x00,
                0x00,
                0xAA,
                0x00,
                0x38,
                0x9B,
                0x71
            ];

            Debug.Assert(data.Length == Unsafe.SizeOf<Guid>());
            return ref Unsafe.As<byte, Guid>(ref MemoryMarshal.GetReference(data));
        }
    }

    public static ref readonly Guid IID_KSDATAFORMAT_SUBTYPE_IEEE_FLOAT
    {
        get
        {
            ReadOnlySpan<byte> data = [
                0x03, 0x00, 0x00, 0x00,
                0x00, 0x00,
                0x10, 0x00,
                0x80,
                0x00,
                0x00,
                0xAA,
                0x00,
                0x38,
                0x9B,
                0x71
            ];

            Debug.Assert(data.Length == Unsafe.SizeOf<Guid>());
            return ref Unsafe.As<byte, Guid>(ref MemoryMarshal.GetReference(data));
        }
    }
}
