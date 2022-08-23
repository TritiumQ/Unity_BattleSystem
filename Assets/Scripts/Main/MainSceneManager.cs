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
    private void Awake()
    {
        Player player = Player.Instance;
        ArchiveManager.LoadPlayerData(1);
    }
    private void Start()
    {
        GameObject _obj = GameObject.Find("GameProcess");
        if(_obj!=null)
        {
            ShopButton.GetComponent<Button>().interactable = false;
            PackageButton.GetComponent<Button>().interactable = false;
            DeckButton.GetComponent<Button>().interactable = false;

            StartButton.SetActive(false);
            ContinueButton.SetActive(true);
        }
        
    }

}
