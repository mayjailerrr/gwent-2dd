using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bran : MonoBehaviour
{
    public GameObject card;

    public Strip cac;

    public bool used = false;
   
    public GameObject player1TurnIndicator;
    public GameObject player2TurnIndicator;
    public GameObject leader1;
    public GameObject leader2;

    private Hand zone;
    private Hand zone2;
    public GameObject PlayerArea;
    public GameObject EnemyArea;

     void Start()
    {
        zone = PlayerArea.GetComponent<Hand>();
        zone2 = EnemyArea.GetComponent<Hand>();
    }

    public void Attack()
    {
        if(used == false)
        {
            GameObject instance = Instantiate(card, new Vector2(0,0), Quaternion.identity);
            instance.transform.SetParent(cac.transform, false);
            instance.transform.position = cac.transform.position;
            
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

    void Update()
    {
        cac = GameObject.FindGameObjectWithTag("CACZone").GetComponent<Strip>();
    }
}
