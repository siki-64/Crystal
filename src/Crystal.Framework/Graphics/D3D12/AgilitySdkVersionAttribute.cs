using System.Reflection;

namespace Crystal.Graphics.D3D12;

[AttributeUsage(AttributeTargets.Assembly)]
internal sealed class AgilitySdkVersionAttribute(uint version) : Attribute
{
    public uint Version { get; } = version;

    public static uint Current { get; } =
        typeof(AgilitySdkVersionAttribute).Assembly.GetCustomAttribute<AgilitySdkVersionAttribute>()?.Version
        ?? throw new InvalidOperationException("Crystal.Framework was built without an Agility SDK version.");
}
