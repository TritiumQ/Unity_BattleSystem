using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;
using UnityEngine.UI;

public class TurnScenes : MonoBehaviour
{
    public void StartGame()
    {
        if (Player.Instance != null)
            ArchiveManager.SavePlayerData(1);
        SceneManager.LoadScene("CardHub");
    }
    
    public void ContinueGame()
    {
        //if (Player.Instance != null)
            ArchiveManager.SavePlayerData(1);
        SceneManager.LoadScene("GameProcess");
    }

    public void TurnScene(string scene)
    {
        if (Player.Instance != null)
        {
            ArchiveManager.SavePlayerData(1);
        }
        SceneManager.LoadScene(scene);
    }

    public void OverGame()
    {
        GameObject obj = GameObject.Find("GameManager");
        if (obj!=null)
        {
            obj.GetComponent<GameManager>().Gameover(0);
        }
    }
    public void ReturnMain()
    {
        if (Player.Instance != null)
        {
            ArchiveManager.SavePlayerData(1);
        }
        SceneManager.LoadScene("Main");
    }
    public void ReturnGameProcess()
    {
        GameObject _obj = GameObject.Find("GameProcess");
        if(_obj!=null)
        {
            ArchiveManager.SavePlayerData(1);
            PlayerDataTF.EventContinue();
            SceneManager.LoadScene("GameProcess");
        }
    }
    

    public void QuitGame()
    {
        if (Player.Instance != null)
        {
            ArchiveManager.SavePlayerData(1);
        }
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#else
        Application.Quit();
#endif
    }

}
