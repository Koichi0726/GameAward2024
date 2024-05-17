using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameTimer : MonoBehaviour
{
	public float m_stageLimitTime { get; private set; }
	public float m_remainTime { get; private set; }

    // Start is called before the first frame update
    void Start()
    {
		// TODO:前シーンのステージ選択を基に引数を指定
		//int stageNum = 1;
		// ステージの情報を取得
		//StageInfo stageInfo = GameScene.ManagerContainer.GetManagerContainer().m_stageInfo;
		// ステージ毎に設定された制限時間を取得
		//m_stageLimitTime = stageInfo.GetStageInfo(stageNum).STAGE_LIMIT_TIME;
		m_stageLimitTime = 10.0f;
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