using System.Numerics;

namespace Crystal;

public readonly record struct BoundingSphere(Vector3 Center, float Radius)
{
    public static BoundingSphere CreateFromPoints(ReadOnlySpan<Vector3> points)
    {
        var center = BoundingBox.CreateFromPoints(points).Center;
        var radiusSquared = 0f;
        foreach (var point in points) radiusSquared = MathF.Max(radiusSquared, Vector3.DistanceSquared(center, point));
        return new BoundingSphere(center, MathF.Sqrt(radiusSquared));
    }

    public static BoundingSphere CreateFromBoundingBox(BoundingBox box) => new(box.Center, box.Extents.Length());

    public static BoundingSphere CreateMerged(BoundingSphere first, BoundingSphere second)
    {
        var offset = second.Center - first.Center;
        var distance = offset.Length();
        if (distance + second.Radius <= first.Radius) return first;
        if (distance + first.Radius <= second.Radius) return second;
        var radius = (distance + first.Radius + second.Radius) * 0.5f;
        return new BoundingSphere(first.Center + offset * ((radius - first.Radius) / distance), radius);
    }

    public BoundingSphere Transform(Matrix4x4 matrix)
    {
        var scaleSquared = MathF.Max(
            new Vector3(matrix.M11, matrix.M12, matrix.M13).LengthSquared(),
            MathF.Max(new Vector3(matrix.M21, matrix.M22, matrix.M23).LengthSquared(), new Vector3(matrix.M31, matrix.M32, matrix.M33).LengthSquared()));
        return new BoundingSphere(Vector3.Transform(Center, matrix), Radius * MathF.Sqrt(scaleSquared));
    }

    public ContainmentType Contains(Vector3 point) =>
        Vector3.DistanceSquared(Center, point) <= Radius * Radius ? ContainmentType.Contains : ContainmentType.Disjoint;

    public ContainmentType Contains(BoundingBox box)
    {
        if (!box.Intersects(this)) return ContainmentType.Disjoint;
        var farthest = Vector3.Max(Vector3.Abs(box.Min - Center), Vector3.Abs(box.Max - Center));
        return farthest.LengthSquared() <= Radius * Radius ? ContainmentType.Contains : ContainmentType.Intersects;
    }

    public ContainmentType Contains(BoundingSphere sphere)
    {
        var distance = Vector3.Distance(Center, sphere.Center);
        if (distance > Radius + sphere.Radius) return ContainmentType.Disjoint;
        return distance + sphere.Radius <= Radius ? ContainmentType.Contains : ContainmentType.Intersects;
    }

    public bool Intersects(BoundingBox box) => box.Intersects(this);

    public bool Intersects(BoundingSphere sphere)
    {
        var radius = Radius + sphere.Radius;
        return Vector3.DistanceSquared(Center, sphere.Center) <= radius * radius;
    }

    public PlaneIntersectionType Intersects(Plane plane)
    {
        var distance = Plane.DotCoordinate(plane, Center);
        if (distance > Radius) return PlaneIntersectionType.Front;
        if (distance < -Radius) return PlaneIntersectionType.Back;
        return PlaneIntersectionType.Intersecting;
    }

    public float? Intersects(Ray ray) => ray.Intersects(this);
}
