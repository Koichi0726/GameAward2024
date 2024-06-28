using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GameScene;

public class BulletBase : MonoBehaviour
{
	public enum E_TARGET_KIND
	{
		PLAYER = 0,
		ENEMY
	}

	const string TO_PLAYER_BULLET_LAYER_NAME = "ToPlayerBullet";
	const string TO_ENEMY_BULLET_LAYER_NAME  = "ToEnemyBullet";
	const string COUNTER_AREA_LAYER_NAME	 = "CounterArea";

	protected BulletDataList.E_BULLET_KIND m_bulletKind;
	protected Transform m_target;
	BulletDataList m_bulletDataList;
	float m_damage;
	float m_destroyTime;
	float m_maxDestroyTime;
    protected BuffDebuffData m_buffDebuffData = new BuffDebuffData();

	Action m_bulletUpdate;
	Action m_bulletFixedUpdate;
	Action<Collider> m_onTriggerEnter;

    protected virtual void Start()
    {
		//--- 弾データの読み込みと設定
		m_bulletDataList = ManagerContainer.instance.bulletManger.bulletDataList;
		m_bulletDataList.Load(m_bulletKind);
		var data = m_bulletDataList.GetData(m_bulletKind);
		SetData(data);
		m_maxDestroyTime = m_destroyTime;

		// ターゲットをプレイヤーに設定
		ChangeTarget(E_TARGET_KIND.PLAYER);
	}

	protected virtual void Update()
    {
		//--- 消滅するまでの時間をカウント
		if (m_destroyTime < 0.0f)
		{
			Destroy(gameObject);  // 自身を破壊
			return;
		}
		m_destroyTime -= Time.deltaTime;

		// 弾の動き
		m_bulletUpdate.Invoke();
    }

	protected virtual void FixedUpdate()
	{
		m_bulletFixedUpdate.Invoke();
	}

	void OnTriggerEnter(Collider other)
	{
		// カウンターの領域に接触した場合は処理しない
		if (other.gameObject.layer == LayerMask.NameToLayer(COUNTER_AREA_LAYER_NAME)) return;

		m_onTriggerEnter.Invoke(other);

		// プレイヤーor敵に当たったら弾を削除する
		Destroy(gameObject);
	}

	public void ChangeTarget(E_TARGET_KIND targetKind)
	{
		// 消滅までの時間をリセット
		m_destroyTime = m_maxDestroyTime;

		CharacterManager characterManager = ManagerContainer.instance.characterManager;

		switch(targetKind)
		{
			case E_TARGET_KIND.PLAYER:
				//--- 関数を差し替え
				m_bulletUpdate = UpdateToPlayer;
				m_bulletFixedUpdate = FixedUpdateToPlayer;
				m_onTriggerEnter = OnTriggerEnterToPlayer;

				//--- レイヤーを変更
				gameObject.layer = 
					LayerMask.NameToLayer(TO_PLAYER_BULLET_LAYER_NAME);

				// ターゲットをプレイヤーに設定
				m_target = characterManager.playerTrans;

				break;
			case E_TARGET_KIND.ENEMY:
				//--- 関数を差し替え
				m_bulletUpdate = UpdateToEnemy;
				m_bulletFixedUpdate = FixedUpdateToEnemy;
				m_onTriggerEnter = OnTriggerEnterToEnemy;

				//--- レイヤーを変更
				gameObject.layer = 
					LayerMask.NameToLayer(TO_ENEMY_BULLET_LAYER_NAME);

				// ターゲットを敵に設定
				m_target = characterManager.enemyTrans;
				break;
		}

		// ターゲット変更時に呼び出される処理
		OnChangeTarget(targetKind);
	}

	/// <summary>
	/// ターゲットが変更された時に呼び出される処理
	/// </summary>
	protected virtual void OnChangeTarget(E_TARGET_KIND targetKind)
	{
	}

	/// <summary>
	/// プレイヤーへ向かう時の処理
	/// </summary>
	protected virtual void UpdateToPlayer()
	{	
	}

	/// <summary>
	/// 敵へ向かう時の処理
	/// </summary>
	protected virtual void UpdateToEnemy()
	{
	}

	/// <summary>
	/// プレイヤーへ向かう時の処理
	/// </summary>
	protected virtual void FixedUpdateToPlayer()
	{
	}

	/// <summary>
	/// 敵へ向かう時の処理
	/// </summary>
	protected virtual void FixedUpdateToEnemy()
	{
	}

	/// <summary>
	/// プレイヤーへ衝突した時の処理
	/// </summary>
	protected virtual void OnTriggerEnterToPlayer(Collider other)
	{
		CharacterManager characterManager = ManagerContainer.instance.characterManager;

		// プレイヤーにバフを付与する
		characterManager.
			buffDebuffHandler.AddBuffDebuff(m_buffDebuffData, this.GetType().Name);

		//--- プレイヤーへダメージを与える
		PlayerControler playerControler;
		if (other.TryGetComponent(out playerControler))
			playerControler.SubHp(m_damage);
	}

	/// <summary>
	/// 敵に衝突した時の処理
	/// </summary>
	protected virtual void OnTriggerEnterToEnemy(Collider other)
	{
		// 敵にダメージを与える
		EnemyBase enemy;
		if (other.TryGetComponent(out enemy))
			enemy.SubHp(m_damage);
	}

	protected virtual void SetData(Dictionary<string, CSVParamData> data)
	{
		//--- 値の吸出し
		data[nameof(m_damage		)].TryGetData(out m_damage);
		data[nameof(m_destroyTime	)].TryGetData(out m_destroyTime);
		data[nameof(m_buffDebuffData.m_paramCoefficient.m_moveDirect	)].TryGetData(out m_buffDebuffData.m_paramCoefficient.m_moveDirect);
		data[nameof(m_buffDebuffData.m_paramCoefficient.m_moveSpeed		)].TryGetData(out m_buffDebuffData.m_paramCoefficient.m_moveSpeed);
		data[nameof(m_buffDebuffData.m_remainingDuration)].TryGetData(out m_buffDebuffData.m_remainingDuration);
	}
}