using Silk.NET.Maths;

namespace AvaloniaGLES.Graphics;

internal struct Mesh(Matrix4X4<float> world, Primitive[] primitives)
{
    public Matrix4X4<float> World = world;

    public Primitive[] Primitives = primitives;
}
