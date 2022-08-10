using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class Effect
{
	/// <summary>
	/// 优化版方法已实现，该方法弃用
	/// </summary>
	/// <return></return>
    public static void Set(GameObject _targetObject, EffectType _action, int _value1, int _value2 = 0, GameObject _requester = null)
	{
		/*if( _targetObject != null )
		{
			string msg = "对" + _targetObject.name + "发起攻击";
			Debug.Log(msg);
			if(_targetObject.GetComponent<BossUnitManager>() != null)
			{
				BossUnitManager target = _targetObject.GetComponent<BossUnitManager>();
				switch (_action)
				{
					case EffectType.Attack:
						target.BeAttacked(_value1);
						break;
					case EffectType.VampireAttack: //吸血攻击
						if(_requester != null)
						{
							int damage = target.BeAttacked( _value1);
							Set(_requester,EffectType.Heal,damage);
						}
						else
						{
							Debug.LogWarning("错误调用吸血攻击，吸血攻击需主体对象");
						}
						break;
					case EffectType.Heal:
						target.BeHealed(_value1);
						break;
					//case CardActionType.HPEnhance:
						//target.BeEnhanced(_value1);
						//break;
					case EffectType.Inspire:
						target.BeInspired(_value1,_value2);
						break;
					//case CardActionType.Waghhh:
						//target.Waghhh(_value1);
						//break;
					case EffectType.Conceal:
						//boss本体无法被隐匿
					case EffectType.Taunt:
						//boss本体无法被嘲讽
					case EffectType.Protect:
						//boss本体无法被加护
					default:
						break;		
				}
			}
			else if(_targetObject.GetComponent<SurventUnitManager>() != null)
			{
				SurventUnitManager target = _targetObject.GetComponent<SurventUnitManager>();
				switch (_action)
				{
					case EffectType.Attack:
						target.BeAttacked(_value1);
						break;
					case EffectType.VampireAttack:
						if (_requester != null)
						{
							int damage = target.BeAttacked(_value1);
							Set(_requester, EffectType.Heal, damage);
						}
						else
						{
							Debug.LogWarning("错误调用吸血攻击，吸血攻击需主体对象");
						}
						break;
					case EffectType.Heal:
						target.BeHealed(_value1);
						break;
					//case CardActionType.HPEnhance:
						//target.BeEnhanced(_value1);
						//break;
					case EffectType.Inspire:
						target.BeInspired(_value1, _value2);
						break;
					//case CardActionType.Waghhh:
						//target.Waghhh(_value1);
						//break;
					case EffectType.Conceal:
						target.BeConcealed(_value1);
						break;
					case EffectType.Taunt:
						target.BeTaunted(_value1);
						break;
					case EffectType.Protect:
						target.BeProtected(_value1);
						break;
					default:
						break;
				}
			}
			else if(_targetObject.GetComponent<PlayerUnitManager>() != null)
			{
				PlayerUnitManager target = _targetObject.GetComponent<PlayerUnitManager>();
				switch (_action)
				{
					case EffectType.Attack:
						target.BeAttacked(_value1);
						break;
					case EffectType.VampireAttack:
						if (_requester != null)
						{
							int damage = target.BeAttacked(_value1);
							Set(_requester, EffectType.Heal, damage);
						}
						else
						{
							Debug.LogWarning("错误调用吸血攻击，吸血攻击需主体对象");
						}
						break;
					case EffectType.Protect:
						target.BeProtected(_value1);
						break;
					case EffectType.Heal:
						target.BeHealed(_value1);
						break;
					//case CardActionType.HPEnhance:
					case EffectType.Inspire:
					//case CardActionType.Waghhh:
					case EffectType.Conceal:
					case EffectType.Taunt:
					default:
						break;
				}
			}
		}*/
	}
	
	/// <summary>
	/// 释放效果的通用方法
	/// </summary>
	/// <param name="_target">效果目标</param>
	/// <param name="_initiator">效果发起者</param>
	/// <param name="_effect">效果传递器</param>
	public static void ApplyTo(GameObject _target, GameObject _initiator, EffectPackage _effect)
	{
		if(_target != null && _initiator != null)
		{
			//效果发送
			object[] ParameterList = { _initiator, _effect };
			_target.SendMessage("AcceptEffect", ParameterList);
		}
	}
}
