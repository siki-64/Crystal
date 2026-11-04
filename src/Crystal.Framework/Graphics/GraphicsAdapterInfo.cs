namespace Crystal.Graphics;

public sealed record GraphicsAdapterInfo(
    string Description,
    uint VendorId,
    uint DeviceId,
    ulong DedicatedVideoMemory,
    bool IsSoftware);
