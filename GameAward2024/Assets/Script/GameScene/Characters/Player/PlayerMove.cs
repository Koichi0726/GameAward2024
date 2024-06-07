using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using GameScene;

public class PlayerMove : MonoBehaviour
{
	const float MAX_DEGREE = Mathf.PI * 2.0f * Mathf.Rad2Deg;

	// プレイヤーデータの取得用変数
	PlayerData m_playerData;

	// プレイヤーパラメータ係数
	PlayerParamCoefficient m_paramCoefficient;

	PlayerActionControler m_playerActionControler;

	// 目標（座標を使用）
	Transform m_enemyTrans;

    // 走るフラグ
    bool m_isDash = false;

    // 前フレームのワールド座標
    Vector3 m_prePos;

    // プレイヤーのポジション
    Vector3 m_pos;

	// 実際に使用する円運動周期
	Vector2 m_period = new Vector2();

    void Start()
    {
		CharacterManager characterManager = ManagerContainer.instance.characterManager;
        m_playerData = characterManager.playerData;
		m_paramCoefficient = characterManager.buffDebuffHandler.m_paramCoefficient;
		m_playerActionControler = characterManager.playerActionController;
		m_enemyTrans = characterManager.enemyTrans;
		m_pos = m_prePos = transform.position;
    }

    void FixedUpdate()
    {
        // 次のUpdateで使うための前フレーム位置更新
        m_prePos = m_pos;

        //--- 左右左右の移動がなければ終了する
        if (Mathf.Approximately(m_period.x, 0.0f) && Mathf.Approximately(m_period.y, 0.0f))
			return;

		// ダッシュ時
        if (m_isDash)	m_period /= m_playerData.DASH_MOVE_SPEED_MULTIPLIER;

		// 移動速度を適用
		m_period /= m_paramCoefficient.m_moveSpeed;
		m_period *= m_paramCoefficient.m_moveDirect;

		//--- 移動処理
		if (m_period.x != 0.0f) PlayerCircularRotation(m_period.x, this.transform.up);
        if (m_period.y != 0.0f) PlayerCircularRotation(m_period.y, this.transform.right);
        transform.position = m_pos;
		m_paramCoefficient.m_moveDirect = m_period.normalized;

		// 移動量をリセット
		m_period.x = m_period.y = 0.0f;
    }

    /// <summary>
    /// 上入力したときの処理関数
    /// </summary>
    public void OnMoveUp()
    {
		m_period.y = m_playerData.VERTICAL_MOVE_SPEED;
        ActionEntry();
    }

    /// <summary>
    /// 下入力したときの処理関数
    /// </summary>
    public void OnMoveDown()
    {
		m_period.y = -m_playerData.VERTICAL_MOVE_SPEED;
        ActionEntry();
    }

    /// <summary>
    /// 左入力したときの処理関数
    /// </summary>
    public void OnMoveLeft()
    {
		m_period.x = m_playerData.HORIZONTAL_MOVE_SPEED;
        ActionEntry();
    }

    /// <summary>
    /// 右入力したときの処理関数
    /// </summary>
    public void OnMoveRight()
    {
		m_period.x = -m_playerData.HORIZONTAL_MOVE_SPEED;
        ActionEntry();
    }

    /// <summary>
    /// 走り始めた時の処理関数
    /// </summary>
    public void OnDashStart(InputAction.CallbackContext context)
    {
        if (!context.started) return;
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

    /// <summary>
    /// 移動方法に応じてアクションを登録する関数
    /// </summary>
    private void ActionEntry()
    {
        if(!m_isDash)
        {
			m_playerActionControler.AddAction(PlayerData.E_PLAYER_ACTION.MOVE);
        }
        else
        {
			m_playerActionControler.AddAction(PlayerData.E_PLAYER_ACTION.DASH);
        }
    }

    public void PlayerCircularRotation(float p, Vector3 axis)
    {
        //--- 変数宣言
        Vector3 center = m_enemyTrans.position;   //回転の中心
        var angleAxis = Quaternion.AngleAxis(MAX_DEGREE / p * Time.deltaTime, axis);     //クオータニオンの計算

        // 移動先を算出
        m_pos -= center;
        m_pos = angleAxis * m_pos;
        m_pos += center;
    }
}