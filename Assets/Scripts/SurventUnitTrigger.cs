using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using DG.Tweening;

public class SurventUnitTrigger : MonoBehaviour, IPointerClickHandler
{
	public GameObject thisSurvent;
	
    public GameObject arrow;

	public CardType cardType;

	public void OnPointerClick(PointerEventData eventData)
	{
		BattleSystem sys = GameObject.Find("BattleSystem").GetComponent<BattleSystem>();
		if (thisSurvent.GetComponent<SurventUnitManager>().type == CardType.Survent && thisSurvent.GetComponent<SurventUnitManager>().isActive)
		{
			if (arrow.activeSelf)
			{
				arrow.SetActive(false);
				//sys.AttackCancel();
			}
			else
			{
				//arrow.SetActive(true);
				//sys.AttackRequest(thisSurvent);
			}
		}
		else
		{
			//sys.AttackConfirm(thisSurvent);
		}
	}

	//public TargetingOptions Targets = TargetingOptions.AllCharacters;
	private void Start()
	{
		arrow.SetActive(false);
		//Test();
	}
	//
	private void Test()
	{
		if(cardType == CardType.Survent)
		{
			thisSurvent.GetComponent<SurventUnitManager>().Initialized(new Card(Resources.Load<CardSOAsset>(Const.CARD_DATA_PATH(2))));
		}
		else
		{
			thisSurvent.GetComponent<SurventUnitManager>().Initialized(new Card(Resources.Load<CardSOAsset>(Const.MONSTER_CARD_PATH(1))));
		}
	}
}