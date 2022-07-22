using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Internal;

public static class Effect
{
	//数值1代表效果数值,如伤害值,回复量
	//数值2代表持续回合数,0为默认不持续/永久
    public static void SingleTargetAttack(GameObject _targetObject, CardActionType _action, int _value1, int _value2 = 0)
	{
		if( _targetObject != null )
		{
			if(_targetObject.GetComponent<BossUnitManager>() != null)
			{
				
				
			}
			else if(_targetObject.GetComponent<SurventUnitManager>() != null)
			{
				
			}
		}
	}
	public static void MultiTargetAttack(List<GameObject> _targets, CardActionType _action, int _value1, int _value2 = 0)
	{
		
	}
	

}
