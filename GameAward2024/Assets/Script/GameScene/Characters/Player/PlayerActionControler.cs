using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerActionControler : MonoBehaviour
{
    public enum E_PLAYER_ACTION
    {
        E_MOVE = 0,
        E_DASH,
        E_AVOID,
        E_MAX,
    }

    const float DONTMOVE_VALUE = -0.3f;
    const float MOVE_VALUE = 0.1f;
    const float DASH_VALUE = 0.2f;
    const float AVOID_VALUE = 0.5f;

    //プレイヤーがなんの動作をしたかを保存するリスト
    static List<E_PLAYER_ACTION> m_actionList;

    //プレイヤーゲージに反映させる数値を保持する
    [SerializeField] private static float m_actionValue;        //0-100で管理する

    // Start is called before the first frame update
    void Start()
    {
        m_actionList = new List<E_PLAYER_ACTION>();
        m_actionValue = 60;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (m_actionList.Count == 0)
        {
            m_actionValue += DONTMOVE_VALUE;
        }
        else
        {
            while(m_actionList.Count != 0)
            {
                switch (m_actionList[0])
                {
                    case E_PLAYER_ACTION.E_MOVE:
                        m_actionValue += MOVE_VALUE;
                        break;
                    case E_PLAYER_ACTION.E_DASH:
                        m_actionValue += DASH_VALUE;
                        break;
                    case E_PLAYER_ACTION.E_AVOID:
                        m_actionValue += AVOID_VALUE;
                        break;
                    default:
                        break;
                }
                m_actionList.RemoveAt(0);
            }
        }

        if(m_actionValue < 0.0f)
        {
            m_actionValue = 0.0f;
        }
        else if(m_actionValue > 100.0f)
        {
            m_actionValue = 100.0f;
        }
    }

    public static void AddAction(E_PLAYER_ACTION act)
    {
        m_actionList.Add(act);
    }

    public static float ActionValue
    {
        get { return m_actionValue; }  //取得用
    }
}
