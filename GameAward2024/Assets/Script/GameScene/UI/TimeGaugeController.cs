using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimeGaugeController : MonoBehaviour
{
	// TODO:ScriptableObjectへ移動
	const float GAUGE_MAX_VALUE = 1.0f;
	const float GAUGE_MIN_VALUE = 0.0f;

	[SerializeField]
	Material m_gaugeMaterial;
	float m_gaugeValue = 1.0f;
	GameTimer m_gameTimer;

    // Start is called before the first frame update
    void Start()
	{
		// ゲーム用のタイマーを取得
		TryGetComponent<GameTimer>(out m_gameTimer);
	}

	// Update is called once per frame
	void Update()
    {
		if (m_gameTimer == null) return;
		if (m_gaugeMaterial == null) return;

		//--- ゲージの値を計算(0.0～1.0)
		m_gaugeValue = m_gameTimer.m_remainTime / m_gameTimer.m_stageLimitTime;
		m_gaugeValue = Mathf.Clamp(m_gaugeValue, GAUGE_MIN_VALUE, GAUGE_MAX_VALUE);

		// TODO:マジックナンバーはScriptableObjectに置き換える
		m_gaugeMaterial.SetFloat("_GaugeValue", m_gaugeValue);
	}
}