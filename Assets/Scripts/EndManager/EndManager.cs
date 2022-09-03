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
        GameManager _object = GameObject.Find("GameManager").GetComponent<GameManager>();
        if (_object != null) 
        {
            Debug.Log("初始化结束场景");
            step = _object.step;
            level= _object.level;
            result= _object.result;
            _object.InitGameEvent();
            GameProcessSave.GameSaveSet(1, _object, true);
            SetDisplayBox(1, step);
            SetDisplayBox(2, step);
            GameObject _obj = GameObject.Find("GameProcess");
            Destroy(_obj);
        }
    }
    
    public void SetDisplayBox(int _key, int _step)//设置数据显示
    {
        int value = step;
        for (int i = 1; i < level; i++)
            value += GameConst.GameEventCount[i];
        if (_key == 1)
        {
            displayBox_1.GetComponent<DisplayBoxUI>().InitSet("步数:", value);
        }
        else if (_key == 2)
        {
            displayBox_2.GetComponent<DisplayBoxUI>().InitSet("获得秘银:", value*2);
        }
    }
}
