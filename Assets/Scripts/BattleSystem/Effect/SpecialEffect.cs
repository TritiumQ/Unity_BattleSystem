using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 特殊触发技能效果
/// </summary>
public enum SpecialSkillType
{
    /// <summary>
    /// 受到伤害时触发
    /// </summary>
    受击反馈,
    /// <summary>
    /// 随从单位专用,放置至场上时触发
    /// </summary>
    放置效果,
    /// <summary>
    /// 每回合开始时触发
    /// </summary>
    先机效果,
    /// <summary>
    /// 每回合结束时触发
    /// </summary>
    后手效果,
}
[System.Serializable]
public class SpecialSkillPackage
{
    public SpecialSkillType SkillType;
    public EffectPackageWithTargetOption Package;
}