using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class SurventUnitManager : MonoBehaviour, IUnitRunner, IEffectRunner, IAbilityRunner
{

    BattleSystem system;
    public SurventInBattle survent { get; private set; }
    public bool isActive { get; private set; }

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
		system = GameObject.Find("BatttleSystem").GetComponent<BattleSystem>();
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
            //TODO 敌方随从自动行动

		}
    }
    public void Die()
	{
        //死亡动画
        //string msg = survent..cardName + "死亡";
        //Debug.Log(msg);
        //亡语效果

        GameObject.Find("BattleSystem").GetComponent<BattleSystem>().SurventUnitDie(gameObject);
        Destroy(gameObject);
	}

    //TODO 随从接口组
    public void AcceptEffect(object[] _parameterList)
    {
        GameObject initiator = (GameObject)_parameterList[0];
        EffectPackage effect = (EffectPackage)_parameterList[1];
        switch (effect.EffectType)
		{
            case EffectType.Attack:
				{
                    EffectPackage returnEffect1 = new EffectPackage();
                    returnEffect1.EffectType = EffectType.Attack;
                    returnEffect1.EffectValue1 = survent.ATK;
                    EffectPackage returnEffect2 = new EffectPackage();
                    returnEffect2.EffectType = EffectType.Heal;
                    returnEffect2.EffectValue1 = survent.BeAttacked(effect.EffectValue1);
                    system.ApplyEffectTo(initiator, gameObject, returnEffect1);
                    system.ApplyEffectTo(initiator, gameObject, returnEffect2);
                }
                break;
            case EffectType.VampireAttack:
				{
                    EffectPackage returnEffect = new EffectPackage();
                    returnEffect.EffectType = EffectType.Heal;
                    returnEffect.EffectValue1 = survent.BeAttacked(effect.EffectValue1);
                    system.ApplyEffectTo(initiator, gameObject, returnEffect);
				}
                break;
            case EffectType.Heal:
				{
                    survent.BeHealed(effect.EffectValue1);
				}
				break;
            case EffectType.Taunt:

				break;
            case EffectType.Protect:
				break;
            case EffectType.Conceal:
                break;
            case EffectType.Enhance:
                break;
            case EffectType.Inspire:
                break;
            default:
                break;
		}
    }

    public void UpdateEffect()
    {
        survent.UpdateEffect();
    }

    #region 特殊能力效果接口组
    public void AdvancedEffectTrigger()
    {
        isActive = true;

    }

    public void FeedbackEffectTrigger()
    {
        
    }

    public void SetupEffectTrigger()
    {
        
    }

    public void SubsequentEffectTrigger()
    {
        
    }

    public void UndeadEffectTrigger()
    {
        
    }

	#endregion

}
