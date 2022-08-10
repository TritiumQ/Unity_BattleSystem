using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using UnityEngine.UI;
using TMPro;
public class TestSystem : MonoBehaviour
{
	public Button func1;
	public void Awake()
	{
		
	}
	void SetSOAsset(int st, int ed)
	{
		for(int i = st; i <= ed; i++)
		{
			var asset = ScriptableObject.CreateInstance<CardSOAsset>();
			asset.CardID = i;
			string path = "SVN-" + i.ToString("D3") + ".asset";
			Debug.Log(path);
			ProjectWindowUtil.CreateAsset(asset, path);
		}
	}
}
