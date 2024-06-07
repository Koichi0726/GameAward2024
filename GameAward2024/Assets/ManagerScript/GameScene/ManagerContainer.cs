using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GameScene
{
	public class ManagerContainer : ManagerBase
	{
		static ManagerContainer m_managerContainer;
		/// <summary>
		/// ManagerContainerを取得
		/// </summary>
		public static ManagerContainer instance => m_managerContainer;

		[SerializeField]
		GameManager m_gameManager;
		/// <summary>
		/// GameManagerを取得
		/// </summary>
		public GameManager gameManager => m_gameManager;

		[SerializeField]
		StudioObjectManager m_studioObjectManager;
		/// <summary>
		/// StudioObjectManagerを取得
		/// </summary>
		public StudioObjectManager studioObjectManager => m_studioObjectManager;

		[SerializeField]
		CharacterManager m_characterManager;
		/// <summary>
		/// CharacterManagerを取得
		/// </summary>
		public CharacterManager characterManager => m_characterManager;

		[SerializeField]
		BulletManager m_bulletManger;
		/// <summary>
		/// BulletManagerを取得
		/// </summary>
		public BulletManager bulletManger => m_bulletManger;

		[SerializeField]
		BackgroundManager m_backgroundManager;
		/// <summary>
		/// BackgroundManagerを取得
		/// </summary>
		public BackgroundManager backgroundManager => m_backgroundManager;

		[SerializeField]
		UIManager m_uiManager;
		public UIManager uiManager => m_uiManager;

		private void Awake()
		{
			// マネージャーコンテナを取得
			TryGetComponent<ManagerContainer>(out m_managerContainer);
		}

		private void OnDestroy()
		{
			// マネージャーコンテナを破棄
			m_managerContainer = null;
		}
	}
}