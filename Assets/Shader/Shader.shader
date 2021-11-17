Shader "Unlit/Shader"
{
    Properties
    {
        ColorA ("ColorA", Color) = (1, 1, 1, 1)
        ColorB ("ColorB", Color) = (1, 1, 1, 1)
        
        ValueY ("ValueY", Float) = 1.0
    }
    SubShader
    {
         Tags { "RenderType"="Transperent" 
            "Queue"="Transparent"}
        LOD 100

        Pass
        {
            ZWrite Off
            Blend DstColor SrcColor
            
            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag

            #include "UnityCG.cginc"

            struct appdata
            {
                float4 vertex : POSITION;
                float3 normals : NORMAL;
                float2 uv : TEXCOORD0;
            };

            struct v2f
            {
                float2 uv : TEXCOORD0;
                float3 normal : TEXCOORD1;
                float4 vertex : SV_POSITION;
            };

            float4 ColorA;
            float4 ColorB;
            
            float OffsetX;
            float ValueY;

            v2f vert (appdata v)
            {
                v2f o;
                o.vertex = UnityObjectToClipPos(v.vertex); //converts local to clip
                o.normal = UnityObjectToWorldNormal( v.normals );
                o.uv = v.uv;
                return o;
            }

            float4 frag (v2f i) : SV_Target
            {
                float t = cos((i.uv.y - _Time.y) * ValueY);
                float4 gradient = lerp(ColorA, ColorB, t);
                return gradient;
            }
            ENDCG
        }
    }
}
