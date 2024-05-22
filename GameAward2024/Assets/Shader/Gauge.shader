Shader "UI/Unlit/Gauge"
{
    Properties
    {
        _MainTex ("Texture", 2D) = "white" {}
		_GaugeValue("GaugeValue", Range(0.0, 1.0)) = 0.0
    }
    SubShader
    {
		Cull Off ZWrite Off ZTest Always

        Pass
        {
            HLSLPROGRAM
            #pragma vertex vert
            #pragma fragment frag

            #include "UnityCG.cginc"

            struct appdata
            {
                float4 vertex : POSITION;
                float2 uv : TEXCOORD0;
            };

            struct v2f
            {
                float2 uv : TEXCOORD0;
                float4 vertex : SV_POSITION;
            };

            v2f vert (appdata vin)
            {
                v2f vout;
                vout.vertex = UnityObjectToClipPos(vin.vertex);
				vout.uv = vin.uv;
                return vout;
            }

            sampler2D _MainTex;
            float _GaugeValue;

            float4 frag (v2f pin) : SV_Target
            {
                // sample the texture
                float4 color = tex2D(_MainTex, pin.uv);
				if (pin.uv.y > _GaugeValue) discard;
				if (color.a <= 0.0f) discard;
				
                return color;
            }
            ENDHLSL
        }
    }
}