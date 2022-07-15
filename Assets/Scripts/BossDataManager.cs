using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossDataManager : MonoBehaviour
{
    readonly BossAsset asset;
    Boss boss;


	private void Start()
	{
		
	}

	void LoadBossAsset(int _bossID)
	{
		string path = Const.BOSS_DATA_PATH(_bossID);
		
	}
}
