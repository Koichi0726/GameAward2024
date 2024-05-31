using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerRotation : MonoBehaviour
{
    private Transform target;

    // Start is called before the first frame update
    void Start()
    {
        target = GameScene.ManagerContainer.GetManagerContainer().m_characterManager.m_enemy;
    }

    // Update is called once per frame
    void Update()
    {
        //// 対象物と自分自身の座標からベクトルを算出
        //Vector3 vector3 = target.transform.position - this.transform.position;
        //// Quaternion(回転値)を取得
        //Quaternion quaternion = Quaternion.LookRotation(vector3);
        //// 算出した回転値をこのゲームオブジェクトのrotationに代入
        //this.transform.rotation = quaternion;
        this.transform.LookAt(target.transform.position, transform.up);
    }
}
