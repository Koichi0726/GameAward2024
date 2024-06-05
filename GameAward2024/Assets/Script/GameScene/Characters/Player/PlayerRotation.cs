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
        this.transform.LookAt(target.transform.position, transform.up);
    }
}
