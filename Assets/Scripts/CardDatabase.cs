using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CardDatabase : MonoBehaviour
{
    public static List<Card> cardList = new List<Card> ();

    void Awake()
    {
        cardList.Add(new Card(0, "None", 0, 0, 0, "None"));
        cardList.Add(new Card(1, "Elf", 2, 0, 0, "Elf"));
        cardList.Add(new Card(2, "Dwarf", 3, 0, 0, "Dwarf"));
        cardList.Add(new Card(3, "Human", 5, 0, 0, "Human"));
        cardList.Add(new Card(4, "Demon", 1, 0, 0, "Demon"));
    }
}
