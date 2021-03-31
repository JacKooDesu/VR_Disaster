// Upgrade NOTE: replaced 'mul(UNITY_MATRIX_MVP,*)' with 'UnityObjectToClipPos(*)'

// Upgrade NOTE: replaced 'mul(UNITY_MATRIX_MVP,*)' with 'UnityObjectToClipPos(*)'

Shader "Unlit/Outline"
{
    Properties
    {
        _OutlineColor ( "Outline Color" , Color) = (0,0,0,1)
        _OutlineWidth ("Outline Width", Range(0.0, 1.0)) = .005
    }
    SubShader
    {
        Tags { "RenderType"="Opaque" }

        Pass
        {
            Cull front

            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag
            // make fog work
            #pragma multi_compile_fog

            #include "UnityCG.cginc"

            struct appdata
            {
                float4 vertex : POSITION;
                float3 normal : NORMAL;
            };

            float _OutlineWidth;
            float4 _OutlineColor;

            float4 vert (appdata v) : SV_POSITION
            {
                float4 pos = UnityObjectToClipPos(v.vertex);
                float3 norm = mul((float3x3)UNITY_MATRIX_MV, v.normal);
                norm.x*= UNITY_MATRIX_P[0][0];
                norm.y*= UNITY_MATRIX_P[1][1];
                //v.vertex.xyz += v.normal * _OutlineWidth;
                pos.xy += norm.xy * pos.z * _OutlineWidth;
                return pos;
            }

            half4 frag () : SV_Target
            {
                return _OutlineColor;
            }
            ENDCG
        }
        
        // Pass{
        //     SetTexture[_MainTex]{
        //         Combine Primary * Texture
        //     }
        // }
    }
    Fallback "Diffuse"
}
