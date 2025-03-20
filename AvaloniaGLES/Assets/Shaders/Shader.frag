#version 300 es

precision highp float;

in vec3 Position;
in vec3 Normal;
in vec2 TexCoord;
flat in uint MaterialIndex;

out vec4 Out_Color;

void main()
{
    Out_Color = vec4(TexCoord, 1.0, 1.0);
}