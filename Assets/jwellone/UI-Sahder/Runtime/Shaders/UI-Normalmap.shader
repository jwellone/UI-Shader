// Unity built-in shader source. Copyright (c) 2016 Unity Technologies. MIT license (see license.txt)

Shader "UI/Normalmap"
{
    Properties
    {
        _MainTex ("Sprite Texture", 2D) = "white" {}
        _ScaleFactor ("Scale Factor", Range(0.0, 5.0)) = 1.0
        _ParallaxScale ("Parallax Scale", Range(0.0, 10.0)) = 3.0
        [Toggle] _InvertHeight ("Invert Height", Float) = 0
    }

    SubShader
    {
        Tags
        {
            "Queue"="Transparent"
            "IgnoreProjector"="True"
            "RenderType"="Transparent"
            "PreviewType"="Plane"
        }

        Cull Off
        Lighting Off
        ZWrite Off
        ZTest [unity_GUIZTestMode]
        Blend SrcColor OneMinusSrcAlpha

        Pass
        {
            Name "Default"
        CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag
            #pragma target 2.0

            #include "UnityCG.cginc"
            #include "UnityUI.cginc"

            #pragma multi_compile_local _ UNITY_UI_CLIP_RECT

            struct appdata_t
            {
                float4 vertex   : POSITION;
                float2 texcoord : TEXCOORD0;
                UNITY_VERTEX_INPUT_INSTANCE_ID
            };

            struct v2f
            {
                float4 vertex   : SV_POSITION;
                float2 texcoord  : TEXCOORD0;
                UNITY_VERTEX_OUTPUT_STEREO
            };

            sampler2D _MainTex;
            fixed4 _TextureSampleAdd;
            float4 _MainTex_ST;
            float4 _MainTex_TexelSize;
            float _ScaleFactor;
            float _ParallaxScale;
            float _InvertHeight;

            v2f vert(appdata_t v)
            {
                v2f OUT;
                UNITY_SETUP_INSTANCE_ID(v);
                UNITY_INITIALIZE_VERTEX_OUTPUT_STEREO(OUT);
                float4 vPosition = UnityObjectToClipPos(v.vertex);
                OUT.vertex = vPosition;
                OUT.texcoord = TRANSFORM_TEX(v.texcoord.xy, _MainTex);

                return OUT;
            }

            float3 GetGrayscale(sampler2D tex, float2 uv)
            {
                float4 color = tex2D(tex, uv);
                float gray = color.r * 0.298912 + color.g * 0.586611 + color.b * 0.114478; 
                return float3(gray, gray, gray) * color.a;
            }

            fixed4 frag(v2f IN) : SV_Target
            {
                float2 uv = IN.texcoord;
                fixed4 color = tex2D(_MainTex, uv) + _TextureSampleAdd;

                clip (color.a - 0.001);

                float shiftScale = _ParallaxScale * _ScaleFactor;
                float2 shiftX = { _MainTex_TexelSize.x * shiftScale, 0 };
                float2 shiftZ = { 0, _MainTex_TexelSize.y * shiftScale };

                float x1 = GetGrayscale(_MainTex, uv + shiftX).x;
                float x2 = GetGrayscale(_MainTex, uv - shiftX).x;
                float z1 = GetGrayscale(_MainTex, uv + shiftZ).x;
                float z2 = GetGrayscale(_MainTex, uv - shiftZ).x;

                _ScaleFactor *= 2 * _InvertHeight - 1;
                float3 du = { 1, 0, _ScaleFactor * (x2 - x1) };
                float3 dv = { 0, 1, _ScaleFactor * (z2 - z1) };

                color.rgb = normalize(cross(du, dv)) * 0.5 + 0.5;

                return color;
            }
        ENDCG
        }
    }
}
