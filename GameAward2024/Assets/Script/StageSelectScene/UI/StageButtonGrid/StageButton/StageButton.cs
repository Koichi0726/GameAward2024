using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StageButton : MonoBehaviour
{
	public enum E_BUTTON_STATE
	{
		SELECTABLE,		// 選択可
		SELECTED,		// 選択中
		UNSELECTABLE,	// 選択不可
		PUSHED			// 押された
	}

	readonly Color[] STATE_COLOR =
	{
		new Color(0.75f, 0.75f, 0.75f, 1.0f),
		new Color(1.00f, 1.00f, 1.00f, 1.0f),
		new Color(0.25f, 0.25f, 0.25f, 1.0f),
	};

	[SerializeField]
	RawImage m_rawImage;

	E_BUTTON_STATE m_state;

	/// <summary>
	/// RawImageのテクスチャを設定
	/// </summary>
	/// <param name="texture">テクスチャ</param>
	public void SetTexture(Texture texture)
	{
		if (m_rawImage == null) return;
		if (texture == null) return;

		m_rawImage.texture = texture;
	}

	/// <summary>
	/// ボタンの状態を設定
	/// </summary>
	/// <param name="state">ボタンの状況</param>
	public void SetState(E_BUTTON_STATE state)
	{
		if (m_rawImage == null) return;

		m_rawImage.color = STATE_COLOR[(int)state];
	}

	/// <summary>
	/// ボタンを押された時の処理
	/// </summary>
	public void OnPush()
	{
		if (m_state == E_BUTTON_STATE.UNSELECTABLE) return;


	}
}