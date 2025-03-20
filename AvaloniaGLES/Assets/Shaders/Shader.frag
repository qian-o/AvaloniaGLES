#version 300 es

precision highp float;

in vec3 Position;
in vec3 Normal;
in vec3 Color;
in vec2 TexCoord;

uniform vec4 BaseColorFactor;
uniform bool HasBaseColorTexture;
uniform sampler2D BaseColorTexture;
uniform bool HasNormalTexture;
uniform sampler2D NormalTexture;
uniform bool IsOpaque;
uniform float AlphaCutoff;

out vec4 Out_Color;

vec3 GammaCorrect(vec3 color)
{
    return pow(color, vec3(1.0 / 2.2));
}

void main()
{
	vec4 baseColor = BaseColorFactor * vec4(Color, 1.0);

    if (HasBaseColorTexture)
    {
        baseColor *= texture(BaseColorTexture, TexCoord);
    }

    if (!IsOpaque && baseColor.a < AlphaCutoff)
        discard;

    Out_Color = vec4(GammaCorrect(baseColor.rgb), baseColor.a);
}