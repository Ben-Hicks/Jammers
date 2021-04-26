Shader "Custom/shaderFillbar"
{
    Properties
    {
        _Color ("Color", Color) = (1,1,1,1)
        _MainTex ("Albedo (RGB)", 2D) = "white" {}
		_SubTex("Pattern", 2D) = "white" {}
		_ScrollXSpeed("X", Range(0, 10)) = 2
		_ScrollYSpeed("Y", Range(0, 10)) = 3
    }
    SubShader
    {
        Tags { "RenderType"="Opaque" }
        LOD 200

        CGPROGRAM
        // Physically based Standard lighting model, and enable shadows on all light types
        #pragma surface surf Standard fullforwardshadows

        // Use shader model 3.0 target, to get nicer looking lighting
        #pragma target 3.0

        sampler2D _MainTex;
		sampler2D _SubTex;

        struct Input
        {
            float2 uv_MainTex;
			float2 uv_SubTex;
        };

        fixed4 _Color;
		fixed _ScrollXSpeed;
		fixed _ScrollYSpeed;

        void surf (Input IN, inout SurfaceOutputStandard o)
        {
			fixed2 uvScrolled = IN.uv_MainTex;

			fixed scrollvalX = _ScrollXSpeed * _Time;
			fixed scrollvalY = _ScrollYSpeed * _Time;

			uvScrolled += fixed2(scrollvalX, scrollvalY);
			half4 col = tex2D(_MainTex, uvScrolled);

			o.Albedo = tex2D(_MainTex, IN.uv_MainTex).rgb;
			o.Albedo += col.rbg;

        }
        ENDCG
    }
    FallBack "Diffuse"
}
