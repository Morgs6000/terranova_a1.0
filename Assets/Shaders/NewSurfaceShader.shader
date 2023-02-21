Shader "Custom/VoxelShader" {
    Properties {
        _MainTex ("Texture", 2D) = "white" {}
        _BumpMap ("Normal Map", 2D) = "bump" {}
        _BumpScale ("Bump Scale", Range(0, 1)) = 0.1
    }
    SubShader {
        Tags {"Queue"="Transparent" "RenderType"="Transparent"}
        LOD 100

        CGPROGRAM
        #pragma surface surf Standard vertex:vert

        sampler2D _MainTex;
        sampler2D _BumpMap;
        float _BumpScale;

        struct Input {
            float2 uv_MainTex;
            float3 worldPos;
            float3 worldNormal;
        };

        void vert(inout appdata_full v, out Input o) {
            UNITY_INITIALIZE_OUTPUT(Input, o);
            o.worldPos = v.vertex;
            o.worldNormal = UnityObjectToWorldNormal(v.normal);
        }

        void surf (Input IN, inout SurfaceOutputStandard o) {
            float4 tex = tex2D(_MainTex, IN.uv_MainTex);
            o.Albedo = tex.rgb;
            o.Alpha = tex.a;

            float3 normal = UnpackNormal(tex2D(_BumpMap, IN.uv_MainTex));
            normal = normalize(mul(normal, (float3x3)UNITY_MATRIX_M));
            o.Normal = normal;

            o.Metallic = 0;
            o.Smoothness = 0.5;
            o.Occlusion = 1;

            IN.worldNormal = normalize(IN.worldNormal);
            float ndotl = max(0, dot(IN.worldNormal, _WorldSpaceLightPos0.xyz));
            o.Emission = ndotl;
        }
        ENDCG
    }
    FallBack "Diffuse"
}