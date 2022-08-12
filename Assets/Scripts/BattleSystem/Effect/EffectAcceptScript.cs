using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class EffectAcceptScript : MonoBehaviour, IPointerClickHandler
{
    public GameObject thisObject;
    public void OnPointerClick(PointerEventData eventData)
    {
        BattleSystem sys = GameObject.Find("BattleSystem").GetComponent<BattleSystem>();
        if (sys != null && thisObject != sys.attacker)
        {
            sys.AttackConfirm(thisObject);
        }
    }
}
