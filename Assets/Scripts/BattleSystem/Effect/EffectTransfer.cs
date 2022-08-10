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
}
/// <summary>
/// 效果包,包含了效果的目标选择信息
/// </summary>
[System.Serializable]
public class EffectPackageWithTargetOption : EffectPackage
{
    public TargetOptions EffectTarget;
    public int TargetCount;
}

