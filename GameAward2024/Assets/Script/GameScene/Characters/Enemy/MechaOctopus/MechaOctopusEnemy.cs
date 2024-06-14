using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MechaOctopusEnemy : EnemyBase
{
	float m_shotInterval;
	public float SHOT_INTERVAL => m_shotInterval;

    protected override void Start()
    {
		m_enemyKind = EnemyDataList.E_ENEMY_KIND.MECHA_OCTOPUS;

		base.Start();
    }

	protected override void Update()
    {
		base.Update();
    }

	protected override void SetData(Dictionary<string, CSVParamData> data)
	{
		base.SetData(data);

		//--- ílÇÃãzèoÇµ
		data[nameof(m_shotInterval)].TryGetData(out m_shotInterval);
	}
}