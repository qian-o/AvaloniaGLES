using System.Runtime.InteropServices;
using Silk.NET.OpenGLES;

namespace AvaloniaGLES.Graphics;

internal unsafe class Mesh : GraphicObject
{
    private readonly uint vao;
    private readonly uint vbo;
    private readonly uint ebo;
    private readonly uint count;

    public Mesh(GL gl, Vertex[] vertices, uint[] indices, uint materialIndex) : base(gl)
    {
        vao = GL.GenVertexArray();
        vbo = GL.GenBuffer();
        ebo = GL.GenBuffer();
        count = (uint)indices.Length;

        GL.BindVertexArray(vao);

        GL.BindBuffer(GLEnum.ArrayBuffer, vbo);
        GL.BufferData<Vertex>(GLEnum.ArrayBuffer, (uint)(vertices.Length * sizeof(Vertex)), vertices, GLEnum.StaticDraw);

        GL.BindBuffer(GLEnum.ElementArrayBuffer, ebo);
        GL.BufferData<uint>(GLEnum.ElementArrayBuffer, (uint)(indices.Length * sizeof(uint)), indices, GLEnum.StaticDraw);

        GL.BindVertexArray(0);

        MaterialIndex = materialIndex;
    }

    public uint MaterialIndex { get; }

    public void VertexAttributePointer(uint index, int size, string fieldName)
    {
        GL.BindVertexArray(vao);

        GL.BindBuffer(GLEnum.ArrayBuffer, vbo);

        GL.VertexAttribPointer(index, size, GLEnum.Float, false, (uint)sizeof(Vertex), (void*)Marshal.OffsetOf<Vertex>(fieldName));
        GL.EnableVertexAttribArray(index);

        GL.BindBuffer(GLEnum.ArrayBuffer, 0);

        GL.BindVertexArray(0);
    }

    public void Draw()
    {
        GL.BindVertexArray(vao);
        GL.DrawElements(GLEnum.Triangles, count, GLEnum.UnsignedInt, null);
        GL.BindVertexArray(0);
    }

    protected override void Destroy()
    {
        GL.DeleteBuffer(vbo);
        GL.DeleteBuffer(ebo);
        GL.DeleteVertexArray(vao);
    }
}
