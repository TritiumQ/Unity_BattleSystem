using UnityEngine;
/// <summary>
/// 效果传递器,用于传输和保存效果内容
/// </summary>
[System.Serializable]
public class EffectPackage
{
	public EffectType EffectType;
	[Header("一般效果区")]
    public int EffectValue1;
    public int EffectValue2;
	public bool IsInfinity;
    public int EffectRounds;
	[Header("特殊效果区")]
	public string SpecialEffectScriptName;

	public EffectPackage(EffectType effectType, int effectValue1, int effectValue2, int effectRounds, string specialEffectScriptName)
	{
		EffectType = effectType;
		EffectValue1 = effectValue1;
		EffectValue2 = effectValue2;
		EffectRounds = effectRounds;
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
    public TargetOptions EffectTarget;
    public int TargetCount;

    public EffectPackageWithTargetOption(EffectType effectType, int effectValue1, int effectValue2, int effectRounds, string specialEffectScriptName, TargetOptions effectTarget, int targetCount)
			: base(effectType, effectValue1, effectValue2, effectRounds, specialEffectScriptName)
	{
		EffectTarget = effectTarget;
		TargetCount = targetCount;
	}
	public EffectPackageWithTargetOption() { }
}

