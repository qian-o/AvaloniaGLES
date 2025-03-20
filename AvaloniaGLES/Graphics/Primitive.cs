namespace AvaloniaGLES.Graphics;

internal struct Primitive(uint firstIndex, uint indexCount)
{
    public uint FirstIndex = firstIndex;

    public uint IndexCount = indexCount;
}
