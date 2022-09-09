using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using DG.Tweening;
//测试用，有bug
public class CardUpShow : MonoBehaviour, IPointerExitHandler, IPointerEnterHandler
{
    public static bool EnablePreview = true;

    [SerializeField]
    private Vector3 savePos;
    [SerializeField]
	[Header("上升高度")]
    private float upmove;
    [SerializeField]
    private int saveOrder;

    private Canvas cv;

    public void OnPointerEnter(PointerEventData eventData)
    {
        SaveCardSate();
        //Debug.Log("开始预览");
        if (EnablePreview)
        {
            StartPreView();
        }
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        //Debug.Log("退出预览");
        if (EnablePreview)
        {
            EndPreView();
        }
    }
    private void StartPreView()
    {
        transform.DOLocalMoveY(upmove, 0.1f);
        transform.localScale = new Vector3(1.5f, 1.5f, 1.5f);
        cv.sortingOrder += 100;
        GetComponent<CardManager>().SetPreview();
    }

    private void EndPreView()
    {
        transform.DOLocalMoveY(0.0f, 0.1f);
        transform.DOMove(savePos, 0.1f);
        transform.localScale = Vector3.one;
        cv.sortingOrder = saveOrder;
        GetComponent<CardManager>().EndPreview();
    }
    private void SaveCardSate()
    {
        savePos = transform.position;
        saveOrder = cv.sortingOrder;
        
    }
    void Start()
    {
        if (GetComponent<Canvas>() != null)
        {
            //Debug.Log("Canvas OK!");
            cv = GetComponent<Canvas>();
        }
        else
        {
            Debug.Log("没有添加Canvas");
        }
    }
}
