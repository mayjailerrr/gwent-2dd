using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;


public class Turns : MonoBehaviour
{
    public int Round = 1;
    public bool Turn = true;
    public Hand PlayerArea;
    public Hand EnemyArea;
    public GameObject HiddenPlayer;
    public GameObject HiddenEnemy;
    public TextMeshProUGUI SurrenderedPlayerText;
    public TextMeshProUGUI SurrenderedEnemyText;
    public bool BranStarkOff;
    public bool TheKingOfTheNightOff;
    // private int BranStark = 1;
    // private int TheKingOfTheNight = 1;
    private int Hand1 = 0;
    private int Hand2 = 0;
    private int Equalizer1 = 0;
    private int Equalizer2 = 0;
    private RectTransform hiddenPlayer;
    private RectTransform hiddenEnemy;

    private bool ESteals;
    private bool ESteals2;
    private bool ESteals3;
    private bool PSteals;
    private bool PSteals2;
    private bool PSteals3;


    void Start()
    {
        hiddenPlayer = HiddenPlayer.GetComponent<RectTransform>();
        hiddenEnemy = HiddenEnemy.GetComponent<RectTransform>();
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







        if(PlayerArea.Surrendered) //when the player has surrendered it will be the oponent's turn
        {
            Turn = false;
            SurrenderedPlayerText.text = "Player has surrendered siu";
        }
        else
        {
            SurrenderedPlayerText.text = "";
        }

        if(EnemyArea.Surrendered)
        {
            Turn = true;
            SurrenderedEnemyText.text = "Enemy has surrendered siu";
        }
        else
        {
            SurrenderedEnemyText.text = "";
        }
        
        if(EnemyArea.Surrendered && PlayerArea.Surrendered) //when both has surrendered then we cant see the cards from any area
        {
            hiddenEnemy.sizeDelta = new Vector2(850, 56);
            hiddenPlayer.sizeDelta = new Vector2(850, 56);
        }


       if(Turn) //change of turn
       {
        hiddenEnemy.sizeDelta = new Vector2(0, 0);
        hiddenPlayer.sizeDelta = new Vector2(850, 56);
        if(Hand1 == 0 && Round == 1 && PSteals)
        {
            Turn = false;
        }
        if(Hand1 == 0 && Round == 2 && PSteals2)
        {
            Turn = false;
        }
        if(Hand1 == 0 && Round == 3 && PSteals3)
        {
            Turn = false;
        }

        if(Equalizer1 != Hand1)
        {
            Equalizer1 = Hand1;
            Turn = false;
        }
       }


       if(Turn == false) //change of turn
       {
        hiddenPlayer.sizeDelta = new Vector2(0, 0);
        hiddenEnemy.sizeDelta = new Vector2(850, 56);
        if(Hand2 == 0 && Round == 1 && ESteals)
        {
            Turn = true;
        }
        if(Hand2 == 0 && Round == 2 && ESteals2)
        {
            Turn = true;
        }
        if(Hand2 == 0 && Round == 3 && ESteals3)
        {
            Turn = true;
        }

        if(Equalizer2 != Hand2)
        {
            Equalizer2 = Hand2;
            Turn = true;
        }
       }
    }
}
