using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GameScene;

public class PlayerActionControler : MonoBehaviour
{
	public const float MAX_GAUGE_VALUE = 100.0f;

    // プレイヤーデータの取得用変数
    private PlayerData m_playerData;

	// ゲージの増減を管理する変数
	PlayerParamCoefficient m_paramCoefficient;

	// プレイヤーがなんの動作をしたかを保存するリスト
	List<PlayerData.E_PLAYER_ACTION> m_actionList = new List<PlayerData.E_PLAYER_ACTION>();

	// プレイヤーゲージに反映させる数値を保持する
	public float m_actionValue { get; private set; }	// 0-100で管理する

    void Start()
    {
		//各種変数初期化
		CharacterManager characterManager = ManagerContainer.instance.characterManager;
        m_playerData = characterManager.playerData;
		m_paramCoefficient = characterManager.buffDebuffHandler.m_paramCoefficient;

		m_actionValue = m_playerData.START_GAUGE_VALUE;     //ゲージの数値初期化
    }

    void FixedUpdate()
    {
        if (m_actionList.Count == 0)
        {//フレーム間でアクションを起こしていない ＝ 停止している
            m_paramCoefficient.m_subGaugeValue += m_playerData.STOP_GAUGE_VALUE;        //停止している時
        }
        else
        {//フレーム間でアクションを起こしている
            while (m_actionList.Count != 0)
            {
                //登録されているアクションに応じる数値を追加
                switch (m_actionList[0])
                {
                    case PlayerData.E_PLAYER_ACTION.E_MOVE:    //移動している時
                        m_paramCoefficient.m_addGaugeValue += m_playerData.MOVE_GAUGE_VALUE;
                        break;
                    case PlayerData.E_PLAYER_ACTION.E_DASH:    //ダッシュしている時
                        m_paramCoefficient.m_addGaugeValue += m_playerData.DASH_GAUGE_VALUE;
                        break;
                    case PlayerData.E_PLAYER_ACTION.E_AVOID:   //回避している時
                        m_paramCoefficient.m_addGaugeValue += m_playerData.AVOID_GAUGE_VALUE;
                        break;
                    default:
                        break;
                }
                //処理済みのアクションを削除
                m_actionList.RemoveAt(0);
            }
        }

        // ゲージの変化量の反映
        m_actionValue += m_paramCoefficient.m_addGaugeValue * m_paramCoefficient.m_gaugeUpSpeed;
        m_actionValue -= m_paramCoefficient.m_subGaugeValue * m_paramCoefficient.m_gaugeDownSpeed;

		// 数値を超えたり下回った時の補正処理
		m_actionValue = Mathf.Clamp(m_actionValue, 0.0f, MAX_GAUGE_VALUE);
    }

    /// <summary>
    /// プレイヤーの起こしたアクションを登録する関数
    /// </summary>
    /// <param name="act"> 起こしたアクションの種類 </param>
    public void AddAction(PlayerData.E_PLAYER_ACTION act)
    {
        m_actionList.Add(act);
    }
}