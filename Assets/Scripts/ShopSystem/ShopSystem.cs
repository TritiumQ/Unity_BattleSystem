using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ShopSystem : MonoBehaviour
{
    public ShopType shopType;
    /// <summary>
    /// 泪滴或秘银,视商店种类而定
    /// </summary>
    int currentMoneys;
	public TextMeshProUGUI moneyText;

	[Header("卡牌选择")]
	public Button Card1;
	public Button Card2;
	public Button Card3;
	public Button Card4;

	[Header("物品选择")]
	public Button goods1;
	public Button goods2;
	public Button goods3;

	private void Awake()
	{

	}

	private void Update()
	{
		moneyText.text = currentMoneys.ToString();
	}

	public void SetCard(int pos)
	{
		switch(pos)
		{
			case 1:
				break;
			case 2:
				break;
			case 3:
				break;
			case 4:
				break;
			default:
				Debug.LogWarning("SetCard: 设定位置位置错误");
				break;
		}
	}
	public void SetGoods(int pos, GoodsSOAsset asset)
	{
		switch (pos)
		{
			case 1:
				break;
			case 2:
				break;
			case 3:
				break;
			default:
				Debug.LogWarning("SetCard: 设定位置位置错误");
				break;
		}
	}
	public void TestLoadData()
	{

	}
}
public enum ShopType
{
    Void,
    Shop,
    ShopInGame,
}
