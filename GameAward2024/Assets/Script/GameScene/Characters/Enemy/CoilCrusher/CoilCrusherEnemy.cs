using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GameScene;

public class CoilCrusherEnemy : EnemyBase
{
	float m_shotInterval;
	public float SHOT_INTERVAL => m_shotInterval;

	protected override void Start()
	{
		m_enemyKind = EnemyDataList.E_ENEMY_KIND.COIL_CRUSHER;

		base.Start();
	}

	protected override void SetData(Dictionary<string, CSVParamData> data)
	{
		base.SetData(data);

		//--- ílÇÃãzèoÇµ
		//data[nameof(m_shotInterval)].TryGetData(out m_shotInterval);
	}
}