using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameProcess : MonoBehaviour
{
    private static GameProcess instance=null;

    void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(this);//ÇÐ»»³¡¾°²»Ïú»Ù
            return;
        }
        Destroy(this.gameObject);
    }

    void Update()
    {
        
    }
}
