﻿//Hugh Edwards (584183) University of Melbourne Graphics and Interaction 2016

Shader "Unlit/PhongShader"
{
	Properties
	{
		_PointLightColor("Point Light Color", Color) = (0, 0, 0)
		_PointLightPosition("Point Light Position", Vector) = (0.0, 0.0, 0.0)
	}
	SubShader
	{
		Tags
	{
		"Queue" = "Transparent"
		"RenderType" = "Transparent"
	}

		Pass
	{
		Blend SrcAlpha OneMinusSrcAlpha

			CGPROGRAM
			#pragma vertex vert
			#pragma fragment frag

			#include "UnityCG.cginc"

			uniform float3 _PointLightColor;
			uniform float3 _PointLightPosition;

			struct vertIn
			{
				float4 vertex : POSITION;
				float4 normal : NORMAL;
				float4 color : COLOR;
			};

			struct vertOut
			{
				float4 vertex : SV_POSITION;
				float4 color : COLOR;
				float4 worldVertex : TEXCOORD0;
				float3 worldNormal : TEXCOORD1;
			};

			vertOut vert(vertIn v)
			{
				vertOut o;

				float4 worldVertex = mul(_Object2World, v.vertex);
				float3 worldNormal = normalize(mul(transpose((float3x3)_World2Object), v.normal.xyz));

				o.vertex = mul(UNITY_MATRIX_MVP, v.vertex);
				o.color = v.color;

				o.worldVertex = worldVertex;
				o.worldNormal = worldNormal;

				return o;
			}

			fixed4 frag(vertOut v) : SV_Target
			{

				float3 interNormal = normalize(v.worldNormal);

				float Ka = 1;
				float F = 1;
				float Kd = 1;
				float Ks = 1;
				float N = 5;

				float3 L = normalize(_PointLightPosition - v.worldVertex.xyz);
				float3 V = normalize(_WorldSpaceCameraPos - v.worldVertex.xyz);
				float LN = dot(L, interNormal);
				float3 H = normalize(V + L);

				float3 diffuse = Kd * v.color.rgb * F * _PointLightColor.rgb  * saturate(LN);
				float3 ambient = Ka * v.color.rgb * UNITY_LIGHTMODEL_AMBIENT.rgb;
				float3 specular = Ks * _PointLightColor.rgb * F * pow(saturate(dot(interNormal, H)), N);

				float4 returnCol = float4(0.0f, 0.0f, 0.0f, 0.0f);
				returnCol.rgb = ambient.rgb + diffuse.rgb + specular.rgb;
				returnCol.a = v.color.a;

				return returnCol;

			}
			ENDCG
		}
	}
}
