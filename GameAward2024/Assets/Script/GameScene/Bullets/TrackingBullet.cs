using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GameScene;

public class TrackingBullet : BulletBase
{ 
    public float m_bulletSpeed;       // 弾の速度
    public float m_trackTimer;        // 追尾の制限時間
    private Vector3 m_direction;      // 弾の方向
    private Transform m_playerTrans;  // プレイヤーの位置
    private Rigidbody m_rigidbody;

    // Start is called before the first frame update
    protected override void Start()
    {
        base.Start();

        //プレイヤーのトランスフォーム取得
        CharacterManager characterManager = 
            ManagerContainer.GetManagerContainer().m_characterManager;
        m_playerTrans = characterManager.m_player;

        m_rigidbody = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
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
}
