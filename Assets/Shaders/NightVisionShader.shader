Shader "KerbVisionIR/NightVision"
{
    Properties
    {
        _MainTex ("Texture", 2D) = "white" {}
        _Brightness ("Brightness", Range(1.0, 6.0)) = 2.0
        _Contrast ("Contrast", Range(0.0, 2.0)) = 1.0
        _TintColor ("Tint Color", Color) = (0.2, 1.0, 0.2, 1.0)
        _TintStrength ("Tint Strength", Range(0.0, 1.0)) = 0.8
        _GrainIntensity ("Grain Intensity", Range(0.0, 1.0)) = 0.3
    }
    
    SubShader
    {
        Tags { "RenderType"="Opaque" }
        LOD 100

        Pass
        {
            CGPROGRAM
            #pragma vertex vert_img
            #pragma fragment frag
            #include "UnityCG.cginc"

            sampler2D _MainTex;
            float _Brightness;
            float _Contrast;
            float4 _TintColor;
            float _TintStrength;
            float _GrainIntensity;

            // Simple random function
            float rand(float2 co)
            {
                return frac(sin(dot(co.xy, float2(12.9898, 78.233))) * 43758.5453);
            }

            fixed4 frag (v2f_img i) : SV_Target
            {
                // Sample texture
                fixed4 col = tex2D(_MainTex, i.uv);
                
                // Convert to grayscale
                float gray = dot(col.rgb, float3(0.299, 0.587, 0.114));
                
                // Apply brightness boost
                gray *= _Brightness;
                
                // Apply contrast
                gray = (gray - 0.5) * _Contrast + 0.5;
                gray = saturate(gray);
                
                // Apply color tint
                float3 tinted = lerp(float3(gray, gray, gray), gray * _TintColor.rgb, _TintStrength);
                
                // Add film grain
                float noise = rand(i.uv * _Time.y * 100.0) * 2.0 - 1.0;
                tinted += noise * _GrainIntensity * 0.1;
                
                return fixed4(saturate(tinted), 1.0);
            }
            ENDCG
        }
    }
    
    Fallback "Diffuse"
}
