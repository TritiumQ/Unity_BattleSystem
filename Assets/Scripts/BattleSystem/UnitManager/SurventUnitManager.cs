using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class SurventUnitManager : MonoBehaviour, IUnitRunner, IEffectRunner, IAbilityRunner
{

    BattleSystem system;
    public SurventInBattle survent { get; private set; }
    public bool isActive { get; set; }

    [Header("卡牌信息")]
    public TextMeshProUGUI atkText;
    public TextMeshProUGUI hpText;
    [Header("卡牌背景图")]
    public Image cardImage;
	[Header("活跃状态特效")]
    public Image activeGlowImage;
	[Header("特殊效果图标")]
    public Image TauntImage;  //嘲讽
    public Image RaidImage;  //快速
    public Image UndeadImage;  //亡语
    public Image VampireImage;  //吸血
    public Image ProtectedImage;  //加护
    public Image ConcealImage;  //隐匿
    public Image DoubleHitImage;  //连击
	private void Awake()
	{
		system = GameObject.Find(FightSceneObjectName.BattleSystem).GetComponent<BattleSystem>();
    }
    private void Update()
    {
        RefreshState();
    }
    public void Initialized(CardSOAsset _card)
    {
        if(_card != null)
		{
            survent = new SurventInBattle(_card);
            if (_card.CardType == CardType.Survent || _card.CardType == CardType.Monster)
            {
                isActive = _card.IsRaid;
                activeGlowImage.enabled = false;

                cardImage.sprite = _card.CardImage;
                TauntImage.enabled = false;
                RaidImage.enabled = false;
                UndeadImage.enabled = false;
                VampireImage.enabled = false;
                ProtectedImage.enabled = false;
                ConcealImage.enabled = false;
                DoubleHitImage.enabled = false;
            }
        }
    }
	public void RefreshState()  //刷新随从当前状态
    {
        if(survent != null)
		{
            atkText.text = survent.ATK.ToString();
            hpText.text = survent.CurrentHP.ToString();
            if (survent.CurrentHP <= 0)
            {
                Die();
            }
            activeGlowImage.enabled = isActive;
            TauntImage.enabled = survent.IsTank;
            VampireImage.enabled = survent.IsVampire;
            ConcealImage.enabled = survent.IsConcealed;
            UndeadImage.enabled = survent.IsUndead;
            DoubleHitImage.enabled = survent.IsDoubleHit;
            ProtectedImage.enabled = survent.IsProtected;
            RaidImage.enabled = survent.IsRaid;
        }
        if (survent.CurrentHP <= 0) 
		{
            Die();
		}
    }
    public void AutoAction(int currentRound)
    {
        if(survent.SurventType == CardType.Monster)
		{
            //敌方随从自动行动
            EffectPackageWithTargetOption effect
                = new EffectPackageWithTargetOption(EffectType.Attack, survent.ATK, 0, 0, null, TargetOptions.SinglePlayerTarget, SingleTargetOption.RandomTarget, 0);
            system.ApplyEffect(gameObject,effect);
		}
    }
    public void Die()
	{
        //死亡动画
        string msg = survent.CardName + "死亡";
        Debug.Log(msg);
        UndeadEffectTrigger();
        system.SurventUnitDie(gameObject);
	}

    //接受效果接口组
    public void AcceptEffect(object[] _parameterList)
    {
        
        GameObject initiator = (GameObject)_parameterList[0];
        EffectPackage effect = (EffectPackage)_parameterList[1];
        switch (effect.EffectType)
		{
            case EffectType.Attack:
				{
                    survent.BeAttacked(effect.EffectValue1);
                    if (initiator != null && system != null)
                    {
                        EffectPackage returnEffect = new EffectPackage();
                        returnEffect.EffectType = EffectType.FeedbackAttack;
                        returnEffect.EffectValue1 = survent.ATK;
                        system.ApplyEffectTo(initiator, gameObject, returnEffect);
                    }
                }
                break;
            case EffectType.VampireAttack:
				{
                    int dmg = survent.BeAttacked(effect.EffectValue1);
                    if (initiator != null && system != null)
                    {
                        EffectPackage returnEffect1 = new EffectPackage();
                        returnEffect1.EffectType = EffectType.FeedbackAttack;
                        returnEffect1.EffectValue1 = survent.ATK;
                        EffectPackage returnEffect2 = new EffectPackage();
                        returnEffect2.EffectType = EffectType.Heal;
                        returnEffect2.EffectValue1 = dmg;
                        system.ApplyEffectTo(initiator, gameObject, returnEffect1);
                        system.ApplyEffectTo(initiator, gameObject, returnEffect2);
                    }
				}
                break;
            case EffectType.FeedbackAttack:
				{
                    survent.BeAttacked(effect.EffectValue1);
				}
                break;
            case EffectType.Heal:
				{
                    survent.BeHealed(effect.EffectValue1);
				}
				break;
            case EffectType.Taunt:
				{
                    survent.SetTaunt(effect.IsInfinityEffect ? Const.INF : effect.EffectValue1);
                }
				break;
            case EffectType.Protect:
				{
                    survent.SetProtected(effect.EffectValue1);
				}
				break;
            case EffectType.Conceal:
				{
                    survent.SetConceal(effect.IsInfinityEffect ? Const.INF : effect.EffectValue1);
				}
                break;
            case EffectType.Enhance:
				{
                    survent.SetEnhance(effect.EffectValue1, effect.EffectValue2);
				}
                break;
            case EffectType.Inspire:
				{
                    survent.SetInspire(effect);
				}
                break;
            case EffectType.SetVampire:
				{
                    survent.SetVmapire(effect.IsInfinityEffect ? Const.INF : effect.EffectValue1);
				}
                break;
            case EffectType.SetDoubleHit:
				{
                    survent.SetDoubleHit(effect.IsInfinityEffect ? Const.INF : effect.EffectValue1);
				}
                break;
            default:
                break;
		}
        string msg = survent.CardName + "接收到效果:" + effect.EffectType;
        Debug.Log(msg);
    }

    public void UpdateEffect()
    {
        isActive = true;
        survent.UpdateEffect();
    }

    #region 特殊能力效果接口组

    public void AdvancedEffectTrigger()
    {
        foreach (var effect in survent.SpecialAbilityList)
		{
            if(effect.SkillType == AbilityType.回合开始效果)
			{
                var eft = effect.Package;
                if (system != null) 
				{
                    //system.ApplyEffect(gameObject, eft);
                    system.AddExtraAction(gameObject, eft);
                }
			}
		}
    }
    public void FeedbackEffectTrigger()
    {
        foreach (var effect in survent.SpecialAbilityList)
        {
            if (effect.SkillType == AbilityType.受击反馈)
            {
                var eft = effect.Package;
                if (system != null)
                {
                    //system.ApplyEffect(gameObject, eft);
                    system.AddExtraAction(gameObject, eft);
                }
            }
        }
    }
    public void SetupEffectTrigger()
    {
        foreach (var effect in survent.SpecialAbilityList)
        {
            if (effect.SkillType == AbilityType.先机效果)
            {
                var eft = effect.Package;
                if (system != null)
				{
                    //system.ApplyEffect(gameObject, eft);
                    system.AddExtraAction(gameObject, eft);
                }
            }
        }
    }
    public void SubsequentEffectTrigger()
    {
        foreach (var effect in survent.SpecialAbilityList)
        {
            if (effect.SkillType == AbilityType.回合结束效果)
            {
                var eft = effect.Package;
				if (system != null)
				{
                    //system.ApplyEffect(gameObject, eft);
                    system.AddExtraAction(gameObject, eft);
                }
            }
        }
    }
    public void UndeadEffectTrigger()
    {
        foreach (var effect in survent.SpecialAbilityList)
        {
            var eft = effect.Package;
            if (effect.SkillType == AbilityType.亡语效果)
            {
                //system.ApplyEffect(gameObject, eft);
                system.AddExtraAction(gameObject, eft);
            }
        }
    }
	#endregion

    void ActionComplete()
	{
        isActive = false;
	}
}
