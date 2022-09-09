using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;
public class BattleSystem : MonoBehaviour
{
	[SerializeField, Header("��ҵ�λ")]
	GameObject playerUnit;
	List<int> playerCardDeck;  //�ƶ�
	List<GameObject> playerHandCards;  //���ƶ�  //max count = 10
	int cardUsedFlag;
	public List<GameObject> PlayerSurventUnitsList { get; private set; } //�������б�

	[SerializeField, Header("Boss��λ")]
	GameObject bossUnit;
	public List<GameObject> BossSurventUnitsList { get; private set; } //Boss����б�  //max count = 7

	//�ؼ�
	GameStage Stage;
	GameStage StageCache;
	//�غϽ�����־
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
	/// �غ���
	/// </summary>
	int Rounds;
	
	//����
	[Header("�غ���")]
	public TextMeshProUGUI roundText;
	[Header("�����غϰ�ť")]
	public Button endButton;
	[Header("�غϱ�ʶ")]
	public TextMeshProUGUI PlayerTurnText;
	public TextMeshProUGUI EnemyTurnText;

	[Header("���������")]
	public GameObject playerHands;
	[Header("����������")]
	public GameObject surventArea;
	[Header("�з������")]
	public GameObject enemyArea;
	[Header("���Ԥ����")]
	public GameObject surventPrefab;
	[Header("����Ԥ����")]
	public GameObject cardPrefab;
	public SpecialEffectManager SpecialEffectManager { get; private set; }
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
		PlayerTurnText.enabled = true;
		EnemyTurnText.enabled = false;

		endButton.onClick.AddListener(EndRound);
		
		Rounds = 0;
		roundText.text = Rounds.ToString();

		playerCardDeck = new List<int>();
		playerHandCards = new List<GameObject>(10);
		cardUsedFlag = 0;
		PlayerSurventUnitsList = new List<GameObject>(7);
		BossSurventUnitsList = new List<GameObject>(7);

		ExtraActionQueue = new Queue<ExtraActionPackage>();

		SpecialEffectManager = GetComponent<SpecialEffectManager>();

