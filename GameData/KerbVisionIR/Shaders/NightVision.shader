Shader "KerbVisionIR/NightVision"
{
    Properties
    {
        _MainTex ("Base (RGB)", 2D) = "white" {}
        _Brightness ("Brightness", Float) = 3.0
        _Contrast ("Contrast", Float) = 1.0
        _Gamma ("Gamma", Float) = 1.0
        _Tint ("Tint Color", Color) = (0.3, 1, 0.3, 1)
        _TintStrength ("Tint Strength", Float) = 0.7
        _GrainIntensity ("Grain Intensity", Float) = 0.2
    }
    
    SubShader
    {
        Tags { "RenderType"="Opaque" }
        ZTest Always
        Cull Off
        ZWrite Off
        Fog { Mode off }
        
        Pass
        {
            CGPROGRAM
            #pragma vertex vert_img
            #pragma fragment frag
            #pragma target 3.0
            
            #include "UnityCG.cginc"
            
            sampler2D _MainTex;
            float _Brightness;
            float _Contrast;
            float _Gamma;
            float4 _Tint;
            float _TintStrength;
            float _GrainIntensity;
            
            float hash(float2 p)
            {
                return frac(sin(dot(p, float2(127.1, 311.7))) * 43758.5453);
            }
            
            fixed4 frag(v2f_img i) : SV_Target
            {
                // Sample texture
                fixed4 col = tex2D(_MainTex, i.uv);
                
                // Convert to luminance (grayscale)
                float lum = dot(col.rgb, float3(0.299, 0.587, 0.114));
                
                // BOOST BRIGHTNESS - this is key for night vision!
                lum = lum * _Brightness;
                
                // Apply gamma correction to lift shadows
                lum = pow(saturate(lum), 1.0 / max(0.5, _Gamma * 2.0));
                
                // Apply contrast
                lum = (lum - 0.5) * _Contrast + 0.5;
                
                // Extra shadow lift for very dark areas (< 30% brightness)
                if (lum < 0.3)
                {
                    lum = lum + (0.3 - lum) * 0.5;
                }
                
                // Clamp to valid range
                lum = saturate(lum);
                
                // Apply color tint with strength control
                float3 grayscale = float3(lum, lum, lum);
                float3 tinted = lum * _Tint.rgb;
                float3 result = lerp(grayscale, tinted, _TintStrength);
                
                // Add animated grain/noise
                if (_GrainIntensity > 0.001)
                {
                    float grain = (hash(i.uv * 1000.0 + _Time.y * 10.0) - 0.5) * _GrainIntensity * 0.2;
                    result = result + grain;
                }
                
                // Vignette effect (darken edges)
                float2 uv = i.uv * 2.0 - 1.0;
                float vignette = 1.0 - dot(uv, uv) * 0.2;
                result = result * vignette;
                
                // Scanlines (subtle)
                float scanline = sin(i.uv.y * 600.0 + _Time.y * 5.0) * 0.015 + 0.985;
                result = result * scanline;
                
                return fixed4(saturate(result), 1.0);
            }
            ENDCG
        }
    }
    
    Fallback Off
}
