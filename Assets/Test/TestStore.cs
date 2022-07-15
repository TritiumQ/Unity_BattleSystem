using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;

public class TestStore : MonoBehaviour
{
    //List<CardAsset> cardList;
	CardAsset card1;
	CardAsset card2;
	int flg = 1;
	private void Awake()
	{
		/*cardList = new List<CardAsset>();
		for(int i = 0; i < 3; i++)
		{
			string path = "CardDatas/SVN-" + i.ToString("D3");
			CardAsset card = Resources.Load<CardAsset>(path);
			cardList.Add(card);
		}*/
		card1 = Resources.Load<CardAsset>("CardDatas/SVN-001");
		card2 = Resources.Load<CardAsset>("CardDatas/SVN-003");
	}
	public CardAsset GetCard()
	{
		if (flg == 1)
		{
			flg = 3;
			return card1;
		}
		else
		{
			flg = 1;
			return card2;
		}
	}
}
