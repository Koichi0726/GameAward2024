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
    float VPeriod;
    float HPeriod;

    //i移動方向を格納
    Vector2 Dir;

    void Start()
    {
        PData = GameScene.ManagerContainer.GetManagerContainer().m_characterManager.m_playerData;
        Enemy = ManagerContainer.GetManagerContainer().m_characterManager.m_enemy;
        _period = PData.HORIZONTAL_MOVE_SPEED;
        _vertical = PData.VERTICAL_MOVE_SPEED;
        DashFlag = false;
        _prevPosition = transform.position;
        tr = transform;
        pos = tr.position;
        VPeriod = 0.0f;
        HPeriod = 0.0f;
        Dir = new Vector2(0.0f, 0.0f);
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        //左右の移動がなければ終了する
        if (VPeriod == 0.0 && HPeriod == 0.0f)
        {
            // 次のUpdateで使うための前フレーム位置更新
            _prevPosition = pos;

            return;
        }

        Dir = new Vector2(0.0f, 0.0f);

        // 次のUpdateで使うための前フレーム位置更新
        _prevPosition = pos;

        if (DashFlag)
        {
            VPeriod /= 2.0f;
            HPeriod /= 2.0f;
        }

        if (VPeriod != 0.0f) PlayerCircularRotation(VPeriod, this.transform.up);
        if (HPeriod != 0.0f) PlayerCircularRotation(HPeriod, this.transform.right);

        transform.position = pos;

        if (VPeriod < 0.0f)
        {
            Dir.x = -1.0f;
        }
        else if (VPeriod > 0.0f)
        {
            Dir.x = 1.0f;
        }
        if (HPeriod < 0.0f)
        {
            Dir.y = -1.0f;
        }
        else if (HPeriod > 0.0f)
        {
            Dir.y = 1.0f;
        }

        PlayerActionControler.PParam.m_moveDirect = Dir;
        
        //各変数のリセット
        VPeriod = 0.0f;      //左右の移動量をリセット
        HPeriod = 0.0f;      //縦の移動量をリセット
    }

    /// <summary>
    /// 上入力したときの処理関数
    /// </summary>
    public void OnMoveUp()
    {
        HPeriod = _period;
        ActionEntry();
    }

    /// <summary>
    /// 下入力したときの処理関数
    /// </summary>
    public void OnMoveDown()
    {
        HPeriod = -_period;
        ActionEntry();
    }

    /// <summary>
    /// 左入力したときの処理関数
    /// </summary>
    public void OnMoveLeft()
    {
        VPeriod = _period;
        ActionEntry();
    }

    /// <summary>
    /// 右入力したときの処理関数
    /// </summary>
    public void OnMoveRight()
    {
        VPeriod = -_period;
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

    public void PlayerCircularRotation(float p, Vector3 axis)
    {
        //変数宣言
        Vector3 _center = Enemy.position;   //回転の中心
        var angleAxis = Quaternion.AngleAxis(360 / p * Time.deltaTime, axis);     //クオータニオンの計算

        //移動先を算出
        pos -= _center;
        pos = angleAxis * pos;
        pos += _center;
    }
}


