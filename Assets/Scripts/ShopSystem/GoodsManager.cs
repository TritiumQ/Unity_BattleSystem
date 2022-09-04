using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class GoodsManager : MonoBehaviour
{
    public ShopType GoodsType;
    public GoodsSOAsset asset { get; private set; }
	public TextMeshProUGUI NameText;
	public TextMeshProUGUI DescriptionText;
	public TextMeshProUGUI PriceText;
	public Image GoodsImage;
	int GoodsCount;
	public TextMeshProUGUI CountText;
	private void Update()
	{
		Refresh();
	}
	public void Initialized(GoodsSOAsset _asset)
	{
		asset = _asset;
	}
	public void Refresh()
	{
		if(asset != null)
		{
			NameText.text = asset.GoodsName;
			DescriptionText.text = asset.GoodsDescription;
			PriceText.text = asset.GoodsPrice.ToString();
			if(GoodsType == ShopType.Shop)
			{
				CountText.text = GoodsCount.ToString();
			}
			GoodsImage.sprite = asset.GoodsImage;
		}
	}
}
