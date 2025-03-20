using Silk.NET.Maths;

namespace AvaloniaGLES.Graphics;

internal struct Material(Vector4D<float> baseColorFactor,
                         uint baseColorTextureIndex,
                         uint normalTextureIndex,
                         bool isOpaque,
                         float alphaCutoff,
                         bool doubleSided)
{
    public Vector4D<float> BaseColorFactor = baseColorFactor;

    public uint BaseColorTextureIndex = baseColorTextureIndex;

    public uint NormalTextureIndex = normalTextureIndex;

    public bool IsOpaque = isOpaque;

    public float AlphaCutoff = alphaCutoff;

    public bool DoubleSided = doubleSided;
}
