using Silk.NET.OpenGLES;

namespace AvaloniaGLES.Graphics;

internal class Model(GL gl, string name, Mesh[] meshes) : GraphicObject(gl)
{
    public string Name { get; } = name;

    public Mesh[] Meshes { get; } = meshes;

    public void VertexAttributePointer(uint index, int size, string fieldName)
    {
        foreach (Mesh mesh in Meshes)
        {
            mesh.VertexAttributePointer(index, size, fieldName);
        }
    }

    public void Draw()
    {
        foreach (Mesh mesh in Meshes)
        {
            mesh.Draw();
        }
    }

    protected override void Destroy()
    {
        foreach (Mesh mesh in Meshes)
        {
            mesh.Dispose();
        }
    }
}
