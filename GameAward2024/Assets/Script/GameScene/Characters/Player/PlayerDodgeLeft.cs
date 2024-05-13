using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerDodgeLeft : MonoBehaviour
{
    //オブジェクト取得
    [SerializeField] GameObject player;
    [SerializeField] GameObject boss;

    //変数宣言
    private float rotY = -5.0f;  //プレイヤー回転角度

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


    void Update()
    {
        //プレイヤーの回避後の座標を計算
        playerPos.x += distance.x * Mathf.Cos(rotY) - distance.z * Mathf.Sin(rotY);
        playerPos.x += distance.x * Mathf.Sin(rotY) - distance.z * Mathf.Cos(rotY);
    }
}
