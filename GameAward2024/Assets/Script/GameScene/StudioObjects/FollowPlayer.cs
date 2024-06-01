using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GameScene;

public class FollowPlayer : MonoBehaviour
{
	const float HALF_DEGREE = 180.0f;

	[SerializeField]
	Vector3 m_offset = new Vector3(0.0f, 0.5f, -2.5f);
	Transform m_playerTrans;
	Transform m_enemyTrans;
	Quaternion m_initialQuaternion;

	void Start()
	{
		// 初期情報を取得
		m_initialQuaternion = this.transform.rotation;

		//--- プレイヤー・敵の情報を取得
		CharacterManager characterManager = ManagerContainer.GetManagerContainer().m_characterManager;
		m_playerTrans = characterManager.m_player;
		m_enemyTrans = characterManager.m_enemy;
	}

	void Update()
	{
		//--- プレイヤー・敵の座標
		Vector3 playerPos = m_playerTrans.position;
		Vector3 enemyPos  = m_enemyTrans.position;

		//--- オフセットに回転を適用
		Quaternion quaternion = m_playerTrans.rotation;
		Vector3 newOffset = quaternion * m_offset;

		//--- 座標・回転を指定
		transform.position = playerPos + newOffset;
		// TODO:躍動感を出す場合は、下の行を採用
		transform.rotation = quaternion * m_initialQuaternion;
		//transform.rotation = m_initialQuaternion * quaternion;
	}
}