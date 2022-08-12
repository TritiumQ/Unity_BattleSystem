using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;
public class BattleSystem : MonoBehaviour
{
	[Header("玩家单位")]
	public GameObject playerUnit;
	//玩家信息区
	PlayerInBattle player;
	List<int> deck;  //牌堆
	List<GameObject> handCards;  //手牌堆  //max count = 10
	//List<int> usedCards;  //弃牌堆
	int cardUsedFlag;
	public List<GameObject> PlayerSurventUnitsList { get; private set; } //玩家随从列表


	[Header("Boss单位")]
	public GameObject bossUnit;
	public BossUnitManager bossUnitManager;
	public List<GameObject> BossSurventUnitsList { get; private set; } //Boss随从列表  //max count = 7

	//控件
	//bool roundEndFlag = false;  //回合结束标志
	bool playerActionCompleted = false;
	//bool getCardCompleted = false;
	bool roundStart = false;
	int round;

	//其他
	

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
	public GameObject enemyPrefab;
	[Header("卡牌预制体")]
	public GameObject cardPrefab;  
	private void Update()
	{
		GamePlay();
	}
	private void Awake()
	{
		vectory.SetActive(false);
		//初始化实例
		endButton.onClick.AddListener(EndRound);
		roundText.text = round.ToString();
		//deck = new List<int>();
		deck = new List<int> { 1, 2, 3, 4, 5, -1, -2, -3, -4 ,-5 };

		handCards = new List<GameObject>(10);
		//usedCards = new List<int>();
		cardUsedFlag = 0;
		PlayerSurventUnitsList = new List<GameObject>(7);
		BossSurventUnitsList = new List<GameObject>(7);

		//
		TestSetData();

		//
	}
	void GetCard(int _count)
	{
		for(int i = 0; i < _count; i++)
		{
			if (handCards.Count == 10) return;
			if (cardUsedFlag == deck.Count) //牌库空，触发洗牌以及抽空惩罚
			{
				RefreshDeck();

				//洗牌惩罚,扣玩家5点血
				Effect.Set(playerUnit, EffectType.Attack, 5);
			}
			int randPos = Random.Range(0, deck.Count); //随机抽牌

			GameObject newCard = Instantiate(cardPrefab, playerHands.transform); //生成预制件实例

			//Debug.Log(Const.CARD_DATA_PATH(deck[randPos]));
			//依据编号，从文件中读取卡牌数据
			//Card card = new Card(Resources.Load<CardSOAsset>(Const.CARD_DATA_PATH(deck[randPos])));
			CardSOAsset card = Resources.Load<CardSOAsset>(Const.CARD_DATA_PATH(deck[cardUsedFlag]));
			cardUsedFlag++;

			newCard.GetComponent<CardDisplay>().Initialized(card);
			newCard.GetComponent<CardDisplay>().LoadInf();


			handCards.Add(newCard);
			//newCard.GetComponent<CardDisplay>().LoadInf();

			//deck.RemoveAt(randPos);
		}
	}
	void RefreshDeck()  //洗牌刷新牌堆
	{
		Debug.Log("刷新牌堆");
		cardUsedFlag = 0;
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
		//1 玩家抽牌,回合开始,重置随从行动状态,触发随从先机效果
		if (roundStart == false)
		{
			if(round == 0)
			{
				GetCard(3);
			}
			else
			{
				GetCard(1);
			}
			Debug.Log("回合开始");
			foreach (var obj in PlayerSurventUnitsList)
			{
				obj.SendMessage("CheckInStart");
			}
			foreach (var obj in BossSurventUnitsList)
			{
				obj.SendMessage("CheckInStart");
			}
			roundStart = true;
		}
		//2 玩家部署随从/使用法术牌
		//3 玩家行动完成后怪物行动,并结束回合
		else  if (playerActionCompleted)  
		{
			// Boss行动
			

			//boss随从行动
			foreach(var obj in BossSurventUnitsList)
			{

				obj.SendMessage("Action");
			}
			round++;
			roundText.text = round.ToString();

			if(player.MaxActionPoint < 10)
			{
				player.MaxActionPoint++; //每经过一回合,战术点上升1点,最大为10
			}
			player.CurrentActionPoint = player.MaxActionPoint;
			
			playerActionCompleted = false;
			roundStart = false;
			//检查并刷新buff,以及触发随从后手效果
			//bossUnitManager.CheckBuff();
			foreach (var obj in PlayerSurventUnitsList)
			{
				obj.SendMessage("CheckInEnd");
			}
			foreach (var obj in BossSurventUnitsList)
			{
				obj.SendMessage("CheckInEnd");
			}
			//结束回合
		}
		
	}
	public void UseCardByPlayer(GameObject _cardObject)  //使用卡牌
	{
		CardSOAsset _card = _cardObject.GetComponent<CardDisplay>().card;
		if(player.CurrentActionPoint - _card.Cost >= 0)
		{
			if (_card.CardType == CardType.Spell)
			{
				//使用法术卡
				//AttackRequest(_cardObject, EffectType.SpellAttack, _cardObject.transform.position);
			}
			else
			{
				if ((_card.CardType == CardType.Survent && PlayerSurventUnitsList.Count < 7))
				{
					GameObject newSurvent = Instantiate(surventPrefab, surventArea.transform);
					newSurvent.GetComponent<SurventUnitManager>().Initialized(_card);
					PlayerSurventUnitsList.Add(newSurvent);
					//触发放置效果
					

					player.CurrentActionPoint -= _card.Cost;
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
	public void SetupBossSurvent(CardSOAsset _card)
	{
		if(_card.CardType == CardType.Monster && BossSurventUnitsList.Count < 7)
		{
			GameObject newEnemy = Instantiate(enemyPrefab, enemyArea.transform);
			newEnemy.GetComponent<SurventUnitManager>().Initialized(_card);
			BossSurventUnitsList.Add(newEnemy);
		}
	}

	public void SurventUnitDie(GameObject unitObject)
	{
		if(PlayerSurventUnitsList.Contains(unitObject))
		{
			PlayerSurventUnitsList.Remove(unitObject);
		}
		else if(BossSurventUnitsList.Contains(unitObject))
		{
			BossSurventUnitsList.Remove(unitObject);
		}
	}

	#region 新版-效果的释放和接收调度函数
	public GameObject EffectInitiator { get; private set; }
	public GameObject EffectTarget { get; private set; }
	public EffectPackage Package { get; private set; }
	public void ApplyEffectTo(GameObject _target, GameObject _initiator, EffectPackage _effect)
	{
		if (_target != null && _initiator != null && _effect != null)
		{
			object[] ParameterList = { _initiator, _effect };
			_target.SendMessage("AcceptEffect", ParameterList);
		}
	}
	public void EffectSetupRequest(GameObject initiator, EffectPackage package)
	{
		EffectInitiator = initiator;
		Package = package;
	}
	public void EffectConfirm(GameObject target)
	{
		EffectTarget = target;
		if (EffectInitiator != EffectTarget && EffectInitiator != null && EffectTarget != null)
		{
			ApplyEffectTo(EffectTarget, EffectInitiator, Package);
		}
		EffectSetupOver();
	}
	public void EffectSetupOver()
	{
		EffectInitiator = null;
		EffectTarget = null;
		Package = null;
	}
	/// <summary>
	/// 对特定单个目标直接释放效果
	/// </summary>
	/// <param name="initiator">效果发起者</param>
	/// <param name="Target">效果目标</param>
	/// <param name="package">效果信息</param>
	public void EffectDirectSetup(GameObject initiator, GameObject Target, EffectPackage package)
	{
		//TODO 

	}
	/// <summary>
	/// 直接释放效果，目标信息由效果包确定
	/// </summary>
	/// <param name="initiator">效果发起者</param>
	/// <param name="package">效果信息包（有目标信息）</param>
	public void EffectDirectSetup(GameObject initiator, EffectPackageWithTargetOption package)
	{
		//TODO

	}
	#endregion

	#region 旧版-效果的释放和接收调度函数
	//一套攻击方法
	public GameObject attacker { get; private set; }
	public CardType attackerType { get; private set; }
	public TargetOptions attackTarget { get; private set; }
	public EffectType actionType { get; private set; }
	public EffectPackage effect { get; private set; }
	public GameObject victim { get; private set; }

	public GameObject arrowPrefab;
	public GameObject arrow; //攻击箭头
	public Transform canvas;
	//1 攻击请求
	public void AttackRequest(GameObject _request, CardType _atkerType , TargetOptions _target,EffectType _action , Vector2 _startPoint)
	{
		if(arrow == null)
		{
			Debug.Log("攻击请求");
			arrow = GameObject.Instantiate(arrowPrefab, canvas);
			arrow.GetComponent<TestArrow>().SetStartPoint(_startPoint);
			attacker = _request;
			attackerType = _atkerType;
			attackTarget = _target;
			actionType = _action;
		}
	}
	//2 目标确认受击
	public void AttackConfirm(GameObject _confirm)
	{
		if(attacker != null)
		{
			victim = _confirm;
			//检查是否误攻击
			if(attackTarget == TargetOptions.SinglePlayerTarget)
			{

			}
			else if(attackTarget == TargetOptions.SingleEnemyTarget)
			{

			}
			else
			{
				AttackOver();
			}
			//TODO 检查敌方是否有嘲讽或隐匿对象
			if (attackerType == CardType.Survent)
			{	

				//Debug.Log("攻击成功");
				attacker.GetComponent<SurventUnitManager>().isActive = false;
				Effect.Set(victim, EffectType.Attack, attacker.GetComponent<SurventUnitManager>().survent.ATK);
			}
			else if(attackerType == CardType.Spell)
			{
				CardSOAsset spellCard = attacker.GetComponent<CardDisplay>().card;
				//Effect.Set(victim, spellCard.SpellActionType, spellCard.SpellActionValue1, spellCard.SpellActionValue2);
				player.CurrentActionPoint -= spellCard.Cost;
				handCards.Remove(attacker);
				Destroy(attacker);
			}

			AttackOver();
		}
	}
	//结束攻击
	public void AttackOver()
	{
		Debug.Log("Cancel");
		if(arrow != null)
		{
			Destroy(arrow);
		}
		attacker = null;
		victim = null;
	}
	#endregion

	//胜利
	public GameObject vectory;

	public void GameEnd(GameResult result)
	{
		switch (result)
		{
			case GameResult.Success:
				{
					Debug.Log("游戏胜利");
				}
				break;
			case GameResult.Failure:
				{
					Debug.Log("游戏失败");
				}
				break;
			case GameResult.Escape:
				{
					Debug.Log("临阵脱逃");
				}
				break;
			default:
				break;
		}
	}

	//相关信息载入方法
	public void LoadBossInformation(int _bossID)
	{
		if(bossUnit != null)
		{
			bossUnit.SendMessage("Initialized", Resources.Load<BossSOAsset>(Const.BOSS_DATA_PATH(_bossID)));
		}
	}
	public void LoadPlayerInformation()
	{
		if(playerUnit != null)
		{
			playerUnit.SendMessage("Initialized");
		}
	}
	
	void TestSetData() //测试载入数据
	{
		Debug.Log("Start Data Setting...");
		ArchiveManager.LoadPlayerData(1);

		playerUnit.SendMessage("Initialized");

		bossUnit.SendMessage("Initialized", Resources.Load<BossSOAsset>(Const.BOSS_DATA_PATH(1)));

		Debug.Log("测试载入数据完成");
	}

}

