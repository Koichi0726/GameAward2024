using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GameScene;

public class TrackingBullet : MonoBehaviour
{ 
    public float bulletSpeed;  // 弾の速度
    public float trackTimer;   // 追尾の制限時間
    private Vector3 direction; // 弾の方向

    Transform m_playerTrans;   // プレイヤーの位置

    // Start is called before the first frame update
    void Start()
    {
        //プレイヤーのトランスフォーム取得
        CharacterManager characterManager = 
            ManagerContainer.GetManagerContainer().m_characterManager;
        m_playerTrans = characterManager.m_player;
    }

    // Update is called once per frame
    void Update()
    {
        // プレイヤー座標更新
        Vector3 playerPos = m_playerTrans.position;

        // 追尾タイマーを進める
        trackTimer--;

        if(trackTimer >= 0)
        {// プレイヤーへの方向を計算
            direction = (playerPos - transform.position).normalized;
        }

        // 弾に力を加える
        GetComponent<Rigidbody>().velocity = direction * bulletSpeed;
    }
}
