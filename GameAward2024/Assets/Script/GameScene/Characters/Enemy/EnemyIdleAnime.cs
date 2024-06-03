using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyIdleAnime : StateMachineBehaviour
{
    float _deltatime;
    Vector3 m_playertransform;
    Vector3 m_enemytransform;
    Vector3 m_enemyforward;

    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        _deltatime = 0.0f;
        Debug.Log("start");
    }

    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        // ログ出力
        Debug.Log("状態:Idle");
        Debug.Log("経過時間:" + _deltatime + "秒");

        // プレイヤーのトランスフォーム取得
        GameScene.CharacterManager characterManager =
            GameScene.ManagerContainer.GetManagerContainer().m_characterManager;
        m_playertransform = characterManager.m_player.position;
        m_enemytransform = characterManager.m_enemy.position;

        characterManager.m_enemy.forward = m_playertransform;




        // 3秒経過後にアタックに切り替える
        if (_deltatime >= 3.0f)
        {
            animator.SetBool("IsAttack", true);
        }

        //経過時間を更新する
        _deltatime += Time.deltaTime;
    }

    // OnStateExit is called when a transition ends and the state machine finishes evaluating this state
    //override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    //{
    //    
    //}

    // OnStateMove is called right after Animator.OnAnimatorMove()
    //override public void OnStateMove(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    //{
    //    // Implement code that processes and affects root motion
    //}

    // OnStateIK is called right after Animator.OnAnimatorIK()
    //override public void OnStateIK(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    //{
    //    // Implement code that sets up animation IK (inverse kinematics)
    //}
}
