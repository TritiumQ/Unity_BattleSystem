using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using DG.Tweening;

public class CardDisplay : MonoBehaviour
{

    //
    //public Card card;
    public CardSOAsset card { get; private set; }
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
	private void Start()
	{
        //Debug.Log("开始");
        LoadInf();
    }
	private void Update()
	{
        Refresh();
	}

	private bool canBePlayedNow = false;
    public bool CanBePlayedNow
    {
        get
        {
            return canBePlayedNow;
        }
        set
        {
            canBePlayedNow = value;
            CardFaceGlowImage.enabled = value;
        }
    }
	public void Initialized(CardSOAsset _card)
	{
        card = _card;
	}
	void Refresh()
	{
        // 更新卡牌信息
        // 添加图片
        CardRarityImage.sprite = card.RarityImage;
        CardGraphicImage.sprite = card.CardImage;
        // 添加卡牌名字
        NameText.text = card.CardName;
        // 添加卡牌费用
        CostText.text = card.Cost.ToString();
        //Debug.Log(cardAsset.Cost);
        // 添加描述
        DescriptionText.text = card.CardDescription;
        CampText.text = card.CardCamp.ToString();
    }
    public void LoadInf()
	{
        if(card != null)
		{
            Refresh();
            //卡牌光效默认关闭
            CardFaceGlowImage.enabled = false;
            //默认关闭卡背
            CardBackImage.enabled = false;
            if (card.CardType == CardType.Survent)
            {
                AtkText.text = card.Atk.ToString();
                HealthText.text = card.MaxHP.ToString();
            }
            else
            {
                AtkText.GetComponent<TextMeshProUGUI>().gameObject.SetActive(false);
                HealthText.GetComponent<TextMeshProUGUI>().gameObject.SetActive(false);
            }
        }
	}
    public void SetPreview()
	{
        CardFaceGlowImage.enabled = true;
    }
    public void EndPreview()
	{
        CardFaceGlowImage.enabled = false;
	}
}
