using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class Change : MonoBehaviour
{
    public Draw deck;
    public Draw deck2;

    public bool use1;
    public bool use2;

    public int Hand1 = 0;
    public int Hand2 = 0;

    void Update()
    {
        use1 = GameObject.Find("Swap1").GetComponent<Swap1>().player1;
        use2 = GameObject.Find("Swap2").GetComponent<Swap1>().player2;

        deck = GameObject.FindGameObjectWithTag("Deck").GetComponent<Draw>();
        deck2 = GameObject.FindGameObjectWithTag("Deck2").GetComponent<Draw>();
    }

     public void Swapping()
    {
        if(use1 == false && Hand1 < 2)
        {
            if(Input.GetMouseButtonUp(1))
            {
                gameObject.GetComponent<MoveCard>().useful = true;
                deck.Steal();
                Destroy(gameObject);
                Hand1 += 1;
            }
        }
    }

    public void Swapping2()
    {
        if(use2 == false && Hand2 < 2)
        {
            if(Input.GetMouseButtonUp(1))
            {
                gameObject.GetComponent<MoveCard>().useful = true;
                deck2.Steal();
                Destroy(gameObject);
                Hand2 += 1;
            }
        }
    }
    
}
