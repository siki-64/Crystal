namespace Crystal.Interop;

internal unsafe interface INativeGuid
{
    static abstract Guid* NativeGuid { get; }
}

internal static unsafe partial class Windows
{
    public static bool FAILED(int result) => result < 0;

    public static bool SUCCEEDED(int result) => result >= 0;

    public static Guid* __uuidof<T>() where T : unmanaged, INativeGuid => T.NativeGuid;
}

internal static partial class D3D12
{
    public static D3D12_FILTER D3D12_ENCODE_BASIC_FILTER(D3D12_FILTER_TYPE min, D3D12_FILTER_TYPE mag, D3D12_FILTER_TYPE mip, D3D12_FILTER_REDUCTION_TYPE reduction) =>
        (D3D12_FILTER)((((int)min & D3D12_FILTER_TYPE_MASK) << D3D12_MIN_FILTER_SHIFT)
            | (((int)mag & D3D12_FILTER_TYPE_MASK) << D3D12_MAG_FILTER_SHIFT)
            | (((int)mip & D3D12_FILTER_TYPE_MASK) << D3D12_MIP_FILTER_SHIFT)
            | (((int)reduction & D3D12_FILTER_REDUCTION_TYPE_MASK) << D3D12_FILTER_REDUCTION_TYPE_SHIFT));

    public static D3D12_FILTER D3D12_ENCODE_ANISOTROPIC_FILTER(D3D12_FILTER_REDUCTION_TYPE reduction) =>
        (D3D12_FILTER)(D3D12_ANISOTROPIC_FILTERING_BIT | (int)D3D12_ENCODE_BASIC_FILTER(D3D12_FILTER_TYPE.D3D12_FILTER_TYPE_LINEAR, D3D12_FILTER_TYPE.D3D12_FILTER_TYPE_LINEAR, D3D12_FILTER_TYPE.D3D12_FILTER_TYPE_LINEAR, reduction));
}
