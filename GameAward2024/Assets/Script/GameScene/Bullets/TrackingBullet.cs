using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GameScene;

public class TrackingBullet : BulletBase
{ 
	float m_period;			// 着弾までの時間
	float m_limitAccel;		// 加速度の上限値
	Vector3 m_velocity;

    protected override void Start()
    {
		m_bulletKind = BulletDataList.E_BULLET_KIND.TRACKING;

        base.Start();
	}

	protected override void OnChangeTarget(E_TARGET_KIND targetKind)
	{
		m_velocity = (m_target.position - transform.position).normalized;
	}

    protected override void Update()
	{
		base.Update();

		Vector3 pos = transform.position;		// 弾の座標
		Vector3 accel = Vector3.zero;           // 弾の加速度
		Vector3 diff = m_target.position - pos; // ターゲットとの位置の差分

		//--- 加速度を計算
		accel = (diff - m_velocity * m_period) * 2.0f / Mathf.Pow(m_period, 2.0f);
		if (accel.magnitude > m_limitAccel)
			accel = accel.normalized * m_limitAccel;

		m_period -= Time.deltaTime;

		//--- 座標を更新
		m_velocity += accel * Time.deltaTime;
		pos += m_velocity * Time.deltaTime;
		transform.position = pos;
	}

	protected override void SetData(Dictionary<string, CSVParamData> data)
	{
		base.SetData(data);

		//--- 値の吸出し
		data[nameof(m_period	)].TryGetData(out m_period);
		data[nameof(m_limitAccel)].TryGetData(out m_limitAccel);
	}
}