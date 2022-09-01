using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class DebugSystem : MonoBehaviour
{
	public Button Func1;
	public Button Func2;
	public TextMeshProUGUI Text1;
	class Pack
	{
		public int id;
		public EffectPackage Effect;
		public bool ok;
		public Pack(int _id, bool _ok)
		{
			Effect = new EffectPackage(EffectType.Attack,1,1,1,"1");
			id = _id;
			ok = _ok;
		}
	}
	private void Awake()
	{
		Func1.onClick.AddListener(Foo1);
		Func2.onClick.AddListener(Foo2);
		phase = Phase.Phase_1;
	}
	void add()
	{
		for (int i = 0; i < 5; i++)
		{
			q.Enqueue(new Pack(i,false));
		}
	}
	Pack ready;
	void Foo1()
	{

	}
	void Foo2()
	{
		if(ready != null)
		{
			Debug.Log("Confirm" + ready.id);
			ready.ok = true;
		}
	}
	Phase phase;
	Queue<Pack> q = new Queue<Pack>();

	private void Update()
	{
		if (q.Count > 0 && q.Peek().ok)
		{
			Debug.Log("Complete" + ready.id);
			q.Dequeue();
			ready = null;
		}
		if (q.Count > 0)
		{
			//Debug.Log("Wait for EX");
			if (ready == null)
			{
				Debug.Log("Push" + q.Peek().id);
				ready = q.Peek();
			}
		}
		else
		{
			switch (phase)
			{
				case Phase.Phase_1:
					{
						Debug.Log("1");
						phase = Phase.Phase_2;
					}
					break;
				case Phase.Phase_2:
					{
						Debug.Log(2);
						add();
						phase = Phase.Phase_3;
					}
					break;
				case Phase.Phase_3:
					{
						Debug.Log(3);
						phase = Phase.Phase_1;
					}
					break;
				default:
					break;
			}
		}
	}



}
enum Phase
{
	Phase_1,
	Phase_2,
	Phase_3,
	Phase_ex,
}