using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;


public class MoveCard : MonoBehaviour
{
    public Draw deck;
    private int Hand1;
    private int Round = 1;
    public GameObject WarZone1;
    public GameObject WarZone2;
    public GameObject WarZone3;

    public GameObject WarZone4;
    public GameObject WarZone5;
    public GameObject WarZone6;

    public GameObject EWarZone1;
    public GameObject EWarZone2;
    public GameObject EWarZone3;

    public GameObject EWarZone4;
    public GameObject EWarZone5;
    public GameObject EWarZone6;

    public GameObject Card;
    public GameObject turnManager;
    public bool useful = true;
    public bool used = false;

    void Start()
    {
        WarZone1 = GameObject.Find("CACWZone");
        WarZone2 = GameObject.Find("DistanceWZone");
        WarZone3 = GameObject.Find("SiegeWZone");

        WarZone4 = GameObject.Find("CACZone");
        WarZone5 = GameObject.Find("DistanceZone");
        WarZone6 = GameObject.Find("SiegeZone");

        EWarZone1 = GameObject.Find("ECACWZone");
        EWarZone2 = GameObject.Find("EDistanceWZone");
        EWarZone3 = GameObject.Find("ESiegeWZone");

        EWarZone4 = GameObject.Find("ECACZone");
        EWarZone5 = GameObject.Find("EDistanceZone");
        EWarZone6 = GameObject.Find("ESiegeZone");

        turnManager = GameObject.Find("TurnManager");
        deck = GameObject.FindGameObjectWithTag("Deck").GetComponent<Draw>();
      
    }

    void Update()
    {
        Round = GameObject.Find("GameManager").GetComponent<GameManager>().Round;
        Hand1 = GameObject.Find("PlayerArea").GetComponent<Hand>().Cards;
    }


    public void CACZone()
    {
        if(useful && turnManager != null)
        {
            Card.transform.SetParent(WarZone4.transform, false);
            Card.transform.position = WarZone4.transform.position;
            useful = false;
            turnManager.GetComponent<TurnManager>().SwitchTurns();
        }
    }

    public void DistanceZone()
    {
        if(useful && turnManager != null)
        {
            Card.transform.SetParent(WarZone5.transform, false);
            Card.transform.position = WarZone5.transform.position;
            useful = false;
            turnManager.GetComponent<TurnManager>().SwitchTurns();
        }
    }
        

    public void SiegeZone()
    {
        if(useful && turnManager != null)
        {
            Card.transform.SetParent(WarZone6.transform, false);
            Card.transform.position = WarZone6.transform.position;
            useful = false;
            turnManager.GetComponent<TurnManager>().SwitchTurns();
        }
    }


    public void ECACZone()
    {
        if(useful && turnManager != null)
        {
            Card.transform.SetParent(EWarZone4.transform, false);
            Card.transform.position = EWarZone4.transform.position;
            useful = false;
            turnManager.GetComponent<TurnManager>().SwitchTurns();
        }
    }

    public void EDistanceZone()
    {
        if(useful && turnManager != null)
        {
            Card.transform.SetParent(EWarZone5.transform, false);
            Card.transform.position = EWarZone5.transform.position;
            useful = false;
            turnManager.GetComponent<TurnManager>().SwitchTurns();
        }
    }
        

    public void ESiegeZone()
    {
        if(useful && turnManager != null)
        {
            Card.transform.SetParent(EWarZone6.transform, false);
            Card.transform.position = EWarZone6.transform.position;
            useful = false;
            turnManager.GetComponent<TurnManager>().SwitchTurns();
        }
    }




    public void CACWZone()
    {
        if(useful && turnManager != null)
        {
            Card.transform.SetParent(WarZone1.transform, false);
            Card.transform.position = WarZone1.transform.position;
            useful = false;
            turnManager.GetComponent<TurnManager>().SwitchTurns();
        }
    }

    public void DistanceWZone()
    {
        if(useful && turnManager != null)
        {
            Card.transform.SetParent(WarZone2.transform, false);
            Card.transform.position = WarZone2.transform.position;
            useful = false;
            turnManager.GetComponent<TurnManager>().SwitchTurns();
        }
    }

    public void SiegeWZone()
    {
        if(useful && turnManager != null)
        {
            Card.transform.SetParent(WarZone3.transform, false);
            Card.transform.position = WarZone3.transform.position;
            useful = false;
            turnManager.GetComponent<TurnManager>().SwitchTurns();
        }
    }



    public void ECACWZone()
    {
        if(useful && turnManager != null)
        {
            Card.transform.SetParent(EWarZone1.transform, false);
            Card.transform.position = EWarZone1.transform.position;
            useful = false;
            turnManager.GetComponent<TurnManager>().SwitchTurns();
        }
    }

    public void EDistanceWZone()
    {
        if(useful && turnManager != null)
        {
            Card.transform.SetParent(EWarZone2.transform, false);
            Card.transform.position = EWarZone2.transform.position;
            useful = false;
            turnManager.GetComponent<TurnManager>().SwitchTurns();
        }
    }

    public void ESiegeWZone()
    {
        if(useful && turnManager != null)
        {
            Card.transform.SetParent(EWarZone3.transform, false);
            Card.transform.position = EWarZone3.transform.position;
            useful = false;
            turnManager.GetComponent<TurnManager>().SwitchTurns();
        }
    }

    public void Deal()
    {
        if(used == false)
        {
            if(Round == 1 && Hand1 != 10)
            {
                deck.Steal();
            }
            else if(Round == 2 && Hand1 != 2)
            {
                deck.Steal();
            }
            else if(Round == 3 && Hand1 != 2)
            {
                deck.Steal();
            }
            used = true;
        }   
    }
   
}
