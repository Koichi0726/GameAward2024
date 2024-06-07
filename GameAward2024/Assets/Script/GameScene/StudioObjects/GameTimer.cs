using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GameScene;

public class GameTimer : MonoBehaviour
{
	public float m_stageLimitTime { get; private set; }
	public float m_remainTime { get; private set; }

    // Start is called before the first frame update
    void Start()
    {
		// ステージの情報を取得
		m_stageLimitTime = ManagerContainer.instance.gameManager.selectStageData.m_stageLimitTime;
		m_remainTime = m_stageLimitTime;
	}

	// Update is called once per frame
	void FixedUpdate()
    {
		if (IsTimeUp()) return;

		// 経過時間を減算
		m_remainTime -= Time.deltaTime;
	}

	public bool IsTimeUp()
	{
		return m_remainTime <= 0.0f;
	}
}