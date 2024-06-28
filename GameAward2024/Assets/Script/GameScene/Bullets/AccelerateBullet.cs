using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GameScene;

public class AccelerateBullet : BulletBase
{
	float m_speed;          // 弾の速度
	float m_maxSpeed;		// 弾の最大速度
	float m_accelerate;     // 弾の加速度

	float m_timer = 0.0f;
	Rigidbody m_rigidbody;
	Vector3 m_vToTarget;

	protected override void Start()
	{
		m_bulletKind = BulletDataList.E_BULLET_KIND.ACCELERATE;

		base.Start();

		TryGetComponent(out m_rigidbody);
	}

	protected override void Update()
	{
		base.Update();

		// 経過時間を加算
		m_timer += Time.deltaTime;

		//--- 速度を計算
		m_speed += m_accelerate * Mathf.Pow(m_timer, 2.0f);
		m_speed = Mathf.Clamp(m_speed, 0.0f, m_maxSpeed);
	}

	protected override void FixedUpdate()
	{
		base.FixedUpdate();

		// 計算されたベクトルを設定
		m_rigidbody.velocity = m_vToTarget * m_speed;	
	}

	protected override void OnChangeTarget(E_TARGET_KIND targetKind)
	{
		//--- ターゲットへのベクトルを計算
		m_vToTarget = m_target.position - transform.position;
		m_vToTarget.Normalize();
	}

	protected override void SetData(Dictionary<string, CSVParamData> data)
	{
		base.SetData(data);
		
		//--- 値の吸出し
		data[nameof(m_speed		)].TryGetData(out m_speed);
		data[nameof(m_maxSpeed	)].TryGetData(out m_maxSpeed);
		data[nameof(m_accelerate)].TryGetData(out m_accelerate);
	}
}