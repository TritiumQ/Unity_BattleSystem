using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class BossSOAsset : ScriptableObject
{
    public int ID;
    public string Name;
    public int MaxHP;
    public int ATK;
    //public string ActionLogicName;
    public List<BossActionType> ActionCycle;
    //public Image HeadIcon;
}
