using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using DG.Tweening;

public class CardDisplay : MonoBehaviour
{
    //public Card card;
    public CardSOAsset Asset { get; private set; }
    //public OneCardManager PreviewManager;
    [Header("Text Component References")]
    public TextMeshProUGUI NameText;// 卡牌名字
    public TextMeshProUGUI CostText;// 卡牌费用
    public TextMeshProUGUI DescriptionText;// 卡牌描述
    public TextMeshProUGUI HealthText;// 卡牌最大生命
    public TextMeshProUGUI AtkText;// 卡牌攻击
    public TextMeshProUGUI CampText;
    //[Header("GameObject References")]
    //public GameObject HealthIcon;
    //public GameObject AtkIcon;
    [Header("Image References")]
    public Image CardGraphicImage;// 卡牌图片
    public Image CardRarityImage;// 卡牌稀有度
    public Image CardFaceGlowImage;// 卡牌发光
    public Image CardBackImage;
	private void Update()
	{
        
	}
	public void Initialized(CardSOAsset _card)
	{
        Asset = _card;
        LoadInf();
	}
    public void LoadInf()
	{
        if(Asset != null)
		{
            // 添加图片
            CardRarityImage.sprite = Asset.RarityImage;
            CardGraphicImage.sprite = Asset.CardImage;
            // 添加卡牌名字
            NameText.text = Asset.CardName;
            // 添加卡牌费用
            CostText.text = Asset.Cost.ToString();
            //Debug.Log(cardAsset.Cost);
            // 添加描述
            DescriptionText.text = Asset.CardDescription;
            CampText.text = Asset.CardCamp.ToString();
            //卡牌光效默认关闭
            CardFaceGlowImage.enabled = false;
            //默认关闭卡背
            CardBackImage.enabled = false;
            if (Asset.CardType == CardType.Survent)
            {
                AtkText.text = Asset.Atk.ToString();
                HealthText.text = Asset.MaxHP.ToString();
            }
            else
            {
                AtkText.GetComponent<TextMeshProUGUI>().gameObject.SetActive(false);
                HealthText.GetComponent<TextMeshProUGUI>().gameObject.SetActive(false);
            }
        }
	}
    //控制卡背
    public void CardBackActive(bool isActive)
	{
        CardBackImage.enabled = isActive;
	}
    //控制预览
    public void SetPreview()
	{
        CardFaceGlowImage.enabled = true;
    }
    public void EndPreview()
	{
        CardFaceGlowImage.enabled = false;
	}
}
