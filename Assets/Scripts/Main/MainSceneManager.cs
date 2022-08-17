using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainSceneManager : MonoBehaviour
{
    private void Awake()
    {
        Player player = Player.Instance;
        ArchiveManager.LoadPlayerData(1);
    }

}
