using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class SceneChange : MonoBehaviour
{
	FadeController m_fadeController;
	[SerializeField]
	SceneData m_sceneData;
	[SerializeField]
	SceneData.E_SCENE_KIND m_nextScene;

	void Start()
	{
		m_fadeController = FadeController.instance;
	}

	void Update()
    {
		// フェード中は処理しない
		if (m_fadeController.m_fadeState != FadeController.E_FADE_STATE.NONE) return;

		// 現在のキーボード情報
		var keyboardCurrent = Keyboard.current;

		// キーボード接続チェック
		if (keyboardCurrent != null)
		{
			// Spaceキーの入力状態取得
			var spaceKey = keyboardCurrent.spaceKey;

			//--- Spaceキーが押された瞬間かどうか
			if (spaceKey.wasPressedThisFrame)
			{
				FadeController.instance.FadeSceneLoad(m_sceneData.GetSceneName(m_nextScene));
				return;
			}
		}

		// 現在のゲームパッド情報
		var gamepadCurrent = Gamepad.current;

		// ゲームパッド接続チェック
		if (gamepadCurrent != null)
		{
			// Aボタンの入力状態取得
			var aButton = gamepadCurrent.aButton;

			//--- Aボタンが押された瞬間かどうか
			if (aButton.wasPressedThisFrame)
				FadeController.instance.FadeSceneLoad(m_sceneData.GetSceneName(m_nextScene));
		}
	}
}