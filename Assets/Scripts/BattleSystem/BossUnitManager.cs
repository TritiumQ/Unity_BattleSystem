using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class BossUnitManager : MonoBehaviour, IEffectRunner
{
    public BossInBattle boss;

    public TextMeshProUGUI hpText;
    //public TextMeshProUGUI bossNameText;
	public TextMeshProUGUI atkText;
	public Image headIcon;
	//public Image backGroungImage;
	public void Initialized(BossSOAsset _asset)
	{
		boss = new BossInBattle(_asset);
	}
	private void Update()
	{
		Refresh();
	}
	void Refresh()
	{
		if(boss != null) //显示刷新
		{
			hpText.text = boss.currentHP.ToString();
			atkText.text = boss.ATK.ToString();
			if (boss.currentHP <= 0)
			{
				Debug.Log("Boss已击败，战斗胜利");
				//胜利特效
				GameObject.Find("BattleSystem").GetComponent<BattleSystem>().Victory();
			}
		}
	}
	//Boss行动
	public void Action(int _CurrentRound)
	{
		BattleSystem sys = GameObject.Find("BattleSystem").GetComponent<BattleSystem>();
		int mod = _CurrentRound % boss.actionCycle.Count;
		switch (boss.actionCycle[mod])
		{
			case BossActionType.AOEAttack:
				{
					sys.playerUnitDisplay.BeAttacked(boss.ATK);
					foreach (var obj in sys.PlayerSurventUnits)
					{
						//obj.SendMessage("BeAttck", boss.ATK);
						Effect.Set(obj, EffectType.Attack, boss.ATK);
					}
				}
				break;
			case BossActionType.AOEAttackExcludePlayer:
				{
					foreach (var obj in sys.PlayerSurventUnits)
					{
						Effect.Set(obj, EffectType.Attack, boss.ATK);
						//obj.SendMessage("BeAttacked", boss.ATK);
					}
				}
				break;
			case BossActionType.SingleAttack:
				{
					Debug.Log("Boss攻击");
					BossSingleAttack rnd = (BossSingleAttack)Random.Range(0,7); //需要更好的方法从枚举中随机选取
					switch (rnd)
					{
						case BossSingleAttack.RandomTarget:
							{
								int random = Random.Range(0, sys.PlayerSurventUnits.Count + 1);
								if(random == sys.PlayerSurventUnits.Count)
								{
									Effect.Set(sys.playerUnit, EffectType.Attack, boss.ATK);
									//sys.playerUnitDisplay.BeAttacked(boss.ATK);
								}
								else
								{
									foreach (var obj in sys.PlayerSurventUnits)
									{
										//obj.SendMessage("BeAttacked", boss.ATK);
										Effect.Set(obj, EffectType.Attack, boss.ATK);
									}
								}
							}
							break;
						case BossSingleAttack.PlayerTarget:
							{
								//sys.playerUnitDisplay.BeAttacked(boss.ATK);
								Effect.Set(sys.playerUnit, EffectType.Attack, boss.ATK);
							}
							break;
						case BossSingleAttack.LowestATKTarget:
							{
								if(sys.PlayerSurventUnits.Count > 0)
								{
									var target = sys.PlayerSurventUnits[0];
									foreach (var obj in sys.PlayerSurventUnits)
									{
										if (obj != null && obj.GetComponent<SurventUnitManager>().survent.atk
											< target.GetComponent<SurventUnitManager>().survent.atk)
										{
											target = obj;
										}
									}
									Effect.Set(target, EffectType.Attack, boss.ATK);
									//target.SendMessage("BeAttacked", boss.ATK);
								}
							}
							break;
						case BossSingleAttack.HigestATKTarget:
							{
								if (sys.PlayerSurventUnits.Count > 0)
								{
									var target = sys.PlayerSurventUnits[0];
									foreach (var obj in sys.PlayerSurventUnits)
									{
										if (obj != null && obj.GetComponent<SurventUnitManager>().survent.atk
											> target.GetComponent<SurventUnitManager>().survent.atk)
										{
											target = obj;
										}
									}
									Effect.Set(target, EffectType.Attack, boss.ATK);
									//target.SendMessage("BeAttacked", boss.ATK);
								}
							}
							break;
						case BossSingleAttack.LowestHPTarget:
							{
								if (sys.PlayerSurventUnits.Count > 0)
								{
									var target = sys.PlayerSurventUnits[0];
									foreach (var obj in sys.PlayerSurventUnits)
									{
										if (obj != null && obj.GetComponent<SurventUnitManager>().survent.currentHP
											< target.GetComponent<SurventUnitManager>().survent.currentHP)
										{
											target = obj;
										}
									}
									//target.SendMessage("BeAttacked", boss.ATK);
									Effect.Set(target, EffectType.Attack, boss.ATK);
								}
							}
							break;
						case BossSingleAttack.HighestHPTarget:
							{
								if (sys.PlayerSurventUnits.Count > 0)
								{
									var target = sys.PlayerSurventUnits[0];
									foreach (var obj in sys.PlayerSurventUnits)
									{
										if (obj != null && obj.GetComponent<SurventUnitManager>().survent.currentHP
											> target.GetComponent<SurventUnitManager>().survent.currentHP)
										{
											target = obj;
										}
									}
									//target.SendMessage("BeAttacked", boss.ATK);
									Effect.Set(target, EffectType.Attack, boss.ATK);
								}
							}
							break;
						default:
							break;
					}
				}
				break;
			case BossActionType.Summon:
				{
					Debug.Log("Boss设置随从");
					int rnd = Random.Range(0, boss.SurventList.Count);
					sys.SetupBossSurvent(Resources.Load<CardSOAsset>(Const.CARD_DATA_PATH(boss.SurventList[rnd])));

				}
				break;
			case BossActionType.Skip:
				Debug.Log("Boss跳过回合");
				break;
			default:
				break;
		}
	}
	
	//受击
	public void AcceptEffect(object[] _parameterList)
	{
		GameObject initiator = (GameObject)_parameterList[0];
		EffectPackage effect = (EffectPackage)_parameterList[1];
		switch (effect.EffectType)
		{
			case EffectType.Attack:
				{
					if(boss.protectTimes > 0)
					{
						boss.protectTimes--;
					}
					else
					{
						boss.currentHP -= effect.EffectValue1;
					}
				}
				break;
			case EffectType.VampireAttack:
				{
					EffectPackage returnEffect = new EffectPackage();
					returnEffect.EffectType = EffectType.Heal;
					if (boss.protectTimes > 0)
					{
						boss.protectTimes--;
						returnEffect.EffectValue1 = 0;
					}
					else
					{
						boss.currentHP -= effect.EffectValue1;
						returnEffect.EffectValue1 = effect.EffectValue1;
					}
					if(initiator != null)
					{
						object[] parameterTable = { null, returnEffect };
						initiator.SendMessage("AcceptEffect", parameterTable);
					}
				}
				break;
			case EffectType.Heal:
				{
					if(boss.currentHP + effect.EffectValue1 <= boss.maxHP)
					{
						boss.currentHP += effect.EffectValue1;
					}
					else
					{
						boss.currentHP = boss.maxHP;
					}
				}
				break;
			case EffectType.Enhance:
				{
					boss.maxHP += effect.EffectValue1;
					boss.currentHP += effect.EffectValue1;
					boss.ATK += effect.EffectValue2;
				}
				break;
			case EffectType.Inspire:
				{
					boss.inspireList.Add(effect);
					boss.maxHP += effect.EffectValue1;
					boss.currentHP += effect.EffectValue1;
					boss.ATK += effect.EffectValue2;
				}
				break;
			default:
				break;
		}
	}

	public void UpdateEffect()
	{
		for (int i = boss.inspireList.Count - 1; i >= 0; i--)
		{
			boss.inspireList[i].EffectRounds--;
			if (boss.inspireList[i].EffectRounds <= 0)
			{
				boss.inspireList.RemoveAt(i);
			}
		}
	}
}
