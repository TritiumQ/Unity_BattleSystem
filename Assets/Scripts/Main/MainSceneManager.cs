using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class MainSceneManager : MonoBehaviour
{
    public GameObject ShopButton;
    public GameObject PackageButton;
    public GameObject DeckButton;
    public GameObject StartButton;
    public GameObject ContinueButton;
    public Player player;
    
    private void Start()
    {
        //ArchiveManager.ResetPlayerDataFile();
        
        player = Player.Instance;
        ArchiveManager.LoadPlayerData(1);
        if (GameProcessSave.ReadSave())
        {
            ShopButton.GetComponent<Button>().interactable = false;
            PackageButton.GetComponent<Button>().interactable = false;
            DeckButton.GetComponent<Button>().interactable = false;

            StartButton.SetActive(false);
            ContinueButton.SetActive(true);
        }
        
    }

}
