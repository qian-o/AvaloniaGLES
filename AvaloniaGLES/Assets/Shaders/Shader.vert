#version 300 es

in vec3 In_Position;
in vec3 In_Normal;
in vec4 In_Color;
in vec2 In_TexCoord;

out vec2 TexCoord;

uniform mat4 Model;
uniform mat4 View;
uniform mat4 Projection;

void main()
{
    gl_Position = Projection * View * Model * vec4(In_Position, 1.0);

    TexCoord = In_TexCoord;
}