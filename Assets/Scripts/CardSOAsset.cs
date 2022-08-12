using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[SerializeField]
public class CardSOAsset : ScriptableObject
{
    [Header("基本信息")]
    //public CharacterAsset characterAsset;
    public int CardID = 0;
    public string CardName = "NULL";
    [TextArea(2, 3)]
    public string CardDescription = "NULL";// 卡牌描述
    public RarityRank CardRarity; //稀有度
    public CardType CardType;  //类型
    public CardCamp CardCamp;  //阵营
    public int Cost = 0;// 卡牌费用
    public Sprite CardImage;// 卡牌图像
    public Sprite RarityImage;// 稀有度图像

    [Header("随从/敌人信息")]
    public int MaxHP;// 最大生命值
    public int Atk;// 攻击力

    public int AtksPerTurn = 1;// 每回合攻击次数

    public bool IsTank; // 是否嘲讽
    public bool IsRaid; // 是否突袭
    public bool IsVampire;

    [Header("随从特殊效果列表")]
    public List<SpecialAbilityPackage> SpecialSkillList;
    /*
    [Header("放置时效果")]
    public bool IsSetupEffect;
    public List<EffectPackage> SetupEffectList;
    [Header("亡语信息")]
    public bool IsUndead; //亡语
    public List<EffectPackage> UndeadEffectList;
    [Header("先机信息")]
    public bool IsAdvanced; //先机
    public List<EffectPackage> AdvancedEffectList;
    [Header("后手信息")]
    public bool IsSubsequent; //后手
    public List<EffectPackage> SubsequentEffectList;
    */

    [Header("法术信息")]
    public bool IsCopidedEffect;
    public bool IsCopied; //复制体标记
    //public string SpellScriptName;
    public EffectType SpellActionType;
    public List<EffectPackage> SpellEffectList;
}
