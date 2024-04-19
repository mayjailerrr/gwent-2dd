using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bran : MonoBehaviour
{
    public Strip cac;

    public bool used = false;

    public Draw deck;
    public int Hand1;
    public int Round = 1;
   
    public GameObject player1TurnIndicator;
    public GameObject player2TurnIndicator;
    public GameObject leader1;
    public GameObject leader2;

    private Hand zone;
    private Hand zone2;
    public GameObject PlayerArea;
    public GameObject EnemyArea;

    private bool PSteals;
    private bool PSteals2;
    private bool PSteals3;

     void Update()
    {
        zone = PlayerArea.GetComponent<Hand>();
        zone2 = EnemyArea.GetComponent<Hand>();
        Hand1 = PlayerArea.GetComponent<Hand>().Cards;
        deck = GameObject.FindGameObjectWithTag("Deck").GetComponent<Draw>();

        PSteals = GameObject.Find("Deck1").GetComponent<Draw>().Stole;
        PSteals2 = GameObject.Find("Deck1").GetComponent<Draw>().Stole2;
        PSteals3 = GameObject.Find("Deck1").GetComponent<Draw>().Stole3;

        Round = GameObject.Find("GameManager").GetComponent<GameManager>().Round;
    }

    public void Attack()
    {
        if(used == false && PSteals && Hand1 != 10 && Round == 1)
        {
            deck.Steal();
            
            if(zone.Surrendered == false && zone2.Surrendered == false)
            {
                 //toggle the active state of the turn indicators
                player1TurnIndicator.SetActive(!player1TurnIndicator.activeSelf);
                player2TurnIndicator.SetActive(!player1TurnIndicator.activeSelf);

                leader1.SetActive(!leader1.activeSelf);
                leader2.SetActive(!leader2.activeSelf);
            }

            used = true;
        }
        else if(used == false && PSteals2 && Hand1 != 2 && Round == 2)
        {
            deck.Steal();
            
            if(zone.Surrendered == false && zone2.Surrendered == false)
            {
                 //toggle the active state of the turn indicators
                player1TurnIndicator.SetActive(!player1TurnIndicator.activeSelf);
                player2TurnIndicator.SetActive(!player1TurnIndicator.activeSelf);

                leader1.SetActive(!leader1.activeSelf);
                leader2.SetActive(!leader2.activeSelf);
            }
            used = true;
        }
        else if(used == false && PSteals3 && Hand1 != 2 && Round == 3)
        {
            deck.Steal();
            
            if(zone.Surrendered == false && zone2.Surrendered == false)
            {
                 //toggle the active state of the turn indicators
                player1TurnIndicator.SetActive(!player1TurnIndicator.activeSelf);
                player2TurnIndicator.SetActive(!player1TurnIndicator.activeSelf);

                leader1.SetActive(!leader1.activeSelf);
                leader2.SetActive(!leader2.activeSelf);
            }
            used = true;
        }
    }
}
