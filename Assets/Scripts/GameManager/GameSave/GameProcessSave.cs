using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

//TODO
//ÉèÎª¾²Ì¬
public class GameProcessSave : MonoBehaviour
{
    public GameManager gameManager;
    protected string savePath;
    public string Lock;
    private void Awake()
    {
        gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
        savePath = UnityEngine.Application.dataPath + "/GameProcessDatas/DataSave01";
        Lock = "Lock";
    }
    private void Update()
    {
        if (Time.frameCount % 6 == 0)
        {
            if (gameManager != null)
            {
                //GameProcessDataSave(true);
            }
            else
                gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
        }
    }

    public void GameProcessDataSave(bool isContinue)
    {
        lock (Lock)
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
        }
    }

    public void GameProcessDataLoad()
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
    public void GameSaveSet(int player, bool process)
    {
        ArchiveManager.SavePlayerData(player);
        GameProcessDataSave(process);
    }
}

