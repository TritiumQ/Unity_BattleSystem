using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;


public class CardMessage : MonoBehaviour, IPointerExitHandler, IPointerEnterHandler
{
    [Header("设置层级")]
    public int layerMaskIndex;
    GameObject hintCard;
    [Header("需要显示的信息")]
    public string hintMessage;
    public void OnPointerEnter(PointerEventData eventData)//当鼠标进入UI后执行的事件执行的
    {
        if (gameObject.layer == layerMaskIndex)
        {
            Transform canvas = GameObject.Find("HintCanvas").transform;
            hintCard = Instantiate(gameObject, canvas);

        }
    }
    public void OnPointerExit(PointerEventData eventData)//当鼠标离开UI后执行的事件执行的
    {
        if (hintCard != null)
        {
            Destroy(hintCard);
        }
    }
}
