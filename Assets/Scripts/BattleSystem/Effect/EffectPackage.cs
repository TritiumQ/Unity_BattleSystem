using System;

/// <summary>
/// 效果传递器,用于传输效果内容
/// </summary>
[System.Serializable]
public class EffectPackage
{
    public EffectType EffectType;
    public int EffectValue1;
    public int EffectValue2;
    public int EffectRounds;

    public EffectPackage(EffectType effectType, int effectValue1, int effectValue2, int effectRounds)
	{
		EffectType = effectType;
		EffectValue1 = effectValue1;
		EffectValue2 = effectValue2;
		EffectRounds = effectRounds;
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

    public EffectPackageWithTargetOption(EffectType effectType, int effectValue1, int effectValue2, int effectRounds, TargetOptions effectTarget, int targetCount)
			: base(effectType, effectValue1, effectValue2, effectRounds)
	{
		EffectTarget = effectTarget;
		TargetCount = targetCount;
	}
	public EffectPackageWithTargetOption() { }
}

