using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;

public class EndManager : MonoBehaviour
{
    public int step;
    public int level;
    public int result;

    public GameObject displayBox_1;
    public GameObject displayBox_2;
    void Awake()
    {
        InitData();
    }
    public void InitData()
    {
        GameObject _object= GameObject.Find("GameManager");
        if (_object != null) 
        {
            //step计算有误
            step = _object.GetComponent<GameManager>().step;
            level= _object.GetComponent<GameManager>().level;
            result= _object.GetComponent<GameManager>().result;
            SetDisplayBox(1, step);
            SetDisplayBox(2, step);
            _object = GameObject.Find("GameProcess");
            Destroy(_object);
        }
    }
    
    public void SetDisplayBox(int _key, int _step)//设置数据显示
    {
        if (_key == 1)
        {
            displayBox_1.GetComponent<DisplayBoxUI>().InitSet("步数:", _step);
        }
        else if (_key == 2)
        {
            displayBox_2.GetComponent<DisplayBoxUI>().InitSet("获得秘银:", _step);
        }
    }
}
