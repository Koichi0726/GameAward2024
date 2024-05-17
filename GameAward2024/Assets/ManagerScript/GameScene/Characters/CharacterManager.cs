using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GameScene
{
	public class CharacterManager : MonoBehaviour
	{
		[field: SerializeField]
		public Transform m_player { get; private set; }
		[field: SerializeField]
		public Transform m_enemy { get; private set; }
	}
}