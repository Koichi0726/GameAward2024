using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "PlayerData", menuName = "ScriptableObjects/PlayerData")]
public class PlayerData : ScriptableObject
{
	//--- プレイヤーアクションの列挙型
	public enum E_PLAYER_ACTION
	{
		E_MOVE = 0,     //移動
		E_DASH,         //走る
		E_AVOID,        //回避
		E_MAX,
	}

	[Header("初期化用")]
	//--- ゲーム開始時のゲージの数値の定数
	[SerializeField]
	float m_startGaugeValue = 60.0f;
	public float START_GAUGE_VALUE => m_startGaugeValue;

	[Space(10)]
	[Header("プレイヤーパラメータ")]
	[SerializeField]
	float m_verticalMoveSpeed = 0.1f;
	/// <summary>
	/// 縦の移動速度
	/// </summary>
	public float VERTICAL_MOVE_SPEED => m_verticalMoveSpeed;

	[SerializeField]
	float m_horizontalMoveSpeed = 4.0f;
	/// <summary>
	/// 横の移動速度
	/// </summary>
	public float HORIZONTAL_MOVE_SPEED => m_horizontalMoveSpeed;

	[SerializeField]
	float m_dodgeRotXZ = 3.0f;
	/// <summary>
	/// 縦の回避時の移動角度
	/// </summary>
	public float DODGE_ROT_XZ => m_dodgeRotXZ;

	[SerializeField]
	float m_dodgeRotY = 3.0f;
	/// <summary>
	/// 横の回避時の移動角度
	/// </summary>
	public float DODGE_ROT_Y => m_dodgeRotY;

	[Space(10)]
	[Header("その他")]
	//--- ゲージの値を変化させる定数
	[SerializeField]
	float m_stopGaugeValue = -0.3f;
	/// <summary>
	/// 停止
	/// </summary>
	public float STOP_GAUGE_VALUE => m_stopGaugeValue;

	[SerializeField]
	float m_moveGaugeValue = 0.1f;
	/// <summary>
	/// 移動
	/// </summary>
	public float MOVE_GAUGE_VALUE => m_moveGaugeValue;

	[SerializeField]
	float m_dashGaugeValue = 0.2f;
	/// <summary>
	/// 走る
	/// </summary>
	public float DASH_GAUGE_VALUE => m_dashGaugeValue;

	[SerializeField]
	float m_avoidGaugeValue = 0.5f;
	/// <summary>
	/// 回避
	/// </summary>
	public float AVOID_GAUGE_VALUE => m_avoidGaugeValue;
}