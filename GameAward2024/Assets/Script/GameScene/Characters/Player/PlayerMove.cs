using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using GameScene;

public class PlayerMove : MonoBehaviour
{
    // プレイヤーデータの取得用変数
    private PlayerData PData;

    // 目標（座標を使用）
    private Transform Enemy;

    // 回転軸
    [SerializeField] private Vector3 _axis = Vector3.up;

    // 基礎円運動周期
    [SerializeField] private float _period;

    // 上下の制限（x:max y:min）
    [SerializeField] private Vector2 VerticalRemit;


    // 上下の移動量
    private float _vertical;

    // 走るフラグ
    private bool DashFlag;

    // 前フレームのワールド座標
    private Vector3 _prevPosition;

    // プレイヤーの情報
    Transform tr;

    // プレイヤーのポジション
    Vector3 pos;

    // 実際に使用する円運動周期
    float period;

    Vector2 Dir;

    void Start()
    {
        PData = PlayerDataParam.data;
        Enemy = ManagerContainer.GetManagerContainer().m_characterManager.m_enemy;
        _period = PData.HORIZONTAL_MOVE_SPEED;
        _vertical = PData.VERTICAL_MOVE_SPEED;
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

        if(DashFlag)
        {
            period /= 2.0f;
        }

        var angleAxis = Quaternion.AngleAxis(360 / period * Time.deltaTime, _axis);     //クオータニオンの計算

        //移動先を算出
        pos -= _center;
        pos = angleAxis * pos;
        pos += _center;

        if(period < 0.0f)
        {
            Dir.x = -1.0f;
        }
        else
        {
            Dir.x = 1.0f;
        }

        //プレイヤーの体の向きを行動に合わせて調整
        if(!DashFlag)
        {//歩いている
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

    }

    //public void OnMove()
    //{
    //    Debug.Log("MoveIvent");
    //}

    /// <summary>
    /// 上入力したときの処理関数
    /// </summary>
    public void OnMoveUp()
    {
        pos.y += _vertical;
        if (pos.y > VerticalRemit.x)
        {
            pos.y = VerticalRemit.x;
            return;
        }
        tr.position = pos;
        ActionEntry();
        Dir.y = 1.0f;
    }

    /// <summary>
    /// 下入力したときの処理関数
    /// </summary>
    public void OnMoveDown()
    {
        pos.y -= _vertical;
        if (pos.y < VerticalRemit.y)
        {
            pos.y = VerticalRemit.y;
            return;
        }
        tr.position = pos;
        ActionEntry();
        Dir.y = -1.0f;
    }

    /// <summary>
    /// 左入力したときの処理関数
    /// </summary>
    public void OnMoveLeft()
    {
        period = _period;
        ActionEntry();
    }

    /// <summary>
    /// 右入力したときの処理関数
    /// </summary>
    public void OnMoveRight()
    {
        period = -_period;
        ActionEntry();
    }

    /// <summary>
    /// 走り始めた時の処理関数
    /// </summary>
    public void OnDashStart(InputAction.CallbackContext context)
    {
        if (!context.started) return;
        DashFlag = true;
    }

    /// <summary>
    /// 走り終わった時の処理関数
    /// </summary>
    public void OnDashEnd(InputAction.CallbackContext context)
    {
        if (!context.performed) return;
        DashFlag = false;
    }

    /// <summary>
    /// 移動方法に応じてアクションを登録する関数
    /// </summary>
    private void ActionEntry()
    {
        if(!DashFlag)
        {
            PlayerActionControler.AddAction(PlayerData.E_PLAYER_ACTION.E_MOVE);
        }
        else
        {
            PlayerActionControler.AddAction(PlayerData.E_PLAYER_ACTION.E_DASH);
        }
    }
}


