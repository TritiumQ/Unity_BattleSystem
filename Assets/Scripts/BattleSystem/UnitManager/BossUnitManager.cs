using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class BossUnitManager : MonoBehaviour, IUnitRunner, IEffectRunner, IAbilityRunner
{
	BattleSystem system;

    public BossInBattle Boss;
    public TextMeshProUGUI HpText;
	public TextMeshProUGUI AtkText;
	public Image HeadIcon;

	private void Awake()
	{
		system = GameObject.Find(FightSceneObjectName.BattleSystem).GetComponent<BattleSystem>();
	}
	private void Update()
	{
		RefreshState();
	}
	public void Initialized(BossSOAsset _asset)
	{
		Boss = new BossInBattle(_asset);
	}
	public void RefreshState()
	{
		if (Boss != null) //显示刷新
		{
			HpText.text = Boss.CurrentHP.ToString();
			AtkText.text = Boss.ATK.ToString();
			HeadIcon.sprite = Boss.Icon;
			if (Boss.CurrentHP <= 0)
			{
				Die();
			}
		}
	}

	public void Die()
	{
		Debug.Log("Boss已击败，战斗胜利");
		//胜利特效
		system.GameEnd(GameResult.Success);
	}

	public void AutoAction(int currentRound)
	{
		if(Boss.Cycle.Count == 0) return;
		int mode = currentRound % Boss.Cycle.Count;
		foreach(var idx in Boss.Cycle[mode].ActionIndex)
		{
			if(idx < Boss.ActionPackages.Count)
			{
				var packages = Boss.ActionPackages[idx];
				if (packages.ActionEffect != null)
				{
					if(packages.ActionEffect.EffectType != EffectType.Void)
					{
						system.ApplyEffect(gameObject, packages.ActionEffect);
					}
				}
				if (packages.SummonCount != 0)
				{
					switch (packages.SummonMode)
					{
						case BossSummonMode.Specific:
							{
								for (int i = 0; i < packages.SummonCount; i++)
								{
									system.SetupSurvent(Resources.Load<CardSOAsset>(Const.CARD_DATA_PATH(packages.SummonSurventID)), CardType.Monster);
								}
							}
							break;
						case BossSummonMode.Random:
							{
								for (int i = 0; i < packages.SummonCount; i++)
								{
									var rnd = Random.Range(0, Boss.SurventList.Count);
									system.SetupSurvent(Resources.Load<CardSOAsset>(Const.CARD_DATA_PATH(rnd)), CardType.Monster);
								}
							}
							break;
						default:
							break;
					}

				}
			}
		}

	}

	public void _Action(int currentRound)
	{
		/*BattleSystem sys = GameObject.Find("BattleSystem").GetComponent<BattleSystem>();
		int mod = currentRound % Boss.ActionCycle.Count;
		//修改boss行动
		switch (Boss.ActionCycle[mod])
		{
			case BossActionType.AOEAttack:
				{
					//EffectPackageWithTargetOption package = new EffectPackageWithTargetOption(EffectType.Attack, Boss.ATK, 0, 0, null, TargetOptions.AllPlayerCreatures, 0);
					//sys.ApplyEffect(this.gameObject, package);
				}
				break;
			case BossActionType.AOEAttackExcludePlayer:
				{
					//EffectPackageWithTargetOption package = new EffectPackageWithTargetOption(EffectType.Attack, Boss.ATK, 0, 0, null, TargetOptions.ALlPlayerCharacter, 0);
					//sys.ApplyEffect(this.gameObject, package);
				}
				break;
			case BossActionType.SingleAttack:
				{
					Debug.Log("Boss单体攻击");

					var TypeArr = System.Enum.GetValues(typeof(SingleTargetOption));
					SingleTargetOption rnd = (SingleTargetOption)TypeArr.GetValue(Random.Range(0, TypeArr.Length));
					switch (rnd)
					{
						case SingleTargetOption.RandomTarget:
							{

							}
							break;
						case SingleTargetOption.LowestATKTarget:
							{
								if (sys.PlayerSurventUnitsList.Count > 0)
								{
									var target = sys.PlayerSurventUnitsList[0];
									foreach (var obj in sys.PlayerSurventUnitsList)
									{
										if (obj != null && obj.GetComponent<SurventUnitManager>().survent.ATK
											< target.GetComponent<SurventUnitManager>().survent.ATK)
										{
											target = obj;
										}
									}
									Effect.Set(target, EffectType.Attack, Boss.ATK);
									//target.SendMessage("BeAttacked", boss.ATK);
								}
							}
							break;
						case SingleTargetOption.HigestATKTarget:
							{
								if (sys.PlayerSurventUnitsList.Count > 0)
								{
									var target = sys.PlayerSurventUnitsList[0];
									foreach (var obj in sys.PlayerSurventUnitsList)
									{
										if (obj != null && obj.GetComponent<SurventUnitManager>().survent.ATK
											> target.GetComponent<SurventUnitManager>().survent.ATK)
										{
											target = obj;
										}
									}
									Effect.Set(target, EffectType.Attack, Boss.ATK);
									//target.SendMessage("BeAttacked", boss.ATK);
								}
							}
							break;
						case SingleTargetOption.LowestHPTarget:
							{
								if (sys.PlayerSurventUnitsList.Count > 0)
								{
									var target = sys.PlayerSurventUnitsList[0];
									foreach (var obj in sys.PlayerSurventUnitsList)
									{
										if (obj != null && obj.GetComponent<SurventUnitManager>().survent.CurrentHP
											< target.GetComponent<SurventUnitManager>().survent.CurrentHP)
										{
											target = obj;
										}
									}
									//target.SendMessage("BeAttacked", boss.ATK);
									Effect.Set(target, EffectType.Attack, Boss.ATK);
								}
							}
							break;
						case SingleTargetOption.HighestHPTarget:
							{
								if (sys.PlayerSurventUnitsList.Count > 0)
								{
									var target = sys.PlayerSurventUnitsList[0];
									foreach (var obj in sys.PlayerSurventUnitsList)
									{
										if (obj != null && obj.GetComponent<SurventUnitManager>().survent.CurrentHP
											> target.GetComponent<SurventUnitManager>().survent.CurrentHP)
										{
											target = obj;
										}
									}
									//target.SendMessage("BeAttacked", boss.ATK);
									Effect.Set(target, EffectType.Attack, Boss.ATK);
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
					int rnd = Random.Range(0, Boss.SurventList.Count);
					//sys.SetupBossSurvent(Resources.Load<CardSOAsset>(Const.CARD_DATA_PATH(Boss.SurventList[rnd])));

				}
				break;
			case BossActionType.Skip:
				Debug.Log("Boss跳过回合");
				break;
			default:
				break;
		}*/
	}

	public Image maskImage;
	IEnumerator MaskMiss(float times, Color color)
	{
		maskImage.enabled = true;
		color.a = 0.5f;
		maskImage.color = color;
		while (times > 0)
		{
			times -= Time.deltaTime;
			yield return new WaitForSeconds(Time.deltaTime);
		}
		maskImage.enabled = false;
	}

	#region 效果接收和运行接口
	public void AcceptEffect(object[] _parameterList)
	{
		GameObject initiator = (GameObject)_parameterList[0];
		EffectPackage effect = (EffectPackage)_parameterList[1];
		switch (effect.EffectType)
		{
			case EffectType.Attack:
				{
					Boss.BeAttacked(effect.EffectValue1);
				}
				break;
			case EffectType.VampireAttack:
				{
					int dmg = Boss.BeAttacked(effect.EffectValue1);
					if (initiator != null && system != null)
					{
						EffectPackage returnEffect = new EffectPackage();
						returnEffect.EffectType = EffectType.Heal;
						returnEffect.EffectValue1 = dmg;
						system.ApplyEffectTo(initiator, gameObject, returnEffect);
					}
				}
				break;
			case EffectType.Heal:
				Boss.BeHealed(effect.EffectValue1);
				break;
			case EffectType.Enhance:
				Boss.SetEnhance(effect.EffectValue1, effect.EffectValue2);
				break;
			case EffectType.Inspire:
				Boss.SetInspire(effect);
				break;
			default:
				break;
		}
		string msg = Boss.Name + "接收到效果" + effect.EffectType;
		Debug.Log(msg);
	}
	public void UpdateEffect()
	{
		Boss.UpdateEffect();
	}
	#endregion

	#region 特殊效果运行接口
	public void SetupEffectTrigger() { }
	public void AdvancedEffectTrigger()
	{
		foreach (var effect in Boss.SpecialAbilityList)
		{
			if(effect.SkillType == AbilityType.先机效果)
			{
				if(system != null)
				{
					system.ApplyEffect(this.gameObject, effect.Package);
				}
			}
		}
	}
	public void SubsequentEffectTrigger()
	{
		foreach (var effect in Boss.SpecialAbilityList)
		{
			if (effect.SkillType == AbilityType.后手效果)
			{
				if (system != null)
				{
					system.ApplyEffect(this.gameObject, effect.Package);
				}
			}
		}
	}
	public void FeedbackEffectTrigger()
	{
		foreach (var effect in Boss.SpecialAbilityList)
		{
			if (effect.SkillType == AbilityType.受击反馈)
			{
				if (system != null)
				{
					system.ApplyEffect(this.gameObject, effect.Package);
				}
			}
		}
	}
	public void UndeadEffectTrigger() { }
	#endregion
}
