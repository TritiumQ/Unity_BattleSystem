using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;
[SerializeField]

public class Player
{
    //单例模式
    private Player() 
    {
        cardSet = new List<int>();
        unlock = new bool[400];
    }
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
    public int mithrils; //秘银
    public int tears;  //泪滴
    public List<int> cardSet  { get; private set; } //牌组
    //0~199  200~399
    bool[] unlock; //记录已解锁的卡牌
    public void Initialized(DefaultPlayer _info) //载入数据
	{

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
public class DefaultPlayer
{
    public string name;
    public int maxHP;
    public int currentHP;
    public int mithrils; //秘银
    public int tears;  //泪滴
    public List<int> cardSet { get; private set; } //牌组
    public bool[] unlock;
    public DefaultPlayer()
    {
        name = "none";
        maxHP = 23333;
        currentHP = 114514;
        mithrils = 1919810;
        tears = 361433;
        cardSet = new List<int>();

        unlock = new bool[401];
        System.Array.Fill(unlock, false);
    }
}