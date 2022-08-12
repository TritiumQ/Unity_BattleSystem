/// <summary>
/// 提供自定义的效果刷新函数接口，当存在该接口时，会被UpdateEffect()自动调用
/// </summary>
public interface IUpdateEffectCustom
{
	/// <summary>
	/// 自定义的效果刷新函数接口，当存在该接口时，会被UpdateEffect()自动调用
	/// </summary>
	public void UpdateEffectCustom();
}