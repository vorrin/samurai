Shader "Custom/Pixelated" {
Properties {
	_MainTex ("Base (RGB)", 2D) = "white" {}
	_PixelWidth ("Width of pixel in px", Range(1,100)) = 1
	_PixelHeight ("Height of pixel in px", Range(1,100)) = 1
}

SubShader {
	Pass {
		ZTest Always Cull Off ZWrite Off
		Fog { Mode off }
				
		CGPROGRAM
			#pragma vertex vert_img
			#pragma fragment frag
			#pragma fragmentoption ARB_precision_hint_fastest 
			#include "UnityCG.cginc"

			uniform sampler2D _MainTex;
			uniform float4 _MainTex_TexelSize;
			float _PixelWidth, _PixelHeight;

			float4 frag (v2f_img i) : COLOR {
				float dx = _PixelWidth*(_MainTex_TexelSize.x);
				float dy = _PixelHeight*(_MainTex_TexelSize.y);
				float2 coord = float2(dx*floor((i.uv.x/dx)), dy*floor((i.uv.y/dy)));
				float4 result = tex2D(_MainTex, coord);
				return result;
			}
		ENDCG

	}
}

Fallback off

}
