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
	//TestStore store;
	int cnt = 0;
	int flg = 1;
	
	private void Awake()
	{
		cards = new List<Card>();
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
			
			if(flg==1)
			{
				flg = 3;
			}
			else
			{
				flg = 1;
			}
			string path = "CardDatas/SVN-" + flg.ToString("D3");
			Debug.Log(path);
			Card card = new Card(Resources.Load<CardAsset>(path));
			cards.Add(card);
			newCard.GetComponent<CardDisplay>().card = card;
			string msg = "Counts:" + cards.Count.ToString();
			Debug.Log(msg);
			cnt++;
		}
	}
}
