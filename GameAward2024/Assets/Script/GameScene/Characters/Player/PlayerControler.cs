using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using GameScene;

public class PlayerControler : MonoBehaviour
{
    PlayerData m_playerData;   // プレイヤーデータの取得用変数
	public float m_hp { get; private set; }
	Transform m_enemyTrans;

	void Start()
    {
		//--- 各種変数初期化
		CharacterManager characterManager = ManagerContainer.instance.characterManager;
        m_playerData = characterManager.playerData;
		m_hp = m_playerData.MAX_HP;     // HPの数値初期化
		m_enemyTrans = characterManager.enemyTrans;
    }

	void Update()
	{
		//--- 敵に正面を向けるように回転
		Vector3 targetPos = m_enemyTrans.position;
		Vector3 up = transform.up;
		transform.LookAt(targetPos, up);
	}

	/// <summary>
	/// プレイヤーのHPを減算
	/// </summary>
	/// <param name="damage">ダメージ量</param>
	public void SubHp(float damage)
	{
		m_hp -= damage;
		m_hp = Mathf.Clamp(m_hp, 0.0f, m_playerData.MAX_HP);
	}

	/// <summary>
	/// 死亡しているかを判定
	/// </summary>
	/// <returns>死亡フラグ</returns>
	public bool IsDead()
	{
		return m_hp <= 0.0f;
	}
}