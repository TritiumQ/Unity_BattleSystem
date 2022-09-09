﻿using System;
using System.Collections.Generic;
using UnityEngine;
public class GoodsEffectManager : MonoBehaviour
{
	public static int BaseHealValue = 5;
	public static int BaseEnhanceValue = 2;
	#region 商品特效(全局商店)
	/// <summary>
	/// 恢复, 回复量 = BaseHealValue * 回复等级)
	/// </summary>
	/// <param name="rank">回复等级</param>
	public void Healing(int rank = 1)
	{
		if(rank > 0)
		{
			Player.Instance.AddCurrentHp(BaseHealValue * rank);
		}
	}
	/// <summary>
	/// 生命最大值增加, 增加量 = BaseEnhanceValue * 等级
	/// </summary>
	/// <param name="rank">等级</param>
	public void HPEnhance(int rank = 1)
	{
		if (rank > 0)
		{
			Player.Instance.AddMaxHp(BaseHealValue * rank);
		}
	}
	/// <summary>
	/// 初始泪滴增加
	/// </summary>
	public void ExtraTears()
	{
		Player.Instance.AddInitTears(2);
	}

	public void GetCurse(int rank)
	{

	}
	#endregion

	#region 商品特效(游戏商店)

	public void UndeadCurse(int rank)
	{

	}

	public void GetTearsByHP(int rank)
	{
		if(Player.Instance != null)
		{
			Player.Instance.AddMoney(0, 5);
			Player.Instance.AddCurrentHp(-3);
		}
	}

	/// <summary>
	/// 获取馈赠(预留,未投入使用)
	/// </summary>
	public void GetGift()
	{

	}

	#endregion
}
