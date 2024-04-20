using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class King : MonoBehaviour
{
    public GameObject card;

    public Strip ecac;

    public bool used = false;
    public int Round = 1;
    public int Hand2;

    public GameObject player1TurnIndicator;
    public GameObject player2TurnIndicator;

    public GameObject leader1;
    public GameObject leader2;

    private Hand zone;
    private Hand zone2;

    public GameObject PlayerArea;
    public GameObject EnemyArea;

    private bool ESteals;
    private bool ESteals2;
    private bool ESteals3;

     void Start()
    {
        ecac = GameObject.FindGameObjectWithTag("ECACZone").GetComponent<Strip>();
    }

    void Update()
    {
        Round = GameObject.Find("GameManager").GetComponent<GameManager>().Round;

        ESteals = GameObject.Find("Deck2").GetComponent<Draw>().Stole;
        ESteals2 = GameObject.Find("Deck2").GetComponent<Draw>().Stole2;
        ESteals3 = GameObject.Find("Deck2").GetComponent<Draw>().Stole3;

        zone = PlayerArea.GetComponent<Hand>();
        zone2 = EnemyArea.GetComponent<Hand>();

        Hand2 = EnemyArea.GetComponent<Hand>().Cards;
    }

    public void Attack()
    {
        if(used == false && Round == 1 && ESteals && Hand2 != 10 )
        {
            GameObject instance = Instantiate(card, new Vector2(0,0), Quaternion.identity);

            instance.transform.SetParent(ecac.transform, false);
            instance.transform.position = ecac.transform.position;

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
        else if(used == false && Round == 2 && ESteals2 && Hand2 != 2 )
        {
            GameObject instance = Instantiate(card, new Vector2(0,0), Quaternion.identity);
            
            instance.transform.SetParent(ecac.transform, false);
            instance.transform.position = ecac.transform.position;

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
        else if(used == false && Round == 3 && ESteals3 && Hand2 != 2 )
        {
            GameObject instance = Instantiate(card, new Vector2(0,0), Quaternion.identity);
            
            instance.transform.SetParent(ecac.transform, false);
            instance.transform.position = ecac.transform.position;

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
