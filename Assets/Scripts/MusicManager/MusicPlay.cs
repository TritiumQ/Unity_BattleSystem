using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicPlay : MonoBehaviour
{
    public string music;
    void Start()
    {
        Audiomanage.Instance.PlayBGM(this.gameObject,music);
    }

    
}
