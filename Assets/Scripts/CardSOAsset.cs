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

    //public int HitCount = 1;// 每回合攻击次数
    public bool IsDoubleHit;
    public bool IsTank; // 是否嘲讽
    public bool IsRaid; // 是否突袭
    public bool IsVampire;

    [Header("随从特殊效果列表")]
    public List<AbilityPackage> SpecialAbilityList;

    [Header("法术信息")]
    public bool IsCopidedEffect;
    public bool IsCopied; //复制体标记
    public EffectPackageWithTargetOption SpellEffect;
    //public List<EffectPackageWithTargetOption> SpellEffectList;
}
