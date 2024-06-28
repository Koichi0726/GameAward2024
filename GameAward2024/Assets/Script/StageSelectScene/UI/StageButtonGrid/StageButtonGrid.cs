using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.InputSystem;

public class StageButtonGrid : MonoBehaviour
{
	const int ERROR_IDX = -1;

	[SerializeField]
	SceneData m_sceneData;
	[SerializeField]
	EnemyDataList m_enemyDataList;
	[SerializeField]
	SelectStageData m_stageData;
	[SerializeField]
	GridLayoutGroup m_grid;
	[SerializeField]
	StageButton m_stageButton;

	List<StageButton> m_buttons = new List<StageButton>();
	Vector2Int m_selectPos = new Vector2Int();

    void Start()
    {
		EnemyDataList.E_ENEMY_KIND maxEnemyNum = EnemyDataList.E_ENEMY_KIND.MAX;

		for(int i = 0; i < (int)maxEnemyNum; ++i)
		{
			// ボタンを作成
			StageButton button = Instantiate(m_stageButton);

			//--- ボタンの設定
			EnemyDataList.E_ENEMY_KIND enemyKind = (EnemyDataList.E_ENEMY_KIND)i;
			button.SetTexture(m_enemyDataList.GetStageImage(enemyKind));
			button.SetState(StageButton.E_BUTTON_STATE.SELECTABLE); // TODO:セーブデータによって変更していく

			// 自身の子に設定
			button.transform.SetParent(transform);

			// リストにボタンを追加
			m_buttons.Add(button);
		}

		// 先頭の要素を選択中にする
		m_buttons[0].SetState(StageButton.E_BUTTON_STATE.SELECTED);
	}

	/// <summary>
	/// カーソル移動時の処理
	/// </summary>
	public void OnMoveCursor(InputAction.CallbackContext context)
	{
		if (!context.performed) return;
		FadeController fadeController = FadeController.instance;
		if (fadeController.m_fadeState != FadeController.E_FADE_STATE.NONE) return;

		Vector2Int maxElementNum = CalcGridElement(m_grid);

		//--- 現在選択中のボタンを選択可能状態にする
		int idx = ToIndex(m_selectPos, maxElementNum);
		m_buttons[idx].SetState(StageButton.E_BUTTON_STATE.SELECTABLE);
		Vector2Int prePos = m_selectPos;	// 過去の座標

		//--- 移動後のボタンを選択中の状態にする
		Vector2 inputVector2 = context.ReadValue<Vector2>();
		Vector2Int value = new Vector2Int((int)inputVector2.x, (int)-inputVector2.y);
		while(true)
		{
			m_selectPos += value;   // 選択中の座標を移動
			m_selectPos += maxElementNum;

			//--- グリッドの中でループさせる
			m_selectPos.x %= maxElementNum.x;
			m_selectPos.y %= maxElementNum.y;

			idx = ToIndex(m_selectPos, maxElementNum);

			// エラー値でなく、かつリストの範囲内である場合のみ
			if (idx != ERROR_IDX && idx < m_buttons.Count) break;
		}
		m_buttons[idx].SetState(StageButton.E_BUTTON_STATE.SELECTED);
	}

	/// <summary>
	/// ステージ選択時の処理
	/// </summary>
	public void OnPushSelectButton(InputAction.CallbackContext context)
	{
		if (!context.performed) return;

		//--- 敵の番号を計算し、データを渡す
		Vector2Int maxElementNum = CalcGridElement(m_grid);
		EnemyDataList.E_ENEMY_KIND enemyKind;
		enemyKind = (EnemyDataList.E_ENEMY_KIND)ToIndex(m_selectPos, maxElementNum);
		m_stageData.m_enemyKind = enemyKind;

		//--- ゲームシーンへ遷移
		FadeController instance = FadeController.instance;
		instance.FadeSceneLoad(m_sceneData.GetSceneName(SceneData.E_SCENE_KIND.GAME_SCENE));
	}

	Vector2Int CalcGridElement(GridLayoutGroup grid)
	{
		Vector2Int result = new Vector2Int();

		//--- 計算に必要な情報を取得
		RectTransform rt;
		grid.TryGetComponent(out rt);
		int childCnt = grid.transform.childCount;

		//--- GridLayoutGroupのセルサイズと間隔
		Vector2 cellSize = grid.cellSize;
		Vector2 spacing = grid.spacing;

		//--- 行・列の数を計算
		float width = rt.rect.width;
		result.x = Mathf.FloorToInt((width + spacing.x) / (cellSize.x + spacing.x));
		result.y = Mathf.CeilToInt((float)childCnt / result.x);

		return result;
	}

	int ToIndex(Vector2Int elementNum, Vector2Int maxElementNum)
	{
		//--- 範囲外アクセスの場合はエラー値を返す
		if (elementNum.x < 0) return ERROR_IDX;
		if (elementNum.y < 0) return ERROR_IDX;
		if (elementNum.x > maxElementNum.x) return ERROR_IDX;
		if (elementNum.y > maxElementNum.y) return ERROR_IDX;

		return elementNum.x + (elementNum.y * maxElementNum.x);
	}
}