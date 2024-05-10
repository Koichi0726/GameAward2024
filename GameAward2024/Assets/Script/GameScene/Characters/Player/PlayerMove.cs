using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMove : MonoBehaviour
{
    //目標（座標を使用）
    [SerializeField] private Transform Enemy;      //TODO:CharacterManagerから参照出来るように変更

    // 回転軸
    [SerializeField] private Vector3 _axis = Vector3.up;

    // 円運動周期
    [SerializeField] private float _period = 2;

    // 向きを更新するかどうか
    [SerializeField] private bool _updateRotation = true;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        var tr = transform;
        float period;
        
        //TODO:InputSystemに置き換え
        if (Input.GetKey(KeyCode.A))
        {
            period = _period;
        }
        else if (Input.GetKey(KeyCode.D))
        {
            period = -_period;
        } 
        else return;

        var angleAxis = Quaternion.AngleAxis(360 / period * Time.deltaTime, _axis);
        Vector3 _center = Enemy.position;

        var pos = tr.position;
        pos -= _center;
        pos = angleAxis * pos;
        pos += _center;

        tr.position = pos;
    }
}
