using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// ����Ч��������
/// </summary>
public class SpecialEffectManager : MonoBehaviour
{
	#region ����Ч������������������SpecialEffectRegistry������һ����̬�ַ���
	/// <summary>
	/// ʹ1������ٴξ���
	/// </summary>
	/// <param name="parameterTable">������[0]Ϊ�����ߣ�[1]ΪĿ�꣬ [2]ΪЧ����</param>
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
	/// Enhance����*����*�����
	/// </summary>
	/// <param name="parameterTable">������[0]Ϊ�����ߣ�[1]ΪĿ�꣬ [2]ΪЧ����</param>
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
	/// Enhance����*����*�����
	/// </summary>
	/// <param name="parameterTable">������[0]Ϊ�����ߣ�[1]ΪĿ�꣬ [2]ΪЧ����</param>
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
/// ���ڱ�������Ч��������
/// </summary>
public static class SpecialEffectRegistry
{
	
}