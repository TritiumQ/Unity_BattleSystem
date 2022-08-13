using System.Collections.Generic;
/// <summary>
/// 储存和管理场上单位信息的统一父类
/// </summary>
public class UnitInBattle
{
	public int MaxHP;
	public int CurrentHP;
	public int ATK;
	public int DEF;
	
	protected int ProtectedTimes;
	public bool IsProtected;

	protected int TauntRounds;
	public bool IsTank;

	protected int ConcealRounds;
	public bool IsConcealed;

	protected List<EffectPackage> Inspire;
	public bool IsInspired;

	protected int SilenceRounds;
	public bool IsSilenced;

	protected int VampireRounds;
	public bool IsVampire;


	public UnitInBattle(int maxHP, int currentHP, int aTK, int dEF, int protectedTimes, bool isProtected, int tauntRounds, bool isTank, int concealRounds, bool isConcealed, bool isInspired, int silenceRounds, bool isSilenced, int vampireRounds, bool isVampire)
	{
		MaxHP = maxHP;
		CurrentHP = currentHP;
		ATK = aTK;
		DEF = dEF;
		ProtectedTimes = protectedTimes;
		IsProtected = isProtected;
		TauntRounds = tauntRounds;
		IsTank = isTank;
		ConcealRounds = concealRounds;
		IsConcealed = isConcealed;
		Inspire = new List<EffectPackage>();
		IsInspired = isInspired;
		SilenceRounds = silenceRounds;
		IsSilenced = isSilenced;
		VampireRounds = vampireRounds;
		IsVampire = isVampire;
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
	public virtual int BeAttacked(int damage)
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
	public virtual void BeHealed(int value)
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
	public virtual void SetTaunt(int rounds)
	{
		TauntRounds += rounds;
	}
	public virtual void SetConceal(int rounds)
	{
		ConcealRounds += rounds;
	}
	public virtual void SetEnhance(int hp, int atk)
	{
		CurrentHP += hp;
		MaxHP += hp;
		ATK += atk;
	}
	public virtual void SetInspire(int hp, int atk, int rounds)
	{
		EffectPackage isp = new EffectPackage(EffectType.Inspire, hp, atk, rounds, null);
		Inspire.Add(isp);
		MaxHP += hp;
		CurrentHP += hp;
		ATK += atk;
	}
	public virtual void SetInspire(EffectPackage inspireEffect)
	{
		Inspire.Add(inspireEffect);
		MaxHP += inspireEffect.EffectValue1;
		CurrentHP += inspireEffect.EffectValue1;
		ATK += inspireEffect.EffectValue2;
	}
	public virtual void SetProtected(int times)
	{
		ProtectedTimes += times;
	}
	#endregion

	#region 效果运行/刷新接口
	/// <summary>
	/// 效果刷新接口,每回合结束调用,属于虚拟方法,可被子类复写
	/// </summary>
	public virtual void UpdateEffect()
	{
		if(TauntRounds > 0)
		{
			TauntRounds--;
			IsTank = true;
		}
		else
		{
			IsTank = false;
		}
		if(ConcealRounds > 0)
		{
			ConcealRounds--;
			IsConcealed = true;
		}
		else
		{
			IsConcealed = false;
		}
		if(SilenceRounds > 0)
		{
			SilenceRounds--;
			IsSilenced = true;
		}
		else
		{
			IsSilenced = false;
		}
		if(Inspire.Count > 0)
		{
			IsInspired = true;
			for (int i = Inspire.Count - 1; i >= 0; i--)
			{
				if (Inspire[i] != null)
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
		else
		{
			IsInspired = false;
		}
		if(VampireRounds > 0)
		{
			IsVampire = true;
			VampireRounds--;
		}
		else
		{
			IsVampire = false;
		}
	}
	#endregion
}
