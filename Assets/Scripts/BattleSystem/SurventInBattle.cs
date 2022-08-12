using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SurventInBattle : UnitInBattle, IUpdateEffectCustom
{
    //readonly CardSOAsset asset;
    public int CardID;
    public string CardName;

    public bool IsRaid;
	public bool IsUndead;
	public bool IsDoubleHit;

	public bool IsSubsequent;
	public bool IsFeedback;
	public bool IsAdvanced;

	public CardType SurventType;
	public SurventInBattle(CardSOAsset _asset) 
		: base(_asset.MaxHP, _asset.MaxHP, _asset.Atk, 0, 0, false, 0, _asset.IsTank, 0, false, false, 0, false, 0, _asset.IsVampire)
	{
       if(_asset != null)
		{
            CardID = _asset.CardID;
            CardName = _asset.CardName;
            if(_asset.IsTank)
			{
                TauntRounds = Const.INF;
			}
            IsRaid = _asset.IsRaid;
			IsDoubleHit = _asset.IsDoubleHit;
			SurventType = _asset.CardType;
			if(_asset.IsVampire)
			{
				VampireRounds = Const.INF;
			}
			IsUndead = IsSubsequent = IsAdvanced = IsFeedback = false;
			foreach(var ability in _asset.SpecialAbilityList)
			{
				switch(ability.SkillType)
				{
					case AbilityType.受击反馈:
						IsFeedback = true;
						break;
					case AbilityType.先机效果:
						IsAdvanced = true;
						break;
					case AbilityType.后手效果:
						IsSubsequent = true;
						break;
					case AbilityType.亡语效果:
						IsUndead = true;
						break;
					default:
						break;
				}
			}
		}
    }

	public void UpdateEffectCustom()
	{
		if(IsRaid == true)
		{
            IsRaid = false;
		}
	}
}
