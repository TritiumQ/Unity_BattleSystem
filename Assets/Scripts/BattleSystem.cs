using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class BattleSystem : MonoBehaviour
{
    int round;
	Button endButton;


	List<int> deck;  //牌堆
	List<int> hands;  //手牌
	List<int> usedCards;  //弃牌堆


	public GameObject surventPrefab;
	public GameObject cardPrefab;  

	public GameObject playerHands; //玩家手牌区域
	public GameObject enemyArea;  //敌方随从区域
	public GameObject surventArea;  //玩家随从区域

	public GameObject playerBody;
	public GameObject bossBody;

	private void Awake()
	{
		endButton = GameObject.Find("EndButton").GetComponent<Button>();
		endButton.onClick.AddListener(EndRound);
		
	}
	void EndRound() // 结束当前回合
	{
		Debug.Log("Click EndRound");

	}

}
