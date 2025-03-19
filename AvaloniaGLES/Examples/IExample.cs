using Silk.NET.OpenGLES;

namespace AvaloniaGLES.Examples;

internal interface IExample
{
    void OnLoad(GL gl);

    void OnUnload(GL gl);

    void OnUpdate(GL gl, double deltaSeconds);

    void OnRender(GL gl, double deltaSeconds);

    void OnResize(int width, int height);
}
