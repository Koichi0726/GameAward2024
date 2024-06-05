using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GameScene;

//--- 参考サイト
// https://qiita.com/No2DGameNoLife/items/d1497a2f98f95a5194ac
[DefaultExecutionOrder(1)]  // HACK:プレイヤーの更新より後に実行する為
public class FollowPlayer : MonoBehaviour
{
	[SerializeField]
	Vector3 m_offset = new Vector3(0.0f, 0.5f, -2.5f);
	Transform m_playerTrans;
	Quaternion m_initialQuaternion;

	void Start()
	{
		// 初期情報を取得
		m_initialQuaternion = this.transform.rotation;

		//--- プレイヤーの情報を取得
		CharacterManager characterManager = ManagerContainer.instance.characterManager;
		m_playerTrans = characterManager.playerTrans;
	}

	void Update()
	{
		//--- オフセットに回転を適用
		Quaternion quaternion = m_playerTrans.rotation;
		Vector3 newOffset = quaternion * m_offset;

		//--- 座標・回転を指定
		Vector3 playerPos = m_playerTrans.position;
		transform.position = playerPos + newOffset;
		// TODO:躍動感を出す場合は、下の行を採用
		transform.rotation = quaternion * m_initialQuaternion;
		//transform.rotation = m_initialQuaternion * quaternion;
	}
}