using System.Security.Cryptography;
using System;
using System.Text;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GameScene;

public class PlayerParamCoefficient // プレイヤーに関わる係数
{
	public Vector2 m_moveDirect
		= new Vector2(1.0f, 1.0f);			// 移動方向の係数
	public float m_moveSpeed = 1.0f;		// 移動速度の係数

	public void Init()
	{
		m_moveDirect	= new Vector2(1.0f, 1.0f);
		m_moveSpeed		= 1.0f;
	}

	public void InsertData(PlayerParamCoefficient insertData)
	{
		m_moveDirect	*= insertData.m_moveDirect;
		m_moveSpeed		*= insertData.m_moveSpeed;
	}
}

public class BuffDebuffData
{
	public PlayerParamCoefficient m_paramCoefficient = new PlayerParamCoefficient();
	public float m_remainingDuration = 0.0f;   // 残り効果継続時間
	public int m_buffDebuffKey = 0;				// 効果を発揮する弾の名前
}

[DefaultExecutionOrder(-1)]
public class BuffDebuffHandler : MonoBehaviour
{
	public PlayerParamCoefficient m_paramCoefficient { get; private set; } = new PlayerParamCoefficient();
	List<BuffDebuffData> m_buffDebuffDatas = new List<BuffDebuffData>();

	void FixedUpdate()
    {
		m_paramCoefficient.Init();	// 係数の情報をリセット

		for (int i = 0; i < m_buffDebuffDatas.Count; ++i)
		{
			BuffDebuffData data = m_buffDebuffDatas[i];

			// 係数の情報をプレイヤーへ流す
			m_paramCoefficient.InsertData(data.m_paramCoefficient);

			// 効果の残り時間を減らしていく
			data.m_remainingDuration -= Time.deltaTime;

			if (data.m_remainingDuration > 0.0f) continue;
			// 残り時間が0秒になった場合、データを消去
			m_buffDebuffDatas.Remove(data);
			--i;
		}
	}

	/// <summary>
	/// バフ・デバフを追加
	/// </summary>
	/// <param name="data">バフ・デバフのデータ</param>
	/// <param name="bulletName">弾の名前(this.GetType().Name)</param>
	public void AddBuffDebuff(BuffDebuffData data, string bulletName)
	{
		//--- 弾の名前から一意のバフ・デバフのキーを計算
		using (var sha256 = SHA256.Create())
		{
			var hashBytes = sha256.ComputeHash(Encoding.UTF8.GetBytes(bulletName));
			data.m_buffDebuffKey = BitConverter.ToInt32(hashBytes, 0);
		}

		foreach(BuffDebuffData listData in m_buffDebuffDatas)
		{
			//--- キーが一致する場合は、効果の残り時間をリセット
			if (listData.m_buffDebuffKey != data.m_buffDebuffKey) continue;
			listData.m_remainingDuration = data.m_remainingDuration;
			return;
		}

		// リストに該当のデータが無ければ追加
		m_buffDebuffDatas.Add(data);
	}
}