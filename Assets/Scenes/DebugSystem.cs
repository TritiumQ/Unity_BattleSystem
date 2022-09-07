using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class DebugSystem : MonoBehaviour
{
	public void foo()
	{
		ArchiveManager.ResetPlayerDataFile("starter");
	}
}
enum Phase
{
	Phase_1,
	Phase_2,
	Phase_3,
	Phase_ex,
}