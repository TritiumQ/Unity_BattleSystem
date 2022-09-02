using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine.Events;
using UnityEngine.UI;
using UnityEngine;


public static class GENLoadManager
{
    public static UnityEvent DoFrontLoad = new UnityEvent();
    public static UnityEvent DoAfterLoad = new UnityEvent();
    public static UnityEvent DoUpdataLoad = new UnityEvent();
    public static UnityEvent DoFinalLoad = new UnityEvent();

    public static IEnumerator LoadScene(string scene)
    {
        DoFrontLoad.Invoke();
        AsyncOperation operation = SceneManager.LoadSceneAsync(scene);
        if (operation != null)
        {
            operation.allowSceneActivation = false;
            while (!operation.isDone)
            {
                DoUpdataLoad.Invoke();
                if (operation.progress >= 0.9F)
                {
                    //DoAfterLoad.AddListener(new UnityAction(LoadFightEvent));
                    DoAfterLoad.Invoke();
                    operation.allowSceneActivation = true;
                }

                yield return null;
            }
            DoFinalLoad.Invoke();
            RemoveAllEventLST();
        }
    }

    private static void RemoveAllEventLST()
    {
        DoFrontLoad.RemoveAllListeners();
        DoAfterLoad.RemoveAllListeners();
        DoUpdataLoad.RemoveAllListeners();
        DoFinalLoad.RemoveAllListeners();
    }
    public static void LoadFightEvent()
    {
        GameObject obj = GameObject.Find("GameManager");
        if (obj != null)
        {
            int level = obj.GetComponent<GameManager>().level;
            int step = obj.GetComponent<GameManager>().step;
            int enemy = GetRandom.GetRandomEnemy(level, step, true);
            //Debug.Log(enemy);
            GameObject _battle = GameObject.Find("BattleSystem");
            if (_battle != null)
            {
                Debug.Log("find");
                _battle.GetComponent<BattleSystem>().LoadBossInformation(enemy);
                Debug.Log("敌人信息载入成功");
            }
        }
    }

}
