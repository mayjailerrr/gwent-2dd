using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class Change : MonoBehaviour
{
    public Draw deck;
    public Draw deck2;

    public Hand Hand1;
    public Hand Hand2;

    public int stop;

    void Update()
    {
        deck = GameObject.FindGameObjectWithTag("Deck").GetComponent<Draw>();
        deck2 = GameObject.FindGameObjectWithTag("Deck2").GetComponent<Draw>();

        Hand1 = GameObject.FindGameObjectWithTag("PlayerArea").GetComponent<Hand>();
        Hand2 = GameObject.FindGameObjectWithTag("EnemyArea").GetComponent<Hand>();

        stop = GameObject.Find("TurnManager").GetComponent<TurnManager>().controller;
    }

     public void Swapping()
    {
        if(Hand1.siu < 2 && stop < 2)
        {
            if(Input.GetMouseButtonUp(1))
            {
                gameObject.GetComponent<MoveCard>().useful = true;
                deck.Steal();
                Destroy(gameObject);
                Hand1.siu += 1;
            }
        }
    }

    public void Swapping2()
    {
        if(Hand2.siu < 2 && stop < 2)
        {
            if(Input.GetMouseButtonUp(1))
            {
                gameObject.GetComponent<MoveCard>().useful = true;
                deck2.Steal();
                Destroy(gameObject);
                Hand2.siu += 1;
            }
        }
    }
    
}
