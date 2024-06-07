using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GameScene
{
	public class GameManager : ManagerBase
	{
		[SerializeField]
		GameTimer m_gameTimer;
		/// <summary>
		/// ゲームタイマーを取得
		/// </summary>
		public GameTimer gameTimer => m_gameTimer;

		[SerializeField]
		SelectStageData m_selectStageData;
		/// <summary>
		/// 選択されたステージの情報を取得
		/// </summary>
		public SelectStageData selectStageData => m_selectStageData;
	}
}