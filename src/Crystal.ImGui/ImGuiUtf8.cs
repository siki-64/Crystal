using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Text;

namespace Crystal.ImGui;

public static unsafe class ImGuiUtf8
{
    public static string? ToString(byte* text) => text == null ? null : Marshal.PtrToStringUTF8((nint)text);

    public static byte* Literal(ReadOnlySpan<byte> nullTerminatedLiteral) =>
        (byte*)Unsafe.AsPointer(ref MemoryMarshal.GetReference(nullTerminatedLiteral));
}

public unsafe ref struct ImGuiUtf8String
{
    public const int StackCapacity = 256;

    private byte* _nativeBuffer;

    public ImGuiUtf8String(string? text, Span<byte> stackBuffer)
    {
        _nativeBuffer = null;
        if (text == null)
        {
            Pointer = null;
            return;
        }

        var byteCount = Encoding.UTF8.GetMaxByteCount(text.Length) + 1;
        if (byteCount > stackBuffer.Length) byteCount = Encoding.UTF8.GetByteCount(text) + 1;

        if (byteCount <= stackBuffer.Length)
        {
            var written = Encoding.UTF8.GetBytes(text, stackBuffer);
            stackBuffer[written] = 0;
            Pointer = (byte*)Unsafe.AsPointer(ref MemoryMarshal.GetReference(stackBuffer));
            return;
        }

        _nativeBuffer = (byte*)NativeMemory.Alloc((nuint)byteCount);
        var heapWritten = Encoding.UTF8.GetBytes(text, new Span<byte>(_nativeBuffer, byteCount));
        _nativeBuffer[heapWritten] = 0;
        Pointer = _nativeBuffer;
    }

    public byte* Pointer { get; private set; }

    public void Dispose()
    {
        if (_nativeBuffer != null)
        {
            NativeMemory.Free(_nativeBuffer);
            _nativeBuffer = null;
        }
        Pointer = null;
    }
}

public unsafe ref struct ImGuiUtf8StringArray
{
    private byte* _block;

    public ImGuiUtf8StringArray(string[] items)
    {
        var pointersSize = items.Length * sizeof(byte*);
        var textSize = 0;
        foreach (var item in items) textSize += Encoding.UTF8.GetByteCount(item ?? string.Empty) + 1;

        _block = (byte*)NativeMemory.Alloc((nuint)Math.Max(pointersSize + textSize, 1));
        var pointers = (byte**)_block;
        var text = _block + pointersSize;
        for (var index = 0; index < items.Length; index++)
        {
            pointers[index] = text;
            var written = Encoding.UTF8.GetBytes(items[index] ?? string.Empty, new Span<byte>(text, textSize));
            text[written] = 0;
            text += written + 1;
            textSize -= written + 1;
        }
    }

    public readonly byte** Pointer => (byte**)_block;

    public void Dispose()
    {
        if (_block != null)
        {
            NativeMemory.Free(_block);
            _block = null;
        }
    }
}
