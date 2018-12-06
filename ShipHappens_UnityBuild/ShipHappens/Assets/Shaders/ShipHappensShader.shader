// Upgrade NOTE: replaced 'mul(UNITY_MATRIX_MVP,*)' with 'UnityObjectToClipPos(*)'

Shader "ShipHappensShader" 
{
	Properties 
	{
		[Header(Texture)]
		Texture1("Texture", 2D) = "white" {}
		Color1("Tint", Color) = (1,1,1,1)
		
		[Header(Transition Texture)]
		TransitionTexture("Texture" , 2D) = "white" {}

		[Space(20)]

		DissolveAmount("Dissolve Amount", Range(0,1)) = 1	
	}

	SubShader 
	{

		Tags {"RenderType"="Opaque" }
		LOD 200
		Blend SrcAlpha OneMinusSrcAlpha
        ZWrite on
		
		Pass
		{
			CGPROGRAM

			#pragma vertex vert
			#pragma fragment frag

			#include "UnityCG.cginc"


			struct appdata
			{
				float4 vertex : POSITION;
				float2 uv : TEXCOORD0;
				float3 normal : NORMAL;
			};

			struct v2f
			{
				float4 position : SV_POSITION;
				float2 uv : TEXCOORD0;
			};

			float4 Color1;
			sampler2D Texture1;

			float4 Color2;
			sampler2D Texture2;

			sampler2D TransitionTexture;
			float DissolveAmount;

			v2f vert(appdata IN)
			{
				v2f OUT;

				OUT.position = UnityObjectToClipPos(IN.vertex);
				OUT.uv = IN.uv;

				return OUT;
			}

			fixed4 frag(v2f IN) : SV_Target
			{
				float4 textureColor = tex2D(Texture1, IN.uv);

				float4 dissolveColor = tex2D(TransitionTexture, IN.uv);

				clip(-dissolveColor.rgb + DissolveAmount);

				return textureColor * Color1;
			}
			ENDCG
		}
	}
}
