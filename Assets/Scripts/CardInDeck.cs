using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CardInDeck
{
	public string cardName;
	public int cardID;
	public int cardCost;
    public CardInDeck(Card _card)
	{
		cardName = _card.cardName;
		cardID = _card.cardID;
		cardCost = _card.cost;
	}
}
