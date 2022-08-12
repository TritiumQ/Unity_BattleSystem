using UnityEngine;
using System.Collections.Generic;
/// <summary>
/// BOSS行动包，每一个代表boss的一次行动
/// </summary>
[System.Serializable]
public class BossActionPackage
{
	//public NewBossActionType ActionType;
	[Header("效果（包括普通攻击）")]
	public EffectPackageWithTargetOption Effect;
	[Header("技能")]
	public string SkillScriptName;
	public TargetOptions TargetOptions;
	public int SkillTargetCount;
	[Header("召唤")]
	public BossSummonMode SummonMode;
	public int SummonCount;
	public int SummonSurventID;
}
public enum BossSummonMode
{
	Specific,
	Random,
}
