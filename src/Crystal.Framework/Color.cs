using System.Numerics;
using System.Runtime.InteropServices;

namespace Crystal;

[StructLayout(LayoutKind.Sequential)]
public readonly record struct Color(byte R, byte G, byte B, byte A = 255)
{
    public Color(float r, float g, float b, float a = 1f)
        : this(ToByte(r), ToByte(g), ToByte(b), ToByte(a))
    {
    }

    public Color(Vector4 rgba)
        : this(rgba.X, rgba.Y, rgba.Z, rgba.W)
    {
    }

    public uint PackedValue => (uint)(R | (G << 8) | (B << 16) | (A << 24));

    public Vector4 ToVector4() => new Vector4(R, G, B, A) / 255f;

    public static Color FromPacked(uint packedValue) =>
        new((byte)packedValue, (byte)(packedValue >> 8), (byte)(packedValue >> 16), (byte)(packedValue >> 24));

    public static Color Lerp(Color from, Color to, float amount) => new(Vector4.Lerp(from.ToVector4(), to.ToVector4(), amount));

    public static Color Transparent => new(0, 0, 0, 0);
    public static Color Black => new(0, 0, 0);
    public static Color White => new(255, 255, 255);
    public static Color Red => new(255, 0, 0);
    public static Color Green => new(0, 128, 0);
    public static Color Blue => new(0, 0, 255);
    public static Color CornflowerBlue => new(100, 149, 237);

    private static byte ToByte(float value) => (byte)Math.Clamp(MathF.Round(value * 255f), 0f, 255f);
}
