public static class GlobalConst
{
    

}

public enum TargetOptions //卡牌目标选项
{
    NoTarget,

    AllCreatures, //所有单位
    AllPlayerCreatures,  //所有玩家单位（包括玩家本体）
    AllEnemyCreatures,  //所有敌方单位（包括boss本体）

    SinglePlayerCreatures, //单个玩家目标
    SingleEnemyCreature, //单个敌方目标

    AllCharacters,  //所有随从（不包括玩家和boss）
    PlayerCharacters, //玩家随从
    EnemyCharacters  //敌方随从
}
public enum RarityRank  //稀有度
{
    Legend, Gold, Silver, Normal
}
public enum CardType  
{
    Player, Monster, Spell, Survent
}
public enum CardCamp
{
    BlueOcean, Oddin, BaseSpeed, Fortress, Enemy, Player, Snake
}
public enum CardEffect
{
    Attack,  //攻击
    VampireAttack,  //吸血
    Taunt,  //嘲讽
    DeadWhisper, //亡语
    Heal, //治疗
    HPEnhance, //生命强化
    Protect,  //加护
    Inspire, //激励(临时攻击提升)
    Waghhh, //Waghhhhhh!!!(永久攻击提升)
}
