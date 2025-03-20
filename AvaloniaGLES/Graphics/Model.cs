using System.Runtime.InteropServices;
using Silk.NET.OpenGLES;

namespace AvaloniaGLES.Graphics;

internal unsafe class Model : GraphicObject
{
    private readonly uint vao;
    private readonly uint vbo;
    private readonly uint ebo;

    public Model(GL gl,
                 Vertex[] vertices,
                 uint[] indices,
                 string name,
                 Mesh[] meshes,
                 Texture[] textures,
                 Material[] materials) : base(gl)
    {
        vao = GL.GenVertexArray();
        vbo = GL.GenBuffer();
        ebo = GL.GenBuffer();

        GL.BindVertexArray(vao);

        GL.BindBuffer(GLEnum.ArrayBuffer, vbo);
        GL.BufferData<Vertex>(GLEnum.ArrayBuffer, (uint)(vertices.Length * sizeof(Vertex)), vertices, GLEnum.StaticDraw);

        GL.BindBuffer(GLEnum.ElementArrayBuffer, ebo);
        GL.BufferData<uint>(GLEnum.ElementArrayBuffer, (uint)(indices.Length * sizeof(uint)), indices, GLEnum.StaticDraw);

        GL.BindVertexArray(0);

        Name = name;
        Meshes = meshes;
        Textures = textures;
        Materials = materials;
    }

    public string Name { get; }

    public Mesh[] Meshes { get; }

    public Texture[] Textures { get; }

    public Material[] Materials { get; }

    public void VertexAttributePointer(uint index, int size, GLEnum type, string fieldName)
    {
        GL.BindVertexArray(vao);

        GL.BindBuffer(GLEnum.ArrayBuffer, vbo);

        GL.VertexAttribPointer(index, size, type, false, (uint)sizeof(Vertex), (void*)Marshal.OffsetOf<Vertex>(fieldName));
        GL.EnableVertexAttribArray(index);

        GL.BindBuffer(GLEnum.ArrayBuffer, 0);

        GL.BindVertexArray(0);
    }

    public void Draw()
    {
        GL.BindVertexArray(vao);

        foreach (Mesh mesh in Meshes)
        {
            mesh.Draw();
        }

        GL.BindVertexArray(0);
    }

    protected override void Destroy()
    {
        GL.DeleteBuffer(vbo);
        GL.DeleteBuffer(ebo);
        GL.DeleteVertexArray(vao);
    }
}
