using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
public class CardHubSystem : MonoBehaviour
{
	List<int> CardIdDeck;
	List<GameObject> CardObjectDeck;

	bool[] unlock;
	List<GameObject> CardHub;

	public GameObject CardHubContent;
	public GameObject CardDeckContent;

	public GameObject CardInDeckPrefab;
	public GameObject CardInHubPrefab;

	private void Awake()
	{

	}
	void LoadInformation()
	{
		if(Player.Instance != null)
		{
			CardIdDeck = Player.Instance.cardSet;
			CardObjectDeck = new List<GameObject>();
			for(int i = 0; i < CardIdDeck.Count; i++)
			{
				GameObject newCard = Instantiate(CardInDeckPrefab, CardDeckContent.transform);
				newCard.GetComponent<CardDisplay>().Initialized(Resources.Load<CardSOAsset>(Const.CARD_DATA_PATH(CardIdDeck[i])));
				CardObjectDeck.Add(newCard);
			}

			unlock = Player.Instance.Unlocked;
			for(int i = 0; i < unlock.Length; i++)
			{
				var asset = Resources.Load<CardSOAsset>(Const.CARD_DATA_PATH(i));
				if(asset != null)
				{
					GameObject newCard = Instantiate(CardInHubPrefab, CardHubContent.transform);
					newCard.GetComponent<CardDisplay>().Initialized(asset);
					newCard.GetComponent<CardDisplay>().CardBackActive(!unlock[i]);
					CardHub.Add(newCard);
				}
			}
		}
	}

	void SaveInformation()
	{
		if (Player.Instance != null)
		{
			Player.Instance.SetCardSet(CardIdDeck);
		}
	}
}
