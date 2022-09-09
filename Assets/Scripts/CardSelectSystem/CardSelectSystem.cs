using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using UnityEngine.SceneManagement;


public class CardSelectSystem : MonoBehaviour
{ 
    public Button ExitButton;
    public Button Card1;
    int id1;
    public Button Card2;
    int id2;
    public Button Card3;
    int id3;

	public Image Mask;

    string NextSceneName;

    private void Awake()
    { 
        StartCoroutine(EnterScene());
        ExitButton.onClick.AddListener(Exit);
        Initialized("GameProcess", GetRandom.GetRandomCard(), GetRandom.GetRandomCard(), GetRandom.GetRandomCard());
        //Initialized(0, 0, 0);
    }

    public void Initialized(string _nextSceneName, int cardID1, int cardID2, int cardID3)
	{
        Debug.Log("Init Card Select");
        NextSceneName = _nextSceneName;
        id1 = cardID1;
        id2 = cardID2;
        id3 = cardID3;
        if(Card1 != null)
		{
            Card1.GetComponentInChildren<CardManager>().Initialized(ArchiveManager.LoadCardAsset(cardID1));
		}
        if(Card2 != null)
		{
            Card2.GetComponentInChildren<CardManager>().Initialized(ArchiveManager.LoadCardAsset(cardID2));
        }
        if( Card3 != null)
		{
            Card3.GetComponentInChildren<CardManager>().Initialized(ArchiveManager.LoadCardAsset(cardID3));
        }
	}

    public void SelectCard(int _pos)
	{
        switch (_pos)
		{
            case 1:
                Player.Instance.AddCard(id1);
                break;
            case 2:
                Player.Instance.AddCard(id2);
                break;
            case 3:
                Player.Instance.AddCard(id3);
                break;
            default:
                //Debug.LogError("错误的位置ID");
                break;
		}
        Exit();
	}

    void Exit()
	{
        Debug.Log("Exit");
        StartCoroutine(_exit());
	}

    IEnumerator _exit()
	{
        //Debug.Log("Exit fade");
        yield return StartCoroutine(QuitScene());
        SceneManager.LoadScene(NextSceneName);
    }

    #region 淡入淡出
    public float alpha = 2;

    IEnumerator EnterScene()
    {
        float alpha_tmp = alpha;
        while (alpha_tmp > 0)
        {
            alpha_tmp -= Time.deltaTime;
            Mask.color = new Color(0, 0, 0, alpha_tmp);
            yield return null;
        }
    }

    public IEnumerator QuitScene()
    {
        float alpha_tmp = 0;
        while (alpha_tmp < alpha)
        {
            alpha_tmp += Time.deltaTime;
            Mask.color = new Color(0, 0, 0, alpha_tmp);
            yield return null;
        }
        PlayerDataTF.EventContinue();
        SceneManager.LoadScene("GameProcess");
    }
    #endregion
}
