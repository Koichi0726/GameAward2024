using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GameScene
{
	public class ManagerContainer : MonoBehaviour
	{
		static ManagerContainer m_managerContainer;

		public GameScene.GameManager m_gameManager;
		public GameScene.UIManager m_uiManager;
		public GameScene.StudioObjectManager m_studioObjectManager;
		public GameScene.CharacterManager m_characterManager;
		public GameScene.BackgroundManager m_backgroundManager;

		private void Awake()
		{
			// マネージャーコンテナを取得
			TryGetComponent<ManagerContainer>(out m_managerContainer);
		}

		private void OnDestroy()
		{
			// マネージャーコンテナの参照を破棄
			m_managerContainer = null;
		}

		/// <summary>
		/// マネージャーコンテナの参照を取得
		/// </summary>
		/// <returns>マネージャーコンテナの参照</returns>
		public static ManagerContainer GetManagerContainer()
		{
			return m_managerContainer;
		}
	}
}