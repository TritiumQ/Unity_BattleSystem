using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DebugSystem : MonoBehaviour
{
	public Button Func1;
	public Button Func2;

	private void Awake()
	{
		Func1.onClick.AddListener(Foo1);
		Func2.onClick.AddListener(Foo2);
		phase = Phase.Phase_1;
	}
	void Foo1()
	{

	}
	void Foo2()
	{

	}
	private void Update()
	{
		StartCoroutine(Refresh());
	}

	Phase phase;

	bool flg2 = true;

	IEnumerator Refresh()
	{
		switch (phase)
		{
			case Phase.Phase_1:
				{
					flg2 = true;
					Debug.Log("1");
					phase = Phase.Phase_2;
				}
				yield break;
			case Phase.Phase_2:
				{
					if(flg2)
					{
						Debug.Log("1 front");
						yield return new WaitForSeconds(1f);
						Debug.Log("1 back");
						phase = Phase.Phase_3;
						flg2 = false;
					}
				}
				yield break;
			case Phase.Phase_3:
				{
					phase = Phase.Phase_4;
				}
				yield break;
			case Phase.Phase_4:
				{

					phase = Phase.Phase_1;
				}
				yield break;
			default:
				yield break;
		}
	}
}
enum Phase
{
	Phase_1,
	Phase_2,
	Phase_3,
	Phase_4,
}