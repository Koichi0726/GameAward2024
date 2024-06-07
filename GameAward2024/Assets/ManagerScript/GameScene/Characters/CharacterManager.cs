using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GameScene
{
	public class CharacterManager : ManagerBase
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
		EnemyDataList m_enemyDataList;
		/// <summary>
		/// 敵のデータリストを取得
		/// </summary>
		public EnemyDataList enemyDataList => m_enemyDataList;

		EnemyBase m_enemyData;
		/// <summary>
		/// 敵の情報を取得
		/// </summary>
		public EnemyBase enemyData => m_enemyData;

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
			//--- 敵を作成
			EnemyDataList.E_ENEMY_KIND enemyKind = ManagerContainer.instance.gameManager.selectStageData.m_enemyKind;
			m_enemyData = CreateEnemy(enemyKind, new Vector3(0.0f, 0.0f, 0.0f));
			m_enemyTrans = m_enemyData.transform;

			m_playerData.Load();
			m_playerData.GetDatas();
		}

		void Update()
		{
#if DEVELOPMENT_BUILD
			m_playerData.GetDatas();
#endif
		}

		/// <summary>
		/// 敵を作成
		/// </summary>
		/// <param name="enemyPrefab">敵の種類を示す列挙定数</param>
		/// <returns>作成した敵への参照</returns>
		EnemyBase CreateEnemy(EnemyDataList.E_ENEMY_KIND enemyKind, Vector3 pos)
		{
			EnemyBase prefab = m_enemyDataList.GetEnemyPrefab(enemyKind);
			EnemyBase enemy = Instantiate(prefab, pos, Quaternion.identity);    // 敵を作成
			enemy.transform.SetParent(this.transform);     // 親をCharacterManagerに設定
			return enemy;
		}
	}
}