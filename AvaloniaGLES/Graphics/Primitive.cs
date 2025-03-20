namespace AvaloniaGLES.Graphics;

internal struct Primitive(uint firstIndex, uint indexCount, uint materialIndex)
{
    public uint FirstIndex = firstIndex;

    public uint IndexCount = indexCount;

    public uint MaterialIndex = materialIndex;
}
