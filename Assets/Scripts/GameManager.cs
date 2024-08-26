using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Threading.Tasks;
using Unity.VisualScripting;
using System;

public class GameManager : Subject
{
    public static bool GameOver = false;
    public static GameManager gameManager;
    public static Player player;
    public static Player opponent;

    public static GameManager AwakeManager()
    {
        return new GameManager();
    }

    private GameManager()
    {
        gameManager = this;

        player = GameData.Player1;
        opponent = GameData.Opponent;

        PickPlayerAsFirst(player, opponent);

        if (player.IsActive == true)
            Debug.Log($"Player {player.PlayerName} is the first to play.");

        if (opponent.IsActive == true)
            Debug.Log($"Player {opponent.PlayerName} is the first to play.");
        
        NotifyObservers(GameEvents.Start);
    }

    public void PlayCard(Card card, ZoneTypes zone)
    {
        if (player.IsActive == true)
            InternalPlayCard(player, opponent, card, zone);
        else InternalPlayCard(opponent, player, card, zone);
    }

    private void InternalPlayCard(Player activePlayer, Player opponentPlayer, Card card, ZoneTypes zone)
    {
        activePlayer.DragAndDrop(activePlayer, opponentPlayer, card, zone);

        Debug.Log($"Player {activePlayer.PlayerName} played {card.Name} to the {zone} zone.");

        if (card.Type == CardTypes.Lure)
            NotifyObservers(GameEvents.DecoyEventStart);
        else 
        {
            NotifyObservers(GameEvents.Summon, card);
            ChangeTurn(activePlayer, opponentPlayer);
        }
    }

    public void UseLeaderAbility()
    {
        if (player.IsActive == true)
            InternalUseLeaderAbility(player, opponent);
        else InternalUseLeaderAbility(opponent, player);
    }

    private void InternalUseLeaderAbility(Player activePlayer, Player opponentPlayer)
    {
        activePlayer.UseLeaderAbility(activePlayer, opponentPlayer);

        NotifyObservers(GameEvents.Summon, activePlayer.PlayerLeader); //playerleader

        Debug.Log($"Player {activePlayer.PlayerName} used the leader ability.");
        ChangeTurn(activePlayer, opponentPlayer);
    }

    public void PassTurn()
    {
        if (player.IsActive == true)
            InternalPassTurn(player, opponent);
        else if (opponent.IsActive == true)
            InternalPassTurn(opponent, player);
    }

    private void InternalPassTurn(Player activePlayer, Player opponentPlayer)
    {
        activePlayer.PassTurn();

        if (opponentPlayer.HasPassed == false)
        {
            NotifyObservers(GameEvents.PassTurn);
            opponentPlayer.StartTurn();
        }
        else BothPlayersPassed();
    }

    public void FinishDecoyEvent(Card cardToSwap)
    {
        if (player.IsActive == true)
            InternalFinishDecoyEvent(player, opponent, cardToSwap);
        else InternalFinishDecoyEvent(opponent, player, cardToSwap);
    }

    private void InternalFinishDecoyEvent(Player activePlayer, Player opponentPlayer, Card cardToSwap)
    {
        int pos = -1;

        for (int i = 0; i < 3; i++)
        {
            foreach (UnityCard unityCard in activePlayer.Battlefield.GetZoneFromBattlefield(Utils.ZoneForIndex[i]))
            {
                if (unityCard.Type == CardTypes.Lure)
                {
                    pos = i;
                }
            }
        }

        if (pos >= 0)
        {
            if (cardToSwap is UnityCard unityCard)
            {
                activePlayer.Battlefield.RemoveCardFromBattlefield(unityCard, Utils.ZoneForIndex[pos]);
                activePlayer.PlayerHand.PlayerHand.Add(unityCard);

                if (unityCard is Silver silverCard)
                    silverCard.initialPower = silverCard.Power;

                activePlayer.Battlefield.UpdateBattlefield();
                opponentPlayer.Battlefield.UpdateBattlefield();

                NotifyObservers(GameEvents.DecoyEventEnd, unityCard);
                ChangeTurn(activePlayer, opponentPlayer);
            }
        }
    }

    public void AbortDecoyEvent()
    {
        if (player.IsActive == true)
            InternalAbortDecoyEvent(player, opponent);
        else InternalAbortDecoyEvent(opponent, player);
    }

    private void InternalAbortDecoyEvent(Player activePlayer, Player opponentPlayer)
    {
        activePlayer.Battlefield.UpdateBattlefield();
        opponentPlayer.Battlefield.UpdateBattlefield();

        NotifyObservers(GameEvents.DecoyEventAbort);
        ChangeTurn(activePlayer, opponentPlayer);
    }

