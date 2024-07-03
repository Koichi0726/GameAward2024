using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GameScene;

public class SpiralBullet : BulletBase
{
	float m_speed;			// ’e‚Ì‘¬“x
	float m_rotTime;		// ˆê‰ñ“]‚ÉŠ|‚©‚éŠÔ
	float m_radScaleSpeed;	// ”¼Œa‚ÌŠg‘å‘¬“x
	float m_maxSpiralRad;   // ‰ñ“]‚Ì”¼Œa

	Rigidbody m_rigidbody;
	float m_radScale = 0.0f;
	float m_moveTimer = 0.0f;
	float m_rotTimer = 0.0f;
	Vector3 m_startPos;
	Vector3 m_vToTarget;

	protected override void Start()
	{
		m_bulletKind = BulletDataList.E_BULLET_KIND.SPIRAL;

		base.Start();

		TryGetComponent(out m_rigidbody);
		m_startPos = transform.position;

		MoveSpiral();	// n‚ß‚ÉˆÊ’u‚ğŒvZ
	}

	protected override void FixedUpdate()
	{
		base.FixedUpdate();

		MoveSpiral();
	}

	protected override void OnChangeTarget(E_TARGET_KIND targetKind)
	{
		//--- ƒ^[ƒQƒbƒg‚Ö‚ÌƒxƒNƒgƒ‹‚ğŒvZ
		m_vToTarget = m_target.position - transform.position;
		m_vToTarget.Normalize();

		m_startPos = transform.position;

		m_moveTimer = 0.0f;
	}

	protected override void SetData(Dictionary<string, CSVParamData> data)
	{
		base.SetData(data);
		
		//--- ’l‚Ì‹zo‚µ
		data[nameof(m_speed		)].TryGetData(out m_speed);
		data[nameof(m_rotTime	)].TryGetData(out m_rotTime);
		data[nameof(m_radScaleSpeed	)].TryGetData(out m_radScaleSpeed);
		data[nameof(m_maxSpiralRad	)].TryGetData(out m_maxSpiralRad);
	}

	void MoveSpiral()
	{
		//--- —†ù‚Ì“®‚«‚ğŒvZ
		float rot = (m_rotTimer / m_rotTime) * Mathf.PI * 2.0f;
		float offsetX = Mathf.Cos(rot) * m_radScale;
		float offsetY = Mathf.Sin(rot) * m_radScale;
		Vector3 spiralMove = transform.right * offsetX + transform.up * offsetY;

		//--- ’e‚ÌV‚µ‚¢ˆÊ’u‚ğŒvZ
		Vector3 forwardMove = m_vToTarget * m_speed * m_moveTimer;
		Vector3 pos = m_startPos + forwardMove + spiralMove;

		m_rigidbody.MovePosition(pos);

		//--- ‰ñ“]‚Ì”¼Œa‚ğŠg‘å
		m_radScale += m_radScaleSpeed * Time.fixedDeltaTime;
		m_radScale = Mathf.Clamp(m_radScale, 0.0f, m_maxSpiralRad);

		// Œo‰ßŠÔ‚ğ‰ÁZ
		m_rotTimer	+= Time.fixedDeltaTime;
		m_moveTimer += Time.fixedDeltaTime;
	}
}