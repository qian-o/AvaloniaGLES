#version 300 es

precision highp float;

in vec2 TexCoord;

out vec4 Out_Color;

void main()
{
    Out_Color = vec4(TexCoord, 0.0, 1.0);
}