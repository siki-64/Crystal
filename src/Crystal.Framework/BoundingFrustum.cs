using System.Numerics;

namespace Crystal;

public readonly struct BoundingFrustum : IEquatable<BoundingFrustum>
{
    public const int PlaneCount = 6;
    public const int CornerCount = 8;

    public BoundingFrustum(Matrix4x4 viewProjection)
    {
        Matrix = viewProjection;
        var m = viewProjection;
        Near = Plane.Normalize(new Plane(-m.M13, -m.M23, -m.M33, -m.M43));
        Far = Plane.Normalize(new Plane(m.M13 - m.M14, m.M23 - m.M24, m.M33 - m.M34, m.M43 - m.M44));
        Left = Plane.Normalize(new Plane(-m.M14 - m.M11, -m.M24 - m.M21, -m.M34 - m.M31, -m.M44 - m.M41));
        Right = Plane.Normalize(new Plane(m.M11 - m.M14, m.M21 - m.M24, m.M31 - m.M34, m.M41 - m.M44));
        Top = Plane.Normalize(new Plane(m.M12 - m.M14, m.M22 - m.M24, m.M32 - m.M34, m.M42 - m.M44));
        Bottom = Plane.Normalize(new Plane(-m.M14 - m.M12, -m.M24 - m.M22, -m.M34 - m.M32, -m.M44 - m.M42));
    }

    public Matrix4x4 Matrix { get; }
    public Plane Near { get; }
    public Plane Far { get; }
    public Plane Left { get; }
    public Plane Right { get; }
    public Plane Top { get; }
    public Plane Bottom { get; }

    public void GetPlanes(Span<Plane> planes)
    {
        ArgumentOutOfRangeException.ThrowIfLessThan(planes.Length, PlaneCount, nameof(planes));
        planes[0] = Near;
        planes[1] = Far;
        planes[2] = Left;
        planes[3] = Right;
        planes[4] = Top;
        planes[5] = Bottom;
    }

    public void GetCorners(Span<Vector3> corners)
    {
        ArgumentOutOfRangeException.ThrowIfLessThan(corners.Length, CornerCount, nameof(corners));
        corners[0] = Intersection(Near, Left, Top);
        corners[1] = Intersection(Near, Right, Top);
        corners[2] = Intersection(Near, Right, Bottom);
        corners[3] = Intersection(Near, Left, Bottom);
        corners[4] = Intersection(Far, Left, Top);
        corners[5] = Intersection(Far, Right, Top);
        corners[6] = Intersection(Far, Right, Bottom);
        corners[7] = Intersection(Far, Left, Bottom);
    }

    public ContainmentType Contains(Vector3 point)
    {
        Span<Plane> planes = stackalloc Plane[PlaneCount];
        GetPlanes(planes);
        foreach (var plane in planes)
        {
            if (Plane.DotCoordinate(plane, point) > 0f) return ContainmentType.Disjoint;
        }
        return ContainmentType.Contains;
    }

    public ContainmentType Contains(BoundingBox box)
    {
        Span<Plane> planes = stackalloc Plane[PlaneCount];
        GetPlanes(planes);
        var result = ContainmentType.Contains;
        foreach (var plane in planes)
        {
            switch (box.Intersects(plane))
            {
                case PlaneIntersectionType.Front:
                    return ContainmentType.Disjoint;
                case PlaneIntersectionType.Intersecting:
                    result = ContainmentType.Intersects;
                    break;
            }
        }
        return result;
    }

    public ContainmentType Contains(BoundingSphere sphere)
    {
        Span<Plane> planes = stackalloc Plane[PlaneCount];
        GetPlanes(planes);
        var result = ContainmentType.Contains;
        foreach (var plane in planes)
        {
            switch (sphere.Intersects(plane))
            {
                case PlaneIntersectionType.Front:
                    return ContainmentType.Disjoint;
                case PlaneIntersectionType.Intersecting:
                    result = ContainmentType.Intersects;
                    break;
            }
        }
        return result;
    }

    public bool Intersects(BoundingBox box) => Contains(box) != ContainmentType.Disjoint;

    public bool Intersects(BoundingSphere sphere) => Contains(sphere) != ContainmentType.Disjoint;

    public bool Equals(BoundingFrustum other) => Matrix == other.Matrix;

    public override bool Equals(object? obj) => obj is BoundingFrustum other && Equals(other);

    public override int GetHashCode() => Matrix.GetHashCode();

    public static bool operator ==(BoundingFrustum left, BoundingFrustum right) => left.Equals(right);

    public static bool operator !=(BoundingFrustum left, BoundingFrustum right) => !left.Equals(right);

    private static Vector3 Intersection(Plane first, Plane second, Plane third)
    {
        var cross = Vector3.Cross(second.Normal, third.Normal);
        var denominator = Vector3.Dot(first.Normal, cross);
        return -(first.D * cross + second.D * Vector3.Cross(third.Normal, first.Normal) + third.D * Vector3.Cross(first.Normal, second.Normal)) / denominator;
    }
}
