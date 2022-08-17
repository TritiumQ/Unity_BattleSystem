using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
/// <summary>
/// 储存Boss信息
/// </summary>
public class BossSOAsset : ScriptableObject
{
	[Header("基本信息")]
    public int ID;
    public string Name;
    public int MaxHP;
    public int ATK;
    public Sprite Icon;
    [Header("可召唤随从列表")]
    public List<int> SummonList;
    [Header("特殊效果列表")]
    public List<AbilityPackage> SpecialAbilityList;
    /// <summary>
    /// 储存boss的行动包
    /// </summary>
    [Header("新版行动循环")]
    public List<BossActionPackage> ActionPackages;
    /// <summary>
    /// 储存boss的行动循环
    /// </summary>
    public List<BossAction> Cycle;
	[Header("旧版行动循环")]
    public List<BossActionType> ActionCycle;
}
[System.Serializable]
public class BossAction
{
    public List<int> ActionIndex;
}
