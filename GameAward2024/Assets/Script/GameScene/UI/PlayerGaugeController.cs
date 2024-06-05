using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GameScene;

public class PlayerGaugeController : MonoBehaviour
{
	[SerializeField] Material m_gaugeMaterial;	//ゲージのマテリアル
	float m_gaugeValue;							//マテリアルに設定する数値
	PlayerActionControler m_playerActionControler;

    void Start()
    {
		m_playerActionControler = ManagerContainer.instance.characterManager.playerActionController;
		m_gaugeValue = m_playerActionControler.m_actionValue / PlayerActionControler.MAX_GAUGE_VALUE;
	}

    void FixedUpdate()
    {
		// マテリアルが設定されていなかった時終了する
		if (m_gaugeMaterial == null) return;

		// 設定する数値を取得して計算
		m_gaugeValue = m_playerActionControler.m_actionValue / PlayerActionControler.MAX_GAUGE_VALUE;

		// マテリアルに適応
		m_gaugeMaterial.SetFloat("_GaugeValue", m_gaugeValue);
	}
}