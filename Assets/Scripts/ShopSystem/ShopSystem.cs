using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

public class ShopSystem : MonoBehaviour
{
    public ShopType shopType;
    /// <summary>
    /// 泪滴或秘银,视商店种类而定
    /// </summary>
    int currentMoneys;
	public TextMeshProUGUI moneyText;

	[Header("卡牌选择")]
	int[] cardPrice;
	public Button Card1;
	public Button Card2;
	public Button Card3;
	public Button Card4;

	[Header("物品选择")]
	public Button goods1;
	public Button goods2;
	public Button goods3;

	[Header("Exit Button")]
	public Button exitButton;

	private void Awake()
	{
		exitButton.onClick.AddListener(Exit);
		cardPrice = new int[4];

		Initialized(ShopType.Shop);
	}

	private void Update()
	{
		moneyText.text = currentMoneys.ToString();
	}

	void Initialized(ShopType type)
	{
		shopType = type;
		int idx = 0;
		foreach(var obj in new Button[4] { Card1, Card2, Card3, Card4 })
		{
			CardSOAsset asset = ArchiveManager.LoadCardAsset(GetRandom.GetRandomCard());
			//obj.GetComponentInChildren<CardManager>().Initialized(asset);
			//obj.transform.Find("Name").GetComponent<TextMeshProUGUI>().text = asset.CardName;
			Debug.Log(obj.transform.Find("Name").parent.name);
			/*
			switch (asset.CardRarity)
			{
				case RarityRank.Normal:
					cardPrice[idx] = Const.NormalCardPrice;
					break;
				case RarityRank.Rare:
					cardPrice[idx] = Const.RareCardPrice;
					break;
				case RarityRank.Epic:
					cardPrice[idx] = Const.EpicCardPrice;
					break;
				case RarityRank.Legend:
					cardPrice[idx] = Const.LegendCardPrice;
					break;
				default:
					cardPrice[idx] = 39;
					break;
			}
			obj.transform.Find("Price").GetComponent<TextMeshProUGUI>().text = cardPrice[idx].ToString();
			idx++;
			*/
		}
	}

	void Initialized(ShopType type,int cardID1, int cardID2, int cardID3, int cardID4)
	{
		shopType = type;
		if(Card1 != null)
		{
			
		}
		if(Card2 != null)
		{

		}
		if(Card3 != null)
		{

		}
		if(Card4 != null)
		{

		}

		if(goods1 != null)
		{

		}
		if(goods2 != null)
		{

		}
		if(goods3 != null)
		{

		}
	}

	void Exit()
	{
		//TODO 退出商店
		if(shopType == ShopType.Shop)
		{
			SceneManager.LoadScene("Main");
		}
		else if(shopType == ShopType.ShopInGame)
		{

		}
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
	
}
public enum ShopType
{
    Void,
    Shop,
    ShopInGame,
}