    public void InvokeCard(Card card, ZoneTypes zone)
    {
        if (player.IsActive == true)
            InternalInvokeCard(player, opponent, card, zone);
        else InternalInvokeCard(opponent, player, card, zone);
    }

    private void InternalInvokeCard(Player activePlayer, Player opponentPlayer, Card card, ZoneTypes zone)
    {
        InvokeMovement(activePlayer, opponentPlayer, card, zone);
        Debug.Log($"Player {activePlayer.PlayerName} invoked {card.Name} to the {zone} zone.");

        NotifyObservers(GameEvents.Invoke, card);
    }

    private void InvokeMovement(Player activePlayer, Player opponentPlayer, Card card, ZoneTypes zone)
    {
        if (zone == ZoneTypes.Melee || zone == ZoneTypes.Distance || zone == ZoneTypes.Siege)
        {
            if (card is Augment augmentCard)
            {
                activePlayer.Battlefield.AddCardToAugmentColumn(augmentCard, zone);
                activePlayer.PlayerHand.RemoveCardFromDeck(card);

                activePlayer.Battlefield.UpdateBattlefield();
                opponentPlayer.Battlefield.UpdateBattlefield();
            }
            if (card is WeatherCard weatherCard)
            {
                PlayerBattlefield.AddWeatherCardToZone(weatherCard, zone);
                activePlayer.PlayerHand.RemoveCardFromDeck(card);

                activePlayer.Battlefield.UpdateBattlefield();
                opponentPlayer.Battlefield.UpdateBattlefield();
            }
        }
    }

    public void ChangeTurn(Player activePlayer, Player opponentPlayer)
    {
        if (activePlayer.HasPassed == false || opponentPlayer.HasPassed == false)
        {
            if (activePlayer.IsActive == true)
                InternalChangeTurn(activePlayer, opponentPlayer);
            else InternalChangeTurn(opponentPlayer, activePlayer);
        }
        else 
        {
            throw new ArgumentException("Both players have passed.");
        }
    }

    private void InternalChangeTurn(Player activePlayer, Player opponentPlayer)
    {
        if (activePlayer.HasPassed == false)
        {
            activePlayer.EndTurn();
            opponentPlayer.StartTurn();
        }
    }

    public void PickPlayerAsFirst(Player player1, Player player2)
    {
        System.Random random = new System.Random();
        
        int choice = random.Next(2);

        if (choice == 0) player1.StartTurn();
        else player2.StartTurn();

    }

    public void BothPlayersPassed()
    {
        if (player.GamesWon < 2 && opponent.GamesWon < 2)
        {
            NotifyObservers(GameEvents.FinishRound);
            FinishRound(player, opponent);
            NotifyObservers(GameEvents.StartRound);
        }
    }

    public void FinishRound(Player player1, Player player2)
    {
        SetRoundWinner(player1, player2);

        player1.GetNewBattlefield();
        player2.GetNewBattlefield();

        if (!GameOver)
        {
            player1.PlayerHand.DrawCard();
            player2.PlayerHand.DrawCard();

            player1.PlayerHand.DrawCard();
            player2.PlayerHand.DrawCard();

            NotifyObservers(GameEvents.DrawCard);
        }
    }

    private void SetRoundWinner(Player player1, Player player2)
    {
        if (player1.Battlefield.TotalScore > player2.Battlefield.TotalScore)
            InternalSetRoundWinner(player1, player2);
        else if (player2.Battlefield.TotalScore > player1.Battlefield.TotalScore)
            InternalSetRoundWinner(player2, player1);
        else
        {
            player1.AddVictory();
            player2.AddVictory();

            if (player1.GamesWon == 2 || player2.GamesWon == 2)
            {
                NotifyObservers(GameEvents.FinishGame);
                GameOver = true;
            }

            else 
            {
                player1.NewRound();
                player2.NewRound();

                PickPlayerAsFirst(player1, player2);
                NotifyObservers(GameEvents.Start);
            }
        }
    }

    private void InternalSetRoundWinner(Player winner, Player loser)
    {
        winner.AddVictory();

        if (winner.GamesWon == 2)
        {
            NotifyObservers(GameEvents.FinishGame);
            GameOver = true;
        }
        else 
        {
            winner.NewRound();
            loser.NewRound();

            winner.StartTurn();
        }
    }
}

 public enum GameEvents
{
    Start,
    Summon,
    Invoke,
    PassTurn,
    DecoyEventStart,
    DecoyEventEnd,
    DecoyEventAbort,
    FinishRound,
    StartRound,
    DrawCard,
    FinishGame
}

