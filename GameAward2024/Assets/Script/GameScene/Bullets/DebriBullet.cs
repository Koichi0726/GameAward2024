using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GameScene;

public class DebriBullet : BulletBase
{
	float m_speed;			// 弾の速度
	float m_stopDistance;	// 停止する距離(敵との距離)

	Rigidbody m_rigidbody;
	Vector3 m_vToTarget;
	Transform m_enemyTrans;

	protected override void Start()
	{
		m_bulletKind = BulletDataList.E_BULLET_KIND.DEBRI;

		TryGetComponent(out m_rigidbody);
		base.Start();

		// 敵の情報を取得
		CharacterManager characterManager = ManagerContainer.instance.characterManager;
		m_enemyTrans = characterManager.enemyTrans;
	}

	protected override void FixedUpdate()
	{
		base.FixedUpdate();

		float toEnemyDist	= Vector3.Distance(transform.position, m_target.position);
		if (toEnemyDist > 10.0f) return;

		// 計算されたベクトルを設定
		m_rigidbody.velocity = m_vToTarget * m_speed;
	}

	protected override void FixedUpdateToPlayer()
	{
		//--- 弾がプレイヤーの軌道上に到達したら静止指せる
		float toEnemyDist = Vector3.Distance(transform.position, m_enemyTrans.position);
		if (toEnemyDist >= m_stopDistance)
		{
			m_rigidbody.isKinematic = true;
			return;
		}

		// 計算されたベクトルを設定
		m_rigidbody.velocity = m_vToTarget * m_speed;
	}

	protected override void FixedUpdateToEnemy()
	{
		// 計算されたベクトルを設定
		m_rigidbody.velocity = m_vToTarget * m_speed;
	}

	protected override void OnChangeTarget(E_TARGET_KIND targetKind)
	{
		//--- ターゲットへのベクトルを計算
		m_vToTarget = m_target.position - transform.position;
		m_vToTarget.Normalize();

		m_rigidbody.isKinematic = false;
	}

	protected override void SetData(Dictionary<string, CSVParamData> data)
	{
		base.SetData(data);
		
		//--- 値の吸出し
		data[nameof(m_speed			)].TryGetData(out m_speed);
		data[nameof(m_stopDistance	)].TryGetData(out m_stopDistance);
	}
}