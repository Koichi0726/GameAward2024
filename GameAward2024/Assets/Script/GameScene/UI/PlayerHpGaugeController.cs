using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GameScene;

public class PlayerHpGaugeController : MonoBehaviour
{
	[SerializeField] Material m_gaugeMaterial;	//ゲージのマテリアル
	float m_gaugeValue  =1.0f;					//マテリアルに設定する数値
	PlayerControler m_playerControler;
	PlayerData m_playerData;

    void Start()
    {
		//--- プレイヤーに関するデータを取得
		var characterManager = ManagerContainer.instance.characterManager;
		m_playerData = characterManager.playerData;
		m_playerControler = characterManager.playerController;
	}

    void Update()
    {
		// マテリアルが設定されていなかった時終了する
		if (m_gaugeMaterial == null) return;

		//--- 設定する数値を取得して計算
		m_gaugeValue = m_playerControler.m_hp / m_playerData.MAX_HP;
		m_gaugeValue = Mathf.Clamp01(m_gaugeValue);

		// マテリアルに適応
		m_gaugeMaterial.SetFloat("_GaugeValue", m_gaugeValue);
	}
}