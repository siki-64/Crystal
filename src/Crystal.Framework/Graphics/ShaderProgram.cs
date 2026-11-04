using System.Collections;
using Crystal.Graphics.D3D12;
using TerraFX.Interop.DirectX;
using TerraFX.Interop.Windows;
using static TerraFX.Interop.DirectX.D3D12_BARRIER_SYNC;

namespace Crystal.Graphics;

public sealed unsafe class ShaderProgram
{
    private ID3D12RootSignature* _rootSignature;
    private readonly bool _ownsRootSignature;
    private readonly ShaderProgramValues _immediateValues;
    private readonly Lock _valuesLock = new();
    private ShaderProgramValues?[] _valuesBySlot;

    internal ShaderProgram(ShaderLibrary library, D3D12ShaderProgramInfo info, byte[][] shaders)
    {
        Library = library;
        Info = info;
        VertexShader = info.VertexShader < 0 ? null : shaders[info.VertexShader];
        PixelShader = info.PixelShader < 0 ? null : shaders[info.PixelShader];
        ComputeShader = info.ComputeShader < 0 ? null : shaders[info.ComputeShader];
        ThreadGroupSize = new ThreadGroupSize(info.ThreadGroupSize.X, info.ThreadGroupSize.Y, info.ThreadGroupSize.Z);

        ConstantBufferSlots = new ShaderConstantBuffer[info.ConstantBuffers.Length];
        var valueBindings = new Dictionary<string, List<ShaderValueBinding>>(StringComparer.Ordinal);
        var valueNames = new List<string>();
        for (var index = 0; index < ConstantBufferSlots.Length; index++)
        {
            var buffer = new ShaderConstantBuffer(this, info.ConstantBuffers[index], index);
            ConstantBufferSlots[index] = buffer;
            foreach (var variable in info.ConstantBuffers[index].Variables)
            {
                if (!valueBindings.TryGetValue(variable.Name, out var bindings))
                {
                    valueBindings.Add(variable.Name, bindings = []);
                    valueNames.Add(variable.Name);
                }
                else if (!HasSameShape(bindings[0].Variable, variable))
                {
                    throw new ArgumentException(
                        $"Program '{info.Name}' declares '{variable.Name}' in constant buffers '{bindings[0].Buffer.Name}' and '{buffer.Name}' with different sizes or shapes.");
                }
                bindings.Add(new ShaderValueBinding(buffer, variable));
            }
        }
        var parameters = new List<ShaderParameter>();
        foreach (var valueName in valueNames) parameters.Add(new ShaderParameter(this, valueName, [.. valueBindings[valueName]]));

        var constantBufferValues = new ShaderConstantBufferValues[ConstantBufferSlots.Length];
        for (var index = 0; index < constantBufferValues.Length; index++) constantBufferValues[index] = new ShaderConstantBufferValues(ConstantBufferSlots[index].Size);
        var resourceValues = new object?[info.Resources.Length];
        ResourceSyncs = new D3D12_BARRIER_SYNC[info.Resources.Length];
        for (var index = 0; index < info.Resources.Length; index++)
        {
            var resource = info.Resources[index];
            ResourceSyncs[index] = ToSync(resource.Visibility);
            if (resource.Kind == D3D12ShaderResourceKind.Sampler) resourceValues[index] = library.GraphicsDevice.GetSamplerState(SamplerDescription.LinearWrap);
            if (valueBindings.ContainsKey(resource.Name))
                throw new ArgumentException($"Program '{info.Name}' uses the name '{resource.Name}' for a constant buffer value and a resource.");
            parameters.Add(new ShaderParameter(this, resource, index));
        }

        _immediateValues = new ShaderProgramValues(GraphicsRecordingState.ImmediateValueGeneration, constantBufferValues, resourceValues);
        _valuesBySlot = [_immediateValues];
        Parameters = new ShaderParameterCollection(info.Name, parameters);
        ConstantBuffers = new ShaderConstantBufferCollection(ConstantBufferSlots);
        ConstantBufferRootParameters = new int[ConstantBufferSlots.Length];
        ConstantBufferPixelRootParameters = new int[ConstantBufferSlots.Length];
        for (var index = 0; index < ConstantBufferSlots.Length; index++)
        {
            var buffer = info.ConstantBuffers[index];
            (ConstantBufferRootParameters[index], ConstantBufferPixelRootParameters[index]) = (info.UsesDescriptorIndices, info.ComputeShader >= 0) switch
            {
                (false, _) => (index, -1),
                (true, true) => (buffer.Register, -1),
                _ => (buffer.Visibility & (D3D12ShaderVisibility.Vertex | D3D12ShaderVisibility.Pixel)) switch
                {
                    D3D12ShaderVisibility.Vertex => (buffer.Register, -1),
                    D3D12ShaderVisibility.Pixel => (D3D12RootSignatures.GraphicsConstantBufferRegisters + buffer.Register, -1),
                    _ => (buffer.Register, D3D12RootSignatures.GraphicsConstantBufferRegisters + buffer.Register),
                },
            };
        }
        foreach (var resource in info.Resources)
        {
            VertexDescriptorSlotCount = Math.Max(VertexDescriptorSlotCount, resource.VertexDescriptorSlot + 1);
            PixelDescriptorSlotCount = Math.Max(PixelDescriptorSlotCount, resource.PixelDescriptorSlot + 1);
            ComputeDescriptorSlotCount = Math.Max(ComputeDescriptorSlotCount, resource.ComputeDescriptorSlot + 1);
        }

        var native = library.GraphicsDevice.Native;
        _ownsRootSignature = !info.UsesDescriptorIndices;
        _rootSignature = !info.UsesDescriptorIndices
            ? D3D12RootSignatures.Create(native, info, $"{library.Name}.{info.Name}")
            : info.ComputeShader >= 0 ? native.DescriptorIndexedComputeRootSignature : native.DescriptorIndexedGraphicsRootSignature;
    }

