using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//[System.Serializable]
public class SurventInBattle : UnitInBattle
{
    //readonly CardSOAsset asset;
    public int CardID;
    public string CardName;

    public bool IsRaid { get; set; }
	public bool IsUndead { get; set; }

	//public int HitCount;
	public bool IsDoubleHit { get; set; }
	private int DoubleHitRound { get; set; }

	public bool IsSubsequent { get; set; }
	public bool IsFeedback { get; set; }
	public bool IsAdvanced { get; set; }

	public List<AbilityPackage> SpecialAbilityList;

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
			DoubleHitRound = 0;
			SurventType = _asset.CardType;
			if(_asset.IsVampire)
			{
				VampireRounds = Const.INF;
			}
			IsUndead = IsSubsequent = IsAdvanced = IsFeedback = false;
			SpecialAbilityList = _asset.SpecialAbilityList;
			foreach(var ability in _asset.SpecialAbilityList)
			{
				switch(ability.SkillType)
				{
					case AbilityType.受击反馈:
						IsFeedback = true;
						break;
					case AbilityType.回合开始效果:
						IsAdvanced = true;
						break;
					case AbilityType.回合结束效果:
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
	public override void UpdateEffect()
	{
		base.UpdateEffect();
		if (IsRaid == true)
		{
			IsRaid = false;
		}
		if(DoubleHitRound > 0)
		{
			IsDoubleHit = true;
			DoubleHitRound--;
		}
		else
		{
			IsDoubleHit = false;
		}
	}
}
