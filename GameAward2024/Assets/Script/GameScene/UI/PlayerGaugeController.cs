using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerGaugeController : MonoBehaviour
{
	[SerializeField] Material m_gaugeMaterial;	//ゲージのマテリアル
	float m_gaugeValue = 0.4f;	//マテリアルに設定する数値

    // Start is called before the first frame update
    void Start()
    {
		//初期化
		m_gaugeValue = PlayerActionControler.ActionValue / 100.0f;
	}

    // Update is called once per frame
    void FixedUpdate()
    {
		//マテリアルが設定されていなかった時終了する
		if (m_gaugeMaterial == null) return;

		//設定する数値を取得して計算
		m_gaugeValue = PlayerActionControler.ActionValue / 100.0f;

		//マテリアルに適応
		m_gaugeMaterial.SetFloat("_GaugeValue", m_gaugeValue);
	}
}