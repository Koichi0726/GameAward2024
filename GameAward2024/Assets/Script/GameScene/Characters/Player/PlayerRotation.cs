using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerRotation : MonoBehaviour
{
    private Transform m_enemyTrans;

    void Start()
    {
        m_enemyTrans = GameScene.ManagerContainer.instance.characterManager.enemyTrans;
    }

    void Update()
    {
        this.transform.LookAt(m_enemyTrans.transform.position, transform.up);
    }
}