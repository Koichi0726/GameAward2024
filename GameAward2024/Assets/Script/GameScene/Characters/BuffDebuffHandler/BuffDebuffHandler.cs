using System.Security.Cryptography;
using System;
using System.Text;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuffDebuffData
{
	public PlayerParamCoefficient m_playerParamCoefficient = new PlayerParamCoefficient();
	public float m_remainingDuration = 0.0f;   // 残り効果継続時間
	public int m_buffDebuffKey = 0;				// 効果を発揮する弾の名前
}

public class BuffDebuffHandler : MonoBehaviour
{
	List<BuffDebuffData> m_buffDebuffDatas = new List<BuffDebuffData>();

    void Update()
    {
		for(int i = 0; i < m_buffDebuffDatas.Count; ++i)
		{
			BuffDebuffData data = m_buffDebuffDatas[i];

			//--- 係数の情報をプレイヤーへ流す
			PlayerParamCoefficient playerParamCoefficient = PlayerActionControler.PParam;
			playerParamCoefficient.m_addGaugeValue = data.m_playerParamCoefficient.m_addGaugeValue;

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
	/// <param name="bulletName">弾の名前(gameObject.name)</param>
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