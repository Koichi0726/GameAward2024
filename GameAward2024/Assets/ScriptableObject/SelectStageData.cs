using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "SelectStageData", menuName = "ScriptableObjects/SelectStageData")]
public class SelectStageData : ScriptableObject
{
	public EnemyDataList.E_ENEMY_KIND m_enemyKind = EnemyDataList.E_ENEMY_KIND.MECHA_OCTOPUS;
}