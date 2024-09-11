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
            //--- 敵のTransformを取得
            CharacterManager characterManager = ManagerContainer.instance.characterManager;
            m_enemyTrans = characterManager.enemyTrans;



            //--- 撃つ弾の種類を決定
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


            //--- 弾の発射
            Vector3 muzzlePos = characterManager.enemyData.GetMuzzleTrans(MUZZLE_NUM).position;
            BulletManager bulletManager = ManagerContainer.instance.bulletManger;
            bulletManager.CreateBullet(
                bulletKind, muzzlePos, m_enemyTrans.rotation);
        }

        // OnStateExit is called when a transition ends and the state machine finishes evaluating this state
        override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
        {
            // アニメーションが終わり次第アイドル状態へ遷移
            animator.SetBool("IsAttack", false);
        }
    }
}
