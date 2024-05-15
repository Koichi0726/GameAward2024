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

    //プレイヤーの情報
    Transform tr;

    //プレイヤーのポジション
    Vector3 pos;

    //実際に使用する円運動周期
    float period;   

    void Start()
    {
        Enemy = GameObject.Find("Enemy").transform;        //TODO:CharacterManagerから参照出来るように変更
        Debug.Log(Enemy);
        DashFlag = false;
        _prevPosition = transform.position;
        tr = transform;
        pos = tr.position;
        period = 0.0f;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        //左右の移動がなければ終了する
        if (period == 0.0)
        {
            // 次のUpdateで使うための前フレーム位置更新
            _prevPosition = pos;

            return;
        }

        //変数宣言
        Vector3 _center = Enemy.position;   //回転の中心
        
        
        //上下の移動量を反映
        tr.position = pos;

        //if (Input.GetKey(KeyCode.LeftShift))
        //{
        //    period /= 2.0f;
        //    DashFlag = true;
        //}

        if(DashFlag)
        {
            period /= 2.0f;
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

        //各変数のリセット
        period = 0.0f;      //左右の移動量をリセット
        DashFlag = false;   //走っているフラグのリセット
    }

    public void OnMove()
    {
        Debug.Log("MoveIvent");
    }

    public void OnMoveUp()
    {
        pos.y += _vertical;
        tr.position = pos;
    }

    public void OnMoveDown()
    {
        pos.y -= _vertical;
        tr.position = pos;
    }

    public void OnMoveLeft()
    {
        period = _period;
    }

    public void OnMoveRight()
    {
        period = -_period;
    }

    public void OnDashStart()
    {
        DashFlag = true;
    }

    public void OnDashEnd()
    {
        DashFlag = false;
    }
}


