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

    public string Name { get; private set; }
    public int MaxHP { get; private set; }
    public int CurrentHP { get; private set; }
    public int Mithrils { get; private set; }//秘银
    public int Tears { get; private set; }//泪滴
    public List<int> cardSet  { get; private set; } //牌组
    //0~199：随从  200~399：法术
    public bool[] Unlocked { get; private set; } //记录已解锁的卡牌

    /// <summary>
    ///  初始化信息, 载入储存信息用, 请勿直接调用
    /// </summary>
    /// <returns></returns>
    public void Initialized(PlayerJSONInformation _info)
	{
        Name = _info.Name;
        MaxHP = _info.MaxHP;
        CurrentHP = _info.CurrentHP;
        Tears = _info.Tears;
        Mithrils = _info.Mithrils;

        cardSet = new List<int>(_info.CardSet);

        Unlocked = new bool[401];
        Array.Fill(Unlocked, false);
        foreach(var id in _info.UnlockCard)
		{
            Unlocked[id] = true;
		}
	}
    
    
    //(弃用)战斗系统所用的三个方法
	public PlayerBattleInformation GetBattleInf()
	{
        PlayerBattleInformation info = new PlayerBattleInformation();
        info.maxHP = MaxHP;
        info.currentHP = CurrentHP;
        info.cardSet = cardSet;
        return info;
	}
    public void SetBattleInf(PlayerBattleInformation _info)
	{
        CurrentHP = _info.currentHP;
	}
    public void SetBattleInf(int _currentHP)
	{
        CurrentHP = _currentHP;
	}

    //新增多种数据修改方法
    public void SetData(string _name, int _maxHP, int _currentHp, int _mithrils, int _tears)
    {
        Name = _name;
        MaxHP = _maxHP;
        CurrentHP = _currentHp;
        Mithrils = _mithrils;
        Tears = _tears;
    }
    public void SetCurrentHP(int _value)
    {
        CurrentHP = _value;
    }
    public void SetMaxHP(int _value)
    {
        MaxHP = _value;
    }
    public void AddCurrentHp(int _value)
    {
        if(CurrentHP + _value <= MaxHP)
		{
            CurrentHP += _value;
		}
        else
		{
            CurrentHP = MaxHP;
		}
    }
    public void AddMaxHp(int _value)
    {
        MaxHP += _value;
    }
    public void SetMoney(int _mithrils, int _tears)
    {
        Mithrils = _mithrils;
        Tears = _tears;
    }
    public void AddMoney(int _mithrils, int _tears)
    {
        Mithrils += _mithrils;
        Tears += _tears;
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
        CheckUnlockedCard();
    }
    public void AddCard(int _newCard)
    {
        cardSet.Add(_newCard);
        CheckUnlockedCard();
    }

    /// <summary>
    /// 卡组修改
    /// </summary>
    /// <return></return>
    public void SetCardSet(List<int> _list)
	{
        cardSet.Clear();
        cardSet = _list;
        CheckUnlockedCard();
	}

    /// <summary>
    /// 卡组以及解锁卡牌修改
    /// </summary>
    /// <return></return>
    public void SetCardSet(List<int> _list, bool[] _unlock)
    {
        cardSet.Clear();
        cardSet = _list;
        Unlocked = _unlock;
        CheckUnlockedCard();
    }

    /// <summary>
    /// 刷新卡牌解锁情况
    /// </summary>
    public void CheckUnlockedCard()
    {
        for (int i = 0; i < cardSet.Count; i++)
        {
            if (Unlocked[cardSet[i]] == false)
                Unlocked[cardSet[i]] = true;
        }
    }
}

//用于json测试
