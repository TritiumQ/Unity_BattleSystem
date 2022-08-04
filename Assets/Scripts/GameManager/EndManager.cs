using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;

public class EndManager : MonoBehaviour
{
    public int step;
    public int result;

    public GameObject displayBox_1;
    public GameObject displayBox_2;
    void Awake()
    {
        InitData();
    }
    public void InitData()//从GameManager处获取数据并初始化
    {
        
    }
    public void SetDisplayBox(int _key,int _step)//设置数据显示
    {
        if(_key==1)
        {
            displayBox_1.GetComponent<DisplayBoxUI>().InitSet("步数:", _step);
        }
        else if(_key==2)
        {
            displayBox_2.GetComponent<DisplayBoxUI>().InitSet("获得秘银:", _step);
        }
    }
}
