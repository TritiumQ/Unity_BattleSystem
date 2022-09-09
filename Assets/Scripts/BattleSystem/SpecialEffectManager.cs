using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// 特殊效果管理器
/// </summary>
public class SpecialEffectManager : MonoBehaviour
{
	#region 特殊效果，方法名必须先在SpecialEffectRegistry中声明一个静态字符串
	/// <summary>
	/// 使1名随从再次就绪
	/// </summary>
	/// <param name="parameterTable">参数表，[0]为发起者，[1]为目标， [2]为效果包</param>
	void SetSurventActive(object[] parameterTable)
	{
		Debug.Log("SetSurventActive");
		GameObject target = null;
		EffectPackage effect = null;
		if (parameterTable != null)
		{
			target = (GameObject)parameterTable[1];
			effect = (EffectPackage)parameterTable[2];
		}
		if (target.GetComponent<SurventUnitManager>() != null)
		{
			target.GetComponent<SurventUnitManager>().isActive = true;
		}
	}

	/// <summary>
	/// Enhance具有*嘲讽*的随从
	/// </summary>
	/// <param name="parameterTable">参数表，[0]为发起者，[1]为目标， [2]为效果包</param>
	void EnhanceTank(object[] parameterTable)
	{
		GameObject target = null;
		EffectPackage effect = null;
		if(parameterTable != null)
		{
			target = (GameObject)parameterTable[1];
			effect = (EffectPackage)parameterTable[2];
;		}
		if(target.GetComponent<SurventUnitManager>() != null && target != null && effect != null)
		{
			if(target.GetComponent<SurventUnitManager>().survent.IsTank)
			{
				target.GetComponent<SurventUnitManager>().survent.SetEnhance(effect.EffectValue1, effect.EffectValue2);
			}
		}
	}

	/// <summary>
	/// Enhance具有*隐匿*的随从
	/// </summary>
	/// <param name="parameterTable">参数表，[0]为发起者，[1]为目标， [2]为效果包</param>
	void EnhanceConceal(object[] parameterTable)
	{
		GameObject target = null;
		EffectPackage effect = null;
		if (parameterTable != null)
		{
			target = (GameObject)parameterTable[1];
			effect = (EffectPackage)parameterTable[2];
			;
		}
		if (target.GetComponent<SurventUnitManager>() != null && target != null && effect != null)
		{
			if (target.GetComponent<SurventUnitManager>().survent.IsConcealed)
			{
				target.GetComponent<SurventUnitManager>().survent.SetEnhance(effect.EffectValue1, effect.EffectValue2);
			}
		}
	}

	#endregion
}

/// <summary>
/// 用于保存特殊效果方法名
/// </summary>
public static class SpecialEffectRegistry
{
	
}