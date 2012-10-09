Shader "Custom/ColorSeparation" {
	Properties {
		_MainTex ("Base (RGB)", 2D) = "" {}
		_rOffset ("R Channel Offset", Vector) = (0, 0, 0, 0)
		_gOffset ("G Channel Offset", Vector) = (0, 0, 0, 0)
		_bOffset ("B Channel Offset", Vector) = (0, 0, 0, 0)
		
	}
	
	// Shader code pasted into all further CGPROGRAM blocks
	CGINCLUDE
	
	#include "UnityCG.cginc"
	
	struct v2f {
		float4 pos : POSITION;
		float2 uv : TEXCOORD0;
	};
	
	sampler2D _MainTex;
	float4 _MainTex_TexelSize;
	half2 _rOffset;
	half2 _gOffset;
	half2 _bOffset;

		
	v2f vert( appdata_img v ) {
		v2f o;
		o.pos = mul(UNITY_MATRIX_MVP, v.vertex);
		
		#ifdef SHADER_API_D3D9
		if (_MainTex_TexelSize.y < 0)
			 v.texcoord.y = 1.0 - v.texcoord.y ;
		#endif
		
		o.uv = v.texcoord.xy;
		return o;
	} 
	
	half4 frag(v2f i) : COLOR {
		half2 uv = i.uv;
		
		
		float2 uvR = uv - _MainTex_TexelSize.xy; 
		float2 uvG = uv - _MainTex_TexelSize.xy; 
		float2 uvB = uv - _MainTex_TexelSize.xy;
		
		uvR += _rOffset;
		uvG += _gOffset;
		uvB += _bOffset;
		
		half4 color = tex2D (_MainTex, uv);
		#if SHADER_API_D3D9
		// Work around Cg's code generation bug for D3D9 pixel shaders :(
		color.r = color.r * 0.0001 + tex2D (_MainTex, uvR).r;
		color.g = color.g * 0.0001 + tex2D (_MainTex, uvG).g;
		color.b = color.b * 0.0001 + tex2D (_MainTex, uvB).b;
		#else
		color.r = tex2D (_MainTex, uvR).r;
		color.g = tex2D (_MainTex, uvG).g;
		color.b = tex2D (_MainTex, uvB).b;
		#endif
		
		return color;
	}

	ENDCG 
	
Subshader {
Pass {
	  ZTest Always Cull Off ZWrite Off
	  Fog { Mode off }      

      CGPROGRAM
      #pragma vertex vert 
      #pragma fragment frag
      ENDCG
  }
}

Fallback off
	
} // shader