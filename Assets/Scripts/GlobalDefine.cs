public enum BossActionType  //
{
    Skip,
    AOEAttack,
    AOEAttackExcludePlayer,
    SingleAttack,
    Summon,

}
public enum BossAttack
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
    
}
public struct PlayerBattleInformation
{
    public string name;
    public int maxHP;
    public int currentHP;
    public System.Collections.Generic.List<int> cardSet;
}
public enum TargetOptions //卡牌目标选项
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
    //Player, 
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

