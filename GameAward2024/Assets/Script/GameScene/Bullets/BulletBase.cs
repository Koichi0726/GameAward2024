using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GameScene;

public class BulletBase : MonoBehaviour
{
	[SerializeField]
	float m_destroyTime;
    protected BuffDebuffData m_buffDebuffData = new BuffDebuffData();

    protected virtual void Start()
    {
        
    }

    protected virtual void Update()
    {
		//--- 消滅するまでの時間をカウント
		if (m_destroyTime < 0.0f)
		{
			Destroy(gameObject);  // 自身を破壊
			return;
		}
        m_destroyTime -= Time.deltaTime;
    }

    protected void OnTriggerEnter(Collider other)
    {
        // プレイヤーにバフを付与する
        ManagerContainer.instance.characterManager.
            buffDebuffHandler.AddBuffDebuff(m_buffDebuffData, gameObject.name);

        // プレイヤーに当たったら弾を削除する
        Destroy(gameObject);
    }
}