using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gigants : MonoBehaviour
{
    public Draw deck;
    public bool useful;

    void Update()
    {
        useful = gameObject.GetComponent<MoveCard>().useful;

        GameObject deck = GameObject.FindGameObjectWithTag("Deck");
    }

    public void Attack()
    {
        if(useful && deck != null)
        {
            deck.Gigants();
        }
    }
}
