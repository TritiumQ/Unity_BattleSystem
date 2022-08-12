using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
public class PlayerUnitManager : MonoBehaviour, IEffectRunner
{
	public PlayerInBattle player { get; private set; }

    public TextMeshProUGUI hpText;
    public TextMeshProUGUI actionPointText;
	public Image headIcon;
	private void Update()
	{
		Refresh();
	}
	public void Initialized()
	{
		player = new PlayerInBattle(Player.Instance);
	}
	public void Refresh()
	{
		if(player != null)
		{
			hpText.text = player.CurrentHP.ToString();
			actionPointText.text = player.CurrentActionPoint.ToString();
		}
	}

	public void AcceptEffect(object[] _parameterList)
	{
		
	}

	public void UpdateEffect()
	{
		
	}
}
