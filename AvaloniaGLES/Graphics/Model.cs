using Silk.NET.OpenGLES;

namespace AvaloniaGLES.Graphics;

internal class Model(GL gl, string name, Mesh[] meshes) : GraphicObject(gl)
{
    public string Name { get; } = name;

    public Mesh[] Meshes { get; } = meshes;

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
