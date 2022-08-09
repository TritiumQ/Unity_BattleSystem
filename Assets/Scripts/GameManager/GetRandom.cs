using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GetRandom : MonoBehaviour
{
    public int step;
    public int level;
    public int[] GameEventCount;

    void Start()
    {
        GameEventCount = LevelEvent.GameEventCount;
    }

    // Update is called once per frame
    void Update()
    {
        step = GetComponent<GameManager>().step;
        level = GetComponent<GameManager>().level;
    }

    public void GetRandomEvent(List<int> _gameEvent,int _level)//伪随机获取游戏事件
    {
        int[] array=new int[0];
        if (_gameEvent == null)
            _gameEvent = new List<int>();
        switch(_level)
        {
            case 1:
                {
                    array = LevelEvent.GetArrray(LevelEvent.level_1);
                    break;
                }
            case 2:
                {
                    array = LevelEvent.GetArrray(LevelEvent.level_2);
                    break;
                }
            case 3:
                {
                    array = LevelEvent.GetArrray(LevelEvent.level_3);
                    break;
                }
            case 4:
                {
                    array = LevelEvent.GetArrray(LevelEvent.level_4);
                    break;
                }
        }
        _gameEvent.Clear();
        for(int i=0;i<array.Length-1;i++)//打乱顺序
        {
            int pos = Random.Range(0, array.Length - 2),tmp;
            tmp = array[i];
            array[i] = array[pos];
            array[pos] = tmp;
        }
        for(int i=0;i<array.Length;i++)
        {
            _gameEvent.Add(array[i]);
        }
    }

    //TODO
    public int GetRandomCard()//伪随机获取游戏卡牌
    {
        int idex=0;
        return idex;
    }

    public int GetRandomEnemy(int _level,int _step,bool choose=false)//伪随机获取敌人编号
    {
        int idex = 0;
        switch(_level)
        {
            case 1:
                {
                    idex = Random.Range(1, 3);
                    break;
                }
            case 2:
                {
                    if(choose==true)
                        idex = 18;
                    else idex = Random.Range(4, 6);
                    break;
                }
            case 3:
                {
                    if (choose == true)
                        idex = Random.Range(19, 20);
                    else if (_step == GameEventCount[_level] - 1)
                        idex = Random.Range(22, 24);
                    else idex = Random.Range(7, 13);
                    break;
                }
            case 4:
                {
                    if (choose == true)
                        idex = 21;
                    else if (_step == GameEventCount[_level] - 1)
                        idex = 25;
                    else idex = Random.Range(14, 17);
                    break;
                }
        }
        return idex;
    }
}
