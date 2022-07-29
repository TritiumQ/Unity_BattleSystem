using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class CardTrigger : MonoBehaviour, IPointerClickHandler
{

	public GameObject thisCard;
	public void OnPointerClick(PointerEventData eventData)
	{
		if(eventData.clickCount == 2)
		{
			Debug.Log("≥¢ ‘ π”√ø®≈∆");
			BattleSystem sys = GameObject.Find("BattleSystem").GetComponent<BattleSystem>();
			sys.UseCardByPlayer(thisCard);
		}
	}
}