using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class AttackConfirm : MonoBehaviour, IPointerClickHandler
{
    public GameObject thisObject;
    public void OnPointerClick(PointerEventData eventData)
    {
        BattleSystem sys = GameObject.Find("BattleSystem").GetComponent<BattleSystem>();
        if (sys != null)
        {
            sys.SurventAttackConfirm(thisObject);
        }
    }
}
