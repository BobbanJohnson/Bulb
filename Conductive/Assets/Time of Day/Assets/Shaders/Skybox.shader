// Upgrade NOTE: replaced '_Object2World' with 'unity_ObjectToWorld'
// Upgrade NOTE: replaced 'mul(UNITY_MATRIX_MVP,*)' with 'UnityObjectToClipPos(*)'

Shader "Time of Day/Skybox"
{
	Properties
	{
	}

	CGINCLUDE
	#include "UnityCG.cginc"
	#include "TOD_Base.cginc"
	#define TOD_SCATTERING_MIE 0
	#include "TOD_Scattering.cginc"

	struct v2f {
		float4 position : SV_POSITION;
		float4 color    : TEXCOORD0;
	};

	v2f vert(appdata_base v) {
		v2f o;

		o.position = UnityObjectToClipPos(v.vertex);

		float3 vertex = normalize(mul((float3x3)TOD_World2Sky, mul((float3x3)unity_ObjectToWorld, v.vertex.xyz)));

		o.color = (vertex.y < 0) ? half4(pow(TOD_GroundColor, TOD_Contrast), 1) : ScatteringColor(vertex.xyz, 1);

#if !TOD_OUTPUT_HDR
		o.color = TOD_HDR2LDR(o.color);
#endif

#if !TOD_OUTPUT_LINEAR
		o.color = TOD_LINEAR2GAMMA(o.color);
#endif

		return o;
	}

	float4 frag(v2f i) : COLOR {
		return i.color;
	}
	ENDCG

	SubShader
	{
		Tags
		{
			"Queue"="Background"
			"RenderType"="Background"
			"PreviewType"="Skybox"
		}

		Pass
		{
			Cull Off
			ZWrite Off
			ZTest LEqual

			CGPROGRAM
			#pragma vertex vert
			#pragma fragment frag
			#pragma multi_compile _ TOD_OUTPUT_HDR
			#pragma multi_compile _ TOD_OUTPUT_LINEAR
			ENDCG
		}
	}

	Fallback Off
}
