using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
public class PlayerUnitManager : MonoBehaviour, IUnitRunner, IEffectRunner
{
	BattleSystem system;
	public PlayerInBattle player { get; private set; }

    public TextMeshProUGUI hpText;
    public TextMeshProUGUI actionPointText;
	public Image headIcon;
	
	private void Awake()
	{
		system = GameObject.Find(FightSceneObjectName.BattleSystem).GetComponent<BattleSystem>();
	}
	private void Update()
	{
		RefreshState();
	}

	/// <summary>
	/// 初始化
	/// </summary>
	public void Initialized()
	{
		player = new PlayerInBattle(Player.Instance);
	}
	/// <summary>
	/// 结算
	/// </summary>
	public void Settle()
	{
		Player.Instance.SetCurrentHP(player.CurrentHP);
	}

	public void AutoAction(int currentRound) { }

	public void RefreshState()
	{
		if (player != null)
		{
			hpText.text = player.CurrentHP.ToString();
			actionPointText.text = player.CurrentActionPoint.ToString();
		}
		if(player.CurrentHP <= 0)
		{
			Die();
		}
	}
	public void Die()
	{
		system.GameEnd(GameResult.Failure);
	}

	public void AcceptEffect(object[] _parameterList)
	{
		GameObject initiator = (GameObject)_parameterList[0];
		EffectPackage effect = (EffectPackage)_parameterList[1];
		switch (effect.EffectType)
		{
			case EffectType.Attack:
				{
					player.BeAttacked(effect.EffectValue1);
					//MaskMiss(0.2f, Color.red);
				}
				break;
			case EffectType.VampireAttack:
				{
					int dmg = player.BeAttacked(effect.EffectValue1);
					if(initiator != null && system != null)
					{
						EffectPackage returnEffect = new EffectPackage();
						returnEffect.EffectType = EffectType.Heal;
						returnEffect.EffectValue1 = dmg;
						system.ApplyEffectTo(initiator, gameObject, returnEffect);
					}
					//MaskMiss(0.5f, Color.red);
				}
				break;
			case EffectType.Heal:
				{
					player.BeHealed(effect.EffectValue1);
					//MaskMiss(0.5f, Color.green);
				}
				break;
			default:
				break;
		}
		string msg = name + "接收到效果" + effect.EffectType;
		Debug.Log(msg);
	}

	public void UpdateEffect()
	{
		player.UpdateEffect();
	}

	public Image maskImage;
	IEnumerator MaskMiss(float times, Color color)
	{
		maskImage.enabled = true;
		color.a = 0.5f;
		maskImage.color = color;
		while (times > 0)
		{
			times -= Time.deltaTime;
			yield return new WaitForSeconds(Time.deltaTime);
		}
		maskImage.enabled = false;
	}

	#region 行动点使用接口
	public bool CanUseActionPoint(int cost)
	{
		return (player.CurrentActionPoint - cost >= 0) ?  true : false;
	}
	public void UseActionPoint(int cost)
	{
		if(CanUseActionPoint(cost))
		{
			player.CurrentActionPoint -= cost;
		}
	}
	#endregion
}
