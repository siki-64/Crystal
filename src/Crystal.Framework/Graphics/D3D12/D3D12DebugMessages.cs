using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using TerraFX.Interop.DirectX;
using static TerraFX.Interop.DirectX.D3D12_MESSAGE_CALLBACK_FLAGS;
using static TerraFX.Interop.DirectX.D3D12_MESSAGE_SEVERITY;
using static TerraFX.Interop.Windows.Windows;

namespace Crystal.Graphics.D3D12;

internal sealed unsafe class D3D12DebugMessages : IDisposable
{
    private readonly Lock _lock = new();
    private readonly List<GraphicsDebugMessage> _pending = [];
    private GCHandle _self;
    private ID3D12InfoQueue1* _infoQueue;
    private uint _callbackCookie;

    private D3D12DebugMessages(ID3D12InfoQueue1* infoQueue)
    {
        _infoQueue = infoQueue;
        _self = GCHandle.Alloc(this);
        uint cookie;
        D3D12Interop.ThrowOnFailure(
            infoQueue->RegisterMessageCallback(&OnMessage, D3D12_MESSAGE_CALLBACK_FLAG_NONE, (void*)GCHandle.ToIntPtr(_self), &cookie),
            "ID3D12InfoQueue1::RegisterMessageCallback");
        _callbackCookie = cookie;
    }

    public static D3D12DebugMessages? TryRegister(ID3D12Device* device)
    {
        ID3D12InfoQueue1* infoQueue = null;
        if (FAILED(device->QueryInterface(__uuidof<ID3D12InfoQueue1>(), (void**)&infoQueue))) return null;
        return new D3D12DebugMessages(infoQueue);
    }

    public void Drain(List<GraphicsDebugMessage> destination)
    {
        lock (_lock)
        {
            destination.AddRange(_pending);
            _pending.Clear();
        }
    }

    public void PostApplicationError(string text)
    {
        fixed (byte* utf8 = System.Text.Encoding.UTF8.GetBytes(text + '\0'))
            D3D12Interop.ThrowOnFailure(_infoQueue->AddApplicationMessage(D3D12_MESSAGE_SEVERITY_ERROR, (sbyte*)utf8), "ID3D12InfoQueue::AddApplicationMessage");
    }

    public void Dispose()
    {
        if (_infoQueue == null) return;
        _infoQueue->UnregisterMessageCallback(_callbackCookie);
        D3D12Interop.Release(ref _infoQueue);
        _self.Free();
    }

    [UnmanagedCallersOnly]
    private static void OnMessage(D3D12_MESSAGE_CATEGORY category, D3D12_MESSAGE_SEVERITY severity, D3D12_MESSAGE_ID id, sbyte* description, void* context)
    {
        if (GCHandle.FromIntPtr((nint)context).Target is not D3D12DebugMessages messages) return;
        var message = new GraphicsDebugMessage(ToSeverity(severity), (int)id, Marshal.PtrToStringUTF8((nint)description) ?? string.Empty);
        lock (messages._lock) messages._pending.Add(message);
    }

    private static GraphicsDebugSeverity ToSeverity(D3D12_MESSAGE_SEVERITY severity) => severity switch
    {
        D3D12_MESSAGE_SEVERITY_CORRUPTION => GraphicsDebugSeverity.Corruption,
        D3D12_MESSAGE_SEVERITY_ERROR => GraphicsDebugSeverity.Error,
        D3D12_MESSAGE_SEVERITY_WARNING => GraphicsDebugSeverity.Warning,
        D3D12_MESSAGE_SEVERITY_INFO => GraphicsDebugSeverity.Info,
        _ => GraphicsDebugSeverity.Message,
    };
}
