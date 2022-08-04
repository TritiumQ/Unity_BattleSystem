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
    public void InitSet(string _explain,int _step)
    {
        explain.text = _explain;
        string _value = string.Format("{ 0:0,0}",_step * 2);
        value.text = _value;
    }
    
}
