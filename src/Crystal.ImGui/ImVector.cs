using System.Runtime.InteropServices;

namespace Crystal.ImGui;

[StructLayout(LayoutKind.Sequential)]
public unsafe struct ImVector<T> where T : unmanaged
{
    public int Size;
    public int Capacity;
    public T* Data;

    public readonly ref T this[int index]
    {
        get
        {
            ArgumentOutOfRangeException.ThrowIfGreaterThanOrEqual((uint)index, (uint)Size, nameof(index));
            return ref Data[index];
        }
    }

    public readonly Span<T> AsSpan() => new(Data, Size);
}

[StructLayout(LayoutKind.Sequential)]
public unsafe struct ImPtrVector<T> where T : unmanaged
{
    public int Size;
    public int Capacity;
    public T** Data;

    public readonly T* this[int index]
    {
        get
        {
            ArgumentOutOfRangeException.ThrowIfGreaterThanOrEqual((uint)index, (uint)Size, nameof(index));
            return Data[index];
        }
    }
}
