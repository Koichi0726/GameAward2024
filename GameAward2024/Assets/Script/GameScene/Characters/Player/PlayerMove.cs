using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using GameScene;

public class PlayerMove : MonoBehaviour
{
	const float MAX_DEGREE = Mathf.PI * 2.0f * Mathf.Rad2Deg;


	PlayerData m_playerData;    // プレイヤーデータの取得用変数
	PlayerParamCoefficient m_paramCoefficient;  // プレイヤーパラメータ係数

	Transform m_enemyTrans; // 目標（座標を使用）

	bool m_isDash = false;	// 走るフラグ
	bool m_isAvoid = false;	// 回避フラグ
	
	Vector2 m_moveDirect = new Vector2();	// 移動する方向
	Vector2 m_period = new Vector2();		// 円運動周期

	void Start()
    {
		CharacterManager characterManager = ManagerContainer.instance.characterManager;
        m_playerData = characterManager.playerData;
		m_paramCoefficient = characterManager.buffDebuffHandler.m_paramCoefficient;
		m_enemyTrans = characterManager.enemyTrans;
		
	}

    void FixedUpdate()
	{
		if (m_isAvoid) Avoid();
		else Move();
	}

	/// <summary>
	/// 移動処理
	/// </summary>
	void Move()
	{
		//--- 円運動周期を計算
		m_moveDirect.Normalize();
		m_period.x = -m_moveDirect.x * m_playerData.HORIZONTAL_MOVE_SPEED;
		m_period.y =  m_moveDirect.y * m_playerData.VERTICAL_MOVE_SPEED;
		if (m_isDash) m_period /= m_playerData.DASH_MOVE_SPEED_MULTIPLIER;

		//--- 移動速度を適用
		m_period /= m_paramCoefficient.m_moveSpeed;
		m_period *= m_paramCoefficient.m_moveDirect;

		//--- 移動処理
		CircularRotation(m_period.x, this.transform.up);
		CircularRotation(m_period.y, this.transform.right);
	}

	/// <summary>
	/// 回避処理
	/// </summary>
	void Avoid()
	{
		//--- 移動処理
		CircularRotation(m_period.x, this.transform.up);
		CircularRotation(m_period.y, this.transform.right);

		// 加速させていく
		m_period *= m_playerData.AVOID_ANCE_MULTIPLIER;

		//--- 回避の限界値で停止させる
		if (Mathf.Abs(m_period.x) >= m_playerData.AVOID_RIMIT_VALUE)
			m_period.x = 0.0f;
		if (Mathf.Abs(m_period.y) >= m_playerData.AVOID_RIMIT_VALUE)
			m_period.y = 0.0f;

		// 回避の終了を判定
		m_isAvoid = (m_period != Vector2.zero);
	}

	/// <summary>
	/// 敵を中心に指定の軸で回転
	/// </summary>
	/// <param name="p">回転量</param>
	/// <param name="axis">回転軸</param>
	public void CircularRotation(float p, Vector3 axis)
	{
		if (Mathf.Approximately(p, 0.0f)) return;

		//--- 変数宣言
		Vector3 center = m_enemyTrans.position;   //回転の中心
		var angleAxis = Quaternion.AngleAxis(MAX_DEGREE / p * Time.deltaTime, axis);     //クオータニオンの計算

		// 移動先を算出
		Vector3 pos = transform.position;
		pos -= center;
		pos = angleAxis * pos;
		pos += center;
		transform.position = pos;
	}

	/// <summary>
	/// 方向転換処理
	/// </summary>
	public void OnChangeDirect(InputAction.CallbackContext context)
	{
		if (!context.performed) return;

		Vector2 axis = context.ReadValue<Vector2>();
		m_moveDirect = axis.normalized;

		//--- タイマーのカウントを始める
		GameTimer gameTimer = ManagerContainer.instance.gameManager.gameTimer;
		if (gameTimer.m_isStart) return;
		gameTimer.StartCount();
	}
	
    /// <summary>
    /// 走り始めた時の処理関数
    /// </summary>
    public void OnDashStart(InputAction.CallbackContext context)
    {
       if (!context.performed) return;
		m_isDash = true;
    }

    /// <summary>
    /// 走り終わった時の処理関数
    /// </summary>
    public void OnDashEnd(InputAction.CallbackContext context)
    {
        if (!context.performed) return;
        m_isDash = false;
    }

	public void OnAvoid(InputAction.CallbackContext context)
	{
		if (!context.performed) return;
		if (m_isAvoid) return;

		//--- 回避する方向を計算
		m_moveDirect.Normalize();
		m_period.x = -m_playerData.AVOID_START_VALUE * m_moveDirect.x;
		m_period.y =  m_playerData.AVOID_START_VALUE * m_moveDirect.y;
		m_period *= m_paramCoefficient.m_moveDirect;
		m_period /= m_paramCoefficient.m_moveSpeed;

		m_isAvoid = true;   // 回避フラグを立てる
	}
}