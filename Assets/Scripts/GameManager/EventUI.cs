using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;
/*
 * 局内游戏事件类型：
 * 1.战斗
 * 2.
 * 3.
 * 4.商店
 * 5.boss战
 * 
 */
public class EventUI : MonoBehaviour
{
    // 存储事件数据与更新事件图标
    public int eventSign = 0; //标记游戏事件类型
    public bool isPass = false; //标记游戏事件是否通过
    public int id;

    public GameObject iconBattle;
    public GameObject icon;
    public GameObject iconMeet;
    public GameObject iconStore;
    public GameObject iconBoss;
    void Start()
    {

    }
    void Update()
    {
        Refresh();
    }
    public void SetEventSign(int _value)//设置事件类型
    {
        eventSign = _value;
        //点亮标记
        switch (eventSign)
        {
            case 1:
                {
                    iconBattle.SetActive(true);
                }
                break;
            case 2:
                {
                    icon.SetActive(true);
                }
                break;
            case 3:
                {
                    iconMeet.SetActive(true);
                }
                break;
            case 4:
                {
                    iconStore.SetActive(true);
                }
                break;
            case 5:
                {
                    iconBoss.SetActive(true);
                }
                break;
            default:
                {
                }
                break;
        }
    }
    void Refresh()
    {
        if(isPass==true)
        {
            //游戏事件图标变灰
        }
        
    }
}
