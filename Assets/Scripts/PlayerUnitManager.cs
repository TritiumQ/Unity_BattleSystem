using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
public class PlayerUnitManager : MonoBehaviour
{
	public PlayerInBattle player;

    public TextMeshProUGUI hpText;
    public TextMeshProUGUI actionPointText;
	public Image headIcon;
	private void Update()
	{
		Refresh();
	}
	public void Initialized()
	{
		//
	}
	public void Refresh()
	{
		if(player != null)
		{
			hpText.text = player.CurrentHP.ToString();
			actionPointText.text = player.CurrentActionPoint.ToString();
		}
	}
	//Κά»χ
	public void BeAttacked(int _value)
	{
		if(player.protectedTimes>0)
		{
			player.protectedTimes--;
		}
		else
		{
			player.CurrentHP -= _value;
		}
	}
	public int BeVampireAttacked(int _value)
	{
		if (player.protectedTimes > 0)
		{
			player.protectedTimes--;
			return 0;
		}
		else
		{
			player.CurrentHP -= _value;
			return _value;
		}
	}
	public void BeHealed(int _value)
	{
		if(player.CurrentHP + _value <= player.MaxHP )
		{
			player.CurrentHP += _value;
		}
		else
		{
			player.CurrentHP = player.MaxHP;
		}
	}
	/*public void BeConcealed(int _value)
	{

	}*/
	/*public void BeEnhanced(int _value)
	{

	}*/
	/*public void BeInspired(int _value, int _rounds)
	{

	}*/
	/*public void Waghhh(int _value)
	{

	}*/
	public void BeProtected(int _times)
	{
		player.protectedTimes += _times;
	}
	/*public void BeTaunted(int _rounds)
	{

	}*/
}
