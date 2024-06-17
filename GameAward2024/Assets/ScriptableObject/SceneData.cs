using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "SceneData", menuName = "ScriptableObjects/SceneData")]
public class SceneData : ScriptableObject
{
	public enum E_SCENE_KIND
	{
		TITLE_SCENE,
		STAGE_SELECT_SCENE,
		GAME_SCENE,
		GAME_CLEAR_SCENE,
		GAME_OVER_SCENE,
	}

	readonly string[] SCENE_NAMES = {
		"TitleScene",
		"StageSelectScene",
		"GameScene",
		"GameClearScene",
		"GameOverScene",
	};

	public string GetSceneName(E_SCENE_KIND sceneKind)
	{
		return this.SCENE_NAMES[(int)sceneKind];
	}
}