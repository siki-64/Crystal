using System.Numerics;

namespace Crystal;

public readonly record struct BoundingBox(Vector3 Min, Vector3 Max)
{
    public const int CornerCount = 8;

    public Vector3 Center => (Min + Max) * 0.5f;
    public Vector3 Extents => (Max - Min) * 0.5f;

    public static BoundingBox CreateFromPoints(ReadOnlySpan<Vector3> points)
    {
        if (points.IsEmpty) throw new ArgumentException("At least one point is needed.", nameof(points));
        var min = points[0];
        var max = points[0];
        foreach (var point in points[1..])
        {
            min = Vector3.Min(min, point);
            max = Vector3.Max(max, point);
        }
        return new BoundingBox(min, max);
    }

    public static BoundingBox CreateFromSphere(BoundingSphere sphere) => new(sphere.Center - new Vector3(sphere.Radius), sphere.Center + new Vector3(sphere.Radius));

    public static BoundingBox CreateMerged(BoundingBox first, BoundingBox second) => new(Vector3.Min(first.Min, second.Min), Vector3.Max(first.Max, second.Max));

    public void GetCorners(Span<Vector3> corners)
    {
        ArgumentOutOfRangeException.ThrowIfLessThan(corners.Length, CornerCount, nameof(corners));
        corners[0] = new Vector3(Min.X, Max.Y, Max.Z);
        corners[1] = new Vector3(Max.X, Max.Y, Max.Z);
        corners[2] = new Vector3(Max.X, Min.Y, Max.Z);
        corners[3] = new Vector3(Min.X, Min.Y, Max.Z);
        corners[4] = new Vector3(Min.X, Max.Y, Min.Z);
        corners[5] = new Vector3(Max.X, Max.Y, Min.Z);
        corners[6] = new Vector3(Max.X, Min.Y, Min.Z);
        corners[7] = new Vector3(Min.X, Min.Y, Min.Z);
    }

    public BoundingBox Transform(Matrix4x4 matrix)
    {
        var center = Vector3.Transform(Center, matrix);
        var extents = Extents;
        var transformedExtents = new Vector3(
            MathF.Abs(matrix.M11) * extents.X + MathF.Abs(matrix.M21) * extents.Y + MathF.Abs(matrix.M31) * extents.Z,
            MathF.Abs(matrix.M12) * extents.X + MathF.Abs(matrix.M22) * extents.Y + MathF.Abs(matrix.M32) * extents.Z,
            MathF.Abs(matrix.M13) * extents.X + MathF.Abs(matrix.M23) * extents.Y + MathF.Abs(matrix.M33) * extents.Z);
        return new BoundingBox(center - transformedExtents, center + transformedExtents);
    }

    public ContainmentType Contains(Vector3 point) =>
        point.X >= Min.X && point.X <= Max.X && point.Y >= Min.Y && point.Y <= Max.Y && point.Z >= Min.Z && point.Z <= Max.Z
            ? ContainmentType.Contains
            : ContainmentType.Disjoint;

    public ContainmentType Contains(BoundingBox box)
    {
        if (!Intersects(box)) return ContainmentType.Disjoint;
        return box.Min.X >= Min.X && box.Max.X <= Max.X && box.Min.Y >= Min.Y && box.Max.Y <= Max.Y && box.Min.Z >= Min.Z && box.Max.Z <= Max.Z
            ? ContainmentType.Contains
            : ContainmentType.Intersects;
    }

    public ContainmentType Contains(BoundingSphere sphere)
    {
        if (!Intersects(sphere)) return ContainmentType.Disjoint;
        var radius = new Vector3(sphere.Radius);
        var min = sphere.Center - radius;
        var max = sphere.Center + radius;
        return min.X >= Min.X && max.X <= Max.X && min.Y >= Min.Y && max.Y <= Max.Y && min.Z >= Min.Z && max.Z <= Max.Z
            ? ContainmentType.Contains
            : ContainmentType.Intersects;
    }

    public bool Intersects(BoundingBox box) =>
        Min.X <= box.Max.X && Max.X >= box.Min.X && Min.Y <= box.Max.Y && Max.Y >= box.Min.Y && Min.Z <= box.Max.Z && Max.Z >= box.Min.Z;

    public bool Intersects(BoundingSphere sphere) =>
        Vector3.DistanceSquared(Vector3.Clamp(sphere.Center, Min, Max), sphere.Center) <= sphere.Radius * sphere.Radius;

    public PlaneIntersectionType Intersects(Plane plane)
    {
        var center = Plane.DotCoordinate(plane, Center);
        var reach = Vector3.Dot(Vector3.Abs(plane.Normal), Extents);
        if (center > reach) return PlaneIntersectionType.Front;
        if (center < -reach) return PlaneIntersectionType.Back;
        return PlaneIntersectionType.Intersecting;
    }

    public float? Intersects(Ray ray) => ray.Intersects(this);
}
