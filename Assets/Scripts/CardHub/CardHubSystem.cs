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
		LoadInformation();
	}
	void LoadInformation()
	{
		ArchiveManager.LoadPlayerData();
		if(Player.Instance != null)
		{
			CardIdDeck = Player.Instance.cardSet;
			CardObjectDeck = new List<GameObject>();
			foreach (int id in CardIdDeck)
			{
				GameObject newCard = Instantiate(CardInDeckPrefab, CardDeckContent.transform);
				newCard.GetComponent<CardInDeckManager>().Initialized(Resources.Load<CardSOAsset>(Const.CARD_DATA_PATH(id)));
				CardObjectDeck.Add(newCard);
			}

			unlock = Player.Instance.Unlocked;
			CardHub = new List<GameObject>();
			for(int i = 0; i < unlock.Length; i++)
			{
				var asset = Resources.Load<CardSOAsset>(Const.CARD_DATA_PATH(i));
				if(asset != null)
				{
					GameObject newCard = Instantiate(CardInHubPrefab, CardHubContent.transform);
					newCard.GetComponent<CardDisplay>().Initialized(asset);
					newCard.GetComponent<CardDisplay>().IsActive = unlock[i];
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
