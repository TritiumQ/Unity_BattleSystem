using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnitManager : MonoBehaviour
{
    protected BattleSystem System;
    public UnitInBattle Unit { get; protected set; }
    public bool IsActive { get; protected set; }

	private void  Start()
	{
		System = GameObject.Find(FightSceneObjectName.BattleSystem).GetComponent<BattleSystem>();
	}
}
