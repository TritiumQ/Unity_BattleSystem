using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class BossUnitManager : MonoBehaviour
{
    public BossInBattle boss;

    public TextMeshProUGUI hpText;
    //public TextMeshProUGUI bossNameText;
	public TextMeshProUGUI atkText;
	public Image headIcon;
	//public Image backGroungImage;
	public void Initialized(BossSOAsset _asset)
	{
		BossInBattle _boss = new BossInBattle(_asset);
		boss = _boss;
	}
	private void Update()
	{
		Refresh();
		if(boss.curentHP <= 0)
		{
			Die();
		}
	}
	void Refresh()
	{
		if(boss != null) //显示刷新
		{
			hpText.text = boss.curentHP.ToString();
			atkText.text = boss.ATK.ToString();
		}
	}
	public void CheckBuff() //每回合结束时调用
	{
		//检查buff
		if (boss.tauntRounds > 0)
		{
			boss.tauntRounds--;
			if(boss.tauntRounds == 0)
			{

			}
		}
		if (boss.inspireRounds > 0)
		{
			boss.inspireRounds--;
			if(boss.inspireRounds == 0)
			{
				boss.ATK -= boss.inspireValue;
				boss.inspireValue = 0;
			}
		}
		if (boss.silenceRounds > 0)
		{
			boss.silenceRounds--;
			if(boss.silenceRounds == 0)
			{

			}
		}
	}
	//受击
	public void Die()
	{

	}
	public void BeAttacked(int _value)
	{
		if(boss.protectTimes == 0)
		{
			boss.maxHP -= _value;
			boss.protectTimes--;
		}
	}
	public void BeHealed(int _value)
	{
		if(boss.curentHP + _value > boss.maxHP)
		{
			boss.curentHP = boss.maxHP;
		}
		else
		{
			boss.curentHP += _value;
		}
	}
	public void BeEnhanced(int _value)//HP永久提升
	{
		boss.maxHP += _value;

	}
	public void BeInspired(int _value, int _rounds)
	{
		boss.ATK += _value;
		boss.inspireValue = _value;
		boss.inspireRounds = _rounds;
	}
	public void Waghhh(int _value)
	{
		boss.ATK += _value;
	}
	public void BeSilenced(int _rounds)
	{
		boss.silenceRounds = _rounds;
	}
}
