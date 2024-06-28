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
	const string CSV_FILE_PATH = "SettingCSV/PlayerData/Player.csv";

	float m_maxHp = 100.0f;
	/// <summary>
	/// HPの最大値
	/// </summary>
	public float MAX_HP => m_maxHp;

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
		paramDatas[nameof(m_maxHp			)].TryGetData(out m_maxHp);
		paramDatas[nameof(m_verticalMoveSpeed		)].TryGetData(out m_verticalMoveSpeed);
		paramDatas[nameof(m_horizontalMoveSpeed		)].TryGetData(out m_horizontalMoveSpeed);
		paramDatas[nameof(m_dashMoveSpeedMultiplier	)].TryGetData(out m_dashMoveSpeedMultiplier);
		paramDatas[nameof(m_avoidAnceMultiplier		)].TryGetData(out m_avoidAnceMultiplier);
		paramDatas[nameof(m_avoidStartValue			)].TryGetData(out m_avoidStartValue);
		paramDatas[nameof(m_avoidRimitValue			)].TryGetData(out m_avoidRimitValue);
	}
}