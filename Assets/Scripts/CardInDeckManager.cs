using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
public class CardInDeckManager : MonoBehaviour
{
	CardInDeck cardInDeck;
	[Header("UI×é¼þ")]
	public TextMeshProUGUI cardCostText;
	public TextMeshProUGUI cardNameText;
	public TextMeshProUGUI cardCountText;

	public int currentCount;
	public void Initialized(Card _card)
	{
		cardInDeck = new CardInDeck(_card);
	}
	void Refresh()
	{
		cardCostText.text = cardInDeck.cardCost.ToString();
		cardNameText.text = cardInDeck.cardName;
		cardCountText.text = currentCount.ToString();
	}
	private void Update()
	{
		Refresh();
	}

}
