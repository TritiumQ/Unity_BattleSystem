using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SurventInBattle
{
    public Card card;
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
    public int deadWhisperEffectValue;
    public TargetOptions deadWhisperTarget;

    public bool isAdvanced; //先机
    public CardActionType advancedEffect;
    public int advancedEffectValue;
    public TargetOptions advancedEffectTarget;

    public bool isSubsequent; //后手
    public CardActionType subsequentEffect;
    public int subsequentEffectValue;
    public TargetOptions subsequentEffectTarget;
    //随从放置时效果
    public bool IsSetupEffect;
    public CardActionType SetupEffect;
    public int SetupEffectValue;
    public TargetOptions SetupEffectTarget;


    public SurventInBattle(Card _card)
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
        //TODO 改用CardSOAsset创建对象
	}
}
