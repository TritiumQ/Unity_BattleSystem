/// <summary>
/// 用于触发特殊效果的接口组
/// </summary>
internal interface ISpecialAbilityRunner
{
	/// <summary>
	/// 放置效果触发接口, 单位放置于场上时调用
	/// </summary>
	public void SetupEffectTrigger();
	/// <summary>
	/// 先机效果触发接口, 每回合开始时调用
	/// </summary>
	public void AdvancedEffectTrigger();
	/// <summary>
	/// 后手效果触发接口, 每回合结束时调用
	/// </summary>
	public void SubsequentEffectTrigger();
	/// <summary>
	/// 受击反馈效果触发接口, 单位受到有效的伤害时调用
	/// </summary>
	public void FeedbackEffectTrigger();
	/// <summary>
	/// 亡语效果触发接口, 单位死亡时调用
	/// </summary>
	public void UndeadEffectTrigger();
}