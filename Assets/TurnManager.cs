using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;


public class TurnManager : MonoBehaviour
{
    public int Round = 1;
    public bool Turn = true;
    public Hand PlayerArea;
    public Hand EnemyArea;
    
    public TextMeshProUGUI SurrenderedPlayerText;
    public TextMeshProUGUI SurrenderedEnemyText;
    // public bool BranStarkOff;
    // public bool TheKingOfTheNightOff;
    // private int BranStark = 1;
    // private int TheKingOfTheNight = 1;
    private int Hand1 = 0;
    private int Hand2 = 0;
    // private int Equalizer1 = 0;
    // private int Equalizer2 = 0;
    public GameObject player1TurnIndicator;
    public GameObject player2TurnIndicator;

    private bool ESteals;
    private bool ESteals2;
    private bool ESteals3;
    private bool PSteals;
    private bool PSteals2;
    private bool PSteals3;


    void Start()
    {
        player1TurnIndicator.SetActive(false);
        player2TurnIndicator.SetActive(true); 
    }

    public void SwitchTurns()
    {
        //toggle the active state of the turn indicators
        player1TurnIndicator.SetActive(!player1TurnIndicator.activeSelf);
        player2TurnIndicator.SetActive(!player1TurnIndicator.activeSelf);
    }


    void Update()
    {
        // BranStarkOff = GameObject.Find("BranStark").GetComponent<BranStarkAbility>().Utilizada;
        // TheKingOfTheNightOff = GameObject.Find("TheKingOfTheNight").GetComponent<TheKingOfTheNightAbility>().Usada;

        PlayerArea = GameObject.FindGameObjectWithTag("PlayerArea").GetComponent<Hand>();
        EnemyArea = GameObject.FindGameObjectWithTag("EnemyArea").GetComponent<Hand>();

        ESteals = GameObject.Find("Deck2").GetComponent<Draw>().Stole;
        PSteals = GameObject.Find("Deck1").GetComponent<Draw>().Stole;
        ESteals2 = GameObject.Find("Deck2").GetComponent<Draw>().Stole2;
        PSteals2 = GameObject.Find("Deck1").GetComponent<Draw>().Stole2;
        ESteals3 = GameObject.Find("Deck2").GetComponent<Draw>().Stole3;
        PSteals3 = GameObject.Find("Deck1").GetComponent<Draw>().Stole3;

        Round = GameObject.Find("GameManager").GetComponent<GameManager>().Round;
        Hand1 = GameObject.Find("PlayerArea").GetComponent<Hand>().Cards;
        Hand2 = GameObject.Find("EnemyArea").GetComponent<Hand>().Cards;
    }

           
        // if(BranStarkOff && BranStark == 1) // changing the turn when the ability of the leader is off (already used)
        // {
        //     BranStark += 1;
        //     Turn = false;
        // }
        // if(TheKingOfTheNightOff && TheKingOfTheNight == 1)
        // {
        //     TheKingOfTheNight += 1;
        //     Turn = true;
        // }







//         if(PlayerArea.Surrendered) //when the player has surrendered it will be the oponent's turn
//         {
//             Turn = false;
//             SurrenderedPlayerText.text = "Player has surrendered siu";
//         }
//         else
//         {
//             SurrenderedPlayerText.text = "";
//         }

//         if(EnemyArea.Surrendered)
//         {
//             Turn = true;
//             SurrenderedEnemyText.text = "Enemy has surrendered siu";
//         }
//         else
//         {
//             SurrenderedEnemyText.text = "";
//         }
        
//         if(EnemyArea.Surrendered && PlayerArea.Surrendered) //when both has surrendered then we cant see the cards from any area
//         {
//             hiddenEnemy.sizeDelta = new Vector2(850, 56);
//             hiddenPlayer.sizeDelta = new Vector2(850, 56);
//         }


//         // if(Hand1 == 0 && Round == 1 && PSteals)
//         // {
//         //     Turn = true;
//         // }
//         // if(Hand1 == 0 && Round == 2 && PSteals2)
//         // {
//         //     Turn = true;
//         // }
//         // if(Hand1 == 0 && Round == 3 && PSteals3)
//         // {
//         //     Turn = true;
//         // }

//     //     if(Equalizer1 != Hand1)
//     //     {
//     //         Equalizer1 = Hand1;
//     //         Turn = false;
//     //     }
//     //    }


//        
//         // if(Hand2 == 0 && Round == 1 && ESteals)
//         // {
//         //     Turn = true;
//         // }
//         // if(Hand2 == 0 && Round == 2 && ESteals2)
//         // {
//         //     Turn = true;
//         // }
//         // if(Hand2 == 0 && Round == 3 && ESteals3)
//         // {
//         //     Turn = true;
//         // }

//         // if(Equalizer2 != Hand2)
//         // {
//         //     Equalizer2 = Hand2;
//         //     Turn = true;
//         // }
//     //    }
//     }
}
