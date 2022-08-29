using System.IO;
using UnityEngine;

public static class ArchiveManager
{
	/// <summary>
	/// 读取存档
	/// </summary>
	/// <returns></returns>
	public static void LoadPlayerData(int _saveID = 1)
	{
		if (_saveID > 0 && _saveID <= Const.MaxSaveCount)
		{
			Debug.Log("载入数据");
			string json = null;
			FileStream fs = new FileStream(Const.PLAYER_DATA_PATH(_saveID), FileMode.Open, FileAccess.Read);
			if(fs != null)
			{
				json = new StreamReader(fs).ReadToEnd();
			}
			SerializablePlayerData save = new SerializablePlayerData();
			JsonUtility.FromJsonOverwrite(json, save);
			if(save.Name != null)
			{
				Player.Instance.Initialized(save);
			}
			fs.Close();
			Debug.Log("载入完成");
		}
	}
	/// <summary>
	/// 保存存档
	/// </summary>
	/// <returns></returns>
	public static void SavePlayerData(int _saveID)
	{
		Debug.Log("保存开始");
		SerializablePlayerData save = new SerializablePlayerData(Player.Instance);
		string json = null;
		json = JsonUtility.ToJson(save);
		FileStream fs = new FileStream(Const.PLAYER_DATA_PATH(_saveID), FileMode.OpenOrCreate, FileAccess.Write);
		if (fs != null)
		{
			StreamWriter sw = new StreamWriter(fs);
			sw.Write(json);
			sw.Flush();
			sw.Close();
		}
		fs.Close();
		Debug.Log("保存完成");

	}
	/// <summary>
	/// 获取卡牌Asset
	/// </summary>
	/// <param name="_id">卡牌ID</param>
	/// <returns></returns>
	public static CardSOAsset LoadCardAsset(int _id)
	{
		CardSOAsset asset = null;
		asset = Resources.Load<CardSOAsset>(Const.CARD_DATA_PATH(_id));
		return asset;
	}

	public static void ResetPlayerDataFile()
	{
		Debug.Log("初始化存档");
		SerializablePlayerData prePlayer = new SerializablePlayerData(Const.InitialCode);
		string json = JsonUtility.ToJson(prePlayer);
		for(int i = 1; i <= Const.MaxSaveCount; i++)
		{
			string path = Application.dataPath + "/PlayerDatas/Save" + i.ToString("D2") + ".json";
			FileStream fs = new FileStream(path, FileMode.OpenOrCreate, FileAccess.ReadWrite);
			StreamWriter sw = new StreamWriter(fs);
			sw.Write(json);
			sw.Flush();
			sw.Close();
			fs.Close();
		}
	}
}