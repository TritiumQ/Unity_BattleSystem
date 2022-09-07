using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Head : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        Debug.Log("Start");
        //SceneManager.LoadScene("Main");
        //ArchiveManager.ResetPlayerDataFile("starter");
        //ArchiveManager.LoadPlayerData();
        SceneManager.LoadScene("Main");

    }


}
