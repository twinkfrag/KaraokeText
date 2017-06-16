Shader "KaraokeText"
{
	Properties
	{
		_FontTex ("FontTexture", 2D) = "white" {}
		_BaseColor ("Base Color", COLOR) = (1,1,1,1)
		_PastColor ("Past Color", COLOR) = (1,0,0,1)
	}
	SubShader {

		Tags {
			"Queue"="Transparent"
			"IgnoreProjector"="True"
			"RenderType"="Transparent"
			"PreviewType"="Plane"
		}
		Lighting Off Cull Off ZTest Always ZWrite Off
		Blend SrcAlpha OneMinusSrcAlpha

		Pass {
			CGPROGRAM
			#pragma vertex vert
			#pragma fragment frag
			#pragma multi_compile _ UNITY_SINGLE_PASS_STEREO STEREO_INSTANCING_ON STEREO_MULTIVIEW_ON
			#include "UnityCG.cginc"

			struct appdata_t {
				float4 vertex : POSITION;
				fixed4 color : COLOR;
				float2 texcoord : TEXCOORD0;
				UNITY_VERTEX_INPUT_INSTANCE_ID
			};

			struct v2f {
				float4 vertex : SV_POSITION;
				fixed4 color : COLOR;
				float2 texcoord : TEXCOORD0;
				float2 position : TEXCOORD1;
				UNITY_VERTEX_OUTPUT_STEREO
			};

			sampler2D _FontTex;
			uniform float4 _FontTex_ST;
			uniform float _Lerp;
			uniform float _Start;
			uniform float _End;
			uniform fixed4 _BaseColor;
			uniform fixed4 _PastColor;

			v2f vert (appdata_t v)
			{
				v2f o;
				UNITY_SETUP_INSTANCE_ID(v);
				UNITY_INITIALIZE_VERTEX_OUTPUT_STEREO(o);
				o.vertex = UnityObjectToClipPos(v.vertex);
				o.color = v.color;
				o.texcoord = TRANSFORM_TEX(v.texcoord, _FontTex);
				o.position = v.vertex;
				return o;
			}

			fixed4 frag (v2f i) : SV_Target
			{
				fixed4 col = i.color;
				if (i.position.x < lerp(_Start, _End, _Lerp)) {
					col *= _PastColor;
				}
				else {
					col *= _BaseColor;
				}

				col.a *= tex2D(_FontTex, i.texcoord).a;
				return col;
			}
			ENDCG
		}
	}
}
