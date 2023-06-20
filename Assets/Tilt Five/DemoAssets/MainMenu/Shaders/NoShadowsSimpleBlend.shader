Shader "Custom/No Shadows Simple Blend"
{
    Properties
    {
        _MainTex("Texture", 2D) = "white" {}
    }

        SubShader{
            Tags { "Queue" = "Transparent"}
            LOD 100
            // ZWrite Off
            Cull Off
            AlphaToMask On
            Blend SrcAlpha OneMinusSrcAlpha

            Pass {
                SetTexture[_MainTex] { combine texture }
            }
    }
        FallBack "Diffuse"
}