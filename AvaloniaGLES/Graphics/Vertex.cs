using Silk.NET.Maths;

namespace AvaloniaGLES.Graphics;

internal struct Vertex(Vector3D<float> position,
                       Vector3D<float> normal,
                       Vector4D<float> color,
                       Vector2D<float> texCoord,
                       int materialIndex)
{
    public Vector3D<float> Position = position;

    public Vector3D<float> Normal = normal;

    public Vector4D<float> Color = color;

    public Vector2D<float> TexCoord = texCoord;

    public int MaterialIndex = materialIndex;
}
