// Upgrade NOTE: replaced 'mul(UNITY_MATRIX_MVP,*)' with 'UnityObjectToClipPos(*)'

// Unlit alpha-blended shader.
 // - no lighting
 // - no lightmap support
 // - no per-material color
 
 Shader "Unlit/Transparent GreenScreen Tex" {
 Properties {
     _MainTex ("Base (RGB) Trans (A)", 2D) = "white" {}
     _Color ("Color", Color) = (0,1,0,1)
     _Threshold ("Threshold", Float) = .1
 }
 
 SubShader {
     Tags {"Queue"="Transparent" "IgnoreProjector"="True" "RenderType"="Transparent"}
     LOD 100
     
     ZWrite Off
     Blend SrcAlpha OneMinusSrcAlpha 
     
     Pass {  
         CGPROGRAM
             #pragma vertex vert
             #pragma fragment frag
             #pragma multi_compile_fog
             
             #include "UnityCG.cginc"
 
             struct appdata_t {
                 float4 vertex : POSITION;
                 float2 texcoord : TEXCOORD0;
             };
 
             struct v2f {
                 float4 vertex : SV_POSITION;
                 half2 texcoord : TEXCOORD0;
                 UNITY_FOG_COORDS(1)
             };
 
             sampler2D _MainTex;
             fixed4 _Color;
             float _Threshold;
             float4 _MainTex_ST;
             
             v2f vert (appdata_t v)
             {
                 v2f o;
                 o.vertex = UnityObjectToClipPos(v.vertex);
                 o.texcoord = TRANSFORM_TEX(v.texcoord, _MainTex);
                 return o;
             }
             
             fixed4 frag (v2f i) : SV_Target
             {
                 fixed4 col = tex2D(_MainTex, i.texcoord);

                 if(col.r > _Color.r - _Threshold
                 && col.r < _Color.r + _Threshold
                 && col.g < _Color.g + _Threshold
                 && col.g > _Color.g - _Threshold
                 && col.b < _Color.b + _Threshold
                 && col.b > _Color.b - _Threshold) {
                 	col.a = 0;
                 }

                 return col;
             }
         ENDCG
     }
 }
 
 }