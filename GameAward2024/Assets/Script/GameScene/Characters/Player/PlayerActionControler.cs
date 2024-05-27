using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerActionControler : MonoBehaviour
{
    // プレイヤーデータの取得用変数
    private PlayerData PData;

    //プレイヤーがなんの動作をしたかを保存するリスト
    static List<PlayerData.E_PLAYER_ACTION> m_actionList;

    //プレイヤーゲージに反映させる数値を保持する
    [SerializeField] private static float m_actionValue;        //0-100で管理する

    // Start is called before the first frame update
    void Start()
    {
        //各種変数初期化
        PData = PlayerDataParam.data;
        m_actionList = new List<PlayerData.E_PLAYER_ACTION>();     //リストの生成
        m_actionValue = PData.START_GAUGE_VALUE;     //ゲージの数値初期化
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (m_actionList.Count == 0)
        {//フレーム間でアクションを起こしていない ＝ 停止している
            m_actionValue += PData.STOP_GAUGE_VALUE;        //停止している時
        }
        else
        {//フレーム間でアクションを起こしている
            while(m_actionList.Count != 0)
            {
                //登録されているアクションに応じる数値を追加
                switch (m_actionList[0])
                {
                    case PlayerData.E_PLAYER_ACTION.E_MOVE:    //移動している時
                        m_actionValue += PData.MOVE_GAUGE_VALUE;
                        break;
                    case PlayerData.E_PLAYER_ACTION.E_DASH:    //ダッシュしている時
                        m_actionValue += PData.DASH_GAUGE_VALUE;
                        break;
                    case PlayerData.E_PLAYER_ACTION.E_AVOID:   //回避している時
                        m_actionValue += PData.AVOID_GAUGE_VALUE;
                        break;
                    default:
                        break;
                }
                //処理済みのアクションを削除
                m_actionList.RemoveAt(0);
            }
        }

        //数値を超えたり下回った時の補正処理
        if(m_actionValue < 0.0f)
        {
            m_actionValue = 0.0f;
        }
        else if(m_actionValue > 100.0f)
        {
            m_actionValue = 100.0f;
        }
    }

    /// <summary>
    /// プレイヤーの起こしたアクションを登録する関数
    /// </summary>
    /// <param name="act"> 起こしたアクションの種類 </param>
    public static void AddAction(PlayerData.E_PLAYER_ACTION act)
    {
        m_actionList.Add(act);
    }

    /// <summary>
    /// 数値取得用のプロパティ
    /// </summary>
    public static float ActionValue
    {
        get { return m_actionValue; }  //取得用
    }
}
