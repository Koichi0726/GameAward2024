using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GameScene;

public class BulletBase : MonoBehaviour
{
	[SerializeField]
	float m_destroyTime;
	float m_deltaTime = 0.0f;
    GameObject m_playerObj;
    [SerializeField]
    protected BuffDebuffData m_buffDebuffData;

    protected void start()
    {
        // プレイヤーのゲームオブジェクトを取得
        m_playerObj = GameObject.Find("Player");
    }

    protected void Update()
    {
		//--- 消滅するまでの時間をカウント
		if (m_deltaTime < m_destroyTime)
		{
			Destroy(this);  // 自身を破壊
			return;
		}
		m_deltaTime += Time.deltaTime;
    }

    protected void OnTriggerEnter(Collider other)
    {
        if (other.gameObject == m_playerObj)
        {
            // プレイヤーにバフを付与する
            ManagerContainer.GetManagerContainer().m_characterManager.
                m_buffDebuffHandler.AddBuffDebuff(m_buffDebuffData, gameObject.name);

            // プレイヤーに当たったら弾を削除する
            Destroy(gameObject);
        }
    }
}