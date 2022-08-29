using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DebugSystem : MonoBehaviour
{
    public Button Func1;
	private void Awake()
	{
		Func1.onClick.AddListener(Foo);
	}
	public void Foo()
	{
		ArchiveManager.ResetPlayerDataFile();
	}

}
