using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerGaugeController : MonoBehaviour
{
	[SerializeField]
	Material m_gaugeMaterial;
	float m_gaugeValue = 0.4f;

    // Start is called before the first frame update
    void Start()
    {
		m_gaugeValue = PlayerActionControler.ActionValue / 100.0f;
	}

    // Update is called once per frame
    void FixedUpdate()
    {
		if (m_gaugeMaterial == null) return;

		m_gaugeValue = PlayerActionControler.ActionValue / 100.0f;

		m_gaugeMaterial.SetFloat("_GaugeValue", m_gaugeValue);
	}
}