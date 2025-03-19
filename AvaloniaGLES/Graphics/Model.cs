namespace AvaloniaGLES.Graphics;

internal class Model(string name, Mesh[] meshes)
{
    public string Name { get; } = name;

    public Mesh[] Meshes { get; } = meshes;

    public void Init()
    {
        foreach (Mesh mesh in Meshes)
        {
            mesh.Init();
        }
    }

    public void Draw()
    {
        foreach (Mesh mesh in Meshes)
        {
            mesh.Draw();
        }
    }

    public void Destroy()
    {
        foreach (Mesh mesh in Meshes)
        {
            mesh.Destroy();
        }
    }
}
