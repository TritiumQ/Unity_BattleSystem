using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
/// <summary>
/// 提供单位自动运行的接口, 供战斗系统调用
/// </summary>
public interface IAutoActionRunner
{
	public void AutoAction(int currentRound); 
}