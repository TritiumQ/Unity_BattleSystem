using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
public class TestSystem : MonoBehaviour
{
	public Button func1;
	public void Awake()
	{
		func1.onClick.AddListener(TestJSONLoader.ResetPlayerDataFile);
	}
}
