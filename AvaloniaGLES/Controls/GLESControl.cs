﻿using System;
using System.Diagnostics;
using Avalonia.Controls;
using Avalonia.OpenGL;
using Avalonia.OpenGL.Controls;
using Avalonia.Threading;
using Silk.NET.OpenGLES;

namespace AvaloniaGLES.Controls;

public delegate void DeltaAction(GL gl, double deltaSeconds);

public delegate void SizeAction(int width, int height);

internal class GLESControl : OpenGlControlBase
{
    private readonly Stopwatch _stopwatch = new();

    public GL? GL { get; private set; }

    public event Action<GL>? OnLoad;

    public event Action<GL>? OnUnload;

    public event DeltaAction? OnUpdate;

    public event DeltaAction? OnRender;

    public event SizeAction? OnResize;

    protected override void OnOpenGlInit(GlInterface gl)
    {
        _stopwatch.Restart();

        GL?.Dispose();
        GL = GL.GetApi(gl.GetProcAddress);

        OnLoad?.Invoke(GL);
        OnResize?.Invoke((int)Bounds.Width, (int)Bounds.Height);
    }

    protected override void OnOpenGlDeinit(GlInterface gl)
    {
        if (GL is not null)
        {
            OnUnload?.Invoke(GL);

            GL.Dispose();
            GL = null;
        }

        _stopwatch.Stop();
    }

    protected override void OnOpenGlRender(GlInterface gl, int fb)
    {
        if (GL is not null)
        {
            RenderSize(out double width, out double height);

            GL.BindFramebuffer(GLEnum.Framebuffer, (uint)fb);

            GL.Viewport(0, 0, (uint)width, (uint)height);

            OnUpdate?.Invoke(GL, _stopwatch.Elapsed.TotalSeconds);

            OnRender?.Invoke(GL, _stopwatch.Elapsed.TotalSeconds);
        }

        Dispatcher.UIThread.Post(RequestNextFrameRendering, DispatcherPriority.Render);
    }

    protected override void OnSizeChanged(SizeChangedEventArgs e)
    {
        base.OnSizeChanged(e);

        RenderSize(out double width, out double height);

        OnResize?.Invoke((int)width, (int)height);
    }

    private void RenderSize(out double width, out double height)
    {
        width = Bounds.Width;
        height = Bounds.Height;

        if (VisualRoot is not null)
        {
            width *= VisualRoot.RenderScaling;
            height *= VisualRoot.RenderScaling;
        }
    }
}
