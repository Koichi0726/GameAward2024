using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GameScene;

public class GameObserver : MonoBehaviour
{
	[SerializeField]
	SceneData m_sceneData;

    GameTimer m_gameTimer;
	PlayerControler m_player;
	EnemyBase m_enemy;
    FadeController m_fadeController;
       
    void Start()
    {
		ManagerContainer managerContainer = ManagerContainer.instance;
        m_gameTimer = managerContainer.gameManager.gameTimer;
	 	CharacterManager characterManager = managerContainer.characterManager;
		m_player = characterManager.playerController;
		m_enemy = characterManager.enemyData;
		m_fadeController = FadeController.instance;
	}

    void Update()
    {
		//--- フェード中か判定
		bool isfade = (m_fadeController.m_fadeState != FadeController.E_FADE_STATE.NONE);
		if (isfade) return;

		//--- タイムアップの場合→ゲームオーバー
		if (m_gameTimer.IsTimeUp())
		{
			m_fadeController.FadeSceneLoad(m_sceneData.GetSceneName(SceneData.E_SCENE_KIND.GAME_OVER_SCENE));
			return;
		}
	
		//--- プレイヤーが死亡した場合→ゲームオーバー
		if(m_player.IsDead())
		{
			m_fadeController.FadeSceneLoad(m_sceneData.GetSceneName(SceneData.E_SCENE_KIND.GAME_OVER_SCENE));
			return;
		}

		//--- 敵が死亡した場合→ゲームクリア
		if (m_enemy.IsDead())
		{
			m_fadeController.FadeSceneLoad(m_sceneData.GetSceneName(SceneData.E_SCENE_KIND.GAME_CLEAR_SCENE));
			return;
		}
	}
}