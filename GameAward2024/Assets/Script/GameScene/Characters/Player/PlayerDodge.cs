using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerDodge : MonoBehaviour
{
    //変数宣言
    private float rotXZ = 3.0f;  //プレイヤー回転角度XZ
    private float rotY = 3.0f;   //プレイヤー回転角度Y
    private float lateRotXZ;
    private float lateRotY;
    private float radius = -5.0f;
     
    private Vector3 playerPos;  //プレイヤー座標
    private Vector3 bossPos;    //ボス座標
    private Vector3 distance;   //プレイヤーとボスの距離
    private Vector3 dodgeDistance;  //プレイヤーの回避距離計算用

    //public float moveDodgeSpeed = 3.0f;

    void Start()
    {
        //マネージャークラスから取得した座標を計算用の変数に格納
        playerPos = GameScene.ManagerContainer.GetManagerContainer().m_characterManager.m_player.position;
        bossPos = GameScene.ManagerContainer.GetManagerContainer().m_characterManager.m_enemy.position;

        //プレイヤーからボスまでの距離を計算
        distance = playerPos - bossPos;

        lateRotXZ = (rotXZ - lateRotXZ) * 0.1f + lateRotXZ;
        lateRotY  = (rotY - lateRotY)   * 0.1f + lateRotY;
    }

    void Update()
    {       
        
      
    }

    public void DodgeRight()
    {
        //プレイヤーの座標を更新
        playerPos.x = Mathf.Cos(rotY) * Mathf.Sin(rotXZ) * radius + playerPos.x;
        playerPos.y = Mathf.Sin(lateRotY) * radius + playerPos.y;
        playerPos.z = Mathf.Cos(lateRotY) * Mathf.Cos(lateRotXZ) * radius + playerPos.z;

        if(Keyboard.current.lKey.isPressed && Keyboard.current.dKey.isPressed)
        {
            Debug.Log("lllllllllldddddddddddd");
        }
        else if(Keyboard.current.lKey.isPressed)
        {
            Debug.Log("lllllllllllll");
        }
        else
        {
            Debug.Log("ddddddddddddddd");
        }
    }

    public void DodgeLeft()
    {

    }

    public void DodgeFront()
    {

    }

    public void DodgeBack()
    {

    }
}
