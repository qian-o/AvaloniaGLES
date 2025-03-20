using System.Collections.Generic;
using System.IO;
using System.Numerics;
using AvaloniaGLES.Graphics;
using SharpGLTF.Materials;
using SharpGLTF.Memory;
using SharpGLTF.Schema2;
using SharpGLTF.Validation;
using Silk.NET.Maths;
using Silk.NET.OpenGLES;
using StbImageSharp;
using AlphaMode = SharpGLTF.Schema2.AlphaMode;
using GltfMaterial = SharpGLTF.Schema2.Material;
using GltfTexture = SharpGLTF.Schema2.Texture;
using Material = AvaloniaGLES.Graphics.Material;
using Mesh = AvaloniaGLES.Graphics.Mesh;
using Texture = AvaloniaGLES.Graphics.Texture;

namespace AvaloniaGLES.Helpers;

internal static unsafe class ModelFactory
{
    public static Model Load(GL gl, string path)
    {
        ModelRoot root = ModelRoot.Load(path, new ReadSettings() { Validation = ValidationMode.Skip });

        List<Vertex> vertices = [];
        List<uint> indices = [];
        List<Mesh> meshes = [];
        List<Texture> textures = [];
        List<Material> materials = [];

        foreach (Node node in root.LogicalNodes)
        {
            ProcessNode(gl, node, vertices, indices, meshes);
        }

        foreach (GltfTexture gltfTexture in root.LogicalTextures)
        {
            using Stream stream = gltfTexture.PrimaryImage.Content.Open();

            if (ImageInfo.FromStream(stream) is not ImageInfo imageInfo)
            {
                continue;
            }

            int width = imageInfo.Width;
            int height = imageInfo.Height;

            ImageResult image = ImageResult.FromStream(stream, ColorComponents.RedGreenBlueAlpha);

            Texture texture = new(gl);

            fixed (byte* data = image.Data)
            {
                texture.SetData(width, height, GLEnum.Rgba8, GLEnum.Rgba, data);
            }

            textures.Add(texture);
        }

        foreach (GltfMaterial gltfMaterial in root.LogicalMaterials)
        {
            Vector4D<float> baseColorFactor = Vector4D<float>.One;
            int baseColorTextureIndex = -1;
            int normalTextureIndex = -1;

            if (gltfMaterial.FindChannel(KnownChannel.BaseColor.ToString()) is MaterialChannel baseColor)
            {
                baseColorFactor = baseColor.Color.ToGeneric();

                if (baseColor.Texture != null)
                {
                    baseColorTextureIndex = baseColor.Texture.LogicalIndex;
                }
            }

            if (gltfMaterial.FindChannel(KnownChannel.Normal.ToString()) is MaterialChannel normal)
            {
                if (normal.Texture != null)
                {
                    normalTextureIndex = normal.Texture.LogicalIndex;
                }
            }

            materials.Add(new(baseColorFactor,
                              baseColorTextureIndex,
                              normalTextureIndex,
                              gltfMaterial.Alpha is AlphaMode.OPAQUE,
                              gltfMaterial.AlphaCutoff,
                              gltfMaterial.DoubleSided));
        }

        return new Model(gl,
                         [.. vertices],
                         [.. indices],
                         root.DefaultScene.Name,
                         [.. meshes],
                         [.. textures],
                         [.. materials]);
    }

    private static void ProcessNode(GL gl, Node node, List<Vertex> vertices, List<uint> indices, List<Mesh> meshes)
    {
        if (node.Mesh is not null)
        {
            List<Primitive> primitives = [];

            foreach (MeshPrimitive primitive in node.Mesh.Primitives)
            {
                uint firsetIndex = (uint)indices.Count;
                uint vertexOffset = (uint)vertices.Count;
                uint indexCount = 0;

                // Vertices
                {
                    IList<Vector3>? positionBuffer = null;
                    IList<Vector3>? normalBuffer = null;
                    IList<Vector3>? colorBuffer = null;
                    IList<Vector2>? texCoordBuffer = null;
                    uint vertexCount = 0;

                    if (primitive.VertexAccessors.TryGetValue("POSITION", out Accessor? positionAccessor))
                    {
                        positionBuffer = positionAccessor.AsVector3Array();

                        vertexCount = (uint)positionAccessor.Count;
                    }

                    if (primitive.VertexAccessors.TryGetValue("NORMAL", out Accessor? normalAccessor))
                    {
                        normalBuffer = normalAccessor.AsVector3Array();
                    }

                    if (primitive.VertexAccessors.TryGetValue("COLOR_0", out Accessor? colorAccessor))
                    {
                        colorBuffer = colorAccessor.AsVector3Array();
                    }

                    if (primitive.VertexAccessors.TryGetValue("TEXCOORD_0", out Accessor? texCoordAccessor))
                    {
                        texCoordBuffer = texCoordAccessor.AsVector2Array();
                    }

                    for (uint i = 0; i < vertexCount; i++)
                    {
                        Vector3D<float> position = positionBuffer is not null ? positionBuffer[(int)i].ToGeneric() : Vector3D<float>.Zero;
                        Vector3D<float> normal = normalBuffer is not null ? normalBuffer[(int)i].ToGeneric() : Vector3D<float>.Zero;
                        Vector3D<float> color = colorBuffer is not null ? colorBuffer[(int)i].ToGeneric() : Vector3D<float>.One;
                        Vector2D<float> texCoord = texCoordBuffer is not null ? texCoordBuffer[(int)i].ToGeneric() : Vector2D<float>.Zero;

                        position = Vector3D.Transform(position, node.WorldMatrix.ToGeneric());

                        vertices.Add(new Vertex(position,
                                                normal,
                                                color,
                                                texCoord));
                    }
                }

                // Indices
                {
                    if (primitive.IndexAccessor != null)
                    {
                        indexCount = (uint)primitive.IndexAccessor.Count;

                        IntegerArray indexBuffer = primitive.IndexAccessor.AsIndicesArray();

                        for (int i = 0; i < primitive.IndexAccessor.Count; i++)
                        {
                            indices.Add(indexBuffer[i] + vertexOffset);
                        }
                    }
                }

                primitives.Add(new Primitive(firsetIndex, indexCount, (uint)primitive.Material.LogicalIndex));
            }

            meshes.Add(new Mesh([.. primitives]));
        }

        foreach (Node item in node.VisualChildren)
        {
            ProcessNode(gl, item, vertices, indices, meshes);
        }
    }
}
