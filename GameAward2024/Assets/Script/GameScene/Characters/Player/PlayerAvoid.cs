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
    private Vector2 Period;

    // Start is called before the first frame update
    void Start()
    {
        AvoidFlag = false;
        Period = new Vector2();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (!AvoidFlag) return;

        PlayerActionControler.AddAction(PlayerData.E_PLAYER_ACTION.E_AVOID);

        if (Period.x != 0.0f) GameScene.ManagerContainer.GetManagerContainer().m_characterManager.m_player.GetComponent<PlayerMove>().PlayerCircularRotation(Period.x, this.transform.up);
        if (Period.y != 0.0f) GameScene.ManagerContainer.GetManagerContainer().m_characterManager.m_player.GetComponent<PlayerMove>().PlayerCircularRotation(Period.y, this.transform.right);

        Period *= 1.3f;

        if (Period.x >= 8.0f || Period.x <= -8.0f)
        {
            Period.x = 0.0f;
        }
        if (Period.y >= 8.0f || Period.y <= -8.0f)
        {
            Period.y = 0.0f;
        }

        if (Period.x == 0.0f && Period.y == 0.0f)
        {
            AvoidFlag = false;
        }

    }

    public void OnAvoid()
    {
        Vector2 dir = PlayerActionControler.PParam.m_moveDirect;

        if (!AvoidFlag)
        {
            AvoidFlag = true;
            if (dir.x < 0.0f)
            {
                Period.x = -0.3f;
            }
            else if (dir.x > 0.0f)
            {
                Period.x = 0.3f;
            }
            if (dir.y < 0.0f)
            {
                Period.y = -0.3f;
            }
            else if (dir.y > 0.0f)
            {
                Period.y = 0.3f;
            }
        }
    }
}
