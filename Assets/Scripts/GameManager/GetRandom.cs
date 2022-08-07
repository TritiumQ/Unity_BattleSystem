using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GetRandom : MonoBehaviour
{
    public int step;
    public int level;
    public int[] GameEventCount;
    public List<int> GameEvent;

    void Start()
    {
        GameEventCount = LevelEvent.GameEventCount;
        GameEvent = GetComponent<GameManager>().GameEvent;
    }

    // Update is called once per frame
    void Update()
    {
        step = GetComponent<GameManager>().step;
        level = GetComponent<GameManager>().level;
    }

    void GetEventRandom()
    {

    }

}
