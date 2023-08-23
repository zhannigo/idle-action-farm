Shader "Custom/WaterShader"
{
    Properties
    {
        _Color ("Color", Color) = (1,1,1,1)
        _FoamTexture ("Foam Texture", 2D) = "white" {}
        _FoamColor ("Foam Color", Color) = (1, 1, 1, 1)
        _FoamSpeed ("Foam Speed", Range(0, 1)) = 0.1
        _DistortionTexture ("Distiortion Texture", 2D) = "white" {}
        _DistortionStrength ("Distortion Strength", Range(0, 1)) = 0.5
        _Opacity ("Opacity", Range(0, 1)) = 1
        _NormalMap ("Normal Map", 2D) = "bump" {}
        _NormalSpeed ("Normal Speed",  Range(0, 1)) = 1
        _Glossiness ("Smoothness", Range(0,1)) = 0.5
    }
    SubShader
    {
        Tags { "RenderType"="Transparent" "Queue"="Transparent"}
        LOD 200

        CGPROGRAM
        
        #pragma surface surf Standard fullforwardshadows alpha:blend
        #pragma target 3.0

        sampler2D _MainTex;

        struct Input
        {
            float2 uv_MainTex;
            float2 uv_FoamTexture;
            float2 uv_NormalMap;
        };

        fixed4 _Color;
        sampler2D _FoamTexture;
        float4 _FoamColor;
        float _FoamSpeed;
        sampler2D _DistortionTexture;
        float _DistortionStrength;
        float _Opacity;
        sampler2D _NormalMap;
        float _NormalSpeed;
        half _Glossiness;

        // Add instancing support for this shader. You need to check 'Enable Instancing' on materials that use the shader.
        // See https://docs.unity3d.com/Manual/GPUInstancing.html for more information about instancing.
        // #pragma instancing_options assumeuniformscaling
        UNITY_INSTANCING_BUFFER_START(Props)
            // put more per-instance properties here
        UNITY_INSTANCING_BUFFER_END(Props)
        

        float2 DistortTexture(float2 uv, float time) {
            float2 distortedUV = uv;
            float2 offset = float2(_FoamSpeed  * time, 0);
            distortedUV.x += offset;
            return distortedUV + tex2D(_DistortionTexture, distortedUV).xy * _DistortionStrength;
        }
        
        void surf (Input IN, inout SurfaceOutputStandard o)
        {
            fixed4 _BaseColor = tex2D (_MainTex, IN.uv_MainTex) * _Color;
            float3 baseColor = _BaseColor.rgb;

            float2 foamUV = DistortTexture(IN.uv_FoamTexture, _Time.y);
            float4 foamTexture = tex2D(_FoamTexture, foamUV);
            float3 foamColor = _FoamColor.rgb * foamTexture.a;
            
            float3 finalColor = lerp(baseColor, foamColor, foamTexture.a);

            float2 offset = float2(_NormalSpeed * _Time.y, 0);
            float2 normalUV = IN.uv_NormalMap + offset;
            float3 normal = UnpackNormal(tex2D(_NormalMap, normalUV));
            
            o.Albedo = finalColor;
            o.Normal = normal;
            o.Smoothness = _Glossiness*2;
            o.Alpha = _BaseColor.a * _Opacity + foamColor;
        }
        ENDCG
    }
    FallBack "Diffuse"
}
