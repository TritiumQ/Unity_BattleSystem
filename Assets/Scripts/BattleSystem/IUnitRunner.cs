/// <summary>
/// 单位运行接口组,供BattleSystem以及UnitManager调用
/// </summary>
internal interface IUnitRunner
{
	/// <summary>
	/// 自动行动接口,每回合调用一次,仅供非玩家单位使用
	/// </summary>
	/// <param name="currentRound">当前回合数</param>
	public void AutoAction(int currentRound);
	/// <summary>
	/// 状态刷新接口
	/// </summary>
	public void RefreshState();
	/// <summary>
	/// 死亡状态接口
	/// </summary>
	public void Die();
}
