using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAvoid : MonoBehaviour
{
    const float AVOID_ANCEMULTI_PLIER = 1.3f;       //回避の速度の減少率（大きいほど速く止まる）
    const float AVOID_START_VALUE = 0.3f;       //回避の初期値（大きいほど初速が遅い）
    const float AVOID_RIMIT_VALUE = 8.0f;       //回避の停止する値（大きいほど停止までの動きが伸びる）

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

        if (Period.x != 0.0f) GameScene.ManagerContainer.instance.characterManager.playerTrans.GetComponent<PlayerMove>().PlayerCircularRotation(Period.x, this.transform.up);
        if (Period.y != 0.0f) GameScene.ManagerContainer.instance.characterManager.playerTrans.GetComponent<PlayerMove>().PlayerCircularRotation(Period.y, this.transform.right);

        Period *= AVOID_ANCEMULTI_PLIER;

        if (Period.x >= AVOID_RIMIT_VALUE || Period.x <= -AVOID_RIMIT_VALUE)
        {
            Period.x = 0.0f;
        }
        if (Period.y >= AVOID_RIMIT_VALUE || Period.y <= -AVOID_RIMIT_VALUE)
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
                Period.x = -AVOID_START_VALUE;
            }
            else if (dir.x > 0.0f)
            {
                Period.x = AVOID_START_VALUE;
            }
            if (dir.y < 0.0f)
            {
                Period.y = -AVOID_START_VALUE;
            }
            else if (dir.y > 0.0f)
            {
                Period.y = AVOID_START_VALUE;
            }
        }
    }
}
