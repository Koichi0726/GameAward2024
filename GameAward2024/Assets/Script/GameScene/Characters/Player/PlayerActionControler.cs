using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerParamCoefficient // プレイヤーに関わる係数
{
    public float m_addGaugeValue = 0.0f;// ゲージに加算する変数
    public float m_subGaugeValue = 0.0f;// ゲージに減算する変数
    public float m_gaugeUpSpeed = 1.0f;// ゲージの増加スピード
    public float m_gaugeDownSpeed = 1.0f;// ゲージの減少スピード
    public float m_moveDirect = 1.0f;// 移動方向の係数
    public float m_moveSpeed = 1.0f;// 移動速度の係数

    public PlayerParamCoefficient()
    {
        m_addGaugeValue = 0.0f;// ゲージに加算する変数
        m_subGaugeValue = 0.0f;// ゲージに減算する変数
        m_gaugeUpSpeed = 1.0f;// ゲージの増加スピード
        m_gaugeDownSpeed = 1.0f;// ゲージの減少スピード
        m_moveDirect = 1.0f;// 移動方向の係数
        m_moveSpeed = 1.0f;// 移動速度の係数
    }
}

public class PlayerActionControler : MonoBehaviour
{
    // プレイヤーデータの取得用変数
    private PlayerData PData;

    // ゲージの増減を管理する変数
    static private PlayerParamCoefficient m_PParamCoefficient;

    // プレイヤーがなんの動作をしたかを保存するリスト
    static private List<PlayerData.E_PLAYER_ACTION> m_actionList;

    // プレイヤーゲージに反映させる数値を保持する
    [SerializeField] private static float m_actionValue;        //0-100で管理する

    // Start is called before the first frame update
    void Start()
    {
        //各種変数初期化
        PData = PlayerDataParam.data;
        m_PParamCoefficient = new PlayerParamCoefficient();
        m_actionList = new List<PlayerData.E_PLAYER_ACTION>();     //リストの生成
        m_actionValue = PData.START_GAUGE_VALUE;     //ゲージの数値初期化
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (m_actionList.Count == 0)
        {//フレーム間でアクションを起こしていない ＝ 停止している
            m_PParamCoefficient.m_subGaugeValue += PData.STOP_GAUGE_VALUE;        //停止している時
        }
        else
        {//フレーム間でアクションを起こしている
            while (m_actionList.Count != 0)
            {
                //登録されているアクションに応じる数値を追加
                switch (m_actionList[0])
                {
                    case PlayerData.E_PLAYER_ACTION.E_MOVE:    //移動している時
                        m_PParamCoefficient.m_addGaugeValue += PData.MOVE_GAUGE_VALUE;
                        break;
                    case PlayerData.E_PLAYER_ACTION.E_DASH:    //ダッシュしている時
                        m_PParamCoefficient.m_addGaugeValue += PData.DASH_GAUGE_VALUE;
                        break;
                    case PlayerData.E_PLAYER_ACTION.E_AVOID:   //回避している時
                        m_PParamCoefficient.m_addGaugeValue += PData.AVOID_GAUGE_VALUE;
                        break;
                    default:
                        break;
                }
                //処理済みのアクションを削除
                m_actionList.RemoveAt(0);
            }
        }

        //ゲージの変化量の反映
        m_actionValue += m_PParamCoefficient.m_addGaugeValue * m_PParamCoefficient.m_gaugeUpSpeed;
        m_actionValue -= m_PParamCoefficient.m_subGaugeValue * m_PParamCoefficient.m_gaugeDownSpeed;
        
        //数値を超えたり下回った時の補正処理
        if (m_actionValue < 0.0f)
        {
            m_actionValue = 0.0f;
        }
        else if(m_actionValue > 100.0f)
        {
            m_actionValue = 100.0f;
        }

        //数値を初期化
        m_PParamCoefficient = new PlayerParamCoefficient();
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

    /// <summary>
    /// ゲージ操作変数取得用のプロパティ
    /// </summary>
    public static PlayerParamCoefficient PParam
    {
        get { return m_PParamCoefficient; }  //取得用 
    }
}

/*----------------------------------------------------------------

〇ゲージの動きを速くさせる時
　PlayerActionControler.PParam.m_gaugeUpSpeed = [速くさせる倍率];

〇ゲージの動きを遅くさせる時
　PlayerActionControler.PParam.m_gaugeDownSpeed = [遅くさせる倍率];

※掛け合わせるためマイナスの数値を入れない

  ----------------------------------------------------------------*/
