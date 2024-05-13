using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerGaugeController : MonoBehaviour
{
	[SerializeField]
	Material m_gaugeMaterial;
	float m_gaugeValue = 1.0f;
	// Player m_player

    // Start is called before the first frame update
    void Start()
    {
		// m_player = ManagerContainer.CharacterManager.m_player;
		//GameScene.ManagerContainer.GetManagerContainer().m_gameManager;
    }

    // Update is called once per frame
    void Update()
    {
		// if(m_player == null) return;
		if (m_gaugeMaterial == null) return;

		// m_gaugeValue = m_player.m_overheatValue / PlayerSetting.MAX_OVERHEAT_VALUE;

		// TODO:マジックナンバーはScriptableObjectに置き換える
		m_gaugeMaterial.SetFloat("_GaugeValue", m_gaugeValue);
	}
}