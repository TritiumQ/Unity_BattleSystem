using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class UseCardTrigger : MonoBehaviour, IPointerClickHandler
{
	public void OnPointerClick(PointerEventData eventData)
	{
		if(eventData.clickCount == 2 )
		{
			BattleSystem sys = GameObject.Find("BattleSystem").GetComponent<BattleSystem>();
			Debug.Log("≥¢ ‘ π”√ø®≈∆");
			sys.UseCardByPlayer(gameObject);
		}
	}
}