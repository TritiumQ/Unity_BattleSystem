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
	void SetSurventActive(GameObject target)
	{
		Debug.Log("SetSurventActive");
		if(target.GetComponent<SurventUnitManager>() != null)
		{
			target.GetComponent<SurventUnitManager>().isActive = true;
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