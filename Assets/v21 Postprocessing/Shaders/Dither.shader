Shader "Custom/Dither" {
	Properties {
		_MainTex ("Base (RGB)", 2D) = "white" {}
		_Levels ("Levels", Range (2, 10)) = 2
		_ThresholdMap ("Threshold Map", 2D) = "grey" {}
		_ThresholdMapSize ("Threshold Map Dimensions", Range(1,20)) = 1
		_BlockSize ("Block Size", Vector) = (1, 1, 0, 0)
	}
	
	// Shader code pasted into all further CGPROGRAM blocks
	CGINCLUDE
	
	#include "UnityCG.cginc"
	
	struct v2f {
		float4 pos : POSITION;
		float2 uv : TEXCOORD0;
	};
	
	
	sampler2D _MainTex;
	uniform float _Levels;
	uniform fixed2 _BlockSize;
	uniform sampler2D _ThresholdMap;
	uniform fixed2 _ThresholdMapSize;
	
	uniform float4 _MainTex_TexelSize;
	
	v2f vert( appdata_img v ) 
	{
		v2f o;
		o.pos = mul(UNITY_MATRIX_MVP, v.vertex);
		o.uv = v.texcoord.xy;
		return o;
	} 
	
	half4 frag(v2f i) : COLOR 
	{
		
		fixed dx = _BlockSize.x*(_MainTex_TexelSize.x);
		fixed dy = _BlockSize.y*(_MainTex_TexelSize.y);
		fixed2 coord = float2(dx*floor(i.uv.x/dx), dy*floor(i.uv.y/dy));
		
		fixed threshx = (fmod((i.uv.x/dx), _ThresholdMapSize.x) / _ThresholdMapSize.x);
		fixed threshy = (fmod((i.uv.y/dy), _ThresholdMapSize.y) / _ThresholdMapSize.y); 
		
		
		fixed2 thresholdPos = fixed2(threshx, threshy);
		
		fixed threshold = tex2D(_ThresholdMap, thresholdPos).a;
		
		
		half4 c = tex2D (_MainTex, coord);
		
		c.r = step(threshold, c.r);
		c.g = step(threshold, c.g);
		c.b = step(threshold, c.b);
		
		//c.rgb = threshold;
		
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

