using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GameScene;

namespace MechaOctopus
{
	public class AnimeIdle : StateMachineBehaviour
	{
		float m_deltatime;
		Transform m_playerTrans;
		Transform m_enemyTrans;
		MechaOctopusEnemy m_mechaOctopus;
		GameTimer m_gameTimer;

		// OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
		override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
		{
			m_deltatime = 0.0f;

			//--- 自分とプレイヤーのTransformを取得
			CharacterManager characterManager = ManagerContainer.instance.characterManager;
			m_playerTrans = characterManager.playerTrans;
            m_enemyTrans = characterManager.enemyTrans;

			// 敵のデータを取得
			m_mechaOctopus = characterManager.enemyData as MechaOctopusEnemy;

			m_gameTimer = ManagerContainer.instance.gameManager.gameTimer;
		}

		// OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
		override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
		{
			// プレイヤーの方を向く
			m_enemyTrans.LookAt(m_playerTrans.position, m_enemyTrans.up);

			if (!m_gameTimer.m_isStart) return;

			// アタックに切り替える
			if (m_deltatime >= m_mechaOctopus.SHOT_INTERVAL)
				animator.SetBool("IsAttack", true);

			//経過時間を更新する
			m_deltatime += Time.deltaTime;
		}
	}
}