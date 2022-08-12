using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EventBgUI : MonoBehaviour
{
    void Start()
    {
        //GetComponent<Image>().color = new Color((147f/255f), (147f / 255f), (147f / 255f), (155f / 255f));
    }

    public void SetColor()
    {
        GetComponent<Image>().color = new Color((147f / 255f), (147f / 255f), (147f / 255f), (155f / 255f));
    }
}
