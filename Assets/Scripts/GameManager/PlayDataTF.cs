using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class PlayDataTF
{
    //游戏局内数据与局内各事件结束后玩家数据交互中转站
    private static bool playerAllow = false; //是否允许获取玩家数据
    private static bool cardAllow = false;//是否允许获取牌组数据
    private static bool eventAllow = false;//是否允许获取事件结局
    
    //玩家数据缓存区
    public static string name;   //这个应该不用改吧
    public static int maxHP;
    public static int currentHP;
    public static int mithrils; //秘银
    public static int tears;  //泪滴
    static List<int> cardSet=new List<int>(); //牌组

    //事件结局
    private static bool eventgo = false;

    public static void GetData(Player _player) //Player类对象获取玩家信息
    {
        if(playerAllow==true)
        {
            _player.name = name;
            _player.maxHP = maxHP;
            _player.currentHP = currentHP;
            _player.mithrils = mithrils;
            _player.tears = tears;
            playerAllow = false;
        }
        if (cardAllow == true)
        {
            for (int i = 0; i < cardSet.Count; i++)
            {
                if (cardSet[i] >= 0)
                {
                    _player.cardSet[i] = cardSet[i];
                }
            }
            cardAllow = false;
        }
    }
    public static int GetResult() //获取事件结局
    {
        if(eventAllow==true)
        {
            eventAllow = false;
            if (eventgo == true)
                return 1; //事件通过
            else return -1;//事件失败
        }
        else return 0; //无响应
    }
    //设置了7个传递玩家数据方法
    public static void SetData(string _name,int _maxHP,int _currentHp,int _mithrils,int _tears)
    {
        name = _name;
        maxHP = _maxHP;
        currentHP = _currentHp;
        mithrils = _mithrils;
        tears = _tears;
        playerAllow = true;
    }
    public static void SetCurrentHP(int _value)
    {
        currentHP = _value;
        playerAllow = true;
    }
    public static void SetMaxHP(int _value)
    {
        maxHP = _value;
        playerAllow = true;
    }
    public static void AddCurrentHp(int _value)
    {
        currentHP += _value;
        playerAllow = true;
    }
    public static void AddMaxHp(int _value)
    {
        maxHP += _value;
        playerAllow = true;
    }
    public static void SetMoney(int _mithrils, int _tears)
    {
        mithrils = _mithrils;
        tears = _tears;
        playerAllow = true;
    }
    public static void AddMoney(int _mithrils, int _tears)
    {
        mithrils += _mithrils;
        tears += _tears;
        playerAllow = true;
    }
    
    //两个修改卡组的方法
    public static void SetCard(List<int> _newCardSet)
    {
        //暂时无法修改使得卡组数量减少
        for(int i=0;i<_newCardSet.Count;i++)
        {
            if (cardSet.Count > i)
                cardSet[i] = _newCardSet[i];
            else cardSet.Add(_newCardSet[i]);
        }
        cardAllow = true;
    }
    public static void AddCard(int _newCard)
    {
        cardSet.Add(_newCard);
        cardAllow = true;
    }

    //设置了两种事件结局数据传递
    public static void EventContinue()
    {
        eventgo = true;
        eventAllow = true;
    }
    public static void EventEnd()
    {
        eventgo = false;
        eventAllow = true;
    }
}
