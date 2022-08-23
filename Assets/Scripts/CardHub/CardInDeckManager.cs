using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using TMPro;
using DG.Tweening;

public class CardInDeckManager : MonoBehaviour,IPointerEnterHandler,IPointerExitHandler
{
	CardSOAsset cardAsset;
	[Header("UI组件")]
	public TextMeshProUGUI cardCostText;
	public TextMeshProUGUI cardNameText;
	public TextMeshProUGUI cardCountText;

	public int currentCount;

	public void Initialized(CardSOAsset _card)
	{
		cardAsset = _card;
	}
	private void Start()
	{
		//cardAsset = Resources.Load<CardSOAsset>(Const.CARD_DATA_PATH(1));
	}
	void Refresh()
	{
		if(cardAsset != null)
		{
			cardCostText.text = cardAsset.Cost.ToString();
			cardNameText.text = cardAsset.CardName;
			if (currentCount == 2)
			{
				cardCountText.text = currentCount.ToString();
			}
			else
			{
				cardCountText.enabled = false;
			}
		}
	}
	private void Update()
	{
		
		Refresh();
	}

	//预览效果
	public GameObject previewPrefab;
	GameObject preview;
	void StartPreview()
	{
		Vector3 Pos = transform.position;
		Pos.x += 340;
		preview = Instantiate(previewPrefab, GameObject.Find("CardHub").transform);
		preview.transform.position = Pos;
		if(preview != null)
		{
			preview.GetComponent<CardDisplay>().Initialized(cardAsset);
		}
	}
	void EndPreview()
	{
		Destroy(preview);
	}
	public void OnPointerEnter(PointerEventData eventData)
	{
		Debug.Log("Preview");
		Invoke("StartPreview", 0.8f);
	}
	public void OnPointerExit(PointerEventData eventData)
	{
		CancelInvoke();
		EndPreview();
	}
}
