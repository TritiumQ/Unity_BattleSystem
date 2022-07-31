using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;

using UnityEngine;

public class Player
{
    
    public int name;
    public int maxHP;
    public int currentHP;
    public int mithrils; //秘银
    public int tears;  //泪滴

    List<int> cardSet; //牌组
    //0~199  200~299
    bool[] unlock; //记录已解锁的卡牌

	private void Start()
	{
        cardSet = new List<int>();
        unlock = new bool[400];
    }

    public void LoadData()
	{
        
	}

	public PlayerBattleInformation GetBattleInf()
	{
        PlayerBattleInformation info = new PlayerBattleInformation();
        info.maxHP = maxHP;
        info.currentHP = currentHP;
        info.cardSet = cardSet;
        return info;
	}
    public void SetBattleInf(PlayerBattleInformation _info)
	{
        currentHP = _info.currentHP;
	}
    public void SetBattleInf(int _currentHP)
	{
        currentHP = _currentHP;
	}

}
