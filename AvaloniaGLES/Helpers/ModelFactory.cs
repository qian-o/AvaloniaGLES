using System;
using System.Collections.Generic;
using System.IO;
using AvaloniaGLES.Graphics;
using Silk.NET.Assimp;
using Silk.NET.Maths;
using Silk.NET.OpenGLES;
using AMesh = Silk.NET.Assimp.Mesh;
using Mesh = AvaloniaGLES.Graphics.Mesh;

namespace AvaloniaGLES.Helpers;

internal static unsafe class ModelFactory
{
    private static readonly Assimp assimp;

    static ModelFactory()
    {
        assimp = Assimp.GetApi();
    }

    public static Model Load(GL gl, string path)
    {
        const PostProcessSteps steps = PostProcessSteps.CalculateTangentSpace
                                       | PostProcessSteps.Triangulate
                                       | PostProcessSteps.GenerateNormals
                                       | PostProcessSteps.GenerateSmoothNormals
                                       | PostProcessSteps.GenerateUVCoords
                                       | PostProcessSteps.OptimizeMeshes
                                       | PostProcessSteps.OptimizeGraph
                                       | PostProcessSteps.PreTransformVertices
                                       | PostProcessSteps.FlipUVs;

        Scene* scene = assimp.ImportFile(path, (uint)steps);

        if (scene is null)
        {
            throw new Exception($"Failed to load model from {path}");
        }

        string name = Path.GetFileNameWithoutExtension(path);
        List<Mesh> meshes = [];

        ProcessNode(gl, scene, scene->MRootNode, meshes);

        return new Model(gl, name, [.. meshes]);
    }

    private static void ProcessNode(GL gl, Scene* scene, Node* node, List<Mesh> meshes)
    {
        for (uint i = 0; i < node->MNumMeshes; i++)
        {
            meshes.Add(Mesh(gl, scene->MMeshes[node->MMeshes[i]]));
        }

        for (uint i = 0; i < node->MNumChildren; i++)
        {
            ProcessNode(gl, scene, node->MChildren[i], meshes);
        }
    }

    private static Mesh Mesh(GL gl, AMesh* mesh)
    {
        Vertex[] vertices = new Vertex[mesh->MNumVertices];
        uint[] indices = new uint[mesh->MNumFaces * 3];

        for (uint i = 0; i < mesh->MNumVertices; i++)
        {
            vertices[i].Position = (*&mesh->MVertices[i]).ToGeneric();
            vertices[i].Normal = (*&mesh->MNormals[i]).ToGeneric();
            vertices[i].Color = (*&mesh->MColors[(int)i][0]).ToGeneric();

            Vector3D<float> texCoord = (*&mesh->MTextureCoords[(int)i][0]).ToGeneric();

            vertices[i].TexCoord = new(texCoord.X, texCoord.Y);
        }

        for (uint i = 0; i < mesh->MNumFaces; i++)
        {
            Face face = mesh->MFaces[i];

            for (uint j = 0; j < face.MNumIndices; j++)
            {
                indices[(i * 3) + j] = face.MIndices[j];
            }
        }

        return new(gl, vertices, indices);
    }
}
