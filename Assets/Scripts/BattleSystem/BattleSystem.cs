using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;
public class BattleSystem : MonoBehaviour
{
	[SerializeField, Header("玩家单位")]
	GameObject playerUnit;
	List<int> playerCardDeck;  //牌堆
	List<GameObject> playerHandCards;  //手牌堆  //max count = 10
	int cardUsedFlag;
	public List<GameObject> PlayerSurventUnitsList { get; private set; } //玩家随从列表

	[SerializeField, Header("Boss单位")]
	GameObject bossUnit;
	public List<GameObject> BossSurventUnitsList { get; private set; } //Boss随从列表  //max count = 7

	//控件
	GameStage Stage;
	GameStage StageCache;
	//回合结束标志
	bool PlayerActionCompleted = false;

	public Queue<ExtraActionPackage> ExtraActionQueue { get; private set; }
	bool ExtraActionExist
	{
		get
		{
			return ExtraActionQueue.Count > 0;
		}
	}

	/// <summary>
	/// 回合数
	/// </summary>
	int Rounds;
	
	//其他
	[Header("回合数")]
	public TextMeshProUGUI roundText;
	[Header("结束回合按钮")]
	public Button endButton;
	[Header("回合标识")]
	public TextMeshProUGUI PlayerTurnText;
	public TextMeshProUGUI EnemyTurnText;

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
		if (ExtraActionExist)
		{
			if(EffectInitiator == null)
			{
				ApplyEffect(ExtraActionQueue.Peek().Initiator, ExtraActionQueue.Peek().Effect);
				ExtraActionQueue.Dequeue();
			}
		}
		else
		{
			MainProcess();
		}
	}
	private void Awake()
	{
		PlayerTurnText.enabled = false;
		EnemyTurnText.enabled = true;

		endButton.onClick.AddListener(EndRound);

		roundText.text = Rounds.ToString();

		playerCardDeck = new List<int>();
		playerHandCards = new List<GameObject>(10);
		cardUsedFlag = 0;
		PlayerSurventUnitsList = new List<GameObject>(7);
		BossSurventUnitsList = new List<GameObject>(7);

		ExtraActionQueue = new Queue<ExtraActionPackage>();

		//
		Stage = GameStage.RoundStart;

//#if UNITY_EDITOR
//        TestSetData();
//        GameObject.Find("Success").SetActive(true);
//        GameObject.Find("Fail").SetActive(true);
//#endif
		LoadPlayerInformation();
		RefreshDeck();
    }
    void SwitchTurnText()
	{
		PlayerTurnText.enabled = !PlayerTurnText.enabled;
		EnemyTurnText.enabled = !EnemyTurnText.enabled;
	}
	/// <summary>
	/// 随机抽卡
	/// </summary>
	/// <param name="_count">抽卡数目</param>
	void DrawCard(int _count)
	{
		for(int i = 0; i < _count; i++)
		{
			if (playerHandCards.Count < 10)
			{
				if (cardUsedFlag == playerCardDeck.Count) //牌库空，触发洗牌以及抽空惩罚
				{
					RefreshDeck();
					Debug.Log("洗牌惩罚");
					//洗牌惩罚,扣玩家5点血
					EffectPackage effect = new EffectPackage(EffectType.Attack, 5, 0, 0, null);
					ApplyEffectTo(playerUnit, null, effect);
				}
				//抽牌
				GetCard(playerCardDeck[cardUsedFlag], 1);
				cardUsedFlag++;
			}
		}
	}
	/// <summary>
	/// 获取特定卡牌
	/// </summary>
	/// <param name="_ID">卡牌ID</param>
	/// <param name="_count">卡牌数量</param>
	void GetCard(int _ID, int _count)
	{
		for(int i = 0; i< _count; i++)
		{
			if (playerHandCards.Count < 10)
			{
				CardSOAsset asset = ArchiveManager.LoadCardAsset(_ID);
				if (asset != null)
				{
					GameObject newCard = Instantiate(cardPrefab, playerHands.transform);
					newCard.GetComponent<CardManager>().Initialized(asset);
					playerHandCards.Add(newCard);
				}
			}
		}
	}
	/// <summary>
	/// 刷新玩家牌堆
	/// </summary>
	void RefreshDeck()
	{
		Debug.Log("洗牌");
		cardUsedFlag = 0;
		for(int i = 0; i < playerCardDeck.Count; i++)
		{
			var rnd = Random.Range(0, playerCardDeck.Count);
			var swap = playerCardDeck[rnd];
			playerCardDeck[rnd] = playerCardDeck[i];
			playerCardDeck[i] = swap;
		}
	}
	void EndRound()
	{
		PlayerActionCompleted = true;
	}
	
	public void AddExtraAction(GameObject initiator, EffectPackageWithTargetOption effect)
	{
		ExtraActionPackage pack = new ExtraActionPackage();
		pack.Initiator = initiator;
		pack.Effect = effect;
		pack.IsEffectOver = false;
		ExtraActionQueue.Enqueue(pack);
	}

	void MainProcess()
	{
		switch (Stage)
		{
			case GameStage.RoundStart: //回合开始
				{
					Debug.Log("Round start");
					//刷新buff
					playerUnit.SendMessage(RunnerMethodName.UpdateEffect);
					foreach (var obj in PlayerSurventUnitsList)
					{
						obj.SendMessage(RunnerMethodName.UpdateEffect);
					}
					bossUnit.SendMessage(RunnerMethodName.UpdateEffect);
					foreach (var obj in BossSurventUnitsList)
					{
						obj.SendMessage(RunnerMethodName.UpdateEffect);
					}
					Debug.Log("回合开始");
					Stage = GameStage.PlayerAdvancedAction;
				}
				break;
			case GameStage.PlayerAdvancedAction: //玩家单位先手效果
				{
					foreach (var unit in PlayerSurventUnitsList)
					{
						unit.SendMessage(RunnerMethodName.AdvancedEffectTrigger);
					}
					Stage = GameStage.EnemyAdvancedAction;
				}
				break;
			case GameStage.EnemyAdvancedAction: //敌人单位先手效果
				{
					bossUnit.SendMessage(RunnerMethodName.AdvancedEffectTrigger);
					foreach (var unit in BossSurventUnitsList)
					{
						unit.SendMessage(RunnerMethodName.AdvancedEffectTrigger);
					}
					Stage = GameStage.PlayerDrawCard;
				}
				break;
			case GameStage.PlayerDrawCard: //玩家抽卡
				{
					if (Rounds == 0)
					{
						DrawCard(3);
					}
					else
					{
						DrawCard(1);
					}
					Stage = GameStage.PlayerAction;
				}
				break;
			case GameStage.PlayerAction: //玩家行动
				{
					if(PlayerActionCompleted)
					{
						Stage = GameStage.EnemyAction;
					}
				}
				break;
			case GameStage.EnemyAction: //敌人行动
				{
					bossUnit.SendMessage(RunnerMethodName.AutoAction, Rounds);
					foreach (var unit in BossSurventUnitsList)
					{
						unit.SendMessage(RunnerMethodName.AutoAction, Rounds);
					}
					Stage = GameStage.PlayerSubsequentAction;
				}
				break;
			case GameStage.PlayerSubsequentAction: //玩家后手效果
				{
					foreach (var unit in PlayerSurventUnitsList)
					{
						unit.SendMessage(RunnerMethodName.SubsequentEffectTrigger);
					}
					Stage = GameStage.EnemySubsequentAction;
				}
				break;
			case GameStage.EnemySubsequentAction:  //敌人后手效果
				{
					bossUnit.SendMessage(RunnerMethodName.SubsequentEffectTrigger);
					foreach (var unit in BossSurventUnitsList)
					{
						unit.SendMessage(RunnerMethodName.SubsequentEffectTrigger);
					}
					Stage = GameStage.RoundEnd;
				}
				break;
			case GameStage.RoundEnd: //回合结束
				{
					Rounds++;
					roundText.text = Rounds.ToString();

					PlayerActionCompleted = false;

					Stage = GameStage.RoundStart;

				}
				break;
			default:
				break;
		}
	}

	public void UseCardByPlayer(GameObject _cardObject)  //使用卡牌
	{
		if(_cardObject != null)
		{
			CardSOAsset card = _cardObject.GetComponent<CardManager>().Asset;
			if (card != null && playerUnit.GetComponent<PlayerUnitManager>().CanUseActionPoint(card.Cost))
			{
				if (card.CardType == CardType.Spell)
				{
					// 使用法术卡
					if(EffectInitiator == null)
					{
						EffectSetupRequest(_cardObject, card.SpellEffect, _cardObject.transform.position, CardType.Spell);
					}
					else
					{
						EffectSetupOver();
					}
				}
				else if ((card.CardType == CardType.Survent && PlayerSurventUnitsList.Count < 7))
				{
					SetupSurvent(card, CardType.Survent);
					playerUnit.GetComponent<PlayerUnitManager>().UseActionPoint(card.Cost);
					playerHandCards.Remove(_cardObject);
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
			//触发*先机*效果
			if(newSurvent != null)
			{
				newSurvent.SendMessage(RunnerMethodName.SetupEffectTrigger);
			}
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
		Destroy(unitObject);
	}

	#region 新版-效果的释放和接收调度函数
	//  新版效果调度
	public GameObject EffectInitiator { get; private set; }
	public CardType InitiatorType { get; private set; }
	public ActionType actionType { get; private set; }
	public GameObject EffectTarget { get; private set; }
	public EffectPackageWithTargetOption effectPack { get; private set; }

	public GameObject ArrowPrefab;
	public GameObject TargetSelectArrow;
	public Transform FightSceneCanvas;

	/// <summary>
	/// 对特定单个目标直接释放效果
	/// </summary>
	/// <param name="_initiator">效果发起者</param>
	/// <param name="_target">效果目标</param>
	/// <param name="_effect">效果信息</param>
	public void ApplyEffectTo(GameObject _target, GameObject _initiator, EffectPackage _effect)
	{
		//抽卡特效
		if (_effect.EffectType == EffectType.DrawSpecificCard || _effect.EffectType == EffectType.DrawRandomCard)
		{
			if (_target != null && _target.GetComponent<PlayerUnitManager>() != null)
			{
				switch (_effect.EffectType)
				{
					case EffectType.DrawSpecificCard:
						GetCard(_effect.EffectValue1, _effect.EffectValue2);
						break;
					case EffectType.DrawRandomCard:
						DrawCard(_effect.EffectValue1);
						break;
					default:
						break;
				}
			}
		}
		else if (_effect.EffectType == EffectType.SpecialEffect)
		{
			//TODO 特殊效果
			GetComponent<SpecialEffectManager>().SendMessage(_effect.SpecialEffectScriptName, _target);
		}
		else
		{
			if (_target != null && _effect != null)
			{
				object[] ParameterList = { _initiator, _effect };
				_target.SendMessage(RunnerMethodName.AcceptEffect, ParameterList);
			}
		}
	}
	/// <summary>
	/// 直接释放效果, 目标选择方式由效果包确定
	/// </summary>
	/// <param name="_initiator">效果发起者</param>
	/// <param name="_effect">效果信息包（包含目标信息）</param>
	public void ApplyEffect(GameObject _initiator, EffectPackageWithTargetOption _effect)
	{
		//抽卡特效
		if(_effect.EffectType == EffectType.DrawSpecificCard || _effect.EffectType == EffectType.DrawRandomCard)
		{
			switch (_effect.EffectType)
			{
				case EffectType.DrawSpecificCard:
					GetCard(_effect.EffectValue1, _effect.EffectValue2);
					break;
				case EffectType.DrawRandomCard:
					DrawCard(_effect.EffectValue1);
					break;
				default:
					break;
			}
		}
		else if(_effect.EffectType == EffectType.SpecialEffect)
		{
			//TODO 特殊效果 
			GetComponent<SpecialEffectManager>().SendMessage(_effect.SpecialEffectScriptName, _effect);
		}
		else
		{
			EffectPackage eft = _effect;
			object[] ParameterList = { _initiator, eft };
			switch (_effect.Target)
			{
				case TargetOptions.AllCreatures:
					{
						playerUnit.SendMessage(RunnerMethodName.AcceptEffect, ParameterList);
						bossUnit.SendMessage(RunnerMethodName.AcceptEffect, ParameterList);
						foreach (var obj in PlayerSurventUnitsList)
						{
							obj.SendMessage(RunnerMethodName.AcceptEffect, ParameterList);
						}
						foreach (var obj in BossSurventUnitsList)
						{
							obj.SendMessage(RunnerMethodName.AcceptEffect, ParameterList);
						}
					}
					break;
				case TargetOptions.AllPlayerCreatures:
					{
						Debug.Log("AllPlayerCreatures");
						playerUnit.SendMessage(RunnerMethodName.AcceptEffect, ParameterList);
						foreach (var obj in PlayerSurventUnitsList)
						{
							obj.SendMessage(RunnerMethodName.AcceptEffect, ParameterList);
						}
					}
					break;
				case TargetOptions.AllEnemyCreatures:
					{
						bossUnit.SendMessage(RunnerMethodName.AcceptEffect, ParameterList);
						foreach (var obj in BossSurventUnitsList)
						{
							obj.SendMessage(RunnerMethodName.AcceptEffect, ParameterList);
						}
					}
					break;
				case TargetOptions.AllCreaturesExcludeMainUnit:
					{
						foreach (var obj in PlayerSurventUnitsList)
						{
							obj.SendMessage(RunnerMethodName.AcceptEffect, ParameterList);
						}
						foreach (var obj in BossSurventUnitsList)
						{
							obj.SendMessage(RunnerMethodName.AcceptEffect, ParameterList);
						}
					}
					break;
				case TargetOptions.AllPlayerCreaturesExcludePlayerUnit:
					{
						foreach (var obj in PlayerSurventUnitsList)
						{
							obj.SendMessage(RunnerMethodName.AcceptEffect, ParameterList);
						}
					}
					break;
				case TargetOptions.ALlEnemyCreaturesExcludeBossUnit:
					{
						foreach (var obj in BossSurventUnitsList)
						{
							obj.SendMessage(RunnerMethodName.AcceptEffect, ParameterList);
						}
					}
					break;
				case TargetOptions.SinglePlayerTarget:
					{
						switch (_effect.SingleTargetOption)
						{
							case SingleTargetOption.RandomTarget:
								{
									do
									{
										int rnd = Random.Range(0, PlayerSurventUnitsList.Count + 1);
										if (rnd == PlayerSurventUnitsList.Count)
										{
											playerUnit.SendMessage(RunnerMethodName.AcceptEffect, ParameterList);
											break;
										}
										else
										{
											if(CheckAttack(PlayerSurventUnitsList[rnd]))
											{
												PlayerSurventUnitsList[rnd].SendMessage(RunnerMethodName.AcceptEffect, ParameterList);
												break;
											}
										}
									} while (true);	
								}
								break;
							case SingleTargetOption.HighestHPTarget:
								{
									GameObject obj = playerUnit;
									int maxhp = playerUnit.GetComponent<PlayerUnitManager>().player.CurrentHP;
									foreach(var tmp in PlayerSurventUnitsList)
									{
										if(tmp.GetComponent<SurventUnitManager>().survent.CurrentHP > maxhp)
										{
											obj = tmp;
											maxhp = tmp.GetComponent<SurventUnitManager>().survent.CurrentHP;
										}
									}
									obj.SendMessage(RunnerMethodName.AcceptEffect, ParameterList);
								}
								break;
							case SingleTargetOption.LowestHPTarget:
								{
									GameObject obj = playerUnit;
									int minhp = playerUnit.GetComponent<PlayerUnitManager>().player.CurrentHP;
									foreach (var tmp in PlayerSurventUnitsList)
									{
										if (tmp.GetComponent<SurventUnitManager>().survent.CurrentHP < minhp)
										{
											obj = tmp;
											minhp = tmp.GetComponent<SurventUnitManager>().survent.CurrentHP;
										}
									}
									obj.SendMessage(RunnerMethodName.AcceptEffect, ParameterList);
								}
								break;
							case SingleTargetOption.HigestATKTarget:
								{
									GameObject obj = playerUnit;
									int maxatk = playerUnit.GetComponent<PlayerUnitManager>().player.ATK;
									foreach (var tmp in PlayerSurventUnitsList)
									{
										if (tmp.GetComponent<SurventUnitManager>().survent.ATK  > maxatk)
										{
											obj = tmp;
											maxatk = tmp.GetComponent<SurventUnitManager>().survent.ATK;
										}
									}
									obj.SendMessage(RunnerMethodName.AcceptEffect, ParameterList);
								}
								break;
							case SingleTargetOption.LowestATKTarget:
								{
									GameObject obj = playerUnit;
									int minatk = playerUnit.GetComponent<PlayerUnitManager>().player.ATK;
									foreach (var tmp in PlayerSurventUnitsList)
									{
										if (tmp.GetComponent<SurventUnitManager>().survent.ATK < minatk)
										{
											obj = tmp;
											minatk = tmp.GetComponent<SurventUnitManager>().survent.ATK;
										}
									}
									obj.SendMessage(RunnerMethodName.AcceptEffect, ParameterList);
								}
								break;
							case SingleTargetOption.SpecificTarget:
								{
									if (_initiator.GetComponent<SurventUnitManager>() != null)
									{
										EffectSetupRequest(_initiator, _effect, _initiator.transform.position, CardType.Survent, ActionType.ExtraAction);
									}
									else if(_initiator.GetComponent<CardManager>() != null)
									{
										EffectSetupRequest(_initiator, _effect, _initiator.transform.position, CardType.Spell);
									}
								}
								break;
							default:
								break;
						}
					}
					break;
				case TargetOptions.SingleEnemyTarget:
					{
						switch (_effect.SingleTargetOption)
						{
							case SingleTargetOption.RandomTarget:
								{
									do
									{
										int rnd = Random.Range(0, BossSurventUnitsList.Count + 1);
										if (rnd == BossSurventUnitsList.Count)
										{
											bossUnit.SendMessage(RunnerMethodName.AcceptEffect, ParameterList);
											break;
										}
										else
										{
											if (CheckAttack(BossSurventUnitsList[rnd]))
											{
												BossSurventUnitsList[rnd].SendMessage(RunnerMethodName.AcceptEffect, ParameterList);
												break;
											}
										}
									} while (true);
								}
								break;
							case SingleTargetOption.HighestHPTarget:
								{
									GameObject obj = bossUnit;
									int maxhp = bossUnit.GetComponent<BossUnitManager>().Boss.CurrentHP;
									foreach (var tmp in BossSurventUnitsList)
									{
										if (tmp.GetComponent<SurventUnitManager>().survent.CurrentHP > maxhp)
										{
											obj = tmp;
											maxhp = tmp.GetComponent<SurventUnitManager>().survent.CurrentHP;
										}
									}
									obj.SendMessage(RunnerMethodName.AcceptEffect, ParameterList);
								}
								break;
							case SingleTargetOption.LowestHPTarget:
								{
									GameObject obj = bossUnit;
									int minhp = bossUnit.GetComponent<BossUnitManager>().Boss.CurrentHP;
									foreach (var tmp in BossSurventUnitsList)
									{
										if (tmp.GetComponent<SurventUnitManager>().survent.CurrentHP < minhp)
										{
											obj = tmp;
											minhp = tmp.GetComponent<SurventUnitManager>().survent.CurrentHP;
										}
									}
									obj.SendMessage(RunnerMethodName.AcceptEffect, ParameterList);
								}
								break;
							case SingleTargetOption.HigestATKTarget:
								{
									GameObject obj = bossUnit;
									int maxatk = bossUnit.GetComponent<BossUnitManager>().Boss.ATK;
									foreach (var tmp in BossSurventUnitsList)
									{
										if (tmp.GetComponent<SurventUnitManager>().survent.ATK > maxatk)
										{
											obj = tmp;
											maxatk = tmp.GetComponent<SurventUnitManager>().survent.ATK;
										}
									}
									obj.SendMessage(RunnerMethodName.AcceptEffect, ParameterList);
								}
								break;
							case SingleTargetOption.LowestATKTarget:
								{
									GameObject obj = bossUnit;
									int minatk = bossUnit.GetComponent<BossUnitManager>().Boss.ATK;
									foreach (var tmp in BossSurventUnitsList)
									{
										if (tmp.GetComponent<SurventUnitManager>().survent.ATK < minatk)
										{
											obj = tmp;
											minatk = tmp.GetComponent<SurventUnitManager>().survent.ATK;
										}
									}
									obj.SendMessage(RunnerMethodName.AcceptEffect, ParameterList);
								}
								break;
							case SingleTargetOption.SpecificTarget:
								{
									if(_initiator.GetComponent<SurventUnitManager>()!=null)
									{
										EffectSetupRequest(_initiator, _effect, _initiator.transform.position, CardType.Survent, ActionType.ExtraAction);
									}
									else if (_initiator.GetComponent<CardManager>() != null)
									{
										EffectSetupRequest(_initiator, _effect, _initiator.transform.position, CardType.Spell);
									}
								}
								break;
							default:
								break;
						}
					}
					break;
					//TODO 优化多目标选择（有可能重复）
				case TargetOptions.MultiPlayerTargets:
					{
						if(_effect.TargetCount >= PlayerSurventUnitsList.Count + 1)
						{
							playerUnit.SendMessage(RunnerMethodName.AcceptEffect, ParameterList);
							foreach (var obj in PlayerSurventUnitsList)
							{
								obj.SendMessage(RunnerMethodName.AcceptEffect, ParameterList);
							}
						}
						else
						{
							for(int i = 0; i < _effect.TargetCount; i++)
							{
								int rnd = Random.Range(0, PlayerSurventUnitsList.Count + 1);
								if (rnd == PlayerSurventUnitsList.Count)
								{
									playerUnit.SendMessage(RunnerMethodName.AcceptEffect, ParameterList);
								}
								else
								{
									PlayerSurventUnitsList[rnd].SendMessage(RunnerMethodName.AcceptEffect, ParameterList);
								}
							}
						}
					}
					break;
				case TargetOptions.MultiEnemyTargets:
					{
						if (_effect.TargetCount >= BossSurventUnitsList.Count + 1)
						{
							bossUnit.SendMessage(RunnerMethodName.AcceptEffect, ParameterList);
							foreach (var obj in BossSurventUnitsList)
							{
								obj.SendMessage(RunnerMethodName.AcceptEffect, ParameterList);
							}
						}
						else
						{
							for (int i = 0; i < _effect.TargetCount; i++)
							{
								int rnd = Random.Range(0, BossSurventUnitsList.Count + 1);
								if (rnd == BossSurventUnitsList.Count)
								{
									bossUnit.SendMessage(RunnerMethodName.AcceptEffect, ParameterList);
								}
								else
								{
									BossSurventUnitsList[rnd].SendMessage(RunnerMethodName.AcceptEffect, ParameterList);
								}
							}
						}
					}
					break;
				case TargetOptions.OneselfTarget:
					{
						if(_initiator != null)
						{
							_initiator.SendMessage(RunnerMethodName.AcceptEffect, ParameterList);
						}
					}
					break;
				default:
					break;
			}
		}
	}
	public void EffectSetupRequest(GameObject initiator, EffectPackageWithTargetOption package, Vector2 Pos, CardType _type, ActionType _actionType = ActionType.NormalAction)
	{
		if(initiator != null && package != null)
		{
			EffectInitiator = initiator;
			effectPack = package;
			InitiatorType = _type;
			actionType = _actionType;
			//Debug.Log("Accept Request");
			// 启动目标选择动画
			TargetSelectArrow = Instantiate(ArrowPrefab, FightSceneCanvas.transform);
			TargetSelectArrow.GetComponent<ArrowDisplay>().SetStartPoint(Pos);
			if (effectPack.EffectType == EffectType.Attack || effectPack.EffectType == EffectType.VampireAttack || effectPack.EffectType == EffectType.SpecialEffect)
			{
				TargetSelectArrow.GetComponent<Image>().color = Color.red;
			}
			else
			{
				TargetSelectArrow.GetComponent<Image>().color = Color.yellow;
			}
		}
		else
		{
			//Debug.Log("Refuse Request");
			EffectSetupOver();
		}
	}
	public void EffectConfirm(GameObject target)
	{
		//TODO 这里写得很狗屎，得找个时间重写
		EffectTarget = target;
		if (EffectInitiator != EffectTarget && EffectInitiator != null && EffectTarget != null)
		{
			if(CheckAttack(EffectTarget))
			{
				UnitType unitType = GetUnitType(target);
				if (effectPack.Target == TargetOptions.SinglePlayerTarget && (unitType == UnitType.Player || unitType == UnitType.PlayerSurvent))
				{
					ApplyEffectTo(EffectTarget, EffectInitiator, effectPack);
					if (InitiatorType == CardType.Survent && actionType != ActionType.ExtraAction)
					{
						EffectInitiator.SendMessage("ActionComplete");
					}
					else if (InitiatorType == CardType.Spell)
					{
						playerHandCards.Remove(EffectInitiator);
						Destroy(EffectInitiator);
					}
				}
				else if (effectPack.Target == TargetOptions.SingleEnemyTarget && (unitType == UnitType.Boss || unitType == UnitType.BossSurvent))
				{
					ApplyEffectTo(EffectTarget, EffectInitiator, effectPack);
					if (InitiatorType == CardType.Survent && actionType != ActionType.ExtraAction)
					{
						EffectInitiator.SendMessage("ActionComplete");
					}
					else if (InitiatorType == CardType.Spell)
					{
						playerHandCards.Remove(EffectInitiator);
						Destroy(EffectInitiator);
					}
				}
			}
		}
		EffectSetupOver();
	}
	public void EffectSetupOver()
	{
		EffectInitiator = null;
		EffectTarget = null;
		effectPack = null;

		//关闭目标选择动画
		Debug.Log("Cancel Request");
		Destroy(TargetSelectArrow);
		TargetSelectArrow = null;
	}
	UnitType GetUnitType(GameObject _obj)
	{
		if (_obj.GetComponent<PlayerUnitManager>() != null)
		{
			return UnitType.Player;
		}
		else if (_obj.GetComponent<BossUnitManager>() != null)
		{
			return UnitType.Boss;
		}
		else if(_obj.GetComponent<SurventUnitManager>() != null)
		{
			if(_obj.GetComponent<SurventUnitManager>().survent.SurventType == CardType.Survent)
			{
				return UnitType.PlayerSurvent;
			}
			else if(_obj.GetComponent<SurventUnitManager>().survent.SurventType == CardType.Monster)
			{
				return UnitType.BossSurvent;
			}
			else
			{
				return UnitType.None;
			}
		}
		else
		{
			return UnitType.None;
		}
	}
	/// <summary>
	/// 检测能否攻击该目标
	/// </summary>
	/// <param name="attackTarget"></param>
	/// <returns></returns>
	bool CheckAttack(GameObject attackTarget)
	{
		if(attackTarget.GetComponent<SurventUnitManager>() != null)
		{
			if(attackTarget.GetComponent<SurventUnitManager>().survent.IsConcealed)
			{
				return false;
			}
			else
			{
				bool tankExist = false;
				switch (GetUnitType(attackTarget))
				{
					case UnitType.PlayerSurvent:
						{
							foreach (var unit in PlayerSurventUnitsList)
							{
								if (unit.GetComponent<SurventUnitManager>().survent.IsTank)
								{
									tankExist = true;
								}
							}
						}
						break;
					case UnitType.BossSurvent:
						{
							foreach (var unit in BossSurventUnitsList)
							{
								if (unit.GetComponent<SurventUnitManager>().survent.IsTank)
								{
									tankExist = true;
								}
							}
						}
						break;
					default:
						return false;
				}
				if(tankExist)
				{
					return attackTarget.GetComponent<SurventUnitManager>().survent.IsTank;
				}
				else
				{
					return true;
				}
			}
		}
		return true;
	}
	#endregion


	IEnumerator LoadNextScene()
	{
		AsyncOperation async = SceneManager.LoadSceneAsync("CardSelect");
		if (async != null)
		{
			async.allowSceneActivation = false;
			while (!async.isDone)
			{
				if (async.progress >= 0.9f)
				{
					async.allowSceneActivation = true;
				}
				yield return null;
			}
		}
	}


	//TODO 战斗结束
	public void GameEnd(GameResult result)
	{
		SavePlayerInformation();
		switch (result)
		{
			case GameResult.Success:
				{
					Debug.Log("游戏胜利");
					//SceneManager.LoadSceneAsync("CardSelect");
					StartCoroutine(LoadNextScene());
				}
				break;
			case GameResult.Failure:
				{
					Debug.Log("游戏失败");
					PlayerDataTF.EventEnd();
					SceneManager.LoadScene("GameProcess");
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
	public void GameEnd(int _result)
	{
		GameResult result = (GameResult)_result;
		GameEnd(result);
	}

	//相关信息载入方法
	public void LoadBossInformation(int _bossID)
	{
		if(bossUnit != null)
		{
			bossUnit.SendMessage("Initialized", Resources.Load<BossSOAsset>(Const.BOSS_DATA_PATH(_bossID)));
		}
        else
        {
			bossUnit.SendMessage("Initialized", Resources.Load<BossSOAsset>(Const.BOSS_DATA_PATH(0)));
		}
	}
	public void LoadPlayerInformation()
	{
		if(playerUnit != null && Player.Instance != null)
		{
			playerUnit.SendMessage("Initialized");
			playerCardDeck = Player.Instance.cardSet;
		}
	}

	public void SavePlayerInformation()
	{
		if (playerUnit != null && Player.Instance != null)
		{
			Player.Instance.SetCurrentHP(playerUnit.GetComponent<PlayerUnitManager>().player.CurrentHP);
		}
	}

	void TestSetData() //测试载入数据
	{
		Debug.Log("Start Data Setting...");
		ArchiveManager.LoadPlayerData();

		playerUnit.SendMessage("Initialized");
		LoadPlayerInformation();
		bossUnit.SendMessage("Initialized", Resources.Load<BossSOAsset>(Const.BOSS_DATA_PATH(0)));
		Debug.Log(Const.BOSS_DATA_PATH(0));

		Debug.Log("测试载入数据完成");
	}
}

