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
	List<int> deck;  //牌堆
	List<GameObject> handCards;  //手牌堆  //max count = 10
	//List<int> usedCards;  //弃牌堆
	int cardUsedFlag;
	public List<GameObject> PlayerSurventUnitsList { get; private set; } //玩家随从列表

	[Header("Boss单位")]
	public GameObject bossUnit;
	public List<GameObject> BossSurventUnitsList { get; private set; } //Boss随从列表  //max count = 7

	//控件
	//回合结束标志
	bool playerActionCompleted = false;
	//回合开始标志
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
	[Header("卡牌预制体")]
	public GameObject cardPrefab;  
	private void Update()
	{
		GamePlay();
	}
	private void Awake()
	{
		victory.SetActive(false);
		//初始化实例
		endButton.onClick.AddListener(EndRound);
		roundText.text = round.ToString();
		//deck = new List<int>();
		deck = new List<int> { 0,200 };

		handCards = new List<GameObject>(10);
		//usedCards = new List<int>();
		cardUsedFlag = 0;
		PlayerSurventUnitsList = new List<GameObject>(7);
		BossSurventUnitsList = new List<GameObject>(7);

		//
		TestSetData();
	}
	void GetCard(int _count)
	{
		for(int i = 0; i < _count; i++)
		{
			if (handCards.Count == 10) return;
			if (cardUsedFlag == deck.Count) //牌库空，触发洗牌以及抽空惩罚
			{
				RefreshDeck();
				Debug.Log("洗牌惩罚");
				//TODO 洗牌惩罚,扣玩家5点血
				//Effect.Set(playerUnit, EffectType.Attack, 5);
				

			}
			//抽牌
			GameObject newCard = Instantiate(cardPrefab, playerHands.transform); //生成预制件实例
			//依据编号，从文件中读取卡牌数据
			CardSOAsset card = Resources.Load<CardSOAsset>(Const.CARD_DATA_PATH(deck[cardUsedFlag]));
			cardUsedFlag++;

			newCard.GetComponent<CardDisplay>().Initialized(card);
			newCard.GetComponent<CardDisplay>().LoadInf();

			handCards.Add(newCard);
		}
	}
	void RefreshDeck()  //洗牌刷新牌堆
	{
		Debug.Log("洗牌");
		cardUsedFlag = 0;
		for(int i = 0; i < deck.Count; i++)
		{
			var rnd = Random.Range(0, deck.Count);
			var swap = deck[rnd];
			deck[rnd] = deck[i];
			deck[i] = swap;
		}
	}
	void EndRound()
	{
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
			roundStart = true;
			//触发玩家随从先机效果
			foreach (var unit in PlayerSurventUnitsList)
			{
				unit.SendMessage("AdvancedEffectTrigger");
			}
		}
		//2 玩家部署随从/使用法术牌
		//3 玩家行动完成后怪物行动,并结束回合
		else  if (playerActionCompleted)  
		{
			//触发玩家随从后手效果
			foreach(var unit in PlayerSurventUnitsList)
			{
				unit.SendMessage("SubsequentEffectTrigger");
			}

			// Boss以及敌人随从，先机效果和行动
			bossUnit.SendMessage("AdvancedEffectTrigger");
			bossUnit.SendMessage("AutoAction", round);
			foreach (var unit in BossSurventUnitsList)
			{
				unit.SendMessage("AdvancedEffectTrigger");
			}
			foreach(var unit in BossSurventUnitsList)
			{
				unit.SendMessage("AutoAction", round);
			}

			//刷新回合
			round++;
			roundText.text = round.ToString();
			playerActionCompleted = false;
			roundStart = false;

			//检查并刷新buff,以及触发boss和敌人随从后手效果
			bossUnit.SendMessage("SubsequentEffectTrigger");
			foreach(var unit in BossSurventUnitsList)
			{
				unit.SendMessage("SubsequentEffectTrigger");
			}
			//结束回合
		}
		
	}
	public void UseCardByPlayer(GameObject _cardObject)  //使用卡牌
	{
		if(_cardObject != null)
		{
			CardSOAsset card = _cardObject.GetComponent<CardDisplay>().Asset;
			if (card != null && playerUnit.GetComponent<PlayerUnitManager>().CanUseActionPoint(card.Cost))
			{
				if (card.CardType == CardType.Spell)
				{
					//TODO 使用法术卡
				}
				else if ((card.CardType == CardType.Survent && PlayerSurventUnitsList.Count < 7))
				{
					SetupSurvent(card, CardType.Survent);
					playerUnit.GetComponent<PlayerUnitManager>().UseActionPoint(card.Cost);
					handCards.Remove(_cardObject);
					Destroy(_cardObject);
				}
			}
			else
			{
				Debug.Log("战术点不足");
			}
		}
	}
	public void SetupSurvent(CardSOAsset _card, CardType _type)
	{
		if (_card != null) 
		{
			GameObject newSurvent = null;
			switch (_type)
			{
				case CardType.Survent:
					{
						newSurvent = Instantiate(surventPrefab, surventArea.transform);
						newSurvent.GetComponent<SurventUnitManager>().Initialized(_card);
						PlayerSurventUnitsList.Add(newSurvent);
					}
					break;
				case CardType.Monster:
					{
						if (BossSurventUnitsList.Count < 7)
						{
							GameObject newEnemy = Instantiate(surventPrefab, enemyArea.transform);
							newEnemy.GetComponent<SurventUnitManager>().Initialized(_card);
							BossSurventUnitsList.Add(newEnemy);
						}
					}
					break;
				default:
					break;
			}
			//触发放置效果
			newSurvent.SendMessage("SetupEffectTrigger");
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
	//TODO 新版效果调度
	public GameObject EffectInitiator { get; private set; }
	public GameObject EffectTarget { get; private set; }
	public EffectPackage Package { get; private set; }
	/// <summary>
	/// 对特定单个目标直接释放效果
	/// </summary>
	/// <param name="_initiator">效果发起者</param>
	/// <param name="_target">效果目标</param>
	/// <param name="_effect">效果信息</param>
	public void ApplyEffectTo(GameObject _target, GameObject _initiator, EffectPackage _effect)
	{
		if (_target != null && _initiator != null && _effect != null)
		{
			object[] ParameterList = { _initiator, _effect };
			_target.SendMessage("AcceptEffect", ParameterList);
		}
	}
	/// <summary>
	/// 直接释放效果, 目标选择方式由效果包确定
	/// </summary>
	/// <param name="initiator">效果发起者</param>
	/// <param name="package">效果信息包（包含目标信息）</param>
	public void ApplyEffect(GameObject initiator, EffectPackageWithTargetOption package)
	{
		//TODO 设置效果
		switch (package.EffectTarget)
		{
			case TargetOptions.AllCreatures:
				break;
			case TargetOptions.AllPlayerCreatures:
				break;
			case TargetOptions.AllEnemyCreatures:
				break;
			case TargetOptions.AllCharacters:
				break;
			case TargetOptions.ALlPlayerCharacter:
				break;
			case TargetOptions.ALlEnemyCharacters:
				break;
			case TargetOptions.SinglePlayerTarget:
				break;
			case TargetOptions.MultiPlayerTargets:
				break;
			case TargetOptions.MultiEnemyTargets:
				break;
			default:
				break;
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
				//attacker.GetComponent<SurventUnitManager>().isActive = false;
				Effect.Set(victim, EffectType.Attack, attacker.GetComponent<SurventUnitManager>().survent.ATK);
			}
			else if(attackerType == CardType.Spell)
			{
				CardSOAsset spellCard = attacker.GetComponent<CardDisplay>().Asset;
				//Effect.Set(victim, spellCard.SpellActionType, spellCard.SpellActionValue1, spellCard.SpellActionValue2);
				//player.CurrentActionPoint -= spellCard.Cost;
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

	//TODO 胜利
	public GameObject victory;

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
		if(playerUnit != null && Player.Instance != null)
		{
			playerUnit.SendMessage("Initialized");
			deck = Player.Instance.cardSet;
		}
	}
	
	void TestSetData() //测试载入数据
	{
		Debug.Log("Start Data Setting...");
		ArchiveManager.LoadPlayerData(1);
		
		playerUnit.SendMessage("Initialized");

		bossUnit.SendMessage("Initialized", Resources.Load<BossSOAsset>(Const.BOSS_DATA_PATH(0)));
		Debug.Log(Const.BOSS_DATA_PATH(0));

		Debug.Log("测试载入数据完成");
	}

}

