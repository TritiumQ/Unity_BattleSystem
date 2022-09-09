using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Audiomanage : MonoBehaviour
{
    public static Audiomanage Instance;// = new Audiomanage();
    public static string musicPath = "Music/";
    private void Awake()
    {
        Instance = this;

    }
   
    public void PlayBGM(GameObject obj, string name, bool isloop = true)
    {
        AudioSource bgmSource;
        bgmSource = obj.AddComponent<AudioSource>();
        AudioClip clip = Resources.Load<AudioClip>(musicPath + name);
        bgmSource.clip = clip;//…Ë÷√“Ù∆µ£ª
        bgmSource.loop = isloop;
        bgmSource.Play();
    }
    public void PlayEffect(string name)
    {
        AudioClip clip = Resources.Load<AudioClip>(musicPath + name);
        AudioSource.PlayClipAtPoint(clip, transform.position);//≤•∑≈£ª
    }
}