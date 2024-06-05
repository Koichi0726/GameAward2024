using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAvoid : MonoBehaviour
{
	PlayerData m_playerData;
    private bool AvoidFlag;
    private Vector2 Period;

    // Start is called before the first frame update
    void Start()
    {
		m_playerData = GameScene.ManagerContainer.instance.characterManager.playerData;
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

        Period *= m_playerData.AVOID_ANCEMULTI_PLIER;

        if (Period.x >= m_playerData.AVOID_RIMIT_VALUE || Period.x <= -m_playerData.AVOID_RIMIT_VALUE)
        {
            Period.x = 0.0f;
        }
        if (Period.y >= m_playerData.AVOID_RIMIT_VALUE || Period.y <= -m_playerData.AVOID_RIMIT_VALUE)
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
                Period.x = -m_playerData.AVOID_START_VALUE;
            }
            else if (dir.x > 0.0f)
            {
                Period.x = m_playerData.AVOID_START_VALUE;
            }
            if (dir.y < 0.0f)
            {
                Period.y = -m_playerData.AVOID_START_VALUE;
            }
            else if (dir.y > 0.0f)
            {
                Period.y = m_playerData.AVOID_START_VALUE;
            }
        }
    }
}
