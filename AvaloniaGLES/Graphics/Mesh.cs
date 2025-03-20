using Silk.NET.OpenGLES;

namespace AvaloniaGLES.Graphics;

internal unsafe struct Mesh(GL gl, Primitive[] primitives)
{
    public readonly void Draw()
    {
        foreach (Primitive primitive in primitives)
        {
            gl.DrawElements(GLEnum.Triangles,
                            primitive.IndexCount,
                            GLEnum.UnsignedInt,
                            (void*)(primitive.FirstIndex * sizeof(uint)));
        }
    }
}
