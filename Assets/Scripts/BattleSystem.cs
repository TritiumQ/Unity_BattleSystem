using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;
public class BattleSystem : MonoBehaviour
{
    int round;
	Button endButton;

	public GameObject dataManager; //游戏数据管理器

	List<int> deck;  //牌堆
	List<int> hands;  //手牌
	List<Card> handCards;
	List<int> usedCards;  //弃牌堆

	List<SurventUnit> surventUnits;  //玩家随从实例
	List<SurventUnit> enemyUnits;  //敌人随从实例

	public GameObject surventPrefab;
	public GameObject cardPrefab;  

	public GameObject playerHands; //玩家手牌区域
	public GameObject enemyArea;  //敌方随从区域
	public GameObject surventArea;  //玩家随从区域

	public GameObject playerBody;
	public GameObject bossBody;

	public TextMeshProUGUI roundText;

	private void Awake()
	{
		endButton = GameObject.Find("EndButton").GetComponent<Button>();
		endButton.onClick.AddListener(GamePlay);

		roundText.text = round.ToString();
		
		//deck = new List<int>();
		//for test
		deck = new List<int> { 1, 2, 3, 4, 5, -1, -2, -3, -4 ,-5 };

		hands = new List<int>();
		handCards = new List<Card>();
		usedCards = new List<int>();

		surventUnits = new List<SurventUnit>();
		enemyUnits = new List<SurventUnit>();

		//TestLoadData();
		GamePlay();
	}
	void GamePlay()
	{
		RoundPlay();
		round++;
		roundText.text = round.ToString();
	}

	void GetCard()
	{
		if (hands.Count == 10) return;
		if(deck.Count == 0) //牌库空，触发洗牌以及抽空惩罚
		{
			RefreshDeck();
			//TODO 洗牌惩罚 未实现
		}
		int randPos = Random.Range(0, deck.Count);
		hands.Add(deck[randPos]);

		GameObject newCard = Instantiate(cardPrefab,playerHands.transform); //生成预制件实例

		Debug.Log(Const.CARD_DATA_PATH(deck[randPos]));
		Card card = new Card(Resources.Load<CardAsset>(Const.CARD_DATA_PATH(deck[randPos])));  //依据编号，从文件中读取卡牌数据
		
		handCards.Add(card);
		//newCard.GetComponent<CardDisplay>().LoadInf();

		deck.RemoveAt(randPos);
	}
	
	void RefreshDeck()  //刷新牌堆
	{
		Debug.Log("刷新牌堆");
		for (int i = 0; i < usedCards.Count; i++)
		{
			deck.Add(usedCards[i]);
		}
		usedCards.Clear();
	}

	void RoundPlay() //单回合流程
	{
		//1 玩家从牌堆抽牌
		if(round == 0) //首轮抽三张卡
		{
			for(int i = 0; i < 3; i++)
			{
				GetCard();
				
			}
		}
		else //之后每回合开始抽一张卡
		{
			GetCard();
		}
		//2 玩家部署随从/使用法术牌

		//3 玩家随从行动

		//4 Boss行动

		//5 Boss随从行动

		//结束回合


	}
	void TestLoadData()
	{
		Debug.Log("Start Data Test...");

		//dataManager.GetComponent<PlayerDataManager>().player;
	}

}

