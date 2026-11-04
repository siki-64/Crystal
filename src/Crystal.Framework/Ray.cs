using System.Numerics;

namespace Crystal;

public readonly record struct Ray(Vector3 Position, Vector3 Direction)
{
    public float? Intersects(BoundingBox box)
    {
        var nearest = 0f;
        var farthest = float.PositiveInfinity;
        for (var axis = 0; axis < 3; axis++)
        {
            var origin = Position[axis];
            var direction = Direction[axis];
            var min = box.Min[axis];
            var max = box.Max[axis];
            if (direction == 0f)
            {
                if (origin < min || origin > max) return null;
                continue;
            }
            var inverse = 1f / direction;
            var entry = (min - origin) * inverse;
            var exit = (max - origin) * inverse;
            if (entry > exit) (entry, exit) = (exit, entry);
            nearest = MathF.Max(nearest, entry);
            farthest = MathF.Min(farthest, exit);
            if (nearest > farthest) return null;
        }
        return nearest;
    }

    public float? Intersects(BoundingSphere sphere)
    {
        var offset = sphere.Center - Position;
        var radiusSquared = sphere.Radius * sphere.Radius;
        var distanceSquared = offset.LengthSquared();
        if (distanceSquared <= radiusSquared) return 0f;
        var directionLengthSquared = Direction.LengthSquared();
        if (directionLengthSquared == 0f) return null;
        var along = Vector3.Dot(offset, Direction) / directionLengthSquared;
        if (along < 0f) return null;
        var closestSquared = distanceSquared - along * along * directionLengthSquared;
        if (closestSquared > radiusSquared) return null;
        return along - MathF.Sqrt((radiusSquared - closestSquared) / directionLengthSquared);
    }

    public float? Intersects(Plane plane)
    {
        var denominator = Vector3.Dot(plane.Normal, Direction);
        if (MathF.Abs(denominator) < 1e-6f) return null;
        var distance = -Plane.DotCoordinate(plane, Position) / denominator;
        return distance < 0f ? null : distance;
    }
}
