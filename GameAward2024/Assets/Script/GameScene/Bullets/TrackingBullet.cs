using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GameScene;

public class TrackingBullet : BulletBase
{ 
    float m_bulletSpeed;		// 弾の速度
    float m_trackTimer;			// 追尾の制限時間
    Vector3 m_direction;		// 弾の方向
    Transform m_playerTrans;	// プレイヤーの位置
    Rigidbody m_rigidbody;

    protected override void Start()
    {
		m_bulletKind = BulletDataList.E_BULLET_KIND.TRACKING;

        base.Start();

        //プレイヤーのトランスフォーム取得
        CharacterManager characterManager = 
            ManagerContainer.instance.characterManager;
        m_playerTrans = characterManager.playerTrans;

        m_rigidbody = GetComponent<Rigidbody>();
    }
	
    protected override void Update()
    {
        base.Update();

        // プレイヤー座標更新
        Vector3 playerPos = m_playerTrans.position;

        // 追尾タイマーを進める
        m_trackTimer -= Time.deltaTime;

        if(m_trackTimer >= 0)
        {// プレイヤーへの方向を計算
            m_direction = (playerPos - transform.position).normalized;
        }

        // 弾に力を加える
        m_rigidbody.velocity = m_direction * m_bulletSpeed;
    }

	protected override void SetData(Dictionary<string, CSVParamData> data)
	{
		base.SetData(data);

		//--- 値の吸出し
		data[nameof(m_bulletSpeed	)].TryGetData(out m_bulletSpeed);
		data[nameof(m_trackTimer	)].TryGetData(out m_trackTimer);
	}
}