using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
public class OpenPackage : MonoBehaviour
{
    public GameObject cardPrefab;
    public GameObject cardPool;

    public List<GameObject> cards = new List<GameObject>();

    public PlayerDataManager playerData;

    CardStore CardStore;

    // Start is called before the first frame update
    void Start()
    {
        CardStore = GetComponent<CardStore>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    
    //开卡包
    public void OnClickOpen() 
    {
        if(playerData.playerCoins < 50)
        {
            return;
        }
        else
        {
            playerData.playerCoins -= 50;
        }

        CleanPool();
        for(int i = 0; i < 5; i++)
        {
            GameObject newCard = GameObject.Instantiate(cardPrefab, cardPool.transform);

            newCard.GetComponent<CardDisplay>().card = CardStore.RandomCard();
            cards.Add(newCard);
        }
        SaveCardData();
        playerData.SavePlayerData();
    }

    //清空
    public void CleanPool()
    {
        foreach(var card in cards)
        {
            Destroy(card);
        }
        cards.Clear();
    }


    public void SaveCardData()
    {
        foreach(var card in cards)
        {
            int id = card.GetComponent<CardDisplay>().card.id;
            if(playerData.playerCards[id] >= 2)
            {
                playerData.playerCoins += 5;
            }
            else
            {
                playerData.playerCards[id] += 1;
            }
        }
    }
}
*/