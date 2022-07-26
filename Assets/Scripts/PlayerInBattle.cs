using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInBattle
{
	public int MaxHP;
	public int CurrentHP;
	public int CurrentActionPoint;
	public int MaxActionPoint;
	public PlayerInBattle(int maxHP, int currentHP, int currentActionPoint, int maxActionPoint)
	{
		MaxHP = maxHP;
		CurrentHP = currentHP;
		CurrentActionPoint = currentActionPoint;
		MaxActionPoint = maxActionPoint;
	}
	public PlayerInBattle(Player _player)
	{
		MaxHP = _player.maxHP;
		CurrentHP = _player.currentHP;
		CurrentActionPoint = MaxActionPoint = 1;

	}
	public PlayerInBattle(PlayerBattleInformation _info)
	{
		CurrentHP = _info.currentHP;
		MaxHP = _info.maxHP;
		CurrentActionPoint = MaxActionPoint = 1;
	}
	public void SetActionPoint(int _max, int _current)
	{
		MaxActionPoint = _max;
		CurrentActionPoint = _current;
	}
}
