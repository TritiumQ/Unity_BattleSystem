using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicManager : MonoBehaviour
{
    private static MusicManager instance=null;
    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(this);//ÇÐ»»³¡¾°²»Ïú»Ù
            return;
        }
        Destroy(this.gameObject);
    }
}
