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


		roundText.text = round.ToString();
		
		deck = new List<int>();
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
		for(; ; )
		{
			RoundPlay();
			round++;
			roundText.text = round.ToString();
		}
	}

	void GetCard()
	{
		if(deck.Count == 0) //牌库空，触发洗牌以及抽空惩罚
		{
			RefreshDeck();
			//punish 未实现
		}
		int rand = Random.Range(0, deck.Count);
		hands.Add(deck[rand]);
		deck.RemoveAt(rand);
	}
	void ShowHandCard() // 展示所抽的的牌
	{
		GameObject newCard = Instantiate(cardPrefab,playerHands.transform);

	}
	void RefreshDeck()  //刷新牌堆
	{
		for(int i = 0; i < usedCards.Count; i++)
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

