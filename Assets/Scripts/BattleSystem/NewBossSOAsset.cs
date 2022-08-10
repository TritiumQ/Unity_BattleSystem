using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NewBossSOAsset : ScriptableObject
{
    public int ID;
    public string Name;
    public int MaxHP;
    public int ATK;
    public List<BossActionType> ActionCycle;
    public List<EffectPackageWithTargetOption> SkillList;
    public List<int> SummonList;

    //public Image HeadIcon;
}