using System.Collections;
using System.Collections.Generic;
using UnityEngine;
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
				BossUnitManager target = _targetObject.GetComponent<BossUnitManager>();
				switch (_action)
				{
					case CardActionType.Attack:
						target.BeAttacked(_value1);
						break;
					case CardActionType.VampireAttack: //吸血攻击未实现
						target.boss.curentHP -= _value1;
						//需要返回一个吸血值
						break;
					case CardActionType.Taunt:
						//boss本体无法被嘲讽?
						break;
					case CardActionType.Protect:
						//boss本体无法被加护?
						break;
					case CardActionType.Heal:
						target.BeHealed(_value1);
						break;
					case CardActionType.HPEnhance:
						target.BeEnhanced(_value1);
						break;
					case CardActionType.Inspire:
						target.BeInspired(_value1,_value2);
						break;
					case CardActionType.Waghhh:
						target.Waghhh(_value1);
						break;
					default:
						break;		
				}
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
