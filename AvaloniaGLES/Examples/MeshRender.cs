using System;
using System.IO;
using AvaloniaGLES.Graphics;
using AvaloniaGLES.Helpers;
using Silk.NET.Maths;
using Silk.NET.OpenGLES;
using Shader = AvaloniaGLES.Graphics.Shader;

namespace AvaloniaGLES.Examples;

internal class MeshRender : IExample
{
    private Vector2D<int> size = Vector2D<int>.Zero;

    private Model model = null!;
    private Pipeline pipeline = null!;

    public void OnLoad(GL gl)
    {
        model = ModelFactory.Load(gl, Path.Combine(AppContext.BaseDirectory,
                                                   "Assets",
                                                   "Models",
                                                   "Sponza",
                                                   "glTF",
                                                   "Sponza.gltf"));

        using Shader vs = new(gl, ShaderType.VertexShader, File.ReadAllText(Path.Combine(AppContext.BaseDirectory, "Assets", "Shaders", "Shader.vert")));
        using Shader fs = new(gl, ShaderType.FragmentShader, File.ReadAllText(Path.Combine(AppContext.BaseDirectory, "Assets", "Shaders", "Shader.frag")));

        pipeline = new(gl, vs, fs);
    }

    public void OnUnload(GL gl)
    {
        pipeline.Dispose();
        model.Dispose();
    }

    public void OnRender(GL gl, double deltaSeconds)
    {
        gl.Clear((uint)GLEnum.ColorBufferBit | (uint)GLEnum.DepthBufferBit | (uint)GLEnum.StencilBufferBit);
        gl.ClearColor(0.0f, 0.0f, 0.0f, 1.0f);

        pipeline.Bind();

        model.VertexAttributePointer((uint)pipeline.GetAttribLocation("In_Position"), 3, nameof(Vertex.Position));
        model.VertexAttributePointer((uint)pipeline.GetAttribLocation("In_Normal"), 3, nameof(Vertex.Normal));
        model.VertexAttributePointer((uint)pipeline.GetAttribLocation("In_Color"), 4, nameof(Vertex.Color));
        model.VertexAttributePointer((uint)pipeline.GetAttribLocation("In_TexCoord"), 2, nameof(Vertex.TexCoord));

        pipeline.SetUniform("Model", Matrix4X4<float>.Identity);
        pipeline.SetUniform("View", Matrix4X4.CreateLookAt(new Vector3D<float>(7.8f, 2.1f, 0.0f), Vector3D<float>.Zero, Vector3D<float>.UnitY));
        pipeline.SetUniform("Projection", Matrix4X4.CreatePerspectiveFieldOfView(MathF.PI / 4, (float)size.X / size.Y, 0.1f, 1000.0f));

        model.Draw();
    }

    public void OnUpdate(GL gl, double deltaSeconds)
    {
    }

    public void OnResize(int width, int height)
    {
        size = new Vector2D<int>(width, height);
    }
}
