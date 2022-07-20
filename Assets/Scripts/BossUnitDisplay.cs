using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class BossUnitDisplay : MonoBehaviour
{
    Boss boss;
    public TextMeshProUGUI hpText;
    //public TextMeshProUGUI bossNameText;
	public TextMeshProUGUI atkText;
	public Image headIcon;
	//public Image backGroungImage;
	private void Start()
	{
		Refresh();
	}
	void Refresh()
	{
		if(boss != null)
		{
			hpText.text = boss.curentHP.ToString();
			atkText.text = boss.ATK.ToString();

		}
	}
}
