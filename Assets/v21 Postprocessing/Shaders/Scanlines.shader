Shader "Custom/Scanline" {
	Properties {
		_MainTex ("Base (RGB)", 2D) = "white" {}
		_ScanlineInterval ("Gap between scanlines in px" , Range(0,100)) = 10
		_ScanlineHeight ("Scanline height in px", Range(0,100)) = 5
		_ScanlineMultiplier ("Darkening/lightening applied to within scanline", Range(0,100)) = 0.9
		_ScanlineShift ("Sideways shift within scanline", Range(-1,1)) = 0.02
	}
	
	CGINCLUDE
	#include "UnityCG.cginc"

	struct v2f {
		float4 pos : POSITION;
		float2 uv : TEXCOORD0;
	};

	sampler2D _MainTex;
	uniform float4 _MainTex_TexelSize;
	
	float _ScanlineInterval;
	fixed _ScanlineMultiplier;
	fixed _ScanlineShift;
	fixed _ScanlineHeight;

	v2f vert( appdata_img v ) 
	{
		v2f o;
		o.pos = mul(UNITY_MATRIX_MVP, v.vertex);
		o.uv = v.texcoord.xy;
		return o;
	} 
	
	half4 frag(v2f i) : COLOR 
	{

		
			half4 c = tex2D (_MainTex, i.uv);
			
			if ( fmod((i.uv.y / _MainTex_TexelSize.y), _ScanlineInterval) < _ScanlineHeight){
				
				c.rgba = tex2D (_MainTex, float2(i.uv.x + _ScanlineShift,i.uv.y));
				c.rgba *= _ScanlineMultiplier; 
				
			}
			
			return c;
		}
		
	ENDCG 
	
	Subshader {
		Pass {
			  ZTest Always Cull Off ZWrite Off
			  Fog { Mode off }      
		
		      CGPROGRAM
		       
      		  #pragma fragmentoption ARB_precision_hint_fastest 
		      #pragma vertex vert 
		      #pragma fragment frag
		      ENDCG
		  }
	}
	
	Fallback off
}



	