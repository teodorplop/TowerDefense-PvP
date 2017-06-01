Shader "Glowable/Blur" {
	Properties {
		_MainTex("Main Texture", 2D) = "white" {}
	}

	SubShader {
		Pass {
			CGPROGRAM

			#pragma vertex vert
			#pragma fragment frag

			#include "UnityCG.cginc"

			sampler2D _MainTex;
			float2 _MainTex_TexelSize;

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

			float4 blur(sampler2D tex, float2 uv, float2 size) {
				float4 col = float4(0, 0, 0, 0);
				for (float i = -1; i <= 1; ++i) {
					for (float j = -1; j <= 1; ++j) {
						col += tex2D(tex, uv + float2(size.x * i, size.y * j));
					}
				}
				return col / 9;
			}

			float4 frag(v2f i) : SV_TARGET{
				return blur(_MainTex, i.uv, _MainTex_TexelSize);
			}

			ENDCG
		}
	}

	FallBack "Diffuse"
}