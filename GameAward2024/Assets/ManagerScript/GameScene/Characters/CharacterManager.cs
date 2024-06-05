using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GameScene
{
	public class CharacterManager : MonoBehaviour
	{
		[SerializeField]
		PlayerData m_playerData;
		/// <summary>
		/// プレイヤーのデータを取得
		/// </summary>
		public PlayerData playerData => m_playerData;

		[SerializeField]
		Transform m_playerTrans;
		/// <summary>
		/// プレイヤーのTransformを取得
		/// </summary>
		public Transform playerTrans => m_playerTrans;

		[SerializeField]
		PlayerActionControler m_playerActionController;
		/// <summary>
		/// PlayerActionControllerを取得
		/// </summary>
		public PlayerActionControler playerActionController => m_playerActionController;

		[SerializeField]
		Transform m_enemyTrans;
		/// <summary>
		/// 敵のTransformを取得
		/// </summary>
		public Transform enemyTrans => m_enemyTrans;

		[SerializeField]
		BuffDebuffHandler m_buffDebuffHandler;
		/// <summary>
		/// BuffDebuffHandlerを取得
		/// </summary>
		public BuffDebuffHandler buffDebuffHandler => m_buffDebuffHandler;

		void Start()
		{
			m_playerData.Load();
			m_playerData.GetDatas();
		}

		void Update()
		{
#if DEVELOPMENT_BUILD
			m_playerData.GetDatas();
#endif
		}
	}
}