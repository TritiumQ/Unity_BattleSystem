using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
public enum CardState
{
    Library,Deck,
}
public class ClickCard : MonoBehaviour,IPointerDownHandler
{
    private DeckManager DeckManager;
   // private PlayerData PlayerData£»
    public CardState state;
  
    void Start()
    {
        DeckManager = GameObject.Find("DeckManager").GetComponent<DeckManager>();
        //PlayerData = GameObject.Find("DataManager").GetComponent<PlayerData>();
    }
    public void OnPointerDown(PointerEventData pointerEventData)
    {
        int id = this.GetComponent<CardDisplay>().Asset.CardID;
        DeckManager.UpdataCard(state, id);
    }
}
