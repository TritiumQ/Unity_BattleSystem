using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LoadTool : MonoBehaviour
{
    public static bool allowLoadFight=false;
    private static int count = 0;
    void Start()
    {
        allowLoadFight = false;
        count = 0;
    }

    
    void Update()
    {
        if (allowLoadFight == true)
        {
            count++;
            if(count>=20)
            {
                GameObject obj = GameObject.Find("GameManager");
                int level = obj.GetComponent<GameManager>().level;
                int step = obj.GetComponent<GameManager>().step;
                int enemy = GetRandom.GetRandomEnemy(level, step, true);
                Debug.Log(enemy);
                GameObject _battle = GameObject.Find("BattleSystem");
                if (_battle != null)
                {
                    //Debug.Log("find");
                    _battle.GetComponent<BattleSystem>().LoadBossInformation(enemy);
                    Debug.Log("敌人信息载入成功");
                    count = 0;
                    allowLoadFight = false;
                }
            }
        }
    }
}
