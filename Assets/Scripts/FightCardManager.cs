using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class FightCardManager : MonoBehaviour
{
    public CardAsset cardAsset;
    public CardDisplay previewManager;

    [Header("Text Component References")]
    public TextMeshProUGUI atkText;
    public TextMeshProUGUI hpText;

    [Header("Image References")]
    public Image cardImage;
    public Image cardGlowImage;
    public Image tauntImage;
    public Image raidImage;
    public Image undeadImage;
    public Image vampireImage;
    public Image guardImage;
    public Image concealImage;
    public Image doubleHitImage;

    private bool canAttackNow = false;
    public bool CanAttackNow
    {
        get
        {
            return canAttackNow;
        }
        set
        {
            canAttackNow = value;
            cardGlowImage.enabled = value;
        }
    }

    private void Awake()
    {
        if (cardAsset != null)
        {
            //ReadFromAsset();
        }
    }
	/*
		//随从卡初始化
		public void ReadFromAsset()
		{
			//更新信息
			//卡牌图片
			cardImage.sprite = cardAsset.CardImage;
			//血量攻击
			atkText.text = cardAsset.Atk.ToString();
			hpText.text = cardAsset.MaxHealth.ToString();

			if(previewManager != null)
			{
				previewManager.cardAsset = cardAsset;
				previewManager.ReadCardFromAsset();
			}

			//所有随从效果的Icon默认关闭
			tauntImage.enabled = false;
			raidImage.enabled = false;
			undeadImage.enabled = false;
			vampireImage.enabled = false;
			guardImage.enabled = false;
			concealImage.enabled = false;
			doubleHitImage.enabled = false;

			//随从光效默认关闭
			cardGlowImage.enabled = false;

		}
		*/
}
