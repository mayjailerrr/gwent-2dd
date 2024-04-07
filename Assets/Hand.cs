using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hand : MonoBehaviour
{
    private GameObject CardEntry;
    public List<GameObject> CardsInStrip;
    public int Cards = 0;
    public bool Surrendered = false;

    private void OnCollisionEnter2D(Collision2D collision) //when collision it gets them on the list of the strip
    {
        CardEntry = collision.gameObject;
        CardsInStrip.Add(CardEntry);
        Cards += 1;
    }

    private void OnCollisionExit2D(Collision2D collision) // it get them out of the list
    {
        CardsInStrip.RemoveAt(0);
        Cards -= 1;
    }
}
