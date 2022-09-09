using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SupriceHaHaHa : MonoBehaviour
{
    public Button logo;
	int counter = 1;
	private void Awake()
	{
		logo.onClick.AddListener(Counter);
	}
	private void Update()
	{
		if(counter >= 39)
		{
			ArchiveManager.LoadPlayerData(11);
		}
	}

	void Counter()
	{
		counter++;
	}

}
