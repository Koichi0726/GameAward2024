using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GameScene;

public class RippleBullet : BulletBase
{
	float m_initScale;		// 初期スケール
	float m_spritCircle;	// 円の分割数
	float m_speed;          // 弾の速度
	float m_scaleSpeed;     // 拡大速度
	float m_maxScale;       // 最大スケール

	float m_timer = 0.0f;
	Rigidbody m_rigidbody;
	Vector3 m_vToTarget;

	protected override void Start()
	{
		m_bulletKind = BulletDataList.E_BULLET_KIND.RIPPLE;

		base.Start();

		TryGetComponent(out m_rigidbody);
	}

	protected override void Update()
	{
		base.Update();

		//--- 大きくしていく
		Vector3 localScale = transform.localScale;
		// 計算結果(初期スケール + 拡大速度 + 経過時間)を0～最大スケールに収める
		float scale = Mathf.Clamp(m_initScale + m_scaleSpeed * m_timer, 0.0f, m_maxScale);
		transform.localScale = new Vector3(scale, scale, scale);

		// 経過時間を加算
		m_timer += Time.deltaTime;
	}

	protected override void FixedUpdate()
	{
		base.FixedUpdate();

		// 計算されたベクトルを設定
		m_rigidbody.MovePosition(transform.position + m_vToTarget * m_speed * Time.fixedDeltaTime);
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
		data[nameof(m_initScale		)].TryGetData(out m_initScale);
		data[nameof(m_spritCircle	)].TryGetData(out m_spritCircle);
		data[nameof(m_speed			)].TryGetData(out m_speed);
		data[nameof(m_scaleSpeed	)].TryGetData(out m_scaleSpeed);
		data[nameof(m_maxScale		)].TryGetData(out m_maxScale);
	}
}