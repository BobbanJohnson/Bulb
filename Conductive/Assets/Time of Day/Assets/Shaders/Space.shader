// Upgrade NOTE: replaced '_Object2World' with 'unity_ObjectToWorld'

Shader "Time of Day/Space"
{
	Properties
	{
		_CubeTex ("Cube (RGB)", Cube) = "black" {}
		_Brightness ("Brightness", float) = 0
	}

	CGINCLUDE
	#include "UnityCG.cginc"
	#include "TOD_Base.cginc"

	uniform samplerCUBE _CubeTex;
	uniform float _Brightness;

	struct v2f {
		float4 position : SV_POSITION;
		float4 viewdir  : TEXCOORD0;
	};

	v2f vert(appdata_base v) {
		v2f o;

		o.position = TOD_TRANSFORM_VERT(v.vertex);

		float3 vertnorm = normalize(v.vertex.xyz);

		float3 worldNormal = normalize(mul((float3x3)unity_ObjectToWorld, vertnorm));

		o.viewdir.xyz = vertnorm;
		o.viewdir.w   = saturate(_Brightness * TOD_StarVisibility * worldNormal.y);

		return o;
	}

	half4 frag(v2f i) : COLOR {
		return half4(texCUBE(_CubeTex, i.viewdir.xyz).rgb * i.viewdir.w, 1);
	}
	ENDCG

	SubShader
	{
		Tags
		{
			"Queue"="Background+10"
			"RenderType"="Background"
			"IgnoreProjector"="True"
		}

		Pass
		{
			ZWrite Off
			ZTest LEqual
			Fog { Mode Off }

			CGPROGRAM
			#pragma vertex vert
			#pragma fragment frag
			ENDCG
		}
	}

	Fallback Off
}
