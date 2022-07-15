using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[System.Serializable]
public class Card 
{
    public int id;
    public string cardName;
    public int cost; //卡牌费用
    public int atk; //攻击
    public int healthMax;//生命初始值
    public string race; //卡牌种类
    //public string cardDesciption; //描述

    public Card()
    {

    }

    public Card(int Id,string Name,int Cost,int Atk, int HealthMax, string Race)
    {
        this.id = Id;
        this.cardName = Name;
        this.cost = Cost;
        this.atk = Atk;
        this.healthMax = HealthMax;
        this.race = Race;
        //this.cardDesciption = Des;
    }
}

public class MonsterCard : Card //怪物卡（随从卡）
{
    public int health; //生命值
    public string skill; //技能

    public MonsterCard()
    {

    }

    public MonsterCard(int Id, string Name, int Cost, int Atk, int HealthMax, string Race, string Skill):base(Id, Name, Cost, Atk, HealthMax, Race)
    {
        this.health = this.healthMax;
        this.skill = Skill;
    }
}

public class SpellCard : Card //法术卡
{
    public string effect; //法术效果

    public SpellCard()
    {

    }

    public SpellCard(int Id, string Name, int Cost, string Race, string Effect) :base(Id, Name, Cost, 0, 0, Race)
    {
        this.effect = Effect;
    }
}
