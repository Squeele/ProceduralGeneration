Shader "Custom/TerrainMaterial" {
	Properties {
		_Text1 ("Albedo (RGB)", 2D) = "" {}
		_Text2("Albedo (RGB)", 2D) = "" {}
		_Text3("Albedo (RGB)", 2D) = "" {}
		_Glossiness ("Smoothness", Range(0,1)) = 0.5
		_Metallic ("Metallic", Range(0,1)) = 0.0
		_ColorB ("ColorB", Color) = (0,0,1,1)
		_ColorM ("ColorM", Color) = (0,1,0,1)
		_ColorT ("ColorT", Color) = (1,0,0,1)
		_Step1("Step1", Range(0,1500)) = 10.0
		_Step2("Step2", Range(0,1500)) = 200.0
	}
	SubShader {
		Tags { "RenderType"="Opaque" }
		LOD 200

		CGPROGRAM
		// Physically based Standard lighting model, and enable shadows on all light types
		#pragma surface surf Standard fullforwardshadows

		// Use shader model 3.0 target, to get nicer looking lighting
		#pragma target 3.0

		sampler2D _Text1;
		sampler2D _Text2;
		sampler2D _Text3;
		fixed4 _ColorB;
		fixed4 _ColorM;
		fixed4 _ColorT;
		float _Step1;
		float _Step2;

		struct Input {
			float2 uv_Text1: TEXCOORD0;
			float3 worldPos;
		};

		half _Glossiness;
		half _Metallic;
		

		// Add instancing support for this shader. You need to check 'Enable Instancing' on materials that use the shader.
		// See https://docs.unity3d.com/Manual/GPUInstancing.html for more information about instancing.
		// #pragma instancing_options assumeuniformscaling
		UNITY_INSTANCING_BUFFER_START(Props)
			// put more per-instance properties here
		UNITY_INSTANCING_BUFFER_END(Props)

		void surf (Input IN, inout SurfaceOutputStandard o) {
			// Albedo comes from a texture tinted by color
			fixed4 c;
			if (IN.worldPos.y <= _Step1)
			{
				 c = lerp(tex2D(_Text1, IN.uv_Text1).rgba, tex2D(_Text2, IN.uv_Text1).rgba, IN.worldPos.y/_Step1);
				 //c = half4(IN.uv_Text1.x,IN.uv_Text1.y,0.0,1.0);
			}
			else {
				if (IN.worldPos.y < _Step2)
				{
					c = lerp(tex2D(_Text2, IN.uv_Text1).rgba, tex2D(_Text3, IN.uv_Text1).rgba, IN.worldPos.y/_Step2);
					//c = half4(IN.uv_Text1.x,IN.uv_Text1.y,0.0,1.0);
				}
				else
				{
					c = tex2D(_Text3, IN.uv_Text1);
					//c = half4(IN.uv_Text1.x,IN.uv_Text1.y,0.0,1.0);
				}
			}
			o.Albedo = c.rgb;
			// Metallic and smoothness come from slider variables
			o.Metallic = _Metallic;
			o.Smoothness = _Glossiness;
			o.Alpha = c.a;
		}
		ENDCG
	}
	FallBack "Diffuse"
}
