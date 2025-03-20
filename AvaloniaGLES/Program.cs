using System;
using Avalonia;
using AvaloniaGLES.Examples;
using Silk.NET.Maths;
using Silk.NET.OpenGLES;
using Silk.NET.Windowing;

namespace AvaloniaGLES;

internal class Program
{
    private static readonly bool Debug = true;

    [STAThread]
    public static void Main(string[] args)
    {
        if (Debug)
        {
            IExample example = new MeshRender();

            WindowOptions windowOptions = WindowOptions.Default;
            windowOptions.Title = "AvaloniaGLES";
            windowOptions.Size = new Vector2D<int>(1280, 720);
            windowOptions.API = new GraphicsAPI(ContextAPI.OpenGLES, new APIVersion(3, 0));
            windowOptions.VSync = false;

            IWindow window = Window.Create(windowOptions);
            window.Initialize();

            GL gl = window.CreateOpenGLES();

            example.OnLoad(gl);
            example.OnResize(window.Size.X, window.Size.Y);

            window.Closing += () => example.OnUnload(gl);
            window.Update += (deltaSeconds) => example.OnUpdate(gl, deltaSeconds);
            window.Render += (deltaSeconds) => example.OnRender(gl, deltaSeconds);
            window.Resize += (size) =>
            {
                gl.Viewport(0, 0, (uint)size.X, (uint)size.Y);

                example.OnResize(size.X, size.Y);
            };

            window.Run();
        }
        else
        {
            BuildAvaloniaApp().StartWithClassicDesktopLifetime(args);
        }
    }

    public static AppBuilder BuildAvaloniaApp()
    {
        return AppBuilder.Configure<App>()
                         .UsePlatformDetect()
                         .WithInterFont()
                         .LogToTrace();
    }
}
