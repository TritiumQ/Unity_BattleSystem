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
		Debug.Log(Boss.Name); ;
	}
	public void RefreshState()
	{
		if (Boss != null) //��ʾˢ��
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
		Debug.Log("Boss�ѻ��ܣ�ս��ʤ��");
		//ʤ����Ч
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
				if (packages.ActionEffect != null && packages.ActionEffect.EffectType != EffectType.Void)
				{
					system.ApplyEffect(gameObject, packages.ActionEffect);
				}
				else if (packages.SummonCount != 0)
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
									if (Boss.SummontList.Count > 0)
									{
										var rnd = Random.Range(0, Boss.SummontList.Count);
										system.SetupSurvent(Resources.Load<CardSOAsset>(Const.CARD_DATA_PATH(Boss.SummontList[rnd])), CardType.Monster);
									}
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

	#region Ч�����պ����нӿ�
	public void AcceptEffect(object[] _parameterList)
	{
		GameObject initiator = (GameObject)_parameterList[0];
		EffectPackage effect = (EffectPackage)_parameterList[1];
		if(effect != null)
		{
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
			string msg = Boss.Name + "���յ�Ч��" + effect.EffectType;
			Debug.Log(msg);
		}
	}
	public void UpdateEffect()
	{
		Boss.UpdateEffect();
	}
	#endregion

	#region ����Ч�����нӿ�
	public void SetupEffectTrigger() { }
	public void AdvancedEffectTrigger()
	{
		foreach (var effect in Boss.SpecialAbilityList)
		{
			if(effect.SkillType == AbilityType.�غϿ�ʼЧ��)
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
			if (effect.SkillType == AbilityType.�غϽ���Ч��)
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
			if (effect.SkillType == AbilityType.�ܻ�����)
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
