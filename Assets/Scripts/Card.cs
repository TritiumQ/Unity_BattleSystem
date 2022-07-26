using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[System.Serializable]
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
    public bool isCharged; // 是否可突袭
    public string creatureScriptName;// 生物脚本名
    public int specialCreatureAmount;// 技能数值

    public bool isCopied; //复制体标记
    public string spellScriptName;
    public int spellCreatureAmount;
    public TargetOptions targets;
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
            isCharged = asset.IsCharged;
            creatureScriptName = asset.CreatureScriptName;
            specialCreatureAmount = asset.SpecialCreatureAmount;

            isCopied = asset.IsCopied;
            spellScriptName = asset.SpellScriptName;
            spellCreatureAmount = asset.SpellCreatureAmount;
            targets = asset.Targets;

		}
    }
}
