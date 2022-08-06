using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine;

public class ReturnMainScene : MonoBehaviour
{
    void Start()
    {
        GameObject btnObj = GameObject.Find("Return");
        Button btn = btnObj.GetComponent<Button>();
        btn.onClick.AddListener(delegate ()
        {
            this.Turn(btnObj);
        });
    }
    public void Turn(GameObject NScene)
    {
        Debug.Log("Ìø×ª");
        SceneManager.LoadScene("Ö÷³¡¾°");
    }
    
}
