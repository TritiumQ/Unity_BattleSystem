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
		_flg2 = true;
	}
	void Foo2()
	{
		_flg4 = true;
	}
	private void Update()
	{
		Refresh();
	}

	Phase phase;
	bool flg2 = false;
	bool _flg2 = false;
	bool flg4 = false;
	bool _flg4 = false;

	void Refresh()
	{
		switch (phase)
		{
			case Phase.Phase_1:
				{
					phase = Phase.Phase_2;
				}
				break;
			case Phase.Phase_2:
				{
					while(phase == Phase.Phase_2)
					{

					}
					StartCoroutine(PhaseAction());
					if (flg2)
					{
						Debug.Log("Pahse2 End");
						flg2 = false;
						phase = Phase.Phase_3;
					}
				}
				break;
			case Phase.Phase_3:
				{
					phase = Phase.Phase_4;
				}
				break;
			case Phase.Phase_4:
				{
					if(flg4)
					{
						Debug.Log("Phase4 End");
						flg4 = false;
						phase = Phase.Phase_1;
						
					}
					else
					{
						StartCoroutine(PhaseAction());
					}
				}
				break;
			default:
				break;
		}
	}
	IEnumerator PhaseAction()
	{
		switch(phase)
		{
			case Phase.Phase_2:
				{
					Debug.Log("phase2 start");

					yield return new WaitUntil(() => _flg2);
					
					_flg2 = false;
					flg2 = true;
					Debug.Log("phase2 complete");
				}
				break;
			case Phase.Phase_4:
				{
					Debug.Log("phase4 start");

					yield return new WaitUntil(() => _flg4);

					_flg4 = false;
					flg4 = true;
					Debug.Log("phase4 complete");
				}
				break;
			default:
				break;
		}
	}
	enum Phase
	{
		Phase_1,
		Phase_2,
		Phase_3,
		Phase_4,
	}
}
