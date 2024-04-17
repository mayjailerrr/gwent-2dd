using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hand : MonoBehaviour
{
    private GameObject CardEntry;
    public List<GameObject> CardsInStripe;
    public int Cards = 0;

    public bool Surrendered = false;


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
}
