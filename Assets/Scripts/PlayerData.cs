using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using UnityEngine.UI;

public class PlayerData : MonoBehaviour
{
    public CardStore CardStore;
    public int playerCoin;
    public int[] playerCards;//所有卡牌；
    public int[] playerDeck;
    public TextAsset playerData;
    // Start is called before the first frame update
    void Start()
    {
        LoadPlayerData();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void LoadPlayerData()
    {
        playerCards = new int[CardStore.cardList.Count];
        playerDeck = new int[CardStore.cardList.Count];
        string[] dataRow = playerData.text.Split('\n');
        foreach (var row in dataRow)
        {
            string[] rowArray = row.Split(',');
            if (rowArray[0] == "#")
            {
                continue;
            }
            else if (rowArray[0] == "coins")
            {
                playerCoin = int.Parse(rowArray[1]);
            }
            else if (rowArray[0] == "card")
            {
                int id = int.Parse(rowArray[1]);
                int num = int.Parse(rowArray[2]);
                playerCards[id] = num;
            }
            else if (rowArray[0] == "deck")
            {
                int id = int.Parse(rowArray[1]);
                int num = int.Parse(rowArray[2]);
                playerDeck[id] = num;
            }
        }
    }
    public  void SavePlayerData()
    {
        string path = Application.dataPath + "/Datas/playerdata.scv";//将玩家数据保存到路径上；
        List<string> data = new List<string>();
        data.Add("coins," + playerCoin.ToString());
        for(int i=0;i<playerCards.Length;i++)
        {
            if (playerCards[i] != 0)
            {
                data.Add("card" + i.ToString() + playerCards[i].ToString());
            }
        }
    }
}
