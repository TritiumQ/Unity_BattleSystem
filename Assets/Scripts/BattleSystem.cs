using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;
public class BattleSystem : MonoBehaviour
{

	//玩家信息区
	PlayerInBattle player;
	int playerMaxHP;
	int playerCurrentHP;
	int playerCurrentActionPoint;
	int playerMaxActionPoint;
	List<int> deck;  //牌堆
	List<GameObject> handCards;  //手牌  //max count = 10
	List<int> usedCards;  //弃牌堆
	List<GameObject> surventUnits; //玩家随从

	[Header("玩家单位")]
	public GameObject playerUnit;
	PlayerUnitDisplay playerUnitDisplay;
	//Boss信息区
	BossInBattle boss;
	//int actionCycleFlg = 0;
	List<GameObject> enemyUnits;  //Boss随从  //max count = 7
	
	[Header("Boss单位")]
	public GameObject bossUnit;
	BossUnitDisplay bossUnitDisplay;

	//控件
	//bool roundEndFlag = false;  //回合结束标志
	bool playerActionCompleted = false;
	bool getCardCompleted = false;
	int round;

	[Header("回合数")]
	public TextMeshProUGUI roundText;
	[Header("结束回合按钮")]
	public Button endButton;

	[Header("玩家手牌区")]
	public GameObject playerHands;
	[Header("玩家随从区域")]
	public GameObject surventArea;
	[Header("敌方随从区")]
	public GameObject enemyArea;
	[Header("随从预制体")]
	public GameObject surventPrefab;
	[Header("卡牌预制体")]
	public GameObject cardPrefab;  
	private void Update()
	{
		GamePlay();
		Refresh();
	}
	private void Awake()
	{
		//初始化实例
		endButton.onClick.AddListener(EndRound);
		roundText.text = round.ToString();
		//deck = new List<int>();
		deck = new List<int> { 1, 2, 3, 4, 5, -1, -2, -3, -4 ,-5 };
		handCards = new List<GameObject>(10);
		usedCards = new List<int>();
		surventUnits = new List<GameObject>(7);
		enemyUnits = new List<GameObject>(7);
		playerUnitDisplay = playerUnit.GetComponent<PlayerUnitDisplay>();
		bossUnitDisplay = bossUnit.GetComponent<BossUnitDisplay>();
		//
		TestSetData();

		//初始化显示
		Refresh();
	}

	void GetCard(int _count)
	{
		for(int i = 0; i < _count; i++)
		{
			if (handCards.Count == 10) return;
			if (deck.Count == 0) //牌库空，触发洗牌以及抽空惩罚
			{
				RefreshDeck();
				//TODO 洗牌惩罚 未实现
			}
			int randPos = Random.Range(0, deck.Count);

			GameObject newCard = Instantiate(cardPrefab, playerHands.transform); //生成预制件实例

			Debug.Log(Const.CARD_DATA_PATH(deck[randPos]));
			Card card = new Card(Resources.Load<CardSOAsset>(Const.CARD_DATA_PATH(deck[randPos])));  //依据编号，从文件中读取卡牌数据

			newCard.GetComponent<CardDisplay>().card = card;
			newCard.GetComponent<CardDisplay>().LoadInf();


			handCards.Add(newCard);
			//newCard.GetComponent<CardDisplay>().LoadInf();

			deck.RemoveAt(randPos);
		}
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
	void EndRound()
	{
		//roundEndFlag = true;
		playerActionCompleted = true;
	}
	void GamePlay()
	{
		//1 抽牌
		if (getCardCompleted == false)
		{
			if (round == 0)
			{
				GetCard(3);
			}
			else
			{
				GetCard(1);
			}
			getCardCompleted = true;
		}
		//2 玩家部署随从/使用法术牌
		//3 玩家随从行动
		if (playerActionCompleted)  //玩家行动完成后怪物行动,并结束回合
		{
			BossAction();       //4 Boss及其随从行动
			
			round++;
			roundText.text = round.ToString();

			if(playerMaxActionPoint < 10)
			{
				playerMaxActionPoint++; //每经过一回合,战术点上升1点,最大为10
			}
			playerCurrentActionPoint = playerMaxActionPoint;
			
			playerActionCompleted = false;
			getCardCompleted = false;
		}
		else
		{
			//检测玩家行动


		}
		//结束回合
	}
	void BossAction()  //Boss行动
	{
		//int flag = round % boss.actionCycle.Count;
		//BossActionType action = boss.actionCycle[flag];
		//TODO boss行动
		Debug.Log("Boss行动");
	}
	void Refresh()
	{
		playerUnitDisplay.Refresh(playerCurrentActionPoint,playerCurrentHP);
	}
	//相关信息载入方法
	public void GetBossInf(BossInBattle _boss)
	{
		boss = _boss;
	}
	public void GetPlayerInf(PlayerBattleInformation _info)
	{
		playerMaxHP = _info.maxHP;
		playerCurrentHP = _info.currentHP;
		deck = _info.cardSet;
	}
	void TestSetData()
	{
		Debug.Log("Start Data Setting...");
		playerMaxHP = 10;
		playerCurrentHP = 10;
		playerCurrentActionPoint = playerMaxActionPoint = 1;
		playerUnitDisplay.Refresh(playerCurrentActionPoint, playerCurrentHP);

		BossInBattle bs = new BossInBattle(Resources.Load<BossSOAsset>(Const.BOSS_DATA_PATH(1)));
		bossUnitDisplay.boss = bs;
	}

}

