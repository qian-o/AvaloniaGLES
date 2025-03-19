using System;
using Silk.NET.Maths;
using Silk.NET.OpenGLES;

namespace AvaloniaGLES.Graphics;

internal unsafe class Pipeline : GraphicObject
{
    public Pipeline(GL gl, Shader vs, Shader fs) : base(gl)
    {
        Handle = GL.CreateProgram();

        GL.AttachShader(Handle, vs.Handle);
        GL.AttachShader(Handle, fs.Handle);
        GL.LinkProgram(Handle);

        string error = GL.GetProgramInfoLog(Handle);

        if (!string.IsNullOrEmpty(error))
        {
            GL.DeleteProgram(Handle);

            throw new Exception($"Link: {error}");
        }
    }

    public uint Handle { get; }

    public void Bind()
    {
        GL.UseProgram(Handle);
    }

    public void Unbind()
    {
        GL.UseProgram(0);
    }

    public int GetAttribLocation(string name)
    {
        return GL.GetAttribLocation(Handle, name);
    }

    public int GetUniformLocation(string name)
    {
        return GL.GetUniformLocation(Handle, name);
    }

    public void SetUniform(string name, int value)
    {
        GL.Uniform1(GetUniformLocation(name), value);
    }

    public void SetUniform(string name, float value)
    {
        GL.Uniform1(GetUniformLocation(name), value);
    }

    public void SetUniform(string name, Vector2D<float> value)
    {
        GL.Uniform2(GetUniformLocation(name), value.X, value.Y);
    }

    public void SetUniform(string name, Vector3D<float> value)
    {
        GL.Uniform3(GetUniformLocation(name), value.X, value.Y, value.Z);
    }

    public void SetUniform(string name, Vector4D<float> value)
    {
        GL.Uniform4(GetUniformLocation(name), value.X, value.Y, value.Z, value.W);
    }

    public void SetUniform(string name, Matrix2X2<float> value)
    {
        GL.UniformMatrix2(GetUniformLocation(name), 1, false, (float*)&value);
    }

    public void SetUniform(string name, Matrix3X3<float> value)
    {
        GL.UniformMatrix3(GetUniformLocation(name), 1, false, (float*)&value);
    }

    public void SetUniform(string name, Matrix4X4<float> value)
    {
        GL.UniformMatrix4(GetUniformLocation(name), 1, false, (float*)&value);
    }

    protected override void Destroy()
    {
        GL.DeleteProgram(Handle);
    }
}
