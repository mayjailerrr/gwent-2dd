using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;


public class GiveUp : MonoBehaviour
{
    public TextMeshProUGUI PSurrendered;
    public TextMeshProUGUI ESurrendered;

    public GameObject PlayerArea;
    public GameObject player1TurnIndicator;
    public GameObject leader1;

    public GameObject EnemyArea;
    public GameObject player2TurnIndicator;
    public GameObject leader2;

    public GameObject give1;
    public GameObject give2;

    private Hand zone;
    private Hand zone2;
    // private bool use = false;

    void Start()
    {
        zone = PlayerArea.GetComponent<Hand>();
        zone2 = EnemyArea.GetComponent<Hand>();
    }


    public void  Surrender()
    {
            if(zone != null)
            {
                zone.Surrendered = !zone.Surrendered;
            }

            //when the player has Surrendered it will be the opponent's turn
            if(zone.Surrendered == true) 
            {
                //toggle the active state of the turn indicators
                player1TurnIndicator.SetActive(!player1TurnIndicator.activeSelf);
                leader1.SetActive(!leader1.activeSelf);

                player2TurnIndicator.SetActive(!player2TurnIndicator.activeSelf);
                leader2.SetActive(!leader2.activeSelf);

                PSurrendered.gameObject.SetActive(true);

                give1.gameObject.SetActive(!give1.activeSelf);
                give2.gameObject.SetActive(!give2.activeSelf);
            }

    }

    // public void both()
    // {
    //     if(zone.Surrendered == true && zone2.Surrendered == true)
    //         {
    //             player1TurnIndicator.SetActive(true);
    //             player2TurnIndicator.SetActive(true); 

    //             leader1.SetActive(false);
    //             leader2.SetActive(false);

    //             PSurrendered.gameObject.SetActive(true);
    //             ESurrendered.gameObject.SetActive(true);

    //             give1.gameObject.SetActive(false);
    //             give2.gameObject.SetActive(false);
    //         }
    // }

}
