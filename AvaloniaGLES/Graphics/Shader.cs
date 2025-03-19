using Silk.NET.OpenGLES;

namespace AvaloniaGLES.Graphics;

internal class Shader : GraphicObject
{
    public Shader(GL gl, ShaderType shaderType, string source) : base(gl)
    {
        Handle = GL.CreateShader(shaderType);

        GL.ShaderSource(Handle, source);
        GL.CompileShader(Handle);
    }

    public uint Handle { get; }

    protected override void Destroy()
    {
        GL.DeleteShader(Handle);
    }
}
