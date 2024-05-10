using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerDodge : MonoBehaviour
{
    // Start is called before the first frame update

    [SerializeField] GameObject player;
    [SerializeField] GameObject boss;

    private float rotY;

    private Vector3 playerPos;
    private Vector3 bossPos;
    private Vector3 distance;

    void Start()
    {
        playerPos = player.transform.position;
        bossPos = boss.transform.position;
        distance = playerPos - bossPos;
    }

    // Update is called once per frame
    void Update()
    {
        //プレイヤーの回避後の座標を計算
        playerPos.x += distance.x * Mathf.Cos(rotY) - distance.z * Mathf.Sin(rotY);
        playerPos.x += distance.x * Mathf.Sin(rotY) - distance.z * Mathf.Cos(rotY);    
    }    
}
