using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class SurventUnitDisplay : MonoBehaviour
{
    //public CardAsset cardAsset;
    public CardDisplay previewManager;
    
    [Header("Text Component References")]
    public TextMeshProUGUI atkText;
    public TextMeshProUGUI hpText;

    [Header("Image References")]
    public Image cardImage;
    public Image cardGlowImage;
    public Image tauntImage;  //嘲讽
    public Image raidImage;  //快速
    public Image undeadImage;  //不死
    public Image vampireImage;  //
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

    private void Start()
    {
        //Initial();
    }

    void Refresh()  //刷新随从当前状态
	{

	}
    /*public void Initial()  //随从卡初始化
    {
        if(cardAsset != null)
		{
            //更新信息
            //卡牌图片
            cardImage.sprite = cardAsset.CardImage;
            //血量攻击
            atkText.text = cardAsset.Atk.ToString();
            hpText.text = cardAsset.MaxHP.ToString();

            if (previewManager != null)
            {
                //previewManager.cardt = cardAsset;
                previewManager.LoadInf();
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
    }*/
}
