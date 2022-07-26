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

	List<int> deck;  //牌堆
	List<GameObject> handCards;  //手牌堆  //max count = 10
	//List<int> usedCards;  //弃牌堆
	int usedFlag;
	List<GameObject> surventUnits; //玩家随从列表

	[Header("玩家单位")]
	public GameObject playerUnit;
	PlayerUnitManager playerUnitDisplay;

	//Boss信息区
	BossInBattle boss;
	//int actionCycleFlg = 0;
	List<GameObject> enemyUnits;  //Boss随从列表  //max count = 7
	
	[Header("Boss单位")]
	public GameObject bossUnit;
	BossUnitManager bossUnitDisplay;

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
	}
	private void Awake()
	{
		//初始化实例
		endButton.onClick.AddListener(EndRound);
		roundText.text = round.ToString();
		//deck = new List<int>();
		deck = new List<int> { 1, 2, 3, 4, 5, -1, -2, -3, -4 ,-5 };

		handCards = new List<GameObject>(10);
		//usedCards = new List<int>();
		usedFlag = 0;
		surventUnits = new List<GameObject>(7);
		enemyUnits = new List<GameObject>(7);
		playerUnitDisplay = playerUnit.GetComponent<PlayerUnitManager>();
		bossUnitDisplay = bossUnit.GetComponent<BossUnitManager>();

		//
		TestSetData();

		//
	}

	void GetCard(int _count)
	{
		for(int i = 0; i < _count; i++)
		{
			if (handCards.Count == 10) return;
			if (usedFlag == deck.Count) //牌库空，触发洗牌以及抽空惩罚
			{
				RefreshDeck();
				//TODO 洗牌惩罚 未实现
			}
			int randPos = Random.Range(0, deck.Count); //随机抽牌

			GameObject newCard = Instantiate(cardPrefab, playerHands.transform); //生成预制件实例

			//Debug.Log(Const.CARD_DATA_PATH(deck[randPos]));
			//依据编号，从文件中读取卡牌数据
			//Card card = new Card(Resources.Load<CardSOAsset>(Const.CARD_DATA_PATH(deck[randPos])));
			Card card = new Card(Resources.Load<CardSOAsset>(Const.CARD_DATA_PATH(deck[usedFlag])));
			usedFlag++;

			newCard.GetComponent<CardDisplay>().card = card;
			newCard.GetComponent<CardDisplay>().LoadInf();


			handCards.Add(newCard);
			//newCard.GetComponent<CardDisplay>().LoadInf();

			//deck.RemoveAt(randPos);
		}
	}
	void RefreshDeck()  //洗牌刷新牌堆
	{
		Debug.Log("刷新牌堆");
		usedFlag = 0;
		for(int i = 0; i < deck.Count; i++)
		{
			var rnd = Random.Range(0, deck.Count);
			var swap = deck[rnd];
			deck[rnd] = deck[i];
			deck[i] = swap;
		}
		
		/* //旧刷新方法，已弃用
		for (int i = 0; i < usedCards.Count; i++)
		{
			deck.Add(usedCards[i]);
		}
		usedCards.Clear();
		*/
	}
	void EndRound()
	{
		//roundEndFlag = true;
		playerActionCompleted = true;
	}
	void GamePlay()
	{
		// 抽牌
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
		//玩家部署随从/使用法术牌
		// 玩家随从行动
		/*
		if(player.CurrentActionPoint == 0) //战术点用完时自动进入随从行动 (实现有困难，暂定废除)
		{
			Debug.Log("随从行动");
			//
		}
		*/
		if (playerActionCompleted)  //玩家行动完成后怪物行动,并结束回合
		{
			BossAction();       // Boss及其随从行动
			
			round++;
			roundText.text = round.ToString();

			if(player.MaxActionPoint < 10)
			{
				player.MaxActionPoint++; //每经过一回合,战术点上升1点,最大为10
			}
			player.CurrentActionPoint = player.MaxActionPoint;
			
			playerActionCompleted = false;
			getCardCompleted = false;

			//检查并刷新buff
			bossUnitDisplay.CheckBuff();
			foreach (var obj in surventUnits)
			{
				obj.GetComponent<SurventUnitManager>().CheckBuff();
			}
			foreach (var obj in enemyUnits)
			{
				obj.GetComponent<SurventUnitManager>().CheckBuff();
			}
			//结束回合
		}
		
	}
	public void UseCard(GameObject _cardObject)  //使用卡牌
	{
		Card _card = _cardObject.GetComponent<CardDisplay>().card;
		if(player.CurrentActionPoint - _card.cost >= 0)
		{
			if (_card.cardType == CardType.Spell)
			{
				SpellTrigger(_card);
			}
			else
			{
				if ((_card.cardType == CardType.Survent && surventUnits.Count < 7)
					|| (_card.cardType == CardType.Monster && enemyUnits.Count < 7))
				{
					SurventSetup(_card);
				}
				else return;
			}
			player.CurrentActionPoint -= _card.cost;
			handCards.Remove(_cardObject);
			//usedCards.Add(_card.cardID);
			Destroy(_cardObject);
		}
		else 
		{
			Debug.Log("战士点不足");
		}
	}
	void SpellTrigger(Card _card)
	{
		Debug.Log("使用法术卡");
		//
	}
	void SurventSetup(Card _card)
	{
		Debug.Log("使用随从卡");
		if (_card.cardType == CardType.Monster)
		{
			if(enemyUnits.Count < 7)
			{
				GameObject newEnemy = GameObject.Instantiate(surventPrefab, enemyArea.transform);
				newEnemy.GetComponent<SurventUnitManager>().Initial(_card);
				enemyUnits.Add(newEnemy);

			}
		}
		else
		{
			if(surventUnits.Count < 7)
			{
				GameObject newSurvent = Instantiate(surventPrefab, surventArea.transform);
				newSurvent.GetComponent<SurventUnitManager>().Initial(_card);
				surventUnits.Add(newSurvent);

			}
		}
	}
	void BossAction()  //TODO Boss行动
	{
		//int flag = round % boss.actionCycle.Count;
		//BossActionType action = boss.actionCycle[flag];
		//TODO boss行动
		Debug.Log("Boss行动");
	}
	//相关信息载入方法
	public void SetBossInf(BossInBattle _boss)
	{
		boss = _boss;
	}
	public void SetPlayerInf(PlayerBattleInformation _info)
	{
		player.MaxHP = _info.maxHP;
		player.CurrentHP = _info.currentHP;
		deck = _info.cardSet;
	}
	void TestSetData() //测试载入数据
	{
		Debug.Log("Start Data Setting...");
		player = new PlayerInBattle(20, 10, 1, 1);
		playerUnitDisplay.player = player;

		boss = new BossInBattle(Resources.Load<BossSOAsset>(Const.BOSS_DATA_PATH(1)));
		bossUnitDisplay.boss = boss;

		Debug.Log("测试载入数据完成");
	}

}

