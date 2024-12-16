Shader "Skybox/Blended"
{
    Properties
    {
        _Sky1 ("Skybox 1", Cube) = "" {}
        _Sky2 ("Skybox 2", Cube) = "" {}
        _Blend ("Blend Factor", Range(0, 1)) = 0
    }
    SubShader
    {
        Tags { "Queue" = "Background" }
        Pass
        {
            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag
            #include "UnityCG.cginc"

            samplerCUBE _Sky1;
            samplerCUBE _Sky2;
            float _Blend;

            struct appdata_t
            {
                float4 vertex : POSITION;
            };

            struct v2f
            {
                float4 pos : SV_POSITION;
                float3 texcoord : TEXCOORD0;
            };

            v2f vert(appdata_t v)
            {
                v2f o;
                o.pos = UnityObjectToClipPos(v.vertex);
                o.texcoord = v.vertex.xyz;
                return o;
            }

            half4 frag(v2f i) : SV_Target
            {
                half4 col1 = texCUBE(_Sky1, i.texcoord);
                half4 col2 = texCUBE(_Sky2, i.texcoord);
                return lerp(col1, col2, _Blend);
            }
            ENDCG
        }
    }
}
