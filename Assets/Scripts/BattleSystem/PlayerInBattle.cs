using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInBattle : UnitInBattle, IUpdateEffectCustom
{
	public string Name;
	public int CurrentActionPoint;
	public int MaxActionPoint;

	public PlayerInBattle(Player _player) : base(_player.MaxHP, _player.CurrentHP, 0, 0, 0, false, 0, false, 0, false, false, 0, false, 0, false)
	{
		CurrentActionPoint = MaxActionPoint = 1;
	}
	public void UpdateEffectCustom()
	{
		if (MaxActionPoint < 10)
		{
			MaxActionPoint++; //每经过一回合,战术点上升1点,最大为10
		}
		CurrentActionPoint = MaxActionPoint;
	}
}
