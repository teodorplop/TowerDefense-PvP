Shader "Glowable/Composite" {
	Properties {
		_MainTex("Albedo (RGB)", 2D) = "white" {}
	}

	SubShader {
		Cull Off ZWrite Off ZTest Always

		Pass {
			CGPROGRAM
			#pragma vertex vert
			#pragma fragment frag
			#include "UnityCG.cginc"

			sampler2D _MainTex;
			sampler2D _GlowPrePassTex;
			sampler2D _GlowBlurTex;
			float _Intensity;

			struct appdata {
				float4 vertex : POSITION;
				float2 uv : TEXCOORD0;
			};

			struct v2f {
				float4 vertex : SV_POSITION;
				float2 uv : TEXCOORD0;
			};

			v2f vert(appdata v) {
				v2f o;
				o.vertex = UnityObjectToClipPos(v.vertex);
				o.uv = v.uv;
				return o;
			}

			fixed4 frag(v2f i) : SV_TARGET{
				fixed4 col = tex2D(_MainTex, i.uv);
				fixed4 glow = max(0, tex2D(_GlowBlurTex, i.uv) - tex2D(_GlowPrePassTex, i.uv));
				return col + glow * _Intensity;
			}

			ENDCG
		}
	}
	
	FallBack "Diffuse"
}