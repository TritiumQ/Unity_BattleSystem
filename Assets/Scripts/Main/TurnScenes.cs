using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;

public class TurnScenes : MonoBehaviour
{
    public void StartGame()
    {
        GameObject _obj=GameObject.Find("GameProcess");
        if (Player.Instance != null)
            ArchiveManager.SavePlayerData(1);
        if(_obj==null)
            SceneManager.LoadScene("Deck");
        else SceneManager.LoadScene("GameProcess");
        //SceneManager.LoadScene("Gameporess");
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
        obj.GetComponent<GameManager>().Gameover(0);
    }
    public void ReturnMain()
    {
        if (Player.Instance != null)
        {
            ArchiveManager.SavePlayerData(1);
        }
        SceneManager.LoadScene("Main");
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
