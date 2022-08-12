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
    ///  初始化信息, 仅供ArchiveManager类载入储存信息用, 请勿直接调用
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

    #region 数据修改接口
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
	#endregion

	#region 卡组修改接口
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

    /// <summary>
    /// 卡组修改, 该方法会覆盖原有的卡组
    /// </summary>
    /// <param name="_list">修改后的卡组</param>
    public void SetCardSet(List<int> _list)
	{
        cardSet.Clear();
        cardSet = _list;
        CheckUnlockedCard();
	}

    /// <summary>
    /// 卡组以及解锁列表修改，该方法会覆盖原有的卡组和解锁列表
    /// </summary>
    /// <param name="_list">修改后的卡组</param>
    /// <param name="_unlock">修改后的解锁列表</param>
    public void SetCardSet(List<int> _list, bool[] _unlock)
    {
        cardSet.Clear();
        cardSet = _list;
        Unlocked = _unlock;
        CheckUnlockedCard();
    }

    /// <summary>
    /// 增添单张卡牌
    /// </summary>
    /// <param name="_cradID">待加入的卡牌ID</param>
    public void AddCard(int _cradID)
	{
        cardSet.Add(_cradID);
        CheckUnlockedCard();
	}

    /// <summary>
    /// 批量增添卡牌
    /// </summary>
    /// <param name="cardList">待加入的卡牌ID列表</param>
    public void AddCard(List<int> cardList)
	{
        foreach(var card in cardList)
		{
            cardSet.Add(card);
		}
        CheckUnlockedCard();
	}

    /// <summary>
    /// 批量增添卡牌
    /// </summary>
    /// <param name="cardList">待加入的卡牌ID列表</param>
    public void AddCard(int[] cardList)
    {
        foreach (var card in cardList)
        {
            cardSet.Add(card);
        }
        CheckUnlockedCard();
    }

    /// <summary>
    /// 删除单张卡牌
    /// </summary>
    /// <param name="_cardID">待删除卡牌ID</param>
    public void DeleteCard(int _cardID)
	{
        cardSet.Remove(_cardID);
	}

	#endregion

}

