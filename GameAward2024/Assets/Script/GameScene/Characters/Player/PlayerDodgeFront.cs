using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerDodgeFront : MonoBehaviour
{
    //オブジェクト取得
    [SerializeField] GameObject player;
    [SerializeField] GameObject boss;

    private Vector3 playerPos;  //プレイヤー座標
    private Vector3 bossPos;    //ボス座標
    private Vector3 distance;   //プレイヤーとボスの距離

    void Start()
    {
        //計算用の変数に格納
        playerPos = player.transform.position;
        bossPos = boss.transform.position;

        //プレイヤーからボスまでの距離を計算
        distance = playerPos - bossPos;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
