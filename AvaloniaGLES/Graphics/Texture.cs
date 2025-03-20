using Silk.NET.OpenGLES;

namespace AvaloniaGLES.Graphics;

internal unsafe class Texture : GraphicObject
{
    public Texture(GL gl) : base(gl)
    {
        Handle = GL.GenTexture();

        GL.BindTexture(GLEnum.Texture2D, Handle);

        GL.TexParameter(GLEnum.Texture2D, GLEnum.TextureMinFilter, (int)GLEnum.Nearest);
        GL.TexParameter(GLEnum.Texture2D, GLEnum.TextureMagFilter, (int)GLEnum.Nearest);
        GL.TexParameter(GLEnum.Texture2D, GLEnum.TextureWrapS, (int)GLEnum.ClampToEdge);
        GL.TexParameter(GLEnum.Texture2D, GLEnum.TextureWrapT, (int)GLEnum.ClampToEdge);

        GL.BindTexture(GLEnum.Texture2D, 0);
    }

    public uint Handle { get; }

    public void Bind()
    {
        GL.BindTexture(GLEnum.Texture2D, Handle);
    }

    public void SetData(int width, int height, GLEnum internalformat, GLEnum format, byte* data)
    {
        GL.BindTexture(GLEnum.Texture2D, Handle);

        GL.TexImage2D(GLEnum.Texture2D,
                      0,
                      (int)internalformat,
                      (uint)width,
                      (uint)height,
                      0,
                      format,
                      GLEnum.UnsignedByte,
                      data);

        GL.BindTexture(GLEnum.Texture2D, 0);
    }

    protected override void Destroy()
    {
        GL.DeleteTexture(Handle);
    }
}
