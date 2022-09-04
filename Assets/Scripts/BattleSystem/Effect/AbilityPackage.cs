using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// 单位特殊效果包
/// </summary>
[System.Serializable]
public class AbilityPackage
{
    public AbilityType SkillType;
    public EffectPackageWithTargetOption Package;
}

/// <summary>
/// 特殊能力效果
/// </summary>
public enum AbilityType
{
	/// <summary>
	/// 受到伤害时触发
	/// </summary>
	受击反馈,
	/// <summary>
	/// 随从单位专用,放置至场上时触发
	/// </summary>
	先机效果,
	/// <summary>
	/// 每回合开始时触发
	/// </summary>
	回合开始效果,
	/// <summary>
	/// 每回合结束时触发
	/// </summary>
	回合结束效果,
	/// <summary>
	/// 单位死亡时调用
	/// </summary>
	亡语效果
}