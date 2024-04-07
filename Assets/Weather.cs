using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Weather : MonoBehaviour
{
    private GameObject CardEntry;
    public List<GameObject> CardsInStripe;
    public string Faction;
    public GameObject PlayerGraveyard;
    public GameObject EnemyGraveyard;
    private int RoundChecker = 1;
    private int Round = 1;

    private void OnCollisionEnter2D(Collision2D collision) //when collision it gets the cards in the list of the stripe
    {
        CardEntry = collision.gameObject;   //designs the new card to whatever she collides
        CardsInStripe.Add(CardEntry);    // it gets the new cards in the list
    }


    void Update()
    {
            Round =  GameObject.Find("GameManager").GetComponent<GameManager>().Round;

        if(RoundChecker != Round)
        {
            RoundChecker = Round;
            if(Faction == "Cloud Of Fraternity")
            {
                foreach(GameObject Card in CardsInStripe)
                {
                    Card.transform.SetParent(PlayerGraveyard.transform, false);
                    Card.transform.position = PlayerGraveyard.transform.position;
                }
                CardsInStripe.Clear();
            }

            if(Faction == "Reign Of Punishment")
            {
                foreach(GameObject Card in CardsInStripe)
                {
                    Card.transform.SetParent(EnemyGraveyard.transform, false);
                    Card.transform.position = EnemyGraveyard.transform.position;
                }
                CardsInStripe.Clear();
            }
        }
    }
}
