using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class ZoomUI : MonoBehaviour, IPointerExitHandler, IPointerEnterHandler
{
    
    public void OnPointerEnter(PointerEventData pointerEventData)
    {
        transform.localScale = new Vector3(1.2f,1.2f, 1.0f);
    }
    public void OnPointerExit(PointerEventData eventData)
    {
        transform.localScale = new Vector3(1f, 1f, 1f);
    }

}
