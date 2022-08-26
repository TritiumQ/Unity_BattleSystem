using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
public class CardHubSystem : MonoBehaviour
{
	Dictionary<int, GameObject> CardObejcts;
	Dictionary<int, int> CardCount;

	List<GameObject> CardHub;

	public GameObject CardHubContent;
	public GameObject CardDeckContent;

	public GameObject CardInDeckPrefab;
	public GameObject CardInHubPrefab;
	[Header("退出按钮")]
	public Button ExitButton;

	private void Awake()
	{
		ArchiveManager.LoadPlayerData();
		LoadInformation();
	}
	void LoadInformation()
	{
		if(Player.Instance != null)
		{
			CardObejcts = new Dictionary<int, GameObject>();
			CardCount = new Dictionary<int, int>();
			foreach (int id in Player.Instance.cardSet)
			{
				if(CardObejcts.ContainsKey(id))
				{
					CardCount[id]++;
					CardObejcts[id].GetComponent<CardInDeckManager>().currentCount++;
				}
				else
				{
					GameObject newCard = Instantiate(CardInDeckPrefab, CardDeckContent.transform);
					newCard.GetComponent<CardInDeckManager>().Initialized(ArchiveManager.LoadCardAsset(id));
					CardCount.Add(id, 1);
					CardObejcts.Add(id, newCard);
				}
			}
			CardHub = new List<GameObject>();
			for(int i = 0; i < Player.Instance.Unlocked.Length; i++)
			{
				var asset = Resources.Load<CardSOAsset>(Const.CARD_DATA_PATH(i));
				if(asset != null)
				{
					GameObject newCard = Instantiate(CardInHubPrefab, CardHubContent.transform);
					newCard.GetComponent<CardManager>().Initialized(asset);
					newCard.GetComponent<CardManager>().IsActive = Player.Instance.Unlocked[i];
					CardHub.Add(newCard);
				}
			}
		}
	}

	void SaveInformation()
	{
		if (Player.Instance != null)
		{
			List<int> CardSet = new List<int>();
			foreach (var card in CardCount)
			{
				for(int i = 0; i < card.Value; i++)
				{
					CardSet.Add(card.Key);
				}
			}
			Player.Instance.SetCardSet(CardSet);
		}
	}
	
	/// <summary>
	/// 配合脚本SelectCardTrigger使用
	/// </summary>
	/// <param name="card">卡牌ID</param>
	public void SelectCardCommit(GameObject card)
	{
		if(card != null)
		{
			if(card.GetComponent<CardInDeckManager>() != null)
			{
				Debug.Log("delete card");
				DeleteCardInDeck(card.GetComponent<CardInDeckManager>().Asset.CardID);
			}
			else if(card.GetComponent<CardManager>() != null && card.GetComponent<CardManager>().IsActive)
			{
				Debug.Log("add card");
				AddCardToDeck(card.GetComponent<CardManager>().Asset.CardID);
			}
		}
	}
	void AddCardToDeck(int _ID)
	{
		if (CardCount.ContainsKey(_ID))
		{
			if (CardCount[_ID] < Const.MaxSingleCardCount)
			{
				CardCount[_ID]++;
				CardObejcts[_ID].GetComponent<CardInDeckManager>().currentCount++;
			}
		}
		else
		{
			GameObject newCard = Instantiate(CardInDeckPrefab, CardDeckContent.transform);
			newCard.GetComponent<CardInDeckManager>().Initialized(ArchiveManager.LoadCardAsset(_ID));
			CardCount.Add(_ID, 1);
			CardObejcts.Add(_ID, newCard);
		}
	}
	void DeleteCardInDeck(int _ID)
	{
		if (CardCount.ContainsKey(_ID))
		{
			if (CardCount[_ID] > 1)
			{
				CardCount[_ID]--;
				CardObejcts[_ID].GetComponent<CardInDeckManager>().currentCount--;
			}
			else
			{
				CardCount.Remove(_ID);
				Destroy(CardObejcts[_ID]);
				CardObejcts.Remove(_ID);
			}
		}
	}
}
