using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

namespace GameScene
{
	public class StudioObjectManager : MonoBehaviour
	{
		[field: SerializeField]
		public Camera m_mainCamera { get; private set; }
		[field: SerializeField]
		public Light m_directionLight { get; private set; }
		[field: SerializeField]
		public Volume m_volume { get; private set; }
		[field: SerializeField]
        public GameTimer m_gameTimer { get; private set; }
	}
}