using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class InEffect : MonoBehaviour
{
    private float startColor;
    private float finalColor;
    private float currentColor;
    private int count;
    private int cnt;
    private float step;

    void Awake()
    {
        startColor = 255f;
        finalColor = 0.0f;
        currentColor = startColor;
        count = 1;
        cnt = 0;
        step = 5.0f;
    }

    void FixedUpdate()
    {
        cnt++;
        if (cnt >= count)
        {
            cnt = 0;
            currentColor -= step;
            GetComponent<Image>().color = new Color((0f / 255f), (0f / 255f), (0f / 255f), (currentColor / 255f));
            if (currentColor == finalColor)
            {
                Destroy(GameObject.Find("InEffect"));
            }
        }
    }
}

