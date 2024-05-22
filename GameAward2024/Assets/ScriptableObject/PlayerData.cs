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

	[field: Header("初期化用")]
	//--- ゲーム開始時のゲージの数値の定数
	[field: SerializeField]
	public float START_GAUGE_VALUE { get; private set; } = 60.0f;

	[field: Space(10)]
	[field: Header("プレイヤーパラメータ")]
	[field: SerializeField]
	public float VERTICAL_MOVE_SPEED { get; private set; } = 0.1f;		// 縦の移動速度
	[field: SerializeField]
	public float HORIZONTAL_MOVE_SPEED { get; private set; } = 4.0f;    // 横の移動速度
	[field: SerializeField]
	public float DODGE_ROT_XZ { get; private set; } = 3.0f;				// 縦の回避時の移動角度
	[field: SerializeField]
	public float DODGE_ROT_Y { get; private set; } = 3.0f;              // 横の回避時の移動角度

	[field: Space(10)]
	[field: Header("その他")]
	//--- ゲージの値を変化させる定数
	[field: SerializeField]
	public float STOP_GAUGE_VALUE { get; private set; } = -0.3f;     // 停止
	[field: SerializeField]
	public float MOVE_GAUGE_VALUE { get; private set; } = 0.1f;      // 移動
	[field: SerializeField]
	public float DASH_GAUGE_VALUE { get; private set; } = 0.2f;      // 走る
	[field: SerializeField]
	public float AVOID_GAUGE_VALUE { get; private set; } = 0.5f;     // 回避
}