using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Strip : MonoBehaviour
{
    private GameObject CardEntry;
    public List<GameObject> CardsInStripe;
    public int Plus = 0;
    public TextMeshProUGUI punctuation;
    public string Faction;
    public int Stripe;
    public GameObject PlayerGraveyard;
    public GameObject EnemyGraveyard;
    public GameObject PlayerArea;
    public GameObject EnemyArea;

    private int RoundChecker = 1;
    private int Round = 1;
    private int PartialPlus = 0;

    private void OnCollisionEnter2D(Collision2D collision) //when collision it sends the cards in the list of the strip
    {
        CardEntry = collision.gameObject;
        CardsInStripe.Add(CardEntry);
    }

    


    void Update()
    {
        Round = GameObject.Find("GameManager").GetComponent<GameManager>().Round;
        PlayerArea = GameObject.Find("PlayerArea");
        EnemyArea = GameObject.Find("EnemyArea");


        PartialPlus = 0;
        for(int i = 0; i < CardsInStripe.Count; i++) // Plus
        {
            PartialPlus += CardsInStripe[i].GetComponent<CardModel>().Power;
        }
        Plus = PartialPlus;
        punctuation.text = Plus.ToString();  //ends the Plus
        


        //when the round has finished it restarts everything and the cards go to their proper graveyard
        if(RoundChecker != Round)
        {
            RoundChecker = Round;
            if(Faction == "Cloud Of Fraternity")
            {
                foreach(GameObject Card in CardsInStripe)
                {
                    Card.transform.SetParent(PlayerGraveyard.transform, true);
                    Card.transform.position = PlayerGraveyard.transform.position;
                }
                CardsInStripe.Clear();
                Plus = 0;
                punctuation.text = Plus.ToString();
            }

            if(Faction == "Reign Of Punishment")
            {
                foreach(GameObject Card in CardsInStripe)
                {
                    Card.transform.SetParent(EnemyGraveyard.transform, true);
                    Card.transform.position = EnemyGraveyard.transform.position;
                }
                CardsInStripe.Clear();
                Plus = 0;
                punctuation.text = Plus.ToString();
            }
        }

    }
}