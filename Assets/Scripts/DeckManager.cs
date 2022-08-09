using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
public class DeckManager : MonoBehaviour
{
    public Transform deckPanel;//对应卡组context位置；
    public Transform libraryPanel;//对应卡库context位置；
    public GameObject deckPrefab;//卡组卡牌预制体；
    public GameObject cardPrefab;//仓库卡牌预制体；
    public GameObject DataManager;//挂载的预制体；
    //private PlayerData playerData;//玩家数据；

    //private CardStore CardStore;//仓库卡牌；

    private List<GameObject> cardPool = new List<GameObject>();
    private Dictionary<int, GameObject> libraryDic = new Dictionary<int, GameObject>();//记录卡库卡牌预制体与卡牌id信息；
    private Dictionary<int, GameObject> deckDic = new Dictionary<int, GameObject>();//记录卡组卡牌与卡牌id信息；
    // Start is called before the first frame update
    void Start()
    {
        //playerData = DataManager.GetComponent<PlayerData>();
        //CardStore = playerData.GetComponent<CardStore>();
        UpdateLibrary();
    }

    // Update is called once per frame
    void Update()
    {
       
    }

    //将卡库卡牌展现出来；
    public void UpdateLibrary()
    {
        /*
        for (int i = 0; i < playerData.playerCards.Length; i++)
		{
            if (playerData.playerDeck[i] != 0)
            {
                CreatCard(i, CardState.Library);
            }
        }
        */
    }
    /*
    public void UpdataDeck()
    {
       for(int i=0;i<playerData.playerDeck.Length;i++)
        {
            if(playerData.playerDeck.Length!=0)
            {
                CreatCard(i, CardState.Deck);
            }
        }
    }*/
    public void UpdataCard(CardState _state,int _id)
    {
        /*
        //若是点击了卡组牌，卡组牌减1回到卡库；
        if (_state == CardState.Deck)
        {
           
            playerData.playerDeck[_id]--;
            playerData.playerCards[_id]++;
            CreatCard(_id, CardState.Library);//卡牌回到卡库；

            // 如果卡组区某张卡牌数为零则摧毁预制体
            if (playerData.playerDeck[_id] == 0)
            {
                Destroy(deckDic[_id]);
                return;
            }
            //改变卡组上的该卡牌数量；
            TextMeshProUGUI Text = deckDic[_id].transform.Find("Number").GetComponent<TextMeshProUGUI>();//找到卡组上卡牌的数量信息；
            int count = int.Parse(Text.text);
            count--;
            Text.text = count.ToString();//类型string转int再转回string显示；
           

        }
        // //若是点击了卡库牌，卡库牌转移到卡组；
        else if (_state==CardState.Library)
        {
            //卡组中该张牌数量等于二则无法添加；
            if(playerData.playerDeck[_id] == 2)
            {
                return;
            }
            playerData.playerCards[_id]--;
            Destroy(libraryDic[_id]);//卡库摧毁预制体；

            //如果卡组中还没有此卡则创建；
            if (playerData.playerDeck[_id]==0)
            {
                CreatCard(_id, CardState.Deck);
            }
            playerData.playerDeck[_id]++;
            // //改变卡组上的该卡牌数量；
            TextMeshProUGUI Text = deckDic[_id].transform.Find("Number").GetComponent<TextMeshProUGUI>();//找到卡组上该卡牌的；
            int count = int.Parse(Text.text);
            count++;
            Text.text = count.ToString();//类型string转int再转回string显示；
           
        }
        */
    }
   public void CreatCard(int _id,CardState _cardState)
    {
        /*
        Transform targetPanel= libraryPanel;
        GameObject targetPrefab = cardPrefab;
        var reData = playerData.playerCards;
        if(_cardState==CardState.Library)
        {
            targetPanel = libraryPanel;
            targetPrefab = cardPrefab;

        }
        else if(_cardState==CardState.Deck)
        {
            targetPanel = deckPanel;
            targetPrefab = deckPrefab;
            reData = playerData.playerDeck;
        }
        GameObject newCard = Instantiate(targetPrefab, targetPanel);
        //TODO
        //newCard.GetComponent<CardDisplay>().card = CardStore.cardList[_id];
        deckDic.Add(_id, newCard);
        */
    }
}
   
