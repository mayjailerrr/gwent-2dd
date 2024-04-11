using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CardGame : MonoBehaviour
{
    public GameObject player1;
    private bool player1Turn = true;
    public int playerCards;

    private GameObject CardEntry;
    public List<GameObject> CardsInStripe;
    public int Cards = 0;



    // Start is called before the first frame update
    void Start()
    {
        playerCards = GameObject.FindGameObjectWithTag("PlayerArea").GetComponent<Hand>().Cards;
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



    // Update is called once per frame
    void Update()
    {
        

        if(player1Turn)

        {
            if(Input.GetButton("Deck1"))
            {
                {
                    for(int i = 0; i < CardsInStripe.Count; i++)
                    {
                        player1.GetComponent<UseCard>().PlayCard();
                    }
                    player1Turn = false;
                    
                }
                
            }
        }
    }
}
