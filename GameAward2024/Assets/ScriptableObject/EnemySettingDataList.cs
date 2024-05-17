using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "EnemySettingDataList", menuName = "ScriptableObjects/EnemySettingDataList")]
public class EnemySettingDataList : ScriptableObject
{
	public enum E_ENEMY_KIND
	{
		// TODO:敵の配列にアクセスする為の列挙定数を追加
	}

	[SerializeField]
	List<EnemySettingDataBase> m_enemySettingDataList = new List<EnemySettingDataBase>();

	/// <summary>
	/// 指定の敵のデータを取得
	/// </summary>
	/// <typeparam name="T">EnemySettingDataBaseを継承した派生クラス</typeparam>
	/// <param name="enemyKind">敵の種類を示す列挙定数</param>
	/// <returns>敵のデータ</returns>
	public T GetEnemySettingData<T>(E_ENEMY_KIND enemyKind) where T : EnemySettingDataBase
	{
		EnemySettingDataBase data = this.m_enemySettingDataList[(int)enemyKind];
		return data as T;	// 指定の敵のデータへキャスト
	}
}