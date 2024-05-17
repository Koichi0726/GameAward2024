using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GameScene
{
	public class UIManager : MonoBehaviour
	{
		[field: SerializeField]
		public PlayerGaugeController m_playerGaugeController { get; private set; }
		[field: SerializeField]
		public TimeGaugeController m_timeGaugeController { get; private set; }
	}
}