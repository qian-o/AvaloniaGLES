using System.Runtime.InteropServices;
using Silk.NET.OpenGLES;

namespace AvaloniaGLES.Graphics;

internal unsafe class Mesh(GL gl, Vertex[] vertices, uint[] indices)
{
    private uint _vao;
    private uint _vbo;
    private uint _ebo;

    public Vertex[] Vertices { get; } = vertices;

    public uint[] Indices { get; } = indices;

    public void Init()
    {
        _vao = gl.GenVertexArray();
        _vbo = gl.GenBuffer();
        _ebo = gl.GenBuffer();

        gl.BindVertexArray(_vao);

        gl.BindBuffer(GLEnum.ArrayBuffer, _vbo);
        gl.BufferData<Vertex>(GLEnum.ArrayBuffer, (uint)(Vertices.Length * sizeof(Vertex)), Vertices, GLEnum.StaticDraw);

        gl.BindBuffer(GLEnum.ElementArrayBuffer, _ebo);
        gl.BufferData<uint>(GLEnum.ElementArrayBuffer, (uint)(Indices.Length * sizeof(uint)), Indices, GLEnum.StaticDraw);

        gl.BindVertexArray(0);
    }

    public void VertexAttributePointer(uint index, int size, string fieldName)
    {
        gl.BindVertexArray(_vao);

        gl.BindBuffer(GLEnum.ArrayBuffer, _vbo);

        gl.VertexAttribPointer(index, size, GLEnum.Float, false, (uint)sizeof(Vertex), (void*)Marshal.OffsetOf<Vertex>(fieldName));
        gl.EnableVertexAttribArray(index);

        gl.BindBuffer(GLEnum.ArrayBuffer, 0);

        gl.BindVertexArray(0);
    }

    public void Draw()
    {
        gl.BindVertexArray(_vao);
        gl.DrawElements(GLEnum.Triangles, (uint)Indices.Length, GLEnum.UnsignedInt, null);
        gl.BindVertexArray(0);
    }

    public void Destroy()
    {
        gl.DeleteBuffer(_vbo);
        gl.DeleteBuffer(_ebo);
        gl.DeleteVertexArray(_vao);
    }
}
