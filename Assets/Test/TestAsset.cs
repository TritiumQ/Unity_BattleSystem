using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestAsset : ScriptableObject
{
	public string str;
	public int num;
	public TestClass cls;
}
[System.Serializable]
public class TestClass
{
	public string str;
	public int num;

}