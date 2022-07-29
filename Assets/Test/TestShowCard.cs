using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class TestShowCard : MonoBehaviour, IPointerExitHandler, IPointerEnterHandler
{
    public float zoomSize;
    public void OnPointerEnter(PointerEventData eventData)//当鼠标进入UI后执行的事件执行的
    {
        transform.localScale = new Vector3(zoomSize, zoomSize, -10f);
    }
    public void OnPointerExit(PointerEventData eventData)//当鼠标离开UI后执行的事件执行的
    {
        transform.localScale = new Vector3(1f, 1f, 1f);
    }
}