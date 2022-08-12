using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
/// <summary>
/// 储存和管理场上单位信息的统一父类
/// </summary>
public  class UnitInBattle
{
	public int MaxHP;
	public int CurrentHP;
	public int ATK;
	public int DEF;

	public int ProtectedTimes;
	public int TauntRounds;
	public int ConcealRounds;
	public List<EffectPackage> Inspire;
	public int SilenceRounds;

	public UnitInBattle(int maxHP, int currentHP, int aTK, int dEF, int protectedTimes, int tauntRounds, int concealRounds, int silenceRounds)
	{
		MaxHP = maxHP;
		CurrentHP = currentHP;
		ATK = aTK;
		DEF = dEF;
		ProtectedTimes = protectedTimes;
		TauntRounds = tauntRounds;
		ConcealRounds = concealRounds;
		SilenceRounds = silenceRounds;
		Inspire = new List<EffectPackage>();
	}
	public UnitInBattle()
	{
		MaxHP = 0;
		CurrentHP = 0;
		ATK = 0;
		DEF = 0;
		ProtectedTimes = 0;
		TauntRounds = 0;
		ConcealRounds = 0;
		SilenceRounds = 0;
		Inspire = new List<EffectPackage>();
	}


	#region 效果接收接口
	public int BeAttacked(int damage)
	{
		if(ProtectedTimes > 0)
		{
			ProtectedTimes--;
			return 0;
		}
		else
		{
			CurrentHP -= damage;
			return damage;
		}
	}
	public void BeHealed(int value)
	{
		if (CurrentHP + value <= MaxHP)
		{
			CurrentHP += value;
		}
		else
		{
			CurrentHP = MaxHP;
		}
	}
	public void SetTaunt(int rounds)
	{
		TauntRounds += rounds;
	}
	public void SetConceal(int rounds)
	{
		ConcealRounds += rounds;
	}
	public void SetEnhance(int hp, int atk)
	{
		CurrentHP += hp;
		MaxHP += hp;
		ATK += atk;
	}
	public void SetInspire(int hp, int atk, int rounds)
	{
		EffectPackage isp = new EffectPackage(EffectType.Inspire, hp, atk, rounds);
		Inspire.Add(isp);
		MaxHP += hp;
		CurrentHP += hp;
		ATK += atk;
	}
	public void SetInspire(EffectPackage inspireEffect)
	{
		Inspire.Add(inspireEffect);
		MaxHP += inspireEffect.EffectValue1;
		CurrentHP += inspireEffect.EffectValue1;
		ATK += inspireEffect.EffectValue2;
	}
	public void SetProtected(int times)
	{
		ProtectedTimes += times;
	}
	#endregion

	#region 效果运行/刷新接口
	/// <summary>
	/// 效果刷新接口,每回合结束调用
	/// </summary>
	public void UpdateEffect()
	{
		if(TauntRounds > 0)
		{
			TauntRounds--;
		}
		if(ConcealRounds > 0)
		{
			ConcealRounds--;
		}
		if(SilenceRounds > 0)
		{
			SilenceRounds--;
		}
		for(int i = Inspire.Count - 1; i >= 0; i--)
		{
			if(Inspire[i] != null)
			{
				Inspire[i].EffectRounds--;
				if (Inspire[i].EffectRounds <= 0)
				{
					MaxHP -= Inspire[i].EffectValue1;
					ATK -= Inspire[i].EffectValue2;
					Inspire.RemoveAt(i);
				}
			}
		}
	}
	#endregion
}
