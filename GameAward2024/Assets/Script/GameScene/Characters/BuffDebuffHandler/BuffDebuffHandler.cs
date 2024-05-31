using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuffDebuffData
{
	public PlayerParamCoefficient m_playerParamCoefficient;
	public float m_remainingDuration;	// 残り効果継続時間
}

public class BuffDebuffHandler : MonoBehaviour
{
	List<BuffDebuffData> m_buffDebuffDatas = new List<BuffDebuffData>();

    void Update()
    {
		foreach(BuffDebuffData data in m_buffDebuffDatas)
		{
			//--- 係数の情報をプレイヤーへ流す
			PlayerParamCoefficient playerParamCoefficient = PlayerActionControler.PParam;
			playerParamCoefficient = data.m_playerParamCoefficient;

			// 効果の残り時間を減らしていく
			data.m_remainingDuration -= Time.deltaTime;

			if (data.m_remainingDuration > 0.0f) continue;
			// 残り時間が0秒になった場合、データを消去
			m_buffDebuffDatas.Remove(data);
		}
    }

	public void AddBuffDebuff(BuffDebuffData data)
	{
		m_buffDebuffDatas.Add(data);
	}
}