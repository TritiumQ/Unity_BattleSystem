using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//专门用来管理局内游戏数据的类对象
public class GameManager : MonoBehaviour
{
    Player player;       //假设已引用or已copy

    public int step;     //关卡步骤
    public int GameEventCount = 5;//关卡数量,做层数就把这个设为数组
    public int level;     //层数
    List<int> GameEvent; //游戏事件

    public GameObject eventPrefab;

    public List<GameObject> eventUnit; //事件对象链表
    private void Awake()
    {
        InitGameManager();
        player = new Player();
        PlayDataTF.GetData(player);
    }
    void Start()
    {
        
    }

    void Update()
    {
        RefreshData(); //数据更新
        //RefreshScenes(); //场景显示更新,可在各自的gameobject对象中进行
    }
    public void InitGameManager() //初始化游戏管理者对象
    {
        //Player数据加载

        //关卡数据初始化
        InitGameEvent();
        //加载关卡进度数据（重新进入游戏的进度加载）
    }
    void InitGameEvent(int _level = 1)   //初始化游戏事件  预置层数参数(未必能用上)
    {
        level = _level; //初始化所在层数
        step = 0;//初始化所在事件
        GameEvent = new List<int>();
        for (int i = 0; i < GameEventCount; i++)
        {
            if (i == GameEventCount - 1) //最后一个事件，非随机
                GameEvent.Add(5);
            else GameEvent.Add(Random.Range(1, 5));//随机生成事件
        }
        //初始化事件预制体对象链表
        //Debug.Log(GameEventCount);
        for (int i = 0; i < GameEventCount; i++)
        {
            GameObject newEvent = Instantiate(eventPrefab,transform); //生成事件预制件
            newEvent.GetComponent<Transform>().position = new Vector3(SetPosition(i, 375, 1725),500,0);
            if (newEvent != null)
                newEvent.GetComponent<EventUI>().SetEventSign(GameEvent[i]); //设置事件对象的类型
            eventUnit.Add(newEvent);
        }
    }
    void RefreshScenes() //更新游戏局内场景
    {
        //玩家数据显示更新
        //游戏进程显示更新
        //事件按钮显示更新
        //
    }
    void RefreshData() //更新游戏局内数据
    {
        //自身的玩家数据更新
        PlayDataTF.GetData(player);
        //局内游戏数据更新
        int judge = PlayDataTF.GetResult();
        if (judge == 1)
        {
            step++;
            if (step == GameEventCount)
            {
                step = 0;
                //进入下一层或游戏胜利
                Gameover(1);//游戏通过
            }
        }
        else if (judge == -1 || player.currentHP <= 0)
        {
            Gameover(0);//游戏失败
        }
    }
    void Gameover(int _result) //局内游戏结束
    {
        //_result控制结局走向,暂定 0是失败，1是胜利......
    }
    
    
    //以下是工具方法
    float SetPosition(int i,float low,float up)//设置x轴位置
    {
        float len,x;
        len = (up - low) / GameEventCount;
        x = i * len + len / 2 + low;
        Debug.Log(x);
        return x;
    }
}
