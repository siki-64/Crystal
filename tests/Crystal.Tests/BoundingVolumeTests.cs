using System.Numerics;

namespace Crystal.Tests;

[TestClass]
public sealed class BoundingVolumeTests
{
    private const float Tolerance = 1e-4f;

    private static readonly Matrix4x4 View = Matrix4x4.CreateLookAt(Vector3.Zero, -Vector3.UnitZ, Vector3.UnitY);
    private static readonly Matrix4x4 Projection = Matrix4x4.CreatePerspectiveFieldOfView(MathF.PI / 2f, 1f, 1f, 100f);
    private static readonly BoundingFrustum Frustum = new(View * Projection);

    [TestMethod]
    public void BoxesAreBuiltMergedAndTransformed()
    {
        var box = BoundingBox.CreateFromPoints([new Vector3(1, -2, 3), new Vector3(-1, 4, 0), new Vector3(0, 0, 5)]);
        Assert.AreEqual(new BoundingBox(new Vector3(-1, -2, 0), new Vector3(1, 4, 5)), box);
        Assert.ThrowsExactly<ArgumentException>(() => BoundingBox.CreateFromPoints([]));

        var merged = BoundingBox.CreateMerged(box, new BoundingBox(new Vector3(5), new Vector3(6)));
        Assert.AreEqual(new BoundingBox(new Vector3(-1, -2, 0), new Vector3(6)), merged);

        var unit = new BoundingBox(-Vector3.One, Vector3.One);
        var rotated = unit.Transform(Matrix4x4.CreateRotationZ(MathF.PI / 4f) * Matrix4x4.CreateTranslation(10, 0, 0));
        Assert.AreEqual(10f - MathF.Sqrt(2f), rotated.Min.X, Tolerance);
        Assert.AreEqual(MathF.Sqrt(2f), rotated.Max.Y, Tolerance);
        Assert.AreEqual(1f, rotated.Max.Z, Tolerance);

        Span<Vector3> corners = stackalloc Vector3[BoundingBox.CornerCount];
        unit.GetCorners(corners);
        Assert.AreEqual(unit, BoundingBox.CreateFromPoints(corners));
    }

    [TestMethod]
    public void BoxAndSphereContainment()
    {
        var box = new BoundingBox(Vector3.Zero, new Vector3(10));
        Assert.AreEqual(ContainmentType.Contains, box.Contains(new Vector3(5)));
        Assert.AreEqual(ContainmentType.Disjoint, box.Contains(new Vector3(11)));
        Assert.AreEqual(ContainmentType.Contains, box.Contains(new BoundingBox(new Vector3(1), new Vector3(2))));
        Assert.AreEqual(ContainmentType.Intersects, box.Contains(new BoundingBox(new Vector3(9), new Vector3(12))));
        Assert.AreEqual(ContainmentType.Contains, box.Contains(new BoundingSphere(new Vector3(5), 2)));
        Assert.AreEqual(ContainmentType.Intersects, box.Contains(new BoundingSphere(new Vector3(-1, 5, 5), 2)));
        Assert.AreEqual(ContainmentType.Disjoint, box.Contains(new BoundingSphere(new Vector3(-2, -2, -2), 2)));

        var sphere = new BoundingSphere(Vector3.Zero, 2);
        Assert.AreEqual(ContainmentType.Contains, sphere.Contains(new BoundingBox(-Vector3.One, Vector3.One)));
        Assert.AreEqual(ContainmentType.Intersects, sphere.Contains(new BoundingBox(Vector3.One, new Vector3(3))));
        Assert.AreEqual(ContainmentType.Contains, sphere.Contains(new BoundingSphere(Vector3.UnitX, 1)));
        Assert.AreEqual(ContainmentType.Disjoint, sphere.Contains(new BoundingSphere(new Vector3(4, 0, 0), 1)));
        Assert.AreEqual(PlaneIntersectionType.Back, sphere.Intersects(new Plane(Vector3.UnitX, -3)));
        Assert.AreEqual(PlaneIntersectionType.Front, box.Intersects(new Plane(Vector3.UnitX, 11)));
        Assert.AreEqual(PlaneIntersectionType.Intersecting, box.Intersects(new Plane(Vector3.UnitX, -5)));
    }

    [TestMethod]
    public void SpheresAreBuiltMergedAndTransformed()
    {
        var sphere = BoundingSphere.CreateFromPoints([new Vector3(-1, 0, 0), new Vector3(1, 0, 0), new Vector3(0, 0.5f, 0)]);
        Assert.AreEqual(new Vector3(0, 0.25f, 0), sphere.Center);
        Assert.IsGreaterThanOrEqualTo(MathF.Sqrt(1f + 0.0625f) - Tolerance, sphere.Radius);

        var merged = BoundingSphere.CreateMerged(new BoundingSphere(Vector3.Zero, 1), new BoundingSphere(new Vector3(4, 0, 0), 1));
        Assert.AreEqual(new BoundingSphere(new Vector3(2, 0, 0), 3), merged);

        var transformed = new BoundingSphere(Vector3.UnitX, 1).Transform(Matrix4x4.CreateScale(1, 3, 2) * Matrix4x4.CreateTranslation(0, 0, 5));
        Assert.AreEqual(new Vector3(1, 0, 5), transformed.Center);
        Assert.AreEqual(3f, transformed.Radius, Tolerance);
    }

