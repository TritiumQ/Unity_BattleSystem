using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
public class SafeHouse : MonoBehaviour
{

    private Transform canvasTf;
    // Start is called before the first frame update
    void Awake()
    {
        canvasTf = this.transform;
    }

    // Update is called once per frame
    void Update()
    {

    }
    //选择秘银；
    public void selectTear()
    {
        //Player.Instance.Tears += 20;
        Player.Instance.AddMoney(0, 12);

        this.transform.Find("GetTear").GetComponent<Button>().enabled = false;
        ShowTip("您获得了12点泪滴", Color.red);
        StartCoroutine(Test1());
       

    }
    //选择卡牌
    public void selectCard()
    {
        //卡库随机卡牌添加到卡组中去；
        for (int i = 0; i < 1; i++)
        {
            int RandomNewCard = GetRandom.GetRandomCard();//这个要结合之后卡库卡牌信息来改正；
            Player.Instance.AddCard(RandomNewCard);
        }
        this.transform.Find("GetCard").GetComponent<Button>().enabled = false;

        ShowTip("您获得了随机1张卡牌", Color.red);
        StartCoroutine(Test1());
       
      

    }
    //选择恢复；
    public void selectCur()
    {
        Player.Instance.AddCurrentHp(5);

        this.transform.Find("Heal").GetComponent<Button>().enabled = false;
        ShowTip("您获得了5点血量恢复", Color.red);
        StartCoroutine(Test1());
       
    }
    //携程两秒之后摧毁物体，1.5秒后开换场景；
    private IEnumerator Test1()
    {
        
        yield return new WaitForSeconds(1.5f);
        PlayerDataTF.EventContinue();
        SceneManager.LoadScene("GameProcess");
    }

    public void ShowTip(string msg, Color color, System.Action callback = null)
    {
        GameObject obj = Instantiate(Resources.Load("prefabs/Tips"), canvasTf) as GameObject;
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