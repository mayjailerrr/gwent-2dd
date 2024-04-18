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

//     private int PlayerArea;
//     private int EnemyArea;

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
        if(GameOver == false && PlayerVictories == 2)
        {
            GameOver = true;
            WinnerText.text = "Player is the winner siu";
        }
        else if(GameOver == false && EnemyVictories == 2)
        {
            GameOver = true;
            WinnerText.text = "Enemy is the winner siu";
        }
        else if(GameOver == false && PlayerVictories == EnemyVictories && EnemyVictories == 2)
        {
            GameOver = true;
            WinnerText.text = "Tie";
        }
    }

        //first Round
    public void WhoWon()
    {               
        if(Round == 1 && Hand1 == 0 && Hand2 == 0 && PSteals && ESteals) //1st round finished
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

            Round += 1;
        }
        // if(Round == 1 && PlayerSurrendered && EnemySurrendered && PSteals && ESteals) //both surrendered
        // {
        //         int PlayerPts = int.Parse(PlayerPoints);
        //         int EnemyPts = int.Parse(EnemyPoints);
        //         if(PlayerPts >= EnemyPts)
        //         {
        //         PlayerVictories += 1;
        //         WinnerIsPlayer.text = PlayerVictories.ToString();
        //         }

        //         if(EnemyPts >= PlayerPts)
        //         {
        //         EnemyVictories += 1;
        //         WinnerIsEnemy.text = EnemyVictories.ToString();
        //         }
        //         Round += 1;
        // }
        // if(Round == 1 && PlayerSurrendered && EnemyArea == 0 && PSteals && ESteals)  //player has surrendered & enemy has no cards
        // {
        //         int PlayerPts = int.Parse(PlayerPoints);
        //         int EnemyPts = int.Parse(EnemyPoints);
        //         if(PlayerPts >= EnemyPts)
        //         {
        //         PlayerVictories += 1;
        //         WinnerIsPlayer.text = PlayerVictories.ToString();
        //         }

        //         if(EnemyPts >= PlayerPts)
        //         {
        //         EnemyVictories += 1;
        //         WinnerIsEnemy.text = EnemyVictories.ToString();
        //         }
        //         Round += 1;
        // }
        // if(Round == 1 && PlayerArea == 0 && EnemySurrendered && PSteals && ESteals)  //player has no cards & enemy has surrendered 
        // {
        //         int PlayerPts = int.Parse(PlayerPoints);
        //         int EnemyPts = int.Parse(EnemyPoints);
        //         if(PlayerPts >= EnemyPts)
        //         {
        //         PlayerVictories += 1;
        //         WinnerIsPlayer.text = PlayerVictories.ToString();
        //         }

        //         if(EnemyPts >= PlayerPts)
        //         {
        //         EnemyVictories += 1;
        //         WinnerIsEnemy.text = EnemyVictories.ToString();
        //         }
        //         Round += 1;
        // }


        
        
        // //second Round
        // if(Round == 2 && PlayerArea == 0 && EnemyArea == 0 && PSteals2 && ESteals2)
        // {
        //         int PlayerPts = int.Parse(PlayerPoints);
        //         int EnemyPts = int.Parse(EnemyPoints);
        //         if(PlayerPts >= EnemyPts)
        //         {
        //         PlayerVictories += 1;
        //         WinnerIsPlayer.text = PlayerVictories.ToString();
        //         }

        //         if(EnemyPts >= PlayerPts)
        //         {
        //         EnemyVictories += 1;
        //         WinnerIsEnemy.text = EnemyVictories.ToString();
        //         }
        //         Round += 1;
        // }
        // if(Round == 2 && PlayerSurrendered && EnemySurrendered && PSteals2 && ESteals2)
        // {
        //         int PlayerPts = int.Parse(PlayerPoints);
        //         int EnemyPts = int.Parse(EnemyPoints);
        //         if(PlayerPts >= EnemyPts)
        //         {
        //         PlayerVictories += 1;
        //         WinnerIsPlayer.text = PlayerVictories.ToString();
        //         }

        //         if(EnemyPts >= PlayerPts)
        //         {
        //         EnemyVictories += 1;
        //         WinnerIsEnemy.text = EnemyVictories.ToString();
        //         }
        //         Round += 1;
        // }
        // if(Round == 2 && PlayerSurrendered && EnemyArea == 0 && PSteals2 && ESteals2)
        // {
        //         int PlayerPts = int.Parse(PlayerPoints);
        //         int EnemyPts = int.Parse(EnemyPoints);
        //         if(PlayerPts >= EnemyPts)
        //         {
        //         PlayerVictories += 1;
        //         WinnerIsPlayer.text = PlayerVictories.ToString();
        //         }

        //         if(EnemyPts >= PlayerPts)
        //         {
        //         EnemyVictories += 1;
        //         WinnerIsEnemy.text = EnemyVictories.ToString();
        //         }
        //         Round += 1;
        // }
        // if(Round == 2 && PlayerArea == 0 && EnemySurrendered && PSteals2 && ESteals2)
        // {
        //         int PlayerPts = int.Parse(PlayerPoints);
        //         int EnemyPts = int.Parse(EnemyPoints);
        //         if(PlayerPts >= EnemyPts)
        //         {
        //         PlayerVictories += 1;
        //         WinnerIsPlayer.text = PlayerVictories.ToString();
        //         }

        //         if(EnemyPts >= PlayerPts)
        //         {
        //         EnemyVictories += 1;
        //         WinnerIsEnemy.text = EnemyVictories.ToString();
        //         }
        //         Round += 1;
        // }



        
        // //third Round
        // if(Round == 3 && PlayerArea == 0 && EnemyArea == 0 && PSteals3 && ESteals3)
        // {
        //         int PlayerPts = int.Parse(PlayerPoints);
        //         int EnemyPts = int.Parse(EnemyPoints);
        //         if(PlayerPts >= EnemyPts)
        //         {
        //         PlayerVictories += 1;
        //         WinnerIsPlayer.text = PlayerVictories.ToString();
        //         }

        //         if(EnemyPts >= PlayerPts)
        //         {
        //         EnemyVictories += 1;
        //         WinnerIsEnemy.text = EnemyVictories.ToString();
        //         }
        //         Round = 0;
        // }
        // if(Round == 3 && PlayerSurrendered && EnemySurrendered && PSteals3 && ESteals3)
        // {
        //         int PlayerPts = int.Parse(PlayerPoints);
        //         int EnemyPts = int.Parse(EnemyPoints);
        //         if(PlayerPts >= EnemyPts)
        //         {
        //         PlayerVictories += 1;
        //         WinnerIsPlayer.text = PlayerVictories.ToString();
        //         }

        //         if(EnemyPts >= PlayerPts)
        //         {
        //         EnemyVictories += 1;
        //         WinnerIsEnemy.text = EnemyVictories.ToString();
        //         }
        //         Round = 0;
        // }
        // if(Round == 3 && PlayerSurrendered && EnemyArea == 0 && PSteals3 && ESteals3)
        // {
        //         int PlayerPts = int.Parse(PlayerPoints);
        //         int EnemyPts = int.Parse(EnemyPoints);
        //         if(PlayerPts >= EnemyPts)
        //         {
        //         PlayerVictories += 1;
        //         WinnerIsPlayer.text = PlayerVictories.ToString();
        //         }

        //         if(EnemyPts >= PlayerPts)
        //         {
        //         EnemyVictories += 1;
        //         WinnerIsEnemy.text = EnemyVictories.ToString();
        //         }
        //         Round = 0;
        // }
        // if(Round == 3 && PlayerArea == 0 && EnemySurrendered && PSteals3 && ESteals3)
        // {
        //         int PlayerPts = int.Parse(PlayerPoints);
        //         int EnemyPts = int.Parse(EnemyPoints);
        //         if(PlayerPts >= EnemyPts)
        //         {
        //         PlayerVictories += 1;
        //         WinnerIsPlayer.text = PlayerVictories.ToString();
        //         }

        //         if(EnemyPts >= PlayerPts)
        //         {
        //         EnemyVictories += 1;
        //         WinnerIsEnemy.text = EnemyVictories.ToString();
        //         }
        //         Round = 0;
        // }
   }
}

