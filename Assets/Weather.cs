using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Weather : MonoBehaviour
{
    private GameObject CardEntry;
    public List<GameObject> CardsInStripe;

    public GameObject PlayerGraveyard;
    public GameObject EnemyGraveyard;

    private int Round = 1;
    private int RoundChecker = 1;

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

        if(RoundChecker != Round)
        {
            GameObject player = GameObject.Find("PlayerGraveyard");
            GameObject enemy = GameObject.Find("EnemyGraveyard");

            foreach(GameObject Card in CardsInStripe)
            {
                if(Card.GetComponent<CardModel>().Faction == "Cloud Of Fraternity")
                {
                    Card.transform.SetParent(player.transform, false);
                    Card.transform.position = player.transform.position;
                }
            }

            foreach(GameObject Card in CardsInStripe)
            {
                if(Card.GetComponent<CardModel>().Faction == "Reign Of Punishment")
                {
                    Card.transform.SetParent(enemy.transform, false);
                    Card.transform.position = enemy.transform.position;
                }
            }
            CardsInStripe.Clear();
            RoundChecker = Round;
        }
    }      
}
