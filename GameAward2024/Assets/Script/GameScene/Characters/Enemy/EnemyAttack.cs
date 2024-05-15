using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAttack : MonoBehaviour
{
    public GameObject bulletPrefab; // 弾のプレハブ
    public Transform player;        // プレーヤーのTransform
    public float bulletSpeed;       // 弾の速度
    public float shootInterval;     // 弾を発射する間隔
    
    private float shootTimer;

    // Start is called before the first frame update
    void Start()
    {
       
    }

    // Update is called once per frame
    void Update()
    {
        // タイマー更新
        shootTimer++;

        // タイマーが発射間隔を越えたら
        if (shootTimer >= shootInterval)
        {
            // タイマーリセット
            shootTimer = 0.0f;
            // 弾発射
            Shoot();
        }
    }

    void Shoot()
    {
        // 弾を生成
        GameObject bullet = Instantiate(bulletPrefab, transform.position, Quaternion.identity);

        // プレーヤーへの方向を計算
        Vector3 direction = (player.position - transform.position).normalized;
        
        // 弾に力を加える
        bullet.GetComponent<Rigidbody>().velocity = direction * bulletSpeed;
    }
}
