using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

[CreateAssetMenu(fileName = "EnemyDataList", menuName = "ScriptableObjects/EnemyDataList")]
public class EnemyDataList : ScriptableObject
{
	public enum E_ENEMY_KIND
	{
		// TODO:敵の配列にアクセスする為の列挙定数を追加
		NONE = -1,
		MECHA_OCTOPUS,
		MAX
	}

	readonly string[] CSV_FILE_PATH =
	{
		"SettingCSV/BulletData/TrackingBullet.csv",
	};

	[SerializeField]
	EnemyBase[] m_enemyPrefabList = new EnemyBase[(int)E_ENEMY_KIND.MAX];
	[SerializeField]
	TextAsset[] m_csvTexts = new TextAsset[(int)E_ENEMY_KIND.MAX];
	CSVReader[] m_csvReaders = Enumerable.Repeat<CSVReader>(new CSVReader(), (int)E_ENEMY_KIND.MAX).ToArray();

	/// <summary>
	/// 指定の敵のプレハブを取得
	/// </summary>
	/// <param name="enemyKind">敵の種類を示す列挙定数</param>
	/// <returns>敵のデータ</returns>
	public EnemyBase GetEnemyPrefab(E_ENEMY_KIND enemyKind)
	{
		return m_enemyPrefabList[(int)enemyKind];
	}

	/// <summary>
	/// 指定の敵のデータを読み込み(EnemyBaseで呼び出す)
	/// </summary>
	/// <param name="enemyKind">敵の種類を示す列挙定数</param>
	public void Load(E_ENEMY_KIND enemyKind)
	{
		//--- CSVファイルを読み込み
#if DEVELOPMENT_BUILD
		m_csvReaders[(int)enemyKind].LoadCSV(CSV_FILE_PATH[(int)enemyKind]);
#else
		m_csvReaders[(int)enemyKind].LoadCSV(m_csvTexts[(int)enemyKind]);
#endif
	}

	/// <summary>
	/// 指定の敵のデータを取得(EnemyBaseで呼び出す)
	/// </summary>
	/// <param name="enemyKind">敵の種類を示す列挙定数</param>
	public Dictionary<string, CSVParamData> GetData(E_ENEMY_KIND enemyKind)
	{
		return m_csvReaders[(int)enemyKind].m_csvDatas;
	}
}