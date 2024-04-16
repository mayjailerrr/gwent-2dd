using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Draw : MonoBehaviour
{
    public GameObject PlayerArea;
    public GameObject Card11;
    public GameObject Card11C1;
    public GameObject Card12;
    public GameObject Card12C1;
    public GameObject Card12C2;
    public GameObject Card13;
    public GameObject Card21;
    public GameObject Card21C1;
    public GameObject Card22;
    public GameObject Card22C1;
    public GameObject Card22C2;
    public GameObject Card23;
    public GameObject Card31;
    public GameObject Card31C1;
    public GameObject Card32;
    public GameObject Card32C1;
    public GameObject Card32C2;
    public GameObject Card33;
    public GameObject Card41;
    public GameObject Card42;
    public GameObject Card43;
    public GameObject Card51;
    public GameObject Card52;
    public GameObject Card53;
    public GameObject Card54;
    public GameObject Card55;
    public GameObject Card56;
    public GameObject Card61;
    public GameObject Card62;
    public GameObject Card63;

    public bool Stole = false;
    public bool Stole2 = false;
    public bool Stole3 = false;

    public bool useful;

    private int Round = 1;

    private List<GameObject> remainingCards;

    List <GameObject> cards = new List<GameObject>();


    // Start is called before the first frame update
    void Start()
    {
       cards.Add(Card11); 
       cards.Add(Card11C1); 
       cards.Add(Card12);
       cards.Add(Card12C1);
       cards.Add(Card12C2);
       cards.Add(Card13);
       cards.Add(Card21);
       cards.Add(Card21C1);
       cards.Add(Card22);
       cards.Add(Card22C1);
       cards.Add(Card22C2);
       cards.Add(Card23);
       cards.Add(Card31C1);
       cards.Add(Card32);
       cards.Add(Card32C1);
       cards.Add(Card32C2);
       cards.Add(Card33);
       cards.Add(Card41); 
       cards.Add(Card42);
       cards.Add(Card43);
       cards.Add(Card51);
       cards.Add(Card52);
       cards.Add(Card53);
       cards.Add(Card54);
       cards.Add(Card55);
       cards.Add(Card56);
       cards.Add(Card61);
       cards.Add(Card62);
       cards.Add(Card63);

        remainingCards = new List<GameObject>(cards);

    }


    public void DealCards()
    {
        //first round
        if(Stole == false && Round == 1)
        {
            //shuffle remaining cards
            ShuffleCards();

            //instantiate 10 cards from remainingCards
            for (int i = 0; i < 17; i++)
            {
                if(remainingCards.Count > 0)
                {
                    GameObject Card = remainingCards[0];
                    remainingCards.RemoveAt(0); //remove the card from remainingCards
                    GameObject cardInstance = Instantiate(Card, PlayerArea.transform);
                }
            }
            Stole = true;
        }

        //second round
        if (Stole2 == false && Round == 2)
        {
             //shuffle remaining cards
            ShuffleCards();

            //instantiate 10 cards from remainingCards
            for (int i = 0; i < 2; i++)
            {
                if(remainingCards.Count > 0)
                {
                    GameObject Card = remainingCards[0];
                    remainingCards.RemoveAt(0); //remove the card from remainingCards
                    GameObject cardInstance = Instantiate(Card, PlayerArea.transform);
                }
            }
            Stole2 = true;
        }

         //third round
        if (Stole3 == true && Round == 3)
        {
              //shuffle remaining cards
            ShuffleCards();

            //instantiate 10 cards from remainingCards
            for (int i = 0; i < 3; i++)
            {
                if(remainingCards.Count > 0)
                {
                    GameObject Card = remainingCards[0];
                    remainingCards.RemoveAt(0); //remove the card from remainingCards
                    GameObject cardInstance = Instantiate(Card, PlayerArea.transform);
                }
            }
            Stole3 = false;
        }
       
    }

    public void ShuffleCards()
    {
       
        //fisher-yates shuffle algorithm
        for(int i = remainingCards.Count - 1; i > 0; i--)
        {
            int j = Random.Range(0, i + 1);
            GameObject temp = remainingCards[i];
            remainingCards[i] = remainingCards[j];
            remainingCards[j] = temp;
        }
    }

    //5th: steal a card
    public void Gigants()
    {
        if(useful)
        {
            ShuffleCards();

            //instantiate 1 card from remainingCards
            for (int i = 0; i < 1; i++)
            {
                if(remainingCards.Count > 0)
                {
                    GameObject Card = remainingCards[0];
                    remainingCards.RemoveAt(0); //remove the card from remainingCards
                    GameObject cardInstance = Instantiate(Card, PlayerArea.transform);
                }
            }
        }
    }

    void Update()
    {
        Round = GameObject.Find("GameManager").GetComponent<GameManager>().Round;

        useful = gameObject.GetComponent<MoveCard>().useful;
    }

}
