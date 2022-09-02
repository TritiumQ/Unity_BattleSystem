using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SingletonScripts<T> : MonoBehaviour where T : Component
{
    public static T _instance;
    public static T Instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = GameObject.FindObjectOfType(typeof(T)) as T;
            }
            if (_instance == null)
            {
                GameObject newobj = new GameObject();
                _instance = newobj.AddComponent<T>();
            }
            return _instance;
        }
    }

}