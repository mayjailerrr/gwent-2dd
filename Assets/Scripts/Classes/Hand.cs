using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hand : MonoBehaviour
{
    private GameObject CardEntry;
    public List<GameObject> CardsInStripe;
    public int Cards = 0;
    public int siu;
    public int siu2;

    public bool Surrendered = false;

    public GameObject PlayerGraveyard;
    public GameObject EnemyGraveyard;

    private int RoundChecker = 1;
    private int Round = 1;

    void Start()
    {
        siu = 0;
        siu2 = 0;
    }


    private void OnCollisionEnter2D(Collision2D collision) //when collision it gets them on the list of the Stripe
    {
        CardEntry = collision.gameObject;
        CardsInStripe.Add(CardEntry);
        Cards += 1;
    }

    private void OnCollisionExit2D(Collision2D collision) // it get them out of the list
    {
        CardsInStripe.RemoveAt(0);
        Cards -= 1;
    }

    void Update()
    {
        Round = GameObject.Find("GameManager").GetComponent<GameManager>().Round;

        if(RoundChecker != Round)
        {
            GameObject player = GameObject.Find("PlayerGraveyard");
            GameObject enemy = GameObject.Find("EnemyGraveyard");

            foreach(GameObject Card in CardsInStripe)
            {
                if(Card.GetComponent<CardModel>().Faction == "Cloud Of Fraternity")
                {
                    Card.transform.SetParent(player.transform, true);
                    Card.transform.position = player.transform.position;
                }
                else if(Card.GetComponent<CardModel>().Faction == "Reign Of Punishment")
                {
                    Card.transform.SetParent(enemy.transform, true);
                    Card.transform.position = enemy.transform.position;
                }
            }
            
            CardsInStripe.Clear();
            RoundChecker = Round;
        }
    }
}
