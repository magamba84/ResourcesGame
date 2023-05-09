Shader "Custom/ColorMobile"
{

	Properties
	{
		_Color("Color", Color) = (0,0,0,0)
	}

	SubShader
	{
		Tags
		{ 
			"RenderType" = "Opaque"  
		}

		CGPROGRAM
		#pragma surface surf Lambert

		fixed4 _Color;

		struct Input
		{
			float2 uv_MainTex;
		};


		void surf( Input IN , inout SurfaceOutput o )
		{
			fixed4 c = _Color;
			o.Albedo = c.rgb;
		}
		ENDCG
	}
	Fallback "Diffuse"
}