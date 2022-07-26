using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
public class CardHubSystem : MonoBehaviour
{
	public GameObject CardHubContent;
	public GameObject CardDeckContent;
	List<GameObject> currentCardSet;

	private void Awake()
	{
		

	}
	public List<int> GetCardSetInformation()
	{
		List<int> cardSetInformation = new List<int>();
		for(int i = 0; i < currentCardSet.Count; i++)
		{
			//
		}
		return cardSetInformation;
	}
	
}
