using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CounterArea : MonoBehaviour
{
	List<BulletBase> m_bullets = new List<BulletBase>();

	void Start()
	{
		StartCoroutine(ListNullCheck());
	}

	private void OnTriggerEnter(Collider other)
	{
		BulletBase bullet;
		if (!other.TryGetComponent(out bullet)) return;

		// 衝突したBulletをリストに追加
		m_bullets.Add(bullet);
	}

	private void OnTriggerExit(Collider other)
	{
		BulletBase bullet;
		if (!other.TryGetComponent(out bullet)) return;

		// 領域から外れた弾をリストから削除
		m_bullets.Remove(bullet);
	}

	public void CounterBullet()
	{
		//--- カウンター可能域にある弾のターゲットを敵に変更
		foreach (BulletBase bullet in m_bullets)
		{
			if (bullet == null) continue;
			bullet.ChangeTarget(BulletBase.E_TARGET_KIND.ENEMY);
		}
	}

	IEnumerator ListNullCheck()
	{
		while (true)
		{
			//--- nullの枠を削除
			// DestroyされるとOnTriggerExit()が反応しない為
			for (int i = 0; i < m_bullets.Count; ++i)
			{
				BulletBase bullet = m_bullets[i];
				if (bullet != null) continue;

				m_bullets.Remove(bullet);
				--i;
			}

			yield return null;
		}
	}
}