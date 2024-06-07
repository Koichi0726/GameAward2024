using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MechaOctopusEnemy : EnemyBase
{
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
	}
}