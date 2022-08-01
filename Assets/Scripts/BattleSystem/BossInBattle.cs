using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossInBattle
{
    readonly BossSOAsset bossAsset;
    public int id;
    public string name;
    public int maxHP;
    public int currentHP;
    public int ATK;
    public List<BossActionType> actionCycle;
    public List<int> SurventList;
    //public string actionLogicName;
    //特殊效果区
    public int inspireRounds;
    public int inspireValue;

    //public int silenceRounds;

    //public int tauntRounds;

    public int protectTimes;
    
    //
    public BossInBattle(BossSOAsset _bossAsset)
	{
        inspireRounds = 0;
       // silenceRounds = 0;
        //tauntRounds = 0;
        protectTimes = 0;

        bossAsset = _bossAsset;
        if(bossAsset != null)
		{
            id = bossAsset.ID;
            name = bossAsset.Name;
            currentHP = maxHP = bossAsset.MaxHP;
            ATK = bossAsset.ATK;
            actionCycle = bossAsset.ActionCycle;
            SurventList = _bossAsset.SummonList;
            //actionLogicName = bossAsset.ActionLogicName;
		}
	}
}
