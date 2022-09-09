using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Head : MonoBehaviour
{
    // Start is called before the first frame update
    //public Button ContinueButton;
    public Image Mask;
    void Awake()
    {
        //Debug.Log("Start");
        //SceneManager.LoadScene("Main");
        //ArchiveManager.ResetPlayerDataFile("starter");
        //ArchiveManager.LoadPlayerData();
        StartCoroutine(EnterScene());
        //ContinueButton.onClick.AddListener(Continue);
        Invoke("Continue", 3f);
        
    }
    void Continue()
	{
        StartCoroutine(QuitScene());
    }

    #region ���뵭��
    public float alpha = 5;

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
        SceneManager.LoadScene("Main");
    }
    #endregion
}
