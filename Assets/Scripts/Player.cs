using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player
{
    int maxHP;
    int curentHP;
    int mithrils; //√ÿ“¯
    int tears;  //¿·µŒ

    List<int> cardSet;
    int[] unlock;
    
    public Player()
	{
        cardSet = new List<int>();
        unlock = new int[1000];
	}
    
}
