/// <summary>
/// 用于接收和运行普通效果的通用接口组
/// </summary>
public interface IEffectRunner
{
	/// <summary>
	/// 效果接收接口
	/// </summary>
	/// <param name="_parameterList">参数列表为两项：[0]效果发起者, [1]效果包</param>
	public void AcceptEffect(object[] _parameterList);

	/// <summary>
	/// 效果刷新接口，必须每回合结束调用
	/// </summary>
	public void UpdateEffect();

}
