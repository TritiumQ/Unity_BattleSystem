using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class CardTrigger : MonoBehaviour, IPointerClickHandler
{

	public GameObject thisCard;
	public void OnPointerClick(PointerEventData eventData)
	{
		if(eventData.clickCount == 2 )
		{

			BattleSystem sys = GameObject.Find("BattleSystem").GetComponent<BattleSystem>();
			if (thisCard.GetComponent<CardDisplay>().card.cardType == CardType.Spell)
			{
				if (sys.attacker == null)
				{
					Debug.Log("尝试使用法术卡牌");
					sys.UseCardByPlayer(thisCard);
				}
				else
				{
					sys.AttackOver();
				}
			}
			else
			{
				Debug.Log("尝试使用随从卡牌");
				sys.UseCardByPlayer(thisCard);
			}
		}
	}
}