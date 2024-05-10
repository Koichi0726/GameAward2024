Shader "UI/Unlit/CircleGauge"
{
    Properties
    {
        _MainTex ("Texture", 2D) = "white" {}
		_GaugeValue("GaugeValue", Range(0.0, 1.0)) = 0.0
		_InnerRadius("InnerRadisu", Range(0.0, 0.5)) = 0.0
		_OuterRadius("OuterRadisu", Range(0.0, 0.5)) = 0.5
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
            float _InnerRadius;
            float _OuterRadius;

            float4 frag (v2f pin) : SV_Target
            {
				//--- UVの値から、中心までの距離を求める
				float2 center = float2(0.5f,0.5f);
				float2 offset = pin.uv - center;
				float r = length(offset);

				//--- 表示しない部分を計算
				// step(a, b) (a <= b) ? 1 : 0
				// └aがb以下の場合に1を返す
				float inner = step(_InnerRadius, r);	// 内側判定
				float outer = step(r, _OuterRadius);	// 外側判定
				if (inner * outer <= 0.0f) discard;		// 円の内側と外側は表示しない

				//--- 極座標の角度を求める
				float pi = 3.141592f;
				//NOTE:Y軸が反転しているため
				float rad = atan2(offset.y, offset.x);	// 座標から角度を計算
				rad += pi * 0.5f;						// 角度を変えてゲージのスタート地点を変更
				rad /= pi;								// -3.14～3.14を-1～1に変換
				rad = rad * 0.5f + 0.5f;				// -1～1 → -0.5～0.5 → 0～1に変化
				// frac(a)─小数部を取り出す
				rad = frac(rad);						// ゲージのスタート地点の変更で0.25から1.25になってるので、

				//--- ゲージを計算
				float gage = rad;
				gage = 1.0f - step(_GaugeValue, gage);	
				if (gage <= 0.0f) discard;	// 表示すべき部分か判定

				//--- ゲージの表示部分の色を計算
				float4 color = tex2D(_MainTex, float2(rad, r * 2.0f));
				float4 zero = float4(0.0f, 0.0f, 0.0f, 0.0f);	// 表示なしの色
				color = lerp(zero, color, gage);				// 表示部分のみ色を設定

				return color;
            }
            ENDHLSL
        }
    }
}