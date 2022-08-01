using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyUnitAction : MonoBehaviour
{
    SurventUnitManager manager;
    void Start()
    {
        manager = GetComponent<SurventUnitManager>();
    }

    public void Action()
	{
        int atk = manager.survent.atk;
        BattleSystem sys = GameObject.Find("BattleSystem").GetComponent<BattleSystem>();
        if(sys != null)
		{
            Debug.Log("bossËæ´Ó¹¥»÷");
            int rand = Random.Range(0, sys.PlayerSurventUnits.Count + 1);
            if(rand == sys.PlayerSurventUnits.Count)
			{
                Effect.Set(sys.playerUnit, CardActionType.Attack, atk);
			}
			else
			{
                Effect.Set(sys.PlayerSurventUnits[rand], CardActionType.Attack, atk);
			}
		}
	}
}