    public string Name => Info.Name;
    public ShaderLibrary Library { get; }
    public ShaderParameterCollection Parameters { get; }
    public ShaderConstantBufferCollection ConstantBuffers { get; }
    public bool IsCompute => ComputeShader != null;
    public ThreadGroupSize ThreadGroupSize { get; }

    internal D3D12ShaderProgramInfo Info { get; }
    internal byte[]? VertexShader { get; }
    internal byte[]? PixelShader { get; }
    internal byte[]? ComputeShader { get; }
    internal ShaderConstantBuffer[] ConstantBufferSlots { get; }
    internal D3D12_BARRIER_SYNC[] ResourceSyncs { get; }
    internal int[] ConstantBufferRootParameters { get; }
    internal int[] ConstantBufferPixelRootParameters { get; }
    internal int VertexDescriptorSlotCount { get; }
    internal int PixelDescriptorSlotCount { get; }
    internal int ComputeDescriptorSlotCount { get; }
    internal bool UsesDescriptorIndices => Info.UsesDescriptorIndices;

    internal ID3D12RootSignature* RootSignature
    {
        get
        {
            ObjectDisposedException.ThrowIf(_rootSignature == null, Library);
            return _rootSignature;
        }
    }

    public override string ToString() => Name;

    internal ShaderProgramValues Values => ValuesFor(Library.GraphicsDevice.Recording);

    internal ShaderProgramValues ValuesFor(GraphicsRecordingState state)
    {
        var valuesBySlot = Volatile.Read(ref _valuesBySlot);
        var slot = state.ValueSlot;
        if (slot < valuesBySlot.Length && valuesBySlot[slot] is { } values && values.Generation == state.ValueGeneration) return values;
        return CreateValues(slot, state.ValueGeneration);
    }

    private ShaderProgramValues CreateValues(int slot, long generation)
    {
        lock (_valuesLock)
        {
            var valuesBySlot = _valuesBySlot;
            if (slot < valuesBySlot.Length && valuesBySlot[slot] is { } existing && existing.Generation == generation) return existing;
            if (slot >= valuesBySlot.Length) Array.Resize(ref valuesBySlot, Math.Max(slot + 1, valuesBySlot.Length * 2));
            var values = _immediateValues.CopyFor(generation);
            valuesBySlot[slot] = values;
            Volatile.Write(ref _valuesBySlot, valuesBySlot);
            return values;
        }
    }

    internal void ReleaseRootSignature()
    {
        if (_rootSignature == null) return;
        if (_ownsRootSignature) Library.GraphicsDevice.ReleaseDeviceObject((IUnknown*)_rootSignature);
        _rootSignature = null;
    }

    private static bool HasSameShape(D3D12ShaderVariableInfo first, D3D12ShaderVariableInfo second) =>
        first.Size == second.Size && first.Class == second.Class && first.Rows == second.Rows && first.Columns == second.Columns && first.Elements == second.Elements;

    internal static D3D12_BARRIER_SYNC ToSync(D3D12ShaderVisibility visibility) =>
        ((visibility & D3D12ShaderVisibility.Vertex) != 0 ? D3D12_BARRIER_SYNC_VERTEX_SHADING : D3D12_BARRIER_SYNC_NONE)
        | ((visibility & D3D12ShaderVisibility.Pixel) != 0 ? D3D12_BARRIER_SYNC_PIXEL_SHADING : D3D12_BARRIER_SYNC_NONE)
        | ((visibility & D3D12ShaderVisibility.Compute) != 0 ? D3D12_BARRIER_SYNC_COMPUTE_SHADING : D3D12_BARRIER_SYNC_NONE);
}

public readonly record struct ThreadGroupSize(int X, int Y, int Z);

public sealed class ShaderParameterCollection : IReadOnlyList<ShaderParameter>
{
    private readonly ShaderParameter[] _parameters;
    private readonly Dictionary<string, ShaderParameter> _parametersByName;

    internal ShaderParameterCollection(string programName, List<ShaderParameter> parameters)
    {
        _parameters = [.. parameters];
        _parametersByName = new Dictionary<string, ShaderParameter>(StringComparer.Ordinal);
        foreach (var parameter in _parameters)
        {
            if (!_parametersByName.TryAdd(parameter.Name, parameter))
                throw new ArgumentException($"Program '{programName}' has two resources named '{parameter.Name}'.");
        }
    }

    public int Count => _parameters.Length;
    public ShaderParameter this[int index] => _parameters[index];
    public ShaderParameter? this[string name] => _parametersByName.GetValueOrDefault(name);

    public IEnumerator<ShaderParameter> GetEnumerator() => ((IEnumerable<ShaderParameter>)_parameters).GetEnumerator();

    IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
}

public sealed class ShaderConstantBufferCollection : IReadOnlyList<ShaderConstantBuffer>
{
    private readonly ShaderConstantBuffer[] _buffers;
    private readonly Dictionary<string, ShaderConstantBuffer> _buffersByName;

    internal ShaderConstantBufferCollection(ShaderConstantBuffer[] buffers)
    {
        _buffers = buffers;
        _buffersByName = _buffers.ToDictionary(buffer => buffer.Name);
    }

    public int Count => _buffers.Length;
    public ShaderConstantBuffer this[int index] => _buffers[index];
    public ShaderConstantBuffer? this[string name] => _buffersByName.GetValueOrDefault(name);

    public IEnumerator<ShaderConstantBuffer> GetEnumerator() => ((IEnumerable<ShaderConstantBuffer>)_buffers).GetEnumerator();

    IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
}
