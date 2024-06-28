using UnityEngine;
using UnityEngine.InputSystem;

public class KeyRepeatInteraction : IInputInteraction
{
    // ボタンが最初に押されてからリピート処理が始まるまでの時間[s]
    public float m_repeatDelay = 0.01f;

    // ボタンが押されている間のリピート処理の間隔[s]
    public float m_repeatInterval = 0.01f;

    // ボタンの閾値（0の場合はデフォルト設定値を使用）
    public float m_pressPoint = 0;

    // 設定値かデフォルト値の値を格納するフィールド
    private float PressPointOrDefault => m_pressPoint > 0 ? m_pressPoint : InputSystem.settings.defaultButtonPressPoint;
    private float ReleasePointOrDefault => PressPointOrDefault * InputSystem.settings.buttonReleaseThreshold;

    // 次のリピート時刻[s]
    private double m_nextRepeatTime;

#if UNITY_EDITOR
    [UnityEditor.InitializeOnLoadMethod]
#else
    [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.SubsystemRegistration)]
#endif
    public static void Initialize()
    {
        // 初回にInteractionを登録する必要がある
        InputSystem.RegisterInteraction<KeyRepeatInteraction>();
    }

    public void Process(ref InputInteractionContext context)
    {
        // 設定値のチェック
        if (m_repeatDelay <= 0 || m_repeatInterval <= 0)
        {
            Debug.LogError("initialDelayとrepeatIntervalは0より大きい値を設定してください。");
            return;
        }

        if (context.timerHasExpired)
        {
            // リピート時刻に達したら再びPerformedに遷移
            if (context.time >= m_nextRepeatTime)
            {
                // リピート処理の次回実行時刻を設定
                m_nextRepeatTime = context.time + m_repeatInterval;

                // リピート時の処理
                context.PerformedAndStayPerformed();

                // 次のリピート時刻にProcessメソッドが呼ばれるようにタイムアウトを設定
                context.SetTimeout(m_repeatInterval);
            }

            return;
        }

        switch (context.phase)
        {
            case InputActionPhase.Waiting:
                // ボタンが押されたらStartedに遷移
                if (context.ControlIsActuated(PressPointOrDefault))
                {
                    // ボタンが押された時の処理
                    context.Started();
                    context.PerformedAndStayPerformed();

                    // リピート処理の初回実行時刻を設定
                    m_nextRepeatTime = context.time + m_repeatDelay;

                    // 次のリピート時刻にProcessメソッドが呼ばれるようにタイムアウトを設定
                    context.SetTimeout(m_repeatDelay);
                }

                break;

            case InputActionPhase.Performed:
                // ボタンが離されたらCanceledに遷移
                if (!context.ControlIsActuated(ReleasePointOrDefault))
                {
                    // ボタンが離された時の処理
                    context.Canceled();
                }

                break;
        }
    }

    public void Reset()
    {
        m_nextRepeatTime = 0;
    }
}