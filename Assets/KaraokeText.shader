Shader "KaraokeText"
{
	Properties
	{
		_FontTex ("FontTexture", 2D) = "white" {}
		_Lerp ("Lerp", Range(0,1)) = 0.5
		_LineWidth ("Line Width", Float) = 10
		_BaseColor ("Base Color", COLOR) = (1,1,1,1)
		_PastColor ("Past Color", COLOR) = (1,0,0,1)
		_LerpMargin ("Lerp Margin", Float) = 0.5
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
				UNITY_VERTEX_OUTPUT_STEREO
			};

			sampler2D _FontTex;
			uniform float4 _FontTex_ST;
			uniform float _Lerp;
			uniform float _LineWidth;
			uniform fixed4 _BaseColor;
			uniform fixed4 _PastColor;
			uniform float _LerpMargin;

			v2f vert (appdata_t v)
			{
				v2f o;
				UNITY_SETUP_INSTANCE_ID(v);
				UNITY_INITIALIZE_VERTEX_OUTPUT_STEREO(o);
				o.vertex = UnityObjectToClipPos(v.vertex);
				o.color = v.color;
				if (v.vertex.x < lerp(0 - _LerpMargin, _LineWidth + _LerpMargin, _Lerp)) {
					o.color *= _PastColor;
				}
				else {
					o.color *= _BaseColor;
				}
				o.texcoord = TRANSFORM_TEX(v.texcoord, _FontTex);
				return o;
			}

			fixed4 frag (v2f i) : SV_Target
			{
				fixed4 col = i.color;
				col.a *= tex2D(_FontTex, i.texcoord).a;
				return col;
			}
			ENDCG
		}
	}
}
