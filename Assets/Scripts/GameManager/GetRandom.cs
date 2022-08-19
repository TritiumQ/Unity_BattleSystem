using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class GetRandom
{
    public static void GetRandomEvent(List<int> _gameEvent,int _level)
    {
        int[] array=new int[0];
        if (_gameEvent == null)
            _gameEvent = new List<int>();
        switch(_level)
        {
            case 1:
                {
                    array = GameConst.GetArrray(GameConst.level_1);
                    break;
                }
            case 2:
                {
                    array = GameConst.GetArrray(GameConst.level_2);
                    break;
                }
            case 3:
                {
                    array = GameConst.GetArrray(GameConst.level_3);
                    break;
                }
            case 4:
                {
                    array = GameConst.GetArrray(GameConst.level_4);
                    break;
                }
        }
        _gameEvent.Clear();
        for(int i=0;i<array.Length-1;i++)//´òÂÒË³Ðò
        {
            int pos = Random.Range(0, array.Length - 1),tmp;
            tmp = array[i];
            array[i] = array[pos];
            array[pos] = tmp;
        }
        for(int i=0;i<array.Length;i++)
        {
            _gameEvent.Add(array[i]);
        }
    }

    /// <summary>
    /// Î±Ëæ»ú»ñÈ¡ÓÎÏ·¿¨ÅÆ±àºÅ
    /// </summary>
    /// <returns></returns>
    public static int GetRandomCard()
    {
        //4 3 2 1
        int idex=0,pos1=0,pos2=0;
        idex = GameConst.DrawCard[Random.Range(0, GameConst.DrawCard.Length)];
        pos1 = Random.Range(GameConst.Card_SVN[idex, 0], GameConst.Card_SVN[idex, 1] + 1);
        pos2 = Random.Range(GameConst.Card_SPL[idex, 0], GameConst.Card_SPL[idex, 1] + 1);
        if (idex == 1)
            return pos1;
        else if (Random.Range(1, 4) <= 2)
            return pos1;
        else return pos2;
    }

    /// <summary>
    /// Î±Ëæ»ú»ñÈ¡µÐÈË±àºÅ
    /// </summary>
    /// <returns></returns>
    public static int GetRandomEnemy(int _level,int _step,bool choose=false)
    {
        int idex = 0;
        switch(_level)
        {
            case 1:
                {
                    idex = Random.Range(1, 3+1);
                    break;
                }
            case 2:
                {
                    if(choose==true)
                        idex = 15;
                    else idex = Random.Range(4, 6+1);
                    break;
                }
            case 3:
                {
                    if (choose == true)
                        idex = 16;
                    else if (_step == GameConst.GameEventCount[_level] - 1)
                        idex = Random.Range(18, 19+1);
                    else idex = Random.Range(7, 9+1);
                    break;
                }
            case 4:
                {
                    if (choose == true)
                        idex = 17;
                    else if (_step == GameConst.GameEventCount[_level] - 1)
                        idex = 20;
                    else idex = Random.Range(10, 14+1);
                    break;
                }
        }
        return idex;
    }
}
