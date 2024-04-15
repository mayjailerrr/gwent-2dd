using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CardGame : MonoBehaviour
{
    public GameObject hiddenPlayer;
    private bool player1Turn = true;
    public int playerCards;
    public GameObject PlayerArea;


    // Start is called before the first frame update
    void Start()
    {
        playerCards = GameObject.FindGameObjectWithTag("PlayerArea").GetComponent<Hand>().Cards;
    }




    // Update is called once per frame
    void Update()
    {
        

        if(player1Turn)

        {
            if(playerCards != 10)
            {
                {
                    //setactive arriba del nametag chatgpt, 

                    player1Turn = false;
                    
                }
                
            }
        }
    }
}
