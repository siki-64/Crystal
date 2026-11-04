namespace Crystal.Graphics;

public enum GraphicsAdapterPreference
{
    HighPerformance,
    MinimumPower,
    Software,
}

public sealed record GraphicsDeviceOptions
{
    public bool VSync { get; init; } = true;
    public int MaximumFrameLatency { get; init; } = 2;
    public GraphicsAdapterPreference AdapterPreference { get; init; } = GraphicsAdapterPreference.HighPerformance;
    public bool EnableDebugLayer { get; init; }
    public bool EnableGpuBasedValidation { get; init; }
    public bool ThrowOnDebugLayerErrors { get; init; } = true;
    public int ShaderResourceDescriptorCapacity { get; init; } = 1_000_000;

    internal GraphicsDeviceOptions ApplyEnvironment()
    {
        var gpuBasedValidation = EnableGpuBasedValidation || IsEnvironmentFlagSet("CRYSTAL_DX12_GBV");
        return this with
        {
            EnableGpuBasedValidation = gpuBasedValidation,
            EnableDebugLayer = EnableDebugLayer || gpuBasedValidation || IsEnvironmentFlagSet("CRYSTAL_DX12_DEBUG"),
            AdapterPreference = IsEnvironmentFlagSet("CRYSTAL_DX12_WARP") ? GraphicsAdapterPreference.Software : AdapterPreference,
        };
    }

    private static bool IsEnvironmentFlagSet(string name) => Environment.GetEnvironmentVariable(name) is "1" or "true";
}
