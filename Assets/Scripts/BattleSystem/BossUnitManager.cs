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
		boss = new BossInBattle(_asset);
	}
	private void Update()
	{
		Refresh();
	}
	public void Vectory()
	{

	}
	void Refresh()
	{
		if(boss != null) //显示刷新
		{
			hpText.text = boss.currentHP.ToString();
			atkText.text = boss.ATK.ToString();
		}
		if (boss.currentHP <= 0)
		{
			Debug.Log("Boss已击败，战斗胜利");
			//胜利特效
			GameObject.Find("BattleSystem").GetComponent<BattleSystem>().Vectory();
		}
	}
	public void CheckBuff() //每回合结束时调用
	{
		//检查buff
		if (boss.inspireRounds > 0)
		{
			boss.inspireRounds--;
		}
	}
	//Boss行动
	public void Action(int _CurrentRound)
	{
		BattleSystem sys = GameObject.Find("BattleSystem").GetComponent<BattleSystem>();
		int mod = _CurrentRound % boss.actionCycle.Count;
		switch (boss.actionCycle[mod])
		{
			case BossActionType.AOEAttack:
				{
					sys.playerUnitDisplay.BeAttacked(boss.ATK);
					foreach (var obj in sys.PlayerSurventUnits)
					{
						//obj.SendMessage("BeAttck", boss.ATK);
						Effect.Set(obj, CardActionType.Attack, boss.ATK);
					}
				}
				break;
			case BossActionType.AOEAttackExcludePlayer:
				{
					foreach (var obj in sys.PlayerSurventUnits)
					{
						Effect.Set(obj, CardActionType.Attack, boss.ATK);
						//obj.SendMessage("BeAttacked", boss.ATK);
					}
				}
				break;
			case BossActionType.SingleAttack:
				{
					Debug.Log("Boss攻击");
					BossAttack rnd = (BossAttack)Random.Range(0,7); //需要更好的方法从枚举中随机选取
					switch (rnd)
					{
						case BossAttack.RandomTarget:
							{
								int random = Random.Range(0, sys.PlayerSurventUnits.Count + 1);
								if(random == sys.PlayerSurventUnits.Count)
								{
									Effect.Set(sys.playerUnit, CardActionType.Attack, boss.ATK);
									//sys.playerUnitDisplay.BeAttacked(boss.ATK);
								}
								else
								{
									foreach (var obj in sys.PlayerSurventUnits)
									{
										//obj.SendMessage("BeAttacked", boss.ATK);
										Effect.Set(obj, CardActionType.Attack, boss.ATK);
									}
								}
							}
							break;
						case BossAttack.PlayerTarget:
							{
								//sys.playerUnitDisplay.BeAttacked(boss.ATK);
								Effect.Set(sys.playerUnit, CardActionType.Attack, boss.ATK);
							}
							break;
						case BossAttack.LowestATKTarget:
							{
								if(sys.PlayerSurventUnits.Count > 0)
								{
									var target = sys.PlayerSurventUnits[0];
									foreach (var obj in sys.PlayerSurventUnits)
									{
										if (obj != null && obj.GetComponent<SurventUnitManager>().GetInf(GetSurventInfomation.ATK)
											< target.GetComponent<SurventUnitManager>().GetInf(GetSurventInfomation.ATK))
										{
											target = obj;
										}
									}
									Effect.Set(target, CardActionType.Attack, boss.ATK);
									//target.SendMessage("BeAttacked", boss.ATK);
								}
							}
							break;
						case BossAttack.HigestATKTarget:
							{
								if (sys.PlayerSurventUnits.Count > 0)
								{
									var target = sys.PlayerSurventUnits[0];
									foreach (var obj in sys.PlayerSurventUnits)
									{
										if (obj != null && obj.GetComponent<SurventUnitManager>().GetInf(GetSurventInfomation.ATK)
											> target.GetComponent<SurventUnitManager>().GetInf(GetSurventInfomation.ATK))
										{
											target = obj;
										}
									}
									Effect.Set(target, CardActionType.Attack, boss.ATK);
									//target.SendMessage("BeAttacked", boss.ATK);
								}
							}
							break;
						case BossAttack.LowestHPTarget:
							{
								if (sys.PlayerSurventUnits.Count > 0)
								{
									var target = sys.PlayerSurventUnits[0];
									foreach (var obj in sys.PlayerSurventUnits)
									{
										if (obj != null && obj.GetComponent<SurventUnitManager>().GetInf(GetSurventInfomation.CurrentHP)
											< target.GetComponent<SurventUnitManager>().GetInf(GetSurventInfomation.CurrentHP))
										{
											target = obj;
										}
									}
									//target.SendMessage("BeAttacked", boss.ATK);
									Effect.Set(target, CardActionType.Attack, boss.ATK);
								}
							}
							break;
						case BossAttack.HighestHPTarget:
							{
								if (sys.PlayerSurventUnits.Count > 0)
								{
									var target = sys.PlayerSurventUnits[0];
									foreach (var obj in sys.PlayerSurventUnits)
									{
										if (obj != null && obj.GetComponent<SurventUnitManager>().GetInf(GetSurventInfomation.CurrentHP)
											> target.GetComponent<SurventUnitManager>().GetInf(GetSurventInfomation.CurrentHP))
										{
											target = obj;
										}
									}
									//target.SendMessage("BeAttacked", boss.ATK);
									Effect.Set(target, CardActionType.Attack, boss.ATK);
								}
							}
							break;
						default:
							break;
					}
				}
				break;
			case BossActionType.Summon:
				{
					Debug.Log("Boss设置随从");
					int rnd = Random.Range(0, boss.SurventList.Count);
					sys.SetupSurventByBoss(new Card(Resources.Load<CardSOAsset>(Const.MONSTER_CARD_PATH(boss.SurventList[rnd]))));
				}
				break;
			case BossActionType.Skip:
				Debug.Log("Boss跳过回合");
				break;
			default:
				break;
		}
	}
	
	//受击
	public int BeAttacked(int _value)
	{
		if(boss.protectTimes == 0)
		{
			string msg = "受到" + _value.ToString() + "点伤害";
			Debug.Log(msg);
			boss.currentHP -= _value;
			return _value;
		}
		else
		{
			boss.protectTimes--;
			return 0;
		}
	}
	public void BeHealed(int _value)
	{
		if(boss.currentHP + _value > boss.maxHP)
		{
			boss.currentHP = boss.maxHP;
		}
		else
		{
			boss.currentHP += _value;
		}
	}
	public void BeEnhanced(int _value)//HP永久提升
	{
		boss.maxHP += _value;
		boss.currentHP += _value;
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
}
