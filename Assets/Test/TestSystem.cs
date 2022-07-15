using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class TestSystem : MonoBehaviour
{
	public GameObject prefab;
    Button btn;
	Button hpp, atkp;
	public GameObject sets;
	List<Card> cards;
	List<GameObject> gameObjects;
	//TestStore store;
	int cnt = 0;
	private void Awake()
	{
		cards = new List<Card>();
		gameObjects = new List<GameObject>();
		btn = GameObject.Find("Load").GetComponent<Button>();
		btn.onClick.AddListener(LoadCard);
		hpp = GameObject.Find("HP+").GetComponent<Button>();
		hpp.onClick.AddListener(ChangedHP);
		atkp = GameObject.Find("ATK+").GetComponent<Button>();
		atkp.onClick.AddListener(ChangedATK);
		//store = GetComponent<TestStore>();
		//sets = GameObject.Find("Sets").GetComponent<GameObject>();
	}

	private void ChangedATK()
	{
		if(0 < cards.Count)
		{
			cards[0].atk++;
		}
	}
	private void ChangedHP()
	{
		if (0 < cards.Count)
		{
			cards[0].maxHP++;
		}
	}

	public void LoadCard()
	{
		if(cnt<3)
		{
			GameObject newCard = GameObject.Instantiate(prefab,sets.transform);
			
			int flg = Random.Range(-5,6);
			Debug.Log(Const.CARD_DATA_PATH(flg));
			Card card = new Card(Resources.Load<CardAsset>(Const.CARD_DATA_PATH(flg)));
			cards.Add(card);
			gameObjects.Add(newCard);
			newCard.GetComponent<CardDisplay>().card = card;
			string msg = "Counts:" + cards.Count.ToString();
			Debug.Log(msg);
			cnt++;
				
		}
		else
		{
			
			GameObject oldCard = gameObjects[0];
			gameObjects.RemoveAt(0);
			Destroy(oldCard);

			GameObject newCard = GameObject.Instantiate(prefab, sets.transform);

			int flg = Random.Range(-5, 6); 
			Debug.Log(Const.CARD_DATA_PATH(flg));
			Card card = new Card(Resources.Load<CardAsset>(Const.CARD_DATA_PATH(flg)));
			cards.Add(card);
			gameObjects.Add(newCard);
			newCard.GetComponent<CardDisplay>().card = card;
			string msg = "Counts:" + cards.Count.ToString();
			Debug.Log(msg);
			cnt++;
		}
	}
}
