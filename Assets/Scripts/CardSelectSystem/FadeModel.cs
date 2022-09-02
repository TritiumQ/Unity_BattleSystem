using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class FadeModel : MonoBehaviour
{
    static FadeModel instance;
    public static FadeModel Instance
	{
        get { return instance; }
	}

    [SerializeField, Header("下一个场景的名字")]
    private string nextSceneName= "GameProcess";
    private void Awake()
    {
        instance = this;
    }
    void Start()
    {
        Image fadeImage = GameObject.Find("Image").GetComponent<Image>();
        StartCoroutine(FadeInScene(fadeImage));
    }
    public void fadeToScene()
    {
        string SceneName = "GameProcess";
        Image fadeImage = GameObject.Find("Image").GetComponent<Image>();
        nextSceneName = SceneName;
        if(nextSceneName!="")
        {
            StartCoroutine(FadeoutScene(nextSceneName));
        }
    }
    IEnumerator FadeInScene(Image fadeImage)
    {
        float alpha = 1;
        while(alpha>0)
        {
            alpha -= Time.deltaTime;
            fadeImage.color = new Color(0, 0, 0, alpha);
            yield return null;
        }
    }
    
    public IEnumerator FadeoutScene(string NextSceneName)
    {
        Image fadeImage = GameObject.Find("Image").GetComponent<Image>();
        float alpha = 0;
        GameObject obj1 = GameObject.Find("GameObject");
        Destroy(obj1, 0.01f);
       /* Destroy(obj1,0.001f);
        yield return null;
        GameObject obj2 = GameObject.Find("Card (1)");
        Destroy(obj1, 0.001f);
        GameObject obj3 = GameObject.Find("Card (2)");
        Destroy(obj1, 0.001f);*/
        while (alpha<1)
        {
            alpha += Time.deltaTime;
            fadeImage.color = new Color(0, 0, 0, alpha);
            yield return null;
        }
        SceneManager.LoadScene(nextSceneName);
    }

}