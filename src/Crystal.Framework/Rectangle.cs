namespace Crystal;

public readonly record struct Rectangle(int X, int Y, int Width, int Height)
{
    public static Rectangle Empty => default;

    public int Left => X;
    public int Top => Y;
    public int Right => X + Width;
    public int Bottom => Y + Height;
    public Point Location => new(X, Y);
    public Point Size => new(Width, Height);
    public bool IsEmpty => Width <= 0 || Height <= 0;

    public bool Contains(Point point) => point.X >= X && point.X < Right && point.Y >= Y && point.Y < Bottom;

    public bool Intersects(Rectangle other) => other.Left < Right && Left < other.Right && other.Top < Bottom && Top < other.Bottom;

    public static Rectangle Intersect(Rectangle first, Rectangle second)
    {
        var left = Math.Max(first.Left, second.Left);
        var top = Math.Max(first.Top, second.Top);
        var right = Math.Min(first.Right, second.Right);
        var bottom = Math.Min(first.Bottom, second.Bottom);
        return right > left && bottom > top ? new Rectangle(left, top, right - left, bottom - top) : Empty;
    }
}
