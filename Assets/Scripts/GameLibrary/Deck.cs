using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Reflection;
using System.Threading;
using System;

public class DeckCreator
{
    public string Faction { get; private set; } = string.Empty;
    public Card DeckLeader { get; private set; }
    public List<Card> CardDeck { get; private set; } = new();
    public int CardsTotalNumber { get; private set; }
    public int UnityCardsTotalNumber { get; private set; }
    public int HeroCardsTotalNumber{ get; private set; }
    public int SpecialCardsTotalNumber { get; private set; }
    public int UnityPowerTotalNumber { get; private set; }

    public DeckCreator(string faction, Dictionary<string, List<Card>> factionCards, Dictionary<string, Card> allLeaders)
    {
        Faction = faction;
        DeckLeader = allLeaders[faction];
        CardDeck = factionCards[faction];

        UpdateDeckInfo();
    }

    public void DuplicateCard(Card card)
    {
        if (card is Silver silverCard)
        {
            if (CardActualAppearances(silverCard) < Silver.possibleZones)
            {
                Card copy = new Silver(card);
                CardDeck.Add(copy);
                UpdateDeckInfo();
            }
            else
            {
                Debug.Log("You can't have more than 2 copies of a silver card in your deck.");
            }
        }
        else 
        {
            Debug.Log("You can't duplicate this card.");
        }
    }

    public void AddCardToDeck(Card card)
    {
        if (card.Faction == Faction || card.Faction == "Neutral")
        {
            if (!(card is Hero && CardDeck.Contains(card)))
            {
                CardDeck.Add(card);
                UpdateDeckInfo();
            } 
        }
        else
        {
            Console.WriteLine("You can't add this card to your deck.");
        }
    }

    public void RemoveCardOfDeck(Card card)
    {
        foreach (Card cards in CardDeck)
        {
            if (cards.Name == card.Name)
            {
                CardDeck.Remove(cards);
                UpdateDeckInfo();
                return;
            }
        }
    }

    private void UpdateDeckInfo()
    {
        CardsTotalNumber = CardDeck.Count;
        UnityCardsTotalNumber = UnityCardsCounter();
        HeroCardsTotalNumber = HeroCardsCounter();
        SpecialCardsTotalNumber = SpecialCardsCounter();
        UnityPowerTotalNumber = TotalPowerCounter();
    }

    private int UnityCardsCounter()
    {
        int unityCardsCount = 0;

        foreach (Card cards in CardDeck)
        {
            if (cards is UnityCard)
            {
                unityCardsCount++;
            }
        }
        return unityCardsCount;
    }

    private int HeroCardsCounter()
    {
        int heroCardsCount = 0;

        foreach (Card cards in CardDeck)
        {
            if (cards is Hero)
            {
                heroCardsCount++;
            }
        }
        return heroCardsCount;
    }

    private int SpecialCardsCounter()
    {
        int specialCardsCount = 0;

        foreach (Card cards in CardDeck)
        {
            if (cards is Special)
            {
                specialCardsCount++;
            }
        }
        return specialCardsCount;
    }

    private int TotalPowerCounter()
    {
        int totalUnityPower = 0;

        foreach (Card cards in CardDeck)
        {
            if (cards is UnityCard unityCard)
            {
                totalUnityPower += unityCard.Power;
            }
        }
        return totalUnityPower;
    }

    public int CardActualAppearances(Card cardToCount)
    {
        int appearances = 0;

        foreach (Card cards in CardDeck)
        {
            if (cards.Name == cardToCount.Name)
            {
                appearances++;
            }
        }
        return appearances;
    }
}
