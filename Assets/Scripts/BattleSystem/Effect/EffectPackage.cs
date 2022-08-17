using UnityEngine;
/// <summary>
/// 效果传递器,用于传输和保存效果内容
/// </summary>
[System.Serializable]
public class EffectPackage
{
	public EffectType EffectType;
	[Header("一般效果区")]
	public bool IsInfinityEffect;
	public int EffectValue1;
    public int EffectValue2;
    public int EffectValue3;
	[Header("特殊效果区")]
	public string SpecialEffectScriptName;

	public EffectPackage(EffectType effectType, int effectValue1, int effectValue2, int effectRounds, string specialEffectScriptName)
	{
		EffectType = effectType;
		EffectValue1 = effectValue1;
		EffectValue2 = effectValue2;
		EffectValue3 = effectRounds;
		SpecialEffectScriptName = specialEffectScriptName;
	}
	public EffectPackage() { }
}
/// <summary>
/// 效果包,包含了效果的目标选择信息
/// </summary>
[System.Serializable]
public class EffectPackageWithTargetOption : EffectPackage
{
	[Header("目标选项")]
    public TargetOptions Target;
	[Header("只有当目标选项为单目标时才启用该项")]
	public SingleTargetOption SingleTargetOption;
	[Header("只有当目标选项为多目标且非全体目标时才启用该项")]
    public int TargetCount;

    public EffectPackageWithTargetOption(EffectType effectType, int effectValue1, int effectValue2, int effectRounds, string specialEffectScriptName, TargetOptions target, SingleTargetOption singleTargetOption, int targetCount)
			: base(effectType, effectValue1, effectValue2, effectRounds, specialEffectScriptName)
	{
		Target = target;
		SingleTargetOption = singleTargetOption;
		TargetCount = targetCount;
	}
	public EffectPackageWithTargetOption() { }
}

