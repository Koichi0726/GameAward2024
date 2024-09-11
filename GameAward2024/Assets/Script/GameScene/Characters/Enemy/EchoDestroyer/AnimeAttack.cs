using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GameScene;

namespace EchoDestroyer
{
    public class AnimeAttack : StateMachineBehaviour
    {
        const int MUZZLE_NUM = 0;
        
        Transform m_enemyTrans;
        Transform m_muzzleTrans;
        // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
        override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
        {
            //--- �G��Transform���擾
            CharacterManager characterManager = ManagerContainer.instance.characterManager;
            m_enemyTrans = characterManager.enemyTrans;



            //--- ���e�̎�ނ�����
            BulletDataList.E_BULLET_KIND bulletKind;
            if(Random.Range(0, 3) == 0)
                bulletKind = BulletDataList.E_BULLET_KIND.RIPPLE;
            else if (Random.Range(0, 3) == 1)
                bulletKind = BulletDataList.E_BULLET_KIND.NORMAL;
            else
                bulletKind = BulletDataList.E_BULLET_KIND.SIN_WAVE;

            //--- �e�̔���
            Vector3 muzzlePos = characterManager.enemyData.GetMuzzleTrans(MUZZLE_NUM).position;
            BulletManager bulletManager = ManagerContainer.instance.bulletManger;
            bulletManager.CreateBullet(
                bulletKind, muzzlePos, m_enemyTrans.rotation);
        }

        // OnStateExit is called when a transition ends and the state machine finishes evaluating this state
        override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
        {
            // �A�j���[�V�������I��莟��A�C�h����Ԃ֑J��
            animator.SetBool("IsAttack", false);
        }
    }
}
