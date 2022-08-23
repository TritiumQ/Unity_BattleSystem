using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class BossInBattle : UnitInBattle
{
    readonly BossSOAsset Asset;

    public int ID;
    public string Name;
    public Sprite Icon;
    public List<int> SummontList;

    public List<AbilityPackage> SpecialAbilityList;

    public List<BossActionType> ActionCycle;

    //新版行动循环
    public List<BossActionPackage> ActionPackages;
    public List<BossAction> Cycle;

    public BossInBattle(BossSOAsset asset) : base(asset.MaxHP, asset.MaxHP, asset.ATK, 0, 0, false, 0, false, 0, false, false, 0, false, 0, false)
	{
        Asset = asset;
        ID = asset.ID;
        Name = asset.Name;
        ActionCycle = asset.ActionCycle;
        SummontList = asset.SummonList;
        Icon = asset.Icon;
        SpecialAbilityList = asset.SpecialAbilityList;
        ActionPackages = asset.ActionPackages;
        Cycle = asset.Cycle;
	}
}
