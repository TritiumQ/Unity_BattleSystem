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
    RandomTarget,
    HighestHPTarget,
    LowestHPTarget,
    HigestATKTarget,
    LowestATKTarget,
    /// <summary>
    /// �ض�Ŀ�꣬����ҷ������Ч����ʹ��
    /// </summary>
    SpecificTarget,
}
/// <summary>
/// Ŀ��ѡ��
/// </summary>
/// <returns></returns>
public enum TargetOptions
{
    NoTarget,
    /// <summary>
    /// ���е�λ
    /// </summary>
    AllCreatures,
    /// <summary>
    /// ������ҵ�λ��������ұ��壩
    /// </summary>
    AllPlayerCreatures,
    /// <summary>
    /// ���ез���λ������boss���壩
    /// </summary>
    AllEnemyCreatures,
    /// <summary>
    /// ������ӣ���������Һ�boss��
    /// </summary>
    AllCreaturesExcludeMainUnit,
    /// <summary>
    /// ���������� ����������ң�
    /// </summary>
    AllPlayerCreaturesExcludePlayerUnit,
    /// <summary>
    /// ���ез���ӣ�������boss��
    /// </summary>
    ALlEnemyCreaturesExcludeBossUnit,

    MultiPlayerTargets,
    MultiEnemyTargets,

    SinglePlayerTarget,
    SingleEnemyTarget,

    /// <summary>
    /// ������ΪĿ��
    /// </summary>
    OneselfTarget,


}
public enum RarityRank  //ϡ�ж�
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
public enum UnitType
{
    None,
    Player,
    PlayerSurvent,
    Boss,
    BossSurvent,

}
public enum CardCamp
{
    ����ҽ��, 
    �¶���ҵ, 
    ��������, 
    ����˹����, 
    ��,
    Enemy,
}
public enum GameResult
{
    Success,
    Failure,
    Escape,
}
/// <summary>
/// ���/����Ч��
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
    /// ����ǿ������ֵ1->��������ֵ2->����
    /// </summary>
    Enhance,
    /// <summary>
    /// ��ʱǿ������ֵ1->��������ֵ2->����, ��ֵ3->�����غ���
    /// </summary>
    Inspire,
    /// <summary>
    /// �ض�����, ��ֵ1->����id, ��ֵ2->��������
    /// </summary>
    DrawSpecificCard,
    /// <summary>
    /// �������, ��ֵ1->��������
    /// </summary>
    DrawRandomCard,

    SpecialEffect,

    FeedbackAttack,
    /// <summary>
    /// �ٻ��ض���λ����ֵ1-������id����ֵ2->�ٻ�����
    /// </summary>
    Summon,

    SetVampire,
    
    SetDoubleHit,

    ActionPointRecovery,


	//*����*��*�Ȼ�*��*ÿ�غϿ�ʼʱ*��*ÿ�غϽ���ʱ*, boss�����⼼��
}

public enum GameStage
{
    Void,

    RoundStart,

    PlayerAdvancedAction,
    EnemyAdvancedAction,

    PlayerDrawCard,
    PlayerAction,
    EnemyAction,

    PlayerSubsequentAction,
    EnemySubsequentAction,

    RoundEnd,
}

public enum ActionType
{
    Void,
    NormalAction,
    ExtraAction,
}

public class ExtraActionPackage
{
    public UnityEngine.GameObject Initiator;
    public EffectPackageWithTargetOption Effect;
    public bool IsEffectOver;
    public ExtraActionPackage()
	{
        Initiator = null;
        Effect = null;
        IsEffectOver = false;
	}
}

