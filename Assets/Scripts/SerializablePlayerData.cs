using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/// <summary>
/// 用于玩家信息和JSON储存文件之间转换使用
/// </summary>
/// <returns></returns>
[System.Serializable]
public class SerializablePlayerData
{
    public string Name;
    public int MaxHP;
    public int CurrentHP;
    public int Mithrils; //秘银
    public int Tears;  //泪滴
    public int InitTears;
    public int[] CardSet;
    public bool IsAllUnlocked;
    public bool IsStarter;
    public int[] UnlockCard;

    

    public SerializablePlayerData(string InitialCode = "null")
    {
        if (InitialCode == "mikufans")
        {
            Name = "MIKU";
            MaxHP = 39;
            CurrentHP = 16;
            Mithrils = 20070831;
            Tears = 0x39C5BB;
            InitTears = 23452345;

            CardSet = new int[2];
            CardSet[0] = 0;
            CardSet[1] = 200;

            IsAllUnlocked = true;
            IsStarter = false;
        }
        else if(InitialCode == "null")
        {
            Name = null;
            MaxHP = -1;
            CurrentHP = -1;
            Mithrils = -1;
            Tears = -1;
            InitTears = -1;
            CardSet = null;
            IsAllUnlocked = false;
            IsStarter = false;
            UnlockCard = null;
        }
        else if(InitialCode == "starter")
		{
            Name = "Noob";
            MaxHP = 15;
            CurrentHP = 15;
            Mithrils = 0;
            Tears = 0;
            InitTears = 10;
            CardSet = null;
            IsAllUnlocked = false;
            UnlockCard = null;
            IsStarter = true;
        }
    }
    /// <summary>
    /// 从Player类初始化
    /// </summary>
    /// <returns></returns>
    public SerializablePlayerData(Player _player)
    {
        Name = _player.Name;
        MaxHP = _player.MaxHP;
        CurrentHP = _player.CurrentHP;
        Mithrils = _player.Mithrils;
        Tears = _player.Tears;
        InitTears = _player.InitTears;

        CardSet = _player.cardSet.ToArray();
        IsAllUnlocked = _player.IsAllUnlocked;
        System.Collections.Generic.List<int> tmpList = new System.Collections.Generic.List<int>();
        for (int i = 0; i < _player.Unlocked.Length; i++)
        {
            if (_player.Unlocked[i] == true)
            {
                tmpList.Add(i);
            }
        }
        UnlockCard = tmpList.ToArray();
        tmpList.Clear();
    }
}
