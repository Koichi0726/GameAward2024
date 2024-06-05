using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "PlayerData", menuName = "ScriptableObjects/PlayerData")]
public class PlayerData : ScriptableObject
{
	/// <summary>
	/// CSVファイルのパス
	/// </summary>
	const string CSV_FILE_PATH = "SettingCSV/Player.csv";

	//--- プレイヤーアクションの列挙型
	public enum E_PLAYER_ACTION
	{
		E_MOVE = 0,     //移動
		E_DASH,         //走る
		E_AVOID,        //回避
		E_MAX,
	}

	//--------------------
	// 初期化用パラメータ

	float m_startGaugeValue = 60.0f;
	/// <summary>
	/// ゲージの初期値
	/// </summary>
	public float START_GAUGE_VALUE => m_startGaugeValue;

	//--------------------
	// 移動用パラメータ

	float m_verticalMoveSpeed = 0.1f;
	/// <summary>
	/// 縦の移動速度
	/// </summary>
	public float VERTICAL_MOVE_SPEED => m_verticalMoveSpeed;

	float m_horizontalMoveSpeed = 4.0f;
	/// <summary>
	/// 横の移動速度
	/// </summary>
	public float HORIZONTAL_MOVE_SPEED => m_horizontalMoveSpeed;

	float m_dashMoveSpeedMultiplier = 2.0f;
	/// <summary>
	/// ダッシュ時の移動速度の乗数
	/// </summary>
	public float DASH_MOVE_SPEED_MULTIPLIER => m_dashMoveSpeedMultiplier;

	float m_avoidAnceMultiplier = 1.3f;
	/// <summary>
	/// 回避の速度の減少率（大きいほど速く止まる）
	/// </summary>
	public float AVOID_ANCE_MULTIPLIER => m_avoidAnceMultiplier;

	float m_avoidStartValue = 0.3f;
	/// <summary>
	/// 回避の初期値（大きいほど初速が遅い）
	/// </summary>
	public float AVOID_START_VALUE => m_avoidStartValue;

	float m_avoidRimitValue = 8.0f;
	/// <summary>
	/// 回避の停止する値（大きいほど停止までの動きが伸びる）
	/// </summary>
	public float AVOID_RIMIT_VALUE => m_avoidRimitValue;

	//--------------------
	// ゲージの値の変化量

	float m_stopGaugeValue = 0.3f;
	/// <summary>
	/// 停止時の変化量
	/// </summary>
	public float STOP_GAUGE_VALUE => m_stopGaugeValue;

	float m_moveGaugeValue = 0.1f;
	/// <summary>
	/// 移動時の変化量
	/// </summary>
	public float MOVE_GAUGE_VALUE => m_moveGaugeValue;

	float m_dashGaugeValue = 0.2f;
	/// <summary>
	/// 走る時の変化量
	/// </summary>
	public float DASH_GAUGE_VALUE => m_dashGaugeValue;

	float m_avoidGaugeValue = 0.5f;
	/// <summary>
	/// 回避時の変化量
	/// </summary>
	public float AVOID_GAUGE_VALUE => m_avoidGaugeValue;

	[SerializeField]
	TextAsset m_csvText;
	CSVReader m_csvReader = new CSVReader();

	public void Load()
	{
		//--- CSVファイルを読み込み
#if DEVELOPMENT_BUILD
		m_csvReader.LoadCSV(CSV_FILE_PATH);
#else
		m_csvReader.LoadCSV(m_csvText);
#endif
	}

	public void GetDatas()
	{
		var paramDatas = m_csvReader.m_csvDatas;

		//--- 値の吸出し
		paramDatas[nameof(m_startGaugeValue			)].TryGetData(out m_startGaugeValue);
		paramDatas[nameof(m_verticalMoveSpeed		)].TryGetData(out m_verticalMoveSpeed);
		paramDatas[nameof(m_horizontalMoveSpeed		)].TryGetData(out m_horizontalMoveSpeed);
		paramDatas[nameof(m_dashMoveSpeedMultiplier	)].TryGetData(out m_dashMoveSpeedMultiplier);
		paramDatas[nameof(m_avoidAnceMultiplier		)].TryGetData(out m_avoidAnceMultiplier);
		paramDatas[nameof(m_avoidStartValue			)].TryGetData(out m_avoidStartValue);
		paramDatas[nameof(m_avoidRimitValue			)].TryGetData(out m_avoidRimitValue);
		paramDatas[nameof(m_stopGaugeValue			)].TryGetData(out m_stopGaugeValue);
		paramDatas[nameof(m_moveGaugeValue			)].TryGetData(out m_moveGaugeValue);
		paramDatas[nameof(m_dashGaugeValue			)].TryGetData(out m_dashGaugeValue);
		paramDatas[nameof(m_avoidGaugeValue			)].TryGetData(out m_avoidGaugeValue);
	}
}