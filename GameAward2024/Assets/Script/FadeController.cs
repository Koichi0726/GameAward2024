using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

//--- 参考サイト
// https://kurokumasoft.com/2022/09/03/unity-dont-destroy-on-load/
// https://zenn.dev/daichi_gamedev/articles/unity-fadeout

public class FadeController : MonoBehaviour
{
	public enum E_FADE_STATE
	{
		NONE,
		FADE_OUT,
		FADE_IN,
		END
	}

	readonly Color MIN_ALPHA_COLOR = new Color(0.0f, 0.0f, 0.0f, 0.0f);
	readonly Color MAX_ALPHA_COLOR = new Color(0.0f, 0.0f, 0.0f, 1.0f);
	const float FADE_TIME = 1.0f;

	public static FadeController instance = null;

	[SerializeField]
	Image m_fadePlane;
	public E_FADE_STATE m_fadeState { get; private set; } = E_FADE_STATE.NONE;

	void Awake()
	{
		CheckInstance();    // シングルトンの処理
	}

	void Start()
	{
		// シーンを跨いで存在
		DontDestroyOnLoad(gameObject);
	}

	void CheckInstance()
	{
		if (FadeController.instance == null)
		{
			// 未作成の場合、参照データを格納
			FadeController.instance = this;
			return;
		}

		// 作成済みの場合、オブジェクトを削除
		Destroy(gameObject);
	}

	public void FadeSceneLoad(string sceneName)
	{
		// フェード中は処理しない
		if (m_fadeState != E_FADE_STATE.NONE) return;
		StartCoroutine(Fade(sceneName));	// フェード処理を実行
	}

	IEnumerator Fade(string sceneName)
	{
		m_fadeState = E_FADE_STATE.FADE_OUT;	// 状態をフェードアウト中に変更
		m_fadePlane.enabled = true;  // パネルを有効化
		float deltaTime = 0.0f;
		
		//--- フェードアウト処理
		while(deltaTime <= FADE_TIME)
		{
			deltaTime += Time.deltaTime;    // 経過時間を加算
			float t = Mathf.Clamp01(deltaTime / FADE_TIME);  // フェードの進行度を計算
			m_fadePlane.color = 
				Color.Lerp(MIN_ALPHA_COLOR, MAX_ALPHA_COLOR, t); // パネルの色を変更してフェードアウト
			AudioListener.volume = 1.0f - t;
			yield return null;
		}

		m_fadePlane.color = MAX_ALPHA_COLOR;	// フェードアウトが完了したら最終色に設定
		SceneManager.LoadScene(sceneName);  // 次のシーンをロード
		yield return null;

		//--- フェードイン処理
		m_fadeState = E_FADE_STATE.FADE_IN;  // 状態をフェードイン中に変更
		deltaTime = 0.0f;
		while (deltaTime <= FADE_TIME)
		{
			deltaTime += Time.deltaTime;    // 経過時間を加算
			float t = 1.0f - Mathf.Clamp01(deltaTime / FADE_TIME);  // フェードの進行度を計算
			m_fadePlane.color =
				Color.Lerp(MIN_ALPHA_COLOR, MAX_ALPHA_COLOR, t); // パネルの色を変更してフェードアウト
			AudioListener.volume = 1.0f - t;
			yield return null;
		}

		m_fadePlane.enabled = false;	// パネルを無効化
		m_fadeState = E_FADE_STATE.END;  // 状態をフェード終了に変更
		yield return null;

		m_fadeState = E_FADE_STATE.NONE; // 状態を非フェード中に変更
	}
}