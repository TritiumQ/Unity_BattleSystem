using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class SerializableGP
{
    public bool isContinue;
    public int level;
    public int step;
    public List<SeriablizableEvent> gameEvent;
    public SerializableGP(GameManager _gameManager, bool _isContinue)
    {
        level = _gameManager.level;
        step = _gameManager.step;
        gameEvent = new List<SeriablizableEvent>();
        foreach (var _eventUI in _gameManager.eventUnit)
        {
            SeriablizableEvent newEvent = new SeriablizableEvent(_eventUI);
            gameEvent.Add(newEvent);
        }
        isContinue = _isContinue;
    }
    public SerializableGP()
    {

    }
}