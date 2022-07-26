using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SurventInBattle
{
    Card card;

    int atk;
    int currentHP;
    int maxHP;

    //特殊效果区
    public int isRaid;
    public int inspireRounds;
    public int inspireValue;
    public int concealRounds;
    //public int silenceRounds;
    public int tauntRounds;
    public int protectedTimes;
    public int doubleHitRounds;
    public int vampireRounds;


    public SurventInBattle(Card _card)
	{
		this.card = _card;

	}
}
