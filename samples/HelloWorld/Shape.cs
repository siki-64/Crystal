using System.Numerics;
using Crystal;
using Crystal.Graphics;

namespace HelloWorld;

internal abstract class Shape
{
    internal const int MaxVertexCount = 6;

    internal abstract int WriteVertices(Span<VertexPositionColor> vertices);
}

internal sealed class Triangle : Shape
{
    internal override int WriteVertices(Span<VertexPositionColor> vertices)
    {
        vertices[0] = new(new Vector3(0f, 0.6f, 0f), Color.Red);
        vertices[1] = new(new Vector3(0.52f, -0.3f, 0f), new Color(0, 255, 0));
        vertices[2] = new(new Vector3(-0.52f, -0.3f, 0f), Color.Blue);
        return 3;
    }
}

internal sealed class Square : Shape
{
    internal override int WriteVertices(Span<VertexPositionColor> vertices)
    {
        var topLeft = new VertexPositionColor(new Vector3(-0.45f, 0.45f, 0f), Color.Red);
        var topRight = new VertexPositionColor(new Vector3(0.45f, 0.45f, 0f), new Color(255, 255, 0));
        var bottomRight = new VertexPositionColor(new Vector3(0.45f, -0.45f, 0f), new Color(0, 255, 0));
        var bottomLeft = new VertexPositionColor(new Vector3(-0.45f, -0.45f, 0f), Color.Blue);
        vertices[0] = topLeft;
        vertices[1] = topRight;
        vertices[2] = bottomRight;
        vertices[3] = topLeft;
        vertices[4] = bottomRight;
        vertices[5] = bottomLeft;
        return 6;
    }
}
