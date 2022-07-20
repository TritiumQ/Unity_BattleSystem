using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
public class PlayerUnitDisplay : MonoBehaviour
{
    public Player player;
    public TextMeshProUGUI hpText;
    public TextMeshProUGUI actionPointText;
	public Image headIcon;
	private void Start()
	{
		Refresh(0);
	}
	void Refresh(int _actionPoint) //由于战术点并不记录在Player类中，故需要传参刷新
	{
		if(player != null)
		{
			hpText.text = player.currentHP.ToString();
			actionPointText.text = _actionPoint.ToString();
		}
	}
}
