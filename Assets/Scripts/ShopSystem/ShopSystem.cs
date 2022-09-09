using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;


public class ShopSystem : MonoBehaviour
{
    public ShopType shopType;
	public GoodsEffectManager manager;
    /// <summary>
    /// 泪滴或秘银,视商店种类而定
    /// </summary>
    int currentMoneys;
	public TextMeshProUGUI moneyText;

	[Header("卡牌选择")]
	public Button Card1;
	int cardID1;
	int cardPrice1;

	public Button Card2;
	int cardID2;
	int cardPrice2;

	public Button Card3;
	int cardID3;
	int cardPrice3;

	public Button Card4;
	int cardID4;
	int cardPrice4;

	[Header("物品选择")]
	public Button goods1;
	int goods1Count;
	public Button goods2;
	int goods2Count;
	public Button goods3;
	int goods3Count;

	[Header("Exit Button")]
	public Button exitButton;

	private void Awake()
	{
		exitButton.onClick.AddListener(Exit);
		currentMoneys = 0;

		if(shopType == ShopType.Shop)
		{
			goods1Count = 2;
			goods2Count = 2;
			goods3Count = 2;
		}
		manager = GetComponent<GoodsEffectManager>();

		Initialized();
	}

	private void Update()
	{
		moneyText.text = currentMoneys.ToString();
		if (shopType == ShopType.Shop)
		{
			goods1.GetComponent<GoodsManager>().GoodsCount =  goods1Count;
			goods2.GetComponent<GoodsManager>().GoodsCount = goods2Count;
			goods3.GetComponent<GoodsManager>().GoodsCount = goods3Count;
			if (goods1Count <= 0)
			{
				goods1.enabled = false;
				goods1.interactable = false;
			}
			if (goods2Count <= 0)
			{
				goods2.enabled = false;
				goods2.interactable = false;
			}
			if (goods3Count <= 0)
			{
				goods3.enabled = false;
				goods3.interactable = false;
			}
		}
	}

	void Initialized()
	{
		for (int i = 1; i <= 4; i++)
		{
			Debug.Log("Card" + i);
			SetCard(i, ArchiveManager.LoadCardAsset(GetRandom.GetRandomCard()));
		}
		for (int i = 1; i <= 3; i++)
		{
			GoodsSOAsset asset = null;
			if(shopType == ShopType.Shop)
			{
				asset = ArchiveManager.LoadGoodsAsset(i);
			}
			else if(shopType == ShopType.ShopInGame)
			{
				asset = ArchiveManager.LoadGoodsAsset(i + 10);
			}
			if(asset != null)
			{
				SetGoods(i, asset);
				Debug.Log("Load Goods:" + asset.name);
			}

		}
		Load();
	}

	void Initialized(ShopSave save)
	{
		if(shopType != ShopType.Shop)
		{
			
		}
	}

	void Exit()
	{
		//TODO 退出商店
		Save();
		if(shopType == ShopType.Shop)
		{
			SceneManager.LoadScene("Main");
		}
		else if(shopType == ShopType.ShopInGame)
		{
			PlayerDataTF.EventContinue();
			SceneManager.LoadScene("GameProcess");
		}
	}

	public void SetCard(int pos, CardSOAsset asset)
	{
		int price;
		switch(asset.CardRarity)
		{
			case RarityRank.Normal:
				price = Const.NormalCardPrice;
				break;
			case RarityRank.Rare:
				price = Const.RareCardPrice;
				break;
			case RarityRank.Epic:
				price = Const.EpicCardPrice;
				break;
			case RarityRank.Legend:
				price = Const.LegendCardPrice;
				break;
			default:
				price = 39;
				break;
		}
		if(shopType == ShopType.ShopInGame)
		{
			price /= 2;
		}
		switch(pos)
		{
			case 1:
				{
					cardPrice1 = price;
					cardID1 = asset.CardID;
					Card1.GetComponentInChildren<CardManager>().Initialized(asset);
					Card1.transform.Find("Name").GetComponent<TextMeshProUGUI>().text = asset.CardName;
					Card1.transform.Find("Price").GetComponent<TextMeshProUGUI>().text = cardPrice1.ToString();
				}
				break;
			case 2:
				{
					cardPrice2 = price;
					cardID2 = asset.CardID;
					Card2.GetComponentInChildren<CardManager>().Initialized(asset);
					Card2.transform.Find("Name").GetComponent<TextMeshProUGUI>().text = asset.CardName;
					Card2.transform.Find("Price").GetComponent<TextMeshProUGUI>().text = cardPrice2.ToString();
				}
				break;
			case 3:
				{
					cardPrice3 = price;
					cardID3 = asset.CardID;
					Card3.GetComponentInChildren<CardManager>().Initialized(asset);
					Card3.transform.Find("Name").GetComponent<TextMeshProUGUI>().text = asset.CardName;
					Card3.transform.Find("Price").GetComponent<TextMeshProUGUI>().text = cardPrice3.ToString();
				}
				break;
			case 4:
				{
					cardPrice4 = price;
					cardID4 = asset.CardID;
					Card4.GetComponentInChildren<CardManager>().Initialized(asset);
					Card4.transform.Find("Name").GetComponent<TextMeshProUGUI>().text = asset.CardName;
					Card4.transform.Find("Price").GetComponent<TextMeshProUGUI>().text = cardPrice4.ToString();
				}
				break;
			default:
				//Debug.LogWarning("SetCard: 设定位置位置错误");
				break;
		}
	}
	public void SetGoods(int pos, GoodsSOAsset asset)
	{
		switch (pos)
		{
			case 1:
				goods1.GetComponent<GoodsManager>().Initialized(asset, shopType);
				break;
			case 2:
				goods2.GetComponent<GoodsManager>().Initialized(asset, shopType);
				break;
			case 3:
				goods3.GetComponent<GoodsManager>().Initialized(asset, shopType);
				break;
			default:
				Debug.LogWarning("SetCard: 设定位置位置错误");
				break;
		}
	}

