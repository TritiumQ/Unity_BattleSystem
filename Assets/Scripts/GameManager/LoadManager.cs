using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine.Events;
using UnityEngine.UI;
using UnityEngine;
using TMPro;


public class LoadManager : MonoBehaviour
{
    public GameObject loadScreen;
    public Slider slider;
    public TextMeshProUGUI text;
    public string scene;
    public GameManager gameManager;
    public LoadTool loadTool;

    
    public void LoadNextLevel()
    {
        StartCoroutine(LoadLevel());//开启协程
    }
    public void SelectFight()
    {
        StartCoroutine(LoadFight());
    }
    IEnumerator LoadLevel()
    {
        loadScreen.SetActive(true);//可以加载场景
        //AsyncOperation operation = SceneManager.LoadSceneAsync(SceneManager.GetActiveScene().buildIndex + 1);
        AsyncOperation operation = SceneManager.LoadSceneAsync(scene);
        if (operation != null)
        {
            operation.allowSceneActivation = false;//不允许场景自动跳转
            while (!operation.isDone)//场景加载没有完成时
            {
                slider.value = operation.progress;//slider的值=加载的进度值
                text.text = operation.progress * 100 + "%";
                if (operation.progress >= 0.9F)
                {
                    slider.value = 1.0f;
                    text.text = "100%";
                    System.Threading.Thread.Sleep(300);
                    operation.allowSceneActivation = true;//允许场景自动跳转
                }

                yield return null;//跳出协程
            }
            System.Threading.Thread.Sleep(500);
            if (scene == "Fight")
            {
                GameObject obj = GameObject.Find("GameManager");
                int level = obj.GetComponent<GameManager>().level;
                int step = obj.GetComponent<GameManager>().step;
                int enemy = GetRandom.GetRandomEnemy(level, step);
                //Debug.Log(enemy);
                GameObject _battle = GameObject.Find("BattleSystem");
                if (_battle != null)
                {
                    //Debug.Log("find");
                    _battle.GetComponent<BattleSystem>().LoadBossInformation(enemy);
                    Debug.Log("敌人信息载入成功");
                }
            }
            //else if(scene=="CardSelect")
            //{
            //    CardSelectSystem obj = GameObject.Find("CardSelectSystem").GetComponent<CardSelectSystem>();
            //    obj.Initialized("GameProcess", GetRandom.GetRandomCard(), GetRandom.GetRandomCard(), GetRandom.GetRandomCard());
            //}
            GameObject _obj = GameObject.Find("Panel");
            _obj.SetActive(false);
        }
    }

    IEnumerator LoadFight()
    {
        //loadScreen.SetActive(true);
        AsyncOperation operation = SceneManager.LoadSceneAsync("Fight");
        if (operation != null)
        {
            operation.allowSceneActivation = false;
            while (!operation.isDone)
            {
                if (operation.progress >= 0.9F)
                {
                    LoadTool.allowLoadFight = true;
                    operation.allowSceneActivation = true;
                }

                yield return null;
            }
            
        }
    }

    public void NextScene(int key)
    {
        switch(key)
        {
            case 1:
                {
                    scene = "Fight";
                }
                break;
            case 2:
                {
                    scene = "SceureHouse";
                }
                break;
            case 3:
                {
                    int level = gameManager.level;
                    if(level==1)
                    {
                        scene = "CardSelect";
                    }
                    else
                    {
                        int pos = Random.Range(1, 2+1);
                        if (pos == 1)
                            scene = "CardSelect";
                        else if (pos == 2)
                            scene = "Choice";
                        else scene = null;
                    }
                }
                break;
            case 4:
                {
                    scene = "ShopInGame";
                }
                break;
            case 5:
                {
                    scene = "Fight";//boss战
                }
                break;
            default:
                {
                    scene = null;
                }
                break;
        }
    }
}
