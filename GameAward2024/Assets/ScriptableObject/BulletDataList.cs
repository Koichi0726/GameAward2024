using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

[CreateAssetMenu(fileName = "BulletDataList", menuName = "ScriptableObjects/BulletDataList")]
public class BulletDataList : ScriptableObject
{
	public enum E_BULLET_KIND
	{
		// TODO:弾の配列にアクセスする為の列挙定数を追加
		NONE = -1,
		NORMAL,
		ACCELERATE,
		SIN_WAVE,
		TRACKING,
		RIPPLE,
		SPIRAL,
		DEBRI,
		MAX
	}

	readonly string[] CSV_FILE_PATH =
	{
		// TODO:csvを読み込む為のファイルパスを追加
		"SettingCSV/BulletData/NormalBullet.csv",
		"SettingCSV/BulletData/AccelerateBullet.csv",
		"SettingCSV/BulletData/SinWaveBullet.csv",
		"SettingCSV/BulletData/TrackingBullet.csv",
		"SettingCSV/BulletData/RippleBullet.csv",
		"SettingCSV/BulletData/SpiralBullet.csv",
		"SettingCSV/BulletData/DebriBullet.csv",
	};

	[SerializeField]
	BulletBase[] m_bulletPrefabList = new BulletBase[(int)E_BULLET_KIND.MAX];
	[SerializeField]
	TextAsset[] m_csvTexts = new TextAsset[(int)E_BULLET_KIND.MAX];
	CSVReader[] m_csvReaders = Enumerable.Repeat(new CSVReader(), (int)E_BULLET_KIND.MAX).ToArray();

	/// <summary>
	/// 指定の弾のプレハブを取得
	/// </summary>
	/// <param name="bulletKind">弾の種類を示す列挙定数</param>
	/// <returns>弾のデータ</returns>
	public BulletBase GetBulletPrefab(E_BULLET_KIND bulletKind)
	{
		return m_bulletPrefabList[(int)bulletKind];
	}

	/// <summary>
	/// 指定の弾のデータを読み込み(BulletBaseで呼び出す)
	/// </summary>
	/// <param name="bulletKind">弾の種類を示す列挙定数</param>
	public void Load(E_BULLET_KIND bulletKind)
	{
		//--- CSVファイルを読み込み
#if DEVELOPMENT_BUILD
		m_csvReaders[(int)bulletKind].LoadCSV(CSV_FILE_PATH[(int)bulletKind]);
#else
		m_csvReaders[(int)bulletKind].LoadCSV(m_csvTexts[(int)bulletKind]);
#endif
	}

	/// <summary>
	/// 指定の弾のデータを取得(BulletBaseで呼び出す)
	/// </summary>
	/// <param name="bulletKind">弾の種類を示す列挙定数</param>
	public Dictionary<string, CSVParamData> GetData(E_BULLET_KIND bulletKind)
	{
		return m_csvReaders[(int)bulletKind].m_csvDatas;
	}
}