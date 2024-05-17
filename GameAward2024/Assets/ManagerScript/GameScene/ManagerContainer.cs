using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GameScene
{
	public class ManagerContainer : MonoBehaviour
	{
		static ManagerContainer m_managerContainer;

		[field: SerializeField]
		public GameScene.GameManager m_gameManager { get; private set; }
		[field: SerializeField]
		public GameScene.StudioObjectManager m_studioObjectManager { get; private set; }
		[field: SerializeField]
		public GameScene.CharacterManager m_characterManager { get; private set; }
		[field: SerializeField]
		public BulletManager m_bulletManger { get; private set; }
		[field: SerializeField]
		public GameScene.BackgroundManager m_backgroundManager { get; private set; }
		[field: SerializeField]
		public GameScene.UIManager m_uiManager { get; private set; }

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