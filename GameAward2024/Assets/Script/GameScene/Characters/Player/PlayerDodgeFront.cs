using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerDodgeFront : MonoBehaviour
{
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
        
    }
}
