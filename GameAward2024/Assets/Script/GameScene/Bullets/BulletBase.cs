using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletBase : MonoBehaviour
{
	[SerializeField]
	float m_destroyTime;
	float m_deltaTime = 0.0f;

    protected void Update()
    {
		//--- 消滅するまでの時間をカウント
		if (m_deltaTime < m_destroyTime)
		{
			Destroy(this);  // 自身を破壊
			return;
		}
		m_deltaTime += Time.deltaTime;
    }
}