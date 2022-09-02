using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

//TODO
//ÉèÎª¾²Ì¬
public static class GameProcessSave 
{ 
    public static string savePath = Application.dataPath + "/GameProcessDatas/DataSave01.json";
    public static bool isLoad = false;
    public static string Lock="Lock";
    private static Player player = Player.Instance;
    public static void GameProcessDataSave(GameManager gameManager, bool isContinue)
    {
        lock (Lock)
        {
            System.IO.File.WriteAllText(savePath, string.Empty);
            SerializableGP gp = new SerializableGP(gameManager, isContinue);
            string json = null;
            json = JsonUtility.ToJson(gp);
            FileStream fs = new FileStream(savePath, FileMode.OpenOrCreate, FileAccess.Write);
                
            if (fs != null)
            {
                StreamWriter sw = new StreamWriter(fs);
                sw.WriteLine(json);
                sw.Flush();
                sw.Close();
            }
            fs.Close();
        }
    }

    public static void GameProcessDataLoad(GameManager gameManager)
    {
        lock (Lock)
        {
            //Debug.Log(1);
            if (gameManager != null)
            {
                //Debug.Log(savePath);
                string json = null;
                FileStream fs = new FileStream(savePath, FileMode.OpenOrCreate, FileAccess.Read);
                if (fs != null)
                {
                    json = new StreamReader(fs).ReadToEnd();
                    //Debug.Log(json);
                    fs.Close();
                }
                SerializableGP gp = new SerializableGP();
                JsonUtility.FromJsonOverwrite(json, gp);
                if (gp != null)
                {
                    if (gp.level == 1 && gp.step == 0)
                        gameManager.InitGameEvent();
                    else gameManager.InitGameEvent(gp);
                }
            }
        }
    }

    public static bool ReadSave()
    {
        lock(Lock)
        {
            string json = null;
            FileStream fs = new FileStream(savePath, FileMode.OpenOrCreate, FileAccess.Read);
            if (fs != null)
            {
                json = new StreamReader(fs).ReadToEnd();
                //Debug.Log(json);
                fs.Close();
            }
            Debug.Log(json);
            SerializableGP gp = new SerializableGP();
            JsonUtility.FromJsonOverwrite(json, gp);
            if (gp.level == 1 && gp.step == 0)
                return false;
            else return true;
        }
    }
    public static void GameSaveSet(int player,GameManager gameManager ,bool process)
    {
        lock (Lock)
        {
            ArchiveManager.SavePlayerData(player);
            GameProcessDataSave(gameManager, process);
        }
    }
}

