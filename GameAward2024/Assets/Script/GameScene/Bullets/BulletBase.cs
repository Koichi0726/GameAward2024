using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GameScene;

public class BulletBase : MonoBehaviour
{
	protected BulletDataList.E_BULLET_KIND m_bulletKind;
	BulletDataList m_bulletDataList;
	float m_destroyTime;
    protected BuffDebuffData m_buffDebuffData = new BuffDebuffData();

    protected virtual void Start()
    {
		//--- 弾データの読み込みと設定
		m_bulletDataList = ManagerContainer.instance.bulletManger.bulletDataList;
		m_bulletDataList.Load(m_bulletKind);
		var data = m_bulletDataList.GetData(m_bulletKind);
		SetData(data);
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

	protected virtual void OnTriggerEnter(Collider other)
    {
        // プレイヤーにバフを付与する
        ManagerContainer.instance.characterManager.
            buffDebuffHandler.AddBuffDebuff(m_buffDebuffData, this.GetType().Name);

        // プレイヤーに当たったら弾を削除する
        Destroy(gameObject);
    }

	protected virtual void SetData(Dictionary<string, CSVParamData> data)
	{
		//--- 値の吸出し
		data[nameof(m_destroyTime)].TryGetData(out m_destroyTime);
		data[nameof(m_buffDebuffData.m_paramCoefficient.m_addGaugeValue	)].TryGetData(out m_buffDebuffData.m_paramCoefficient.m_addGaugeValue);
		data[nameof(m_buffDebuffData.m_paramCoefficient.m_subGaugeValue	)].TryGetData(out m_buffDebuffData.m_paramCoefficient.m_subGaugeValue);
		data[nameof(m_buffDebuffData.m_paramCoefficient.m_gaugeUpSpeed	)].TryGetData(out m_buffDebuffData.m_paramCoefficient.m_gaugeUpSpeed);
		data[nameof(m_buffDebuffData.m_paramCoefficient.m_gaugeDownSpeed)].TryGetData(out m_buffDebuffData.m_paramCoefficient.m_gaugeDownSpeed);
		data[nameof(m_buffDebuffData.m_paramCoefficient.m_moveDirect	)].TryGetData(out m_buffDebuffData.m_paramCoefficient.m_moveDirect);
		data[nameof(m_buffDebuffData.m_paramCoefficient.m_moveSpeed		)].TryGetData(out m_buffDebuffData.m_paramCoefficient.m_moveSpeed);
		data[nameof(m_buffDebuffData.m_remainingDuration)].TryGetData(out m_buffDebuffData.m_remainingDuration);
	}
}