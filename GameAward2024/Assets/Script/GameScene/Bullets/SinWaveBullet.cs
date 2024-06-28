using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GameScene;

public class SinWaveBullet : BulletBase
{
	float m_speed;          // 弾の速度
	float m_waveInterval;	// 波が一周する時間
	float m_waveScale;		// 波の大きさ

	float m_timer = 0.0f;
	Rigidbody m_rigidbody;
	Vector3 m_startPos;
	Vector3 m_vToTarget;

	protected override void Start()
	{
		m_bulletKind = BulletDataList.E_BULLET_KIND.SIN_WAVE;

		base.Start();

		TryGetComponent(out m_rigidbody);
		m_startPos = transform.position;
	}

	protected override void Update()
	{
		base.Update();

		//--- 経過時間を加算
		m_timer += Time.deltaTime;
		m_timer = Mathf.Repeat(m_timer, m_waveInterval);
	}

	protected override void FixedUpdate()
	{
		base.FixedUpdate();

		Vector3 forwardMove = m_vToTarget * m_speed * Time.fixedDeltaTime;

		//--- 波の大きさを計算
		float rad = (m_timer / m_waveInterval) * (Mathf.PI * 2.0f);
		float waveOffset = Mathf.Sin(rad) * m_waveScale;
		Vector3 waveMove = transform.up * waveOffset;

		//--- 移動後の座標を計算
		Vector3 pos = m_startPos + forwardMove + waveMove;
		m_rigidbody.MovePosition(pos);

		// 初期位置を更新
		m_startPos += forwardMove;
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
		data[nameof(m_speed			)].TryGetData(out m_speed);
		data[nameof(m_waveInterval	)].TryGetData(out m_waveInterval);
		data[nameof(m_waveScale		)].TryGetData(out m_waveScale);
	}
}