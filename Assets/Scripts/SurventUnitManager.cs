using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class SurventUnitManager : MonoBehaviour
{
    //public CardAsset cardAsset;
    Card card;
    SurventInBattle survent;
    //public CardDisplay previewManager;
    int atk;
    int currentHP;
    int maxHP;

    //特殊效果区
    public int isRaid;
    public int inspireRounds;
    public int inspireValue;
    public int concealRounds;
    //public int silenceRounds;
    public int tauntRounds;
    public int protectedTimes;
    public int doubleHitRounds;
    public int vampireRounds;

    public bool isActive;

    [Header("Text Component References")]
    public TextMeshProUGUI atkText;
    public TextMeshProUGUI hpText;

    [Header("Image References")]
    public Image cardImage;
    public Image cardGlowImage;

	[Header("特殊效果图标")]
    public Image tauntImage;  //嘲讽
    public Image raidImage;  //快速
    public Image deadWhisperImage;  //亡语
    public Image vampireImage;  //吸血
    public Image protectedImage;  //加护
    public Image concealImage;  //隐匿
    public Image doubleHitImage;  //连击
    public void Initial(Card _card)
    {
        if (_card.cardType == CardType.Survent || _card.cardType == CardType.Monster)
        {
            card = _card;
            currentHP = card.maxHP;
            maxHP = card.maxHP;
            atk = card.atk;
            cardImage.sprite = card.cardImage;
            if(card.isTank == true)
			{
                this.tauntRounds = Const.Forever;
			}
            if(card.isCharged == true)
			{
                isActive = true;
                isRaid = 1;
			}
            else
			{
                isActive = false;
			}
            //所有随从效果的Icon默认关闭
            tauntImage.enabled = false;
            raidImage.enabled = false;
            deadWhisperImage.enabled = false;
            vampireImage.enabled = false;
            protectedImage.enabled = false;
            concealImage.enabled = false;
            doubleHitImage.enabled = false;

            //随从光效默认关闭
            cardGlowImage.enabled = false;

            Refresh();
        }
    }
    private void Update()
    {
        Refresh();
        if(currentHP <= 0)
		{
            Die();
		}
    }
    void Refresh()  //刷新随从当前状态
    {
        atkText.text = atk.ToString();
        hpText.text = currentHP.ToString();

    }
    void Die()
	{

	}
    public void CheckBuff()
	{
        if(inspireRounds > 0)
		{
            inspireRounds--;
		}
        if(tauntRounds != Const.Forever && tauntRounds > 0)
		{
            tauntRounds--;
		}
        if(isRaid > 0)
		{
            isRaid--;
		}
	}
    public int BeVampireAttack(int _value)
	{
        return 0;
	}
    public void BeAttacked(int _value)
	{
        if(protectedTimes == 0)
		{
            currentHP -= _value;
		}
        protectedTimes--;
	}
    public void BeHealed(int _value)
	{
        if(currentHP + _value < maxHP)
		{
            currentHP += _value;
		}
        else
		{
            currentHP = maxHP;
		}
	}
    public void BeConcealed(int _value)
	{
        
	}
    public void BeEnhanced(int _value)
	{
        currentHP += _value;
        maxHP += _value;
	}
    /*public void BeSilenced(int _rounds)
	{
        silenceRounds = _rounds;
        
	}*/

    public void BeInspired(int _value, int _rounds)
	{
        atk += _value;
        inspireRounds = _rounds;
        inspireValue = _value;
	}
    public void Waghhh(int _value)
	{
        atk += _value;
	}
    public void BeProtected(int _times)
	{
        protectedTimes = _times;
	}
    public void BeTaunted(int _rounds)
	{
        tauntRounds = _rounds;
	}
}
