using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LoadTool : MonoBehaviour
{
    private static LoadTool instance = null;
    private void Awake()
    {
        if(instance==null)
        {
            instance = this;
            DontDestroyOnLoad(this);
            return;
        }
        Destroy(this);
    }

    private void Update()
    {
        CardSelectSystem obj = GameObject.Find("CardSelectSystem").GetComponent<CardSelectSystem>();
        if (obj != null)
        {
            obj.Initialized("GameProcess", GetRandom.GetRandomCard(), GetRandom.GetRandomCard(), GetRandom.GetRandomCard());
            instance = null;
            Destroy(this);
        }
    }
}
