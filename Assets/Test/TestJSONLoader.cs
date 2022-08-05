using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class TestJSONLoader
{

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
			JsonUtility.FromJsonOverwrite(json, Player.Instance);
			
		}
	}

	public static void SavePlayerData()
	{

	}
	public static void ResetPlayerDataFile()
	{
		DefaultPlayer prePlayer = new DefaultPlayer();
		string json = JsonUtility.ToJson(prePlayer);
		for(int i = 1; i <= 10; i++)
		{
			string path = Application.dataPath + "/PlayerDatas/" + i.ToString("D2") + ".json";
			FileStream fs = new FileStream(path, FileMode.OpenOrCreate, FileAccess.ReadWrite);
			StreamWriter sw = new StreamWriter(fs);
			sw.Write(json);
			sw.Flush();
			sw.Close();
			fs.Close();
		}
	}
}