	public void BuyGoods(int pos)
	{
		Debug.Log("Buy Goods" + pos.ToString());
		int price;
		Button goods;
		switch (pos)
		{
			case 1:
				price = goods1.GetComponent<GoodsManager>().asset.GoodsPrice;
				goods = goods1;
				break;
			case 2:
				price = goods2.GetComponent<GoodsManager>().asset.GoodsPrice;
				goods = goods2;
				break;
			case 3:
				price = goods3.GetComponent<GoodsManager>().asset.GoodsPrice;
				goods = goods3;
				break;
			default:
				price = 39;
				goods = null;
				Debug.LogWarning("商品位置错误");
				break;
		}
		if(goods != null && currentMoneys - price >= 0)
		{
			Debug.Log("购买成功");
			currentMoneys -= price;
			manager.SendMessage(goods.GetComponent<GoodsManager>().asset.GoodsEffectName, goods.GetComponent<GoodsManager>().asset.GoodsRnak);
			if(goods == goods1)
			{
				goods1Count--;
				if(shopType == ShopType.ShopInGame)
				{
					goods1.interactable = false;
				}
			}
			if(goods == goods2)
			{
				goods2Count--;
				if(shopType == ShopType.ShopInGame)
				{
					goods2.interactable = false;
				}
			}
			if(goods == goods3)
			{
				goods3Count--;
				if(shopType== ShopType.ShopInGame)
				{
					goods3.interactable = false;
				}
			}
		}
		else
		{
			Debug.Log("no money no talk");
		}
	}

	public void BuyCard(int pos)
	{
		Debug.Log("Buy Card" + pos.ToString());
		switch (pos)
		{
			case 1:
				if(currentMoneys - cardPrice1 >= 0)
				{
					currentMoneys -= cardPrice1;
					Player.Instance.AddCard(cardID1);
					Card1.enabled = false;
					Card1.interactable = false;
				}
				break;
			case 2:
				if(currentMoneys - cardPrice2 >= 0)
				{
					currentMoneys -= cardPrice2;
					Player.Instance.AddCard(cardID2);
					Card2.enabled = false;
					Card2.interactable = false;
				}
				break;
			case 3:
				if(currentMoneys - cardPrice3 >= 0)
				{
					currentMoneys -= cardPrice3;
					Player.Instance.AddCard(cardID3);
					Card3.enabled = false;
					Card3.interactable = false;
				}
				break;
			case 4: 
				if(currentMoneys - cardPrice4 >= 0)
				{
					currentMoneys -= cardPrice4;
					Player.Instance.AddCard(cardID4);
					Card4.enabled = false;
					Card4.interactable = false;
				}
				break;
			default:
				Debug.LogWarning("商品位置错误");
				break;
		}
	}

	void Load()
	{
		if (shopType == ShopType.Shop)
		{
			currentMoneys =  Player.Instance.Mithrils;
		}
		else if (shopType == ShopType.ShopInGame)
		{
			currentMoneys = Player.Instance.Tears;
		}
		else
		{
			currentMoneys = 39;
		}
	}

	void Save()
	{
		if (shopType == ShopType.Shop)
		{
			Player.Instance.SetMithrils(currentMoneys);
		}
		else if (shopType == ShopType.ShopInGame)
		{
			Player.Instance.SetTears(currentMoneys);
		}
		ArchiveManager.SavePlayerData(1);
	}
	

}


public class ShopSave
{
	int cardID1;
	int cardPrice1;

	int cardID2;
	int cardPrice2;

	int cardID3;
	int cardPrice3;

	int cardID4;
	int cardPrice4;

	int goods1ID;
	int goods1Count;

	int goods2ID;
	int goods2Count;

	int goods3ID;
	int goods3Count;
}


public enum ShopType
{
    Void,
    Shop,
    ShopInGame,
}
