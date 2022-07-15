using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CardAsset : ScriptableObject
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
    public bool IsCharged; // 是否突袭
    public string CreatureScriptName;// 生物脚本名
    public int SpecialCreatureAmount;// 技能数值

    [Header("法术信息")]
    public bool IsCopied; //复制体标记
    public string SpellScriptName;
    public int SpellCreatureAmount;
    public TargetOptions Targets;

}
