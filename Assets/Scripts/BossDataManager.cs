using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossDataManager : MonoBehaviour
{
    readonly BossSOAsset asset;
    BossInBattle boss;


	private void Start()
	{
		
	}

	void LoadBossAsset(int _bossID)
	{
		string path = Const.BOSS_DATA_PATH(_bossID);
		
	}
}
