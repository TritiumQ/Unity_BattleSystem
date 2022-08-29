using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class EffectAcceptScript : MonoBehaviour, IPointerClickHandler
{
    public void OnPointerClick(PointerEventData eventData)
    {
        BattleSystem sys = GameObject.Find("BattleSystem").GetComponent<BattleSystem>();
        if (sys != null && sys.EffectInitiator != null && sys.effectPack != null && gameObject != sys.EffectInitiator)
        {
            sys.EffectConfirm(gameObject);
        }
        else
		{
            Debug.Log("Dame");
            //sys.EffectSetupOver();
		}
    }
}
