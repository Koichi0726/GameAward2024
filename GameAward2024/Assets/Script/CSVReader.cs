using System;
using System.IO;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/**
 * ○データの並び
 * パラメータの説明	,メンバ変数名	,数値...
 * 
 * ○使用例
 * ライフ			,m_life			,3
 * 移動速度			,m_moveSpeed	,0.3
 * デバッグモード	,m_isDebug		,true
 * 名前				,m_name			,Player
 * 座標				,m_pos			,0.0 5.0 10.0
 * 
 * ※Vector3の場合は半角スペースで値を区切る
 * ※対応している型はint, float, bool, string, Vector3
 * ※空行や1行目のセルの文字列の先頭に「//」が付与されている場合は無視される
 */

public class CSVReader
{
	const char CSV_SPLIT = ',';

	public Dictionary<string, CSVParamData> m_csvDatas { get; private set; }
	string m_filePath;

	/// <summary>
	/// CSVファイルを読み込む
	/// </summary>
	/// <param name="filePath">ファイルパス</param>
	public void LoadCSV(string filePath)
	{
		m_filePath = AppDomain.CurrentDomain.BaseDirectory.TrimEnd('\\') + "/" + filePath;	// ファイルパスを記憶

		StringReader reader = new StringReader(ReadAllText(m_filePath));
		ParseCSVData(reader);   // CSVデータをCSVParamDataへ変換

#if DEVELOPMENT_BUILD
		FileSystemWatcher watcher = new FileSystemWatcher();

		//--- 監視するフォルダー・ファイルを設定
		watcher.Path = Path.GetDirectoryName(m_filePath);
		watcher.Filter = Path.GetFileName(m_filePath);

		//---- ファイルが変更されたときのイベントを設定
		watcher.Changed += (sender, e) =>
		{
			// ファイルが変更されたら、リロード処理を実行
			ReloadCSV(m_filePath);
		};

		// 監視を開始する
		watcher.EnableRaisingEvents = true;
#endif
	}

	/// <summary>
	/// CSVファイルを読み込む
	/// </summary>
	/// <param name="textAsset">テキストデータ</param>
	public void LoadCSV(TextAsset textAsset)
	{
		StringReader reader = new StringReader(textAsset.text);
		ParseCSVData(reader);   // CSVデータをCSVParamDataへ変換
	}

	void ReloadCSV(string filePath)
	{
		StringReader reader = new StringReader(ReadAllText(m_filePath));
		ParseCSVData(reader);   // CSVデータをCSVParamDataへ変換
	}

	void ParseCSVData(StringReader reader)
	{
		//--- データをクリア
		m_csvDatas = new Dictionary<string, CSVParamData>();
		m_csvDatas.Clear();

		//--- 最後まで全て読む
		while (reader.Peek() != -1)
		{
			// 1行ずつ取得
			string line = reader.ReadLine();

			//--- 空行やコメント部分(// コメント内容)を除外
			if (string.IsNullOrWhiteSpace(line))	continue;
			if (line.TrimStart().StartsWith("//"))	continue;

			// 「,」で分割してリスト化
			List<string> str = new List<string>(line.Split(CSV_SPLIT));

			//--- キー(変数名)、変数型、値を文字列で取得
			str.RemoveAt(0);		// 先頭(パラメータの説明部分)を削除
			string key = str[0];
			str.RemoveAt(0);		// キーの部分を削除
			string value = str[0];

			// データを追加
			m_csvDatas.Add(key, new CSVParamData(value));
		}
	}

	static string ReadAllText(string filePath)
	{
		//--- ファイルを読み取り専用かつ共有アクセス権を指定して開く
		using (FileStream fs = new FileStream(filePath, FileMode.Open, FileAccess.Read, FileShare.ReadWrite))
		{
			using (StreamReader sr = new StreamReader(fs))
			{
				return sr.ReadToEnd();
			}
		}
	}
}

public class CSVParamData
{
	const char VECTOR_SPLIT = ' ';

	string m_value;

	public CSVParamData(string value)
	{
		m_value = value;
	}

	public bool TryGetData(out int value)
	{
		return int.TryParse(m_value, out value);
	}

	public bool TryGetData(out float value)
	{
		return float.TryParse(m_value, out value);
	}

	public bool TryGetData(out bool value)
	{
		return bool.TryParse(m_value, out value);
	}

	public bool TryGetData(out string value)
	{
		value = m_value;
		return true;
	}

	public bool TryGetData(out Vector3 value)
	{
		value = new Vector3();

		string[] str = m_value.Split(VECTOR_SPLIT);
		if (str.Length < 3) return false;

		//--- xyzに値を適用していく
		bool isFail = !float.TryParse(str[0], out value.x);
		if (isFail) return false;
		isFail = !float.TryParse(str[1], out value.y);
		if (isFail) return false;
		isFail = !float.TryParse(str[2], out value.z);
		if (isFail) return false;

		return true;
	}
}