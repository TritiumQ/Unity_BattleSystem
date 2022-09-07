using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
public class Producer : MonoBehaviour
{
    public Image Mask;
    public Button ExitButton;
    // Start is called before the first frame update
    void Awake()
    {
         
        StartCoroutine(EnterScene());
        ExitButton.onClick.AddListener(Exit);
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
        SceneManager.LoadScene("Main");
    }
    // Update is called once per frame
    void Update()
    {
        
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
        SceneManager.LoadScene("Main");
    }
    #endregion
}
