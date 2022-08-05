using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using DG.Tweening;

public class AttackRequest : MonoBehaviour, IPointerClickHandler
{
    public GameObject thisObject;
    BattleSystem sys;
    //public BattleManager BattleManager;
    // Start is called before the first frame update
    void Start()
    {
        sys = GameObject.Find("BattleSystem").GetComponent<BattleSystem>();
        
    }

    // Update is called once per frame
    void Update()
    {

    }
    public void OnPointerClick(PointerEventData eventData)
    {
        if(eventData.clickCount == 2)
		{
            bool attackable = thisObject.GetComponent<SurventUnitManager>().isActive;
            if (attackable && sys != null)
            {
                if (sys.attacker == null)
                {
                    sys.AttackRequest(thisObject, CardType.Survent, TargetOptions.SingleEnemyCreature, CardActionType.Attack, transform.position);
                }
                else
                {
                    sys.AttackOver();
                }
            }
        }
    }
}