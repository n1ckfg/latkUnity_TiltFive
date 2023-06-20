Shader "Custom/DistanceFade"{
	Properties{
		_MainTex("Texture", 2D) = "white" {}
		_Color("Tint", Color) = (1,1,1,1)
		_ColorClose("ColorClose", Color) = (1,1,1,1)
		_ColorSafe("ColorSafe", Color) = (1,1,1,1)
		_DitherPattern("Dithering Pattern", 2D) = "white" {}
		_MinFadeDistance("Minimum Fade Distance", Float) = 0
		_MaxFadeDistance("Maximum Fade Distance", Float) = 1
		_MinDistanceColor("Minimum Fade Distance", Float) = 0
		_MaxDistanceColor("Maximum Fade Distance", Float) = 1
	}

		SubShader{
			Tags{ "RenderType" = "Opaque" "Queue" = "Geometry"}

			CGPROGRAM

			#pragma surface surf Standard
			#pragma target 3.0

			sampler2D _MainTex;
			float4 _Color;
			float4 _ColorClose;
			float4 _ColorSafe;

			sampler2D _DitherPattern;
			float4 _DitherPattern_TexelSize;

			float _MinFadeDistance;
			float _MaxFadeDistance;

			float _MinDistanceColor;
			float _MaxDistanceColor;

			struct Input {
				float2 uv_MainTex;
				float4 screenPos;
			};

			void surf(Input i, inout SurfaceOutputStandard o) {
				float3 texColor = tex2D(_MainTex, i.uv_MainTex);
				
				float2 screenPos = i.screenPos.xy / i.screenPos.w;
				float2 ditherCoordinate = screenPos * _ScreenParams.xy * _DitherPattern_TexelSize.xy;
				float ditherValue = tex2D(_DitherPattern, ditherCoordinate).r;

				float relDistance = i.screenPos.w;

				relDistance = relDistance - _MinFadeDistance;
				relDistance = relDistance / (_MaxFadeDistance - _MinFadeDistance);

				clip(relDistance - ditherValue);

				float normalizeDistance = clamp(((1.0 / (_MaxDistanceColor - _MinDistanceColor)) * (i.screenPos.w - _MinDistanceColor)), 0.0, 1.0);

				o.Albedo = lerp(_ColorClose, _ColorSafe, normalizeDistance);
			}
			ENDCG
		}
}