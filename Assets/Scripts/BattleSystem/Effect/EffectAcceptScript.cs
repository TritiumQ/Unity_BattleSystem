using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class EffectAcceptScript : MonoBehaviour, IPointerClickHandler
{
    public void OnPointerClick(PointerEventData eventData)
    {
        BattleSystem sys = GameObject.Find("BattleSystem").GetComponent<BattleSystem>();
        if (sys != null && sys.EffectInitiator != null && sys.Effect != null)
        {
            sys.EffectConfirm(gameObject);
        }
    }
}
