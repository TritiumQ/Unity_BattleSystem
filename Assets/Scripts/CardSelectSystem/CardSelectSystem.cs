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
    public Button Card2;
    public Button Card3;

	public Image Mask;

    List<int> CurrentCardSet;

	private void Awake()
	{
        StartCoroutine(EnterScene());
        ExitButton.StartCoroutine(Exit());

        //Initialized(0, 0, 0);
	}

	public void Initialized(int cardID1, int cardID2, int cardID3)
	{
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

    IEnumerator Exit()
	{
        yield return StartCoroutine(QuitScene());
	}

    #region µ­Èëµ­³ö
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
    }
    #endregion
}
