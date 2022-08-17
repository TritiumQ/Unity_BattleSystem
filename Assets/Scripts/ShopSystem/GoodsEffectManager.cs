using System;
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
	public void Healing(int rank = 0)
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
	public void HPEnhance(int rank = 0)
	{
		if (rank > 0)
		{
			Player.Instance.AddMaxHp(BaseHealValue * rank);
		}
	}
	

	#endregion

	#region 商品特效(游戏商店)
	/// <summary>
	/// 获取馈赠(预留,未投入使用)
	/// </summary>
	public void GetGift()
	{

	}

	#endregion
}
