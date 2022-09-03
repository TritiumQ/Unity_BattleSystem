using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    Player player;       //设置为单例模式

    //TODO 结构优化
    public int step;     //关卡步骤
    public int[] GameEventCount; //每层关卡数量
    public int level;     //层数
    public int result=-1;    //游戏结局
    public List<int> GameEvent; //游戏事件
    public static bool isFistOpen = true;

    public GameObject eventPrefab;
    public GameObject levelUI;
    public GameObject LoadManager;

    public List<GameObject> eventUnit; //事件对象链表
    private void Awake()
    {
        InitGameManager();
    }
    void Start()
    {
        //InitGameManager();

        //测试区
        //step = 4; level = 2;
        //PlayerDataTF.EventEnd();
        //InitGameManager();
        //InitGameEvent(2);

        //数据重置
        //player = Player.Instance;
        //ArchiveManager.LoadPlayerData(1);
        //GameEventCount = GameConst.GameEventCount;//初始化每层关卡数量
        //if (isFistOpen)
        //{
        //    InitGameEvent(2);
        //    GameProcessSave.GameSaveSet(1, this, true);
        //    isFistOpen = false;
        //}
    }

    void Update()
    {
        RefreshData(); //数据更新
    }
    void InitGameManager() //初始化游戏管理者对象
    {
        player = Player.Instance;
        GameEventCount = GameConst.GameEventCount;//初始化每层关卡数量
        if (isFistOpen)
        {
            ArchiveManager.LoadPlayerData(1);
            GameProcessSave.GameProcessDataLoad(this);
            isFistOpen = false;
        }
    }
    public void InitGameEvent(int _level = 1)   //初始化/设置 游戏事件
    {
        level = _level; //初始化所在层数
        step = 0;
        GameEvent = new List<int>();

        //刷新层区UI
        levelUI.GetComponent<LevelUI>().SetLevel(level);
        
        //销毁原有事件UI对象
        for (int i = eventUnit.Count - 1; i >= 0; i--)
        {
            Destroy(eventUnit[i]);
        }
        eventUnit.Clear();
        
        //伪随机生成事件的类型
        GetRandom.GetRandomEvent(GameEvent,level);
        
        //初始化事件预制体对象链表
        for (int i = 0; i < GameEventCount[level]; i++)//初始化事件预制体对象链表
        {
            GameObject newEvent = Instantiate(eventPrefab, transform); //生成事件预制件
            newEvent.GetComponent<Transform>().position = new Vector3(SetPosition(i, 375, 1725), 500, 0);
            if (newEvent != null)
                newEvent.GetComponent<EventUI>().SetEventSign(GameEvent[i]); //设置事件对象的类型
            eventUnit.Add(newEvent);
        }

        //设置下一关场景加载
        //Debug.Log(GameEvent[0]);
        LoadManager.GetComponent<LoadManager>().NextScene(GameEvent[0]);
    }

    public void InitGameEvent(SerializableGP gp)
    {
        level = gp.level; //初始化所在层数
        //Debug.Log(level);
        step = gp.step;
        GameEvent = new List<int>();

        levelUI.GetComponent<LevelUI>().SetLevel(level);

        for (int i = eventUnit.Count - 1; i >= 0; i--)
        {
            Destroy(eventUnit[i]);
        }
        eventUnit.Clear();
        GameEvent.Clear();

        foreach (var e in gp.gameEvent)
        {
            GameEvent.Add(e.eventSign);
        }

        for (int i = 0; i < GameEventCount[level]; i++)//初始化事件预制体对象链表
        {
            GameObject newEvent = Instantiate(eventPrefab, transform); //生成事件预制件
            newEvent.GetComponent<Transform>().position = new Vector3(SetPosition(i, 375, 1725), 500, 0);
            if (newEvent != null)
            {
                newEvent.GetComponent<EventUI>().SetEventSign(GameEvent[i]); //设置事件对象的类型
                if (gp.gameEvent[i].isPass)
                {
                    newEvent.GetComponent<EventUI>().SetPass();
                }
            }
            eventUnit.Add(newEvent);
        }

        LoadManager.GetComponent<LoadManager>().NextScene(GameEvent[step]);
    }
    void RefreshData() //更新游戏局内数据
    {
        //局内游戏数据更新
        int judge = PlayerDataTF.GetResult();
        if (judge == 1)//通过当前事件
        {
            AddStep();
        }
        else if (judge == -1 || player.CurrentHP <= 0)
        {
            Gameover(0);//游戏失败
        }
    }
    public void Gameover(int _result) //局内游戏结束
    {
        result = _result;
        GetReward();
        isFistOpen = true;
        if (player != null)
        {
            player.ReSet();
            //InitGameEvent();
            //GameProcessSave.GameSaveSet(1, this, true);
        }
        //_result控制结局走向,暂定 0是失败，1是胜利......
        //切换End场景
        SceneManager.LoadScene("EndScene");
    }

    
    #region 行为/工具方法

    void AddStep()
    {
        Debug.Log(eventUnit.Count);
        eventUnit[step].GetComponent<EventUI>().SetPass();//事件图标更新
        step++;
        if (step < GameEventCount[level])
        {
            LoadManager.GetComponent<LoadManager>().NextScene(GameEvent[step]);//设置下一关场景加载
        }
        else//通过当前层区
        {
            AddLevel();
        }
        //数据保存
        if (player != null)
        {
            GameProcessSave.GameSaveSet(1, this, true);
        }
    }
    void AddLevel()
    {
        level++;
        step = 0;//重置步数
        if (level<GameConst.GameEventCount.Length)
        {
            InitGameEvent(level); //刷新事件
        }
        else//游戏通关
        {
            Gameover(1); //进入结算页面
        }
    }
    float SetPosition(int i,float low,float up)//设置x轴位置
    {
        float len,x;
        len = (up - low) / GameEventCount[level];
        x = i * len + len / 2 + low;
        return x;
    }

    void GetReward()
    {
        int value=0;
        for(int i=1;i<level;i++)
        {
            value += GameEventCount[i];
        }
        value += step;
        value *= 2;
        player.AddMoney(value, 0);
    }

}
#endregion
