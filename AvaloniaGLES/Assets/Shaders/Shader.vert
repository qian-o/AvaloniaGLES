#version 300 es

in vec3 In_Position;
in vec3 In_Normal;
in vec3 In_Color;
in vec2 In_TexCoord;
in int In_MaterialIndex;

out vec3 Position;
out vec3 Normal;
out vec2 TexCoord;
flat out int MaterialIndex;

uniform mat4 Model;
uniform mat4 View;
uniform mat4 Projection;

void main()
{
    gl_Position = Projection * View * Model * vec4(In_Position, 1.0);

    Position = (Model * vec4(In_Position, 1.0)).xyz;
    Normal = In_Normal;
    TexCoord = In_TexCoord;
    MaterialIndex = In_MaterialIndex;
}