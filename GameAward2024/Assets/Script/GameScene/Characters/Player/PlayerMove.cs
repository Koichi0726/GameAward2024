using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMove : MonoBehaviour
{
    // 目標（座標を使用）
    private Transform Enemy;      //TODO:CharacterManagerから参照出来るように変更

    // 回転軸
    [SerializeField] private Vector3 _axis = Vector3.up;

    // 基礎円運動周期
    [SerializeField] private float _period = 4;

    // 上下の移動量
    private float _vertical = 0.1f;

    // 走るフラグ
    private bool DashFlag;

    // 前フレームのワールド座標
    private Vector3 _prevPosition;

    Transform tr;
    Vector3 pos;

    void Start()
    {
        DashFlag = false;
        _prevPosition = transform.position;
        Enemy = GameObject.Find("Enemy").transform;        //TODO:CharacterManagerから参照出来るように変更
        tr = transform;
        pos = tr.position;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        //変数宣言
        //var tr = transform; //プレイヤーの情報
        float period;   //実際に使用する円運動周期
        Vector3 _center = Enemy.position;   //回転の中心
        DashFlag = false;   //走っているフラグのリセット
        //var pos = tr.position;  //プレイヤーのポジション

        //TODO:InputSystemに置き換え
        //上下の移動
        //if (Input.GetKey(KeyCode.W))
        //{//上
        //    pos.y += _vertical;
        //}
        //else if(Input.GetKey(KeyCode.S))
        //{//下
        //    pos.y -= _vertical;
        //}

        //上下の移動量を反映
        tr.position = pos;

        //左右の移動（回転移動）
        if (Input.GetKey(KeyCode.A))
        {//左
            period = _period;
        }
        else if (Input.GetKey(KeyCode.D))
        {//右
            period = -_period;
        }
        else return;    //移動キー入力がされてなければ終了

        if (Input.GetKey(KeyCode.LeftShift))
        {
            period /= 2.0f;
            DashFlag = true;
        }

        var angleAxis = Quaternion.AngleAxis(360 / period * Time.deltaTime, _axis);     //クオータニオンの計算
       
        //移動先を算出
        pos -= _center;
        pos = angleAxis * pos;
        pos += _center;

        //算出した結果を反映
        tr.position = pos;

        if(!DashFlag)
        {//歩いている
            //敵の方向を向く
            //tr.rotation = tr.rotation * angleAxis;
            Vector3 trans = Enemy.position; //敵の座標取得
            trans = new Vector3(trans.x, tr.position.y, trans.z);   //Y軸成分を無効化
            tr.LookAt(trans);   //敵の方向に回転
        }
        else
        {//走っている
            //前フレームからの移動量を計算
            var delta = pos - _prevPosition;

            // 進行方向（移動量ベクトル）に向くようなクォータニオンを取得
            var rotation = Quaternion.LookRotation(delta, Vector3.up);

            // オブジェクトの回転に反映
            tr.rotation = rotation;
        }
        // 次のUpdateで使うための前フレーム位置更新
        _prevPosition = pos;
    }

    public void OnMove()
    {
        Debug.Log("MoveIvent");
    }

    public void OnMoveUp()
    {
        Debug.Log("A");
        pos.y += _vertical;
    }

    public void OnMoveDown()
    {
        pos.y -= _vertical;
    }

    public void OnMoveLeft()
    {
        Debug.Log("MoveIvent");
    }

    public void OnMoveRight()
    {
        Debug.Log("MoveIvent");
    }

    public void OnDash()
    {
        Debug.Log("MoveIvent");
    }
}


