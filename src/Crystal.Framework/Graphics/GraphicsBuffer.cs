using System.Runtime.InteropServices;
using Crystal.Graphics.D3D12;
using TerraFX.Interop.DirectX;
using static TerraFX.Interop.DirectX.D3D12_BARRIER_LAYOUT;
using static TerraFX.Interop.DirectX.D3D12_BUFFER_SRV_FLAGS;
using static TerraFX.Interop.DirectX.D3D12_BUFFER_UAV_FLAGS;
using static TerraFX.Interop.DirectX.D3D12_HEAP_TYPE;
using static TerraFX.Interop.DirectX.D3D12_RESOURCE_FLAGS;
using static TerraFX.Interop.DirectX.D3D12_SRV_DIMENSION;
using static TerraFX.Interop.DirectX.D3D12_UAV_DIMENSION;
using static TerraFX.Interop.DirectX.DXGI_FORMAT;

namespace Crystal.Graphics;

public sealed unsafe class GraphicsBuffer : GraphicsResource
{
    public const int MaximumConstantBufferSize = 65536;

    private const int ConstantBufferAlignment = 256;
    private const uint NoDescriptor = uint.MaxValue;

    public GraphicsBuffer(GraphicsDevice graphicsDevice, in BufferDescription description)
        : base(graphicsDevice, description.Name)
    {
        Validate(description);
        Description = description;

        var usage = description.Usage;
        var allocatedSize = (usage & BufferUsage.Constant) != 0 ? AlignUp((ulong)description.Size, ConstantBufferAlignment) : (ulong)description.Size;
        var flags = (usage & BufferUsage.UnorderedAccess) != 0 ? D3D12_RESOURCE_FLAG_ALLOW_UNORDERED_ACCESS : D3D12_RESOURCE_FLAG_NONE;
        var device = graphicsDevice.Native;
        var pointer = device.CreateCommittedResource(
            D3D12_HEAP_TYPE_DEFAULT, D3D12Device.BufferDescription(allocatedSize, flags), D3D12_BARRIER_LAYOUT_UNDEFINED, null, Name ?? "Buffer");
        Resource = new D3D12Resource(pointer, isTexture: false, D3D12_BARRIER_LAYOUT_UNDEFINED, Name ?? "Buffer");
        GpuAddress = pointer->GetGPUVirtualAddress();
        AllocatedSize = allocatedSize;

        if ((usage & BufferUsage.Constant) != 0)
        {
            ConstantBufferViewIndex = device.ShaderResourceHeap.Allocate();
            var view = new D3D12_CONSTANT_BUFFER_VIEW_DESC { BufferLocation = GpuAddress, SizeInBytes = (uint)allocatedSize };
            device.Device->CreateConstantBufferView(&view, device.ShaderResourceHeap.CpuHandle(ConstantBufferViewIndex));
        }

        var isRaw = (usage & BufferUsage.UnorderedAccess) != 0 && (usage & BufferUsage.Structured) == 0;
        if ((usage & BufferUsage.Structured) != 0 || isRaw)
        {
            ShaderResourceViewIndex = device.ShaderResourceHeap.Allocate();
            var view = new D3D12_SHADER_RESOURCE_VIEW_DESC
            {
                Format = isRaw ? DXGI_FORMAT_R32_TYPELESS : DXGI_FORMAT_UNKNOWN,
                ViewDimension = D3D12_SRV_DIMENSION_BUFFER,
                Shader4ComponentMapping = D3D12Formats.DefaultComponentMapping,
            };
            view.Buffer = new D3D12_BUFFER_SRV
            {
                NumElements = (uint)(isRaw ? description.Size / 4 : description.Size / description.Stride),
                StructureByteStride = isRaw ? 0 : (uint)description.Stride,
                Flags = isRaw ? D3D12_BUFFER_SRV_FLAG_RAW : D3D12_BUFFER_SRV_FLAG_NONE,
            };
            device.Device->CreateShaderResourceView(pointer, &view, device.ShaderResourceHeap.CpuHandle(ShaderResourceViewIndex));
        }

        if ((usage & BufferUsage.UnorderedAccess) != 0)
        {
            UnorderedAccessViewIndex = device.ShaderResourceHeap.Allocate();
            var isStructured = (usage & BufferUsage.Structured) != 0;
            var view = new D3D12_UNORDERED_ACCESS_VIEW_DESC
            {
                Format = isStructured ? DXGI_FORMAT_UNKNOWN : DXGI_FORMAT_R32_TYPELESS,
                ViewDimension = D3D12_UAV_DIMENSION_BUFFER,
            };
            view.Buffer = new D3D12_BUFFER_UAV
            {
                NumElements = (uint)(isStructured ? description.Size / description.Stride : description.Size / 4),
                StructureByteStride = isStructured ? (uint)description.Stride : 0,
                Flags = isStructured ? D3D12_BUFFER_UAV_FLAG_NONE : D3D12_BUFFER_UAV_FLAG_RAW,
            };
            device.Device->CreateUnorderedAccessView(pointer, null, &view, device.ShaderResourceHeap.CpuHandle(UnorderedAccessViewIndex));
        }

        graphicsDevice.Track(this);
    }

