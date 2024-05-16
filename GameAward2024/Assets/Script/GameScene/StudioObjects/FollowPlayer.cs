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

	// Start is called before the first frame update
	void Start()
	{
		//--- プレイヤー・敵の情報を取得
		CharacterManager characterManager = ManagerContainer.GetManagerContainer().m_characterManager;
		m_playerTrans = characterManager.m_player;
		m_enemyTrans = characterManager.m_enemy;
	}

	// Update is called once per frame
	void Update()
	{
		//--- プレイヤー・敵の座標
		Vector3 playerPos = m_playerTrans.position;
		Vector3 enemyPos  = m_enemyTrans.position;

		// 角度を計算
		float angle = CalcToEnemyAngleY(enemyPos - playerPos);

		//--- オフセットに回転を適用
		Quaternion quaternion = Quaternion.Euler(0.0f, angle, 0.0f);
		Vector3 newOffset = quaternion * m_offset;

		//--- 座標・回転を指定
		transform.position = playerPos + newOffset;
		transform.rotation = Quaternion.Euler(0.0f, angle, 0.0f);
	}

	/// <summary>
	/// 敵へ向かうベクトルのY軸の角度を求める
	/// </summary>
	/// <param name="vToEnemy">プレイヤーから敵へ向かうベクトル</param>
	/// <returns>敵へ向かうベクトルのY軸の角度</returns>
	float CalcToEnemyAngleY(Vector3 vToEnemy)
	{
		vToEnemy.y = 0.0f;		// yの移動成分を無視
		vToEnemy.Normalize();	// ベクトルを正規化

		//--- 敵へのベクトルとZ方向ベクトルの成す角を求める
		float dot = Vector3.Dot(Vector3.forward, vToEnemy);
		float angle = Mathf.Acos(dot);
		angle = angle * (HALF_DEGREE / Mathf.PI);

		//--- 左右判定によって-180.0～180.0に変換
		Vector3 cross = Vector3.Cross(Vector3.forward, vToEnemy);
		// Z方向ベクトルから見て、左側に位置する場合は[* -1.0]
		if (cross.y < 0.0f) angle = -angle;

		return angle;
	}
}