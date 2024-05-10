using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimeGaugeController : MonoBehaviour
{
	[SerializeField]
	Material m_gaugeMaterial;
	float m_gaugeValue = 1.0f;
	// GameTimer m_gameTimer

    // Start is called before the first frame update
    void Start()
    {
		// m_gameTimer = ManagerContainer.GameManager.m_gameTimer;
	}

	// Update is called once per frame
	void Update()
    {
		// if(m_gameTimer == null) return;
		if (m_gaugeMaterial == null) return;

		// g_gaugeValue = m_gameTimer.m_remainTime / m_gameTimer.m_stageLimitTime;

		// TODO:マジックナンバーはScriptableObjectに置き換える
		m_gaugeMaterial.SetFloat("_GaugeValue", m_gaugeValue);
	}
}