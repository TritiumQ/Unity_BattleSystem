using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using DG;
using DG.Tweening;
using TMPro;
public class SafeHouse : MonoBehaviour
{

    private  Transform canvasTf;
    // Start is called before the first frame update
    void Awake()
    {
        canvasTf = this.transform;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void selectLeidi()
    {
        ShowTip("您获得了20泪滴", Color.red);
        Destroy(this.gameObject,2);
        LoadCanvas.Instance.CloseAndOpen("SafeHouse/Tips");

    }
    public void selectCard()
    {
        ShowTip("您获得了2张卡牌", Color.red);
        Destroy(this.gameObject, 2);
        LoadCanvas.Instance.CloseAndOpen("SafeHouse/Tips");
    }
    public void selectCur()
    {
        ShowTip("您获得了2点血量回复",Color.red);
        Destroy(this.gameObject, 2);
        LoadCanvas.Instance.CloseAndOpen("SafeHouse/Tips");
    }
    public void ShowTip(string msg, Color color, System.Action callback = null)
    {
        GameObject obj = Instantiate(Resources.Load("SafeHouse/Tips"), canvasTf) as GameObject;
          Text text = obj.transform.Find("bg/Text").GetComponent<Text>();
        text.color = color;
        text.text = msg;
        Tween scale1 = obj.transform.Find("bg").DOScale(2, 0.4f);
        Tween scale2 = obj.transform.Find("bg").DOScale(0, 0.4f);
        Sequence seq = DOTween.Sequence();
        seq.Append(scale1);
        seq.AppendInterval(0.5f);
        seq.Append(scale2);
        seq.AppendCallback(delegate ()
        {
            if (callback != null)
            {
                callback();
            }
        });
        MonoBehaviour.Destroy(obj, 2);
    }
}