using System;
using System.Threading;
using Silk.NET.OpenGLES;

namespace AvaloniaGLES.Graphics;

internal abstract class GraphicObject(GL gl) : IDisposable
{
    private volatile uint isDisposed;

    ~GraphicObject()
    {
        Dispose();
    }

    public GL GL { get; } = gl;

    public bool IsDisposed => isDisposed is not 0;

    public void Dispose()
    {
        if (Interlocked.Exchange(ref isDisposed, 1) is not 0)
        {
            return;
        }

        Destroy();

        GC.SuppressFinalize(this);
    }

    protected abstract void Destroy();
}
