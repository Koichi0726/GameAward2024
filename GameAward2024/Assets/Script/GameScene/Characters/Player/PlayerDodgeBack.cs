using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerDodgeBack : MonoBehaviour
{
    //変数
    private Vector3 playerPos;  //プレイヤー座標
    private Vector3 bossPos;    //ボス座標
    private Vector3 distance;   //プレイヤーとボスの距離
    void Start()
    {
        //マネージャークラスから取得した座標を計算用の変数に格納
        playerPos = GameScene.ManagerContainer.GetManagerContainer().m_characterManager.m_player.position;
        bossPos = GameScene.ManagerContainer.GetManagerContainer().m_characterManager.m_enemy.position;

        //プレイヤーからボスまでの距離を計算
        distance = playerPos - bossPos;
    }

    // Update is called once per frame
    void Update()
    {
        //後方に回避する場合のみ、プレイヤーとボスの距離を図る
        //それを正規化し、マイナスを掛ける、これにムーブスピードを掛けることで、後方への移動ができる
    }
}
