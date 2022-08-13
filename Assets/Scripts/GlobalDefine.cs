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
/// <summary>
/// 目标选择
/// </summary>
/// <returns></returns>
public enum TargetOptions
{
    NoTarget,
    /// <summary>
    /// 所有单位
    /// </summary>
    AllCreatures,
    /// <summary>
    /// 所有玩家单位（包括玩家本体）
    /// </summary>
    AllPlayerCreatures,
    /// <summary>
    /// 所有敌方单位（包括boss本体）
    /// </summary>
    AllEnemyCreatures,
    /// <summary>
    /// 所有随从（不包括玩家和boss）
    /// </summary>
    AllCharacters,
    /// <summary>
    /// 所有玩家随从 （不包括玩家）
    /// </summary>
    ALlPlayerCharacter,
    /// <summary>
    /// 所有敌方随从（不包括boss）
    /// </summary>
    ALlEnemyCharacters,

    MultiPlayerTargets,
    MultiEnemyTargets,

    SinglePlayerTarget, //单个玩家目标
    SingleEnemyTarget, //单个敌方目标
}
public enum RarityRank  //稀有度
{
    Normal,
    Rare,
    Epic,
    Legend, 
}
public enum CardType  
{
    Spell, 
    Survent,
    Monster,
}
public enum CardCamp
{
    蓝海医疗, 
    奥丁工业, 
    基速物流, 
    福特斯安保, 
    蛇,
    Enemy,
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
    Void,
    Attack,
    VampireAttack,
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
    DrawRandomCard,

    SpecialEffect,

    //*亡语*，*先机*，*每回合开始时*，*每回合结束时*, boss的特殊技能
}
