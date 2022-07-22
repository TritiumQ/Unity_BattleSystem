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
    //public CardDisplay previewManager;
    int atk;
    int currentHP;
    int maxHP;

    //特殊效果区

    public int inspireRounds;
    public int inspireValue;

    public int silenceRounds;

    public int tauntRounds;

    public int protectTimes;

    [Header("Text Component References")]
    public TextMeshProUGUI atkText;
    public TextMeshProUGUI hpText;

    [Header("Image References")]
    public Image cardImage;
    public Image cardGlowImage;

	[Header("特殊效果图标")]
    public Image tauntImage;  //嘲讽
    public Image raidImage;  //快速
    public Image undeadImage;  //不死
    public Image vampireImage;  //
    public Image guardImage;
    public Image concealImage;
    public Image doubleHitImage;

    private void Update()
    {
        Refresh();
        if(currentHP <= 0)
		{
            Die();
		}
    }
    void Die()
	{

	}
    public void CheckBuff()
	{
        
	}
    public int BeVampireAttack(int _value)
	{
        return 0;
	}
    public void BeAttacked(int _value)
	{
        if(protectTimes == 0)
		{
            currentHP -= _value;
		}
        protectTimes--;
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
    public void BeEnhanced(int _value)
	{
        currentHP += _value;
        maxHP += _value;
	}
    public void BeSilenced(int _rounds)
	{
        silenceRounds = _rounds;
        
	}
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
        protectTimes = _times;
	}
    public void BeTaunted(int _rounds)
	{
        tauntRounds = _rounds;
	}
    void Refresh()  //刷新随从当前状态
	{
        atkText.text = atk.ToString();
        hpText.text = currentHP.ToString();
	}
    public void Initial(Card _card)
	{
        if(_card.cardType == CardType.Survent || _card.cardType == CardType.Monster)
		{
            card = _card;
            currentHP = card.maxHP;
            maxHP = card.maxHP;
            atk = card.atk;
            cardImage.sprite = card.cardImage;

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

            Refresh();
		}
	}


}
