using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class TestSystem : MonoBehaviour
{
	public GameObject prefab;
    Button btn;
	public GameObject sets;
	//TestStore store;
	int cnt = 0;
	int flg = 1;
	
	private void Awake()
	{
		btn = GameObject.Find("Load").GetComponent<Button>();
		btn.onClick.AddListener(LoadCard);
		//store = GetComponent<TestStore>();
		//sets = GameObject.Find("Sets").GetComponent<GameObject>();
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
			newCard.GetComponent<CardDisplay>().cardAsset = Resources.Load<CardAsset>(path);
			
			cnt++;
		}
	}
}
