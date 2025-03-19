using Silk.NET.OpenGLES;

namespace AvaloniaGLES.Examples;

internal class MeshRender : IExample
{
    public void OnLoad(GL gl)
    {
    }

    public void OnUnload(GL gl)
    {
    }

    public void OnRender(GL gl, double deltaSeconds)
    {
        gl.Clear((uint)GLEnum.ColorBufferBit | (uint)GLEnum.DepthBufferBit | (uint)GLEnum.StencilBufferBit);
        gl.ClearColor(0.0f, 0.0f, 1.0f, 1.0f);
    }

    public void OnUpdate(GL gl, double deltaSeconds)
    {
    }

    public void OnResize(int width, int height)
    {
    }
}
