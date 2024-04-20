using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class GameManager : MonoBehaviour
{
    public int Round = 1;

    public TextMeshProUGUI WinnerIsPlayer;
    public TextMeshProUGUI WinnerIsEnemy;

    public string PlayerPoints;
    public string EnemyPoints;

    public TextMeshProUGUI WinnerText;

    private bool GameOver = false;

    private int PlayerVictories = 0;
    private int EnemyVictories = 0;

    private bool ESteals;
    private bool ESteals2;
    private bool ESteals3;

    private bool PSteals;
    private bool PSteals2;
    private bool PSteals3;

    private Hand zone;
    private Hand zone2;

    private int Hand1;
    private int Hand2;

    public GameObject PlayerArea;
    public GameObject EnemyArea;

    public GameObject player1TurnIndicator;
    public GameObject player2TurnIndicator;

    public GameObject leader1;
    public GameObject leader2;

    public GameObject give1;
    public GameObject give2;

    public TextMeshProUGUI PSurrendered;
    public TextMeshProUGUI ESurrendered;


    void Update()
    {
        Hand1 = PlayerArea.GetComponent<Hand>().Cards;
        Hand2 = EnemyArea.GetComponent<Hand>().Cards;

        zone = PlayerArea.GetComponent<Hand>();
        zone2 = EnemyArea.GetComponent<Hand>();


        PlayerPoints = GameObject.Find("PlayerCnt").GetComponent<TextMeshProUGUI>().text;
        EnemyPoints = GameObject.Find("EnemyCnt").GetComponent<TextMeshProUGUI>().text;


        ESteals = GameObject.Find("Deck2").GetComponent<Draw>().Stole;
        PSteals = GameObject.Find("Deck1").GetComponent<Draw>().Stole;
        ESteals2 = GameObject.Find("Deck2").GetComponent<Draw>().Stole2;
        PSteals2 = GameObject.Find("Deck1").GetComponent<Draw>().Stole2;
        ESteals3 = GameObject.Find("Deck2").GetComponent<Draw>().Stole3;
        PSteals3 = GameObject.Find("Deck1").GetComponent<Draw>().Stole3;


        //deciding the winner
        if(GameOver == false && PlayerVictories == 2 && EnemyVictories == 0)
        {
            GameOver = true;
            WinnerText.text = "Player1 is the winner siu";

            player1TurnIndicator.SetActive(true);
            player2TurnIndicator.SetActive(true); 

            leader1.SetActive(false);
            leader2.SetActive(false);

            give1.gameObject.SetActive(false);
            give2.gameObject.SetActive(false);

            zone.Surrendered = false;
            zone2.Surrendered = false;

            PSurrendered.gameObject.SetActive(false);
            ESurrendered.gameObject.SetActive(false);
        }
        else if(GameOver == false && EnemyVictories == 2 && PlayerVictories == 0)
        {
            GameOver = true;
            WinnerText.text = "Player2 is the winner siu";

            player1TurnIndicator.SetActive(true);
            player2TurnIndicator.SetActive(true); 

            leader1.SetActive(false);
            leader2.SetActive(false);

            give1.gameObject.SetActive(false);
            give2.gameObject.SetActive(false);

            zone.Surrendered = false;
            zone2.Surrendered = false;

            PSurrendered.gameObject.SetActive(false);
            ESurrendered.gameObject.SetActive(false);
        }
        else if(GameOver == false && Round == 0 && PlayerVictories == EnemyVictories)
        {
            GameOver = true;
            WinnerText.text = "Tie";

            player1TurnIndicator.SetActive(true);
            player2TurnIndicator.SetActive(true); 

            leader1.SetActive(false);
            leader2.SetActive(false);

            give1.gameObject.SetActive(false);
            give2.gameObject.SetActive(false);

            zone.Surrendered = false;
            zone2.Surrendered = false;

            PSurrendered.gameObject.SetActive(false);
            ESurrendered.gameObject.SetActive(false);
        }
        else if(GameOver == false && Round == 0 && EnemyVictories > PlayerVictories)
        {
            GameOver = true;
            WinnerText.text = "Player2 is the winner siu";
        }
        else if(GameOver == false && Round == 0 && EnemyVictories < PlayerVictories)
        {
            GameOver = true;
            WinnerText.text = "Player1 is the winner siu";
        }
        
        
    

        //first Round
    // public void WhoWon()
    // {               
        if(Round == 1 && Hand1 == 0 && Hand2 == 0 && PSteals && ESteals) //1st round finished normally
        {
            int PlayerPts = int.Parse(PlayerPoints);
            int EnemyPts = int.Parse(EnemyPoints);

            if(PlayerPts > EnemyPts)
            {
                PlayerVictories += 1;
                WinnerIsPlayer.text = PlayerVictories.ToString();

                player1TurnIndicator.SetActive(false);
                player2TurnIndicator.SetActive(true); 

                leader1.SetActive(true);
                leader2.SetActive(false);

                give1.gameObject.SetActive(true);
                give2.gameObject.SetActive(false);
            }

            if(EnemyPts > PlayerPts)
            {
                EnemyVictories += 1;
                WinnerIsEnemy.text = EnemyVictories.ToString();

                player1TurnIndicator.SetActive(true);
                player2TurnIndicator.SetActive(false); 

                leader1.SetActive(false);
                leader2.SetActive(true);

                give1.gameObject.SetActive(false);
                give2.gameObject.SetActive(true);
            }

            if(PlayerPts == EnemyPts)
            {
                PlayerVictories += 1;
                WinnerIsPlayer.text = PlayerVictories.ToString();
                EnemyVictories += 1;
                WinnerIsEnemy.text = EnemyVictories.ToString();

                player1TurnIndicator.SetActive(false);
                player2TurnIndicator.SetActive(true); 

                leader1.SetActive(true);
                leader2.SetActive(false);

                give1.gameObject.SetActive(true);
                give2.gameObject.SetActive(false);
            }

            Round += 1;
        }
        if(Round == 1 && zone.Surrendered && zone2.Surrendered && PSteals && ESteals) //both surrendered in a middle of a round
        {
            int PlayerPts = int.Parse(PlayerPoints);
            int EnemyPts = int.Parse(EnemyPoints);

            if(PlayerPts > EnemyPts)
            {
                PlayerVictories += 1;
                WinnerIsPlayer.text = PlayerVictories.ToString();

                player1TurnIndicator.SetActive(false);
                player2TurnIndicator.SetActive(true); 

                leader1.SetActive(true);
                leader2.SetActive(false);

                give1.gameObject.SetActive(true);
                give2.gameObject.SetActive(false);
            }

            if(EnemyPts > PlayerPts)
            {
                EnemyVictories += 1;
                WinnerIsEnemy.text = EnemyVictories.ToString();

                player1TurnIndicator.SetActive(true);
                player2TurnIndicator.SetActive(false); 

                leader1.SetActive(false);
                leader2.SetActive(true);

                give1.gameObject.SetActive(false);
                give2.gameObject.SetActive(true);
            }

            if(PlayerPts == EnemyPts)
            {
                PlayerVictories += 1;
                WinnerIsPlayer.text = PlayerVictories.ToString();
                EnemyVictories += 1;
                WinnerIsEnemy.text = EnemyVictories.ToString();

                player1TurnIndicator.SetActive(false);
                player2TurnIndicator.SetActive(true); 

                leader1.SetActive(true);
                leader2.SetActive(false);

                give1.gameObject.SetActive(true);
                give2.gameObject.SetActive(false);
            }

            zone.Surrendered = false;
            zone2.Surrendered = false;

            PSurrendered.gameObject.SetActive(false);
            ESurrendered.gameObject.SetActive(false);

            Round += 1;
        }
        if(Round == 1 && zone.Surrendered && zone2.Surrendered) //both surrendered in the start of the round
        {
            int PlayerPts = int.Parse(PlayerPoints);
            int EnemyPts = int.Parse(EnemyPoints);

            if(PlayerPts > EnemyPts)
            {
                PlayerVictories += 1;
                WinnerIsPlayer.text = PlayerVictories.ToString();

                player1TurnIndicator.SetActive(false);
                player2TurnIndicator.SetActive(true); 

                leader1.SetActive(true);
                leader2.SetActive(false);

                give1.gameObject.SetActive(true);
                give2.gameObject.SetActive(false);
            }

            if(EnemyPts > PlayerPts)
            {
                EnemyVictories += 1;
                WinnerIsEnemy.text = EnemyVictories.ToString();

                player1TurnIndicator.SetActive(true);
                player2TurnIndicator.SetActive(false); 

                leader1.SetActive(false);
                leader2.SetActive(true);

                give1.gameObject.SetActive(false);
                give2.gameObject.SetActive(true);
            }

            if(PlayerPts == EnemyPts)
            {
                PlayerVictories += 1;
                WinnerIsPlayer.text = PlayerVictories.ToString();
                EnemyVictories += 1;
                WinnerIsEnemy.text = EnemyVictories.ToString();

                player1TurnIndicator.SetActive(false);
                player2TurnIndicator.SetActive(true); 

                leader1.SetActive(true);
                leader2.SetActive(false);

                give1.gameObject.SetActive(true);
                give2.gameObject.SetActive(false);
            }

            zone.Surrendered = false;
            zone2.Surrendered = false;

            PSurrendered.gameObject.SetActive(false);
            ESurrendered.gameObject.SetActive(false);

            Round += 1;
        }
        if(Round == 1 && zone.Surrendered && Hand2 == 0 && PSteals && ESteals)  //player has surrendered & enemy has no cards
        {
            int PlayerPts = int.Parse(PlayerPoints);
            int EnemyPts = int.Parse(EnemyPoints);

            if(PlayerPts > EnemyPts)
            {
                PlayerVictories += 1;
                WinnerIsPlayer.text = PlayerVictories.ToString();

                player1TurnIndicator.SetActive(false);
                player2TurnIndicator.SetActive(true); 

                leader1.SetActive(true);
                leader2.SetActive(false);

                give1.gameObject.SetActive(true);
                give2.gameObject.SetActive(false);
            }

            if(EnemyPts > PlayerPts)
            {
                EnemyVictories += 1;
                WinnerIsEnemy.text = EnemyVictories.ToString();

                player1TurnIndicator.SetActive(true);
                player2TurnIndicator.SetActive(false); 

                leader1.SetActive(false);
                leader2.SetActive(true);

                give1.gameObject.SetActive(false);
                give2.gameObject.SetActive(true);
            }

            if(PlayerPts == EnemyPts)
            {
                PlayerVictories += 1;
                WinnerIsPlayer.text = PlayerVictories.ToString();
                EnemyVictories += 1;
                WinnerIsEnemy.text = EnemyVictories.ToString();

                player1TurnIndicator.SetActive(false);
                player2TurnIndicator.SetActive(true); 

                leader1.SetActive(true);
                leader2.SetActive(false);

                give1.gameObject.SetActive(true);
                give2.gameObject.SetActive(false);
            }

            zone.Surrendered = false;

            PSurrendered.gameObject.SetActive(false);

            Round += 1;
        }
        if(Round == 1 && Hand1 == 0 && zone2.Surrendered && PSteals && ESteals)  //player has no cards & enemy has surrendered 
        {
            int PlayerPts = int.Parse(PlayerPoints);
            int EnemyPts = int.Parse(EnemyPoints);
            
            if(PlayerPts > EnemyPts)
            {
                PlayerVictories += 1;
                WinnerIsPlayer.text = PlayerVictories.ToString();

                player1TurnIndicator.SetActive(false);
                player2TurnIndicator.SetActive(true); 

                leader1.SetActive(true);
                leader2.SetActive(false);

                give1.gameObject.SetActive(true);
                give2.gameObject.SetActive(false);
            }

            if(EnemyPts > PlayerPts)
            {
                EnemyVictories += 1;
                WinnerIsEnemy.text = EnemyVictories.ToString();

                player1TurnIndicator.SetActive(true);
                player2TurnIndicator.SetActive(false); 

                leader1.SetActive(false);
                leader2.SetActive(true);

                give1.gameObject.SetActive(false);
                give2.gameObject.SetActive(true);
            }

            if(PlayerPts == EnemyPts)
            {
                PlayerVictories += 1;
                WinnerIsPlayer.text = PlayerVictories.ToString();
                EnemyVictories += 1;
                WinnerIsEnemy.text = EnemyVictories.ToString();

                player1TurnIndicator.SetActive(false);
                player2TurnIndicator.SetActive(true); 

                leader1.SetActive(true);
                leader2.SetActive(false);

                give1.gameObject.SetActive(true);
                give2.gameObject.SetActive(false);
            }

            zone2.Surrendered = false;

            ESurrendered.gameObject.SetActive(false);

            Round += 1;
        }


        
        
        //second Round
        if(Round == 2 && Hand1 == 0 && Hand2 == 0 && PSteals2 && ESteals2) //second round ends normally
        {
            int PlayerPts = int.Parse(PlayerPoints);
            int EnemyPts = int.Parse(EnemyPoints);

            if(PlayerPts > EnemyPts)
            {
                PlayerVictories += 1;
                WinnerIsPlayer.text = PlayerVictories.ToString();

                player1TurnIndicator.SetActive(false);
                player2TurnIndicator.SetActive(true); 

                leader1.SetActive(true);
                leader2.SetActive(false);

                give1.gameObject.SetActive(true);
                give2.gameObject.SetActive(false);
            }

            if(EnemyPts > PlayerPts)
            {
                EnemyVictories += 1;
                WinnerIsEnemy.text = EnemyVictories.ToString();

                player1TurnIndicator.SetActive(true);
                player2TurnIndicator.SetActive(false); 

                leader1.SetActive(false);
                leader2.SetActive(true);

                give1.gameObject.SetActive(false);
                give2.gameObject.SetActive(true);
            }

            if(PlayerPts == EnemyPts)
            {
                PlayerVictories += 1;
                WinnerIsPlayer.text = PlayerVictories.ToString();
                EnemyVictories += 1;
                WinnerIsEnemy.text = EnemyVictories.ToString();

                player1TurnIndicator.SetActive(false);
                player2TurnIndicator.SetActive(true); 

                leader1.SetActive(true);
                leader2.SetActive(false);

                give1.gameObject.SetActive(true);
                give2.gameObject.SetActive(false);
            }

            Round += 1;
        }
        if(Round == 2 && zone.Surrendered && zone2.Surrendered && PSteals2 && ESteals2) //both surrendered in the middle of the round
    {
            int PlayerPts = int.Parse(PlayerPoints);
            int EnemyPts = int.Parse(EnemyPoints);

            if(PlayerPts > EnemyPts)
            {
                PlayerVictories += 1;
                WinnerIsPlayer.text = PlayerVictories.ToString();

                player1TurnIndicator.SetActive(false);
                player2TurnIndicator.SetActive(true); 

                leader1.SetActive(true);
                leader2.SetActive(false);

                give1.gameObject.SetActive(true);
                give2.gameObject.SetActive(false);
            }

            if(EnemyPts > PlayerPts)
            {
                EnemyVictories += 1;
                WinnerIsEnemy.text = EnemyVictories.ToString();

                player1TurnIndicator.SetActive(true);
                player2TurnIndicator.SetActive(false); 

                leader1.SetActive(false);
                leader2.SetActive(true);

                give1.gameObject.SetActive(false);
                give2.gameObject.SetActive(true);
            }

            if(PlayerPts == EnemyPts)
            {
                PlayerVictories += 1;
                WinnerIsPlayer.text = PlayerVictories.ToString();
                EnemyVictories += 1;
                WinnerIsEnemy.text = EnemyVictories.ToString();

                player1TurnIndicator.SetActive(false);
                player2TurnIndicator.SetActive(true); 

                leader1.SetActive(true);
                leader2.SetActive(false);

                give1.gameObject.SetActive(true);
                give2.gameObject.SetActive(false);
            }

            zone.Surrendered = false;
            zone2.Surrendered = false;

            PSurrendered.gameObject.SetActive(false);
            ESurrendered.gameObject.SetActive(false);

            Round += 1;
        }
        if(Round == 2 && zone.Surrendered && zone2.Surrendered) //both surrendered in the start of the round
        {
            int PlayerPts = int.Parse(PlayerPoints);
            int EnemyPts = int.Parse(EnemyPoints);

            if(PlayerPts > EnemyPts)
            {
                PlayerVictories += 1;
                WinnerIsPlayer.text = PlayerVictories.ToString();

                player1TurnIndicator.SetActive(false);
                player2TurnIndicator.SetActive(true); 

                leader1.SetActive(true);
                leader2.SetActive(false);

                give1.gameObject.SetActive(true);
                give2.gameObject.SetActive(false);
            }

            if(EnemyPts > PlayerPts)
            {
                EnemyVictories += 1;
                WinnerIsEnemy.text = EnemyVictories.ToString();

                player1TurnIndicator.SetActive(true);
                player2TurnIndicator.SetActive(false); 

                leader1.SetActive(false);
                leader2.SetActive(true);

                give1.gameObject.SetActive(false);
                give2.gameObject.SetActive(true);
            }

            if(PlayerPts == EnemyPts)
            {
                PlayerVictories += 1;
                WinnerIsPlayer.text = PlayerVictories.ToString();
                EnemyVictories += 1;
                WinnerIsEnemy.text = EnemyVictories.ToString();

                player1TurnIndicator.SetActive(false);
                player2TurnIndicator.SetActive(true); 

                leader1.SetActive(true);
                leader2.SetActive(false);

                give1.gameObject.SetActive(true);
                give2.gameObject.SetActive(false);
            }

            zone.Surrendered = false;
            zone2.Surrendered = false;

            PSurrendered.gameObject.SetActive(false);
            ESurrendered.gameObject.SetActive(false);

            Round += 1;
        }
        if(Round == 1 && zone.Surrendered && Hand2 == 0 && PSteals && ESteals)  //player has surrendered & enemy has no cards
        {
            int PlayerPts = int.Parse(PlayerPoints);
            int EnemyPts = int.Parse(EnemyPoints);

            if(PlayerPts > EnemyPts)
            {
                PlayerVictories += 1;
                WinnerIsPlayer.text = PlayerVictories.ToString();

                player1TurnIndicator.SetActive(false);
                player2TurnIndicator.SetActive(true); 

                leader1.SetActive(true);
                leader2.SetActive(false);

                give1.gameObject.SetActive(true);
                give2.gameObject.SetActive(false);
            }

            if(EnemyPts > PlayerPts)
            {
                EnemyVictories += 1;
                WinnerIsEnemy.text = EnemyVictories.ToString();

                player1TurnIndicator.SetActive(true);
                player2TurnIndicator.SetActive(false); 

                leader1.SetActive(false);
                leader2.SetActive(true);

                give1.gameObject.SetActive(false);
                give2.gameObject.SetActive(true);
            }

            if(PlayerPts == EnemyPts)
            {
                PlayerVictories += 1;
                WinnerIsPlayer.text = PlayerVictories.ToString();
                EnemyVictories += 1;
                WinnerIsEnemy.text = EnemyVictories.ToString();

                player1TurnIndicator.SetActive(false);
                player2TurnIndicator.SetActive(true); 

                leader1.SetActive(true);
                leader2.SetActive(false);

                give1.gameObject.SetActive(true);
                give2.gameObject.SetActive(false);
            }

            zone.Surrendered = false;

            PSurrendered.gameObject.SetActive(false);

            Round += 1;
        }
        if(Round == 2 && Hand1 == 0 && zone2.Surrendered && PSteals2 && ESteals2) //player has no cards and enemy surrendered
        {
            int PlayerPts = int.Parse(PlayerPoints);
            int EnemyPts = int.Parse(EnemyPoints);
            
            if(PlayerPts > EnemyPts)
            {
                PlayerVictories += 1;
                WinnerIsPlayer.text = PlayerVictories.ToString();

                player1TurnIndicator.SetActive(false);
                player2TurnIndicator.SetActive(true); 

                leader1.SetActive(true);
                leader2.SetActive(false);

                give1.gameObject.SetActive(true);
                give2.gameObject.SetActive(false);
            }

            if(EnemyPts > PlayerPts)
            {
                EnemyVictories += 1;
                WinnerIsEnemy.text = EnemyVictories.ToString();

                player1TurnIndicator.SetActive(true);
                player2TurnIndicator.SetActive(false); 

                leader1.SetActive(false);
                leader2.SetActive(true);

                give1.gameObject.SetActive(false);
                give2.gameObject.SetActive(true);
            }

            if(PlayerPts == EnemyPts)
            {
                PlayerVictories += 1;
                WinnerIsPlayer.text = PlayerVictories.ToString();
                EnemyVictories += 1;
                WinnerIsEnemy.text = EnemyVictories.ToString();

                player1TurnIndicator.SetActive(false);
                player2TurnIndicator.SetActive(true); 

                leader1.SetActive(true);
                leader2.SetActive(false);

                give1.gameObject.SetActive(true);
                give2.gameObject.SetActive(false);
            }

            zone2.Surrendered = false;

            ESurrendered.gameObject.SetActive(false);

            Round += 1;
        }



        
        //third Round
        if(Round == 3 && Hand1 == 0 && Hand2 == 0 && PSteals3 && ESteals3) //3rd round ends normally
        {
            int PlayerPts = int.Parse(PlayerPoints);
            int EnemyPts = int.Parse(EnemyPoints);

            if(PlayerPts >= EnemyPts)
            {
                PlayerVictories += 1;
                WinnerIsPlayer.text = PlayerVictories.ToString();
            }

            if(EnemyPts >= PlayerPts)
            {
                EnemyVictories += 1;
                WinnerIsEnemy.text = EnemyVictories.ToString();
            }

            player1TurnIndicator.SetActive(true);
            player2TurnIndicator.SetActive(true); 

            leader1.SetActive(false);
            leader2.SetActive(false);

            give1.gameObject.SetActive(false);
            give2.gameObject.SetActive(false);

            Round = 0;
        }
        if(Round == 3 && zone.Surrendered && zone2.Surrendered && PSteals3 && ESteals3) //both surrendered in the middle of the round
        {
            int PlayerPts = int.Parse(PlayerPoints);
            int EnemyPts = int.Parse(EnemyPoints);

            if(PlayerPts >= EnemyPts)
            {
                PlayerVictories += 1;
                WinnerIsPlayer.text = PlayerVictories.ToString();
            }

            if(EnemyPts >= PlayerPts)
            {
                EnemyVictories += 1;
                WinnerIsEnemy.text = EnemyVictories.ToString();
            }

            player1TurnIndicator.SetActive(true);
            player2TurnIndicator.SetActive(true);  

            leader1.SetActive(false);
            leader2.SetActive(false);

            give1.gameObject.SetActive(false);
            give2.gameObject.SetActive(false);

            zone.Surrendered = false;
            zone2.Surrendered = false;

            PSurrendered.gameObject.SetActive(false);
            ESurrendered.gameObject.SetActive(false);

            Round = 0;
        }
        if(Round == 3 && zone.Surrendered && zone2.Surrendered) //both surrendered in the start of the round
        {
            int PlayerPts = int.Parse(PlayerPoints);
            int EnemyPts = int.Parse(EnemyPoints);

            if(PlayerPts >= EnemyPts)
            {
                PlayerVictories += 1;
                WinnerIsPlayer.text = PlayerVictories.ToString();
            }

            if(EnemyPts >= PlayerPts)
            {
                EnemyVictories += 1;
                WinnerIsEnemy.text = EnemyVictories.ToString();
            }

            player1TurnIndicator.SetActive(true);
            player2TurnIndicator.SetActive(true);  

            leader1.SetActive(false);
            leader2.SetActive(false);

            give1.gameObject.SetActive(false);
            give2.gameObject.SetActive(false);

            zone.Surrendered = false;
            zone2.Surrendered = false;

            PSurrendered.gameObject.SetActive(false);
            ESurrendered.gameObject.SetActive(false);

            Round = 0;
        }
        if(Round == 3 && zone.Surrendered && Hand2 == 0 && PSteals3 && ESteals3) //player has surrendered and enemy has no cards
        {
            int PlayerPts = int.Parse(PlayerPoints);
            int EnemyPts = int.Parse(EnemyPoints);

            if(PlayerPts >= EnemyPts)
            {
                PlayerVictories += 1;
                WinnerIsPlayer.text = PlayerVictories.ToString();
            }

            if(EnemyPts >= PlayerPts)
            {
                EnemyVictories += 1;
                WinnerIsEnemy.text = EnemyVictories.ToString();
            }

            player1TurnIndicator.SetActive(true);
            player2TurnIndicator.SetActive(true); 

            leader1.SetActive(false);
            leader2.SetActive(false);

            give1.gameObject.SetActive(false);
            give2.gameObject.SetActive(false);

            zone.Surrendered = false;
            zone2.Surrendered = false;

            PSurrendered.gameObject.SetActive(false);
            ESurrendered.gameObject.SetActive(false);

            Round = 0;
        }
        if(Round == 3 && Hand1 == 0 && zone2.Surrendered && PSteals3 && ESteals3)//player has no cards and enemy surrendered
        {
            int PlayerPts = int.Parse(PlayerPoints);
            int EnemyPts = int.Parse(EnemyPoints);

            if(PlayerPts >= EnemyPts)
            {
                PlayerVictories += 1;
                WinnerIsPlayer.text = PlayerVictories.ToString();
            }

            if(EnemyPts >= PlayerPts)
            {
                EnemyVictories += 1;
                WinnerIsEnemy.text = EnemyVictories.ToString();
            }

            player1TurnIndicator.SetActive(true);
            player2TurnIndicator.SetActive(true); 

            leader1.SetActive(false);
            leader2.SetActive(false);

            give1.gameObject.SetActive(false);
            give2.gameObject.SetActive(false);

            zone.Surrendered = false;
            zone2.Surrendered = false;

            PSurrendered.gameObject.SetActive(false);
            ESurrendered.gameObject.SetActive(false);

            Round = 0;
        }
   }
//}
}

