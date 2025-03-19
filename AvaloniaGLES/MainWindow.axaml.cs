using Avalonia.Controls;
using AvaloniaGLES.Examples;

namespace AvaloniaGLES;

public partial class MainWindow : Window
{
    public MainWindow()
    {
        InitializeComponent();

        IExample example = new MeshRender();

        GLES.OnLoad += example.OnLoad;
        GLES.OnUnload += example.OnUnload;
        GLES.OnUpdate += example.OnUpdate;
        GLES.OnRender += example.OnRender;
        GLES.OnResize += example.OnResize;
    }
}