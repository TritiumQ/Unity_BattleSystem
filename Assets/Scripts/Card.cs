using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
//计划弃用该类
public class Card 
{
    readonly CardSOAsset asset;

    public int cardID;
    public string cardName;
    public string cardDescription;
    public RarityRank cardRarity; //稀有度
    public CardType cardType;  //类型
    public CardCamp cardCamp;  //阵营
    public int cost; // 卡牌费用
    public Sprite cardImage;// 卡牌图像
    public Sprite rarityImage;// 稀有度图像

    public int maxHP;// 最大生命值
    public int atk;// 攻击力
    public int atksPerTurn = 1;// 每回合攻击次数
    public bool isTank; // 是否默认嘲讽
    public bool isRaid; // 是否可突袭
    public bool IsVampire;

    public bool isUndead; //亡语
    public CardActionType deadWhisperEffect;
    public int deadWhisperEffectValue;
    public TargetOptions deadWhisperTarget;

    public bool isAdvanced; //先机
    public CardActionType advancedEffect;
    public int advancedEffectValue;
    public TargetOptions advancedEffectTarget;

    public bool isSubsequent; //后手
    public CardActionType subsequentEffect;
    public int subsequentEffectValue;
    public TargetOptions subsequentEffectTarget;
    //随从放置时效果
    public bool IsSetupEffect;
    public CardActionType SetupEffect;
    public int SetupEffectValue;
    public TargetOptions SetupEffectTarget;
    //delete
    //public string creatureScriptName;// 生物脚本名
    //public int specialCreatureAmount;// 技能数值


    public bool isCopied; //复制体标记
    public string spellScriptName;
    public int spellActionValue;
    public TargetOptions targets;
    public CardActionType spellactionType;
    public Card(CardSOAsset _asset)
    {
        asset = _asset;
        if(asset != null)
		{
            cardID = asset.CardID;
            cardName = asset.CardName;
            cardDescription = asset.CardDescription;
            cardRarity = asset.CardRarity;
            cardType = asset.CardType;
            cardCamp = asset.CardCamp;
            cost = asset.Cost;
            cardImage = asset.CardImage;
            rarityImage = asset.RarityImage;
            
            maxHP = asset.MaxHP;
            atk = asset.Atk;
            atksPerTurn = asset.AtksPerTurn;
            isRaid = asset.IsRaid;
            isTank = asset.IsTank;
            IsVampire = asset.IsVampire;

            isUndead = asset.IsUndead;
            deadWhisperEffect = asset.DeadWhisperEffect;
            deadWhisperEffectValue = asset.DeadWhisperEffectValue;
            deadWhisperTarget = asset.DeadWhisperEffectTarget;

            isSubsequent = asset.IsSubsequent;
            subsequentEffect = asset.SubsequentEffect;
            subsequentEffectValue = asset.SubsequentEffectValue;
            subsequentEffectTarget = asset.SubsequentEffectTarget;

            isAdvanced = asset.IsAdvanced;
            advancedEffect = asset.AdvancedEffect;
            advancedEffectValue = asset.AdvancedEffectValue;
            advancedEffectTarget = asset.SubsequentEffectTarget;

            IsSetupEffect = asset.IsSetupEffect;
            SetupEffect = asset.SetupEffect;
            SetupEffectValue = asset.SetupEffectValue;
            SetupEffectTarget = asset.SetupEffectTarget;
            //delete
            //creatureScriptName = asset.CreatureScriptName;
            //specialCreatureAmount = asset.SpecialCreatureAmount;


            isCopied = asset.IsCopied;
            spellScriptName = asset.SpellScriptName;
            spellActionValue = asset.SpellActionValue;
            targets = asset.Targets;
            spellactionType = asset.SpellActionType;
		}
    }
}
