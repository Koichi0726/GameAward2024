using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using GameScene;

public class PlayerCounter : MonoBehaviour
{
	[SerializeField] CounterArea m_counterArea;

	public void OnCounter(InputAction.CallbackContext context)
	{
		if (!context.performed) return;

		m_counterArea.CounterBullet();

		//--- タイマーのカウントを始める
		GameTimer gameTimer = ManagerContainer.instance.gameManager.gameTimer;
		if (gameTimer.m_isStart) return;
		gameTimer.StartCount();
	}
}