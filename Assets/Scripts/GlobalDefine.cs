public enum BossActionType
{
    Skip,
    AOEAttack,
    AOEAttackExcludePlayer,
    SingleAttack,
    Summon,
}
public enum SingleTargetOption
{
    PlayerTarget,
    RandomTarget,
    HighestHPTarget,
    LowestHPTarget,
    HigestATKTarget,
    LowestATKTarget
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
    AllPlayerCreatures,  //所有玩家单位（包括玩家本体）
    AllEnemyCreatures,  //所有敌方单位（包括boss本体）

    SinglePlayerTarget, //单个玩家目标
    SingleEnemyTarget, //单个敌方目标

    MultiPlayerTargets,
    MultiEnemyTargets,

    AllCharacters,  //所有随从（不包括玩家和boss）
    ALlPlayerCharacter, //所有玩家随从 （不包括玩家）
    ALlEnemyCharacters  //所有敌方随从（不包括boss）
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
public enum GameResult
{
    Success,
    Failure,
    Escape,
}
/// <summary>
/// 随从/法术效果
/// </summary>
/// <returns></returns>
public enum EffectType
{
    Attack,
    VampireAttack,
    /// <summary>
    /// 自爆攻击，数值1->对自身伤害值, 数值2->对敌方伤害值
    /// </summary>
    SuicideAttack,
    Heal,
    Taunt,
    Protect,
    Conceal,
    /// <summary>
    /// 永久强化，数值1->生命，数值2->攻击
    /// </summary>
    Enhance,
    /// <summary>
    /// 临时强化，数值1->生命，数值2->攻击, 数值3->持续回合数
    /// </summary>
    Inspire,
    /// <summary>
    /// 特定抽牌, 数值1->卡牌id, 数值2->卡牌数量
    /// </summary>
    DrawSpecificCard,
    /// <summary>
    /// 随机抽牌, 数值1->抽牌数量
    /// </summary>
    DrawRandomCard

    //*亡语*，*先机*，*每回合开始时*，*每回合结束时*, boss的特殊技能
}

/// <summary>
/// 游戏局内全局常量
/// </summary>
/// <returns></returns>
public static class GameConst
{
    public static int[] GameEventCount = { 0, 5, 6, 7, 8 };//初始化每层关卡数量（1-4）
    public static int[] level_1 = { 1, 1, 3, 3, 4 };
    public static int[] level_2 = { 1, 1, 3, 3, 4, 2 };
    public static int[] level_3 = { 1, 1, 1, 2, 3, 3, 5 };
    public static int[] level_4 = { 1, 1, 1, 3, 3, 4, 2, 5 };

    public static int[] DrawCard = { 1,1,1,1,2,2,2,3,3,4 };
    public static int[,] Card_SVN = { { 0, 0 }, { 1, 23 }, { 50, 78 }, { 100, 117 }, { 150, 159 } };
    public static int[,] Card_SPL = { { 0, 0 }, { 201, 249 }, { 250, 299 }, { 300, 349 }, { 350, 399 } };//待修改

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
        Name = _player.Name;
        MaxHP = _player.MaxHP;
        CurrentHP = _player.CurrentHP;
        Mithrils = _player.Mithrils;
        Tears = _player.Tears;

        CardSet = _player.cardSet.ToArray();

        System.Collections.Generic.List<int> tmpList = new System.Collections.Generic.List<int>();
        for(int i = 0; i < _player.Unlocked.Length; i++)
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
