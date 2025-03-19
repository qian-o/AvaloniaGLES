using Silk.NET.Maths;

namespace AvaloniaGLES.Graphics;

internal struct Vertex(Vector3D<float> position = default,
                       Vector3D<float> normal = default,
                       Vector4D<float> color = default,
                       Vector2D<float> texCoord = default)
{
    public Vector3D<float> Position = position;

    public Vector3D<float> Normal = normal;

    public Vector4D<float> Color = color;

    public Vector2D<float> TexCoord = texCoord;
}
