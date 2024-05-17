using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class GameObsever : MonoBehaviour
{
    GameTimer m_gameTimer;
    FadeController m_fadeController;
    
    
    // Start is called before the first frame update
    void Start()
    {
        m_gameTimer = GameScene.ManagerContainer.GetManagerContainer().m_studioObjectManager.m_gameTimer;
        m_fadeController = FadeController.instance;
    }

    // Update is called once per frame
    void Update()
    {
        // フェードを呼び出して、フェード中か判定
        bool isfade = (m_fadeController.m_fadeState != FadeController.E_FADE_STATE.NONE);
        if (!m_gameTimer.IsTimeUp()) { return; }
        if (isfade) { return; }
       // FadeController.instance.FadeSceneLoad("result");  //todo:シーンができ次第追加
        
        
    }
}
