using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Runtime.CompilerServices;
using System;

public class Hand
{
    public List<Card> PlayerHand { get; private set; } = new();
    public List<Card> GameDeck { get; private set; }

    public Hand(DeckCreator initialDeck)
    {
        GameDeck = initialDeck.CardDeck;
        FillDeck();
    }

    private void FillDeck()
    {
        for (int i = 0; i < 10; i++)
        {
            Card temp = RandomChoice(GameDeck);
            GameDeck.Remove(temp);
            PlayerHand.Add(temp);
        }
    }

    public void DrawCard()
    {
        Card temp = RandomChoice(GameDeck);

        if (PlayerHand.Count < 10)
        {
            PlayerHand.Add(temp);
            GameDeck.Remove(temp);
            Debug.Log($"You drew the card {temp.Name}.");
        }
        else
        {
            GameDeck.Remove(temp);
            Debug.Log("You can't have more than 10 cards in your hand.");
            Debug.Log($"Actually you have {PlayerHand.Count} cards in your hand.");
        }
    }

    public static Card RandomChoice(List<Card> cards)
    {
        System.Random random = new System.Random();

        if (cards == null || cards.Count == 0)
        {
            throw new ArgumentException("The list of cards is empty.");
        }

        int index = random.Next(cards.Count - 1);
        return (cards[index]);
    }

    public void RemoveCardFromDeck(Card card)
    {
        Card toRemove = null;

        foreach (Card cardToRemove in GameDeck)
        {
            if (cardToRemove.Name == card.Name)
            {
                toRemove = cardToRemove;
            }
        }

        if (toRemove != null)
        {
            GameDeck.Remove(toRemove);
        }
    }
}

