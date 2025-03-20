using Silk.NET.Maths;

namespace AvaloniaGLES.Graphics;

internal struct Material(Vector4D<float> baseColorFactor,
                         int baseColorTextureIndex,
                         int normalTextureIndex,
                         bool isOpaque,
                         float alphaCutoff,
                         bool doubleSided)
{
    public Vector4D<float> BaseColorFactor = baseColorFactor;

    public int BaseColorTextureIndex = baseColorTextureIndex;

    public int NormalTextureIndex = normalTextureIndex;

    public bool IsOpaque = isOpaque;

    public float AlphaCutoff = alphaCutoff;

    public bool DoubleSided = doubleSided;
}
