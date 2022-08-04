using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
public class TestSystem : MonoBehaviour
{
	public GameObject prefab;
    Button btn;
	TextMeshProUGUI countText;
	public GameObject sets;
	List<GameObject> gameObjects;
	int idx = -5;
	//public Button bbtn2;
	//TestStore store;
	private void Awake()
	{
		//bbtn2.onClick.AddListener(set);

		//bbtn2.enabled = false;
		gameObjects = new List<GameObject>();
		btn = GameObject.Find("Load").GetComponent<Button>();
		btn.onClick.AddListener(TestJSONLoader.ResetPlayerDataFile);
		//btn.onClick.AddListener(LoadCard);
		//btn.onClick.AddListener(Send);
		countText = GameObject.Find("Count").GetComponent<TextMeshProUGUI>();
		//store = GetComponent<TestStore>();
		//sets = GameObject.Find("Sets").GetComponent<GameObject>();
	}
	private void set()
	{
		Debug.Log("±»µã»÷");
	}
	void Send()
	{
		//SendMessage("YEYEYE");
		SendMessageUpwards("YEYEYE");
	}

	public void LoadCard()
	{
		if(gameObjects.Count<10)
		{
			//GameObject newCard = GameObject.Instantiate(prefab,sets.transform);
			GameObject newCard = GameObject.Instantiate(prefab, sets.transform);

			//int flg = Random.Range(-5,6);
			int flg = idx++;
			Debug.Log(Const.CARD_DATA_PATH(flg));
			Card card = new Card(Resources.Load<CardSOAsset>(Const.CARD_DATA_PATH(flg)));
			newCard.GetComponent<CardDisplay>().card = card;
			gameObjects.Add(newCard);

			countText.text = gameObjects.Count.ToString();
			string msg = "Counts:" + gameObjects.Count.ToString();
			Debug.Log(msg);
				
		}
		/*else
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
		}*/
	}
}
