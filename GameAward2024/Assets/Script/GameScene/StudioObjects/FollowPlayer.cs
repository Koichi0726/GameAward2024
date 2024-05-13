using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GameScene;

public class FollowPlayer : MonoBehaviour
{
	Transform playerTrans;
	Matrix4x4 worldMat;

	// Start is called before the first frame update
	void Start()
	{
		//--- プレイヤーの情報を取得
		CharacterManager characterManager = ManagerContainer.GetManagerContainer().m_characterManager;
		playerTrans = characterManager.m_player;
		
		// オブジェクトのワールド行列を取得
		worldMat = this.transform.localToWorldMatrix;
	}

	// Update is called once per frame
	void Update()
	{
		// プレイヤーのワールド行列
		Matrix4x4 playerWorldMat = playerTrans.localToWorldMatrix;
		// プレイヤーの行列に自身の行列を適用
		Matrix4x4 newWorldMat = playerWorldMat * worldMat;

		//--- 移動成分を取り出す
		Vector3 pos = new Vector3(newWorldMat.m03, newWorldMat.m13, newWorldMat.m23);
		this.transform.position = pos;

		//--- 回転成分を取り出す
		Quaternion rot = newWorldMat.rotation;
		this.transform.rotation = rot;
	}
}