using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CardStore : MonoBehaviour
{
    public TextAsset cardData;

    public List<Card> cardList = new List<Card>(); //卡牌链表

    // Start is called before the first frame update
    void Start()
    {
        //LoadCardData();
        //TestLoad();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    /*
    public void LoadCardData()
    {
        string[] dataRow = cardData.text.Split("\n");
        foreach(var row in dataRow)
        {
            string[] rowArray = row.Split(',');
            if (rowArray[0] == "#") continue;
            else if(rowArray[0] == "monster")
            {
                //新建随从卡
                int id = int.Parse(rowArray[1]);
                string name = rowArray[2];
                int cost = int.Parse(rowArray[3]);
                int atk = int.Parse(rowArray[4]);
                int hp = int.Parse(rowArray[5]);
                string race = rowArray[6];
                string skill = rowArray[7];
                cardList.Add(new MonsterCard(id, name, cost, atk, hp, race, skill));

                //Debug.Log("读取到随从卡：" + name);
            }
            else if(rowArray[0] == "spell")
            {
                //新建法术卡
                int id = int.Parse(rowArray[1]);
                string name = rowArray[2];
                int cost = int.Parse(rowArray[3]);
                string race = rowArray[4];
                string effect = rowArray[5];
                cardList.Add(new SpellCard(id, name, cost, race, effect));

                //Debug.Log("读取到法术卡：" + name);
            }
        }
    }

    public void TestLoad()
    {
        foreach(var item in cardList)
        {
            Debug.Log("载入卡牌："+item.cardName);
        }
    }

    public Card RandomCard() //抽卡随机
    {
        Card card = cardList[Random.Range(0, cardList.Count)];
        return card;
    }
    */
}
