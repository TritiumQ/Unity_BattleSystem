using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

//TODO
//ÉèÎª¾²Ì¬
public static class GameProcessSave 
{ 
    public static string savePath= UnityEngine.Application.dataPath + "/GameProcessDatas/DataSave01";
    public static bool isLoad = false;
    public static string Lock="Lock";    

    public static void GameProcessDataSave(GameManager gameManager, bool isContinue)
    {
        lock (Lock)
        {
            if (!isLoad)
            {
                SerializableGP gp = new SerializableGP(gameManager, isContinue);
                string json = null;
                json = JsonUtility.ToJson(gp);
                FileStream fs = new FileStream(savePath, FileMode.OpenOrCreate, FileAccess.ReadWrite);
                if (fs != null)
                {
                    StreamWriter sw = new StreamWriter(fs);
                    //Debug.Log(json);
                    sw.Write(json);
                    sw.Flush();
                    sw.Close();
                }
                fs.Close();
                isLoad = true;
            }
        }
    }

    public static void GameProcessDataLoad(GameManager gameManager)
    {
        lock (Lock)
        {
            //Debug.Log(1);
            if (gameManager != null)
            {
                Debug.Log(savePath);
                string json = null;
                FileStream fs = new FileStream(savePath, FileMode.OpenOrCreate, FileAccess.Read);
                if (fs != null)
                {
                    json = new StreamReader(fs).ReadToEnd();
                    Debug.Log(json);
                    fs.Close();
                }
                SerializableGP gp = new SerializableGP();
                JsonUtility.FromJsonOverwrite(json, gp);
                if (gp != null)
                {
                    Debug.Log(json);
                    gameManager.InitGameEvent(gp);
                }
            }
        }
    }
    public static void GameSaveSet(int player,GameManager gameManager ,bool process)
    {
        ArchiveManager.SavePlayerData(player);
        GameProcessDataSave(gameManager,process);
    }
}

