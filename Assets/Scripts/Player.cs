using System.Collections;
using System.Collections.Generic;

using UnityEngine;

public class Player
{
    public int name;
    public int maxHP;
    public int curentHP;
    public int mithrils; //秘银
    public int tears;  //泪滴

    List<int> cardSet; //牌组

    //int[] unlock; 记录已解锁的卡牌

	private void Start()
	{
        cardSet = new List<int>();
        //unlock = new int[1000];
    }

    public void LoadData()
	{
        
	}
}
