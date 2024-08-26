using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GameScene;

namespace CoilCrusher
{
    public class AnimeAttack : StateMachineBehaviour
    {
        const int MUZZLE_NUM = 0;

        Transform m_enemyTrans;
        Transform m_muzzleTrans;

        int i;

        // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
        override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
        {
            //--- �G��Transform���擾
            CharacterManager characterManager = ManagerContainer.instance.characterManager;
            m_enemyTrans = characterManager.enemyTrans;



            //--- ���e�̎�ނ�����
            i = Random.Range(0, 4);
            BulletDataList.E_BULLET_KIND bulletKind;
            if (i == 0)
                bulletKind = BulletDataList.E_BULLET_KIND.SIN_WAVE;
            else
                bulletKind = BulletDataList.E_BULLET_KIND.RIPPLE;
            //if (i == 0)
            //    bulletKind = BulletDataList.E_BULLET_KIND.TRACKING;
            //else if (i == 1)
            //    bulletKind = BulletDataList.E_BULLET_KIND.SPIRAL;
            //else if (i == 2)
            //    bulletKind = BulletDataList.E_BULLET_KIND.ACCELERATE;
            //else if (i == 3)
            //    bulletKind = BulletDataList.E_BULLET_KIND.DEBRI;
            //else
            //    bulletKind = BulletDataList.E_BULLET_KIND.NORMAL;


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
