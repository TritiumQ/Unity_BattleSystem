using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class UnitDisplay : MonoBehaviour, IPointerEnterHandler,IPointerExitHandler
{
    BattleSystem system;
	private void Awake()
	{
        system = GameObject.Find("BattleSystem").GetComponent<BattleSystem>();
    }
	public void OnPointerEnter(PointerEventData eventData)
    {
        
        if (system != null && gameObject != system.EffectInitiator && system.TargetSelectArrow != null)
        {
            transform.localScale = Vector3.one * 1.3f;
        }
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        if (system != null && gameObject != system.EffectInitiator && system.TargetSelectArrow != null)
        {
            transform.localScale = Vector3.one;
        }
    }
}
