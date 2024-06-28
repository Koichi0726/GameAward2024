using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GameScene;

public class EnemyBase : MonoBehaviour
{
	protected EnemyDataList.E_ENEMY_KIND m_enemyKind;
	[SerializeField]
	protected List<Transform> m_muzzleTransList =  new List<Transform>();
	EnemyDataList m_enemyDataList;
	float m_limitTime;
	public float limitTime => m_limitTime;
	float m_hp;
	public float hp => m_hp;
	public float m_maxHp { get; private set; }

	protected virtual void Start()
	{
		//--- 敵データの読み込みと設定
		m_enemyDataList = ManagerContainer.instance.characterManager.enemyDataList;
		m_enemyDataList.Load(m_enemyKind);
		var data = m_enemyDataList.GetData(m_enemyKind);
		SetData(data);
		m_maxHp = m_hp;
	}

	protected virtual void Update()
    {
#if DEVELOPMENT_BUILD
		//--- 敵データの再設定
		m_enemyDataList.Load(m_enemyKind);
		var data = m_enemyDataList.GetData(m_enemyKind);
		SetData(data);
#endif
	}

	protected virtual void SetData(Dictionary<string, CSVParamData> data)
	{
		//--- 値の吸出し
		data[nameof(m_limitTime)].TryGetData(out m_limitTime);
		data[nameof(m_hp		)].TryGetData(out m_hp);
	}

	/// <summary>
	/// 敵のHPを減算
	/// </summary>
	/// <param name="damage">ダメージ量</param>
	public void SubHp(float damage)
	{
		m_hp -= damage;
		m_hp = Mathf.Clamp(m_hp, 0.0f, m_maxHp);
	}

	/// <summary>
	/// 死亡しているかを判定
	/// </summary>
	/// <returns>死亡フラグ</returns>
	public bool IsDead()
	{
		return m_hp <= 0.0f;
	}

	public Transform GetMuzzleTrans(int mussleNum)
	{
		if (mussleNum < 0) return null;
		if (mussleNum >= m_muzzleTransList.Count) return null;

		return m_muzzleTransList[mussleNum];
	}
}