using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class SelectCardTrigger : MonoBehaviour, IPointerClickHandler
{
	public void OnPointerClick(PointerEventData eventData)
	{
		//Debug.Log("Click");
		if(eventData.clickCount == 2)
		{
			CardHubSystem system = GameObject.Find("CardHubSystem").GetComponent<CardHubSystem>();
			if(system != null)
			{
				Debug.Log("Push commit");
				system.SelectCardCommit(gameObject);
			}
		}
	}
}
