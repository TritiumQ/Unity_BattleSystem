using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SurventInBattle
{
    //public Card card;
    //public CardSOAsset asset;

    public int atk;
    public int currentHP;
    public int maxHP;

    //特殊效果区
    public bool isRaid;
    public int inspireRounds;
    public int inspireValue;
    public int concealRounds;
    //public int silenceRounds;
    public int tauntRounds;
    public int protectedTimes;
    public int doubleHitRounds;
   
    //public bool isVampire;
    public int vampireRounds;

    public bool isUndead; //亡语
    public CardActionType deadWhisperEffect;
    public int deadWhisperEffectValue1;
    public int deadWhisperEffectValue2;
    public TargetOptions deadWhisperTarget;

    public bool isAdvanced; //先机
    public CardActionType advancedEffect;
    public int advancedEffectValue1;
    public int advancedEffectValue2;
    public TargetOptions advancedEffectTarget;

    public bool isSubsequent; //后手
    public CardActionType subsequentEffect;
    public int subsequentEffectValue1;
    public int subsequentEffectValue2;
    public TargetOptions subsequentEffectTarget;
    //随从放置时效果
    public bool IsSetupEffect;
    public CardActionType SetupEffect;
    public int SetupEffectValue1;
    public int SetupEffectValue2;
    public TargetOptions SetupEffectTarget;


    public SurventInBattle(Card _card)
    //弃用
    {
        this.card = _card;
        atk = card.atk;
        maxHP = card.maxHP;
        currentHP = maxHP;

        isRaid = card.isRaid;

        if(card.isTank == true)
		{
            //Debug.Log("坦克卡");
            tauntRounds = Const.Forever;
		}
        else
		{
            tauntRounds = 0;
		}

        inspireRounds = 0;
        inspireValue = 0;
        concealRounds = 0;
        protectedTimes = 0;
        doubleHitRounds = 0;

        if(card.IsVampire == true)
		{
            vampireRounds = Const.Forever;
		}
        else
		{
            vampireRounds = 0;
        }

        isUndead = card.isUndead;
        deadWhisperEffect = card.deadWhisperEffect;
        deadWhisperEffectValue = card.deadWhisperEffectValue;

        isSubsequent = card.isSubsequent;
        subsequentEffect = card.subsequentEffect;
        subsequentEffectValue = card.subsequentEffectValue;

        isAdvanced = card.isAdvanced;
        advancedEffect = card.advancedEffect;
        advancedEffectValue = card.advancedEffectValue;

        IsSetupEffect = card.IsSetupEffect;
        SetupEffect = card.SetupEffect;
        SetupEffectValue = card.SetupEffectValue;
        SetupEffectTarget = card.SetupEffectTarget;
        
	}
    public SurventInBattle(CardSOAsset _asset)
	{
        //改用CardSOAsset创建对象
        atk = _asset.Atk;
        currentHP =  maxHP = _asset.MaxHP;
        isRaid = _asset.IsRaid;

        if(_asset.IsTank)
		{
            tauntRounds = Const.Forever;
		}
        else
		{
            tauntRounds = 0;
		}

        inspireRounds = 0;
        inspireValue = 0;
        concealRounds = 0;
        protectedTimes = 0;
        doubleHitRounds = 0;

        if (_asset.IsVampire == true)
        {
            vampireRounds = Const.Forever;
        }
        else
        {
            vampireRounds = 0;
        }

        isUndead = _asset.IsUndead;
        deadWhisperEffect = _asset.DeadWhisperEffect;
        deadWhisperEffectValue1 = _asset.DeadWhisperEffectValue1;
        deadWhisperEffectValue2 = _asset.DeadWhisperEffectValue2;
        deadWhisperTarget = _asset.DeadWhisperEffectTarget;

        isAdvanced = _asset.IsAdvanced;
        advancedEffect = _asset.AdvancedEffect;
        advancedEffectTarget = _asset.AdvancedEffectTarget;
        advancedEffectValue1 = _asset.AdvancedEffectValue1;
        advancedEffectValue2 = _asset.AdvancedEffectValue2;

        isSubsequent = _asset.IsSubsequent;
        subsequentEffect = _asset.SubsequentEffect;
        subsequentEffectTarget = _asset.SubsequentEffectTarget;
        subsequentEffectValue1 = _asset.SubsequentEffectValue1;
        subsequentEffectValue2 = _asset.SubsequentEffectValue2;

        IsSetupEffect = _asset.IsSetupEffect;
        SetupEffect = _asset.SetupEffect;
        SetupEffectTarget = _asset.SetupEffectTarget;
        SetupEffectValue1 = _asset.SetupEffectValue1;
        SetupEffectValue2 = _asset.SetupEffectValue2;


    }
}
