using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
public class PlayerUnitManager : MonoBehaviour, IUnitRunner, IEffectRunner
{
	public PlayerInBattle player { get; private set; }

    public TextMeshProUGUI hpText;
    public TextMeshProUGUI actionPointText;
	public Image headIcon;
	private void Update()
	{
		RefreshState();
	}

	/// <summary>
	/// ≥ı ºªØ
	/// </summary>
	public void Initialized()
	{
		player = new PlayerInBattle(Player.Instance);
	}
	/// <summary>
	/// Ω·À„
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
	}

	public void Die()
	{
		GameObject.Find("BattleSystem").GetComponent<BattleSystem>().GameEnd(GameResult.Failure);
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
				}
				break;
			case EffectType.VampireAttack:
				{
					EffectPackage returnEffect = new EffectPackage();
					returnEffect.EffectType = EffectType.Heal;
					returnEffect.EffectValue1 = player.BeAttacked(effect.EffectValue1);
					if (initiator != null)
					{
						object[] parameterTable = { null, returnEffect };
						initiator.SendMessage("AcceptEffect", parameterTable);
					}
				}
				break;
			case EffectType.Heal:
				{
					player.BeHealed(effect.EffectValue1);
				}
				break;
			default:
				break;
		}
	}

	public void UpdateEffect()
	{
		player.UpdateEffect();
	}

}
