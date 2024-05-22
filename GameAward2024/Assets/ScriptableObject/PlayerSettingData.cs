using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "PlayerSettingData", menuName = "ScriptableObjects/PlayerSettingData")]
public class PlayerSettingData : ScriptableObject
{
	[field: SerializeField]
	public float MOVE_SPEED { get; private set; }
}