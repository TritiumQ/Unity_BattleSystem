using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CardInDeck
{
	public string cardName;
	public int cardID;
	public int cardCost;
    public CardInDeck(CardSOAsset _card)
	{
		cardName = _card.CardName;
		cardID = _card.CardID;
		cardCost = _card.Cost;
	}
}
