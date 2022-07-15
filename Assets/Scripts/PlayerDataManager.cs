using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
public class PlayerDataManager : MonoBehaviour
{

}

/*
public class PlayerDataManager : MonoBehaviour
{
    public CardStore cardStore;
    public int playerCoins; //玩家金币数
    public int[] playerCards; //玩家牌库

    public TextAsset playerData;

    // Start is called before the first frame update
    void Start()
    {
        cardStore.LoadCardData();
        LoadPlayerData();
    }

    // Update is called once per frame
    void Update()
    {

    }
    
    //加载玩家数据
    public void LoadPlayerData()
    {
        playerCards = new int[cardStore.cardList.Count];
        string[] dataRow = playerData.text.Split('\n');
        foreach (var row in dataRow)
        {
            string[] rowArray = row.Split(',');
            if (rowArray[0] == "#") continue;
            else if (rowArray[0] == "coins")
            {
                playerCoins = int.Parse(rowArray[1]);
            }
            else if (rowArray[0] == "card")
            {
                //载入玩家卡牌库
                playerCards[int.Parse(rowArray[1])] = int.Parse(rowArray[2]);
            }
            else if (rowArray[0] == "deck")
            {

            }
        }
    }
    
    //保存玩家数据
    public void SavePlayerData()
    {
        string path = Application.dataPath + "/Datas/playerData.csv";

        //保存卡牌库
        List<string> datas = new List<string>();
        datas.Add("coins," + playerCoins.ToString());
        for(int i = 0; i < playerCards.Length; i++)
        {
            if (playerCards[i] != 0)
            {
                datas.Add("card," + i.ToString() + "," + playerCards[i].ToString());
            }
        }

        //保存卡组

        //保存数据
        File.WriteAllLines(path, datas);
    }
}*/
