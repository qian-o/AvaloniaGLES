#version 300 es

precision highp float;

in vec3 Position;
in vec3 Normal;
in vec2 TexCoord;
flat in int MaterialIndex;

out vec4 Out_Color;

void main()
{
    Out_Color = vec4(Position, 1.0);
}