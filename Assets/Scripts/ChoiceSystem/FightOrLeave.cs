using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class FightOrLeave : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void Leave()
    {
        PlayerDataTF.EventContinue();
        SceneManager.LoadScene("GameProcess");
    }
    public void Fight()
    {
        PlayerDataTF.EventContinue();
        SceneManager.LoadScene("GameProcess");
    }
}
