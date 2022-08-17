using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;

public class TurnScenes : MonoBehaviour
{
    public void TurnScene(string scene)
    {
        if (Player.Instance != null)
        {
            ArchiveManager.SavePlayerData(1);
        }
        SceneManager.LoadScene(scene);
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
