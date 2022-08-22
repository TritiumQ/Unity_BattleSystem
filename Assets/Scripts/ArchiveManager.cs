using System.IO;
using UnityEngine;

public static class ArchiveManager
{

	/// <summary>
	/// ∂¡»°¥Êµµ
	/// </summary>
	/// <returns></returns>
	public static void LoadPlayerData(int _saveID)
	{
		if (_saveID > 0 && _saveID <= Const.MaxSaveCount)
		{
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
				Debug.Log("1");
				Player.Instance.Initialized(save);
			}
		}
	}
	/// <summary>
	/// ±£¥Ê¥Êµµ
	/// </summary>
	/// <returns></returns>
	public static void SavePlayerData(int _saveID)
	{
		SerializablePlayerData save = new SerializablePlayerData(Player.Instance);
		string json = null;
		json = JsonUtility.ToJson(save);
		FileStream fs = new FileStream(Const.PLAYER_DATA_PATH(_saveID), FileMode.OpenOrCreate, FileAccess.Write);
		if(fs != null)
		{
			StreamWriter sw = new StreamWriter(fs);
			sw.Write(json);
			sw.Flush();
			sw.Close();
		}
		fs.Close();
	}

	public static void ResetPlayerDataFile()
	{
		Debug.Log("≥ı ºªØ¥Êµµ");
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