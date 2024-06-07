using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

namespace GameScene
{
	public class StudioObjectManager : ManagerBase
	{
		[SerializeField]
		Camera m_mainCamera;
		/// <summary>
		/// メインカメラを取得
		/// </summary>
		public Camera mainCamera => m_mainCamera;

		[SerializeField]
		Light m_directionalLight;
		/// <summary>
		/// 平行ライトを取得
		/// </summary>
		public Light directionalLight => m_directionalLight;

		[SerializeField]
		Volume m_volume;
		/// <summary>
		/// ボリュームを取得
		/// </summary>
		public Volume volume => m_volume;
	}
}