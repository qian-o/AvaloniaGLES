using System;
using System.Collections.Generic;
using AvaloniaGLES.Graphics;
using SharpGLTF.Schema2;
using SharpGLTF.Validation;
using Silk.NET.OpenGLES;
using Material = AvaloniaGLES.Graphics.Material;
using Mesh = AvaloniaGLES.Graphics.Mesh;
using Texture = AvaloniaGLES.Graphics.Texture;

namespace AvaloniaGLES.Helpers;

internal static unsafe class ModelFactory
{
    public static Model Load(GL gl, string path)
    {
        ModelRoot root = ModelRoot.Load(path, new ReadSettings() { Validation = ValidationMode.Skip });

        List<Mesh> meshes = [];
        List<Texture> textures = [];
        List<Material> materials = [];

        foreach (Node node in root.LogicalNodes)
        {
            ProcessNode(node, meshes);
        }

        throw new NotImplementedException();
    }

    private static void ProcessNode(Node node, List<Mesh> meshes)
    {
        if (node.Mesh is not null)
        {
            foreach (var item in node.Mesh.Primitives)
            {

            }
        }

        foreach (Node item in node.VisualChildren)
        {
            ProcessNode(item, meshes);
        }
    }
}   
