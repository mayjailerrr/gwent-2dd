using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Graveyard : MonoBehaviour
{
    private GameObject CardEntry;
    public List<GameObject> CardsInStripe;

    public GameObject PlayerGraveyard;
    public GameObject EnemyGraveyard;

    private int Round = 1;

    public int Hand1;
    public int Hand2;

    public GameObject PlayerArea;
    public GameObject EnemyArea;
    

    private void OnCollisionEnter2D(Collision2D collision)
    {
        CardEntry = collision.gameObject;  
        CardsInStripe.Add(CardEntry);  
    }


    void Update()
    {
        Round =  GameObject.Find("GameManager").GetComponent<GameManager>().Round;

        Hand1 = PlayerArea.GetComponent<Hand>().Cards;
        Hand2 = EnemyArea.GetComponent<Hand>().Cards;

    }

    public void Clean()
    {
        if(Hand1 == 0 && Hand2 == 0)
        {
            foreach(GameObject Card in CardsInStripe)
            {
                if(Card.GetComponent<CardModel>().Faction == "Cloud Of Fraternity")
                {
                    Card.transform.SetParent(PlayerGraveyard.transform, false);
                    Card.transform.position = PlayerGraveyard.transform.position;
                }

                CardsInStripe.Clear();
            }

            foreach(GameObject Card in CardsInStripe)
            {
                if(Card.GetComponent<CardModel>().Faction == "Reign Of Punishment")
                {
                    Card.transform.SetParent(EnemyGraveyard.transform, false);
                    Card.transform.position = EnemyGraveyard.transform.position;
                }

                CardsInStripe.Clear();
            }
        }
    }      
}
