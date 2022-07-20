using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss
{
    readonly BossAsset bossAsset;
    public int id;
    public string name;
    public int maxHP;
    public int curentHP;
    public int ATK;
    public string actionLogicName;
    public Boss(BossAsset _bossAsset)
	{
        bossAsset = _bossAsset;
        if(bossAsset != null)
		{
            id = bossAsset.ID;
            name = bossAsset.Name;
            curentHP = maxHP = bossAsset.MaxHP;
            ATK = bossAsset.ATK;
            actionLogicName = bossAsset.ActionLogicName;
		}
	}
}
