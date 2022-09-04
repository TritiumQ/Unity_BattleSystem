using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class HintEvent : MonoBehaviour,IPointerExitHandler, IPointerEnterHandler
{
    [Header("设置层级")]
    public int layerMaskIndex;
    GameObject hint;
    [Header("需要显示的信息")]
    public string hintMessage;
    [Header("偏移计算的目标（默认自身）")]
    public GameObject offsetObj;
    public void OnPointerEnter(PointerEventData eventData)//当鼠标进入UI后执行的事件执行的
    {
        if(offsetObj==null||offsetObj.GetComponent<RectTransform>()==null)
        {
            offsetObj = eventData.pointerEnter.gameObject;
        }
        if(gameObject.layer==layerMaskIndex)
        {
            Transform canvas = GameObject.Find("HintCanvas").transform;
            hint = Instantiate(Resources.Load<GameObject>("Prefabs/Hint"),canvas);
            float offset = offsetObj.GetComponent<RectTransform>().sizeDelta.y / 2;
            hint.GetComponentInChildren<UnityEngine.UI.Text>().text = hintMessage;
            hint.transform.position = eventData.position+Vector2.up*offset;
        }
    }
    public void OnPointerExit(PointerEventData eventData)//当鼠标离开UI后执行的事件执行的
    {
        if(hint!=null)
        {
            Destroy(hint);
        }
    }
}
