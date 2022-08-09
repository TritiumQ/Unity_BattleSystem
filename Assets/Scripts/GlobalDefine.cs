
public enum BossActionType
{
    Skip,
    AOEAttack,
    AOEAttackExcludePlayer,
    SingleAttack,
    Summon,

}
public enum BossSingleAttack
{
    PlayerTarget,
    RandomTarget,
    HighestHPTarget,
    LowestHPTarget,
    HigestATKTarget,
    LowestATKTarget
}
public enum GetSurventInfomation
{
    ATK,
    CurrentHP,
    MaxHP,
    Type
}
/// <summary>
/// 常量类
/// </summary>
/// <returns></returns>
public static class Const
{
    //返回相应资源文件位置字符串
    public static string CARD_DATA_PATH(int _id)
	{
        if (_id > 0)
        {
            return "CardDatas/SVN-" + _id.ToString("D3");
        }
        else if (_id < 0)
        {
            return "CardDatas/SPL-" + (-_id).ToString("D3");
        }
        else return null;
    }
    public static string MONSTER_CARD_PATH(int _id)
	{
        return "CardDatas/MON-" + _id.ToString("D3");
	}
    public static string BOSS_DATA_PATH(int _id)
	{
        return "BossDatas/MON-" + _id.ToString("D3");
    }
    public static string PLAYER_DATA_PATH(int _id)
	{
        return UnityEngine.Application.dataPath + "/PlayerDatas/" + _id.ToString("D2") + ".json";
	}
    //常量
    public static int Forever = -1;
    public static int MaxSaveCount = 10;
    public static string InitialCode = "mikufans";
    
}
public struct PlayerBattleInformation
{
    public string name;
    public int maxHP;
    public int currentHP;
    public System.Collections.Generic.List<int> cardSet;
}

/// <summary>
/// 目标选择
/// </summary>
/// <returns></returns>
public enum TargetOptions
{
    NoTarget,
    
    AllCreatures, //所有单位
    PlayerCreatures,  //所有玩家单位（包括玩家本体）
    EnemyCreatures,  //所有敌方单位（包括boss本体）

    SinglePlayerTarget, //单个玩家目标
    SingleEnemyTarget, //单个敌方目标

    MultiPlayerTargets,
    MultiEnemyTargets,

    AllCharacters,  //所有随从（不包括玩家和boss）
    PlayerCharacter, //所有玩家随从 （不包括玩家）
    EnemyCharacters  //所有敌方随从（不包括boss）
}
public enum RarityRank  //稀有度
{
    Legend, Gold, Silver, Normal
}
public enum CardType  
{
    Monster, 
    Spell, 
    Survent
}
public enum CardCamp
{
    蓝海医疗, 奥丁工业, 基速物流, 福特斯安保, 
    Enemy,
    Snake
}

/// <summary>
/// 随从/法术效果
/// </summary>
/// <returns></returns>
public enum CardActionType
{
    Attack,  //攻击->伤害值
    VampireAttack,  //吸血->伤害值
    Taunt,  //嘲讽->回合数
    Heal, //治疗->回复量
    HPEnhance, //生命强化->强化量
    Protect,  //加护->回合数
    Inspire, //激励(临时攻击提升)->数值+回合
    Waghhh, //Waghhhhhh!!!(永久攻击提升)->数值
    Conceal, //隐匿->回合

    //Silence, //沉默->回合数

    //*亡语*，*先机*，*每回合开始时*，*每回合结束时*的效果通用
}

public static class LevelEvent
{
    public static int[] GameEventCount = { 0, 5, 6, 7, 8 };//初始化每层关卡数量（1-4）
    public static int[] level_1 = { 1, 1, 3, 3, 4 };
    public static int[] level_2 = { 1, 1, 3, 3, 4, 2 };
    public static int[] level_3 = { 1, 1, 1, 2, 3, 3, 5 };
    public static int[] level_4 = { 1, 1, 1, 3, 3, 4, 2, 5 };

    public static int[] GetArrray(int[] _target)
    {
        int[] copy = (int[])_target.Clone();
        return copy;
	}
}

/// <summary>
/// 用于玩家信息和JSON储存文件之间转换使用
/// </summary>
/// <returns></returns>
public class PlayerJSONInformation
{
    public string Name;
    public int MaxHP;
    public int CurrentHP;
    public int Mithrils; //秘银
    public int Tears;  //泪滴
    public int[] CardSet;
    public int[] UnlockCard;

    public PlayerJSONInformation(string InitialCode = "null")
    {
        if(InitialCode == "mikufans")
		{
            Name = "MIKU";
            MaxHP = 39;
            CurrentHP = 16;
            Mithrils = 20070831;
            Tears = 0x39C5BB;

            CardSet = new int[2];
            CardSet[0] = 0;
            CardSet[1] = 200;

            UnlockCard = new int[2];
            UnlockCard[0] = 0;
            UnlockCard[1] = 200;
        }
        else
		{
            Name = null;
            MaxHP = -1;
            CurrentHP = -1;
            Mithrils= -1;
            Tears = -1;
            CardSet = null;
            UnlockCard = null;
		}
    }

    /// <summary>
    /// 从Player类初始化
    /// </summary>
    /// <returns></returns>
    public PlayerJSONInformation(Player _player)
	{
        Name = _player.name;
        MaxHP = _player.maxHP;
        CurrentHP = _player.currentHP;
        Mithrils = _player.mithrils;
        Tears = _player.tears;

        CardSet = _player.cardSet.ToArray();

        System.Collections.Generic.List<int> tmpList = new System.Collections.Generic.List<int>();
        for(int i = 0; i < _player.unlock.Length; i++)
		{
			if (_player.unlock[i] == true)
			{
                tmpList.Add(i);
			}
		}
        UnlockCard = tmpList.ToArray();
        tmpList.Clear();
	}
}