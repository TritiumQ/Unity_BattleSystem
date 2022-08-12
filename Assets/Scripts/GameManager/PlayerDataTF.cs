using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class PlayerDataTF
{
    //游戏局内数据与局内各事件结束后数据交互中转站
    private static bool eventAllow = false;//是否允许获取事件结局

    //事件结局
    private static bool eventgo = false;


    public static int GetResult() //获取事件结局
    {
        if(eventAllow==true)
        {
            eventAllow = false;
            if (eventgo == true)
                return 1; //事件通过
            else return -1;//事件失败
        }
        else return 0; //无响应
    }

    //设置了两种事件结局数据传递
    public static void EventContinue()
    {
        eventgo = true;
        eventAllow = true;
    }
    public static void EventEnd()
    {
        eventgo = false;
        eventAllow = true;
    }
    
}
