using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;
using TMPro;

public class DisplayBoxUI : MonoBehaviour
{
    public TextMeshProUGUI explain;
    public TextMeshProUGUI value;
    void Start()
    {
        
    }
    public void InitSet(string _explain,int _v)
    {
        explain.text = _explain;
        value.text = _v.ToString();
    }
    
}
