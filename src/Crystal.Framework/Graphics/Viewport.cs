namespace Crystal.Graphics;

public readonly record struct Viewport(float X, float Y, float Width, float Height, float MinDepth = 0f, float MaxDepth = 1f)
{
    public Viewport(Rectangle bounds)
        : this(bounds.X, bounds.Y, bounds.Width, bounds.Height)
    {
    }

    public float AspectRatio => Height == 0f ? 0f : Width / Height;
    public Rectangle Bounds => new((int)X, (int)Y, (int)Width, (int)Height);
}
