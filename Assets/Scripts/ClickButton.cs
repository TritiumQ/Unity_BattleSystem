using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class ClickButton : MonoBehaviour
{
    public void CloseSelectCard()
    {
        PlayerDataTF.EventContinue();
        SceneManager.LoadScene("GameProcess");
            
    }
}
