using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;


public class TurnManager : MonoBehaviour
{
    public int Round = 1;

    public bool player;
    public bool enemy;
    
    public TextMeshProUGUI PSurrendered;
    public TextMeshProUGUI ESurrendered;

    private int Hand1 = 0;
    private int Hand2 = 0;

    public GameObject player1TurnIndicator;
    public GameObject player2TurnIndicator;

    public GameObject leader1;
    public GameObject leader2;

    public GameObject give1;
    public GameObject give2;

    public GameObject swap1;
    public GameObject swap2;


    private Hand zone;
    private Hand zone2;

    public GameObject PlayerArea;
    public GameObject EnemyArea;


    public void SwitchTurns()
    {
        if(zone.Surrendered == false && zone2.Surrendered == false)
        {
            //toggle the active state of the turn indicators
            player1TurnIndicator.SetActive(!player1TurnIndicator.activeSelf);
            player2TurnIndicator.SetActive(!player1TurnIndicator.activeSelf);

            leader1.SetActive(!leader1.activeSelf);
            leader2.SetActive(!leader2.activeSelf);

            give1.gameObject.SetActive(!give1.activeSelf);
            give2.gameObject.SetActive(!give2.activeSelf);
        }
    }


    void Start()
    {
        //the game is started by the first player
        player1TurnIndicator.SetActive(false);
        player2TurnIndicator.SetActive(true); 

        leader1.SetActive(true);
        leader2.SetActive(false);

        PSurrendered.gameObject.SetActive(false);
        ESurrendered.gameObject.SetActive(false);

        give1.gameObject.SetActive(true);
        give2.gameObject.SetActive(false);


        player = GameObject.FindGameObjectWithTag("PlayerArea").GetComponent<Hand>().Surrendered;
        enemy = GameObject.FindGameObjectWithTag("EnemyArea").GetComponent<Hand>().Surrendered;

        Round = GameObject.Find("GameManager").GetComponent<GameManager>().Round;

        Hand1 = GameObject.Find("PlayerArea").GetComponent<Hand>().Cards;
        Hand2 = GameObject.Find("EnemyArea").GetComponent<Hand>().Cards;

        zone = PlayerArea.GetComponent<Hand>();
        zone2 = EnemyArea.GetComponent<Hand>();
    }
    
}

