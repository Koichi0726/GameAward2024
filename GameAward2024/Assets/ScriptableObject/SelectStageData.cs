using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "SelectStageData", menuName = "ScriptableObjects/SelectStageData")]
public class SelectStageData : ScriptableObject
{
	public float m_stageLimitTime { get; set; }
	public Object m_enemyPrefab { get; set; }
}