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
	public List<GameObject> PlayerSurventUnits; //玩家随从列表

	[Header("玩家单位")]
	public GameObject playerUnit;
	public PlayerUnitManager playerUnitDisplay;

	//Boss信息区
	BossInBattle boss;
	//int actionCycleFlg = 0;
	public List<GameObject> BossSurventUnits;  //Boss随从列表  //max count = 7
	
	[Header("Boss单位")]
	public GameObject bossUnit;
	public BossUnitManager bossUnitManager;

	//控件
	//bool roundEndFlag = false;  //回合结束标志
	bool playerActionCompleted = false;
	bool getCardCompleted = false;
	bool roundStart;
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
		PlayerSurventUnits = new List<GameObject>(7);
		BossSurventUnits = new List<GameObject>(7);
		playerUnitDisplay = playerUnit.GetComponent<PlayerUnitManager>();
		bossUnitManager = bossUnit.GetComponent<BossUnitManager>();

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
		//1 玩家抽牌
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
		//2 回合开始,重置随从行动状态,触发随从先机效果
		else if(roundStart == false)
		{
			foreach (var obj in PlayerSurventUnits)
			{
				obj.SendMessage("CheckInStart");
			}
			foreach (var obj in BossSurventUnits)
			{
				obj.SendMessage("CheckInStart");
			}
			roundStart = true;
		}
		//3 玩家部署随从/使用法术牌
		//4 玩家行动完成后怪物行动,并结束回合
		else  if (playerActionCompleted)  
		{
			bossUnitManager.Action(round);// Boss行动
			//boss随从行动
			round++;
			roundText.text = round.ToString();

			if(player.MaxActionPoint < 10)
			{
				player.MaxActionPoint++; //每经过一回合,战术点上升1点,最大为10
			}
			player.CurrentActionPoint = player.MaxActionPoint;
			
			playerActionCompleted = false;
			getCardCompleted = false;
			roundStart = true;
			//检查并刷新buff,以及触发随从后手效果
			bossUnitManager.CheckBuff();
			foreach (var obj in PlayerSurventUnits)
			{
				obj.SendMessage("CheckInEnd");
			}
			foreach (var obj in BossSurventUnits)
			{
				obj.SendMessage("CheckInEnd");
			}
			//结束回合
		}
		
	}
	public void UseCardByPlayer(GameObject _cardObject)  //使用卡牌
	{
		Card _card = _cardObject.GetComponent<CardDisplay>().card;
		if(player.CurrentActionPoint - _card.cost >= 0)
		{
			if (_card.cardType == CardType.Spell)
			{
				//使用法术卡
			}
			else
			{
				if ((_card.cardType == CardType.Survent && PlayerSurventUnits.Count < 7))
				{
					GameObject newSurvent = Instantiate(surventPrefab, surventArea.transform);
					newSurvent.GetComponent<SurventUnitManager>().Initialized(_card);
					PlayerSurventUnits.Add(newSurvent);

					player.CurrentActionPoint -= _card.cost;
					handCards.Remove(_cardObject);
					//usedCards.Add(_card.cardID);
					Destroy(_cardObject);
				}
			}
		}
		else 
		{
			Debug.Log("战术点不足");
		}
	}
	public void SetupSurventByBoss(Card _card)
	{
		if(_card.cardType == CardType.Monster && BossSurventUnits.Count < 7)
		{
			GameObject newEnemy = Instantiate(surventPrefab, enemyArea.transform);
			newEnemy.GetComponent<SurventUnitManager>().Initialized(_card);
			BossSurventUnits.Add(newEnemy);
		}
	}
	//
	public void PlayerSurventDie(GameObject _obj)
	{
		PlayerSurventUnits.Remove(_obj);
	}
	public void BossSurventDie(GameObject _obj)
	{
		BossSurventUnits.Remove(_obj);
	}
	//一套随从攻击方法
	GameObject attacker;
	GameObject victim;
	//1 随从发起攻击请求
	public void AttackRequest(GameObject _request)
	{
		attacker = _request;
	}
	//2 目标确认受击
	public void AttackConfirm(GameObject _confirm)
	{
		if(attacker != null)
		{
			victim = _confirm;
			int damage = attacker.GetComponent<SurventUnitManager>().GetInf(GetSurventInfomation.ATK);
			victim.SendMessage("BeAttacked", damage);
			attacker.GetComponent<SurventUnitManager>().isActive = false;
			victim = null;
			attacker = null;
		}
	}
	//2 或取消攻击
	public void AttackCancel()
	{
		attacker = null;
		victim = null;
	}
	//
	public void Vectory()
	{
		Debug.Log("好耶~！");
		//
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
		bossUnitManager.boss = boss;

		Debug.Log("测试载入数据完成");
	}

}

