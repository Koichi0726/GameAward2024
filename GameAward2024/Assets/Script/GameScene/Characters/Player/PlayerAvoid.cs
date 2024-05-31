using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAvoid : MonoBehaviour
{
    //変数宣言
    private float rotXZ = 3.0f;  //プレイヤー回転角度XZ
    private float rotY = 0.0f;   //プレイヤー回転角度Y
    private float lateRotXZ;
    private float lateRotY;
    private float radius = -5.0f;

    private Vector3 playerPos;  //プレイヤー座標
    private Vector3 bossPos;    //ボス座標
    private Vector3 distance;   //プレイヤーとボスの距離
    private Vector3 dodgeDistance;  //プレイヤーの回避距離計算用

    private bool AvoidFlag;
    private float Period;

    // Start is called before the first frame update
    void Start()
    {
        ////マネージャークラスから取得した座標を計算用の変数に格納
        //playerPos = GameScene.ManagerContainer.GetManagerContainer().m_characterManager.m_player.position;
        //bossPos = GameScene.ManagerContainer.GetManagerContainer().m_characterManager.m_enemy.position;

        ////プレイヤーからボスまでの距離を計算
        //distance = playerPos - bossPos;

        //lateRotXZ = (rotXZ - lateRotXZ) * 0.1f + lateRotXZ;
        //lateRotY = (rotY - lateRotY) * 0.1f + lateRotY;

        AvoidFlag = false;
        Period = 0.0f;
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            Debug.Log("押した");
            if (!AvoidFlag)
            {
                AvoidFlag = true;
                Period = 0.3f;
            }
        }
    }

    // Update is called once per frame
    void FixedUpdate()
    {


        if (!AvoidFlag) return;

        GameScene.ManagerContainer.GetManagerContainer().m_characterManager.m_player.GetComponent<PlayerMove>().PlayerCircularRotation(Period, Vector3.up);

        Period *= 1.3f;

        if(Period >= 8.0f)
        {
            Period = 0.0f;
            AvoidFlag = false;
        }

    }

    public void OnAvoid()
    {

    }
}
