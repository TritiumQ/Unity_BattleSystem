using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine.SceneManagement;
using UnityEngine;
[SerializeField]

public class Player
{
    //单例模式
    private Player() { }
    private static Player instance;//全局唯一实例
    public static Player Instance //获取实例的属性
    {
        get
        {
            if (instance == null)
                instance = new Player();
            return instance;
        }
    }

    public string name;
    public int maxHP;
    public int currentHP;
    public int mithrils;//秘银
    public int tears;//泪滴
    public List<int> cardSet  { get; private set; } //牌组
    //0~199：随从  200~399：法术
    public bool[] unlock { get; private set; } //记录已解锁的卡牌

    /// <summary>
    ///  初始化信息, 载入储存信息用
    /// </summary>
    /// <returns></returns>
    public void Initialized(PlayerJSONInformation _info)
	{
        name = _info.Name;
        maxHP = _info.MaxHP;
        currentHP = _info.CurrentHP;
        tears = _info.Tears;
        mithrils = _info.Mithrils;

        cardSet = new List<int>(_info.CardSet);

        unlock = new bool[401];
        Array.Fill(unlock, false);
        foreach(var id in _info.UnlockCard)
		{
            unlock[id] = true;
		}
	}
    
    
    //战斗系统所用的三个方法
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

    //新增多种数据修改方法
    public void SetData(string _name, int _maxHP, int _currentHp, int _mithrils, int _tears)
    {
        name = _name;
        maxHP = _maxHP;
        currentHP = _currentHp;
        mithrils = _mithrils;
        tears = _tears;
    }
    public void SetCurrentHP(int _value)
    {
        currentHP = _value;
    }
    public void SetMaxHP(int _value)
    {
        maxHP = _value;
    }
    public void AddCurrentHp(int _value)
    {
        currentHP += _value;
    }
    public void AddMaxHp(int _value)
    {
        maxHP += _value;
    }
    public void SetMoney(int _mithrils, int _tears)
    {
        mithrils = _mithrils;
        tears = _tears;
    }
    public void AddMoney(int _mithrils, int _tears)
    {
        mithrils += _mithrils;
        tears += _tears;
    }

    //卡组的修改推荐使用下面这两种方法
    public void SetCard(List<int> _newCardSet)
    {
        //暂时无法修改使得卡组数量减少
        for (int i = 0; i < _newCardSet.Count; i++)
        {
            if (cardSet.Count > i)
                cardSet[i] = _newCardSet[i];
            else cardSet.Add(_newCardSet[i]);
        }
        CheckCard();
    }
    public void AddCard(int _newCard)
    {
        cardSet.Add(_newCard);
        CheckCard();
    }


    public void CheckCard() //检测卡牌解锁
    {
        for(int i=0;i<cardSet.Count;i++)
        {
            if (unlock[cardSet[i]] == false)
                unlock[cardSet[i]] = true;
        }
    }
}

//用于json测试
