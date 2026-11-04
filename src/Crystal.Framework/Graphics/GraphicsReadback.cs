using System.Runtime.InteropServices;
using Crystal.Graphics.D3D12;
using TerraFX.Interop.DirectX;
using TerraFX.Interop.Windows;
using static Crystal.Graphics.D3D12.D3D12Interop;

namespace Crystal.Graphics;

public sealed unsafe class GraphicsReadback : GraphicsResource
{
    private readonly ID3D12Resource* _buffer;
    private readonly ulong _offset;
    private readonly uint _rowCount;
    private readonly ulong _rowSize;
    private readonly ulong _rowPitch;

    internal GraphicsReadback(GraphicsDevice graphicsDevice, ID3D12Resource* buffer, ulong offset, uint rowCount, ulong rowSize, ulong rowPitch)
        : base(graphicsDevice, null)
    {
        _buffer = buffer;
        _offset = offset;
        _rowCount = rowCount;
        _rowSize = rowSize;
        _rowPitch = rowPitch;
        graphicsDevice.Track(this);
    }

    public long SizeInBytes => (long)(_rowCount * _rowSize);
    public bool IsComplete => FenceValue != 0 && GraphicsDevice.Native.DirectQueue.IsComplete(FenceValue);

    internal ulong FenceValue { get; set; }

    public void Wait()
    {
        ThrowIfDisposed();
        if (FenceValue == 0) GraphicsDevice.Flush();
        GraphicsDevice.Native.DirectQueue.WaitForValue(FenceValue);
    }

    public void GetData<T>(Span<T> destination) where T : unmanaged
    {
        var bytes = MemoryMarshal.AsBytes(destination);
        if (bytes.Length != SizeInBytes)
            throw new ArgumentException($"The destination must be exactly {SizeInBytes} bytes, but it is {bytes.Length} bytes.", nameof(destination));
        Wait();

        var readRange = new D3D12_RANGE { Begin = 0, End = (nuint)(_offset + (_rowCount - 1) * _rowPitch + _rowSize) };
        byte* mapped;
        ThrowOnFailure(_buffer->Map(0, &readRange, (void**)&mapped), "ID3D12Resource::Map");
        for (var row = 0u; row < _rowCount; row++)
            new ReadOnlySpan<byte>(mapped + _offset + row * _rowPitch, (int)_rowSize).CopyTo(bytes.Slice((int)(row * _rowSize), (int)_rowSize));
        var emptyRange = new D3D12_RANGE();
        _buffer->Unmap(0, &emptyRange);
    }

    private protected override void ReleaseNativeResources()
    {
        var directValue = FenceValue == 0 ? D3D12ReleaseValues.AwaitingSubmission : FenceValue;
        GraphicsDevice.Native.DeferredReleases.Release((IUnknown*)_buffer, new D3D12ReleaseValues(directValue, 0, 0));
    }
}
