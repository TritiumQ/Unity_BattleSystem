using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
//专门用来管理局内游戏数据的类对象
public class GameManager : MonoBehaviour
{
    Player player;       //设置为单例模式

    //TODO
    //结构优化
    public int step;     //关卡步骤
    public int[] GameEventCount; //每层关卡数量
    public int level;     //层数
    public int result=-1;    //游戏结局
    public List<int> GameEvent; //游戏事件

    public GameObject eventPrefab;
    public GameObject levelUI;
    public GameObject LoadManager;

    public List<GameObject> eventUnit; //事件对象链表
    private void Awake()
    {
        InitGameManager();
        player = Player.Instance;
        PlayerDataTF.GetData(player);
    }
    void Start()
    {
        //测试区
        //PlayDataTF.EventEnd();
        //InitGameEvent(4);
    }

    void Update()
    {
        RefreshData(); //数据更新
    }
    void InitGameManager() //初始化游戏管理者对象
    {

        //Player数据加载

        //关卡数据初始化
        //GameEventCount = new int[5] { 0, 5, 6, 7, 8 }; 
        GameEventCount = LevelEvent.GameEventCount;//初始化每层关卡数量
        InitGameEvent();
        //加载关卡进度数据（重新进入游戏的进度加载）
    }
    void InitGameEvent(int _level = 1)   //初始化/设置 游戏事件
    {
        level = _level; //初始化所在层数
        if(level==1)//初始化所在事件
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
        GetComponent<GetRandom>().GetRandomEvent(GameEvent,level);
        
        //初始化事件预制体对象链表
        for (int i = 0; i < GameEventCount[_level]; i++)//初始化事件预制体对象链表
        {
            GameObject newEvent = Instantiate(eventPrefab, transform); //生成事件预制件
            newEvent.GetComponent<Transform>().position = new Vector3(SetPosition(i, 375, 1725), 500, 0);
            if (newEvent != null)
                newEvent.GetComponent<EventUI>().SetEventSign(GameEvent[i]); //设置事件对象的类型
            eventUnit.Add(newEvent);
        }
        
        //设置下一关场景加载
        LoadManager.GetComponent<LoadManager>().NextScene(GameEvent[step]);
    }
    
    void RefreshData() //更新游戏局内数据
    {
        //自身的玩家数据更新
        PlayerDataTF.GetData(player);
        //局内游戏数据更新
        int judge = PlayerDataTF.GetResult();
        if (judge == 1)//通过当前事件
        {
            AddStep();
        }
        else if (judge == -1 /*|| player.currentHP <= 0*/)
        {
            Gameover(0);//游戏失败
        }
    }
    void Gameover(int _result) //局内游戏结束
    {
        result = _result;
        player.AddMoney(2 * step, 0); //游戏奖励
        //_result控制结局走向,暂定 0是失败，1是胜利......
        //Debug.Log("游戏失败");
        //切换End场景
        SceneManager.LoadScene("EndScene");
    }


    //以下是工具方法

    void AddStep()
    {
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
        //TODO
        //数据保存
    }
    void AddLevel()
    {
        level++;
        if(level<=4)
        {
            InitGameEvent(level); //刷新事件
        }
        else//游戏通关
        {
            Gameover(1); //进入结算页面
        }
        step = 0;//重置步数
    }
    float SetPosition(int i,float low,float up)//设置x轴位置
    {
        float len,x;
        len = (up - low) / GameEventCount[level];
        x = i * len + len / 2 + low;
        //Debug.Log(x);
        return x;
    }

}
