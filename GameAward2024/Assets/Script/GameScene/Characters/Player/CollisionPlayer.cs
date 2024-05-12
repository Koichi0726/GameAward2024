using System.Collections;
using System.Collections.Generic;
using UnityEditor.Rendering;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;

public class CollisionPlayer : MonoBehaviour
{

    [SerializeField] private GameObject damageTexture;
    private Volume volume;
    Vignette vignette;
    bool isVignette = false;
    [SerializeField] float displaytime = 1.0f;
    float countdownTimer;
    
    // Start is called before the first frame update
    void Start()
    {
        volume = GameScene.ManagerContainer.GetManagerContainer().m_studioObjectManager.m_volume;
        volume.profile.TryGet(out vignette);
        countdownTimer = displaytime;
        
    }

    // Update is called once per frame
    void Update()
    {
        if (isVignette)
        {
            countdownTimer -= Time.deltaTime;
            if (countdownTimer < 0)
            {
                isVignette = false;
                vignette.active = false;
            }
        }

        
    }

    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.TryGetComponent<BoxCollider> (out var EnemyAttackComponent))   // todo:BoxColliderから敵の弾用コンポーネントに後で直す
        {

            //テクスチャを画面に出す
            Instantiate(damageTexture, transform.position, Quaternion.identity);

            //周りを一瞬点滅させるポストエフェクト
            vignette.active = true;
            isVignette = true;

            
        }

    }
}