    [TestMethod]
    public void RaysReportTheDistanceToTheFirstHit()
    {
        var ray = new Ray(new Vector3(-5, 0, 0), Vector3.UnitX);
        Assert.AreEqual(4f, ray.Intersects(new BoundingBox(-Vector3.One, Vector3.One))!.Value, Tolerance);
        Assert.AreEqual(3f, ray.Intersects(new BoundingSphere(Vector3.Zero, 2))!.Value, Tolerance);
        Assert.AreEqual(5f, ray.Intersects(new Plane(Vector3.UnitX, 0))!.Value, Tolerance);
        Assert.IsNull(ray.Intersects(new BoundingBox(new Vector3(-1, 2, -1), new Vector3(1, 3, 1))));
        Assert.IsNull(ray.Intersects(new BoundingSphere(new Vector3(-10, 0, 0), 1)));
        Assert.IsNull(ray.Intersects(new Plane(Vector3.UnitY, 0)));
        Assert.AreEqual(0f, new Ray(Vector3.Zero, Vector3.UnitY).Intersects(new BoundingBox(-Vector3.One, Vector3.One)));
        Assert.AreEqual(0f, new Ray(Vector3.Zero, Vector3.UnitY).Intersects(new BoundingSphere(Vector3.Zero, 1)));
    }

    [TestMethod]
    public void FrustumPlanesFaceOutward()
    {
        Assert.AreEqual(ContainmentType.Contains, Frustum.Contains(new Vector3(0, 0, -10)));
        Assert.IsGreaterThan(0f, Plane.DotCoordinate(Frustum.Near, new Vector3(0, 0, -0.5f)));
        Assert.IsGreaterThan(0f, Plane.DotCoordinate(Frustum.Far, new Vector3(0, 0, -101)));
        Assert.IsGreaterThan(0f, Plane.DotCoordinate(Frustum.Left, new Vector3(-20, 0, -10)));
        Assert.IsGreaterThan(0f, Plane.DotCoordinate(Frustum.Right, new Vector3(20, 0, -10)));
        Assert.IsGreaterThan(0f, Plane.DotCoordinate(Frustum.Top, new Vector3(0, 20, -10)));
        Assert.IsGreaterThan(0f, Plane.DotCoordinate(Frustum.Bottom, new Vector3(0, -20, -10)));
        Assert.AreEqual(1f, Frustum.Near.Normal.Length(), Tolerance);
    }

    [TestMethod]
    public void FrustumCornersLieOnTheNearAndFarPlanes()
    {
        Span<Vector3> corners = stackalloc Vector3[BoundingFrustum.CornerCount];
        Frustum.GetCorners(corners);
        AssertVector(new Vector3(-1, 1, -1), corners[0]);
        AssertVector(new Vector3(1, -1, -1), corners[2]);
        AssertVector(new Vector3(-100, 100, -100), corners[4]);
        AssertVector(new Vector3(100, -100, -100), corners[6]);
    }

    [TestMethod]
    public void FrustumClassifiesBoxesAndSpheres()
    {
        Assert.AreEqual(ContainmentType.Contains, Frustum.Contains(new BoundingBox(new Vector3(-1, -1, -11), new Vector3(1, 1, -9))));
        Assert.AreEqual(ContainmentType.Intersects, Frustum.Contains(new BoundingBox(new Vector3(-1, -1, -2), new Vector3(1, 1, 0))));
        Assert.AreEqual(ContainmentType.Disjoint, Frustum.Contains(new BoundingBox(new Vector3(-1, -1, 1), new Vector3(1, 1, 3))));
        Assert.AreEqual(ContainmentType.Disjoint, Frustum.Contains(new BoundingBox(new Vector3(30, -1, -11), new Vector3(32, 1, -9))));
        Assert.IsTrue(Frustum.Intersects(new BoundingSphere(new Vector3(0, 0, -50), 1)));
        Assert.AreEqual(ContainmentType.Intersects, Frustum.Contains(new BoundingSphere(new Vector3(0, 0, -100), 1)));
        Assert.IsFalse(Frustum.Intersects(new BoundingSphere(new Vector3(0, 0, 5), 1)));
        Assert.AreEqual(new BoundingFrustum(View * Projection), Frustum);
    }

    private static void AssertVector(Vector3 expected, Vector3 actual)
    {
        Assert.AreEqual(expected.X, actual.X, Tolerance * 100);
        Assert.AreEqual(expected.Y, actual.Y, Tolerance * 100);
        Assert.AreEqual(expected.Z, actual.Z, Tolerance * 100);
    }
}
