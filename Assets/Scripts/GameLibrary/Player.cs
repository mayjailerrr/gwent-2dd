using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Net.WebSockets;
using Unity.VisualScripting;
using System.Dynamic;
using System;

public class Player
{
    public string PlayerName { get; private set; }
    public Hand PlayerHand { get; private set; }
    public string PlayerFaction { get; private set; }
    public Card PlayerLeader { get; private set; }
    public PlayerBattlefield Battlefield { get; private set; }
    public bool HasPassed { get; private set; }
    public int GamesWon { get; private set; }
    public bool IsActive { get; private set; }

    public Player(string name, string faction, DeckCreator initialDeck)
    {
        PlayerName = name;
        PlayerFaction = faction;
        PlayerLeader = initialDeck.DeckLeader;
        PlayerHand = new Hand(initialDeck);
        Battlefield = new();
        HasPassed = false;
        GamesWon = 0;
        IsActive = false;
    }

    public void UseLeaderAbility(Player activePlayer, Player opponentPLayer)
    {
        PlayerLeader.ActivateEffect(activePlayer, opponentPLayer, PlayerLeader);
        activePlayer.Battlefield.UpdateBattlefield();
        opponentPLayer.Battlefield.UpdateBattlefield();
    }
    
    public void PassTurn()
    {
        HasPassed = true;
        IsActive = false;
    }

    public void AddVictory()
    {
        GamesWon++;
    }

    public void StartTurn()
    {
        IsActive = true;
    }

    public void EndTurn()
    {
        IsActive = false;
    }

    public void NewRound()
    {
        HasPassed = false;
    }

    public void DragAndDrop(Player activePlayer, Player opponentPlayer, Card card, ZoneTypes zone)
    {
        if (zone == ZoneTypes.Melee || zone == ZoneTypes.Distance || zone == ZoneTypes.Siege)
        {
            if (card is UnityCard unityCard)
            {
                Battlefield.AddCardToBattlefield(unityCard, zone);
                RemoveCardAndActivateEffect(activePlayer, opponentPlayer, unityCard);
            }
            if (card is Augment augmentCard)
            {
                Battlefield.AddCardToAugmentColumn(augmentCard, zone);
                RemoveCardAndActivateEffect(activePlayer, opponentPlayer, augmentCard);
            }
            if (card is WeatherCard weatherCard)
            {
                PlayerBattlefield.AddWeatherCardToZone(weatherCard, zone);
                RemoveCardAndActivateEffect(activePlayer, opponentPlayer, weatherCard);
            }
            if (card is Clearance clearanceCard)
            {
                RemoveCardAndActivateEffect(activePlayer, opponentPlayer, clearanceCard);
            }
        }
        else
        {
            throw new ArgumentException("The zone is not valid.");
        }
    }

    private void RemoveCardAndActivateEffect(Player activePlayer, Player opponentPlayer, Card card)
    {
        PlayerHand.PlayerHand.Remove(card);

        activePlayer.Battlefield.UpdateBattlefield();
        opponentPlayer.Battlefield.UpdateBattlefield();

        card.ActivateEffect(activePlayer, opponentPlayer, card);

        activePlayer.Battlefield.UpdateBattlefield();
        opponentPlayer.Battlefield.UpdateBattlefield();
    }

    public void GetNewBattlefield()
    {
        Battlefield.ClearBattlefield();
        Battlefield.UpdateBattlefield();
    }
}
