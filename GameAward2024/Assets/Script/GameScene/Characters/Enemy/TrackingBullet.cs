using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GameScene;

public class TrackingBullet : MonoBehaviour
{ 
    public float m_bulletSpeed;       // 弾の速度
    public float m_trackTimer;        // 追尾の制限時間
    private Vector3 m_direction;      // 弾の方向
    private GameObject m_playerObj;   // プレイヤーのゲームオブジェクト
    private Transform m_playerTrans;  // プレイヤーの位置

    // Start is called before the first frame update
    void Start()
    {
        // プレイヤーのゲームオブジェクトを取得
        m_playerObj = GameObject.Find("Player");

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
        m_trackTimer--;

        if(m_trackTimer >= 0)
        {// プレイヤーへの方向を計算
            m_direction = (playerPos - transform.position).normalized;
        }

        // 弾に力を加える
        GetComponent<Rigidbody>().velocity = m_direction * m_bulletSpeed;
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject == m_playerObj)
        {
            // プレイヤーに当たったら弾を削除する
            Destroy(gameObject);
        }
    }
}