		//
		//Stage = GameStage.RoundStart;
		Stage = GameStage.PlayerDrawCard;


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
	}
	/// <summary>
	/// ����鿨
	/// </summary>
	/// <param name="_count">�鿨��Ŀ</param>
	void DrawCard(int _count)
	{
		for(int i = 0; i < _count; i++)
		{
			if (playerHandCards.Count < 10)
			{
				if (cardUsedFlag == playerCardDeck.Count) //�ƿ�գ�����ϴ���Լ���ճͷ�
				{
					RefreshDeck();
					Debug.Log("ϴ�Ƴͷ�");
					//ϴ�Ƴͷ�,�����5��Ѫ
					EffectPackage effect = new EffectPackage(EffectType.Attack, 5, 0, 0, null);
					ApplyEffectTo(playerUnit, null, effect);
				}
				//����
				GetCard(playerCardDeck[cardUsedFlag], 1);
				cardUsedFlag++;
			}
		}
	}
	/// <summary>
	/// ��ȡ�ض�����
	/// </summary>
	/// <param name="_ID">����ID</param>
	/// <param name="_count">��������</param>
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
	/// ˢ������ƶ�
	/// </summary>
	void RefreshDeck()
	{
		Debug.Log("ϴ��");
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
			case GameStage.RoundStart: //�غϿ�ʼ
				{
					Debug.Log("Round start");
					//ˢ��buff
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
					Debug.Log("�غϿ�ʼ");
					Stage = GameStage.PlayerAdvancedAction;
				}
				break;
			case GameStage.PlayerAdvancedAction: //��ҵ�λ����Ч��
				{
					foreach (var unit in PlayerSurventUnitsList)
					{
						unit.SendMessage(RunnerMethodName.AdvancedEffectTrigger);
					}
					Stage = GameStage.EnemyAdvancedAction;
				}
				break;
			case GameStage.EnemyAdvancedAction: //���˵�λ����Ч��
				{
					bossUnit.SendMessage(RunnerMethodName.AdvancedEffectTrigger);
					foreach (var unit in BossSurventUnitsList)
					{
						unit.SendMessage(RunnerMethodName.AdvancedEffectTrigger);
					}
					Stage = GameStage.PlayerDrawCard;
				}
				break;
			case GameStage.PlayerDrawCard: //��ҳ鿨
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
			case GameStage.PlayerAction: //����ж�
				{
					if(PlayerActionCompleted)
					{
						Stage = GameStage.EnemyAction;
					}
				}
				break;
			case GameStage.EnemyAction: //�����ж�
				{
					//SwitchTurnText();
					bossUnit.SendMessage(RunnerMethodName.AutoAction, Rounds);
					foreach (var unit in BossSurventUnitsList)
					{
						unit.SendMessage(RunnerMethodName.AutoAction, Rounds);
					}
					Stage = GameStage.PlayerSubsequentAction;
					//SwitchTurnText();
				}
				break;
			case GameStage.PlayerSubsequentAction: //��Һ���Ч��
				{
					foreach (var unit in PlayerSurventUnitsList)
					{
						unit.SendMessage(RunnerMethodName.SubsequentEffectTrigger);
					}
					Stage = GameStage.EnemySubsequentAction;
				}
				break;
			case GameStage.EnemySubsequentAction:  //���˺���Ч��
				{
					bossUnit.SendMessage(RunnerMethodName.SubsequentEffectTrigger);
					foreach (var unit in BossSurventUnitsList)
					{
						unit.SendMessage(RunnerMethodName.SubsequentEffectTrigger);
					}
					Stage = GameStage.RoundEnd;
				}
				break;
			case GameStage.RoundEnd: //�غϽ���
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

	public void UseCardByPlayer(GameObject _cardObject)  //ʹ�ÿ���
	{
		if(_cardObject != null)
		{
			CardSOAsset card = _cardObject.GetComponent<CardManager>().Asset;
			if (card != null && playerUnit.GetComponent<PlayerUnitManager>().CanUseActionPoint(card.Cost))
			{
				if (card.CardType == CardType.Spell)
				{
					// ʹ�÷�����
					if(EffectInitiator == null)
					{
						if(card.SpellEffect.Target == TargetOptions.SinglePlayerTarget
							|| card.SpellEffect.Target == TargetOptions.SinglePlayerTarget
							&& card.SpellEffect.SingleTargetOption == SingleTargetOption.SpecificTarget)
						{
							EffectSetupRequest(_cardObject, card.SpellEffect, _cardObject.transform.position, CardType.Spell);
						}
						else
						{
							ApplyEffect(_cardObject, card.SpellEffect);
							Destroy(_cardObject);
						}
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
				Debug.Log("ս���㲻��");
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
						if(PlayerSurventUnitsList.Count < 7)
						{
							newSurvent = Instantiate(surventPrefab, surventArea.transform);
							newSurvent.GetComponent<SurventUnitManager>().Initialized(_card);
							PlayerSurventUnitsList.Add(newSurvent);
						}
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
			//����*�Ȼ�*Ч��
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

	#region �°�-Ч�����ͷźͽ��յ��Ⱥ���
	//  �°�Ч������
	public GameObject EffectInitiator { get; private set; }
	public CardType InitiatorType { get; private set; }
	public ActionType actionType { get; private set; }
	public GameObject EffectTarget { get; private set; }
	public EffectPackageWithTargetOption effectPack { get; private set; }

	public GameObject ArrowPrefab;
	public GameObject TargetSelectArrow;
	public Transform FightSceneCanvas;

	/// <summary>
	/// ���ض�����Ŀ��ֱ���ͷ�Ч��
	/// </summary>
	/// <param name="_initiator">Ч��������</param>
	/// <param name="_target">Ч��Ŀ��</param>
	/// <param name="_effect">Ч����Ϣ</param>
	public void ApplyEffectTo(GameObject _target, GameObject _initiator, EffectPackage _effect)
	{
		//�鿨��Ч
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
		else if (_effect.EffectType == EffectType.ActionPointRecovery)
		{
			if (_target != null && _target.GetComponent<PlayerUnitManager>() != null)
			{
				playerUnit.GetComponent<PlayerUnitManager>().player.AddCurrentActionPoint(_effect.EffectValue1);
			}
		}
		else if (_effect.EffectType == EffectType.SpecialEffect)
		{
			//TODO ����Ч��
			object[] parameterTable = { _initiator, _target, _effect };
			SpecialEffectManager.SendMessage(_effect.SpecialEffectScriptName, parameterTable);
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
	/// ֱ���ͷ�Ч��, Ŀ��ѡ��ʽ��Ч����ȷ��
	/// </summary>
	/// <param name="_initiator">Ч��������</param>
	/// <param name="_effect">Ч����Ϣ��������Ŀ����Ϣ��</param>
	public void ApplyEffect(GameObject _initiator, EffectPackageWithTargetOption _effect)
	{
		//�鿨��Ч
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
		else if(_effect.EffectType == EffectType.Summon)
		{
			for (int i = 0; i < _effect.EffectValue1; i++)
			{
				SetupSurvent(ArchiveManager.LoadCardAsset(_effect.EffectValue2), CardType.Survent);
			}
		}
		//else if(_effect.EffectType == EffectType.SpecialEffect)
		//{
		//	//TODO ����Ч�� 
		//	GetComponent<SpecialEffectManager>().SendMessage(_effect.SpecialEffectScriptName, _effect);
		//}
		else
		{
			EffectPackage eft = _effect;
			object[] ParameterList = { _initiator, eft };
			switch (_effect.Target)
			{
				case TargetOptions.AllCreatures:
					{
						//playerUnit.SendMessage(RunnerMethodName.AcceptEffect, ParameterList);
						ApplyEffectTo(playerUnit,_initiator, _effect);
						//bossUnit.SendMessage(RunnerMethodName.AcceptEffect, ParameterList);
						ApplyEffectTo(bossUnit, _initiator, _effect);
						foreach (var obj in PlayerSurventUnitsList)
						{
							//obj.SendMessage(RunnerMethodName.AcceptEffect, ParameterList);
							ApplyEffectTo(obj, _initiator, _effect);
						}
						foreach (var obj in BossSurventUnitsList)
						{
							//obj.SendMessage(RunnerMethodName.AcceptEffect, ParameterList);
							ApplyEffectTo(obj, _initiator, _effect);
						}
					}
					break;
				case TargetOptions.AllPlayerCreatures:
					{
						//Debug.Log("AllPlayerCreatures");
						//playerUnit.SendMessage(RunnerMethodName.AcceptEffect, ParameterList);
						ApplyEffectTo(playerUnit, _initiator, _effect);
						foreach (var obj in PlayerSurventUnitsList)
						{
							ApplyEffectTo(obj, _initiator, _effect);
							//obj.SendMessage(RunnerMethodName.AcceptEffect, ParameterList);
						}
					}
					break;
				case TargetOptions.AllEnemyCreatures:
					{
						ApplyEffectTo(bossUnit, _initiator, _effect);
						//SendMessage(RunnerMethodName.AcceptEffect, ParameterList);
						foreach (var obj in BossSurventUnitsList)
						{
							ApplyEffectTo(obj, _initiator, _effect);
							//obj.SendMessage(RunnerMethodName.AcceptEffect, ParameterList);
						}
					}
					break;
				case TargetOptions.AllCreaturesExcludeMainUnit:
					{
						foreach (var obj in PlayerSurventUnitsList)
						{
							ApplyEffectTo(obj, _initiator, _effect);
							//obj.SendMessage(RunnerMethodName.AcceptEffect, ParameterList);
						}
						foreach (var obj in BossSurventUnitsList)
						{
							ApplyEffectTo(obj, _initiator, _effect);
							//obj.SendMessage(RunnerMethodName.AcceptEffect, ParameterList);
						}
					}
					break;
				case TargetOptions.AllPlayerCreaturesExcludePlayerUnit:
					{
						foreach (var obj in PlayerSurventUnitsList)
						{
							ApplyEffectTo(obj, _initiator, _effect);
							//obj.SendMessage(RunnerMethodName.AcceptEffect, ParameterList);
						}
					}
					break;
				case TargetOptions.ALlEnemyCreaturesExcludeBossUnit:
					{
						foreach (var obj in BossSurventUnitsList)
						{
							ApplyEffectTo(obj, _initiator, _effect);
							//obj.SendMessage(RunnerMethodName.AcceptEffect, ParameterList);
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
											ApplyEffectTo(playerUnit, _initiator, _effect);
											//playerUnit.SendMessage(RunnerMethodName.AcceptEffect, ParameterList);
											break;
										}
										else
										{
											if(CheckAttack(PlayerSurventUnitsList[rnd]))
											{
												ApplyEffectTo(PlayerSurventUnitsList[rnd], _initiator, _effect);
												//PlayerSurventUnitsList[rnd].SendMessage(RunnerMethodName.AcceptEffect, ParameterList);
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
									ApplyEffectTo(obj, _initiator, _effect);
									//obj.SendMessage(RunnerMethodName.AcceptEffect, ParameterList);
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
									ApplyEffectTo(obj, _initiator, _effect);
									//obj.SendMessage(RunnerMethodName.AcceptEffect, ParameterList);
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
									ApplyEffectTo(obj, _initiator, _effect);
									//obj.SendMessage(RunnerMethodName.AcceptEffect, ParameterList);
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
									ApplyEffectTo(obj, _initiator, _effect);
									//obj.SendMessage(RunnerMethodName.AcceptEffect, ParameterList);
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
											ApplyEffectTo(bossUnit, _initiator, _effect);
											//bossUnit.SendMessage(RunnerMethodName.AcceptEffect, ParameterList);
											break;
										}
										else
										{
											if (CheckAttack(BossSurventUnitsList[rnd]))
											{
												ApplyEffectTo(BossSurventUnitsList[rnd], _initiator, _effect);
												//BossSurventUnitsList[rnd].SendMessage(RunnerMethodName.AcceptEffect, ParameterList);
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
									ApplyEffectTo(obj, _initiator, _effect);
									//obj.SendMessage(RunnerMethodName.AcceptEffect, ParameterList);
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
									ApplyEffectTo(obj, _initiator, _effect);
									//obj.SendMessage(RunnerMethodName.AcceptEffect, ParameterList);
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
									ApplyEffectTo(obj, _initiator, _effect);
									//obj.SendMessage(RunnerMethodName.AcceptEffect, ParameterList);
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
									ApplyEffectTo(obj, _initiator, _effect);
									//obj.SendMessage(RunnerMethodName.AcceptEffect, ParameterList);
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
					//TODO �Ż���Ŀ��ѡ���п����ظ���
				case TargetOptions.MultiPlayerTargets:
					{
						if(_effect.TargetCount >= PlayerSurventUnitsList.Count + 1)
						{
							playerUnit.SendMessage(RunnerMethodName.AcceptEffect, ParameterList);
							foreach (var obj in PlayerSurventUnitsList)
							{
								//obj.SendMessage(RunnerMethodName.AcceptEffect, ParameterList);
								ApplyEffectTo(obj, _initiator, _effect);
							}
						}
						else
						{
							for(int i = 0; i < _effect.TargetCount; i++)
							{
								int rnd = Random.Range(0, PlayerSurventUnitsList.Count + 1);
								if (rnd == PlayerSurventUnitsList.Count)
								{
									ApplyEffectTo(playerUnit, _initiator, _effect);
									//playerUnit.SendMessage(RunnerMethodName.AcceptEffect, ParameterList);
								}
								else
								{
									ApplyEffectTo(PlayerSurventUnitsList[rnd], _initiator, _effect);
									//PlayerSurventUnitsList[rnd].SendMessage(RunnerMethodName.AcceptEffect, ParameterList);
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
								//obj.SendMessage(RunnerMethodName.AcceptEffect, ParameterList);
								ApplyEffectTo(obj, _initiator, _effect);
							}
						}
						else
						{
							for (int i = 0; i < _effect.TargetCount; i++)
							{
								int rnd = Random.Range(0, BossSurventUnitsList.Count + 1);
								if (rnd == BossSurventUnitsList.Count)
								{
									//bossUnit.SendMessage(RunnerMethodName.AcceptEffect, ParameterList);
									ApplyEffectTo(bossUnit, _initiator, _effect);
								}
								else
								{
									ApplyEffectTo(BossSurventUnitsList[rnd], _initiator, _effect);
									//BossSurventUnitsList[rnd].SendMessage(RunnerMethodName.AcceptEffect, ParameterList);
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
			// ����Ŀ��ѡ�񶯻�
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
		//TODO ����д�úܹ�ʺ�����Ҹ�ʱ����д
		EffectTarget = target;
		if (EffectInitiator != EffectTarget && EffectInitiator != null && EffectTarget != null)
		{
			if(CheckAttack(EffectTarget))
			{
				UnitType unitType = GetUnitType(target);
				if (effectPack.Target == TargetOptions.SinglePlayerTarget && (unitType == UnitType.Player || unitType == UnitType.PlayerSurvent))
				{
					if(InitiatorType == CardType.Survent && EffectInitiator.GetComponent<SurventUnitManager>().survent.IsDoubleHit)
					{
						ApplyEffectTo(EffectTarget, EffectInitiator, effectPack);
					}
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

		//�ر�Ŀ��ѡ�񶯻�
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
	/// ����ܷ񹥻���Ŀ��
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


	//TODO ս������
	public void GameEnd(GameResult result)
	{
		SavePlayerInformation();
		switch (result)
		{
			case GameResult.Success:
				{
					Debug.Log("��Ϸʤ��");
					Player.Instance.AddMoney(0, 2);
					//SceneManager.LoadSceneAsync("CardSelect");
					StartCoroutine(LoadNextScene());
				}
				break;
			case GameResult.Failure:
				{
					Debug.Log("��Ϸʧ��");
					PlayerDataTF.EventEnd();
					SceneManager.LoadScene("GameProcess");
				}
				break;
			case GameResult.Escape:
				{
					Debug.Log("��������");
					PlayerDataTF.EventEnd();
					SceneManager.LoadScene("GameProcess");
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

	void TestSetData() //������������
	{
		Debug.Log("Start Data Setting...");
		ArchiveManager.LoadPlayerData();

		playerUnit.SendMessage("Initialized");
		LoadPlayerInformation();
		bossUnit.SendMessage("Initialized", Resources.Load<BossSOAsset>(Const.BOSS_DATA_PATH(0)));
		Debug.Log(Const.BOSS_DATA_PATH(0));

		Debug.Log("���������������");
	}
}

