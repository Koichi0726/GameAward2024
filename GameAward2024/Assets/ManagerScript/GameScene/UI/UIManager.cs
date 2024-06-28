using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GameScene
{
	public class UIManager : ManagerBase
	{
		[SerializeField]
		PlayerHpGaugeController m_playerGaugeController;
		/// <summary>
		/// PlayerGaugeController‚ðŽæ“¾
		/// </summary>
		public PlayerHpGaugeController playerGaugeController => m_playerGaugeController;

		[SerializeField]
		EnemyHpGaugeController m_enemyGaugeController;
		/// <summary>
		/// EnemyHpGaugeController‚ðŽæ“¾
		/// </summary>
		public EnemyHpGaugeController enemyGaugeController => m_enemyGaugeController;

		[SerializeField]
		TimeGaugeController m_timeGaugeController;
		/// <summary>
		/// TimeGaugeController‚ðŽæ“¾
		/// </summary>
		public TimeGaugeController timeGaugeController => m_timeGaugeController;
	}
}