    public BufferDescription Description { get; }
    public long Size => Description.Size;
    public int Stride => Description.Stride;
    public BufferUsage Usage => Description.Usage;

    internal D3D12Resource Resource { get; }
    internal ulong GpuAddress { get; }
    internal ulong AllocatedSize { get; }
    internal bool IsRaw => (Usage & BufferUsage.UnorderedAccess) != 0 && (Usage & BufferUsage.Structured) == 0;
    internal uint ConstantBufferViewIndex { get; } = NoDescriptor;
    internal uint ShaderResourceViewIndex { get; } = NoDescriptor;
    internal uint UnorderedAccessViewIndex { get; } = NoDescriptor;

    public void SetData<T>(ReadOnlySpan<T> data, long offsetInBytes = 0) where T : unmanaged
    {
        ThrowIfDisposed();
        var bytes = MemoryMarshal.AsBytes(data);
        if (bytes.IsEmpty) return;
        CheckRange(offsetInBytes, bytes.Length);
        GraphicsDevice.UploadToBuffer(Resource, (ulong)offsetInBytes, bytes);
    }

    public void GetData<T>(Span<T> destination, long offsetInBytes = 0) where T : unmanaged
    {
        using var readback = BeginGetData(offsetInBytes, MemoryMarshal.AsBytes(destination).Length);
        readback.GetData(destination);
    }

    public GraphicsReadback BeginGetData(long offsetInBytes, long sizeInBytes)
    {
        ThrowIfDisposed();
        ArgumentOutOfRangeException.ThrowIfNegativeOrZero(sizeInBytes);
        CheckRange(offsetInBytes, sizeInBytes);
        return GraphicsDevice.BeginBufferReadback(Resource, (ulong)offsetInBytes, (ulong)sizeInBytes);
    }

    private protected override void ReleaseNativeResources()
    {
        if (ConstantBufferViewIndex != NoDescriptor) GraphicsDevice.FreeShaderResourceDescriptor(Resource, ConstantBufferViewIndex);
        if (ShaderResourceViewIndex != NoDescriptor) GraphicsDevice.FreeShaderResourceDescriptor(Resource, ShaderResourceViewIndex);
        if (UnorderedAccessViewIndex != NoDescriptor) GraphicsDevice.FreeShaderResourceDescriptor(Resource, UnorderedAccessViewIndex);
        GraphicsDevice.ReleaseResource(Resource);
    }

    private void CheckRange(long offsetInBytes, long sizeInBytes)
    {
        if (offsetInBytes < 0 || offsetInBytes + sizeInBytes > Size)
            throw new ArgumentOutOfRangeException(nameof(offsetInBytes), $"The range {offsetInBytes}..{offsetInBytes + sizeInBytes} is outside the buffer of {Size} bytes.");
    }

    private static void Validate(in BufferDescription description)
    {
        ArgumentOutOfRangeException.ThrowIfNegativeOrZero(description.Size, nameof(description));
        ArgumentOutOfRangeException.ThrowIfNegative(description.Stride, nameof(description));
        var usage = description.Usage;
        if ((usage & BufferUsage.Index) != 0 && description.Stride is not (2 or 4))
            throw new ArgumentException("Index buffers need a stride of 2 or 4 bytes.", nameof(description));
        if ((usage & BufferUsage.Vertex) != 0 && description.Stride == 0)
            throw new ArgumentException("Vertex buffers need a stride.", nameof(description));
        if ((usage & BufferUsage.Constant) != 0 && description.Size > MaximumConstantBufferSize)
            throw new ArgumentException($"Constant buffers can't be larger than {MaximumConstantBufferSize} bytes.", nameof(description));
        if ((usage & BufferUsage.Structured) != 0 && (description.Stride == 0 || description.Size % description.Stride != 0))
            throw new ArgumentException("Structured buffers need a stride that divides the size.", nameof(description));
        if ((usage & BufferUsage.UnorderedAccess) != 0 && (usage & BufferUsage.Structured) == 0 && description.Size % 4 != 0)
            throw new ArgumentException("Raw unordered access buffers need a size that is a multiple of 4 bytes.", nameof(description));
    }

    private static ulong AlignUp(ulong value, ulong alignment) => (value + alignment - 1) / alignment * alignment;
}
