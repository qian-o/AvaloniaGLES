using System;
using AvaloniaGLES.Graphics;
using SharpGLTF.Schema2;
using SharpGLTF.Validation;
using Silk.NET.OpenGLES;

namespace AvaloniaGLES.Helpers;

internal static unsafe class ModelFactory
{
    public static Model Load(GL gl, string path)
    {
        ModelRoot root = ModelRoot.Load(path, new ReadSettings() { Validation = ValidationMode.Skip });

        throw new NotImplementedException();
    }
}
