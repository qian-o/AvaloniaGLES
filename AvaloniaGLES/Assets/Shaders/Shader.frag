#version 300 es

precision highp float;

in vec3 Position;
in vec3 Normal;
in vec3 Color;
in vec2 TexCoord;

uniform vec4 BaseColorFactor;
uniform sampler2D BaseColorTexture;
uniform sampler2D NormalTexture;
uniform bool IsOpaque;
uniform float AlphaCutoff;

out vec4 Out_Color;

void main()
{
	vec4 baseColor = BaseColorFactor * texture(BaseColorTexture, TexCoord) * vec4(Color, 1.0);

    if (!IsOpaque && baseColor.a < AlphaCutoff)
        discard;

    Out_Color = baseColor;
}