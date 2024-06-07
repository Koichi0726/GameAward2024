using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GameScene
{
	public class UIManager : ManagerBase
	{
		[SerializeField]
		PlayerGaugeController m_playerGaugeController;
		/// <summary>
		/// PlayerGaugeController‚ðŽæ“¾
		/// </summary>
		public PlayerGaugeController playerGaugeController => m_playerGaugeController;

		[SerializeField]
		TimeGaugeController m_timeGaugeController;
		/// <summary>
		/// TimeGaugeController‚ðŽæ“¾
		/// </summary>
		public TimeGaugeController timeGaugeController => m_timeGaugeController;
	}
}