using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GameScene;

public class EnemyHpGaugeController : MonoBehaviour
{
	[SerializeField] Material m_gaugeMaterial;	//ゲージのマテリアル
	float m_gaugeValue = 1.0f;					//マテリアルに設定する数値
	EnemyBase m_enemy;

    void Start()
    {
		m_enemy = ManagerContainer.instance.characterManager.enemyData;
	}

    void Update()
    {
		// マテリアルが設定されていなかった時終了する
		if (m_gaugeMaterial == null) return;

		//--- 設定する数値を取得して計算
		m_gaugeValue = m_enemy.hp / m_enemy.m_maxHp;
		m_gaugeValue = Mathf.Clamp01(m_gaugeValue);

		// マテリアルに適応
		m_gaugeMaterial.SetFloat("_GaugeValue", m_gaugeValue);
	}
}